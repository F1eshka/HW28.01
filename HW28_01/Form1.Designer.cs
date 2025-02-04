using System.Drawing;
using System.Windows.Forms;

namespace HW28_01
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        private Button btnStartTracking;
        private Button btnStopTracking;
        private Button btnViewReport;
        private CheckBox chkLogKeys;
        private CheckBox chkMonitorProcesses;
        private TextBox txtLogFilePath;
        private TextBox txtRestrictedWords;
        private TextBox txtRestrictedApps;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.BackColor = Color.LightGray;
            this.Font = new Font("Segoe UI", 10, FontStyle.Regular);
            this.Text = "Трекер активності";

            this.btnStartTracking = new Button();
            this.btnStopTracking = new Button();
            this.btnViewReport = new Button();
            this.chkLogKeys = new CheckBox();
            this.chkMonitorProcesses = new CheckBox();
            this.txtLogFilePath = new TextBox();
            this.txtRestrictedWords = new TextBox();
            this.txtRestrictedApps = new TextBox();
            this.SuspendLayout();

            int buttonWidth = 180, buttonHeight = 50;

            void StyleButton(Button btn, string text, int x, int y)
            {
                btn.Text = text;
                btn.Size = new Size(buttonWidth, buttonHeight);
                btn.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                btn.BackColor = Color.DarkSlateBlue;
                btn.ForeColor = Color.White;
                btn.FlatStyle = FlatStyle.Flat;
                btn.FlatAppearance.BorderSize = 0;
                btn.Location = new Point(x, y);
            }

            StyleButton(this.btnStartTracking, "Старт", 50, 300);
            this.btnStartTracking.Click += StartTracking;

            StyleButton(this.btnStopTracking, "Стоп", 300, 300);
            this.btnStopTracking.Click += StopTracking;

            StyleButton(this.btnViewReport, "Перегляд", 550, 300);
            this.btnViewReport.Click += ViewReport;

            this.chkLogKeys.AutoSize = true;
            this.chkLogKeys.Location = new Point(50, 50);
            this.chkLogKeys.Text = "Запис натискань";

            this.chkMonitorProcesses.AutoSize = true;
            this.chkMonitorProcesses.Location = new Point(200, 50);
            this.chkMonitorProcesses.Text = "Моніторинг процесів";

            void StyleTextBox(TextBox txt, int y, string placeholder)
            {
                txt.Size = new Size(450, 30);
                txt.Font = new Font("Segoe UI", 10, FontStyle.Regular);
                txt.Location = new Point(50, y);
                txt.BackColor = Color.WhiteSmoke;
                txt.ForeColor = Color.Black;
                txt.Text = placeholder;
            }

            StyleTextBox(this.txtLogFilePath, 90, "Файл журналу (TXT.txt)");
            StyleTextBox(this.txtRestrictedWords, 150, "Заборонені слова (через кому)");
            StyleTextBox(this.txtRestrictedApps, 210, "Заборонені програми (через кому)");

            this.ClientSize = new Size(800, 450);
            this.Controls.Add(this.txtRestrictedApps);
            this.Controls.Add(this.txtRestrictedWords);
            this.Controls.Add(this.txtLogFilePath);
            this.Controls.Add(this.chkMonitorProcesses);
            this.Controls.Add(this.chkLogKeys);
            this.Controls.Add(this.btnViewReport);
            this.Controls.Add(this.btnStopTracking);
            this.Controls.Add(this.btnStartTracking);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
