using System;
using System.Threading;
using System.Windows.Forms;

namespace HW28_01
{
    internal static class Program
    {
        // ������������ ��� �������������� ������� ���������� ����������� ����������
        private static readonly Mutex appMutex = new Mutex(true, "ActivityTrackerMutex");

        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            Application.Run(new Form1());
        }
    }
}
