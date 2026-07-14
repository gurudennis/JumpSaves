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
            model_ = new Model.Manager();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainWindow(model_));

            model_.Dispose();
        }

        private static Model.Manager model_;
    }
}
