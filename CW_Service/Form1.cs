using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Windows.Forms;

namespace CW_Service
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            log4net.Config.XmlConfigurator.Configure();
        }

        ServiceHost serviceHost;

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                Common.log.Error("Bat dau khoi tao service ....");
                Config_DB_Info.GetAppSettings();

                SMS_Config_Info.Load_Sms_Config();

                lblConnectionString.Text = Config_DB_Info.GConnectionString;

                serviceHost = new ServiceHost(typeof(CWService));
                string _error = "";
                int s = Master.RunStart(ref _error);
                /// return = 0 la ko co loi gi ca,
                /// return = -1 lỗi chung chung
                if (s == 0)
                {
                    serviceHost.Open();
                    this.lblStatus.ForeColor = System.Drawing.Color.Blue;
                    this.lblStatus.Text = @"Custom Warehouse Service is running !";
                    Common.log.Error("Khoi tao service thanh cong ....");
                }
                else if (s != 0 && s != -1)
                {
                    this.lblStatus.ForeColor = System.Drawing.Color.Red;
                    this.lblStatus.Text = _error;
                }
                else
                {
                    this.lblStatus.ForeColor = System.Drawing.Color.Red;
                    this.lblStatus.Text = @"Lỗi trong quá trình bật service!";
                }
            }
            catch (Exception ex)
            {
                Common.log.Error(ex.ToString());
                lblStatus.Text = @"Lỗi rồi";
                lblStatus.ForeColor = System.Drawing.Color.Red;
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult rs = MessageBox.Show("Bạn có chắc chắn muốn tắt ngay lập tức Service hay không?", "Thông báo", MessageBoxButtons.YesNo);
            if (rs == DialogResult.No)
            {
                e.Cancel = true;
            }
        }
    }
}
