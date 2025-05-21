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

namespace MR2AdvancedViewer
{
    [SupportedOSPlatform("windows")]
    public partial class MR2ControllerView : Form
    {
        public bool[] P1_Control = new bool[16];
        public bool[] P2_Control = new bool[16];
        public int P1Raw, P2Raw, P1RawOld, P2RawOld;

        public MR2ControllerView()
        {
            InitializeComponent();
        }

        public void UpdateConDisplay()
        {
            if (P1Raw != P1RawOld)
            {
                Label_Shoulder.Text = "";
                Label_Misc.Text = "";
                Label_Face.Text = "";
                Label_DPad.Text = "";
                if (P1_Control[0]) Label_Shoulder.Text += "L2 ";
                if (P1_Control[1]) Label_Shoulder.Text += "R2 ";
                if (P1_Control[2]) Label_Shoulder.Text += "L1 ";
                if (P1_Control[3]) Label_Shoulder.Text += "R1 ";
                if (P1_Control[4]) Label_Face.Text += "CI ";
                if (P1_Control[5]) Label_Face.Text += "CR ";
                if (P1_Control[6]) Label_Face.Text += "TR ";
                if (P1_Control[7]) Label_Face.Text += "SQ ";
                if (P1_Control[8]) Label_Misc.Text += "SE ";
                if (P1_Control[9]) Label_Misc.Text += "L3 ";
                if (P1_Control[10]) Label_Misc.Text += "R3 ";
                if (P1_Control[11]) Label_Misc.Text += "ST ";
                if (P1_Control[12]) Label_DPad.Text += "U ";
                if (P1_Control[13]) Label_DPad.Text += "R ";
                if (P1_Control[14]) Label_DPad.Text += "D ";
                if (P1_Control[15]) Label_DPad.Text += "L ";
            }
            P1RawOld = P1Raw;
        }

        private void Label_DPad_Click(object sender, EventArgs e)
        {

        }
    }
}
