using JSL;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JumpSaves
{
    internal static class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            // Used during Setup only
            if (args.Length == 1 && args[0] == "/relaunch")
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = Application.ExecutablePath,
                    UseShellExecute = true
                });
                return;
            }

            mutex_ = new Mutex(true, "Global\\JumpSaves-7E864969-4BF6-4548-BC30-F6825F52497D", out bool createdNew);
            if (!createdNew)
            {
                MessageBox.Show("Another instance of JumpSaves is already running.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            model_ = new Model.Manager();

            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MainWindow(model_));
                model_.ActionLog.AddEntry(Model.ActionLog.Origin.Application, Model.ActionLog.Level.Info, "Application closing gracefully.");
            }
            catch (Exception ex)
            {
                model_.ActionLog.AddEntry(Model.ActionLog.Origin.Application, Model.ActionLog.Level.Info, $"Application closing with exception: {ex}");
                throw;
            }

            model_.Dispose();
        }

        private static Mutex mutex_ = null;
        private static Model.Manager model_;
    }
}
