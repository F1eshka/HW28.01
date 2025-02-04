using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace HW28_01
{
    
    public partial class Form1 : Form
    {
        private bool isTracking = false;
        private Thread trackingThread;

        [DllImport("user32.dll")]
        public static extern short GetAsyncKeyState(Keys vKey);

        public Form1()
        {
            InitializeComponent();
        }

        
        private void StartTracking(object sender, EventArgs e)
        {
            isTracking = true;
            trackingThread = new Thread(TrackUserActivity)
            {
                IsBackground = true
            };
            trackingThread.Start();
        }

       
        private void StopTracking(object sender, EventArgs e)
        {
            isTracking = false;
            trackingThread?.Join();
        }

       
        private void ViewReport(object sender, EventArgs e)
        {
            if (File.Exists(txtLogFilePath.Text))
            {
                Process.Start("notepad.exe", txtLogFilePath.Text);
            }
            else
            {
                MessageBox.Show("Файл журналу не знайдено.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        
        private void TrackUserActivity()
        {
            string logPath = txtLogFilePath.Text;
            string[] restrictedWords = txtRestrictedWords.Text.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(s => s.Trim()).ToArray();
            string[] restrictedApps = txtRestrictedApps.Text.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(s => s.Trim()).ToArray();

            while (isTracking)
            {
                if (chkMonitorProcesses.Checked)
                {
                    var processes = Process.GetProcesses().Select(p => p.ProcessName);
                    foreach (string process in processes)
                    {
                        if (restrictedApps.Contains(process, StringComparer.OrdinalIgnoreCase))
                        {
                            File.AppendAllText(logPath, $"Заборонена програма закрита: {process} {DateTime.Now}\n");
                            foreach (var proc in Process.GetProcessesByName(process))
                            {
                                try
                                {
                                    proc.Kill();
                                }
                                catch (Exception ex)
                                {
                                    File.AppendAllText(logPath, $"Не вдалося завершити {process}: {ex.Message}\n");
                                }
                            }
                        }
                    }
                }

                if (chkLogKeys.Checked)
                {
                    foreach (Keys key in Enum.GetValues(typeof(Keys)))
                    {
                        if (GetAsyncKeyState(key) < 0)
                        {
                            File.AppendAllText(logPath, key.ToString() + " ");
                        }
                    }
                }
                Thread.Sleep(100);
            }
        }
    }
}
