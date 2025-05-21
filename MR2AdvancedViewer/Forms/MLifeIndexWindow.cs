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
    public partial class MonLifeIndexWindow : Form
    {
        public int Fatigue;
        public int Stress;
        public string LIBox;
        public int LSR, LSM;
        public Color boxCol;

        public MonLifeIndexWindow()
        {
            InitializeComponent();
        }

        private void MonLifeIndexWindow_Load(object sender, EventArgs e)
        {
            LITimer.Interval = 100;
            LITimer.Start();
        }        

        private void LITimer_Tick(object sender, EventArgs e)
        {
            LIFatigueBox.Invoke((MethodInvoker)delegate { LIFatigueBox.Text = Fatigue.ToString(); });
            LIStressBox.Invoke((MethodInvoker)delegate { LIStressBox.Text = Stress.ToString(); });
            LILifeIndexBox.Invoke((MethodInvoker)delegate { LILifeIndexBox.Text = LIBox; });
            LILifeIndexBox.BackColor = boxCol;
            LILifeLabel.Text = "Lifespan Remaining: " + LSR.ToString() + " / " + LSM.ToString();
            LITimer.Interval = 1000;
        }
    }
}
