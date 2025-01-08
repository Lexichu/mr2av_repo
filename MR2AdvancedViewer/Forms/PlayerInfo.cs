using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Versioning;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MR2AdvancedViewer.Forms
{
    [SupportedOSPlatform("windows")]
    public partial class PlayerInfo : Form
    {
        public ViewerWindow AVW;
        public bool bEditName;
        public string OldName;

        public PlayerInfo()
        {
            InitializeComponent();
        }

        private void EditSaveBtn_Click(object sender, EventArgs e)
        {
            bEditName = !bEditName;
            EditSaveBtn.Text = (bEditName ? "Save" : "Edit");

            if (bEditName)
            {
                OldName = PlayerName.Text;
                CancelBtn.Show();
                CancelBtn.Enabled = true;
                PlayerName.ReadOnly = false;
            }
            else
            {
                if (PlayerName.Text != OldName)
                {
                    if (PlayerName.Text == "")
                        PlayerName.Text = OldName;

                    AVW.convTextAndWrite(PlayerName.Text, true);
                }
                CancelBtn.Hide();
                CancelBtn.Enabled = false;
                PlayerName.ReadOnly = true;
            }
        }

        private void CancelBtn_Click(object sender, EventArgs e)
        {
            bEditName = false;
            EditSaveBtn.Text = "Edit";
            PlayerName.Text = OldName;
            CancelBtn.Hide();
            CancelBtn.Enabled = false;
            PlayerName.ReadOnly = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if(AVW != null && !bEditName)
            {
                PlayerName.Text = AVW.MonReadGivenName(true);
            }
        }

        private void PlayerName_TextChanged(object sender, EventArgs e)
        {
//            if (PlayerName.Text.Length > 12)
//                PlayerName.Text = PlayerName.Text.Substring(0, 12);

//              This was unneeded. You can just set the field to a max size of 12. Lexi confirmed a dummy _/(0.0)\_
        }
    }
}
