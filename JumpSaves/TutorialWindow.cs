using System;
using System.Windows.Forms;

namespace JumpSaves
{
    public partial class TutorialWindow : Form
    {
        public TutorialWindow(Model.Instance model)
        {
            model_ = model;

            InitializeComponent();
        }

        private void TutorialWindow_Load(object sender, EventArgs e)
        {
            checkBoxNotAgain.Checked = !model_.Settings.ShowTutorial;
        }

        private void TutorialWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            model_.Settings.ShowTutorial = !checkBoxNotAgain.Checked;
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            Close();
        }

        Model.Instance model_;
    }
}
