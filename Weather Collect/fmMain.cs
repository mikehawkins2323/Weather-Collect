using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Weather_Collect
{
    public partial class fmMain : Form
    {
        public fmMain()
        {
            InitializeComponent();
        }

        private void fmMain_Load(object sender, EventArgs e)
        {
            setInitialValues();
        }
        private void btnSaveLocationSel_Click(object sender, EventArgs e)
        {

            FolderBrowserDialog fbdFolderBrowser = new FolderBrowserDialog();
            fbdFolderBrowser.SelectedPath = txtSaveLocation.Text.Replace(@"\Charts", "");
            fbdFolderBrowser.Description = "Select the location to downlaod charts";
            DialogResult result = fbdFolderBrowser.ShowDialog();
            if(result == DialogResult.OK)
            {
                txtSaveLocation.Text = fbdFolderBrowser.SelectedPath + @"\Charts";
            }
        }
        private void btnDownload_Click(object sender, EventArgs e)
        {
            enableControls(false);
            downloadCharts();
        }

        private void setInitialValues()
        {
            txtSaveLocation.Text = getPublicFolderDirectory() + @"\Charts";
            cmbArea.SelectedIndex = 0;
        }

        private async void downloadCharts()
        {
            //get parameters
            bool blFileHDSelect = chkHighDef.Checked;
            string[] strSaveLocations = createSaveDirectories(txtSaveLocation.Text);
            lblFileDownloading.Text = "Collecting download information...";
            List<chartInfo> lstChartInfo = getDownloadAddresses();
            //create objects and vars
            WebClient wc = new WebClient();
            int intTotalFiles = lstChartInfo.Count;
            PdfDocument doc = new PdfDocument();
            int intPdfPageNo = 0;
            //download files and create PDF
            chartInfo previousChart = new chartInfo();
            for (int i = 0; i < intTotalFiles; i++)
            {
                //set applicable charts
                chartInfo currentChart = lstChartInfo[i].setFileParameters(blFileHDSelect);
                //get file type
                string strFileType = Path.GetExtension(currentChart.FileLocation);
                string strFileSaveName = strSaveLocations[1] + "\\" + i.ToString();
                //show file being downloaded
                string strFileInfo = currentChart.Name + " - " + currentChart.SubGroup + " - " + currentChart.Group + " ";
                int intIndexLengthRemove = 30;
                if(strFileInfo.Length < 31) { intIndexLengthRemove = strFileInfo.Length - 1; }
                lblFileDownloading.Text = strFileInfo.Remove(intIndexLengthRemove) + "...";
                //download, if download fails move to next file
                try { await wc.DownloadFileTaskAsync(currentChart.FileLocation, strFileSaveName + strFileType); }
                catch { continue; }
                //process images to pdf
                int intPagesAdded = 0;
                if (strFileType == ".pdf")
                {
                    //get downloaded pdf and add to output pdf
                    PdfDocument inputDocument = PdfReader.Open(strFileSaveName + strFileType, PdfDocumentOpenMode.Import);
                    foreach (PdfPage page in inputDocument.Pages)
                    {
                        page.Rotate = currentChart.FileRotation;
                        doc.AddPage(page);
                        intPagesAdded++;
                    }
                }
                else
                {
                    //turn downloaded image into pdf and add to output pdf
                    PdfPage page = new PdfPage();
                    XImage img = XImage.FromFile(strFileSaveName + strFileType);
                    page.Width = img.PointWidth;
                    page.Height = img.PointHeight;
                    doc.Pages.Add(page); 
                    XGraphics xgr = XGraphics.FromPdfPage(doc.Pages[intPdfPageNo]);
                    xgr.DrawImage(img, 0, 0);
                    doc.Pages[intPdfPageNo].Rotate = currentChart.FileRotation;
                    intPagesAdded++;
                }
                //add outline
                PdfOutline.PdfOutlineCollection outlines = doc.Outlines;
                //check whether new group level is required and add sub group outline if required
                if (currentChart.Group != previousChart.Group)
                {
                    outlines.Add(currentChart.Group, doc.Pages[intPdfPageNo]);
                }
                if (currentChart.SubGroup.Length > 0)
                {
                    //using count -1 gives the outline that was just created
                    outlines = outlines[outlines.Count - 1].Outlines;
                    if (currentChart.SubGroup != previousChart.SubGroup)
                    {
                        outlines.Add(currentChart.SubGroup, doc.Pages[intPdfPageNo]);
                    }
                }
                //create and add final level
                outlines = outlines[outlines.Count - 1].Outlines;
                outlines.Add(lstChartInfo[i].Name, doc.Pages[intPdfPageNo]);
                //update page count
                intPdfPageNo += intPagesAdded;
                previousChart = currentChart;
                //give progress
                int intCompletedPercentage = Convert.ToInt16((((double)i + 1.0) / (double)intTotalFiles) * 100);
                pgrDownloadPerc.Value = intCompletedPercentage;
            }
            //save and close pdf
            doc.Save(strSaveLocations[2]);
            doc.Close();
            //add clone with common name
            try { File.Copy(strSaveLocations[2], strSaveLocations[0] + @"\Met Charts.pdf", true); }
            catch { MessageBox.Show("Could not update the common file, ensure the file is not in use"); }
            //upload to cloud?? / email??

            if (chkEmail.Checked)
            {
                try
                {
                    lblFileDownloading.Text = "Sending email...";
                    MailMessage mail = new MailMessage();
                    SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                    mail.From = new MailAddress("4crew10sqn@gmail.com");
                    mail.To.Add("all92wgefb@gmail.com");
                    mail.Subject = "Met & NOTAMs";
                    mail.Body = "See weather charts attached";
                    mail.Attachments.Add(new Attachment(strSaveLocations[2]));

                    SmtpServer.Port = 587;
                    SmtpServer.Credentials = new System.Net.NetworkCredential("4crew10sqn@gmail.com", "ginMONKEYS");
                    SmtpServer.EnableSsl = true;

                    SmtpServer.Send(mail);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }

            //re-enable download button and complete message
            lblFileDownloading.Text = "Download complete.";
            enableControls(true);
        }
        private string[] createSaveDirectories(string saveLoc)
        {
            string[] saveInfo = new string[3];
            //check if file exists and create
            if(!Directory.Exists(saveLoc))
            {
                Directory.CreateDirectory(saveLoc);
            }
            saveInfo[0] = saveLoc;
            //ensure raw doc folder empty
            string strCurrentDate = getDateTimeGroup(DateTime.Now);
            string strRawSaveLocation = saveLoc + @"\Raw " + strCurrentDate;
            string strOutputFile = saveLoc + @"\Met Charts " + strCurrentDate;
            int intFolderRepeatNo = 1;
            string strFolderRepeatNo = "";
            while (Directory.Exists(strRawSaveLocation + strFolderRepeatNo) || File.Exists(strOutputFile + strFolderRepeatNo + ".pdf"))
            {
                strFolderRepeatNo = " (" + intFolderRepeatNo++.ToString() + ")";
            }
            saveInfo[1] = strRawSaveLocation + strFolderRepeatNo;
            saveInfo[2] = strOutputFile + strFolderRepeatNo + ".pdf";
            Directory.CreateDirectory(saveInfo[1]);
            return saveInfo;
        }
        private List<chartInfo> getDownloadAddresses()
        {
            List<chartInfo> onlineFileInformation = new List<chartInfo>();
            //get info from config file
            string fileContent = null;
            switch (cmbArea.Text)
            {
                case "Resolute":
                    fileContent = Weather_Collect.Properties.Resources.Config_Resolute;
                    break;
                case "Philippines":
                    fileContent = Weather_Collect.Properties.Resources.Config_Philippines;
                    break;
                case "Philippines Transit":
                    fileContent = Weather_Collect.Properties.Resources.Config_Philippines_Transit;
                    break;
                case "East Coast":
                    fileContent = Weather_Collect.Properties.Resources.Config_East_Coast;
                    break;
                case "Hawaii Transit":
                    fileContent = Weather_Collect.Properties.Resources.Config_RIMPAC_Transit;
                    break;
                case "Hawaii":
                    fileContent = Weather_Collect.Properties.Resources.Config_RIMPAC_Ex;
                    break;
                case "SAXA":
                    fileContent = Weather_Collect.Properties.Resources.Config_SAXA;
                    break;
                case "Oceania":
                    fileContent = Weather_Collect.Properties.Resources.Config_Oceania;
                    break;
                case "Gateway":
                    fileContent = Weather_Collect.Properties.Resources.Config_Gateway;
                    break;
                case "Japan":
                    fileContent = Weather_Collect.Properties.Resources.Config_Japan;
                    break;
                case "Japan South":
                    fileContent = Weather_Collect.Properties.Resources.Config_Japan_South;
                    break;
                case "West Coast":
                    fileContent = Weather_Collect.Properties.Resources.Config_West_Coast;
                    break;
                default:
                    fileContent = Weather_Collect.Properties.Resources.Config_Resolute;
                    break;
            }
            using (StringReader reader = new StringReader(fileContent))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    //add line of chart info
                    string[] values = line.Split(new string[] { "\",\"" }, StringSplitOptions.None);
                    chartInfo c = new chartInfo(values);
                    onlineFileInformation.Add(c);
                }
            }
            return onlineFileInformation;
        }
        private string getDateTimeGroup(DateTime dtInputDateTime)
        {
            //convert times
            dtInputDateTime = dtInputDateTime.ToUniversalTime();
            return dtInputDateTime.ToString("ddHHmmKMMMyy").ToUpper();
        }
        private string getPublicFolderDirectory()
        {
            // This should give you something like C:\Users\Public\Documents
            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments);
            DirectoryInfo directory = new DirectoryInfo(documentsPath);
            // Now this should give you something like C:\Users\Public
            return directory.Parent.FullName;
        }
        private void enableControls(bool enable)
        {
            btnDownload.Enabled = enable;
            chkHighDef.Enabled = enable;
            btnSaveLocationSel.Enabled = enable;
            cmbArea.Enabled = enable;
        }

        public class chartInfo
        {
            public string Group { get; set; }
            public string SubGroup { get; set; }
            public string Name { get; set; }
            public string StdDefLocation { get; set; }
            public int StdDefRotation { get; set; }
            public string HighDefLocation { get; set; }
            public int HighDefRotation { get; set; }
            public string FileLocation { get; set; }
            public int FileRotation { get; set; }

            public chartInfo()
            {

            }
            public chartInfo(string chartGroup, string chartSubGroup, string chartName, string SDLoc, int SDRot, string HDLoc, int HDRot)
            {
                Group = chartGroup;
                SubGroup = chartSubGroup;
                Name = chartName;
                StdDefLocation = SDLoc;
                StdDefRotation = SDRot;
                HighDefLocation = HDLoc;
                HighDefRotation = HDRot;
            }
            public chartInfo(string[] data)
            {
                Group = data[0];
                SubGroup = data[1];
                Name = data[2];
                StdDefLocation = data[3];
                StdDefRotation = Convert.ToInt16(data[4]);
                HighDefLocation = data[5];
                HighDefRotation = Convert.ToInt16(data[6]);
            }

            public chartInfo setFileParameters(bool highQuality)
            {
                chartInfo c = new chartInfo();
                //Transfer common parameters
                c.Group = Group;
                c.SubGroup = SubGroup;
                c.Name = Name;
                //Change info params to useable params. ie Dont need SD and HD info just requested info
                c.FileLocation = StdDefLocation;
                c.FileRotation = StdDefRotation;
                if (highQuality == true && HighDefLocation.Length > 0)
                {
                    c.FileLocation = HighDefLocation;
                    c.FileRotation = HighDefRotation;
                }
                //Check for dynamic section in address -- improve this section
                string[] arrStrSeperator = new string[] { "|||" };
                string[] strDynamicLocation = c.FileLocation.Split(arrStrSeperator, StringSplitOptions.None);
                if(strDynamicLocation.Length > 1)
                {
                    string[] strDynamicSection = strDynamicLocation[1].Split(':');
                    if(strDynamicSection[0] == "TIME")
                    {
                        int intTimeStep = Convert.ToInt32(strDynamicSection[2]);
                        int attemptNo = 0;
                        DateTime dtRoundedTime = DateTime.UtcNow;                       
                        while (!UrlExists(c.FileLocation) && attemptNo < 24)
                        {
                            dtRoundedTime = dtRoundedTime.AddMinutes(-intTimeStep);
                            dtRoundedTime = timeRoundDown(dtRoundedTime, TimeSpan.FromMinutes(intTimeStep));
                            string strDynamicTime = dtRoundedTime.ToString(strDynamicSection[1]);
                            c.FileLocation = strDynamicLocation[0] + strDynamicTime + strDynamicLocation[2];
                            attemptNo++;
                        }                   
                    }
                }
                return c;
            }

            private DateTime timeRoundDown(DateTime dt, TimeSpan d)
            {
                var delta = dt.Ticks % d.Ticks;
                return new DateTime(dt.Ticks - delta, dt.Kind);
            }
            public bool UrlExists(string file)
            {
                bool exists = false;
                HttpWebResponse response = null;
                var request = (HttpWebRequest)WebRequest.Create(file);
                request.Method = "HEAD";
                request.Timeout = 5000; // milliseconds
                request.AllowAutoRedirect = false;

                try
                {
                    response = (HttpWebResponse)request.GetResponse();
                    exists = response.StatusCode == HttpStatusCode.OK;
                }
                catch
                {
                    exists = false;
                }
                finally
                {
                    // close your response.
                    if (response != null)
                        response.Close();
                }
                return exists;
            }


        }
    }
}
