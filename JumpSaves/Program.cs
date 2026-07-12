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
            model_ = new Model.Manager(SynchronizationContext.Current);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainWindow(model_.CreateInstance()));
        }

        private static Model.Manager model_;
    }
}
