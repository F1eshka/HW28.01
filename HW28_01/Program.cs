using System;
using System.Threading;
using System.Windows.Forms;

namespace HW28_01
{
    internal static class Program
    {
        // »спользуетс€ дл€ предотвращени€ запуска нескольких экземпл€ров приложени€
        private static readonly Mutex appMutex = new Mutex(true, "ActivityTrackerMutex");

        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            Application.Run(new Form1());
        }
    }
}
