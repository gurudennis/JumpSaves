using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JumpSaves
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            mutex_ = new Mutex(true, "Global\\JumpSaves-7E864969-4BF6-4548-BC30-F6825F52497D", out bool createdNew);
            if (!createdNew)
            {
                MessageBox.Show("Another instance of JumpSaves is already running.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            model_ = new Model.Manager();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainWindow(model_));

            model_.Dispose();
        }

        private static Mutex mutex_ = null;
        private static Model.Manager model_;
    }
}
