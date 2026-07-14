using System;
using System.Windows.Forms;

namespace JumpSaves
{
    internal class Common
    {
        public static bool Safe(IWin32Window owner, string description, Action action)
        {
            try
            {
                action();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(owner, $"Error encountered while {description}.\n\n{ex.Message}", "JumpSaves error");
                return false;
            }
        }
    }
}
