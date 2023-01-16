using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MR2AdvancedViewer
{
    public partial class TrainingWindow : Form
    {
        public TrainingWindow()
        {
            InitializeComponent();
        }

        public int TrainInt, OldTrainInt = -1, LifeStage;
        public int[] StatGRs = new int[6];

        [Flags]
        private enum TSpec
        {
            NON = 0,
            DOM = 1,
            STU = 2,
            RUN = 4,
            SHO = 8,
            DOD = 16,
            END = 32,
            PUL = 64,
            MED = 128,
            LEA = 256,
            POO = 512,
            SEA = 1024,
            MTN = 2048,
            DES = 4096,
            JUN = 8192,
            VOL = 16384,
            IDK = 32768
        }

        private void DrillSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (DrillSelect.SelectedIndex)
            {
                default:
                    PrimStatLabel.Text = "N/A";
                    SecStatLabel.Text = "N/A";
                    break;
                case 1:
                    PrimStatLabel.Text = ProduceValues(false, false, 1);
                    SecStatLabel.Text = "N/A";
                    break;
                case 2:
                    PrimStatLabel.Text = ProduceValues(false, false, 2);
                    SecStatLabel.Text = "N/A";
                    break;
                case 3:
                    PrimStatLabel.Text = ProduceValues(false, false, 0);
                    SecStatLabel.Text = "N/A";
                    break;
                case 4:
                    PrimStatLabel.Text = ProduceValues(false, false, 3);
                    SecStatLabel.Text = "N/A";
                    break;
                case 5:
                    PrimStatLabel.Text = ProduceValues(false, false, 4);
                    SecStatLabel.Text = "N/A";
                    break;
                case 6:
                    PrimStatLabel.Text = ProduceValues(false, false, 5);
                    SecStatLabel.Text = "N/A";
                    break;
                case 7:
                    PrimStatLabel.Text = ProduceValues(true, false, 1);
                    SecStatLabel.Text = ProduceValues(true, true, 0);
                    break;
                case 8:
                    PrimStatLabel.Text = ProduceValues(true, false, 2);
                    SecStatLabel.Text = ProduceValues(true, true, 3);
                    break;
                case 9:
                    PrimStatLabel.Text = ProduceValues(true, false, 4);
                    SecStatLabel.Text = ProduceValues(true, true, 2);
                    break;
                case 10:
                    PrimStatLabel.Text = ProduceValues(true, false, 0);
                    SecStatLabel.Text = ProduceValues(true, true, 5);
                    break;
                case 11:
                    PrimStatLabel.Text = ProduceValues(true, false, 5);
                    SecStatLabel.Text = ProduceValues(true, true, 0);
                    break;
            }
        }

        private string ProduceValues(bool bHeavy, bool bSecondary, int StatID)
        {
            int SValue;
            SValue = Convert.ToInt32(bHeavy);
            SValue += Convert.ToInt32(bSecondary) * 2;

            switch (LifeStage)
            {
                case 0:
                case 9:
                    switch (StatGRs[StatID])
                    {
                        case 0:
                            switch (SValue)
                            {
                                case 1: return "2-4 (2/2/3/4)";
                                case 3: return "2 (2/2/2/2)";
                                default: return "1-3 (0/1/2/3)";
                            }
                        case 1:
                            switch (SValue)
                            {
                                case 1: return "3-5 (3/3/4/5)";
                                case 3: return "2 (2/2/2/2)";
                                default: return "1-3 (0/1/2/3)";
                            }
                        case 2:
                            switch (SValue)
                            {
                                case 1: return "3-6 (3/4/5/6)";
                                case 3: return "2-3 (2/2/2/3)";
                                default: return "1-4 (1/2/3/4)";
                            }
                        case 3:
                            switch (SValue)
                            {
                                case 1: return "5-8 (5/6/7/8)";
                                case 3: return "2-3 (2/2/2/3)";
                                default: return "2-5 (2/3/4/5)";
                            }
                        case 4:
                            switch (SValue)
                            {
                                case 1: return "6-9 (6/7/8/9)";
                                case 3: return "2-4 (2/2/3/4)";
                                default: return "3-6 (3/4/5/6)";
                            }
                        default:
                            {
                                return "N/A";
                            }
                    }
                case 1:
                case 8:
                    switch (StatGRs[StatID])
                    {
                        case 0:
                            switch (SValue)
                            {
                                case 1: return "3-5 (3/3/4/5)";
                                case 3: return "2 (2/2/2/2)";
                                default: return "1-4 (1/2/3/4)";
                            }
                        case 1:
                            switch (SValue)
                            {
                                case 1: return "4-7 (4/5/6/7)";
                                case 3: return "2-3 (2/2/2/3)";
                                default: return "2-5 (2/3/4/5)";
                            }
                        case 2:
                            switch (SValue)
                            {
                                case 1: return "6-9 (6/7/8/9)";
                                case 3: return "2-4 (2/2/3/4)";
                                default: return "3-6 (3/4/5/6)";
                            }
                        case 3:
                            switch (SValue)
                            {
                                case 1: return "8-11 (8/9/10/11)";
                                case 3: return "2-5 (2/3/4/5)";
                                default: return "4-7 (4/5/6/7)";
                            }
                        case 4:
                            switch (SValue)
                            {
                                case 1: return "11-14 (11/12/13/14)";
                                case 3: return "2-5 (2/3/4/5)";
                                default: return "6-9 (6/7/8/9)";
                            }
                        default:
                            {
                                return "N/A";
                            }
                    }
                case 2:
                case 7:
                    switch (StatGRs[StatID])
                    {
                        case 0:
                            switch (SValue)
                            {
                                case 1: return "4-7 (4/5/6/7)";
                                case 3: return "2-3 (2/2/2/3)";
                                default: return "2-5 (2/3/4/5)";
                            }
                        case 1:
                            switch (SValue)
                            {
                                case 1: return "6-9 (6/7/8/9)";
                                case 3: return "2-4 (2/2/3/4)";
                                default: return "3-6 (3/4/5/6)";
                            }
                        case 2:
                            switch (SValue)
                            {
                                case 1: return "9-12 (9/10/11/12)";
                                case 3: return "2-5 (2/3/4/5)";
                                default: return "5-8 (5/6/7/8)";
                            }
                        case 3:
                            switch (SValue)
                            {
                                case 1: return "12-15 (12/13/14/15)";
                                case 3: return "3-6 (3/4/5/6)";
                                default: return "7-10 (7/8/9/10)";
                            }
                        case 4:
                            switch (SValue)
                            {
                                case 1: return "15-18 (15/16/17/18)";
                                case 3: return "4-7 (4/5/6/7)";
                                default: return "9-12 (9/10/11/12)";
                            }
                        default:
                            {
                                return "N/A";
                            }
                    }
                case 3:
                case 6:
                    switch (StatGRs[StatID])
                    {
                        case 0:
                            switch (SValue)
                            {
                                case 1: return "5-8 (5/6/7/8)";
                                case 3: return "2-3 (2/2/2/3)";
                                default: return "2-5 (2/3/4/5)";
                            }
                        case 1:
                            switch (SValue)
                            {
                                case 1: return "8-11 (8/9/10/11)";
                                case 3: return "2-4 (2/2/3/4)";
                                default: return "4-7 (4/5/6/7)";
                            }
                        case 2:
                            switch (SValue)
                            {
                                case 1: return "11-14 (11/12/13/14)";
                                case 3: return "3-6 (3/4/5/6)";
                                default: return "6-9 (6/7/8/9)";
                            }
                        case 3:
                            switch (SValue)
                            {
                                case 1: return "15-18 (15/16/17/18)";
                                case 3: return "4-7 (4/5/6/7)";
                                default: return "8-11 (8/9/10/11)";
                            }
                        case 4:
                            switch (SValue)
                            {
                                case 1: return "19-20 (19/20/20/20)";
                                case 3: return "5-8 (5/6/7/8)";
                                default: return "11-14 (11/12/13/14)";
                            }
                        default:
                            {
                                return "N/A";
                            }
                    }
                case 4:
                    switch (StatGRs[StatID])
                    {
                        case 0:
                            switch (SValue)
                            {
                                case 1: return "7-10 (7/8/9/10)";
                                case 3: return "2-4 (2/2/3/4)";
                                default: return "4-7 (4/5/6/7)";
                            }
                        case 1:
                            switch (SValue)
                            {
                                case 1: return "11-14 (11/12/13/14)";
                                case 3: return "2-5 (2/3/4/5)";
                                default: return "6-9 (6/7/8/9)";
                            }
                        case 2:
                            switch (SValue)
                            {
                                case 1: return "14-17 (14/15/16/17)";
                                case 3: return "4-7 (4/5/6/7)";
                                default: return "8-11 (8/9/10/11)";
                            }
                        case 3:
                            switch (SValue)
                            {
                                case 1: return "19-20 (19/20/20/20)";
                                case 3: return "6-9 (6/7/8/9)";
                                default: return "11-14 (11/12/13/14)";
                            }
                        case 4:
                            switch (SValue)
                            {
                                case 1: return "20 (20/20/20/20)";
                                case 3: return "7-10 (7/8/9/10)";
                                default: return "14-15 (14/15/15/15)";
                            }
                        default:
                            {
                                return "N/A";
                            }
                    }
                case 5:
                    switch (StatGRs[StatID])
                    {
                        case 0:
                            switch (SValue)
                            {
                                case 1: return "6-9 (6/7/8/9)";
                                case 3: return "2-4 (2/2/3/4)";
                                default: return "3-6 (3/4/5/6)";
                            }
                        case 1:
                            switch (SValue)
                            {
                                case 1: return "10-13 (10/11/12/13)";
                                case 3: return "2-5 (2/3/4/5)";
                                default: return "5-8 (5/6/7/8)";
                            }
                        case 2:
                            switch (SValue)
                            {
                                case 1: return "13-16 (13/14/15/16)";
                                case 3: return "3-6 (3/4/5/6)";
                                default: return "7-10 (7/8/9/10)";
                            }
                        case 3:
                            switch (SValue)
                            {
                                case 1: return "18-20 (18/19/20/20)";
                                case 3: return "5-8 (5/6/7/8)";
                                default: return "10-13 (10/11/12/13)";
                            }
                        case 4:
                            switch (SValue)
                            {
                                case 1: return "20 (20/20/20/20)";
                                case 3: return "7-10 (7/8/9/10)";
                                default: return "13-15 (13/14/15/15)";
                            }
                        default:
                            {
                                return "N/A";
                            }
                    }
                default:
                    return "N/A";
            }
        }

        public void PopulateSpecialty()
        {
            if (OldTrainInt != TrainInt)
            {
                TSpec BSFL = (TSpec)TrainInt;

                DominoChk.Checked = (BSFL & TSpec.DOM) == TSpec.DOM;
                StudyChk.Checked = (BSFL & TSpec.STU) == TSpec.STU;
                RunChk.Checked = (BSFL & TSpec.RUN) == TSpec.RUN;
                ShootChk.Checked = (BSFL & TSpec.SHO) == TSpec.SHO;
                DodgeChk.Checked = (BSFL & TSpec.DOD) == TSpec.DOD;
                EndureChk.Checked = (BSFL & TSpec.END) == TSpec.END;
                PullChk.Checked = (BSFL & TSpec.PUL) == TSpec.PUL;
                MeditateChk.Checked = (BSFL & TSpec.MED) == TSpec.MED;
                LeapChk.Checked = (BSFL & TSpec.LEA) == TSpec.LEA;
                PoolChk.Checked = (BSFL & TSpec.POO) == TSpec.POO;
                SeaChk.Checked = (BSFL & TSpec.SEA) == TSpec.SEA;
                MountainChk.Checked = (BSFL & TSpec.MTN) == TSpec.MTN;
                DesertChk.Checked = (BSFL & TSpec.DES) == TSpec.DES;
                JungleChk.Checked = (BSFL & TSpec.JUN) == TSpec.JUN;
                VolcanoChk.Checked = (BSFL & TSpec.VOL) == TSpec.VOL;
                UnknownChk.Checked = (BSFL & TSpec.IDK) == TSpec.IDK;
            }
            OldTrainInt = TrainInt;
        }
    }
}
