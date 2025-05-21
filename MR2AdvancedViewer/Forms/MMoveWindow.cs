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
    public partial class MonMoveWindow : Form
    {
        public int[] Mon_Moves = new int[24];
        public int[] Mon_MoveUsed = new int[24];
        public int Mon_Genus, Mon_SubGenus;
        public int oldGenus = -1;
        public int MoveSelected = -1;
        public int rowSelected, colSelected;
        public int[] Mon_Stats = new int[6];
        public int Mon_Nature = -1; //bedeg
        public int[] MonActMoves = new int[4];
        public Color PowCol = System.Drawing.ColorTranslator.FromHtml("#DAA520");
        public Color IntCol = Color.Green;
        public Random rng = new Random();

        public void LazyDataFill(bool bIntelligence, int Guts, int Force, int Hit, int Wither, int Crit, int MoveSlot)
        {
            if (bIntelligence)
                MoveName.ForeColor = IntCol;
            else
                MoveName.ForeColor = PowCol;

            if (Force > 0)
                MoveDamage.Text = GenerateStatValue(0, Force);
            else
                MoveDamage.Text = "---";

            if (Wither > 0)
                MoveGD.Text = GenerateStatValue(2, Wither);
            else
                MoveGD.Text = "---";

            if (Crit > 0)
                MoveSharp.Text = GenerateStatValue(3, Crit);
            else
                MoveSharp.Text = "---";

            MoveHit.Text = GenerateStatValue(1, Hit);
            MoveGuts.Text = Guts.ToString();
            MoveUses.Text = Mon_MoveUsed[MoveSlot].ToString();
            MoveUnlocked.Checked = (Mon_Moves[MoveSlot] == 1);
            PlaceHighlight(MoveSlot); //bedeg                                                                                 
        }

        private void PlaceHighlight(int MoveSlot) //bedeg
        {
            if (Mon_Genus == 0 && ((MoveSlot >= 2 && MoveSlot <= 6) || (MoveSlot >= 7 && MoveSlot <= 11) || (MoveSlot >= 20 && MoveSlot <= 23))) //pixie is weird
                MoveSlot -= 1;

            hilightSelect.Parent = (PictureBox)this.Controls[("Slot" + ((MoveSlot / 6) + 1) + "_" + ((MoveSlot % 6) + 1))];  //bedeg
            hilightSelect.Image = Properties.Resources.hilight;                                                             
            hilightSelect.BackColor = Color.Transparent;                                                                    
            hilightSelect.Location = new Point(0, 0);                                                                       
            hilightSelect.Visible = true;
        }

        private void PlaceCheckmark() //bedeg
        {
            PictureBox[] childBoxes = { skillChecked_0, skillChecked_1, skillChecked_2, skillChecked_3, skillChecked_4, skillChecked_5,             
                                        skillChecked_6, skillChecked_7, skillChecked_8, skillChecked_9, skillChecked_10, skillChecked_11,           
                                        skillChecked_12, skillChecked_13, skillChecked_14, skillChecked_15, skillChecked_16, skillChecked_17,       
                                        skillChecked_18, skillChecked_19, skillChecked_20, skillChecked_21, skillChecked_22, skillChecked_23 };     
            PictureBox[] parentBoxes = { Slot1_1, Slot1_2, Slot1_3, Slot1_4, Slot1_5, Slot1_6,                                                      
                                        Slot2_1, Slot2_2, Slot2_3, Slot2_4, Slot2_5, Slot2_6,                                                      
                                        Slot3_1, Slot3_2, Slot3_3, Slot3_4, Slot3_5, Slot3_6,                                                       
                                        Slot4_1, Slot4_2, Slot4_3, Slot4_4, Slot4_5, Slot4_6 };                                                     

            for (int i = 0; i <= 23; i++)
            {
                if (Mon_Genus == 0 && ((i >= 3 && i <= 6) || (i >= 7 && i <= 11) || (i >= 20 && i <= 23))) //pixie is weird
                {
                    {
                        childBoxes[i - 1].Parent = parentBoxes[i - 1];
                        childBoxes[i - 1].Location = new Point(30, 2);
                        if (Mon_Moves[i] == 1 && parentBoxes[i - 1].Image.PixelFormat != Properties.Resources.No_Move.PixelFormat)
                        {
                            childBoxes[i - 1].Image = Properties.Resources.skillchecked;
                            childBoxes[i - 1].Visible = true;
                        }
                        else
                        {
                            childBoxes[i - 1].Image = null;
                            childBoxes[i - 1].Visible = false;
                        }
                    }
                }
                else
                {
                    childBoxes[i].Parent = parentBoxes[i];
                    childBoxes[i].Location = new Point(30, 2);
                    if (Mon_Moves[i] == 1 && parentBoxes[i].Image.PixelFormat != Properties.Resources.No_Move.PixelFormat)
                    {
                        childBoxes[i].Image = Properties.Resources.skillchecked;
                        childBoxes[i].Visible = true;
                    }
                    else
                    {
                        childBoxes[i].Image = null;
                        childBoxes[i].Visible = false;
                    }
                }
            }
        }

        public MonMoveWindow()
        {
            InitializeComponent();
        }

        private void Slot1_1_Click(object sender, EventArgs e)
        {
            rowSelected = 1;
            colSelected = 1;
            GenerateMoveNumber();
        }
        private void Slot1_2_Click(object sender, EventArgs e)
        {
            rowSelected = 2;
            colSelected = 1;
            GenerateMoveNumber();
        }
        private void Slot1_3_Click(object sender, EventArgs e)
        {
            rowSelected = 3;
            colSelected = 1;
            GenerateMoveNumber();
        }
        private void Slot1_4_Click(object sender, EventArgs e)
        {
            rowSelected = 4;
            colSelected = 1;
            GenerateMoveNumber();
        }
        private void Slot1_5_Click(object sender, EventArgs e)
        {
            rowSelected = 5;
            colSelected = 1;
            GenerateMoveNumber();
        }
        private void Slot1_6_Click(object sender, EventArgs e)
        {
            rowSelected = 6;
            colSelected = 1;
            GenerateMoveNumber();
        }
        private void Slot2_1_Click(object sender, EventArgs e)
        {
            rowSelected = 1;
            colSelected = 2;
            GenerateMoveNumber();
        }
        private void Slot2_2_Click(object sender, EventArgs e)
        {
            rowSelected = 2;
            colSelected = 2;
            GenerateMoveNumber();
        }
        private void Slot2_3_Click(object sender, EventArgs e)
        {
            rowSelected = 3;
            colSelected = 2;
            GenerateMoveNumber();
        }
        private void Slot2_4_Click(object sender, EventArgs e)
        {
            rowSelected = 4;
            colSelected = 2;
            GenerateMoveNumber();
        }
        private void Slot2_5_Click(object sender, EventArgs e)
        {
            rowSelected = 5;
            colSelected = 2;
            GenerateMoveNumber();
        }
        private void Slot2_6_Click(object sender, EventArgs e)
        {
            rowSelected = 6;
            colSelected = 2;
            GenerateMoveNumber();
        }
        private void Slot3_1_Click(object sender, EventArgs e)
        {
            rowSelected = 1;
            colSelected = 3;
            GenerateMoveNumber();
        }
        private void Slot3_2_Click(object sender, EventArgs e)
        {
            rowSelected = 2;
            colSelected = 3;
            GenerateMoveNumber();
        }
        private void Slot3_3_Click(object sender, EventArgs e)
        {
            rowSelected = 3;
            colSelected = 3;
            GenerateMoveNumber();
        }
        private void Slot3_4_Click(object sender, EventArgs e)
        {
            rowSelected = 4;
            colSelected = 3;
            GenerateMoveNumber();
        }
        private void Slot3_5_Click(object sender, EventArgs e)
        {
            rowSelected = 5;
            colSelected = 3;
            GenerateMoveNumber();
        }
        private void Slot3_6_Click(object sender, EventArgs e)
        {
            rowSelected = 6;
            colSelected = 3;
            GenerateMoveNumber();
        }
        private void Slot4_1_Click(object sender, EventArgs e)
        {
            rowSelected = 1;
            colSelected = 4;
            GenerateMoveNumber();
        }
        private void Slot4_2_Click(object sender, EventArgs e)
        {
            rowSelected = 2;
            colSelected = 4;
            GenerateMoveNumber();
        }
        private void Slot4_3_Click(object sender, EventArgs e)
        {
            rowSelected = 3;
            colSelected = 4;
            GenerateMoveNumber();
        }
        private void Slot4_4_Click(object sender, EventArgs e)
        {
            rowSelected = 4;
            colSelected = 4;
            GenerateMoveNumber();
        }
        private void Slot4_5_Click(object sender, EventArgs e)
        {
            rowSelected = 5;
            colSelected = 4;
            GenerateMoveNumber();
        }
        private void Slot4_6_Click(object sender, EventArgs e)
        {
            rowSelected = 6;
            colSelected = 4;
            GenerateMoveNumber();
        }

        private void GenerateMoveNumber()
        {
            switch (Mon_Genus)
            {
                case 0:
                    if (colSelected == 1 && rowSelected < 6) { MoveSelected = rowSelected; }
                    else if (colSelected == 2 && rowSelected < 6) { MoveSelected = rowSelected + 5; }
                    else if (colSelected == 3 && rowSelected < 7) { MoveSelected = rowSelected + 10; }
                    else if (colSelected == 4 && rowSelected < 6) { MoveSelected = rowSelected + 16; }
                    else { MoveSelected = -1; }
                    break;
                case 1:
                    if (colSelected == 1 && rowSelected < 6) { MoveSelected = rowSelected; }
                    else if (colSelected == 2 && rowSelected < 6) { MoveSelected = rowSelected + 5; }
                    else if (colSelected == 3 && rowSelected < 6) { MoveSelected = rowSelected + 10; }
                    else if (colSelected == 4 && rowSelected < 5) { MoveSelected = rowSelected + 15; }
                    else { MoveSelected = -1; }
                    break;
                case 2:
                    if (colSelected == 1 && rowSelected < 6) { MoveSelected = rowSelected; }
                    else if (colSelected == 2 && rowSelected < 5) { MoveSelected = rowSelected + 5; }
                    else if (colSelected == 3 && rowSelected < 5) { MoveSelected = rowSelected + 9; }
                    else if (colSelected == 4 && rowSelected < 5) { MoveSelected = rowSelected + 13; }
                    else { MoveSelected = -1; }
                    break;
                case 3:
                    if (colSelected == 1 && rowSelected < 3) { MoveSelected = rowSelected; }
                    else if (colSelected == 2 && rowSelected < 5) { MoveSelected = rowSelected + 2; }
                    else if (colSelected == 3 && rowSelected < 5) { MoveSelected = rowSelected + 6; }
                    else if (colSelected == 4 && rowSelected < 6) { MoveSelected = rowSelected + 10; }
                    else { MoveSelected = -1; }
                    break;
                case 4:
                    if (colSelected == 1 && rowSelected < 6) { MoveSelected = rowSelected; }
                    else if (colSelected == 2 && rowSelected < 5) { MoveSelected = rowSelected + 5; }
                    else if (colSelected == 3 && rowSelected < 7) { MoveSelected = rowSelected + 9; }
                    else if (colSelected == 4 && rowSelected < 7) { MoveSelected = rowSelected + 15; }
                    else { MoveSelected = -1; }
                    break;
                case 5:
                    if (colSelected == 1 && rowSelected < 5) { MoveSelected = rowSelected; }
                    else if (colSelected == 2 && rowSelected < 7) { MoveSelected = rowSelected + 4; }
                    else if (colSelected == 3 && rowSelected < 6) { MoveSelected = rowSelected + 10; }
                    else if (colSelected == 4 && rowSelected < 6) { MoveSelected = rowSelected + 15; }
                    else { MoveSelected = -1; }
                    break;
                case 6:
                    MoveSelected = rowSelected + ((colSelected - 1) * 6);
                    break;
                case 7:
                    if (colSelected == 1 && rowSelected < 7) { MoveSelected = rowSelected; }
                    else if (colSelected == 2 && rowSelected < 7) { MoveSelected = rowSelected + 6; }
                    else if (colSelected == 3 && rowSelected < 5) { MoveSelected = rowSelected + 12; }
                    else if (colSelected == 4 && rowSelected < 7) { MoveSelected = rowSelected + 16; }
                    else { MoveSelected = -1; }
                    break;
                case 8:
                    MoveSelected = rowSelected + ((colSelected - 1) * 6);
                    break;
                case 9:
                    if (colSelected == 1 && rowSelected < 7) { MoveSelected = rowSelected; }
                    else if (colSelected == 2 && rowSelected < 7) { MoveSelected = rowSelected + 6; }
                    else if (colSelected == 3 && rowSelected < 6) { MoveSelected = rowSelected + 12; }
                    else if (colSelected == 4 && rowSelected < 6) { MoveSelected = rowSelected + 17; }
                    else { MoveSelected = -1; }
                    break;
                case 10:
                    MoveSelected = rowSelected + ((colSelected - 1) * 6);
                    break;
                case 11:
                    if (colSelected == 1 && rowSelected < 3) { MoveSelected = rowSelected; }
                    else if (colSelected == 2 && rowSelected < 4) { MoveSelected = rowSelected + 2; }
                    else if (colSelected == 3 && rowSelected < 5) { MoveSelected = rowSelected + 5; }
                    else if (colSelected == 4 && rowSelected < 4) { MoveSelected = rowSelected + 9; }
                    else { MoveSelected = -1; }
                    break;
                case 12:
                    if (colSelected == 1 && rowSelected < 7) { MoveSelected = rowSelected; }
                    else if (colSelected == 2 && rowSelected < 2) { MoveSelected = rowSelected + 6; }
                    else if (colSelected == 3 && rowSelected < 3) { MoveSelected = rowSelected + 7; }
                    else if (colSelected == 4 && rowSelected < 5) { MoveSelected = rowSelected + 9; }
                    else { MoveSelected = -1; }
                    break;
                case 13:
                    if (colSelected == 1 && rowSelected < 7) { MoveSelected = rowSelected; }
                    else if (colSelected == 2 && rowSelected < 7) { MoveSelected = rowSelected + 6; }
                    else if (colSelected == 3 && rowSelected < 5) { MoveSelected = rowSelected + 12; }
                    else if (colSelected == 4 && rowSelected < 2) { MoveSelected = rowSelected + 16; }
                    else { MoveSelected = -1; }
                    break;
                case 14:
                    if (colSelected == 1 && rowSelected < 6) { MoveSelected = rowSelected; }
                    else if (colSelected == 2 && rowSelected < 5) { MoveSelected = rowSelected + 5; }
                    else if (colSelected == 3 && rowSelected < 5) { MoveSelected = rowSelected + 9; }
                    else if (colSelected == 4 && rowSelected < 4) { MoveSelected = rowSelected + 13; }
                    else { MoveSelected = -1; }
                    break;
                case 15:
                    if (colSelected == 1 && rowSelected < 5) { MoveSelected = rowSelected; }
                    else if (colSelected == 2 && rowSelected < 7) { MoveSelected = rowSelected + 4; }
                    else if (colSelected == 3 && rowSelected < 7) { MoveSelected = rowSelected + 10; }
                    else if (colSelected == 4 && rowSelected < 7) { MoveSelected = rowSelected + 16; }
                    else { MoveSelected = -1; }
                    break;
                case 16:
                    if (colSelected == 1 && rowSelected < 7) { MoveSelected = rowSelected; }
                    else if (colSelected == 2 && rowSelected < 6) { MoveSelected = rowSelected + 6; }
                    else if (colSelected == 3 && rowSelected < 6) { MoveSelected = rowSelected + 11; }
                    else if (colSelected == 4 && rowSelected < 7) { MoveSelected = rowSelected + 16; }
                    else { MoveSelected = -1; }
                    break;
                case 17:
                    if (colSelected == 1 && rowSelected < 4) { MoveSelected = rowSelected; }
                    else if (colSelected == 2 && rowSelected < 6) { MoveSelected = rowSelected + 3; }
                    else if (colSelected == 3 && rowSelected < 5) { MoveSelected = rowSelected + 8; }
                    else if (colSelected == 4 && rowSelected < 3) { MoveSelected = rowSelected + 12; }
                    else { MoveSelected = -1; }
                    break;
                case 18:
                    if (colSelected == 1 && rowSelected < 2) { MoveSelected = rowSelected; }
                    else if (colSelected == 2 && rowSelected < 7) { MoveSelected = rowSelected + 1; }
                    else if (colSelected == 3 && rowSelected < 4) { MoveSelected = rowSelected + 7; }
                    else if (colSelected == 4 && rowSelected < 6) { MoveSelected = rowSelected + 10; }
                    else { MoveSelected = -1; }
                    break;
                case 19:
                    if (colSelected == 1 && rowSelected < 4) { MoveSelected = rowSelected; }
                    else if (colSelected == 2 && rowSelected < 5) { MoveSelected = rowSelected + 3; }
                    else if (colSelected == 3 && rowSelected < 7) { MoveSelected = rowSelected + 7; }
                    else if (colSelected == 4 && rowSelected < 6) { MoveSelected = rowSelected + 13; }
                    else { MoveSelected = -1; }
                    break;
                case 20:
                    if (colSelected == 1 && rowSelected < 2) { MoveSelected = rowSelected; }
                    else if (colSelected == 2 && rowSelected < 3) { MoveSelected = rowSelected + 1; }
                    else if (colSelected == 3 && rowSelected < 5) { MoveSelected = rowSelected + 3; }
                    else if (colSelected == 4 && rowSelected < 4) { MoveSelected = rowSelected + 7; }
                    else { MoveSelected = -1; }
                    break;
                case 21:
                    if (colSelected == 1 && rowSelected < 5) { MoveSelected = rowSelected; }
                    else if (colSelected == 2 && rowSelected < 4) { MoveSelected = rowSelected + 4; }
                    else if (colSelected == 3 && rowSelected < 4) { MoveSelected = rowSelected + 7; }
                    else if (colSelected == 4 && rowSelected < 3) { MoveSelected = rowSelected + 10; }
                    else { MoveSelected = -1; }
                    break;
                case 22:
                    if (colSelected == 1 && rowSelected < 3) { MoveSelected = rowSelected; }
                    else if (colSelected == 2 && rowSelected < 5) { MoveSelected = rowSelected + 2; }
                    else if (colSelected == 3 && rowSelected < 4) { MoveSelected = rowSelected + 6; }
                    else if (colSelected == 4 && rowSelected < 5) { MoveSelected = rowSelected + 9; }
                    else { MoveSelected = -1; }
                    break;
                case 23:
                    if (colSelected == 1 && rowSelected < 4) { MoveSelected = rowSelected; }
                    else if (colSelected == 2 && rowSelected < 5) { MoveSelected = rowSelected + 3; }
                    else if (colSelected == 3 && rowSelected < 4) { MoveSelected = rowSelected + 7; }
                    else if (colSelected == 4 && rowSelected < 3) { MoveSelected = rowSelected + 10; }
                    else { MoveSelected = -1; }
                    break;
                case 24:
                    if (colSelected == 1 && rowSelected < 4) { MoveSelected = rowSelected; }
                    else if (colSelected == 2 && rowSelected < 4) { MoveSelected = rowSelected + 3; }
                    else if (colSelected == 3 && rowSelected < 5) { MoveSelected = rowSelected + 6; }
                    else if (colSelected == 4 && rowSelected < 3) { MoveSelected = rowSelected + 10; }
                    else { MoveSelected = -1; }
                    break;
                case 25:
                    if (colSelected == 1 && rowSelected < 5) { MoveSelected = rowSelected; }
                    else if (colSelected == 2 && rowSelected < 6) { MoveSelected = rowSelected + 4; }
                    else if (colSelected == 3 && rowSelected < 6) { MoveSelected = rowSelected + 9; }
                    else if (colSelected == 4 && rowSelected < 7) { MoveSelected = rowSelected + 14; }
                    else { MoveSelected = -1; }
                    break;
                case 26:
                    if (colSelected == 1 && rowSelected < 2) { MoveSelected = 1; }
                    else if (colSelected == 2 && rowSelected < 3) { MoveSelected = rowSelected + 1; }
                    else if (colSelected == 3 && rowSelected < 2) { MoveSelected = 4; }
                    else if (colSelected == 4 && rowSelected < 3) { MoveSelected = rowSelected + 4; }
                    else { MoveSelected = -1; }
                    break;
                case 27:
                    if (colSelected == 1 && rowSelected < 6) { MoveSelected = rowSelected; }
                    else if (colSelected == 2 && rowSelected < 7) { MoveSelected = rowSelected + 5; }
                    else if (colSelected == 3 && rowSelected < 4) { MoveSelected = rowSelected + 11; }
                    else if (colSelected == 4 && rowSelected < 4) { MoveSelected = rowSelected + 14; }
                    else { MoveSelected = -1; }
                    break;
                case 28:
                    if (colSelected == 1 && rowSelected < 5) { MoveSelected = rowSelected; }
                    else if (colSelected == 2 && rowSelected < 6) { MoveSelected = rowSelected + 4; }
                    else if (colSelected == 3 && rowSelected < 7) { MoveSelected = rowSelected + 9; }
                    else if (colSelected == 4 && rowSelected < 6) { MoveSelected = rowSelected + 15; }
                    else { MoveSelected = -1; }
                    break;
                case 29:
                    if (colSelected == 1 && rowSelected < 5) { MoveSelected = rowSelected; }
                    else if (colSelected == 2 && rowSelected < 6) { MoveSelected = rowSelected + 4; }
                    else if (colSelected == 3 && rowSelected < 7) { MoveSelected = rowSelected + 9; }
                    else if (colSelected == 4 && rowSelected < 7) { MoveSelected = rowSelected + 15; }
                    else { MoveSelected = -1; }
                    break;
                case 30:
                    if (colSelected == 1 && rowSelected < 5) { MoveSelected = rowSelected; }
                    else if (colSelected == 2 && rowSelected < 7) { MoveSelected = rowSelected + 4; }
                    else if (colSelected == 3 && rowSelected < 5) { MoveSelected = rowSelected + 10; }
                    else if (colSelected == 4 && rowSelected < 4) { MoveSelected = rowSelected + 14; }
                    else { MoveSelected = -1; }
                    break;
                case 31:
                    if (colSelected == 1 && rowSelected < 2) { MoveSelected = rowSelected; }
                    else if (colSelected == 2 && rowSelected < 2) { MoveSelected = rowSelected + 1; }
                    else if (colSelected == 3 && rowSelected < 5) { MoveSelected = rowSelected + 2; }
                    else if (colSelected == 4 && rowSelected < 4) { MoveSelected = rowSelected + 6; }
                    else { MoveSelected = -1; }
                    break;
                case 32:
                    if (colSelected == 1 && rowSelected < 3) { MoveSelected = rowSelected; }
                    else if (colSelected == 2 && rowSelected < 6) { MoveSelected = rowSelected + 2; }
                    else if (colSelected == 3 && rowSelected < 7) { MoveSelected = rowSelected + 7; }
                    else if (colSelected == 4 && rowSelected < 6) { MoveSelected = rowSelected + 13; }
                    else { MoveSelected = -1; }
                    break;
                case 33:
                    if (colSelected == 1 && rowSelected < 5) { MoveSelected = rowSelected; }
                    else if (colSelected == 2 && rowSelected < 4) { MoveSelected = rowSelected + 4; }
                    else if (colSelected == 3 && rowSelected < 4) { MoveSelected = rowSelected + 7; }
                    else if (colSelected == 4 && rowSelected < 3) { MoveSelected = rowSelected + 10; }
                    else { MoveSelected = -1; }
                    break;
                case 34:
                    if (colSelected == 1 && rowSelected < 7) { MoveSelected = rowSelected; }
                    else if (colSelected == 2 && rowSelected < 7) { MoveSelected = rowSelected + 6; }
                    else if (colSelected == 3 && rowSelected < 4) { MoveSelected = rowSelected + 12; }
                    else if (colSelected == 4 && rowSelected < 7) { MoveSelected = rowSelected + 15; }
                    else { MoveSelected = -1; }
                    break;
                case 35:
                    if (colSelected == 1 && rowSelected < 4) { MoveSelected = rowSelected; }
                    else if (colSelected == 2 && rowSelected < 3) { MoveSelected = rowSelected + 3; }
                    else if (colSelected == 3 && rowSelected < 3) { MoveSelected = rowSelected + 5; }
                    else if (colSelected == 4 && rowSelected < 6) { MoveSelected = rowSelected + 7; }
                    else { MoveSelected = -1; }
                    break;
                case 36:
                    if (colSelected == 1 && rowSelected < 4) { MoveSelected = rowSelected; }
                    else if (colSelected == 2 && rowSelected < 7) { MoveSelected = rowSelected + 3; }
                    else if (colSelected == 3 && rowSelected < 5) { MoveSelected = rowSelected + 9; }
                    else if (colSelected == 4 && rowSelected < 3) { MoveSelected = rowSelected + 13; }
                    else { MoveSelected = -1; }
                    break;
                case 37:
                    if (colSelected == 1 && rowSelected < 3) { MoveSelected = rowSelected; }
                    else if (colSelected == 2 && rowSelected < 5) { MoveSelected = rowSelected + 2; }
                    else if (colSelected == 3 && rowSelected < 4) { MoveSelected = rowSelected + 6; }
                    else if (colSelected == 4 && rowSelected < 4) { MoveSelected = rowSelected + 9; }
                    else { MoveSelected = -1; }
                    break;
                default:
                    MoveSelected = -1; break;
            }
            GenerateMoveInfo();
        }


        private string GenerateStatValue(int statID, int statValue)
        {
            switch (statID)
            {
                case 0: // Force
                    if (statValue == 0) return "---";
                    else if (statValue > 0 && statValue <= 9) return "E (" + statValue + ")";
                    else if (statValue >= 10 && statValue <= 19) return "D (" + statValue + ")";
                    else if (statValue >= 20 && statValue <= 29) return "C (" + statValue + ")";
                    else if (statValue >= 30 && statValue <= 39) return "B (" + statValue + ")";
                    else if (statValue >= 40 && statValue <= 49) return "A (" + statValue + ")";
                    else return "S (" + statValue + ")";
                case 1: // Hit... a weird case. It can apparently go from -99 to 156.
                    int modStatValue = statValue;
                    while (modStatValue > 156) // Values can be dumped verbatim from memory.
                        modStatValue -= 256;

                    if (modStatValue <= -15) return "E (" + statValue + ")";
                    else if (modStatValue >= -14 && modStatValue <= -5) return "D (" + statValue + ")";
                    else if (modStatValue >= -4 && modStatValue <= 0) return "C (" + statValue + ")";
                    else if (statValue >= 1 && statValue <= 4) return "B (" + statValue + ")";
                    else if (statValue >= 5 && statValue <= 14) return "A (" + statValue + ")";
                    else return "S (" + statValue + ")";
                case 2: // Withering
                    if (statValue == 0) return "---";
                    else if (statValue > 0 && statValue <= 9) return "E (" + statValue + ")";
                    else if (statValue >= 10 && statValue <= 19) return "D (" + statValue + ")";
                    else if (statValue >= 20 && statValue <= 29) return "C (" + statValue + ")";
                    else if (statValue >= 30 && statValue <= 39) return "B (" + statValue + ")";
                    else if (statValue >= 40 && statValue <= 49) return "A (" + statValue + ")";
                    else return "S (" + statValue + ")";
                case 3: // Sharpness
                    if (statValue == 0) return "---";
                    else if (statValue > 0 && statValue <= 9) return "E (" + statValue + ")";
                    else if (statValue >= 10 && statValue <= 14) return "D (" + statValue + ")";
                    else if (statValue >= 15 && statValue <= 19) return "C (" + statValue + ")";
                    else if (statValue >= 20 && statValue <= 24) return "B (" + statValue + ")";
                    else if (statValue >= 25 && statValue <= 29) return "A (" + statValue + ")";
                    else return "S (" + statValue + ")";
                default: // Assume Default is Force calculation anyway.
                    if (statValue == 0) return "---";
                    else if (statValue > 0 && statValue <= 9) return "E (" + statValue + ")";
                    else if (statValue >= 10 && statValue <= 19) return "D (" + statValue + ")";
                    else if (statValue >= 20 && statValue <= 29) return "C (" + statValue + ")";
                    else if (statValue >= 30 && statValue <= 39) return "B (" + statValue + ")";
                    else if (statValue >= 40 && statValue <= 49) return "A (" + statValue + ")";
                    else return "S (" + statValue + ")";
            }
        }

        private void MonMoveWindow_Load(object sender, EventArgs e)
        {
            Mon_Genus = -1;
            PopulateMoveIcons();
            GenerateMoveInfo();
            MMITimer.Interval = 500;
            MMITimer.Start();

        }

        private void MMITimer_Tick(object sender, EventArgs e)
        {
            if (Mon_Genus != oldGenus)
                PopulateMoveIcons();
            GenerateMoveInfo();

            int curMove = MonActMoves[0];

            PictureBox[] MoveBoxes = { Slot1_1, Slot1_2, Slot1_3, Slot1_4, Slot1_5, Slot1_6,
                                        Slot2_1, Slot2_2, Slot2_3, Slot2_4, Slot2_5, Slot2_6,
                                        Slot3_1, Slot3_2, Slot3_3, Slot3_4, Slot3_5, Slot3_6,
                                        Slot4_1, Slot4_2, Slot4_3, Slot4_4, Slot4_5, Slot4_6 };

            if (MonActMoves[0] != 24 && MonActMoves[0] < 6)
            {
                curMove = MonActMoves[0];
                if (Mon_Genus == 0 && curMove >= 3 && curMove <= 6)
                    curMove--;

                ActiveR1.Image = MoveBoxes[curMove].Image;
            }
            else
            {
                ActiveR1.Image = Properties.Resources.No_Move;
            }

            if (MonActMoves[1] != 24 && MonActMoves[1] < 12 && MonActMoves[1] > 5)
            {
                curMove = MonActMoves[1];

                if (Mon_Genus == 0 && curMove >= 7 && curMove <= 11)
                    curMove--;
                ActiveR2.Image = MoveBoxes[curMove].Image;
            }
            else
            {
                ActiveR2.Image = Properties.Resources.No_Move;
            }

            if (MonActMoves[2] != 24 && MonActMoves[2] < 18 && MonActMoves[2] > 11)
            {
                ActiveR3.Image = MoveBoxes[MonActMoves[2]].Image;
            }
            else
            {
                ActiveR3.Image = Properties.Resources.No_Move;
            }

            if (MonActMoves[3] != 24 && MonActMoves[3] < 24 && MonActMoves[3] > 17)
            {
                curMove = MonActMoves[3];

                if (Mon_Genus == 0 && curMove >= 20 && curMove <= 23)
                    curMove--;
                ActiveR4.Image = MoveBoxes[curMove].Image;
            }
            else
            {
                ActiveR4.Image = Properties.Resources.No_Move;
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {
            label2.Text = MonActMoves[3] + " / " + MonActMoves[2] + " / " + MonActMoves[1] + " / " + MonActMoves[0];
        }

        private void PopulateMoveIcons()
        {
            switch(Mon_Genus)
            {
                case 0:
                    Slot1_1.Image = Properties.Resources.Pixie_1;
                    Slot1_2.Image = Properties.Resources.Pixie_2;
                    Slot1_3.Image = Properties.Resources.Pixie_3;
                    Slot1_4.Image = Properties.Resources.Pixie_4;
                    Slot1_5.Image = Properties.Resources.Pixie_5;
                    Slot1_6.Image = Properties.Resources.No_Move;
                    Slot2_1.Image = Properties.Resources.Pixie_6;
                    Slot2_2.Image = Properties.Resources.Pixie_7;
                    Slot2_3.Image = Properties.Resources.Pixie_8;
                    Slot2_4.Image = Properties.Resources.Pixie_9;
                    Slot2_5.Image = Properties.Resources.Pixie_10;
                    Slot2_6.Image = Properties.Resources.No_Move;
                    Slot3_1.Image = Properties.Resources.Pixie_11;
                    Slot3_2.Image = Properties.Resources.Pixie_12;
                    Slot3_3.Image = Properties.Resources.Pixie_13;
                    Slot3_4.Image = Properties.Resources.Pixie_14;
                    Slot3_5.Image = Properties.Resources.Pixie_15;
                    Slot3_6.Image = Properties.Resources.Pixie_16;
                    Slot4_1.Image = Properties.Resources.Pixie_17;
                    Slot4_2.Image = Properties.Resources.Pixie_18;
                    Slot4_3.Image = Properties.Resources.Pixie_19;
                    Slot4_4.Image = Properties.Resources.Pixie_20;
                    Slot4_5.Image = Properties.Resources.Pixie_21;
                    Slot4_6.Image = Properties.Resources.No_Move;
                    break;
                case 1:
                    Slot1_1.Image = Properties.Resources.Drago_1;
                    Slot1_2.Image = Properties.Resources.Drago_2;
                    Slot1_3.Image = Properties.Resources.Drago_3;
                    Slot1_4.Image = Properties.Resources.Drago_4;
                    Slot1_5.Image = Properties.Resources.Drago_5;
                    Slot1_6.Image = Properties.Resources.No_Move;
                    Slot2_1.Image = Properties.Resources.Drago_6;
                    Slot2_2.Image = Properties.Resources.Drago_7;
                    Slot2_3.Image = Properties.Resources.Drago_8;
                    Slot2_4.Image = Properties.Resources.Drago_9;
                    Slot2_5.Image = Properties.Resources.Drago_10;
                    Slot2_6.Image = Properties.Resources.No_Move;
                    Slot3_1.Image = Properties.Resources.Drago_11;
                    Slot3_2.Image = Properties.Resources.Drago_12;
                    Slot3_3.Image = Properties.Resources.Drago_13;
                    Slot3_4.Image = Properties.Resources.Drago_14;
                    Slot3_5.Image = Properties.Resources.Drago_15;
                    Slot3_6.Image = Properties.Resources.No_Move;
                    Slot4_1.Image = Properties.Resources.Drago_16;
                    Slot4_2.Image = Properties.Resources.Drago_17;
                    Slot4_3.Image = Properties.Resources.Drago_18;
                    Slot4_4.Image = Properties.Resources.Drago_19;
                    Slot4_5.Image = Properties.Resources.No_Move;
                    Slot4_6.Image = Properties.Resources.No_Move;
                    break;
                case 2:
                    Slot1_1.Image = Properties.Resources.Centa_1;
                    Slot1_2.Image = Properties.Resources.Centa_2;
                    Slot1_3.Image = Properties.Resources.Centa_3;
                    Slot1_4.Image = Properties.Resources.Centa_4;
                    Slot1_5.Image = Properties.Resources.Centa_5;
                    Slot1_6.Image = Properties.Resources.No_Move;
                    Slot2_1.Image = Properties.Resources.Centa_6;
                    Slot2_2.Image = Properties.Resources.Centa_7;
                    Slot2_3.Image = Properties.Resources.Centa_8;
                    Slot2_4.Image = Properties.Resources.Centa_9;
                    Slot2_5.Image = Properties.Resources.No_Move;
                    Slot2_6.Image = Properties.Resources.No_Move;
                    Slot3_1.Image = Properties.Resources.Centa_10;
                    Slot3_2.Image = Properties.Resources.Centa_11;
                    Slot3_3.Image = Properties.Resources.Centa_12;
                    Slot3_4.Image = Properties.Resources.Centa_13;
                    Slot3_5.Image = Properties.Resources.No_Move;
                    Slot3_6.Image = Properties.Resources.No_Move;
                    Slot4_1.Image = Properties.Resources.Centa_14;
                    Slot4_2.Image = Properties.Resources.Centa_15;
                    Slot4_3.Image = Properties.Resources.Centa_16;
                    Slot4_4.Image = Properties.Resources.Centa_17;
                    Slot4_5.Image = Properties.Resources.No_Move;
                    Slot4_6.Image = Properties.Resources.No_Move;
                    break;
                case 3:
                    Slot1_1.Image = Properties.Resources.Color_1;
                    Slot1_2.Image = Properties.Resources.Color_2;
                    Slot1_3.Image = Properties.Resources.No_Move;
                    Slot1_4.Image = Properties.Resources.No_Move;
                    Slot1_5.Image = Properties.Resources.No_Move;
                    Slot1_6.Image = Properties.Resources.No_Move;
                    Slot2_1.Image = Properties.Resources.Color_3;
                    Slot2_2.Image = Properties.Resources.Color_4;
                    Slot2_3.Image = Properties.Resources.Color_5;
                    Slot2_4.Image = Properties.Resources.Color_6;
                    Slot2_5.Image = Properties.Resources.No_Move;
                    Slot2_6.Image = Properties.Resources.No_Move;
                    Slot3_1.Image = Properties.Resources.Color_7;
                    Slot3_2.Image = Properties.Resources.Color_8;
                    Slot3_3.Image = Properties.Resources.Color_9;
                    Slot3_4.Image = Properties.Resources.Color_10;
                    Slot3_5.Image = Properties.Resources.No_Move;
                    Slot3_6.Image = Properties.Resources.No_Move;
                    Slot4_1.Image = Properties.Resources.Color_11;
                    Slot4_2.Image = Properties.Resources.Color_12;
                    Slot4_3.Image = Properties.Resources.Color_13;
                    Slot4_4.Image = Properties.Resources.Color_14;
                    Slot4_5.Image = Properties.Resources.Color_15;
                    Slot4_6.Image = Properties.Resources.No_Move;
                    break;
                case 4:
                    Slot1_1.Image = Properties.Resources.Beacl_1;
                    Slot1_2.Image = Properties.Resources.Beacl_2;
                    Slot1_3.Image = Properties.Resources.Beacl_3;
                    Slot1_4.Image = Properties.Resources.Beacl_4;
                    Slot1_5.Image = Properties.Resources.Beacl_5;
                    Slot1_6.Image = Properties.Resources.No_Move;
                    Slot2_1.Image = Properties.Resources.Beacl_6;
                    Slot2_2.Image = Properties.Resources.Beacl_7;
                    Slot2_3.Image = Properties.Resources.Beacl_8;
                    Slot2_4.Image = Properties.Resources.Beacl_9;
                    Slot2_5.Image = Properties.Resources.No_Move;
                    Slot2_6.Image = Properties.Resources.No_Move;
                    Slot3_1.Image = Properties.Resources.Beacl_10;
                    Slot3_2.Image = Properties.Resources.Beacl_11;
                    Slot3_3.Image = Properties.Resources.Beacl_12;
                    Slot3_4.Image = Properties.Resources.Beacl_13;
                    Slot3_5.Image = Properties.Resources.Beacl_14;
                    Slot3_6.Image = Properties.Resources.Beacl_15;
                    Slot4_1.Image = Properties.Resources.Beacl_16;
                    Slot4_2.Image = Properties.Resources.Beacl_17;
                    Slot4_3.Image = Properties.Resources.Beacl_18;
                    Slot4_4.Image = Properties.Resources.Beacl_19;
                    Slot4_5.Image = Properties.Resources.Beacl_20;
                    Slot4_6.Image = Properties.Resources.Beacl_21;
                    break;
                case 5:
                    Slot1_1.Image = Properties.Resources.Henge_1;
                    Slot1_2.Image = Properties.Resources.Henge_2;
                    Slot1_3.Image = Properties.Resources.Henge_3;
                    Slot1_4.Image = Properties.Resources.Henge_4;
                    Slot1_5.Image = Properties.Resources.No_Move;
                    Slot1_6.Image = Properties.Resources.No_Move;
                    Slot2_1.Image = Properties.Resources.Henge_5;
                    Slot2_2.Image = Properties.Resources.Henge_6;
                    Slot2_3.Image = Properties.Resources.Henge_7;
                    Slot2_4.Image = Properties.Resources.Henge_8;
                    Slot2_5.Image = Properties.Resources.Henge_9;
                    Slot2_6.Image = Properties.Resources.Henge_10;
                    Slot3_1.Image = Properties.Resources.Henge_11;
                    Slot3_2.Image = Properties.Resources.Henge_12;
                    Slot3_3.Image = Properties.Resources.Henge_13;
                    Slot3_4.Image = Properties.Resources.Henge_14;
                    Slot3_5.Image = Properties.Resources.Henge_15;
                    Slot3_6.Image = Properties.Resources.No_Move;
                    Slot4_1.Image = Properties.Resources.Henge_16;
                    Slot4_2.Image = Properties.Resources.Henge_17;
                    Slot4_3.Image = Properties.Resources.Henge_18;
                    Slot4_4.Image = Properties.Resources.Henge_19;
                    Slot4_5.Image = Properties.Resources.Henge_20;
                    Slot4_6.Image = Properties.Resources.No_Move;
                    break;
                case 6:
                    Slot1_1.Image = Properties.Resources.Wrack_1;
                    Slot1_2.Image = Properties.Resources.Wrack_2;
                    Slot1_3.Image = Properties.Resources.Wrack_3;
                    Slot1_4.Image = Properties.Resources.Wrack_4;
                    Slot1_5.Image = Properties.Resources.Wrack_5;
                    Slot1_6.Image = Properties.Resources.Wrack_6;
                    Slot2_1.Image = Properties.Resources.Wrack_7;
                    Slot2_2.Image = Properties.Resources.Wrack_8;
                    Slot2_3.Image = Properties.Resources.Wrack_9;
                    Slot2_4.Image = Properties.Resources.Wrack_10;
                    Slot2_5.Image = Properties.Resources.Wrack_11;
                    Slot2_6.Image = Properties.Resources.Wrack_12;
                    Slot3_1.Image = Properties.Resources.Wrack_13;
                    Slot3_2.Image = Properties.Resources.Wrack_14;
                    Slot3_3.Image = Properties.Resources.Wrack_15;
                    Slot3_4.Image = Properties.Resources.Wrack_16;
                    Slot3_5.Image = Properties.Resources.Wrack_17;
                    Slot3_6.Image = Properties.Resources.Wrack_18;
                    Slot4_1.Image = Properties.Resources.Wrack_19;
                    Slot4_2.Image = Properties.Resources.Wrack_20;
                    Slot4_3.Image = Properties.Resources.Wrack_21;
                    Slot4_4.Image = Properties.Resources.Wrack_22;
                    Slot4_5.Image = Properties.Resources.Wrack_23;
                    Slot4_6.Image = Properties.Resources.Wrack_24;
                    break;
                case 7:
                    Slot1_1.Image = Properties.Resources.Golem_1;
                    Slot1_2.Image = Properties.Resources.Golem_2;
                    Slot1_3.Image = Properties.Resources.Golem_3;
                    Slot1_4.Image = Properties.Resources.Golem_4;
                    Slot1_5.Image = Properties.Resources.Golem_5;
                    Slot1_6.Image = Properties.Resources.Golem_6;
                    Slot2_1.Image = Properties.Resources.Golem_7;
                    Slot2_2.Image = Properties.Resources.Golem_8;
                    Slot2_3.Image = Properties.Resources.Golem_9;
                    Slot2_4.Image = Properties.Resources.Golem_10;
                    Slot2_5.Image = Properties.Resources.Golem_11;
                    Slot2_6.Image = Properties.Resources.Golem_12;
                    Slot3_1.Image = Properties.Resources.Golem_13;
                    Slot3_2.Image = Properties.Resources.Golem_14;
                    Slot3_3.Image = Properties.Resources.Golem_15;
                    Slot3_4.Image = Properties.Resources.Golem_16;
                    Slot3_5.Image = Properties.Resources.No_Move;
                    Slot3_6.Image = Properties.Resources.No_Move;
                    Slot4_1.Image = Properties.Resources.Golem_17;
                    Slot4_2.Image = Properties.Resources.Golem_18;
                    Slot4_3.Image = Properties.Resources.Golem_19;
                    Slot4_4.Image = Properties.Resources.Golem_20;
                    Slot4_5.Image = Properties.Resources.Golem_21;
                    Slot4_6.Image = Properties.Resources.Golem_22;
                    break;
                case 8:
                    Slot1_1.Image = Properties.Resources.Zuum_1;
                    Slot1_2.Image = Properties.Resources.Zuum_2;
                    Slot1_3.Image = Properties.Resources.Zuum_3;
                    Slot1_4.Image = Properties.Resources.Zuum_4;
                    Slot1_5.Image = Properties.Resources.Zuum_5;
                    Slot1_6.Image = Properties.Resources.Zuum_6;
                    Slot2_1.Image = Properties.Resources.Zuum_7;
                    Slot2_2.Image = Properties.Resources.Zuum_8;
                    Slot2_3.Image = Properties.Resources.Zuum_9;
                    Slot2_4.Image = Properties.Resources.Zuum_10;
                    Slot2_5.Image = Properties.Resources.Zuum_11;
                    Slot2_6.Image = Properties.Resources.Zuum_12;
                    Slot3_1.Image = Properties.Resources.Zuum_13;
                    Slot3_2.Image = Properties.Resources.Zuum_14;
                    Slot3_3.Image = Properties.Resources.Zuum_15;
                    Slot3_4.Image = Properties.Resources.Zuum_16;
                    Slot3_5.Image = Properties.Resources.Zuum_17;
                    Slot3_6.Image = Properties.Resources.Zuum_18;
                    Slot4_1.Image = Properties.Resources.Zuum_19;
                    Slot4_2.Image = Properties.Resources.Zuum_20;
                    Slot4_3.Image = Properties.Resources.Zuum_21;
                    Slot4_4.Image = Properties.Resources.Zuum_22;
                    Slot4_5.Image = Properties.Resources.Zuum_23;
                    Slot4_6.Image = Properties.Resources.Zuum_24;
                    break;
                case 9:
                    Slot1_1.Image = Properties.Resources.Durah_1;
                    Slot1_2.Image = Properties.Resources.Durah_2;
                    Slot1_3.Image = Properties.Resources.Durah_3;
                    Slot1_4.Image = Properties.Resources.Durah_4;
                    Slot1_5.Image = Properties.Resources.Durah_5;
                    Slot1_6.Image = Properties.Resources.Durah_6;
                    Slot2_1.Image = Properties.Resources.Durah_7;
                    Slot2_2.Image = Properties.Resources.Durah_8;
                    Slot2_3.Image = Properties.Resources.Durah_9;
                    Slot2_4.Image = Properties.Resources.Durah_10;
                    Slot2_5.Image = Properties.Resources.Durah_11;
                    Slot2_6.Image = Properties.Resources.Durah_12;
                    Slot3_1.Image = Properties.Resources.Durah_13;
                    Slot3_2.Image = Properties.Resources.Durah_14;
                    Slot3_3.Image = Properties.Resources.Durah_15;
                    Slot3_4.Image = Properties.Resources.Durah_16;
                    Slot3_5.Image = Properties.Resources.Durah_17;
                    Slot3_6.Image = Properties.Resources.No_Move;
                    Slot4_1.Image = Properties.Resources.Durah_18;
                    Slot4_2.Image = Properties.Resources.Durah_19;
                    Slot4_3.Image = Properties.Resources.Durah_20;
                    Slot4_4.Image = Properties.Resources.Durah_21;
                    Slot4_5.Image = Properties.Resources.Durah_22;
                    Slot4_6.Image = Properties.Resources.No_Move;
                    break;
                case 10:
                    Slot1_1.Image = Properties.Resources.Arrow_1;
                    Slot1_2.Image = Properties.Resources.Arrow_2;
                    Slot1_3.Image = Properties.Resources.Arrow_3;
                    Slot1_4.Image = Properties.Resources.Arrow_4;
                    Slot1_5.Image = Properties.Resources.Arrow_5;
                    Slot1_6.Image = Properties.Resources.Arrow_6;
                    Slot2_1.Image = Properties.Resources.Arrow_7;
                    Slot2_2.Image = Properties.Resources.Arrow_8;
                    Slot2_3.Image = Properties.Resources.Arrow_9;
                    Slot2_4.Image = Properties.Resources.Arrow_10;
                    Slot2_5.Image = Properties.Resources.Arrow_11;
                    Slot2_6.Image = Properties.Resources.Arrow_12;
                    Slot3_1.Image = Properties.Resources.Arrow_13;
                    Slot3_2.Image = Properties.Resources.Arrow_14;
                    Slot3_3.Image = Properties.Resources.Arrow_15;
                    Slot3_4.Image = Properties.Resources.Arrow_16;
                    Slot3_5.Image = Properties.Resources.Arrow_17;
                    Slot3_6.Image = Properties.Resources.Arrow_18;
                    Slot4_1.Image = Properties.Resources.Arrow_19;
                    Slot4_2.Image = Properties.Resources.Arrow_20;
                    Slot4_3.Image = Properties.Resources.Arrow_21;
                    Slot4_4.Image = Properties.Resources.Arrow_22;
                    Slot4_5.Image = Properties.Resources.Arrow_23;
                    Slot4_6.Image = Properties.Resources.Arrow_24;
                    break;
                case 11:
                    Slot1_1.Image = Properties.Resources.Tiger_1;
                    Slot1_2.Image = Properties.Resources.Tiger_2;
                    Slot1_3.Image = Properties.Resources.No_Move;
                    Slot1_4.Image = Properties.Resources.No_Move;
                    Slot1_5.Image = Properties.Resources.No_Move;
                    Slot1_6.Image = Properties.Resources.No_Move;
                    Slot2_1.Image = Properties.Resources.Tiger_3;
                    Slot2_2.Image = Properties.Resources.Tiger_4;
                    Slot2_3.Image = Properties.Resources.Tiger_5;
                    Slot2_4.Image = Properties.Resources.No_Move;
                    Slot2_5.Image = Properties.Resources.No_Move;
                    Slot2_6.Image = Properties.Resources.No_Move;
                    Slot3_1.Image = Properties.Resources.Tiger_6;
                    Slot3_2.Image = Properties.Resources.Tiger_7;
                    Slot3_3.Image = Properties.Resources.Tiger_8;
                    Slot3_4.Image = Properties.Resources.Tiger_9;
                    Slot3_5.Image = Properties.Resources.No_Move;
                    Slot3_6.Image = Properties.Resources.No_Move;
                    Slot4_1.Image = Properties.Resources.Tiger_10;
                    Slot4_2.Image = Properties.Resources.Tiger_11;
                    Slot4_3.Image = Properties.Resources.Tiger_12;
                    Slot4_4.Image = Properties.Resources.No_Move;
                    Slot4_5.Image = Properties.Resources.No_Move;
                    Slot4_6.Image = Properties.Resources.No_Move;
                    break;
                case 12:
                    Slot1_1.Image = Properties.Resources.Hoppe_1;
                    Slot1_2.Image = Properties.Resources.Hoppe_2;
                    Slot1_3.Image = Properties.Resources.Hoppe_3;
                    Slot1_4.Image = Properties.Resources.Hoppe_4;
                    Slot1_5.Image = Properties.Resources.Hoppe_5;
                    Slot1_6.Image = Properties.Resources.Hoppe_6;
                    Slot2_1.Image = Properties.Resources.Hoppe_7;
                    Slot2_2.Image = Properties.Resources.No_Move;
                    Slot2_3.Image = Properties.Resources.No_Move;
                    Slot2_4.Image = Properties.Resources.No_Move;
                    Slot2_5.Image = Properties.Resources.No_Move;
                    Slot2_6.Image = Properties.Resources.No_Move;
                    Slot3_1.Image = Properties.Resources.Hoppe_8;
                    Slot3_2.Image = Properties.Resources.Hoppe_9;
                    Slot3_3.Image = Properties.Resources.No_Move;
                    Slot3_4.Image = Properties.Resources.No_Move;
                    Slot3_5.Image = Properties.Resources.No_Move;
                    Slot3_6.Image = Properties.Resources.No_Move;
                    Slot4_1.Image = Properties.Resources.Hoppe_10;
                    Slot4_2.Image = Properties.Resources.Hoppe_11;
                    Slot4_3.Image = Properties.Resources.Hoppe_12;
                    Slot4_4.Image = Properties.Resources.Hoppe_13;
                    Slot4_5.Image = Properties.Resources.No_Move;
                    Slot4_6.Image = Properties.Resources.No_Move;
                    break;
                case 13:
                    Slot1_1.Image = Properties.Resources.Hare_1;
                    Slot1_2.Image = Properties.Resources.Hare_2;
                    Slot1_3.Image = Properties.Resources.Hare_3;
                    Slot1_4.Image = Properties.Resources.Hare_4;
                    Slot1_5.Image = Properties.Resources.Hare_5;
                    Slot1_6.Image = Properties.Resources.Hare_6;
                    Slot2_1.Image = Properties.Resources.Hare_7;
                    Slot2_2.Image = Properties.Resources.Hare_8;
                    Slot2_3.Image = Properties.Resources.Hare_9;
                    Slot2_4.Image = Properties.Resources.Hare_10;
                    Slot2_5.Image = Properties.Resources.Hare_11;
                    Slot2_6.Image = Properties.Resources.Hare_12;
                    Slot3_1.Image = Properties.Resources.Hare_13;
                    Slot3_2.Image = Properties.Resources.Hare_14;
                    Slot3_3.Image = Properties.Resources.Hare_15;
                    Slot3_4.Image = Properties.Resources.Hare_16;
                    Slot3_5.Image = Properties.Resources.No_Move;
                    Slot3_6.Image = Properties.Resources.No_Move;
                    Slot4_1.Image = Properties.Resources.Hare_17;
                    Slot4_2.Image = Properties.Resources.No_Move;
                    Slot4_3.Image = Properties.Resources.No_Move;
                    Slot4_4.Image = Properties.Resources.No_Move;
                    Slot4_5.Image = Properties.Resources.No_Move;
                    Slot4_6.Image = Properties.Resources.No_Move;
                    break;
                case 14:
                    Slot1_1.Image = Properties.Resources.Baku_1;
                    Slot1_2.Image = Properties.Resources.Baku_2;
                    Slot1_3.Image = Properties.Resources.Baku_3;
                    Slot1_4.Image = Properties.Resources.Baku_4;
                    Slot1_5.Image = Properties.Resources.Baku_5;
                    Slot1_6.Image = Properties.Resources.No_Move;
                    Slot2_1.Image = Properties.Resources.Baku_6;
                    Slot2_2.Image = Properties.Resources.Baku_7;
                    Slot2_3.Image = Properties.Resources.Baku_8;
                    Slot2_4.Image = Properties.Resources.Baku_9;
                    Slot2_5.Image = Properties.Resources.No_Move;
                    Slot2_6.Image = Properties.Resources.No_Move;
                    Slot3_1.Image = Properties.Resources.Baku_10;
                    Slot3_2.Image = Properties.Resources.Baku_11;
                    Slot3_3.Image = Properties.Resources.Baku_12;
                    Slot3_4.Image = Properties.Resources.Baku_13;
                    Slot3_5.Image = Properties.Resources.No_Move;
                    Slot3_6.Image = Properties.Resources.No_Move;
                    Slot4_1.Image = Properties.Resources.Baku_14;
                    Slot4_2.Image = Properties.Resources.Baku_15;
                    Slot4_3.Image = Properties.Resources.Baku_16;
                    Slot4_4.Image = Properties.Resources.No_Move;
                    Slot4_5.Image = Properties.Resources.No_Move;
                    Slot4_6.Image = Properties.Resources.No_Move;
                    break;
                case 15:
                    Slot1_1.Image = Properties.Resources.Gali_1;
                    Slot1_2.Image = Properties.Resources.Gali_2;
                    Slot1_3.Image = Properties.Resources.Gali_3;
                    Slot1_4.Image = Properties.Resources.Gali_4;
                    Slot1_5.Image = Properties.Resources.No_Move;
                    Slot1_6.Image = Properties.Resources.No_Move;
                    Slot2_1.Image = Properties.Resources.Gali_5;
                    Slot2_2.Image = Properties.Resources.Gali_6;
                    Slot2_3.Image = Properties.Resources.Gali_7;
                    Slot2_4.Image = Properties.Resources.Gali_8;
                    Slot2_5.Image = Properties.Resources.Gali_9;
                    Slot2_6.Image = Properties.Resources.Gali_10;
                    Slot3_1.Image = Properties.Resources.Gali_11;
                    Slot3_2.Image = Properties.Resources.Gali_12;
                    Slot3_3.Image = Properties.Resources.Gali_13;
                    Slot3_4.Image = Properties.Resources.Gali_14;
                    Slot3_5.Image = Properties.Resources.Gali_15;
                    Slot3_6.Image = Properties.Resources.Gali_16;
                    Slot4_1.Image = Properties.Resources.Gali_17;
                    Slot4_2.Image = Properties.Resources.Gali_18;
                    Slot4_3.Image = Properties.Resources.Gali_19;
                    Slot4_4.Image = Properties.Resources.Gali_20;
                    Slot4_5.Image = Properties.Resources.Gali_21;
                    Slot4_6.Image = Properties.Resources.Gali_22;
                    break;
                case 16:
                    Slot1_1.Image = Properties.Resources.Kato_1;
                    Slot1_2.Image = Properties.Resources.Kato_2;
                    Slot1_3.Image = Properties.Resources.Kato_3;
                    Slot1_4.Image = Properties.Resources.Kato_4;
                    Slot1_5.Image = Properties.Resources.Kato_5;
                    Slot1_6.Image = Properties.Resources.Kato_6;
                    Slot2_1.Image = Properties.Resources.Kato_7;
                    Slot2_2.Image = Properties.Resources.Kato_8;
                    Slot2_3.Image = Properties.Resources.Kato_9;
                    Slot2_4.Image = Properties.Resources.Kato_10;
                    Slot2_5.Image = Properties.Resources.Kato_11;
                    Slot2_6.Image = Properties.Resources.No_Move;
                    Slot3_1.Image = Properties.Resources.Kato_12;
                    Slot3_2.Image = Properties.Resources.Kato_13;
                    Slot3_3.Image = Properties.Resources.Kato_14;
                    Slot3_4.Image = Properties.Resources.Kato_15;
                    Slot3_5.Image = Properties.Resources.Kato_16;
                    Slot3_6.Image = Properties.Resources.No_Move;
                    Slot4_1.Image = Properties.Resources.Kato_17;
                    Slot4_2.Image = Properties.Resources.Kato_18;
                    Slot4_3.Image = Properties.Resources.Kato_19;
                    Slot4_4.Image = Properties.Resources.Kato_20;
                    Slot4_5.Image = Properties.Resources.Kato_21;
                    Slot4_6.Image = Properties.Resources.Kato_22;
                    break;
                case 17:
                    Slot1_1.Image = Properties.Resources.Zilla_1;
                    Slot1_2.Image = Properties.Resources.Zilla_2;
                    Slot1_3.Image = Properties.Resources.Zilla_3;
                    Slot1_4.Image = Properties.Resources.No_Move;
                    Slot1_5.Image = Properties.Resources.No_Move;
                    Slot1_6.Image = Properties.Resources.No_Move;
                    Slot2_1.Image = Properties.Resources.Zilla_4;
                    Slot2_2.Image = Properties.Resources.Zilla_5;
                    Slot2_3.Image = Properties.Resources.Zilla_6;
                    Slot2_4.Image = Properties.Resources.Zilla_7;
                    Slot2_5.Image = Properties.Resources.Zilla_8;
                    Slot2_6.Image = Properties.Resources.No_Move;
                    Slot3_1.Image = Properties.Resources.Zilla_9;
                    Slot3_2.Image = Properties.Resources.Zilla_10;
                    Slot3_3.Image = Properties.Resources.Zilla_11;
                    Slot3_4.Image = Properties.Resources.Zilla_12;
                    Slot3_5.Image = Properties.Resources.No_Move;
                    Slot3_6.Image = Properties.Resources.No_Move;
                    Slot4_1.Image = Properties.Resources.Zilla_13;
                    Slot4_2.Image = Properties.Resources.Zilla_14;
                    Slot4_3.Image = Properties.Resources.No_Move;
                    Slot4_4.Image = Properties.Resources.No_Move;
                    Slot4_5.Image = Properties.Resources.No_Move;
                    Slot4_6.Image = Properties.Resources.No_Move;
                    break;
                case 18:
                    Slot1_1.Image = Properties.Resources.Bajar_1;
                    Slot1_2.Image = Properties.Resources.No_Move;
                    Slot1_3.Image = Properties.Resources.No_Move;
                    Slot1_4.Image = Properties.Resources.No_Move;
                    Slot1_5.Image = Properties.Resources.No_Move;
                    Slot1_6.Image = Properties.Resources.No_Move;
                    Slot2_1.Image = Properties.Resources.Bajar_2;
                    Slot2_2.Image = Properties.Resources.Bajar_3;
                    Slot2_3.Image = Properties.Resources.Bajar_4;
                    Slot2_4.Image = Properties.Resources.Bajar_5;
                    Slot2_5.Image = Properties.Resources.Bajar_6;
                    Slot2_6.Image = Properties.Resources.Bajar_7;
                    Slot3_1.Image = Properties.Resources.Bajar_8;
                    Slot3_2.Image = Properties.Resources.Bajar_9;
                    Slot3_3.Image = Properties.Resources.Bajar_10;
                    Slot3_4.Image = Properties.Resources.No_Move;
                    Slot3_5.Image = Properties.Resources.No_Move;
                    Slot3_6.Image = Properties.Resources.No_Move;
                    Slot4_1.Image = Properties.Resources.Bajar_11;
                    Slot4_2.Image = Properties.Resources.Bajar_12;
                    Slot4_3.Image = Properties.Resources.Bajar_13;
                    Slot4_4.Image = Properties.Resources.Bajar_14;
                    Slot4_5.Image = Properties.Resources.Bajar_15;
                    Slot4_6.Image = Properties.Resources.No_Move;
                    break;
                case 19:
                    Slot1_1.Image = Properties.Resources.Mew_1;
                    Slot1_2.Image = Properties.Resources.Mew_2;
                    Slot1_3.Image = Properties.Resources.Mew_3;
                    Slot1_4.Image = Properties.Resources.No_Move;
                    Slot1_5.Image = Properties.Resources.No_Move;
                    Slot1_6.Image = Properties.Resources.No_Move;
                    Slot2_1.Image = Properties.Resources.Mew_4;
                    Slot2_2.Image = Properties.Resources.Mew_5;
                    Slot2_3.Image = Properties.Resources.Mew_6;
                    Slot2_4.Image = Properties.Resources.Mew_7;
                    Slot2_5.Image = Properties.Resources.No_Move;
                    Slot2_6.Image = Properties.Resources.No_Move;
                    Slot3_1.Image = Properties.Resources.Mew_8;
                    Slot3_2.Image = Properties.Resources.Mew_9;
                    Slot3_3.Image = Properties.Resources.Mew_10;
                    Slot3_4.Image = Properties.Resources.Mew_11;
                    Slot3_5.Image = Properties.Resources.Mew_12;
                    Slot3_6.Image = Properties.Resources.Mew_13;
                    Slot4_1.Image = Properties.Resources.Mew_14;
                    Slot4_2.Image = Properties.Resources.Mew_15;
                    Slot4_3.Image = Properties.Resources.Mew_16;
                    Slot4_4.Image = Properties.Resources.Mew_17;
                    Slot4_5.Image = Properties.Resources.Mew_18;
                    Slot4_6.Image = Properties.Resources.No_Move;
                    break;
                case 20:
                    Slot1_1.Image = Properties.Resources.Phoen_1;
                    Slot1_2.Image = Properties.Resources.No_Move;
                    Slot1_3.Image = Properties.Resources.No_Move;
                    Slot1_4.Image = Properties.Resources.No_Move;
                    Slot1_5.Image = Properties.Resources.No_Move;
                    Slot1_6.Image = Properties.Resources.No_Move;
                    Slot2_1.Image = Properties.Resources.Phoen_2;
                    Slot2_2.Image = Properties.Resources.Phoen_3;
                    Slot2_3.Image = Properties.Resources.No_Move;
                    Slot2_4.Image = Properties.Resources.No_Move;
                    Slot2_5.Image = Properties.Resources.No_Move;
                    Slot2_6.Image = Properties.Resources.No_Move;
                    Slot3_1.Image = Properties.Resources.Phoen_4;
                    Slot3_2.Image = Properties.Resources.Phoen_5;
                    Slot3_3.Image = Properties.Resources.Phoen_6;
                    Slot3_4.Image = Properties.Resources.Phoen_7;
                    Slot3_5.Image = Properties.Resources.No_Move;
                    Slot3_6.Image = Properties.Resources.No_Move;
                    Slot4_1.Image = Properties.Resources.Phoen_8;
                    Slot4_2.Image = Properties.Resources.Phoen_9;
                    Slot4_3.Image = Properties.Resources.Phoen_10;
                    Slot4_4.Image = Properties.Resources.No_Move;
                    Slot4_5.Image = Properties.Resources.No_Move;
                    Slot4_6.Image = Properties.Resources.No_Move;
                    break;
                case 21:
                    Slot1_1.Image = Properties.Resources.Ghost_1;
                    Slot1_2.Image = Properties.Resources.Ghost_2;
                    Slot1_3.Image = Properties.Resources.Ghost_3;
                    Slot1_4.Image = Properties.Resources.Ghost_4;
                    Slot1_5.Image = Properties.Resources.No_Move;
                    Slot1_6.Image = Properties.Resources.No_Move;
                    Slot2_1.Image = Properties.Resources.Ghost_5;
                    Slot2_2.Image = Properties.Resources.Ghost_6;
                    Slot2_3.Image = Properties.Resources.Ghost_7;
                    Slot2_4.Image = Properties.Resources.No_Move;
                    Slot2_5.Image = Properties.Resources.No_Move;
                    Slot2_6.Image = Properties.Resources.No_Move;
                    Slot3_1.Image = Properties.Resources.Ghost_8;
                    Slot3_2.Image = Properties.Resources.Ghost_9;
                    Slot3_3.Image = Properties.Resources.Ghost_10;
                    Slot3_4.Image = Properties.Resources.No_Move;
                    Slot3_5.Image = Properties.Resources.No_Move;
                    Slot3_6.Image = Properties.Resources.No_Move;
                    if(rng.Next(100) > 2)
                        Slot4_1.Image = Properties.Resources.Ghost_11;
                    else
                        Slot4_1.Image = Properties.Resources.Ghost_11a;
                    Slot4_2.Image = Properties.Resources.Ghost_12;
                    Slot4_3.Image = Properties.Resources.No_Move;
                    Slot4_4.Image = Properties.Resources.No_Move;
                    Slot4_5.Image = Properties.Resources.No_Move;
                    Slot4_6.Image = Properties.Resources.No_Move;
                    break;
                case 22:
                    Slot1_1.Image = Properties.Resources.Metal_1;
                    Slot1_2.Image = Properties.Resources.Metal_2;
                    Slot1_3.Image = Properties.Resources.No_Move;
                    Slot1_4.Image = Properties.Resources.No_Move;
                    Slot1_5.Image = Properties.Resources.No_Move;
                    Slot1_6.Image = Properties.Resources.No_Move;
                    Slot2_1.Image = Properties.Resources.Metal_3;
                    Slot2_2.Image = Properties.Resources.Metal_4;
                    Slot2_3.Image = Properties.Resources.Metal_5;
                    Slot2_4.Image = Properties.Resources.Metal_6;
                    Slot2_5.Image = Properties.Resources.No_Move;
                    Slot2_6.Image = Properties.Resources.No_Move;
                    Slot3_1.Image = Properties.Resources.Metal_7;
                    Slot3_2.Image = Properties.Resources.Metal_8;
                    Slot3_3.Image = Properties.Resources.Metal_9;
                    Slot3_4.Image = Properties.Resources.No_Move;
                    Slot3_5.Image = Properties.Resources.No_Move;
                    Slot3_6.Image = Properties.Resources.No_Move;
                    Slot4_1.Image = Properties.Resources.Metal_10;
                    Slot4_2.Image = Properties.Resources.Metal_11;
                    Slot4_3.Image = Properties.Resources.Metal_12;
                    Slot4_4.Image = Properties.Resources.Metal_13;
                    Slot4_5.Image = Properties.Resources.No_Move;
                    Slot4_6.Image = Properties.Resources.No_Move;
                    break;
                case 23:
                    Slot1_1.Image = Properties.Resources.Suezo_1;
                    Slot1_2.Image = Properties.Resources.Suezo_2;
                    Slot1_3.Image = Properties.Resources.Suezo_3;
                    Slot1_4.Image = Properties.Resources.No_Move;
                    Slot1_5.Image = Properties.Resources.No_Move;
                    Slot1_6.Image = Properties.Resources.No_Move;
                    Slot2_1.Image = Properties.Resources.Suezo_4;
                    Slot2_2.Image = Properties.Resources.Suezo_5;
                    Slot2_3.Image = Properties.Resources.Suezo_6;
                    Slot2_4.Image = Properties.Resources.Suezo_7;
                    Slot2_5.Image = Properties.Resources.No_Move;
                    Slot2_6.Image = Properties.Resources.No_Move;
                    Slot3_1.Image = Properties.Resources.Suezo_8;
                    Slot3_2.Image = Properties.Resources.Suezo_9;
                    Slot3_3.Image = Properties.Resources.Suezo_10;
                    Slot3_4.Image = Properties.Resources.No_Move;
                    Slot3_5.Image = Properties.Resources.No_Move;
                    Slot3_6.Image = Properties.Resources.No_Move;
                    Slot4_1.Image = Properties.Resources.Suezo_11;
                    Slot4_2.Image = Properties.Resources.Suezo_12;
                    Slot4_3.Image = Properties.Resources.No_Move;
                    Slot4_4.Image = Properties.Resources.No_Move;
                    Slot4_5.Image = Properties.Resources.No_Move;
                    Slot4_6.Image = Properties.Resources.No_Move;
                    break;
                case 24:
                    Slot1_1.Image = Properties.Resources.Jill_1;
                    Slot1_2.Image = Properties.Resources.Jill_2;
                    Slot1_3.Image = Properties.Resources.Jill_3;
                    Slot1_4.Image = Properties.Resources.No_Move;
                    Slot1_5.Image = Properties.Resources.No_Move;
                    Slot1_6.Image = Properties.Resources.No_Move;
                    Slot2_1.Image = Properties.Resources.Jill_4;
                    Slot2_2.Image = Properties.Resources.Jill_5;
                    Slot2_3.Image = Properties.Resources.Jill_6;
                    Slot2_4.Image = Properties.Resources.No_Move;
                    Slot2_5.Image = Properties.Resources.No_Move;
                    Slot2_6.Image = Properties.Resources.No_Move;
                    Slot3_1.Image = Properties.Resources.Jill_7;
                    Slot3_2.Image = Properties.Resources.Jill_8;
                    Slot3_3.Image = Properties.Resources.Jill_9;
                    Slot3_4.Image = Properties.Resources.Jill_10;
                    Slot3_5.Image = Properties.Resources.No_Move;
                    Slot3_6.Image = Properties.Resources.No_Move;
                    Slot4_1.Image = Properties.Resources.Jill_11;
                    Slot4_2.Image = Properties.Resources.Jill_12;
                    Slot4_3.Image = Properties.Resources.No_Move;
                    Slot4_4.Image = Properties.Resources.No_Move;
                    Slot4_5.Image = Properties.Resources.No_Move;
                    Slot4_6.Image = Properties.Resources.No_Move;
                    break;
                case 25:
                    Slot1_1.Image = Properties.Resources.Mocch_1;
                    Slot1_2.Image = Properties.Resources.Mocch_2;
                    Slot1_3.Image = Properties.Resources.Mocch_3;
                    Slot1_4.Image = Properties.Resources.Mocch_4;
                    Slot1_5.Image = Properties.Resources.No_Move;
                    Slot1_6.Image = Properties.Resources.No_Move;
                    Slot2_1.Image = Properties.Resources.Mocch_5;
                    Slot2_2.Image = Properties.Resources.Mocch_6;
                    Slot2_3.Image = Properties.Resources.Mocch_7;
                    Slot2_4.Image = Properties.Resources.Mocch_8;
                    Slot2_5.Image = Properties.Resources.Mocch_9;
                    Slot2_6.Image = Properties.Resources.No_Move;
                    Slot3_1.Image = Properties.Resources.Mocch_10;
                    Slot3_2.Image = Properties.Resources.Mocch_11;
                    Slot3_3.Image = Properties.Resources.Mocch_12;
                    Slot3_4.Image = Properties.Resources.Mocch_13;
                    Slot3_5.Image = Properties.Resources.Mocch_14;
                    Slot3_6.Image = Properties.Resources.No_Move;
                    Slot4_1.Image = Properties.Resources.Mocch_15;
                    Slot4_2.Image = Properties.Resources.Mocch_16;
                    Slot4_3.Image = Properties.Resources.Mocch_17;
                    Slot4_4.Image = Properties.Resources.Mocch_18;
                    Slot4_5.Image = Properties.Resources.Mocch_19;
                    Slot4_6.Image = Properties.Resources.Mocch_20;
                    break;
                case 26:
                    Slot1_1.Image = Properties.Resources.Joker_1;
                    Slot1_2.Image = Properties.Resources.No_Move;
                    Slot1_3.Image = Properties.Resources.No_Move;
                    Slot1_4.Image = Properties.Resources.No_Move;
                    Slot1_5.Image = Properties.Resources.No_Move;
                    Slot1_6.Image = Properties.Resources.No_Move;
                    Slot2_1.Image = Properties.Resources.Joker_2;
                    Slot2_2.Image = Properties.Resources.Joker_3;
                    Slot2_3.Image = Properties.Resources.No_Move;
                    Slot2_4.Image = Properties.Resources.No_Move;
                    Slot2_5.Image = Properties.Resources.No_Move;
                    Slot2_6.Image = Properties.Resources.No_Move;
                    Slot3_1.Image = Properties.Resources.Joker_4;
                    Slot3_2.Image = Properties.Resources.No_Move;
                    Slot3_3.Image = Properties.Resources.No_Move;
                    Slot3_4.Image = Properties.Resources.No_Move;
                    Slot3_5.Image = Properties.Resources.No_Move;
                    Slot3_6.Image = Properties.Resources.No_Move;
                    Slot4_1.Image = Properties.Resources.Joker_5;
                    Slot4_2.Image = Properties.Resources.Joker_6;
                    Slot4_3.Image = Properties.Resources.No_Move;
                    Slot4_4.Image = Properties.Resources.No_Move;
                    Slot4_5.Image = Properties.Resources.No_Move;
                    Slot4_6.Image = Properties.Resources.No_Move;
                    break;
                case 27:
                    Slot1_1.Image = Properties.Resources.Gaboo_1;
                    Slot1_2.Image = Properties.Resources.Gaboo_2;
                    Slot1_3.Image = Properties.Resources.Gaboo_3;
                    Slot1_4.Image = Properties.Resources.Gaboo_4;
                    Slot1_5.Image = Properties.Resources.Gaboo_5;
                    Slot1_6.Image = Properties.Resources.No_Move;
                    Slot2_1.Image = Properties.Resources.Gaboo_6;
                    Slot2_2.Image = Properties.Resources.Gaboo_7;
                    Slot2_3.Image = Properties.Resources.Gaboo_8;
                    Slot2_4.Image = Properties.Resources.Gaboo_9;
                    Slot2_5.Image = Properties.Resources.Gaboo_10;
                    Slot2_6.Image = Properties.Resources.Gaboo_11;
                    Slot3_1.Image = Properties.Resources.Gaboo_12;
                    Slot3_2.Image = Properties.Resources.Gaboo_13;
                    Slot3_3.Image = Properties.Resources.Gaboo_14;
                    Slot3_4.Image = Properties.Resources.No_Move;
                    Slot3_5.Image = Properties.Resources.No_Move;
                    Slot3_6.Image = Properties.Resources.No_Move;
                    Slot4_1.Image = Properties.Resources.Gaboo_15;
                    Slot4_2.Image = Properties.Resources.Gaboo_16;
                    Slot4_3.Image = Properties.Resources.Gaboo_17;
                    Slot4_4.Image = Properties.Resources.No_Move;
                    Slot4_5.Image = Properties.Resources.No_Move;
                    Slot4_6.Image = Properties.Resources.No_Move;
                    break;
                case 28:
                    Slot1_1.Image = Properties.Resources.Jell_1;
                    Slot1_2.Image = Properties.Resources.Jell_2;
                    Slot1_3.Image = Properties.Resources.Jell_3;
                    Slot1_4.Image = Properties.Resources.Jell_4;
                    Slot1_5.Image = Properties.Resources.No_Move;
                    Slot1_6.Image = Properties.Resources.No_Move;
                    Slot2_1.Image = Properties.Resources.Jell_5;
                    Slot2_2.Image = Properties.Resources.Jell_6;
                    Slot2_3.Image = Properties.Resources.Jell_7;
                    Slot2_4.Image = Properties.Resources.Jell_8;
                    Slot2_5.Image = Properties.Resources.Jell_9;
                    Slot2_6.Image = Properties.Resources.No_Move;
                    Slot3_1.Image = Properties.Resources.Jell_10;
                    Slot3_2.Image = Properties.Resources.Jell_11;
                    Slot3_3.Image = Properties.Resources.Jell_12;
                    Slot3_4.Image = Properties.Resources.Jell_13;
                    Slot3_5.Image = Properties.Resources.Jell_14;
                    Slot3_6.Image = Properties.Resources.Jell_15;
                    Slot4_1.Image = Properties.Resources.Jell_16;
                    Slot4_2.Image = Properties.Resources.Jell_17;
                    Slot4_3.Image = Properties.Resources.Jell_18;
                    Slot4_4.Image = Properties.Resources.Jell_19;
                    Slot4_5.Image = Properties.Resources.Jell_20;
                    Slot4_6.Image = Properties.Resources.No_Move;
                    break;
                case 29:
                    Slot1_1.Image = Properties.Resources.Undin_1;
                    Slot1_2.Image = Properties.Resources.Undin_2;
                    Slot1_3.Image = Properties.Resources.Undin_3;
                    Slot1_4.Image = Properties.Resources.Undin_4;
                    Slot1_5.Image = Properties.Resources.No_Move;
                    Slot1_6.Image = Properties.Resources.No_Move;
                    Slot2_1.Image = Properties.Resources.Undin_5;
                    Slot2_2.Image = Properties.Resources.Undin_6;
                    Slot2_3.Image = Properties.Resources.Undin_7;
                    Slot2_4.Image = Properties.Resources.Undin_8;
                    Slot2_5.Image = Properties.Resources.Undin_9;
                    Slot2_6.Image = Properties.Resources.No_Move;
                    Slot3_1.Image = Properties.Resources.Undin_10;
                    Slot3_2.Image = Properties.Resources.Undin_11;
                    Slot3_3.Image = Properties.Resources.Undin_12;
                    Slot3_4.Image = Properties.Resources.Undin_13;
                    Slot3_5.Image = Properties.Resources.Undin_14;
                    Slot3_6.Image = Properties.Resources.Undin_15;
                    Slot4_1.Image = Properties.Resources.Undin_16;
                    Slot4_2.Image = Properties.Resources.Undin_17;
                    Slot4_3.Image = Properties.Resources.Undin_18;
                    Slot4_4.Image = Properties.Resources.Undin_19;
                    Slot4_5.Image = Properties.Resources.Undin_20;
                    Slot4_6.Image = Properties.Resources.Undin_21;
                    break;
                case 30:
                    Slot1_1.Image = Properties.Resources.Niton_1;
                    Slot1_2.Image = Properties.Resources.Niton_2;
                    Slot1_3.Image = Properties.Resources.Niton_3;
                    Slot1_4.Image = Properties.Resources.Niton_4;
                    Slot1_5.Image = Properties.Resources.No_Move;
                    Slot1_6.Image = Properties.Resources.No_Move;
                    Slot2_1.Image = Properties.Resources.Niton_5;
                    Slot2_2.Image = Properties.Resources.Niton_6;
                    Slot2_3.Image = Properties.Resources.Niton_7;
                    Slot2_4.Image = Properties.Resources.Niton_8;
                    Slot2_5.Image = Properties.Resources.Niton_9;
                    Slot2_6.Image = Properties.Resources.Niton_10;
                    Slot3_1.Image = Properties.Resources.Niton_11;
                    Slot3_2.Image = Properties.Resources.Niton_12;
                    Slot3_3.Image = Properties.Resources.Niton_13;
                    Slot3_4.Image = Properties.Resources.Niton_14;
                    Slot3_5.Image = Properties.Resources.No_Move;
                    Slot3_6.Image = Properties.Resources.No_Move;
                    Slot4_1.Image = Properties.Resources.Niton_15;
                    Slot4_2.Image = Properties.Resources.Niton_16;
                    Slot4_3.Image = Properties.Resources.Niton_17;
                    Slot4_4.Image = Properties.Resources.No_Move;
                    Slot4_5.Image = Properties.Resources.No_Move;
                    Slot4_6.Image = Properties.Resources.No_Move;
                    break;
                case 31:
                    Slot1_1.Image = Properties.Resources.Mock_1;
                    Slot1_2.Image = Properties.Resources.No_Move;
                    Slot1_3.Image = Properties.Resources.No_Move;
                    Slot1_4.Image = Properties.Resources.No_Move;
                    Slot1_5.Image = Properties.Resources.No_Move;
                    Slot1_6.Image = Properties.Resources.No_Move;
                    Slot2_1.Image = Properties.Resources.Mock_2;
                    Slot2_2.Image = Properties.Resources.No_Move;
                    Slot2_3.Image = Properties.Resources.No_Move;
                    Slot2_4.Image = Properties.Resources.No_Move;
                    Slot2_5.Image = Properties.Resources.No_Move;
                    Slot2_6.Image = Properties.Resources.No_Move;
                    Slot3_1.Image = Properties.Resources.Mock_3;
                    Slot3_2.Image = Properties.Resources.Mock_4;
                    Slot3_3.Image = Properties.Resources.Mock_5;
                    Slot3_4.Image = Properties.Resources.Mock_6;
                    Slot3_5.Image = Properties.Resources.No_Move;
                    Slot3_6.Image = Properties.Resources.No_Move;
                    Slot4_1.Image = Properties.Resources.Mock_7;
                    Slot4_2.Image = Properties.Resources.Mock_8;
                    Slot4_3.Image = Properties.Resources.Mock_9;
                    Slot4_4.Image = Properties.Resources.No_Move;
                    Slot4_5.Image = Properties.Resources.No_Move;
                    Slot4_6.Image = Properties.Resources.No_Move;
                    break;
                case 32:
                    Slot1_1.Image = Properties.Resources.Ducke_1;
                    Slot1_2.Image = Properties.Resources.Ducke_2;
                    Slot1_3.Image = Properties.Resources.No_Move;
                    Slot1_4.Image = Properties.Resources.No_Move;
                    Slot1_5.Image = Properties.Resources.No_Move;
                    Slot1_6.Image = Properties.Resources.No_Move;
                    Slot2_1.Image = Properties.Resources.Ducke_3;
                    Slot2_2.Image = Properties.Resources.Ducke_4;
                    Slot2_3.Image = Properties.Resources.Ducke_5;
                    Slot2_4.Image = Properties.Resources.Ducke_6;
                    Slot2_5.Image = Properties.Resources.Ducke_7;
                    Slot2_6.Image = Properties.Resources.No_Move;
                    Slot3_1.Image = Properties.Resources.Ducke_8;
                    Slot3_2.Image = Properties.Resources.Ducke_9;
                    Slot3_3.Image = Properties.Resources.Ducke_10;
                    Slot3_4.Image = Properties.Resources.Ducke_11;
                    Slot3_5.Image = Properties.Resources.Ducke_12;
                    Slot3_6.Image = Properties.Resources.Ducke_13;
                    Slot4_1.Image = Properties.Resources.Ducke_14;
                    Slot4_2.Image = Properties.Resources.Ducke_15;
                    Slot4_3.Image = Properties.Resources.Ducke_16;
                    Slot4_4.Image = Properties.Resources.Ducke_17;
                    Slot4_5.Image = Properties.Resources.Ducke_18;
                    Slot4_6.Image = Properties.Resources.No_Move;
                    break;
                case 33:
                    Slot1_1.Image = Properties.Resources.Plant_1;
                    Slot1_2.Image = Properties.Resources.Plant_2;
                    Slot1_3.Image = Properties.Resources.Plant_3;
                    Slot1_4.Image = Properties.Resources.Plant_4;
                    Slot1_5.Image = Properties.Resources.No_Move;
                    Slot1_6.Image = Properties.Resources.No_Move;
                    Slot2_1.Image = Properties.Resources.Plant_5;
                    Slot2_2.Image = Properties.Resources.Plant_6;
                    Slot2_3.Image = Properties.Resources.Plant_7;
                    Slot2_4.Image = Properties.Resources.No_Move;
                    Slot2_5.Image = Properties.Resources.No_Move;
                    Slot2_6.Image = Properties.Resources.No_Move;
                    Slot3_1.Image = Properties.Resources.Plant_8;
                    Slot3_2.Image = Properties.Resources.Plant_9;
                    Slot3_3.Image = Properties.Resources.Plant_10;
                    Slot3_4.Image = Properties.Resources.No_Move;
                    Slot3_5.Image = Properties.Resources.No_Move;
                    Slot3_6.Image = Properties.Resources.No_Move;
                    Slot4_1.Image = Properties.Resources.Plant_11;
                    Slot4_2.Image = Properties.Resources.Plant_12;
                    Slot4_3.Image = Properties.Resources.No_Move;
                    Slot4_4.Image = Properties.Resources.No_Move;
                    Slot4_5.Image = Properties.Resources.No_Move;
                    Slot4_6.Image = Properties.Resources.No_Move;
                    break;
                case 34:
                    Slot1_1.Image = Properties.Resources.Monol_1;
                    Slot1_2.Image = Properties.Resources.Monol_2;
                    Slot1_3.Image = Properties.Resources.Monol_3;
                    Slot1_4.Image = Properties.Resources.Monol_4;
                    Slot1_5.Image = Properties.Resources.Monol_5;
                    Slot1_6.Image = Properties.Resources.Monol_6;
                    Slot2_1.Image = Properties.Resources.Monol_7;
                    Slot2_2.Image = Properties.Resources.Monol_8;
                    Slot2_3.Image = Properties.Resources.Monol_9;
                    Slot2_4.Image = Properties.Resources.Monol_10;
                    Slot2_5.Image = Properties.Resources.Monol_11;
                    Slot2_6.Image = Properties.Resources.Monol_12;
                    Slot3_1.Image = Properties.Resources.Monol_13;
                    Slot3_2.Image = Properties.Resources.Monol_14;
                    Slot3_3.Image = Properties.Resources.Monol_15;
                    Slot3_4.Image = Properties.Resources.No_Move;
                    Slot3_5.Image = Properties.Resources.No_Move;
                    Slot3_6.Image = Properties.Resources.No_Move;
                    Slot4_1.Image = Properties.Resources.Monol_16;
                    Slot4_2.Image = Properties.Resources.Monol_17;
                    Slot4_3.Image = Properties.Resources.Monol_18;
                    Slot4_4.Image = Properties.Resources.Monol_19;
                    Slot4_5.Image = Properties.Resources.Monol_20;
                    Slot4_6.Image = Properties.Resources.Monol_21;
                    break;
                case 35:
                    Slot1_1.Image = Properties.Resources.Ape_1;
                    Slot1_2.Image = Properties.Resources.Ape_2;
                    Slot1_3.Image = Properties.Resources.Ape_3;
                    Slot1_4.Image = Properties.Resources.No_Move;
                    Slot1_5.Image = Properties.Resources.No_Move;
                    Slot1_6.Image = Properties.Resources.No_Move;
                    Slot2_1.Image = Properties.Resources.Ape_4;
                    Slot2_2.Image = Properties.Resources.Ape_5;
                    Slot2_3.Image = Properties.Resources.No_Move;
                    Slot2_4.Image = Properties.Resources.No_Move;
                    Slot2_5.Image = Properties.Resources.No_Move;
                    Slot2_6.Image = Properties.Resources.No_Move;
                    Slot3_1.Image = Properties.Resources.Ape_6;
                    Slot3_2.Image = Properties.Resources.Ape_7;
                    Slot3_3.Image = Properties.Resources.No_Move;
                    Slot3_4.Image = Properties.Resources.No_Move;
                    Slot3_5.Image = Properties.Resources.No_Move;
                    Slot3_6.Image = Properties.Resources.No_Move;
                    Slot4_1.Image = Properties.Resources.Ape_8;
                    Slot4_2.Image = Properties.Resources.Ape_9;
                    Slot4_3.Image = Properties.Resources.Ape_10;
                    Slot4_4.Image = Properties.Resources.Ape_11;
                    Slot4_5.Image = Properties.Resources.Ape_12;
                    Slot4_6.Image = Properties.Resources.No_Move;
                    break;
                case 36:
                    Slot1_1.Image = Properties.Resources.Worm_1;
                    Slot1_2.Image = Properties.Resources.Worm_2;
                    Slot1_3.Image = Properties.Resources.Worm_3;
                    Slot1_4.Image = Properties.Resources.No_Move;
                    Slot1_5.Image = Properties.Resources.No_Move;
                    Slot1_6.Image = Properties.Resources.No_Move;
                    Slot2_1.Image = Properties.Resources.Worm_4;
                    Slot2_2.Image = Properties.Resources.Worm_5;
                    Slot2_3.Image = Properties.Resources.Worm_6;
                    Slot2_4.Image = Properties.Resources.Worm_7;
                    Slot2_5.Image = Properties.Resources.Worm_8;
                    Slot2_6.Image = Properties.Resources.Worm_9;
                    Slot3_1.Image = Properties.Resources.Worm_10;
                    Slot3_2.Image = Properties.Resources.Worm_11;
                    Slot3_3.Image = Properties.Resources.Worm_12;
                    Slot3_4.Image = Properties.Resources.Worm_13;
                    Slot3_5.Image = Properties.Resources.No_Move;
                    Slot3_6.Image = Properties.Resources.No_Move;
                    Slot4_1.Image = Properties.Resources.Worm_14;
                    Slot4_2.Image = Properties.Resources.Worm_15;
                    Slot4_3.Image = Properties.Resources.No_Move;
                    Slot4_4.Image = Properties.Resources.No_Move;
                    Slot4_5.Image = Properties.Resources.No_Move;
                    Slot4_6.Image = Properties.Resources.No_Move;
                    break;
                case 37:
                    Slot1_1.Image = Properties.Resources.Naga_1;
                    Slot1_2.Image = Properties.Resources.Naga_2;
                    Slot1_3.Image = Properties.Resources.No_Move;
                    Slot1_4.Image = Properties.Resources.No_Move;
                    Slot1_5.Image = Properties.Resources.No_Move;
                    Slot1_6.Image = Properties.Resources.No_Move;
                    Slot2_1.Image = Properties.Resources.Naga_3;
                    Slot2_2.Image = Properties.Resources.Naga_4;
                    Slot2_3.Image = Properties.Resources.Naga_5;
                    Slot2_4.Image = Properties.Resources.Naga_6;
                    Slot2_5.Image = Properties.Resources.No_Move;
                    Slot2_6.Image = Properties.Resources.No_Move;
                    Slot3_1.Image = Properties.Resources.Naga_7;
                    Slot3_2.Image = Properties.Resources.Naga_8;
                    Slot3_3.Image = Properties.Resources.Naga_9;
                    Slot3_4.Image = Properties.Resources.No_Move;
                    Slot3_5.Image = Properties.Resources.No_Move;
                    Slot3_6.Image = Properties.Resources.No_Move;
                    Slot4_1.Image = Properties.Resources.Naga_10;
                    Slot4_2.Image = Properties.Resources.Naga_11;
                    Slot4_3.Image = Properties.Resources.Naga_12;
                    Slot4_4.Image = Properties.Resources.No_Move;
                    Slot4_5.Image = Properties.Resources.No_Move;
                    Slot4_6.Image = Properties.Resources.No_Move;
                    break;
                default:
                    Slot1_1.Image = Properties.Resources.No_Move;
                    Slot1_2.Image = Properties.Resources.No_Move;
                    Slot1_3.Image = Properties.Resources.No_Move;
                    Slot1_4.Image = Properties.Resources.No_Move;
                    Slot1_5.Image = Properties.Resources.No_Move;
                    Slot1_6.Image = Properties.Resources.No_Move;
                    Slot2_1.Image = Properties.Resources.No_Move;
                    Slot2_2.Image = Properties.Resources.No_Move;
                    Slot2_3.Image = Properties.Resources.No_Move;
                    Slot2_4.Image = Properties.Resources.No_Move;
                    Slot2_5.Image = Properties.Resources.No_Move;
                    Slot2_6.Image = Properties.Resources.No_Move;
                    Slot3_1.Image = Properties.Resources.No_Move;
                    Slot3_2.Image = Properties.Resources.No_Move;
                    Slot3_3.Image = Properties.Resources.No_Move;
                    Slot3_4.Image = Properties.Resources.No_Move;
                    Slot3_5.Image = Properties.Resources.No_Move;
                    Slot3_6.Image = Properties.Resources.No_Move;
                    Slot4_1.Image = Properties.Resources.No_Move;
                    Slot4_2.Image = Properties.Resources.No_Move;
                    Slot4_3.Image = Properties.Resources.No_Move;
                    Slot4_4.Image = Properties.Resources.No_Move;
                    Slot4_5.Image = Properties.Resources.No_Move;
                    Slot4_6.Image = Properties.Resources.No_Move;
                    break;
            }
        }
                
        private void GenerateMoveInfo()
        {
            int SHBoxLoc;
            PlaceCheckmark(); //bedeg


            if (CanUnlock.Checked == true && MoveUnlocked.Checked == false)  //bedeg                                                                       //just do this I guess...
            {
                canGet.Image = Properties.Resources.skillget;
                canGet.Visible = true;
            }
            else
            {
                canGet.Image = null;
                canGet.Visible = false;
            }

            if (MoveSelected == -1)
            {
                hilightSelect.Image = null;
                hilightSelect.Visible = false;
            }

            switch (Mon_Genus)
            {
                case -1:
                    MoveName.Text = "No Move"; MoveName.ForeColor = Color.Black;
                    MoveGuts.Text = "---";
                    MoveDamage.Text = "---";
                    MoveHit.Text = "---";
                    MoveGD.Text = "---";
                    MoveSharp.Text = "---";
                    MoveSH.Hide();
                    SHLabel.Hide();
                    MoveUses.Text = "---";
                    MoveUnlocked.Checked = false;
                    CanUnlock.Checked = false;
                    MoveInfo.Text = "Click on a move to learn more.";
                    break;
                case 0: // Pixie
                    switch (MoveSelected)
                    {
                        case 1:
                            MoveName.Text = "Pat";
                            LazyDataFill(false, 12, 6, 10, 0, 0, 0);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = true;
                            MoveInfo.Text = "Starting basic tech. Chains into Slap when used 30 times.";
                            break;
                        case 2:
                            MoveName.Text = "Slap"; MoveName.ForeColor = PowCol;
                            LazyDataFill(false, 18, 15, 9, 5, 0, 1);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_MoveUsed[0] >= 30);
                            MoveInfo.Text = "Basic tech. Chains from 30 Slaps. No stat requirement. Learning priority over High Kick.";
                            break;
                        case 3:
                            MoveName.Text = "Kick";
                            LazyDataFill(false, 10, 7, 4, 0, 0, 3);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = true;
                            MoveInfo.Text = "Starting basic tech. Chains into High Kick when used 30x.";
                            break;
                        case 4:
                            MoveName.Text = "High Kick";
                            LazyDataFill(false, 19, 15, 3, 5, 5, 4);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_MoveUsed[3] >= 30);
                            MoveInfo.Text = "Basic tech. Chains from 30 Kicks. Chains into Heel Raid when used 50x. No stat requirement.";
                            break;
                        case 5:
                            MoveName.Text = "Heel Raid";
                            LazyDataFill(false, 30, 28, -10, 20, 5, 5);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_MoveUsed[4] >= 50);
                            MoveInfo.Text = "Heavy tech. Chains from 50 High Kicks. No stat requirement.";
                            break;
                        case 6:
                            MoveName.Text = "Bang";
                            LazyDataFill(true, 42, 34, -11, 16, 15, 7);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_Stats[2] + Mon_Stats[4] > 800);
                            MoveInfo.Text = "Special tech. INT + SPD should total over 800 to unlock.\r\nChains into Big Bang when used 50x.";
                            break;
                        case 7:
                            MoveName.Text = "Big Bang";
                            LazyDataFill(true, 46, 41, -17, 23, 22, 8);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_MoveUsed[7] >= 50);
                            MoveInfo.Text = "Special tech. Chains from 50 Bangs. No stat requirement.";
                            break;
                        case 8:
                            MoveName.Text = "1-2 Punch";
                            LazyDataFill(false, 15, 12, 15, 0, 5, 9);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = false;//(Mon_SubGenus == 13);
                            MoveInfo.Text = @"Basic tech. Unique to Lepus. No stat requirement.
Unobtainable in Vanilla MR2.";
                            break;
                        case 9:
                            MoveName.Text = "Phantom Claw";
                            LazyDataFill(false, 25, 15, 15, 5, 5, 10);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_Stats[1] + Mon_Stats[3] >= 300 && Mon_SubGenus == 16);
                            MoveInfo.Text = "Hit tech. Unique to Kitten. POW + SKI should total over 300 to unlock.";
                            break;
                        case 10:
                            MoveName.Text = "Death Final";
                            LazyDataFill(false, 52, 44, -18, 44, 5, 11);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_Stats[1] + Mon_Stats[2] >= 1000 && Mon_SubGenus == 26);
                            MoveInfo.Text = "Special tech. Unique to Lilim. POW + INT should total over 1000 to unlock.";
                            break;
                        case 11:
                            MoveName.Text = "Bolt";
                            LazyDataFill(true, 16, 9, 13, 5, 3, 12);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_Stats[2] + Mon_Stats[3] >= 300);
                            MoveInfo.Text = "Hit tech. INT + SKI should total over 300 to unlock. Chains into Lightning when used 50x.";
                            break;
                        case 12:
                            MoveName.Text = "Lightning"; MoveName.ForeColor = IntCol;
                            MoveGuts.Text = "25";
                            MoveDamage.Text = GenerateStatValue(0, 16);
                            MoveHit.Text = GenerateStatValue(1, 12);
                            MoveGD.Text = GenerateStatValue(2, 5);
                            MoveSharp.Text = GenerateStatValue(3, 5);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[13].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[13] == 1);
                            CanUnlock.Checked = (Mon_MoveUsed[12] >= 50);
                            PlaceHighlight(13);
                            MoveInfo.Text = "Hit tech. Chains from 50 Bolts. No stat requirement.";
                            break;
                        case 13:
                            MoveName.Text = "Kiss"; MoveName.ForeColor = IntCol;
                            MoveGuts.Text = "18";
                            MoveDamage.Text = "---";
                            MoveHit.Text = GenerateStatValue(1, 2);
                            MoveGD.Text = GenerateStatValue(2, 25);
                            MoveSharp.Text = GenerateStatValue(3, 8);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[14].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[14] == 1);
                            CanUnlock.Checked = (Mon_Stats[2] >= 250);
                            PlaceHighlight(14);
                            MoveInfo.Text = "Withering tech. INT should be over 250 to unlock.";
                            break;
                        case 14:
                            MoveName.Text = "Life Steal"; MoveName.ForeColor = IntCol;
                            MoveGuts.Text = "40";
                            MoveDamage.Text = GenerateStatValue(0, 20);
                            MoveHit.Text = GenerateStatValue(1, -10);
                            MoveGD.Text = "---";
                            MoveSharp.Text = "---";
                            MoveSH.Show();
                            MoveSH.Text = "100%";
                            SHLabel.Show();
                            SHLabel.Text = "HP Drain:";
                            MoveUses.Text = Mon_MoveUsed[15].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[15] == 1);
                            CanUnlock.Checked = (Mon_Stats[2] >= 600 && Mon_Nature <= -50); //bedeg
                            PlaceHighlight(15);
                            MoveInfo.Text = "Special tech. INT should be over 600 to unlock. Requires Bad (-50) Nature.";
                            break;
                        case 15:
                            MoveName.Text = "Refreshment"; MoveName.ForeColor = IntCol;
                            MoveGuts.Text = "50";
                            MoveDamage.Text = "---";
                            MoveHit.Text = GenerateStatValue(1, -20);
                            MoveGD.Text = "---";
                            MoveSharp.Text = "---";
                            MoveSH.Show();
                            MoveSH.Text = GenerateStatValue(0, 30);
                            SHLabel.Show();
                            SHLabel.Text = "Recovery:";
                            MoveUses.Text = Mon_MoveUsed[16].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[16] == 1);
                            CanUnlock.Checked = (Mon_Stats[2] >= 600 && Mon_Nature >= 50); //bedeg
                            PlaceHighlight(16);
                            MoveInfo.Text = "Special tech. INT should be over 600 to unlock. Requires Good (+50) Nature.";
                            break;
                        case 16:
                            MoveName.Text = "Fire Breath"; MoveName.ForeColor = IntCol;
                            MoveGuts.Text = "49";
                            MoveDamage.Text = GenerateStatValue(0, 45);
                            MoveHit.Text = GenerateStatValue(1, -14);
                            MoveGD.Text = GenerateStatValue(2, 19);
                            MoveSharp.Text = GenerateStatValue(3, 8);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[17].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[17] == 1);
                            CanUnlock.Checked = (Mon_Stats[1] + Mon_Stats[2] >= 1000);
                            PlaceHighlight(17);
                            MoveInfo.Text = "Special tech. Unique to Daina. POW + INT should total over 1000 to unlock.";
                            break;
                        case 17:
                            MoveName.Text = "Flame"; MoveName.ForeColor = IntCol;
                            MoveGuts.Text = "19";
                            MoveDamage.Text = GenerateStatValue(0, 24);
                            MoveHit.Text = GenerateStatValue(1, -15);
                            MoveGD.Text = GenerateStatValue(2, 11);
                            MoveSharp.Text = GenerateStatValue(3, 5);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[18].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[18] == 1);
                            CanUnlock.Checked = (Mon_Stats[1] + Mon_Stats[2] >= 400 && Mon_SubGenus != 24); //bedeg
                            PlaceHighlight(18);
                            MoveInfo.Text = @"Heavy tech. Unlearnable by Snowy. POW + INT should total over 400 to unlock.
Chains into Gigaflame when used 50x.";
                            break;
                        case 18:
                            MoveName.Text = "Gigaflame"; MoveName.ForeColor = IntCol;
                            MoveGuts.Text = "30";
                            MoveDamage.Text = GenerateStatValue(0, 39);
                            MoveHit.Text = GenerateStatValue(1, -19);
                            MoveGD.Text = GenerateStatValue(2, 15);
                            MoveSharp.Text = GenerateStatValue(3, 5);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[20].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[20] == 1);
                            CanUnlock.Checked = (Mon_SubGenus != 24 && Mon_MoveUsed[18] >= 50);
                            PlaceHighlight(20);
                            MoveInfo.Text = "Heavy tech. Chains from 50 Flames. Requires Bad (-20) Nature. Learning priority over Heel Raid.";
                            break;
                        case 19:
                            MoveName.Text = "Ray"; MoveName.ForeColor = IntCol;
                            MoveGuts.Text = "17";
                            MoveDamage.Text = GenerateStatValue(0, 12);
                            MoveHit.Text = GenerateStatValue(1, -3);
                            MoveGD.Text = GenerateStatValue(2, 5);
                            MoveSharp.Text = GenerateStatValue(3, 16);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[21].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[21] == 1);
                            CanUnlock.Checked = (Mon_Stats[2] + Mon_Stats[4] >= 400);
                            PlaceHighlight(21);
                            MoveInfo.Text = "Sharp tech. INT + SPD should total over 400 to unlock. Chains into Megaray when used 50x.";
                            break;
                        case 20:
                            MoveName.Text = "Megaray"; MoveName.ForeColor = IntCol;
                            MoveGuts.Text = "29";
                            MoveDamage.Text = GenerateStatValue(0, 19);
                            MoveHit.Text = GenerateStatValue(1, -4);
                            MoveGD.Text = GenerateStatValue(2, 5);
                            MoveSharp.Text = GenerateStatValue(3, 21);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[22].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[22] == 1);
                            CanUnlock.Checked = (Mon_MoveUsed[21] >= 50);
                            PlaceHighlight(22);
                            MoveInfo.Text = "Sharp tech. Chains from 50 Rays. Chains into Gigaray when used 50x. No stat requirement.";
                            break;
                        case 21:
                            MoveName.Text = "Gigaray"; MoveName.ForeColor = IntCol;
                            MoveGuts.Text = "34";
                            MoveDamage.Text = GenerateStatValue(0, 25);
                            MoveHit.Text = GenerateStatValue(1, -8);
                            MoveGD.Text = GenerateStatValue(2, 5);
                            MoveSharp.Text = GenerateStatValue(3, 32);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[23].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[23] == 1);
                            CanUnlock.Checked = (Mon_MoveUsed[22] >= 50 && Mon_Nature >= 20);
                            PlaceHighlight(23);
                            MoveInfo.Text = "Special tech. Chains from 50 Megarays. Requires Good (+20) Nature."; //bedeg
                            break;
                        default:
                            MoveName.Text = "No Move"; MoveName.ForeColor = Color.Black;
                            MoveGuts.Text = "---";
                            MoveDamage.Text = "---";
                            MoveHit.Text = "---";
                            MoveGD.Text = "---";
                            MoveSharp.Text = "---";
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = "---";
                            MoveUnlocked.Checked = false;
                            CanUnlock.Checked = false;
                            MoveInfo.Text = "Click on a move to learn more.";
                            break;
                    }
                    break;
                case 1: // Dragon
                    switch (MoveSelected)
                    {
                        case 1:
                            MoveName.Text = "Tail Whip"; MoveName.ForeColor = PowCol;
                            MoveGuts.Text = "11";
                            MoveGD.Text = "---";
                            MoveSharp.Text = "---";
                            MoveDamage.Text = GenerateStatValue(0, 17);
                            MoveHit.Text = GenerateStatValue(1, 5);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[0].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[0] == 1);
                            CanUnlock.Checked = true;
                            PlaceHighlight(0);
                            MoveInfo.Text = "Starting basic tech. Chains into Tail Attack when used 30 times.";
                            break;
                        case 2:
                            MoveName.Text = "Tail Attack"; MoveName.ForeColor = PowCol;
                            MoveGuts.Text = "16";
                            MoveDamage.Text = GenerateStatValue(0, 23);
                            MoveHit.Text = GenerateStatValue(1, 5);
                            MoveGD.Text = "---";
                            MoveSharp.Text = "---";
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[1].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[1] == 1);
                            CanUnlock.Checked = (Mon_MoveUsed[0] >= 30);
                            PlaceHighlight(1);
                            MoveInfo.Text = "Basic tech. Chains from 30 Tail Whips. No stat requirement. Learning priority over Two Bites.";
                            break;
                        case 3:
                            MoveName.Text = "Bite"; MoveName.ForeColor = PowCol;
                            MoveGuts.Text = "13";
                            MoveDamage.Text = GenerateStatValue(0, 13);
                            MoveHit.Text = GenerateStatValue(1, 10);
                            MoveGD.Text = GenerateStatValue(2, 5);
                            MoveSharp.Text = "---";
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[2].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[2] == 1);
                            CanUnlock.Checked = true;
                            PlaceHighlight(2);
                            MoveInfo.Text = "Starting basic tech. Chains into Two Bites when used 30x.";
                            break;
                        case 4:
                            MoveName.Text = "Two Bites"; MoveName.ForeColor = PowCol;
                            MoveGuts.Text = "18";
                            MoveDamage.Text = GenerateStatValue(0, 18);
                            MoveHit.Text = GenerateStatValue(1, 7);
                            MoveGD.Text = GenerateStatValue(2, 8);
                            MoveSharp.Text = GenerateStatValue(3, 5);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[3].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[3] == 1);
                            CanUnlock.Checked = (Mon_MoveUsed[2] >= 30);
                            PlaceHighlight(3);
                            MoveInfo.Text = "Basic tech. Chains from 30 Bites. No stat requirement.";
                            break;
                        case 5:
                            MoveName.Text = "Dragon Punch"; MoveName.ForeColor = PowCol;
                            MoveGuts.Text = "17";
                            MoveDamage.Text = GenerateStatValue(0, 18);
                            MoveHit.Text = GenerateStatValue(1, -1);
                            MoveGD.Text = GenerateStatValue(2, 7);
                            MoveSharp.Text = GenerateStatValue(3, 17);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[4].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[4] == 1);
                            CanUnlock.Checked = (Mon_Stats[1] + Mon_Stats[4] >= 400);
                            PlaceHighlight(4);
                            MoveInfo.Text = "Sharp tech. POW + SPD should total over 400 to unlock.";
                            break;
                        case 6:
                            MoveName.Text = "Wing Attack"; MoveName.ForeColor = PowCol;
                            MoveGuts.Text = "17";
                            MoveDamage.Text = GenerateStatValue(0, 15);
                            MoveHit.Text = GenerateStatValue(1, 14);
                            MoveGD.Text = GenerateStatValue(2, 5);
                            MoveSharp.Text = GenerateStatValue(3, 5);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[6].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[6] == 1);
                            CanUnlock.Checked = (Mon_Stats[1] + Mon_Stats[3] >= 400);
                            PlaceHighlight(6);
                            MoveInfo.Text = "Hit tech. Pow + SKI should total over 400 to unlock. Chains into Wing Combo when used 50x.";
                            break;
                        case 7:
                            MoveName.Text = "Wing Combo"; MoveName.ForeColor = PowCol;
                            MoveGuts.Text = "22";
                            MoveDamage.Text = GenerateStatValue(0, 22);
                            MoveHit.Text = GenerateStatValue(1, 14);
                            MoveGD.Text = GenerateStatValue(2, 6);
                            MoveSharp.Text = GenerateStatValue(3, 5);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[7].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[7] == 1);
                            CanUnlock.Checked = (Mon_MoveUsed[6] >= 50);
                            PlaceHighlight(7);
                            MoveInfo.Text = "Hit tech. Chains from 50 Wing Attacks. No stat requirement. Learning priority over Flutters.";
                            break;
                        case 8:
                            MoveName.Text = "Claw Combo"; MoveName.ForeColor = PowCol;
                            MoveGuts.Text = "19"; //bedeg
                            MoveDamage.Text = GenerateStatValue(0, 30);
                            MoveHit.Text = GenerateStatValue(1, -10);
                            MoveGD.Text = GenerateStatValue(2, 9);
                            MoveSharp.Text = GenerateStatValue(3, 5);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[8].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[8] == 1);
                            CanUnlock.Checked = (Mon_Stats[1] >= 250);
                            PlaceHighlight(8);
                            MoveInfo.Text = "Heavy tech. POW should be over 250 to unlock. Chains into Flying Combo when used 50x.";
                            break;
                        case 9:
                            MoveName.Text = "Claw"; MoveName.ForeColor = PowCol;
                            MoveGuts.Text = "24";
                            MoveDamage.Text = GenerateStatValue(0, 24);
                            MoveHit.Text = GenerateStatValue(1, -5);
                            MoveGD.Text = GenerateStatValue(2, 12);
                            MoveSharp.Text = GenerateStatValue(3, 24);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[9].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[9] == 1);
                            CanUnlock.Checked = (Mon_Stats[1] + Mon_Stats[4] >= 650 && Mon_Nature <= -20); //bedeg
                            PlaceHighlight(9);
                            MoveInfo.Text = @"Sharp tech. POW + SPD should total over 650 to unlock. Requires Bad (-20) Nature. 
Chains into Spinning Claw when used 50x.";
                            break;
                        case 10:
                            MoveName.Text = "Spinning Claw"; MoveName.ForeColor = PowCol;
                            MoveGuts.Text = "30";
                            MoveDamage.Text = GenerateStatValue(0, 32);
                            MoveHit.Text = GenerateStatValue(1, -7);
                            MoveGD.Text = GenerateStatValue(2, 12);
                            MoveSharp.Text = GenerateStatValue(3, 24);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[10].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[10] == 1);
                            CanUnlock.Checked = (Mon_MoveUsed[9] >= 50 && Mon_Nature <= -50); //bedeg
                            PlaceHighlight(10);
                            MoveInfo.Text = "Sharp tech. Chains from 50 Claws. Requires Bad (-50) Nature.";
                            break;
                        case 11:
                            MoveName.Text = "Flutter"; MoveName.ForeColor = IntCol;
                            MoveGuts.Text = "29";
                            MoveDamage.Text = GenerateStatValue(0, 21);
                            MoveHit.Text = GenerateStatValue(1, 19);
                            MoveGD.Text = GenerateStatValue(2, 15);
                            MoveSharp.Text = GenerateStatValue(3, 5);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[12].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[12] == 1);
                            CanUnlock.Checked = (Mon_Stats[2] + Mon_Stats[3] >= 650);
                            PlaceHighlight(12);
                            MoveInfo.Text = "Hit tech. INT + SKI should total over 650 to unlock. Chains into Flutters when used 50x.";
                            break;
                        case 12:
                            MoveName.Text = "Flutters"; MoveName.ForeColor = IntCol;
                            MoveGuts.Text = "33";
                            MoveDamage.Text = GenerateStatValue(0, 31);
                            MoveHit.Text = GenerateStatValue(1, 14);
                            MoveGD.Text = GenerateStatValue(2, 15);
                            MoveSharp.Text = GenerateStatValue(3, 5);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[13].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[13] == 1);
                            CanUnlock.Checked = (Mon_MoveUsed[12] >= 50);
                            PlaceHighlight(13);
                            MoveInfo.Text = "Hit tech. Chains from 50 Flutter uses. No stat requirement.";
                            break;
                        case 13:
                            MoveName.Text = "Trample"; MoveName.ForeColor = PowCol;
                            MoveGuts.Text = "27";
                            MoveDamage.Text = GenerateStatValue(0, 50);
                            MoveHit.Text = GenerateStatValue(1, -16);
                            MoveGD.Text = GenerateStatValue(2, 13);
                            MoveSharp.Text = GenerateStatValue(3, 5);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[14].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[14] == 1);
                            CanUnlock.Checked = (Mon_Stats[2] >= 450);
                            PlaceHighlight(14);
                            MoveInfo.Text = "Heavy tech. POW should be over 450 to unlock.";
                            break;
                        case 14:
                            MoveName.Text = "Fire Breath"; MoveName.ForeColor = IntCol;
                            MoveGuts.Text = "20";
                            MoveDamage.Text = GenerateStatValue(0, 20);
                            MoveHit.Text = GenerateStatValue(1, -8);
                            MoveGD.Text = GenerateStatValue(2, 25);
                            MoveSharp.Text = GenerateStatValue(3, 5);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[15].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[15] == 1);
                            CanUnlock.Checked = (Mon_Stats[2] >= 250);
                            PlaceHighlight(15);
                            MoveInfo.Text = "Withering tech. INT should be over 250 to unlock. Chains into Inferno when used 50x.";
                            break;
                        case 15:
                            MoveName.Text = "Dragon Combo"; MoveName.ForeColor = PowCol;
                            MoveGuts.Text = "50";
                            MoveDamage.Text = GenerateStatValue(0, 65);
                            MoveHit.Text = GenerateStatValue(1, -20);
                            MoveGD.Text = GenerateStatValue(2, 35);
                            MoveSharp.Text = GenerateStatValue(3, 15);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[16].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[16] == 1);
                            CanUnlock.Checked = (Mon_Stats[1] + Mon_Stats[2] >= 1000);
                            PlaceHighlight(16);
                            MoveInfo.Text = "Special tech. POW + INT should total over 1000 to unlock."; //bedeg
                            break;
                        case 16:
                            MoveName.Text = "Inferno"; MoveName.ForeColor = IntCol;
                            MoveGuts.Text = "29";
                            MoveDamage.Text = GenerateStatValue(0, 27);
                            MoveHit.Text = GenerateStatValue(1, -8);
                            MoveGD.Text = GenerateStatValue(2, 39);
                            MoveSharp.Text = GenerateStatValue(3, 5); //bedeg
                            MoveDamage.Text = "C (27)";
                            MoveHit.Text = "D (-8)";
                            MoveGD.Text = "B (39)";
                            MoveSharp.Text = "E (5)";
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[18].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[18] == 1);
                            CanUnlock.Checked = (Mon_MoveUsed[15] >= 50 && Mon_Stats[2] >= 450);
                            PlaceHighlight(18);
                            MoveInfo.Text = "Withering tech. INT should be over 450 to unlock. Chains from 50 Fire Breaths.";
                            break;
                        case 17:
                            MoveName.Text = "Glide Charge"; MoveName.ForeColor = PowCol;
                            MoveGuts.Text = "27";
                            MoveDamage.Text = GenerateStatValue(0, 22);
                            MoveHit.Text = GenerateStatValue(1, 9);
                            MoveGD.Text = GenerateStatValue(2, 5);
                            MoveSharp.Text = GenerateStatValue(3, 27);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[19].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[19] == 1);
                            CanUnlock.Checked = (Mon_Stats[1] + Mon_Stats[4] > 650 && Mon_Nature >= 20); //bedeg
                            PlaceHighlight(19);
                            MoveInfo.Text = "Sharp tech. POW + SPD should total over 650 to unlock. Requires Good (+20) Nature.";
                            break;
                        case 18:
                            MoveName.Text = "Slamming Down"; MoveName.ForeColor = PowCol;
                            MoveGuts.Text = "40";
                            MoveDamage.Text = GenerateStatValue(0, 36);
                            MoveHit.Text = GenerateStatValue(1, 0);
                            MoveGD.Text = GenerateStatValue(2, 36);
                            MoveSharp.Text = GenerateStatValue(3, 5);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[20].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[20] == 1);
                            CanUnlock.Checked = (Mon_Stats[1] + Mon_Stats[2] >= 800);
                            PlaceHighlight(20);
                            MoveInfo.Text = @"Special tech. POW + INT should total over 800 to unlock.
(And yes, I am aware it's 'Slammimg Down.')";
                            break;
                        case 19:
                            MoveName.Text = "Flying Combo"; MoveName.ForeColor = PowCol;
                            MoveGuts.Text = "25";
                            MoveDamage.Text = GenerateStatValue(0, 46);
                            MoveHit.Text = GenerateStatValue(1, -15);
                            MoveGD.Text = GenerateStatValue(2, 10);
                            MoveSharp.Text = GenerateStatValue(3, 5);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[21].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[21] == 1);
                            CanUnlock.Checked = (Mon_Stats[1] >= 450 && Mon_MoveUsed[8] >= 50);
                            PlaceHighlight(21);
                            MoveInfo.Text = "Heavy tech. POW should be over 450 to unlock. Chains from 50 Claw Combos.";
                            break;
                        default:
                            MoveName.Text = "No Move"; MoveName.ForeColor = Color.Black;
                            MoveGuts.Text = "---";
                            MoveDamage.Text = "---";
                            MoveHit.Text = "---";
                            MoveGD.Text = "---";
                            MoveSharp.Text = "---";
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = "---";
                            MoveUnlocked.Checked = false;
                            CanUnlock.Checked = false;
                            MoveInfo.Text = "Click on a move to learn more.";
                            break;
                    }
                    break;
                case 2: // Centaur
                    switch (MoveSelected)
                    {
                        case 1:
                            MoveName.Text = "Smash"; MoveName.ForeColor = PowCol;
                            MoveGuts.Text = "13";
                            MoveDamage.Text = GenerateStatValue(0, 15);
                            MoveHit.Text = GenerateStatValue(1, 5);
                            MoveGD.Text = "---";
                            MoveSharp.Text = GenerateStatValue(3, 5);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[0].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[0] == 1);
                            CanUnlock.Checked = true;
                            PlaceHighlight(0);
                            MoveInfo.Text = "Starting basic tech. Chains into Smash Combo when used 30x.";
                            break;
                        case 2:
                            MoveName.Text = "Smash Combo"; MoveName.ForeColor = PowCol;
                            MoveGuts.Text = "20";
                            MoveDamage.Text = GenerateStatValue(0, 20);
                            MoveHit.Text = GenerateStatValue(1, 5);
                            MoveGD.Text = GenerateStatValue(2, 5);
                            MoveSharp.Text = GenerateStatValue(3, 5);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[1].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[1] == 1);
                            CanUnlock.Checked = (Mon_MoveUsed[0] >= 30); //bedeg
                            PlaceHighlight(1);
                            MoveInfo.Text = "Basic tech. Chains from 30 Smashes. No stat requirement.";
                            break;
                        case 3:
                            MoveName.Text = "Triple Stabs"; MoveName.ForeColor = PowCol;
                            MoveGuts.Text = "27";
                            MoveDamage.Text = GenerateStatValue(0, 25);
                            MoveHit.Text = GenerateStatValue(1, 15);
                            MoveGD.Text = GenerateStatValue(2, 5);
                            MoveSharp.Text = GenerateStatValue(3, 5);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[2].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[2] == 1);
                            CanUnlock.Checked = (Mon_Stats[1] + Mon_Stats[3] >= 650);
                            PlaceHighlight(2);
                            MoveInfo.Text = "Hit tech. POW + SKI should total over 650 to unlock.";
                            break;
                        case 4:
                            MoveName.Text = "Stab-Throw"; MoveName.ForeColor = PowCol;
                            MoveGuts.Text = "28";
                            MoveDamage.Text = GenerateStatValue(0, 49);
                            MoveHit.Text = GenerateStatValue(1, -15);
                            MoveGD.Text = GenerateStatValue(2, 9);
                            MoveSharp.Text = GenerateStatValue(3, 5);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[3].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[3] == 1);
                            CanUnlock.Checked = (Mon_Stats[1] >= 450);
                            PlaceHighlight(3);
                            MoveInfo.Text = "Heavy tech. POW should be over 450 to unlock. Chains into Death Thrust when used 50x.";
                            break;
                        case 5:
                            MoveName.Text = "Z Smash"; MoveName.ForeColor = PowCol;
                            MoveGuts.Text = "55";
                            MoveDamage.Text = GenerateStatValue(0, 35);
                            MoveHit.Text = GenerateStatValue(1, 5);
                            MoveGD.Text = GenerateStatValue(2, 35);
                            MoveSharp.Text = GenerateStatValue(3, 25);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[4].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[4] == 1);
                            CanUnlock.Checked = (Mon_MoveUsed[9] >= 50 && Mon_Nature >= 50);
                            PlaceHighlight(4);
                            MoveInfo.Text = "Special tech. Chains from 50 Cross Slash. Requires Good (+50) Nature.";
                            break;
                        case 6:
                            MoveName.Text = "Turn Stab"; MoveName.ForeColor = PowCol;
                            MoveGuts.Text = "18";
                            MoveDamage.Text = GenerateStatValue(0, 16);
                            MoveHit.Text = GenerateStatValue(1, 16);
                            MoveGD.Text = GenerateStatValue(2, 5);
                            MoveSharp.Text = GenerateStatValue(3, 5);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[6].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[6] == 1);
                            CanUnlock.Checked = (Mon_Stats[1] + Mon_Stats[3] >= 400);
                            PlaceHighlight(6);
                            MoveInfo.Text = "Hit tech. POW + SKI should total over 400 to unlock. Learning priority over Triple Stabs.";
                            break;
                        case 7:
                            MoveName.Text = "Mind Flare"; MoveName.ForeColor = IntCol;
                            MoveGuts.Text = "22";
                            MoveDamage.Text = GenerateStatValue(0, 16);
                            MoveHit.Text = GenerateStatValue(1, -4);
                            MoveGD.Text = GenerateStatValue(2, 29);
                            MoveSharp.Text = GenerateStatValue(3, 5);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[7].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[7] == 1);
                            CanUnlock.Checked = (Mon_Stats[2] >= 450);
                            PlaceHighlight(7);
                            MoveInfo.Text = "Withering tech. INT should be over 450 to unlock. Chains into Mind Blast when used 50x.";
                            break;
                        case 8:
                            MoveName.Text = "Mind Blast"; MoveName.ForeColor = IntCol;
                            MoveGuts.Text = "27";
                            MoveDamage.Text = GenerateStatValue(0, 26);
                            MoveHit.Text = GenerateStatValue(1, -8);
                            MoveGD.Text = GenerateStatValue(2, 34);
                            MoveSharp.Text = GenerateStatValue(3, 5);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[8].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[8] == 1);
                            CanUnlock.Checked = (Mon_MoveUsed[7] >= 50);
                            PlaceHighlight(8);
                            MoveInfo.Text = "Withering tech. Chains from 50 Mind Flares. No stat requirements.";
                            break;
                        case 9:
                            MoveName.Text = "Cross Slash"; MoveName.ForeColor = PowCol;
                            MoveGuts.Text = "45";
                            MoveDamage.Text = GenerateStatValue(0, 25);
                            MoveHit.Text = GenerateStatValue(1, 10);
                            MoveGD.Text = GenerateStatValue(2, 25);
                            MoveSharp.Text = GenerateStatValue(3, 25);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[9].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[9] == 1);
                            CanUnlock.Checked = (Mon_Stats[1] + Mon_Stats[2] + Mon_Stats[4] >= 1200 && Mon_Nature >= 20); //bedeg
                            PlaceHighlight(9);
                            MoveInfo.Text = @"Special tech. POW + INT + SPD should total over 1200 to unlock. Requires Good (+20) Nature.
Chains into Z-Smash when used 50x.";
                            break;
                        case 10:
                            MoveName.Text = "Rear-Leg Kick"; MoveName.ForeColor = PowCol;
                            MoveGuts.Text = "11";
                            MoveDamage.Text = GenerateStatValue(0, 12);
                            MoveHit.Text = GenerateStatValue(1, 8);
                            MoveGD.Text = GenerateStatValue(2, 5);
                            MoveSharp.Text = "---";
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[12].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[12] == 1);
                            CanUnlock.Checked = true;
                            PlaceHighlight(12);
                            MoveInfo.Text = "Starting basic tech.";
                            break;
                        case 11:
                            MoveName.Text = "Energy Shot"; MoveName.ForeColor = IntCol;
                            MoveGuts.Text = "17";
                            MoveDamage.Text = GenerateStatValue(0, 9);
                            MoveHit.Text = GenerateStatValue(1, 0);
                            MoveGD.Text = GenerateStatValue(2, 24);
                            MoveSharp.Text = GenerateStatValue(3, 5);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[13].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[13] == 1);
                            CanUnlock.Checked = (Mon_Stats[2] >= 250);
                            PlaceHighlight(13);
                            MoveInfo.Text = @"Withering tech. INT should be over 250 to unlock. Chains into Energy Shots when used 50x.
Learning priority over Mind Flare.";
                            break;
                        case 12:
                            MoveName.Text = "Javelin"; MoveName.ForeColor = PowCol;
                            MoveGuts.Text = "17";
                            MoveDamage.Text = GenerateStatValue(0, 17);
                            MoveHit.Text = GenerateStatValue(1, -1);
                            MoveGD.Text = GenerateStatValue(2, 7);
                            MoveSharp.Text = GenerateStatValue(3, 17);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[14].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[14] == 1);
                            CanUnlock.Checked = (Mon_Stats[1] + Mon_Stats[4] >= 400);
                            PlaceHighlight(14);
                            MoveInfo.Text = "Sharp tech. POW + SPD should total over 400 to unlock. Chains into Jump Javelin when used 50x."; //bedeg
                            break;
                        case 13:
                            MoveName.Text = "Death Thrust"; MoveName.ForeColor = PowCol;
                            MoveGuts.Text = "35";
                            MoveDamage.Text = GenerateStatValue(0, 62);
                            MoveHit.Text = GenerateStatValue(1, -18);
                            MoveGD.Text = GenerateStatValue(2, 19);
                            MoveSharp.Text = GenerateStatValue(3, 5);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[15].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[15] == 1);
                            CanUnlock.Checked = (Mon_Moves[3] >= 50 && Mon_Stats[1] >= 600 && Mon_Nature <= -50);
                            PlaceHighlight(15);
                            MoveInfo.Text = "Heavy tech. Chains from 50 Stab-Throws. Requires 600 POW, and Bad (-50) Nature.";
                            break;
                        case 14:
                            MoveName.Text = "Rush Slash"; MoveName.ForeColor = PowCol;
                            MoveGuts.Text = "18";
                            MoveDamage.Text = GenerateStatValue(0, 31);
                            MoveHit.Text = GenerateStatValue(1, -14);
                            MoveGD.Text = GenerateStatValue(2, 6);
                            MoveSharp.Text = GenerateStatValue(3, 5);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[18].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[18] == 1);
                            CanUnlock.Checked = (Mon_Stats[1] >= 250);
                            PlaceHighlight(18);
                            MoveInfo.Text = "Heavy tech. POW should be over 250 to unlock. Learning priority over Stab-Throw.";
                            break;
                        case 15:
                            MoveName.Text = "Energy Shots"; MoveName.ForeColor = IntCol;
                            MoveGuts.Text = "25";
                            MoveDamage.Text = GenerateStatValue(0, 11);
                            MoveHit.Text = GenerateStatValue(1, 0);
                            MoveGD.Text = GenerateStatValue(2, 37);
                            MoveSharp.Text = GenerateStatValue(3, 5);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[19].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[19] == 1);
                            CanUnlock.Checked = (Mon_MoveUsed[13] >= 50);
                            PlaceHighlight(19);
                            MoveInfo.Text = "Withering tech. Chains from 50 Energy Shot uses. No stat requirement.";
                            break;
                        case 16:
                            MoveName.Text = "Jump Javelin"; MoveName.ForeColor = PowCol;
                            MoveGuts.Text = "26";
                            MoveDamage.Text = GenerateStatValue(0, 22);
                            MoveHit.Text = GenerateStatValue(1, -3);
                            MoveGD.Text = GenerateStatValue(2, 10);
                            MoveSharp.Text = GenerateStatValue(3, 30);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[20].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[20] == 1);
                            CanUnlock.Checked = (Mon_MoveUsed[14] >= 50 && (Mon_Stats[1] + Mon_Stats[4] >= 650));
                            PlaceHighlight(20);
                            MoveInfo.Text = "Sharp tech. Chains from 50 Javelins. POW + SPD should total over 650 to unlock.";
                            break;
                        case 17:
                            MoveName.Text = "Meteor Drive"; MoveName.ForeColor = PowCol;
                            MoveGuts.Text = "50";
                            MoveDamage.Text = GenerateStatValue(0, 55);
                            MoveHit.Text = GenerateStatValue(1, -5);
                            MoveGD.Text = GenerateStatValue(2, 15);
                            MoveSharp.Text = GenerateStatValue(3, 15);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[21].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[21] == 1);
                            CanUnlock.Checked = (Mon_Stats[1] + Mon_Stats[4] + Mon_Stats[5] >= 1200);
                            PlaceHighlight(21);
                            MoveInfo.Text = @"Special tech. POW + SPD + DEF should total over 1200 to unlock.
Learning priority over Cross Slash.";
                            break;
                        default:
                            MoveName.Text = "No Move"; MoveName.ForeColor = Color.Black;
                            MoveGuts.Text = "---";
                            MoveDamage.Text = "---";
                            MoveHit.Text = "---";
                            MoveGD.Text = "---";
                            MoveSharp.Text = "---";
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = "---";
                            MoveUnlocked.Checked = false;
                            CanUnlock.Checked = false;
                            MoveInfo.Text = "Click on a move to learn more.";
                            break;
                    }
                    break;
                case 3: // Colour Pandora
                    switch (MoveSelected)
                    {
                        case 1:
                            MoveName.Text = "Face Attack"; MoveName.ForeColor = PowCol;
                            MoveGuts.Text = "12";
                            MoveDamage.Text = GenerateStatValue(0, 12);
                            MoveHit.Text = GenerateStatValue(0, 5); //bedeg
                            MoveGD.Text = "---";
                            MoveSharp.Text = "---";
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[0].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[0] == 1);
                            CanUnlock.Checked = true;
                            PlaceHighlight(0);
                            MoveInfo.Text = "Starting basic tech.";
                            break;
                        case 2:
                            MoveName.Text = "Giant Whip"; MoveName.ForeColor = PowCol;
                            MoveGuts.Text = "17";
                            MoveDamage.Text = GenerateStatValue(0, 30);
                            MoveHit.Text = GenerateStatValue(1, -9);
                            MoveGD.Text = GenerateStatValue(2, 5);
                            MoveSharp.Text = GenerateStatValue(3, 5);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[1].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[1] == 1);
                            CanUnlock.Checked = (Mon_Stats[1] >= 250);
                            PlaceHighlight(1);
                            MoveInfo.Text = "Heavy tech. POW should be over 250 to unlock.";
                            break;
                        case 3:
                            MoveName.Text = "Tail Swing"; MoveName.ForeColor = PowCol;
                            MoveGuts.Text = "10";
                            MoveDamage.Text = GenerateStatValue(0, 8);
                            MoveHit.Text = GenerateStatValue(1, 9);
                            MoveGD.Text = GenerateStatValue(2, 5);
                            MoveSharp.Text = "---";
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[6].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[6] == 1);
                            CanUnlock.Checked = true;
                            PlaceHighlight(6);
                            MoveInfo.Text = "Starting basic tech. Chains into Two Swings when used 30x."; //bedeg
                            break;
                        case 4:
                            MoveName.Text = "Two Swings"; MoveName.ForeColor = PowCol;
                            MoveGuts.Text = "14";
                            MoveDamage.Text = GenerateStatValue(0, 15);
                            MoveHit.Text = GenerateStatValue(1, 9);
                            MoveGD.Text = GenerateStatValue(2, 5);
                            MoveSharp.Text = "---";
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[7].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[7] == 1);
                            CanUnlock.Checked = (Mon_MoveUsed[6] > 30);
                            PlaceHighlight(7);
                            MoveInfo.Text = "Basic tech. Chains from 30 Tail Swing (Torble Sea Errantry),\r\nor 50 Tail Swing (Papas, Mandy, Parepare Errantry)"; //bedeg
                            break;
                        case 5:
                            MoveName.Text = "Kamikaze"; MoveName.ForeColor = PowCol;
                            MoveGuts.Text = "28";
                            MoveDamage.Text = GenerateStatValue(0, 23);
                            MoveHit.Text = GenerateStatValue(1, 20);
                            MoveGD.Text = GenerateStatValue(2, 18);
                            MoveSharp.Text = GenerateStatValue(3, 10);
                            MoveSH.Text = GenerateStatValue(0, 10);
                            MoveSH.Show();
                            SHLabel.Text = "Self-Damage (Miss):";
                            SHLabel.Show();
                            MoveUses.Text = Mon_MoveUsed[8].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[8] == 1);
                            CanUnlock.Checked = (Mon_Stats[1] + Mon_Stats[3] >= 650);
                            PlaceHighlight(8);
                            MoveInfo.Text = "Hit tech. POW + SKI should total over 650 to unlock.";
                            break;
                        case 6:
                            MoveName.Text = "Vital Ritual"; MoveName.ForeColor = IntCol;
                            MoveGuts.Text = "50";
                            MoveDamage.Text = GenerateStatValue(0, 30);
                            MoveHit.Text = GenerateStatValue(1, -20);
                            MoveGD.Text = GenerateStatValue(2, 30);
                            MoveSharp.Text = "---";
                            MoveSH.Text = "100%";
                            MoveSH.Show();
                            SHLabel.Text = "Guts/HP Drain:";
                            SHLabel.Show();
                            MoveUses.Text = Mon_MoveUsed[9].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[9] == 1);
                            CanUnlock.Checked = (Mon_Stats[2] >= 600 && Mon_Nature <= -50);
                            PlaceHighlight(9);
                            MoveInfo.Text = "Special tech. INT should be over 600 to unlock. Requires Bad (-50) Nature.";
                            break;
                        case 7:
                            MoveName.Text = "Cracker"; MoveName.ForeColor = PowCol;
                            MoveGuts.Text = "18";
                            MoveDamage.Text = GenerateStatValue(0, 5);
                            MoveHit.Text = GenerateStatValue(1, 0);
                            MoveGD.Text = GenerateStatValue(2, 28);
                            MoveSharp.Text = GenerateStatValue(3, 5);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[12].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[12] == 1);
                            CanUnlock.Checked = (Mon_Stats[1] + Mon_Stats[2] >= 400);
                            PlaceHighlight(12);
                            MoveInfo.Text = "Withering tech. POW + INT should total over 400 to unlock.\r\nChains into Megacracker when used 50x.";
                            break;
                        case 8:
                            MoveName.Text = "Megacracker"; MoveName.ForeColor = PowCol;
                            MoveGuts.Text = "30";
                            MoveDamage.Text = GenerateStatValue(0, 11);
                            MoveHit.Text = GenerateStatValue(1, 0);
                            MoveGD.Text = GenerateStatValue(2, 40);
                            MoveSharp.Text = GenerateStatValue(3, 5);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[13].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[13] == 1);
                            CanUnlock.Checked = (Mon_MoveUsed[12] >= 50);
                            PlaceHighlight(13);
                            MoveInfo.Text = "Withering tech. Chains from 50 Crackers. No stat requirements.";
                            break;
                        case 9:
                            MoveName.Text = "Triple Shots"; MoveName.ForeColor = PowCol;
                            MoveGuts.Text = "18";
                            MoveDamage.Text = GenerateStatValue(0, 20);
                            MoveHit.Text = GenerateStatValue(1, -2);
                            MoveGD.Text = GenerateStatValue(2, 5);
                            MoveSharp.Text = GenerateStatValue(3, 20);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[14].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[14] == 1);
                            CanUnlock.Checked = (Mon_Stats[1] + Mon_Stats[4] >= 400);
                            PlaceHighlight(14);
                            MoveInfo.Text = "Sharp tech. POW + SPD should total over 400 to unlock. Chains into Delta Attack when used 50x.";
                            break;
                        case 10:
                            MoveName.Text = "Delta Attack"; MoveName.ForeColor = IntCol;
                            MoveGuts.Text = "55";
                            MoveDamage.Text = GenerateStatValue(0, 30);
                            MoveHit.Text = GenerateStatValue(1, 15);
                            MoveGD.Text = GenerateStatValue(2, 30);
                            MoveSharp.Text = GenerateStatValue(3, 15);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[15].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[15] == 1);
                            CanUnlock.Checked = (Mon_MoveUsed[14] >= 50 && (Mon_Stats[2] + Mon_Stats[3] + Mon_Stats[4] >= 1200) && Mon_Nature >= 20); //bedeg
                            PlaceHighlight(15);
                            MoveInfo.Text = @"Special tech. Chains from 50 Triple Shots. INT + SKI + SPD should total over 1200 to unlock.
Requires Good (+20) Nature.";
                            break;
                        case 11:
                            MoveName.Text = "Shotgun"; MoveName.ForeColor = IntCol;
                            MoveGuts.Text = "18";
                            MoveDamage.Text = GenerateStatValue(0, 14);
                            MoveHit.Text = GenerateStatValue(1, 15);
                            MoveGD.Text = GenerateStatValue(2, 7);
                            MoveSharp.Text = GenerateStatValue(3, 5);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[18].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[18] == 1);
                            CanUnlock.Checked = (Mon_Stats[2] + Mon_Stats[3] >= 400);
                            PlaceHighlight(18);
                            MoveInfo.Text = @"Hit tech. INT + SKI should total over 400 to unlock. Chains into Megashotgun when used 50x.
Learning priority over Kamikaze.";
                            break;
                        case 12:
                            MoveName.Text = "Megashotgun"; MoveName.ForeColor = IntCol;
                            MoveGuts.Text = "24";
                            MoveDamage.Text = GenerateStatValue(0, 22);
                            MoveHit.Text = GenerateStatValue(1, 10);
                            MoveGD.Text = GenerateStatValue(2, 9);
                            MoveSharp.Text = GenerateStatValue(3, 5);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[19].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[19] == 1);
                            CanUnlock.Checked = (Mon_MoveUsed[18] >= 50);
                            PlaceHighlight(19);
                            MoveInfo.Text = "Hit tech. Chains from 50 Shotguns. No stat requirement.";
                            break;
                        case 13:
                            MoveName.Text = "Giant Wheel"; MoveName.ForeColor = PowCol;
                            MoveGuts.Text = "29";
                            MoveDamage.Text = GenerateStatValue(0, 42);
                            MoveHit.Text = GenerateStatValue(1, -15);
                            MoveGD.Text = GenerateStatValue(2, 12);
                            MoveSharp.Text = GenerateStatValue(3, 5);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[20].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[20] == 1);
                            CanUnlock.Checked = (Mon_Stats[1] >= 450);
                            PlaceHighlight(20);
                            MoveInfo.Text = "Heavy tech. POW should be over 450 to unlock.";
                            break;
                        case 14:
                            MoveName.Text = "Spiral Rush"; MoveName.ForeColor = PowCol;
                            MoveGuts.Text = "29";
                            MoveDamage.Text = GenerateStatValue(0, 26);
                            MoveHit.Text = GenerateStatValue(1, -5);
                            MoveGD.Text = GenerateStatValue(2, 10);
                            MoveSharp.Text = GenerateStatValue(3, 26);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[21].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[21] == 1);
                            CanUnlock.Checked = (Mon_Stats[1] + Mon_Stats[4] >= 650);
                            PlaceHighlight(21);
                            MoveInfo.Text = "Sharp tech. POW + SPD should be over 650 to unlock.";
                            break;
                        case 15:
                            MoveName.Text = "Meteor Drive"; MoveName.ForeColor = PowCol;
                            MoveGuts.Text = "50";
                            MoveDamage.Text = GenerateStatValue(0, 55);
                            MoveHit.Text = GenerateStatValue(1, -10);
                            MoveGD.Text = GenerateStatValue(2, 45);
                            MoveSharp.Text = GenerateStatValue(3, 5);
                            MoveSH.Text = GenerateStatValue(0, 30);
                            MoveSH.Show();
                            SHLabel.Text = "Self-Damage (Miss):";
                            SHLabel.Show();
                            MoveUses.Text = Mon_MoveUsed[22].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[22] == 1);
                            CanUnlock.Checked = (Mon_Stats[0] + Mon_Stats[5] >= 800);
                            PlaceHighlight(22);
                            MoveInfo.Text = "Special tech. LIF + DEF should total over 800. Learning priority over Vital Ritual.";
                            break;
                        default:
                            MoveName.Text = "No Move"; MoveName.ForeColor = Color.Black;
                            MoveGuts.Text = "---";
                            MoveDamage.Text = "---";
                            MoveHit.Text = "---";
                            MoveGD.Text = "---";
                            MoveSharp.Text = "---";
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = "---";
                            MoveUnlocked.Checked = false;
                            CanUnlock.Checked = false;
                            MoveInfo.Text = "Click on a move to learn more.";
                            break;
                    }
                    break;
                case 4: // Beaclon
                    switch (MoveSelected)
                    {
                        case 1:
                            MoveName.Text = "Punch"; MoveName.ForeColor = PowCol;
                            MoveGuts.Text = "10";
                            MoveDamage.Text = GenerateStatValue(0, 12);
                            MoveHit.Text = GenerateStatValue(1, 6);
                            MoveGD.Text = "---";
                            MoveSharp.Text = "---";
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[0].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[0] == 1);
                            CanUnlock.Checked = true;
                            PlaceHighlight(0);
                            MoveInfo.Text = "Starting basic tech. Chains into Heavy Punch when used 30x.";
                            break;
                        case 2:
                            MoveName.Text = "Heavy Punch"; MoveName.ForeColor = PowCol;
                            MoveGuts.Text = "16";
                            MoveDamage.Text = GenerateStatValue(0, 15);
                            MoveHit.Text = GenerateStatValue(1, 5);
                            MoveGD.Text = GenerateStatValue(2, 5);
                            MoveSharp.Text = GenerateStatValue(3, 5);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[1].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[1] == 1);
                            CanUnlock.Checked = (Mon_MoveUsed[0] >= 30);
                            PlaceHighlight(1);
                            MoveInfo.Text = "Basic tech. Chains from 30 Punches. Chains into Maximal Punch when used 30x.";
                            break;
                        case 3:
                            MoveName.Text = "Maximal Punch"; MoveName.ForeColor = PowCol;
                            MoveGuts.Text = "22";
                            MoveDamage.Text = GenerateStatValue(0, 22);
                            MoveHit.Text = GenerateStatValue(1, 3);
                            MoveGD.Text = GenerateStatValue(2, 10);
                            MoveSharp.Text = GenerateStatValue(3, 10);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[2].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[2] == 1);
                            CanUnlock.Checked = (Mon_MoveUsed[1] >= 30);
                            PlaceHighlight(2);
                            MoveInfo.Text = "Basic tech. Chains from 30 Heavy Punches.";
                            break;
                        case 4:
                            MoveName.Text = "Horn Strike"; MoveName.ForeColor = PowCol;
                            MoveGuts.Text = "13";
                            MoveDamage.Text = GenerateStatValue(0, 14);
                            MoveHit.Text = GenerateStatValue(1, 2);
                            MoveGD.Text = "---";
                            MoveSharp.Text = GenerateStatValue(3, 5);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[3].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[3] == 1);
                            CanUnlock.Checked = true;
                            PlaceHighlight(3);
                            MoveInfo.Text = "Starting basic tech. Chains into Horn Attack when used 50x.";
                            break;
                        case 5:
                            MoveName.Text = "Horn Attack"; MoveName.ForeColor = PowCol;
                            MoveGuts.Text = "18";
                            MoveDamage.Text = GenerateStatValue(0, 25);
                            MoveHit.Text = GenerateStatValue(1, 1);
                            MoveGD.Text = "---";
                            MoveSharp.Text = GenerateStatValue(3, 5);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[4].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[4] == 1);
                            CanUnlock.Checked = (Mon_MoveUsed[3] >= 50);
                            PlaceHighlight(4);
                            MoveInfo.Text = "Basic tech. Chains from 50 Horn Strikes.";
                            break;
                        case 6:
                            MoveName.Text = "Spinning Horn"; MoveName.ForeColor = PowCol;
                            MoveGuts.Text = "25";
                            MoveDamage.Text = GenerateStatValue(0, 16);
                            MoveHit.Text = GenerateStatValue(1, -12);
                            MoveGD.Text = GenerateStatValue(2, 35);
                            MoveSharp.Text = GenerateStatValue(3, 5);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[6].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[6] == 1);
                            CanUnlock.Checked = (Mon_Stats[1] + Mon_Stats[2] >= 650 && Mon_Nature <= -20); //bedeg
                            PlaceHighlight(6);
                            MoveInfo.Text = "Withering tech. POW + INT should total over 650 to unlock. Requires Bad (-20) Nature.";
                            break;
                        case 7:
                            MoveName.Text = "Punch Combo"; MoveName.ForeColor = PowCol;
                            MoveGuts.Text = "42";
                            MoveDamage.Text = GenerateStatValue(0, 40);
                            MoveHit.Text = GenerateStatValue(1, -5);
                            MoveGD.Text = GenerateStatValue(2, 15);
                            MoveSharp.Text = GenerateStatValue(3, 15);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[7].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[7] == 1);
                            CanUnlock.Checked = (Mon_Stats[1] + Mon_Stats[4] >= 800);
                            PlaceHighlight(7);
                            MoveInfo.Text = @"Special tech. POW + SPD should total over 800 to unlock.
Chains into Beaclon Combo when used 50x";
                            break;
                        case 8:
                            MoveName.Text = "Beaclon Combo"; MoveName.ForeColor = PowCol;
                            MoveGuts.Text = "50";
                            MoveDamage.Text = GenerateStatValue(0, 55);
                            MoveHit.Text = GenerateStatValue(1, -7);
                            MoveGD.Text = GenerateStatValue(2, 20);
                            MoveSharp.Text = GenerateStatValue(3, 15);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[8].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[8] == 1);
                            CanUnlock.Checked = (Mon_MoveUsed[7] >= 50);
                            PlaceHighlight(8);
                            MoveInfo.Text = "Special tech. Chains from 50 Punch Combos.";
                            break;
                        case 9:
                            MoveName.Text = "Triple Stabs"; MoveName.ForeColor = PowCol;
                            MoveGuts.Text = "34";
                            MoveDamage.Text = GenerateStatValue(0, 21);
                            MoveHit.Text = GenerateStatValue(1, -5);
                            MoveGD.Text = GenerateStatValue(2, 39);
                            MoveSharp.Text = GenerateStatValue(3, 5);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[9].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[9] == 1);
                            CanUnlock.Checked = ((Mon_Stats[1] + Mon_Stats[2] > 800) && Mon_SubGenus == 32);
                            PlaceHighlight(9);
                            MoveInfo.Text = "Withering tech. Unique to Ducklon. POW + INT should total over 800 to unlock.";
                            break;
                        case 10:
                            MoveName.Text = "Dive Assault"; MoveName.ForeColor = PowCol;
                            MoveGuts.Text = "18";
                            MoveDamage.Text = GenerateStatValue(0, 28);
                            MoveHit.Text = GenerateStatValue(1, -13);
                            MoveGD.Text = GenerateStatValue(2, 5);
                            MoveSharp.Text = GenerateStatValue(3, 5);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[12].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[12] == 1);
                            CanUnlock.Checked = (Mon_Stats[1] >= 250);
                            PlaceHighlight(12);
                            MoveInfo.Text = "Heavy tech. POW should be over 250 to unlock. Chains into Spiral Dive when used 50x.";
                            break;
                        case 11:
                            MoveName.Text = "Spiral Dive"; MoveName.ForeColor = PowCol;
                            MoveGuts.Text = "26";
                            MoveDamage.Text = GenerateStatValue(0, 34);
                            MoveHit.Text = GenerateStatValue(1, -10);
                            MoveGD.Text = GenerateStatValue(2, 8);
                            MoveSharp.Text = GenerateStatValue(3, 8);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[13].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[13] == 1);
                            CanUnlock.Checked = (Mon_MoveUsed[12] >= 50);
                            PlaceHighlight(13);
                            MoveInfo.Text = "Heavy tech. Chains from 50 Dive Assaults.";
                            break;
                        case 12:
                            MoveName.Text = "Tremor"; MoveName.ForeColor = IntCol;
                            MoveGuts.Text = "20";
                            MoveDamage.Text = GenerateStatValue(0, 8);
                            MoveHit.Text = GenerateStatValue(1, 0);
                            MoveGD.Text = GenerateStatValue(2, 27);
                            MoveSharp.Text = GenerateStatValue(3, 5);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[14].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[14] == 1);
                            CanUnlock.Checked = (Mon_Stats[2] >= 250);
                            PlaceHighlight(14);
                            MoveInfo.Text = @"Withering tech. INT should be over 250 to unlock.
Rocklon can chain into Earthquake when used 50x.";
                            break;
                        case 13:
                            MoveName.Text = "Horn Combo"; MoveName.ForeColor = PowCol;
                            MoveGuts.Text = "22";
                            MoveDamage.Text = GenerateStatValue(0, 22);
                            MoveHit.Text = GenerateStatValue(1, -8);
                            MoveGD.Text = GenerateStatValue(2, 12);
                            MoveSharp.Text = GenerateStatValue(3, 25);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[15].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[15] == 1);
                            CanUnlock.Checked = (Mon_Stats[1] + Mon_Stats[4] >= 650);
                            PlaceHighlight(15);
                            MoveInfo.Text = "Sharp tech. POW + SPD should total over 650 to unlock.";
                            break;
                        case 14:
                            MoveName.Text = "Horn Smash"; MoveName.ForeColor = PowCol;
                            MoveGuts.Text = "50";
                            MoveDamage.Text = GenerateStatValue(0, 30);
                            MoveHit.Text = GenerateStatValue(1, 10);
                            MoveGD.Text = GenerateStatValue(2, 25);
                            MoveSharp.Text = GenerateStatValue(3, 15);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[16].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[16] == 1);
                            CanUnlock.Checked = (Mon_Stats[1] + Mon_Stats[3] + Mon_Stats[4] >= 1200);
                            PlaceHighlight(16);
                            MoveInfo.Text = @"Special tech. POW + SKI + SPD should total over 1200 to unlock.
Chains into Frantic Horn when used 50x. Learning priority over Punch Combo.";
                            break;
                        case 15:
                            MoveName.Text = "Earthquake"; MoveName.ForeColor = IntCol;
                            MoveGuts.Text = "40";
                            MoveDamage.Text = GenerateStatValue(0, 37);
                            MoveHit.Text = GenerateStatValue(1, -15);
                            MoveGD.Text = GenerateStatValue(2, 45);
                            MoveSharp.Text = GenerateStatValue(3, 5);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[17].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[17] == 1);
                            CanUnlock.Checked = ((Mon_Stats[2] + Mon_Stats[5] >= 800) && Mon_SubGenus == 7 && Mon_MoveUsed[14] >= 50);
                            PlaceHighlight(17);
                            MoveInfo.Text = @"Special tech. Unique to Rocklon. Chains from 50 Tremors. 
INT + DEF should total over 800 to unlock.";
                            break;
                        case 16:
                            MoveName.Text = "Top Assault"; MoveName.ForeColor = PowCol;
                            MoveGuts.Text = "17";
                            MoveDamage.Text = GenerateStatValue(0, 13);
                            MoveHit.Text = GenerateStatValue(1, 15);
                            MoveGD.Text = GenerateStatValue(2, 5);
                            MoveSharp.Text = GenerateStatValue(3, 5);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[18].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[18] == 1);
                            CanUnlock.Checked = (Mon_Stats[1] + Mon_Stats[3] >= 400);
                            PlaceHighlight(18);
                            MoveInfo.Text = "Hit tech. POW + SKI should total over 400 to unlock.";
                            break;
                        case 17:
                            MoveName.Text = "Rolling Bomb"; MoveName.ForeColor = PowCol;
                            MoveGuts.Text = "25";
                            MoveDamage.Text = GenerateStatValue(0, 23);
                            MoveHit.Text = GenerateStatValue(1, 12);
                            MoveGD.Text = GenerateStatValue(2, 9);
                            MoveSharp.Text = GenerateStatValue(3, 5);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[19].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[19] == 1);
                            CanUnlock.Checked = (Mon_Stats[1] + Mon_Stats[3] >= 650 && Mon_Nature >= 20);
                            PlaceHighlight(19);
                            MoveInfo.Text = "Hit tech. POW + SKI should total over 650 to unlock. Requires Good (+20) Nature.";
                            break;
                        case 18:
                            MoveName.Text = "Flying Press"; MoveName.ForeColor = PowCol;
                            MoveGuts.Text = "27";
                            MoveDamage.Text = GenerateStatValue(0, 44);
                            MoveHit.Text = GenerateStatValue(1, -18);
                            MoveGD.Text = GenerateStatValue(2, 15);
                            MoveSharp.Text = GenerateStatValue(3, 5);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[20].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[20] == 1);
                            CanUnlock.Checked = (Mon_Stats[1] >= 450);
                            PlaceHighlight(20);
                            MoveInfo.Text = "Heavy tech. POW should be over 450 to unlock.";
                            break;
                        case 19:
                            MoveName.Text = "Horn Cannon"; MoveName.ForeColor = IntCol;
                            MoveGuts.Text = "17";
                            MoveDamage.Text = GenerateStatValue(0, 15);
                            MoveHit.Text = GenerateStatValue(1, -10);
                            MoveGD.Text = GenerateStatValue(2, 15);
                            MoveSharp.Text = GenerateStatValue(3, 16);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[21].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[21] == 1);
                            CanUnlock.Checked = (Mon_Stats[2] + Mon_Stats[4] >= 400);
                            PlaceHighlight(21);
                            MoveInfo.Text = "Sharp tech. INT + SPD should total over 400 to unlock. Learning priority over Horn Combo.";
                            break;
                        case 20:
                            MoveName.Text = "Frantic Horn"; MoveName.ForeColor = IntCol;
                            MoveGuts.Text = "55";
                            MoveDamage.Text = GenerateStatValue(0, 35);
                            MoveHit.Text = GenerateStatValue(1, 5);
                            MoveGD.Text = GenerateStatValue(2, 35);
                            MoveSharp.Text = GenerateStatValue(3, 15);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[22].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[22] == 1);
                            CanUnlock.Checked = (Mon_MoveUsed[16] >= 50);
                            PlaceHighlight(22);
                            MoveInfo.Text = "Special tech. Chains from 50 Horn Smashes.";
                            break;
                        case 21:
                            MoveName.Text = "Fist Missile"; MoveName.ForeColor = PowCol;
                            MoveGuts.Text = "30";
                            MoveDamage.Text = GenerateStatValue(0, 27);
                            MoveHit.Text = GenerateStatValue(1, -6);
                            MoveGD.Text = GenerateStatValue(2, 10);
                            MoveSharp.Text = GenerateStatValue(3, 27);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[23].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[23] == 1);
                            CanUnlock.Checked = ((Mon_Stats[1] + Mon_Stats[4] >= 800) && Mon_SubGenus == 5);
                            PlaceHighlight(23);
                            MoveInfo.Text = "Sharp tech. Mecarbor exclusive. POW + SPD should total over 800 to unlock.";
                            break;
                        default:
                            MoveName.Text = "No Move"; MoveName.ForeColor = Color.Black;
                            MoveGuts.Text = "---";
                            MoveDamage.Text = "---";
                            MoveHit.Text = "---";
                            MoveGD.Text = "---";
                            MoveSharp.Text = "---";
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = "---";
                            MoveUnlocked.Checked = false;
                            CanUnlock.Checked = false;
                            MoveInfo.Text = "Click on a move to learn more.";
                            break;
                    }
                    break;
                case 5: // Henger
                    switch (MoveSelected)
                    {
                        case 1:
                            MoveName.Text = "Punch"; MoveName.ForeColor = PowCol;
                            MoveGuts.Text = "10";
                            MoveDamage.Text = GenerateStatValue(0, 13);
                            MoveHit.Text = GenerateStatValue(1, 5);
                            MoveGD.Text = "---";
                            MoveSharp.Text = "---";
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[0].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[0] == 1);
                            CanUnlock.Checked = true;
                            PlaceHighlight(0);
                            MoveInfo.Text = "Starting basic tech. Chains into Heavy Chop when used 30x.\r\nChains into Eye Beam for some reason when used 50x.";
                            break;
                        case 2:
                            MoveName.Text = "Kick"; MoveName.ForeColor = PowCol;
                            MoveGuts.Text = "16";
                            MoveDamage.Text = GenerateStatValue(0, 19);
                            MoveHit.Text = GenerateStatValue(1, 1);
                            MoveGD.Text = GenerateStatValue(2, 5);
                            MoveSharp.Text = GenerateStatValue(3, 5);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[1].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[1] == 1);
                            CanUnlock.Checked = (Mon_MoveUsed[3] >= 30);
                            PlaceHighlight(1);
                            MoveInfo.Text = "Basic tech. Chains from 30 Low Kicks.";
                            break;
                        case 3:
                            MoveName.Text = "Heavy Chop"; MoveName.ForeColor = PowCol;
                            MoveGuts.Text = "15"; //bedeg
                            MoveDamage.Text = GenerateStatValue(0, 18);
                            MoveHit.Text = GenerateStatValue(1, 3);
                            MoveGD.Text = GenerateStatValue(2, 6);
                            MoveSharp.Text = "---";
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[2].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[2] == 1);
                            CanUnlock.Checked = (Mon_MoveUsed[0] >= 30);
                            PlaceHighlight(2);
                            MoveInfo.Text = "Basic tech. Chains from 30 Punches.";
                            break;
                        case 4:
                            MoveName.Text = "Low Kick"; MoveName.ForeColor = PowCol;
                            MoveGuts.Text = "13";
                            MoveDamage.Text = GenerateStatValue(0, 15);
                            MoveHit.Text = GenerateStatValue(1, 7);
                            MoveGD.Text = "---";
                            MoveSharp.Text = GenerateStatValue(3, 5);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[3].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[3] == 1);
                            CanUnlock.Checked = true;
                            PlaceHighlight(3);
                            MoveInfo.Text = "Starting basic tech. Chains into Kick when used 30x.";
                            break;
                        case 5:
                            MoveName.Text = "Laser Cutter"; MoveName.ForeColor = PowCol;
                            MoveGuts.Text = "28";
                            MoveDamage.Text = GenerateStatValue(0, 42);
                            MoveHit.Text = GenerateStatValue(1, -15);
                            MoveGD.Text = GenerateStatValue(2, 15);
                            MoveSharp.Text = GenerateStatValue(3, 10);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[6].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[6] == 1);
                            CanUnlock.Checked = (Mon_Stats[1] >= 450);
                            PlaceHighlight(6);
                            MoveInfo.Text = "Heavy tech. POW should be over 450 to unlock. Chains into Two Cutters when used 50x.";
                            break;
                        case 6:
                            MoveName.Text = "Yo-Yo"; MoveName.ForeColor = PowCol;
                            MoveGuts.Text = "17";
                            MoveDamage.Text = GenerateStatValue(0, 10);
                            MoveHit.Text = GenerateStatValue(1, -3);
                            MoveGD.Text = GenerateStatValue(2, 24);
                            MoveSharp.Text = GenerateStatValue(3, 10);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[7].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[7] == 1);
                            CanUnlock.Checked = (Mon_Stats[1] + Mon_Stats[2] >= 400);
                            PlaceHighlight(7);
                            MoveInfo.Text = @"Withering tech. POW + INT should total over 400 to unlock.
Chains into Two Yo-Yos when used 50x.";
                            break;
                        case 7:
                            MoveName.Text = "Laser Sword"; MoveName.ForeColor = PowCol;
                            MoveGuts.Text = "50";
                            MoveDamage.Text = GenerateStatValue(0, 40);
                            MoveHit.Text = GenerateStatValue(1, -5);
                            MoveGD.Text = GenerateStatValue(2, 40);
                            MoveSharp.Text = GenerateStatValue(3, 15);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[8].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[8] == 1);
                            CanUnlock.Checked = (Mon_Stats[1] + Mon_Stats[4] >= 800);
                            PlaceHighlight(8);
                            MoveInfo.Text = @"Special tech. POW + SPD should total over 800 to unlock.
Chains into Laser Swords when used 50x.";
                            break;
                        case 8:
                            MoveName.Text = "Laser Swords"; MoveName.ForeColor = PowCol;
                            MoveGuts.Text = "55";
                            MoveDamage.Text = GenerateStatValue(0, 55);
                            MoveHit.Text = GenerateStatValue(1, -10);
                            MoveGD.Text = GenerateStatValue(2, 40);
                            MoveSharp.Text = GenerateStatValue(3, 20);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[9].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[9] == 1);
                            CanUnlock.Checked = (Mon_MoveUsed[8] >= 50);
                            PlaceHighlight(9);
                            MoveInfo.Text = "Special tech. Chains from 50 Laser Sword uses.";
                            break;
                        case 9:
                            MoveName.Text = "Two Cutters"; MoveName.ForeColor = PowCol;
                            MoveGuts.Text = "50";
                            MoveDamage.Text = GenerateStatValue(0, 50);
                            MoveHit.Text = GenerateStatValue(1, -18);
                            MoveGD.Text = GenerateStatValue(2, 16); //bedeg
                            MoveSharp.Text = GenerateStatValue(3, 10);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[10].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[10] == 1);
                            CanUnlock.Checked = (Mon_MoveUsed[6] >= 50);
                            PlaceHighlight(10);
                            MoveInfo.Text = "Heavy tech. Chains from 50 Laser Cutters.";
                            break;
                        case 10:
                            MoveName.Text = "Two Yo-Yos"; MoveName.ForeColor = PowCol;
                            MoveGuts.Text = "29";
                            MoveDamage.Text = GenerateStatValue(0, 20);
                            MoveHit.Text = GenerateStatValue(1, -5);
                            MoveGD.Text = GenerateStatValue(2, 32);
                            MoveSharp.Text = GenerateStatValue(3, 10);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[11].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[11] == 1);
                            CanUnlock.Checked = (Mon_MoveUsed[7] >= 50);
                            PlaceHighlight(11);
                            MoveInfo.Text = "Withering tech. Chains from 50 Yo-Yos.";
                            break;
                        case 11:
                            MoveName.Text = "Arm Cannon"; MoveName.ForeColor = IntCol;
                            MoveGuts.Text = "18";
                            MoveDamage.Text = GenerateStatValue(0, 15);
                            MoveHit.Text = GenerateStatValue(1, 15);
                            MoveGD.Text = GenerateStatValue(2, 5);
                            MoveSharp.Text = GenerateStatValue(3, 5);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[12].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[12] == 1);
                            CanUnlock.Checked = (Mon_Stats[2] + Mon_Stats[3] >= 400);
                            PlaceHighlight(12);
                            MoveInfo.Text = "Hit tech. INT + SKI should total over 400 to unlock. Chains into Napalm Shot when used 50x.";
                            break;
                        case 12:
                            MoveName.Text = "Napalm Shot"; MoveName.ForeColor = IntCol;
                            MoveGuts.Text = "24";
                            MoveDamage.Text = GenerateStatValue(0, 20);
                            MoveHit.Text = GenerateStatValue(1, 15);
                            MoveGD.Text = GenerateStatValue(2, 5);
                            MoveSharp.Text = GenerateStatValue(3, 10);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[13].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[13] == 1);
                            CanUnlock.Checked = (Mon_MoveUsed[12] > 50 && (Mon_Stats[2] + Mon_Stats[3] >= 650));
                            PlaceHighlight(13);
                            MoveInfo.Text = @"Hit tech. Chains from 50 Arm Cannons. Chains into Burst Cannon when used 50x.
INT + SKI should total over 650 to unlock.";
                            break;
                        case 13:
                            MoveName.Text = "Hammer Fall"; MoveName.ForeColor = PowCol;
                            MoveGuts.Text = "19";
                            MoveDamage.Text = GenerateStatValue(0, 27);
                            MoveHit.Text = GenerateStatValue(1, -9);
                            MoveGD.Text = GenerateStatValue(2, 5);
                            MoveSharp.Text = GenerateStatValue(3, 10);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[14].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[14] == 1);
                            CanUnlock.Checked = (Mon_Stats[1] >= 250);
                            PlaceHighlight(14);
                            MoveInfo.Text = "Heavy tech. POW should be over 250 to unlock. Chains into Sledge Fall when used 50x.";
                            break;
                        case 14:
                            MoveName.Text = "Burst Cannon"; MoveName.ForeColor = IntCol;
                            MoveGuts.Text = "35";
                            MoveDamage.Text = GenerateStatValue(0, 29);
                            MoveHit.Text = GenerateStatValue(1, 10);
                            MoveGD.Text = GenerateStatValue(2, 14);
                            MoveSharp.Text = GenerateStatValue(3, 10);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[15].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[15] == 1);
                            CanUnlock.Checked = (Mon_MoveUsed[13] >= 50);
                            PlaceHighlight(15);
                            MoveInfo.Text = "Special tech. Chains from 50 Napalm Shots.";
                            break;
                        case 15:
                            MoveName.Text = "Sledge Fall"; MoveName.ForeColor = PowCol;
                            MoveGuts.Text = "25";
                            MoveDamage.Text = GenerateStatValue(0, 35);
                            MoveHit.Text = GenerateStatValue(1, -8);
                            MoveGD.Text = GenerateStatValue(2, 5);
                            MoveSharp.Text = GenerateStatValue(3, 10);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[16].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[16] == 1);
                            CanUnlock.Checked = (Mon_MoveUsed[14] >= 50);
                            PlaceHighlight(16);
                            MoveInfo.Text = "Heavy tech. Chains from 50 Hammer Falls.";
                            break;
                        case 16:
                            MoveName.Text = "Sound Wave"; MoveName.ForeColor = IntCol;
                            MoveGuts.Text = "25";
                            MoveDamage.Text = GenerateStatValue(0, 6);
                            MoveHit.Text = GenerateStatValue(1, 1);
                            MoveGD.Text = GenerateStatValue(2, 35);
                            MoveSharp.Text = GenerateStatValue(3, 5);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[18].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[18] == 1);
                            CanUnlock.Checked = (Mon_Stats[2] >= 450);
                            PlaceHighlight(18);
                            MoveInfo.Text = "Withering tech. INT should be over 450 to unlock.";
                            break;
                        case 17:
                            //if (Random) -- Not to self; figure out how random works in C#.
                            //    MoveName.Text = "FIST MISSILE ME DADDY!!"; MoveName.ForeColor = PowCol;
                            //else
                            MoveName.Text = "Fist Missile"; MoveName.ForeColor = PowCol;
                            MoveGuts.Text = "19";
                            MoveDamage.Text = GenerateStatValue(0, 16);
                            MoveHit.Text = GenerateStatValue(1, 6);
                            MoveGD.Text = GenerateStatValue(2, 5);
                            MoveSharp.Text = GenerateStatValue(3, 21);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[19].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[19] == 1);
                            CanUnlock.Checked = (Mon_Stats[1] + Mon_Stats[4] >= 400 && Mon_Nature >= 20); //bedeg
                            PlaceHighlight(19);
                            MoveInfo.Text = @"Sharp tech. POW + SPD should total over 400 to unlock. Chains into Drill Shot when used 50x.
Requires Good (+20) Nature.";
                            break;
                        case 18:
                            MoveName.Text = "Drill Shot"; MoveName.ForeColor = PowCol;
                            MoveGuts.Text = "27";
                            MoveDamage.Text = GenerateStatValue(0, 22);
                            MoveHit.Text = GenerateStatValue(1, 0);
                            MoveGD.Text = GenerateStatValue(2, 12);
                            MoveSharp.Text = GenerateStatValue(3, 25);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[20].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[20] == 1);
                            CanUnlock.Checked = (Mon_MoveUsed[19] >= 50 && Mon_Nature >= 30); //bedeg
                            PlaceHighlight(20);
                            MoveInfo.Text = @"Sharp tech. Chains from 50 Fist Missiles. Chains into Drill Shots when used 50x.
Requires Good (+30) Nature.";
                            break;
                        case 19:
                            MoveName.Text = "Drill Shots"; MoveName.ForeColor = PowCol;
                            MoveGuts.Text = "45";
                            MoveDamage.Text = GenerateStatValue(0, 35);
                            MoveHit.Text = GenerateStatValue(1, 4);
                            MoveGD.Text = GenerateStatValue(2, 14);
                            MoveSharp.Text = GenerateStatValue(3, 30);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[21].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[21] == 1);
                            CanUnlock.Checked = (Mon_MoveUsed[20] >= 50 && Mon_Nature >= 50); //bedeg
                            PlaceHighlight(21);
                            MoveInfo.Text = "Special tech. Chains from 50 Drill Shot uses. Requires Good (+50) Nature.";
                            break;
                        case 20:
                            MoveName.Text = "Eye Beam"; MoveName.ForeColor = IntCol;
                            MoveGuts.Text = "26";
                            MoveDamage.Text = GenerateStatValue(0, 24);
                            MoveHit.Text = GenerateStatValue(1, -2);
                            MoveGD.Text = GenerateStatValue(2, 9);
                            MoveSharp.Text = GenerateStatValue(3, 23);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[22].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[22] == 1);
                            CanUnlock.Checked = (Mon_MoveUsed[0] >= 50 && (Mon_Stats[2] + Mon_Stats[4] >= 650));
                            PlaceHighlight(26);
                            MoveInfo.Text = "Sharp tech. Chains from 50 Punches(?!). INT + SPD should be over 650 to unlock.";
                            break;
                        default:
                            MoveName.Text = "No Move"; MoveName.ForeColor = Color.Black;
                            MoveGuts.Text = "---";
                            MoveDamage.Text = "---";
                            MoveHit.Text = "---";
                            MoveGD.Text = "---";
                            MoveSharp.Text = "---";
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = "---";
                            MoveUnlocked.Checked = false;
                            CanUnlock.Checked = false;
                            MoveInfo.Text = "Click on a move to learn more.";
                            break;
                    }
                    break;
                case 6: // Wracky
                    switch (MoveSelected)
                    {
                        case 1:
                            MoveName.Text = "Weapon";
                            LazyDataFill(false, 10, 8, 10, 5, 0, 0);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = true;
                            MoveInfo.Text = "Starting basic tech. Chains into Weapon Combo when used 30 times.";
                            break;

                        case 2:
                            MoveName.Text = "Weapon Combo";
                            LazyDataFill(false, 20, 16, 5, 10, 0, 1);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_MoveUsed[0] >= 30);
                            MoveInfo.Text = "Basic tech. Chains from 30 Weapon. No stat requirement.";
                            break;

                        case 3:
                            MoveName.Text = "Kick";
                            LazyDataFill(false, 28, 17, 17, 7, 5, 2);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_Stats[1] + Mon_Stats[3] >= 650);
                            MoveInfo.Text = "Hit tech. POW + SKI should total over 650 to unlock. Chains into Spin Kick when used 50x.";
                            break;

                        case 4:
                            MoveName.Text = "Spin Kick";
                            LazyDataFill(false, 33, 22, 15, 11, 5, 3);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_MoveUsed[2] >= 50);
                            MoveInfo.Text = "Hit tech. Chains from 50 Kick. Chains into Twister Kick when used 50x. No stat requirement.";
                            break;

                        case 5:
                            MoveName.Text = "Twister Kick";
                            LazyDataFill(false, 45, 35, 15, 15, 5, 4);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_MoveUsed[3] >= 50);
                            MoveInfo.Text = "Special tech. Chains from 50 Spin Kick. No stat requirement.";
                            break;

                        case 6:
                            MoveName.Text = "Punch";
                            LazyDataFill(false, 18, 20, -10, 10, 5, 5);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_Stats[1] >= 250);
                            MoveInfo.Text = "Heavy tech. POW should be over 250 to unlock. Chains into Heavy Punch when used 50x.";
                            break;

                        case 7:
                            MoveName.Text = "Heavy Punch";
                            LazyDataFill(false, 25, 27, -7, 15, 5, 6);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_MoveUsed[5] >= 50);
                            MoveInfo.Text = "Heavy tech. Chains from 50 Punch. Chains into Wracky Combo when used 50x.\r\nNo stat requirement.";
                            break;

                        case 8:
                            MoveName.Text = "Wracky Combo";
                            LazyDataFill(false, 50, 45, -15, 45, 5, 7);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_MoveUsed[6] >= 50);
                            MoveInfo.Text = "Special tech. Chains from 50 Heavy Punch. No stat requirement.";
                            break;

                        case 9:
                            MoveName.Text = "Necromancy";
                            LazyDataFill(true, 20, 4, 0, 28, 5, 8);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_Stats[2] >= 250);
                            MoveInfo.Text = "Withering tech. INT should be over 250 to unlock.";
                            break;

                        case 10:
                            MoveName.Text = "Sneak Attack";
                            LazyDataFill(false, 16, 11, 3, 9, 19, 9);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_Stats[1] + Mon_Stats[4] >= 400);
                            MoveInfo.Text = "Sharp tech. POW + SPD should total over 400 to unlock.";
                            break;

                        case 11:
                            MoveName.Text = "Sneak Combo";
                            LazyDataFill(false, 42, 20, 2, 10, 25, 10);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_Stats[1] + Mon_Stats[4] >= 600);
                            MoveInfo.Text = "Sharp tech. POW + SPD should total over 600 to unlock.";
                            break;

                        case 12:
                            MoveName.Text = "Fire Juggler";
                            LazyDataFill(true, 50, 55, -14, 25, 5, 11);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_Stats[2] >= 600 && Mon_SubGenus == 1);
                            MoveInfo.Text = "Special tech. Draco Doll exclusive. INT should be over 600 to unlock.";
                            break;

                        case 13:
                            MoveName.Text = "Weapon Throw";
                            LazyDataFill(true, 12, 11, 6, 5, 0, 12);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = true;
                            MoveInfo.Text = "Starting basic tech.";
                            break;

                        case 14:
                            MoveName.Text = "Spin Slash";
                            LazyDataFill(false, 28, 32, -12, 5, 10, 13);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_Stats[1] >= 450 && Mon_Nature >= 20);
                            MoveInfo.Text = "Heavy tech. POW should be over 450 to unlock. Chains into TwisterSlash when used 50x.\r\nRequires Good (+20) Nature.";
                            break;

                        case 15:
                            MoveName.Text = "Trick";
                            LazyDataFill(true, 32, 5, 0, 45, 5, 14);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_Stats[2] >= 450);
                            MoveInfo.Text = "Withering tech. INT should be over 450 to unlock.";
                            break;

                        case 16:
                            MoveName.Text = "Explosion";
                            LazyDataFill(false, 50, 70, -25, 50, 10, 15);
                            MoveSH.Text = GenerateStatValue(0, 70);
                            MoveSH.Show();
                            SHLabel.Text = "Self-Damage:";
                            SHLabel.Show();
                            CanUnlock.Checked = (Mon_Stats[1] + Mon_Stats[2] + Mon_Stats[5] >= 1200 && Mon_Nature <= -50 && (Mon_SubGenus != 0 && Mon_SubGenus != 1 && Mon_SubGenus != 18 && Mon_SubGenus != 31));
                            MoveInfo.Text = "Special tech. POW + INT + DEF should total over 1200 to unlock. Requires Bad (-50) Nature.\r\nBaby Doll, Draco Doll, Bakky, Mocky can't learn.";
                            break;

                        case 17:
                            MoveName.Text = "Head Spike";
                            LazyDataFill(true, 45, 25, -1, 37, 5, 16);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_Stats[2] >= 600);
                            MoveInfo.Text = "Special tech. INT should be over 600 to unlock. Chains into Fire Spike when used 50x.";
                            break;

                        case 18:
                            MoveName.Text = "Fire Spike";
                            LazyDataFill(true, 50, 33, -5, 43, 5, 17);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_MoveUsed[16] >= 50);
                            MoveInfo.Text = "Special tech. Chains from 50 Head Spike. No stat requirement.";
                            break;

                        case 19:
                            MoveName.Text = "Air Shot";
                            LazyDataFill(true, 17, 9, 22, 8, 5, 18);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_Stats[2] + Mon_Stats[3] >= 400);
                            MoveInfo.Text = "Hit tech. INT + SKI should total over 400 to unlock. Chains into Blast Shot when used 50x.";
                            break;

                        case 20:
                            MoveName.Text = "Blast Shot";
                            LazyDataFill(true, 28, 20, 22, 12, 5, 19);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_MoveUsed[18] >= 50);
                            MoveInfo.Text = "Hit tech. Chains from 50 Air Shot. No stat requirement.";
                            break;

                        case 21:
                            MoveName.Text = "TwisterSlash";
                            LazyDataFill(false, 39, 46, -13, 6, 10, 20);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_MoveUsed[13] >= 50 && Mon_Nature >= 50);
                            MoveInfo.Text = "Hit tech. Chains from 50 Spin Slash. Requires Good (+50) Nature.";
                            break;

                        case 22:
                            MoveName.Text = "Beat Dance";
                            LazyDataFill(false, 50, 25, 0, 25, 25, 21);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_Stats[1] + Mon_Stats[4] >= 800);
                            MoveInfo.Text = "Special tech. POW + SPD should total over 800 to unlock. Chains into Cursed Dance when used 50x.";
                            break;

                        case 23:
                            MoveName.Text = "Cursed Dance";
                            LazyDataFill(false, 55, 35, 0, 35, 35, 22);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_MoveUsed[21] >= 50);
                            MoveInfo.Text = "Special tech. Chains from 50 Beat Dance. No stat requirement.";
                            break;

                        case 24:
                            MoveName.Text = "Flame";
                            LazyDataFill(true, 50, 44, -14, 44, 5, 23);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_Stats[2] >= 600);
                            MoveInfo.Text = "Special tech. INT should be over 600 to unlock.";
                            break;

                        default:
                            MoveName.Text = "No Move"; MoveName.ForeColor = Color.Black;
                            MoveGuts.Text = "---";
                            MoveDamage.Text = "---";
                            MoveHit.Text = "---";
                            MoveGD.Text = "---";
                            MoveSharp.Text = "---";
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = "---";
                            MoveUnlocked.Checked = false;
                            CanUnlock.Checked = false;
                            MoveInfo.Text = "Click on a move to learn more.";
                            break;
                    }
                    break;
                case 7: // Gollum                    
                    switch (MoveSelected)
                    {
                        case 1:
                            MoveName.Text = "Heavy Punch";
                            LazyDataFill(false, 16, 28, -10, 5, 0, 0);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_MoveUsed[6] >= 30);
                            MoveInfo.Text = "Basic tech. Chains from 30 Punch. No stat requirement.";
                            break;

                        case 2:
                            MoveName.Text = "Kick";
                            LazyDataFill(false, 12, 24, -14, 0, 5, 1);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = true;
                            MoveInfo.Text = "Starting basic tech. Chains into Heavy Kick when used 30x.";
                            break;

                        case 3:
                            MoveName.Text = "Heavy Kick";
                            LazyDataFill(false, 18, 34, -15, 5, 6, 2);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_MoveUsed[1] >= 30);
                            MoveInfo.Text = "Basic tech. Chains from 30 Kick.\r\nMany Golem subs can't learn this at Torble Sea.";
                            break;

                        case 4:
                            MoveName.Text = "Slap";
                            LazyDataFill(false, 17, 22, 0, 5, 5, 3);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_Stats[1] + Mon_Stats[3] >= 400);
                            MoveInfo.Text = "Hit tech. POW + SKI should total over 400 to unlock.\r\nChains into Heavy Slap when used 50x.";
                            break;

                        case 5:
                            MoveName.Text = "Uppercut";
                            LazyDataFill(false, 21, 45, -18, 5, 5, 4);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_Stats[1] >= 250);
                            MoveInfo.Text = "Heavy tech. POW should be over 250 to unlock.\r\nLearning priority over Palm Strike";
                            break;

                        case 6:
                            MoveName.Text = "Thwack";
                            LazyDataFill(false, 17, 21, -12, 19, 5, 5);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_Stats[1] + Mon_Stats[2] >= 400);
                            MoveInfo.Text = "Withering tech. POW + INT should total over 400 to unlock.\r\nChains into Smash Thwack when used 50x.";
                            break;

                        case 7:
                            MoveName.Text = "Punch";
                            LazyDataFill(false, 10, 19, -9, 0, 0, 6);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = true;
                            MoveInfo.Text = "Starting basic tech. Chains into Heavy Punch when used 30x.";
                            break;

                        case 8:
                            MoveName.Text = "Brow Hit";
                            LazyDataFill(false, 18, 29, -7, 10, 25, 7);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_Stats[1] + Mon_Stats[4] >= 400);
                            MoveInfo.Text = "Sharp tech. POW + SPD should total over 400 to unlock. \r\nChains into Brow Smash when used 50x. Learning priority over Clap Attack";
                            break;

                        case 9:
                            MoveName.Text = "Smash Thwack";
                            LazyDataFill(false, 21, 23, -13, 35, 5, 8);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_MoveUsed[5] >= 50 && Mon_Nature <= -50);
                            MoveInfo.Text = "Withering tech. Chains from 50 Thwack. Requires Bad (-50) Nature.";
                            break;

                        case 10:
                            MoveName.Text = "Clap Attack";
                            LazyDataFill(false, 26, 33, -12, 13, 18, 9);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_Stats[1] + Mon_Stats[4] >= 650);
                            MoveInfo.Text = "Sharp tech. POW + SPD should total over 650 to unlock.\r\nChains into Giant Clap when used 50x.";
                            break;

                        case 11:
                            MoveName.Text = "Palm Strike";
                            LazyDataFill(false, 24, 31, -5, 12, 5, 10);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_Stats[1] >= 450);
                            MoveInfo.Text = "Heavy tech. POW should be over 450 to unlock. Chains into Double Palms when used 50x.\r\nLearning priority over Diving Press";
                            break;

                        case 12:
                            MoveName.Text = "Double Palms";
                            LazyDataFill(false, 33, 47, -8, 15, 5, 11);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_MoveUsed[10] >= 50);
                            MoveInfo.Text = "Heavy tech. Chains from 50 Palm Strike. No stat requirement.";
                            break;

                        case 13:
                            MoveName.Text = "Heavy Slap";
                            LazyDataFill(false, 23, 26, 2, 9, 5, 12);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_MoveUsed[3] >= 50);
                            MoveInfo.Text = "Hit tech. Chains from 50 Slap. No stat requirement.";
                            break;

                        case 14:
                            MoveName.Text = "Diving Press";
                            LazyDataFill(false, 30, 65, -20, 17, 15, 13);
                            MoveSH.Text = GenerateStatValue(0, 20);
                            MoveSH.Show();
                            SHLabel.Text = "Self-Damage (Miss):";
                            SHLabel.Show();
                            CanUnlock.Checked = (Mon_Stats[1] + Mon_Stats[5] >= 650);
                            MoveInfo.Text = "Heavy tech. POW + DEF should total over 650 to unlock.";
                            break;

                        case 15:
                            MoveName.Text = "Charge";
                            LazyDataFill(false, 26, 30, 1, 11, 5, 14);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_Stats[1] + Mon_Stats[3] >= 650);
                            MoveInfo.Text = "Hit tech. POW + SKI should total over 650 to unlock.";
                            break;

                        case 16:
                            MoveName.Text = "Roll Assault";
                            LazyDataFill(false, 50, 64, -16, 38, 10, 15);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_Stats[1] >= 600);
                            MoveInfo.Text = "Special tech. POW should be over 600 to unlock.";
                            break;

                        case 17:
                            MoveName.Text = "Brow Smash";
                            LazyDataFill(false, 28, 44, -16, 17, 30, 18);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_MoveUsed[7] >= 50);
                            MoveInfo.Text = "Sharp tech. Chains from 50 Brow Hit. No stat requirement.";
                            break;

                        case 18:
                            MoveName.Text = "Earthquake";
                            LazyDataFill(true, 28, 27, -11, 39, 5, 19);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_Stats[2] >= 450);
                            MoveInfo.Text = "Withering tech. INT should be over 450 to unlock.";
                            break;

                        case 19:
                            MoveName.Text = "Giant Clap";
                            LazyDataFill(false, 31, 35, -15, 30, 19, 20);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_MoveUsed[9] >= 50);
                            MoveInfo.Text = "Sharp tech. Chains from 50 Clap Attack. No stat requirement.";
                            break;

                        case 20:
                            MoveName.Text = "Fist Shot";
                            LazyDataFill(false, 50, 38, -5, 38, 15, 21);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_Stats[2] + Mon_Stats[4] >= 800 && Mon_Nature >= 20);
                            MoveInfo.Text = "Special tech. INT + SPD (lolwut) should total over 800 to unlock.\r\nChains into Fist Missile when used 50x. Requires Good (+20) Nature.";
                            break;

                        case 21:
                            MoveName.Text = "Fist Missile";
                            LazyDataFill(false, 55, 45, -3, 45, 15, 22);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_MoveUsed[21] >= 50 && Mon_Nature >= 50);
                            MoveInfo.Text = "Special tech. Chains from 50 Fist Shot.\r\nRequires Good (+50) Nature.";
                            break;

                        case 22:
                            MoveName.Text = "Cyclone";
                            LazyDataFill(false, 50, 78, -16, 21, 10, 23);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_Stats[1] >= 800);
                            MoveInfo.Text = "Special tech. POW should be over 800 to unlock.";
                            break;

                        default:
                            MoveName.Text = "No Move"; MoveName.ForeColor = Color.Black;
                            MoveGuts.Text = "---";
                            MoveDamage.Text = "---";
                            MoveHit.Text = "---";
                            MoveGD.Text = "---";
                            MoveSharp.Text = "---";
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = "---";
                            MoveUnlocked.Checked = false;
                            CanUnlock.Checked = false;
                            MoveInfo.Text = "Click on a move to learn more.";
                            break;
                    }
                    break;

                case 8: // Zuum                    
                    switch (MoveSelected)
                    {
                        case 1:
                            MoveName.Text = "Claw";
                            LazyDataFill(false, 10, 10, 0, 0, 0, 0);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = true;
                            MoveInfo.Text = "Starting basic tech. Chains into MillionClaws when used 30x.";
                            break;

                        case 2:
                            MoveName.Text = "MillionClaws";
                            LazyDataFill(false, 15, 16, 5, 5, 0, 1);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_MoveUsed[0] >= 30);
                            MoveInfo.Text = "Basic tech. Chains from 30 Claw. Chains into Claw Combo when used 30x.\r\nLearning priority over Tail Lash. No stat requirement.";
                            break;

                        case 3:
                            MoveName.Text = "Bite";
                            LazyDataFill(false, 18, 25, -9, 5, 5, 2);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_Stats[1] >= 250);
                            MoveInfo.Text = "Heavy tech. POW should be over 250 to unlock.\r\nChains into MillionBites when used 50x. Learning priority over Fire Ball.";
                            break;

                        case 4:
                            MoveName.Text = "Bite-Throw";
                            LazyDataFill(false, 32, 41, -12, 19, 5, 3);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_MoveUsed[5] >= 50);
                            MoveInfo.Text = "Heavy tech. Chains from 50 MillionBites. No stat requirement.";
                            break;

                        case 5:
                            MoveName.Text = "Claw Combo";
                            LazyDataFill(false, 23, 22, 4, 10, 5, 4);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_MoveUsed[1] >= 30);
                            MoveInfo.Text = "Basic tech. Chains from 30 MillionClaws. No stat requirement.\r\nLearning priority over Tail Lash.";
                            break;

                        case 6:
                            MoveName.Text = "MillionBites";
                            LazyDataFill(false, 26, 34, -10, 10, 5, 5);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_MoveUsed[2] >= 50);
                            MoveInfo.Text = "Heavy tech. Chains from 50 Bite. Chains into Bite-Throw when used 50x.\r\nNo stat requirement.";
                            break;

                        case 7:
                            MoveName.Text = "Tail";
                            LazyDataFill(false, 12, 13, 9, 0, 5, 6);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = true;
                            MoveInfo.Text = "Starting basic tech. Chains into Tail Lash when used 30x.\r\nChains into Tail Combo when used 25x.";
                            break;

                        case 8:
                            MoveName.Text = "Tail Lash";
                            LazyDataFill(false, 20, 18, 8, 5, 5, 7);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_MoveUsed[6] >= 30);
                            MoveInfo.Text = "Basic tech. Chains from 30 Tail. Chains into Tail Lashes when used 30x.\r\nNo stat requirement.";
                            break;

                        case 9:
                            MoveName.Text = "Tail Lashes";
                            LazyDataFill(false, 25, 24, 7, 5, 10, 8);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_MoveUsed[7] >= 30);
                            MoveInfo.Text = "Basic tech. Chains from 30 Tail Lash. No stat requirement.";
                            break;

                        case 10:
                            MoveName.Text = "Dust Cloud";
                            LazyDataFill(true, 27, 8, -5, 39, 5, 9);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_Stats[2] >= 450 && Mon_Nature <= -20);
                            MoveInfo.Text = "Withering tech. INT should be over 450 to unlock.\r\nRequires Bad (-20) Nature.";
                            break;

                        case 11:
                            MoveName.Text = "Hypnotism";
                            LazyDataFill(true, 16, 7, 0, 22, 5, 10);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_Stats[2] >= 250);
                            MoveInfo.Text = "Withering tech. INT should be over 250 to unlock.\r\nLearning priority over Dust Cloud.";
                            break;

                        case 12:
                            MoveName.Text = "Tail Combo";
                            LazyDataFill(false, 19, 19, 10, 5, 5, 11);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_Stats[1] + Mon_Stats[3] >= 400 && Mon_MoveUsed[6] >= 25);
                            MoveInfo.Text = "Hit tech. POW + SKI should total over 400 to unlock.\r\nChains from 25 Tail. Learning priority over MillionClaws.";
                            break;

                        case 13:
                            MoveName.Text = "Jumping Claw";
                            LazyDataFill(false, 16, 13, -1, 8, 18, 12);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_Stats[1] + Mon_Stats[4] >= 400);
                            MoveInfo.Text = "Sharp tech. POW + SPD should total over 400 to unlock.\r\nChains into Diving Claw when used 50x. Learning priority over Charge.";
                            break;

                        case 14:
                            MoveName.Text = "Diving Claw";
                            LazyDataFill(false, 22, 19, -3, 10, 18, 13);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_MoveUsed[12] >= 50);
                            MoveInfo.Text = "Sharp tech. Chains from 50 Jumping Claw. \r\nChains into Aerial Claw when used 50x. No stat requirement.";
                            break;

                        case 15:
                            MoveName.Text = "Aerial Claw";
                            LazyDataFill(false, 36, 31, -5, 17, 20, 14);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_MoveUsed[13] >= 50);
                            MoveInfo.Text = "Special tech. Chains from 50 Diving Claw. No stat requirement.";
                            break;

                        case 16:
                            MoveName.Text = "Fire Ball";
                            LazyDataFill(true, 28, 35, -9, 12, 5, 15);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_Stats[1] + Mon_Stats[2] >= 650);
                            MoveInfo.Text = "Heavy tech. POW + INT should total over 650 to unlock.\r\nChains into Jumping Fire when used 50x.";
                            break;

                        case 17:
                            MoveName.Text = "Fire Breath";
                            LazyDataFill(true, 37, 49, -13, 19, 10, 16);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_MoveUsed[17] >= 50);
                            MoveInfo.Text = "Special tech. Chains from 50 Jumping Fire. No stat requirement.";
                            break;

                        case 18:
                            MoveName.Text = "Jumping Fire";
                            LazyDataFill(true, 32, 43, -13, 16, 5, 17);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_MoveUsed[15] >= 50);
                            MoveInfo.Text = "Heavy tech. Chains from 50 Fire Ball. \r\nChains into Fire Breath when used 50x. No stat requirement.";
                            break;

                        case 19:
                            MoveName.Text = "Charge";
                            LazyDataFill(false, 24, 20, -6, 13, 20, 18);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_Stats[1] + Mon_Stats[4] >= 650 && Mon_Nature >= 20);
                            MoveInfo.Text = "Sharp tech. POW + SPD should total over 650 to unlock.\r\nChains into Fire Charge when used 50x. Requires Good (+20) Nature.";
                            break;

                        case 20:
                            MoveName.Text = "Fire Charge";
                            LazyDataFill(false, 30, 25, -8, 25, 25, 19);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_MoveUsed[18] >= 50 && Mon_Nature >= 50);
                            MoveInfo.Text = "Sharp tech. Chains from 50 Charge. No stat requirement.\r\nRequires Good (+50) Nature.";
                            break;

                        case 21:
                            MoveName.Text = "Roll Assault";
                            LazyDataFill(false, 50, 37, 2, 26, 10, 20);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_Stats[1] + Mon_Stats[5] >= 800);
                            MoveInfo.Text = "Special tech. POW + DEF should total over 800 to unlock.\r\nChains into Burning Roll when used 50x. Learning priority over Five Balls.";
                            break;

                        case 22:
                            MoveName.Text = "Five Balls";
                            LazyDataFill(true, 45, 45, -10, 25, 15, 21);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_Stats[2] + Mon_Stats[4] >= 800);
                            MoveInfo.Text = "Special tech. INT + SPD should total over 800 to unlock.\r\nChains into Fire Bomb when used 50x. Learning priority over Burning Roll.";
                            break;

                        case 23:
                            MoveName.Text = "Fire Bomb";
                            LazyDataFill(true, 50, 59, -15, 33, 25, 22);
                            MoveSH.Text = GenerateStatValue(0, 15);
                            MoveSH.Show();
                            SHLabel.Text = "Self-Damage:";
                            SHLabel.Show();
                            CanUnlock.Checked = (Mon_Stats[5] >= 450 && Mon_MoveUsed[21] >= 50);
                            MoveInfo.Text = "Special tech. DEF should be over 450 to unlock. Chains from 50 Five Balls.";
                            break;

                        case 24:
                            MoveName.Text = "Burning Roll";
                            LazyDataFill(false, 55, 47, -2, 32, 10, 23);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_Stats[2] >= 450 && Mon_MoveUsed[20] >= 50);
                            MoveInfo.Text = "Special tech. INT should be over 450 to unlock. Chains from 50 Roll Assault.\r\nLearning priority over Aerial Claw.";
                            break;

                        default:
                            MoveName.Text = "No Move"; MoveName.ForeColor = Color.Black;
                            MoveGuts.Text = "---";
                            MoveDamage.Text = "---";
                            MoveHit.Text = "---";
                            MoveGD.Text = "---";
                            MoveSharp.Text = "---";
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = "---";
                            MoveUnlocked.Checked = false;
                            CanUnlock.Checked = false;
                            MoveInfo.Text = "Click on a move to learn more.";
                            break;
                    }

                    break;

                case 9: // Durahan
                        //{
                    switch (MoveSelected)
                    {
                        case 1:
                            MoveName.Text = "Swing";
                            LazyDataFill(false, 18, 22, -4, 5, 8, 0);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_MoveUsed[6] >= 30);
                            MoveInfo.Text = "Basic tech. Chains from 30 Cut-In-Two. No stat requirement.";
                            break;

                        case 2:
                            MoveName.Text = "TwisterSlash";
                            LazyDataFill(false, 19, 30, -12, 5, 5, 1);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_Stats[1] >= 250 && (Mon_SubGenus != 1 && Mon_SubGenus != 11 && Mon_SubGenus != 39 && Mon_SubGenus != 40));
                            MoveInfo.Text = "Heavy tech. POW should be over 250 to unlock. Learning priority over Lightning.\r\nVesuvius, Hound Knight, Ruby Knight, Kokushi Muso cannot learn.";
                            break;

                        case 3:
                            MoveName.Text = "Thunderbolt";
                            LazyDataFill(false, 32, 57, -17, 15, 5, 2);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_MoveUsed[19] >= 50 && Mon_Nature <= -50);
                            MoveInfo.Text = "Heavy tech. Chains from 50 Lightning. No stat requirement.\r\nRequires Bad (-50) Nature.";
                            break;

                        case 4:
                            MoveName.Text = "Flash Slash";
                            LazyDataFill(false, 18, 18, -1, 5, 15, 3);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_Stats[1] + Mon_Stats[4] >= 400);
                            MoveInfo.Text = "Sharp tech. POW + SPD should total over 400 to unlock.\r\nLearning priority over Jumping Stab.";
                            break;

                        case 5:
                            MoveName.Text = "Triple Slash";
                            LazyDataFill(false, 40, 39, -5, 18, 15, 4);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_Stats[1] + Mon_Stats[4] >= 800);
                            MoveInfo.Text = "Special tech. POW + SPD should total over 800 to unlock.\r\nLearning priority over Punch Combo.";
                            break;

                        case 6:
                            MoveName.Text = "Slash Combo";
                            LazyDataFill(false, 35, 60, -15, 15, 5, 5);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_Stats[1] >= 450);
                            MoveInfo.Text = "Heavy tech. POW should be over 450 to unlock.";
                            break;

                        case 7:
                            MoveName.Text = "Cut-In-Two";
                            LazyDataFill(false, 12, 16, -2, 0, 5, 6);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = true;
                            MoveInfo.Text = "Starting basic tech. Chains into Swing when used 30x.";
                            break;

                        case 8:
                            MoveName.Text = "MillionStabs";
                            LazyDataFill(false, 27, 22, 10, 12, 5, 7);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_Stats[1] + Mon_Stats[3] >= 400);
                            MoveInfo.Text = "Hit tech. POW + SKI should total over 400 to unlock.\r\nLearning priority over Air Shot.";
                            break;

                        case 9:
                            MoveName.Text = "Punch Combo";
                            LazyDataFill(false, 45, 35, 11, 20, 5, 8);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_Stats[1] + Mon_Stats[3] >= 800 && Mon_Nature <= -20);
                            MoveInfo.Text = "Special tech. POW + SKI should total over 800 to unlock.\r\nRequires Bad (-20) Nature.";
                            break;

                        case 10:
                            MoveName.Text = "DeathBringer";
                            LazyDataFill(false, 50, 69, -18, 41, 5, 9);
                            MoveSH.Text = GenerateStatValue(0, 20);
                            MoveSH.Show();
                            SHLabel.Text = "Self-Damage:";
                            SHLabel.Show();
                            CanUnlock.Checked = (Mon_Stats[1] + Mon_Stats[5] >= 800);
                            MoveInfo.Text = "Special tech. POW + DEF should total over 800 to unlock.";
                            break;

                        case 11:
                            MoveName.Text = "Kick Combo";
                            LazyDataFill(false, 19, 25, -5, 5, 5, 10);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_Stats[1] >= 250 && (Mon_SubGenus == 1 | Mon_SubGenus == 11 | Mon_SubGenus == 40));
                            MoveInfo.Text = "Heavy tech. POW should be over 250 to unlock.\r\nVesuvius, Hound Knight, Kokushi Muso only";
                            break;

                        case 12:
                            MoveName.Text = "V Slash";
                            LazyDataFill(false, 21, 25, -3, 10, 15, 11);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_Stats[1] >= 250 && Mon_SubGenus == 39);
                            MoveInfo.Text = "Heavy tech. POW should be over 250 to unlock.\r\nExclusive to Ruby Knight.";
                            break;

                        case 13:
                            MoveName.Text = "Rush Slash";
                            LazyDataFill(false, 10, 14, 5, 0, 5, 12);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = true;
                            MoveInfo.Text = "Starting basic tech. Chains into Dash Slash when used 30x.";
                            break;

                        case 14:
                            MoveName.Text = "Dash Slash";
                            LazyDataFill(false, 17, 20, 5, 0, 6, 13);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_MoveUsed[12] >= 30);
                            MoveInfo.Text = "Basic tech. Chains from 30 Rush Slash. No stat requirement.";
                            break;

                        case 15:
                            MoveName.Text = "Charge";
                            LazyDataFill(false, 16, 13, 15, 5, 5, 14);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_Stats[1] + Mon_Stats[3] >= 250 && (Mon_SubGenus != 0 && Mon_SubGenus != 26 && Mon_SubGenus != 38));
                            MoveInfo.Text = "Hit tech. POW + SKI should total over 250 to unlock.\r\nLeziena, Genocider, Shogun cannot learn.";
                            break;

                        case 16:
                            MoveName.Text = "Air Shot";
                            LazyDataFill(true, 30, 17, 17, 17, 5, 15);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_Stats[2] + Mon_Stats[3] >= 650 && Mon_Nature >= 20);
                            MoveInfo.Text = "Hit tech. INT + SKI should total over 650 to unlock.\r\nChains into Blast Shot when used 50x. Requires Good (+20) Nature.";
                            break;

                        case 17:
                            MoveName.Text = "Jumping Stab";
                            LazyDataFill(false, 26, 22, 0, 8, 22, 16);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_Stats[1] + Mon_Stats[4] >= 650 && Mon_Nature >= 20);
                            MoveInfo.Text = "Sharp tech. POW + SPD should total over 650 to unlock.\r\nRequires Good (+20) Nature.";
                            break;

                        case 18:
                            MoveName.Text = "RollingSlash";
                            LazyDataFill(false, 28, 37, -9, 12, 5, 18);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_Stats[1] >= 450);
                            MoveInfo.Text = "Heavy tech. POW should be over 450 to unlock.\r\nLearning priority over Lightning.";
                            break;

                        case 19:
                            MoveName.Text = "Lightning";
                            LazyDataFill(true, 26, 43, -11, 11, 5, 19);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_Stats[1] + Mon_Stats[2] >= 650 && Mon_Nature <= -30);
                            MoveInfo.Text = "Heavy tech. POW + INT should total over 650 to unlock.\r\nRequires Bad (-30) Nature.";
                            break;

                        case 20:
                            MoveName.Text = "Blast Shot";
                            LazyDataFill(true, 40, 25, 14, 25, 5, 20);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_MoveUsed[15] >= 50 && Mon_Nature >= 50);
                            MoveInfo.Text = "Hit tech. Chains from 50 Air Shot. No stat requirement.\r\nRequires Good (+50) Nature.";
                            break;

                        case 21:
                            MoveName.Text = "Sword Throw";
                            LazyDataFill(true, 25, 27, -13, 12, 25, 21);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_Stats[2] + Mon_Stats[4] >= 650);
                            MoveInfo.Text = "Sharp tech. INT + SPD should total over 650 to unlock.\r\nLearning priority over Jumping Stab.";
                            break;

                        case 22:
                            MoveName.Text = "Gust Slash";
                            LazyDataFill(true, 19, 16, 20, 5, 5, 22);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_Stats[2] + Mon_Stats[3] >= 400 && (Mon_SubGenus == 0 | Mon_SubGenus == 26 | Mon_SubGenus == 38));
                            MoveInfo.Text = "Hit tech. INT + SKI should total over 400 to unlock.\r\nLeziena, Genocider, Shogun only.";
                            break;

                        default:
                            MoveName.Text = "No Move"; MoveName.ForeColor = Color.Black;
                            MoveGuts.Text = "---";
                            MoveDamage.Text = "---";
                            MoveHit.Text = "---";
                            MoveGD.Text = "---";
                            MoveSharp.Text = "---";
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = "---";
                            MoveUnlocked.Checked = false;
                            CanUnlock.Checked = false;
                            MoveInfo.Text = "Click on a move to learn more.";
                            break;
                    }

                    break;

                case 10: // Arrow Head
                    switch (MoveSelected)
                    {
                        case 1:
                            MoveName.Text = "Punch";
                            LazyDataFill(false, 10, 14, 9, 0, 0, 0);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = true;
                            MoveInfo.Text = "Starting basic tech. Chains into Long Punch when used 30x.";
                            break;

                        case 2:
                            MoveName.Text = "Claw Pinch";
                            LazyDataFill(false, 29, 39, -8, 15, 6, 1);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_Stats[1] >= 450);
                            MoveInfo.Text = "Heavy tech. POW should be over 450 to unlock.";
                            break;

                        case 3:
                            MoveName.Text = "Bloodsuction";
                            LazyDataFill(false, 50, 30, -10, 0, 0, 2);
                            MoveSH.Text = "100%";
                            MoveSH.Show();
                            SHLabel.Text = "HP Drain:";
                            SHLabel.Show();
                            CanUnlock.Checked = (Mon_Stats[1] >= 600 && Mon_Nature <= -50);
                            MoveInfo.Text = "Special tech. POW should be over 600 to unlock.\r\nRequires Bad (-50) Nature.";
                            break;

                        case 4:
                            MoveName.Text = "Somersault";
                            LazyDataFill(false, 26, 22, 3, 10, 21, 3);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_Stats[1] + Mon_Stats[4] >= 650 && Mon_Nature >= 20);
                            MoveInfo.Text = "Sharp tech. POW + SPD should total over 650 to unlock.\r\nChains into Somersaults when used 50x. Requires Good (+20) Nature.";
                            if (rng.Next(100) < 10)
                                MoveInfo.Text += " 🠫 ↑+K";
                            break;

                        case 5:
                            MoveName.Text = "Somersaults";
                            LazyDataFill(false, 50, 44, 3, 20, 25, 4);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_MoveUsed[3] >= 50 && Mon_Nature >= 50);
                            MoveInfo.Text = "Special tech. Chains from 50 Somersault. No stat requirement.\r\nRequires Good (+50) Nature.";
                            break;

                        case 6:
                            MoveName.Text = "Sting Slash";
                            LazyDataFill(false, 30, 18, 10, 15, 25, 5);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_Stats[1] + Mon_Stats[4] >= 650 && Mon_SubGenus == 9);
                            MoveInfo.Text = "Sharp tech. POW + SPD should total over 650 to unlock.\r\nExclusive to Plated Arrow.";
                            break;

                        case 7:
                            MoveName.Text = "Long Punch";
                            LazyDataFill(false, 19, 20, 8, 7, 3, 6);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_MoveUsed[0] >= 30);
                            MoveInfo.Text = "Basic tech. Chains from 50 Punch. No stat requirement.";
                            break;

                        case 8:
                            MoveName.Text = "Sting";
                            LazyDataFill(true, 16, 15, 12, 6, 5, 7);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_Stats[2] + Mon_Stats[3] >= 400);
                            MoveInfo.Text = "Hit tech. INT + SKI should total over 400 to unlock.\r\nChains into TripleStings when used 50x. Learning priority over Hidden Sting.";
                            break;

                        case 9:
                            MoveName.Text = "TripleStings";
                            LazyDataFill(true, 29, 23, 15, 12, 8, 8);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_MoveUsed[7] >= 50);
                            MoveInfo.Text = "Hit tech. Chains from 50 Sting. No stat requirement.";
                            break;

                        case 10:
                            MoveName.Text = "Tail Swing";
                            LazyDataFill(false, 16, 24, -11, 9, 5, 9);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_Stats[1] >= 250);
                            MoveInfo.Text = "Heavy tech. POW should be over 250 to unlock.\r\nChains into Tail Swings when used 50x. Learning priority over Claw Pinch.";
                            break;

                        case 11:
                            MoveName.Text = "Tail Swings";
                            LazyDataFill(false, 23, 36, -13, 12, 6, 10);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_MoveUsed[9] >= 50);
                            MoveInfo.Text = "Heavy tech. Chains from 50 Tail Swing. No stat requirement.";
                            break;

                        case 12:
                            MoveName.Text = "Death Scythe";
                            LazyDataFill(false, 30, 18, 7, 15, 28, 11);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_Stats[1] + Mon_Stats[4] >= 650 && Mon_SubGenus == 26);
                            MoveInfo.Text = "Sharp tech. POW + SPD should total over 650 to unlock.\r\nExclusive to Selketo.";
                            break;

                        case 13:
                            MoveName.Text = "Claw Assault";
                            LazyDataFill(false, 12, 17, 3, 0, 5, 12);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = true;
                            MoveInfo.Text = "Starting basic tech.";
                            break;

                        case 14:
                            MoveName.Text = "Jumping Claw";
                            LazyDataFill(false, 16, 15, 0, 8, 17, 13);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_Stats[1] + Mon_Stats[4] >= 400);
                            MoveInfo.Text = "Sharp tech. POW + SPD should total over 400 to unlock.\r\nChains into Aerial Claw when used 50x. Learning priority over Somersault.";
                            break;

                        case 15:
                            MoveName.Text = "Aerial Claw";
                            LazyDataFill(false, 21, 19, -1, 11, 19, 14);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_MoveUsed[13] >= 50);
                            MoveInfo.Text = "Sharp tech. Chains from 50 Jumping Claw. No stat requirement.";
                            break;

                        case 16:
                            MoveName.Text = "Acrobatics";
                            LazyDataFill(false, 45, 29, 13, 33, 5, 15);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_Stats[1] + Mon_Stats[3] >= 800);
                            MoveInfo.Text = "Special tech. POW + SKI should total over 800 to unlock.";
                            break;

                        case 17:
                            MoveName.Text = "Meteor";
                            LazyDataFill(true, 45, 61, -14, 27, 5, 16);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_Stats[2] >= 600);
                            MoveInfo.Text = "Special tech. INT should be over 600 to unlock.";
                            break;

                        case 18:
                            MoveName.Text = "Cyclone";
                            LazyDataFill(false, 50, 62, -16, 29, 10, 17);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_Stats[1] >= 600 && Mon_SubGenus == 7);
                            MoveInfo.Text = "Special tech. POW should be over 600 to unlock.\r\nExclusive to Priarocks.";
                            break;

                        case 19:
                            MoveName.Text = "Hidden Sting";
                            LazyDataFill(false, 27, 24, 19, 6, 5, 18);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_Stats[2] + Mon_Stats[3] >= 650);
                            MoveInfo.Text = "Hit tech. INT + SKI (not a typo) should total over 650 to unlock.";
                            break;

                        case 20:
                            MoveName.Text = "Energy Shot";
                            LazyDataFill(true, 18, 7, 5, 27, 5, 19);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_Stats[2] >= 250);
                            MoveInfo.Text = "Withering tech. INT should be over 250 to unlock.\r\nChains into Energy Shots when used 50x.";
                            break;

                        case 21:
                            MoveName.Text = "Energy Shots";
                            LazyDataFill(true, 25, 11, 5, 35, 5, 20);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_MoveUsed[19] >= 50);
                            MoveInfo.Text = "Withering tech. Chains from 50 Energy Shot. No stat requirement.";
                            break;

                        case 22:
                            MoveName.Text = "Javelin";
                            LazyDataFill(false, 24, 20, -7, 23, 20, 21);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_Stats[2] + Mon_Stats[4] >= 650 && Mon_Nature <= -20 && Mon_SubGenus != 5 && Mon_SubGenus != 9);
                            MoveInfo.Text = "Sharp tech. INT + SPD should total over 650 to unlock. Requires Bad (-20) Nature.\r\nRenocraft, Plated Arrow cannot learn.";
                            break;

                        case 23:
                            MoveName.Text = "Roll Assault";
                            LazyDataFill(false, 32, 50, -9, 8, 5, 22);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_Stats[1] >= 600 && Mon_Nature <= -30);
                            MoveInfo.Text = "Heavy tech. POW should be over 600 to unlock. Requires Bad (-30) Nature.";
                            break;

                        case 24:
                            MoveName.Text = "Fist Missile";
                            LazyDataFill(false, 19, 16, 5, 5, 19, 23);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_Stats[1] + Mon_Stats[4] >= 800 && Mon_SubGenus == 5);
                            MoveInfo.Text = "Special tech. POW + SPD should total over 800 to unlock.\r\nExclusive to Renocraft.";
                            break;

                        default:
                            MoveName.Text = "No Move"; MoveName.ForeColor = Color.Black;
                            MoveGuts.Text = "---";
                            MoveDamage.Text = "---";
                            MoveHit.Text = "---";
                            MoveGD.Text = "---";
                            MoveSharp.Text = "---";
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = "---";
                            MoveUnlocked.Checked = false;
                            CanUnlock.Checked = false;
                            MoveInfo.Text = "Click on a move to learn more.";
                            break;
                    }
                    break;

                case 11: // Tiger
                    switch (MoveSelected)
                    {
                        case 1:
                            MoveName.Text = "Bite"; MoveName.ForeColor = PowCol;
                            MoveGuts.Text = "11";
                            MoveDamage.Text = GenerateStatValue(0, 12);
                            MoveHit.Text = GenerateStatValue(1, 4);
                            MoveGD.Text = "---";
                            MoveSharp.Text = GenerateStatValue(3, 5);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[0].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[0] == 1);
                            CanUnlock.Checked = true;
                            PlaceHighlight(0);
                            MoveInfo.Text = "Starting basic tech.";
                            break;
                        case 2:
                            MoveName.Text = "Bolt"; MoveName.ForeColor = IntCol;
                            MoveGuts.Text = "18";
                            MoveDamage.Text = GenerateStatValue(0, 9);
                            MoveHit.Text = GenerateStatValue(1, -2);
                            MoveGD.Text = GenerateStatValue(2, 16);
                            MoveSharp.Text = GenerateStatValue(3, 15);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[1].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[1] == 1);
                            CanUnlock.Checked = (Mon_Stats[2] >= 250);
                            PlaceHighlight(1);
                            MoveInfo.Text = "Withering tech. INT should be over 250 to unlock.";
                            break;
                        case 3:
                            MoveName.Text = "Scratch"; MoveName.ForeColor = PowCol;
                            MoveGuts.Text = "10";
                            MoveDamage.Text = GenerateStatValue(0, 12);
                            MoveHit.Text = GenerateStatValue(1, -4);
                            MoveGD.Text = "---";
                            MoveSharp.Text = GenerateStatValue(3, 5);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[6].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[6] == 1);
                            CanUnlock.Checked = true;
                            PlaceHighlight(6);
                            MoveInfo.Text = "Starting basic tech.";
                            break;
                        case 4:
                            MoveName.Text = "One-Two"; MoveName.ForeColor = PowCol;
                            MoveGuts.Text = "18";
                            MoveDamage.Text = GenerateStatValue(0, 19);
                            MoveHit.Text = GenerateStatValue(1, -9); //bedeg
                            MoveGD.Text = GenerateStatValue(2, 5);
                            MoveSharp.Text = GenerateStatValue(3, 20);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[7].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[7] == 1);
                            CanUnlock.Checked = (Mon_Stats[1] >= 250);
                            PlaceHighlight(7);
                            MoveInfo.Text = "Heavy tech. POW should be over 250 to unlock.";
                            break;
                        case 5:
                            MoveName.Text = "Lightning"; MoveName.ForeColor = IntCol;
                            MoveGuts.Text = "29";
                            MoveDamage.Text = GenerateStatValue(0, 12);
                            MoveHit.Text = GenerateStatValue(1, -5);
                            MoveGD.Text = GenerateStatValue(2, 34);
                            MoveSharp.Text = GenerateStatValue(3, 15);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[8].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[8] == 1);
                            CanUnlock.Checked = (Mon_Stats[2] >= 450);
                            PlaceHighlight(8);
                            MoveInfo.Text = "Withering tech. INT should be over 450 to unlock.";
                            break;
                        case 6:
                            MoveName.Text = "Charge"; MoveName.ForeColor = PowCol;
                            MoveGuts.Text = "15";
                            MoveDamage.Text = GenerateStatValue(0, 9);
                            MoveHit.Text = GenerateStatValue(1, 15);
                            MoveGD.Text = GenerateStatValue(2, 5);
                            MoveSharp.Text = GenerateStatValue(3, 15);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[12].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[12] == 1);
                            CanUnlock.Checked = (Mon_Stats[1] + Mon_Stats[3] >= 400);
                            PlaceHighlight(12);
                            MoveInfo.Text = "Hit tech. POW + SKI should total over 400 to unlock.";
                            break;
                        case 7:
                            MoveName.Text = "Combination"; MoveName.ForeColor = PowCol;
                            MoveGuts.Text = "25";
                            MoveDamage.Text = GenerateStatValue(0, 30); //bedeg
                            MoveHit.Text = GenerateStatValue(1, -13); //bedeg
                            MoveGD.Text = GenerateStatValue(2, 5); //bedeg
                            MoveSharp.Text = GenerateStatValue(3, 20); //bedeg
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[13].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[13] == 1);
                            CanUnlock.Checked = (Mon_Stats[1] >= 450);
                            PlaceHighlight(13);
                            MoveInfo.Text = "Heavy tech. POW should be over 450 to unlock.";
                            break;
                        case 8:
                            MoveName.Text = "Ice Bomb"; MoveName.ForeColor = IntCol;
                            MoveGuts.Text = "15";
                            MoveDamage.Text = GenerateStatValue(0, 15);
                            MoveHit.Text = GenerateStatValue(1, -3);
                            MoveGD.Text = "---";
                            MoveSharp.Text = GenerateStatValue(3, 30);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[14].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[14] == 1);
                            CanUnlock.Checked = (Mon_Stats[2] + Mon_Stats[4] >= 400);
                            PlaceHighlight(14);
                            MoveInfo.Text = "Sharp tech. INT + SPD should total over 400 to unlock.";
                            break;
                        case 9:
                            MoveName.Text = "Stab"; MoveName.ForeColor = PowCol; //bedeg
                            MoveGuts.Text = "45"; //bedeg
                            MoveDamage.Text = GenerateStatValue(0, 34);
                            MoveHit.Text = GenerateStatValue(1, 0);
                            MoveGD.Text = GenerateStatValue(2, 17);
                            MoveSharp.Text = GenerateStatValue(3, 25);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[15].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[15] == 1);
                            CanUnlock.Checked = (Mon_Stats[1] + Mon_Stats[4] >= 800);
                            PlaceHighlight(15);
                            MoveInfo.Text = "Special tech. POW + SPD should total over 800 to unlock.";
                            break;
                        case 10:
                            MoveName.Text = "Roll Assault"; MoveName.ForeColor = PowCol;
                            MoveGuts.Text = "23";
                            MoveDamage.Text = GenerateStatValue(0, 10);
                            MoveHit.Text = GenerateStatValue(1, 21);
                            MoveGD.Text = GenerateStatValue(2, 10);
                            MoveSharp.Text = GenerateStatValue(3, 20);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[18].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[18] == 1);
                            CanUnlock.Checked = (Mon_Stats[1] + Mon_Stats[3] >= 650);
                            PlaceHighlight(18);
                            MoveInfo.Text = "Hit tech. POW + SKI should total over 650 to unlock.";
                            break;
                        case 11:
                            MoveName.Text = "Blizzard"; MoveName.ForeColor = IntCol;
                            MoveGuts.Text = "32"; //bedeg
                            MoveDamage.Text = GenerateStatValue(0, 20);
                            MoveHit.Text = GenerateStatValue(1, 2);
                            MoveGD.Text = GenerateStatValue(2, 11);
                            MoveSharp.Text = GenerateStatValue(3, 35);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[19].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[19] == 1);
                            CanUnlock.Checked = (Mon_Stats[2] + Mon_Stats[4] >= 650);
                            PlaceHighlight(19);
                            MoveInfo.Text = "Sharp tech. INT + SPD should total over 650 to unlock.";
                            break;
                        case 12:
                            MoveName.Text = "Roar"; MoveName.ForeColor = IntCol;
                            MoveGuts.Text = "40";
                            MoveDamage.Text = GenerateStatValue(0, 31);
                            MoveHit.Text = GenerateStatValue(1, -15);
                            MoveGD.Text = GenerateStatValue(2, 40);
                            MoveSharp.Text = GenerateStatValue(3, 15);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[20].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[20] == 1);
                            CanUnlock.Checked = (Mon_Stats[2] >= 600);
                            PlaceHighlight(20);
                            MoveInfo.Text = "Special tech. INT should be over 600 to unlock.";
                            break;
                        default:
                            MoveName.Text = "No Move"; MoveName.ForeColor = Color.Black;
                            MoveGuts.Text = "---";
                            MoveDamage.Text = "---";
                            MoveHit.Text = "---";
                            MoveGD.Text = "---";
                            MoveSharp.Text = "---";
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = "---";
                            MoveUnlocked.Checked = false;
                            CanUnlock.Checked = false;
                            MoveInfo.Text = "Click on a move to learn more.";
                            break;
                    }
                    break;

                case 12: // Hopper
                    switch (MoveSelected)
                    {
                        case 1:
                            MoveName.Text = "Hook"; MoveName.ForeColor = PowCol;
                            MoveGuts.Text = "12";
                            MoveDamage.Text = GenerateStatValue(0, 12);
                            MoveHit.Text = GenerateStatValue(1, 0);
                            MoveGD.Text = "---";
                            MoveSharp.Text = GenerateStatValue(3, 5);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[0].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[0] == 1);
                            CanUnlock.Checked = true;
                            PlaceHighlight(0);
                            MoveInfo.Text = "Starting basic tech.";
                            break;
                        case 2:
                            MoveName.Text = "Jump Blow"; MoveName.ForeColor = PowCol;
                            MoveGuts.Text = "22";
                            MoveDamage.Text = GenerateStatValue(0, 15);
                            MoveHit.Text = GenerateStatValue(1, 1);
                            MoveGD.Text = GenerateStatValue(2, 5);
                            MoveSharp.Text = GenerateStatValue(3, 15);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[1].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[1] == 1);
                            CanUnlock.Checked = (Mon_Stats[1] >= 150 && Mon_Stats[4] >= 250); //bedeg
                            PlaceHighlight(1);
                            MoveInfo.Text = "Sharp tech. Chains into 2 Jump Blows when used 50x.\r\nPOW should be over 150 and SPD should be over 250 to unlock.";
                            break;
                        case 3:
                            MoveName.Text = "2 Jump Blows"; MoveName.ForeColor = PowCol;
                            MoveGuts.Text = "29";
                            MoveDamage.Text = GenerateStatValue(0, 20);
                            MoveHit.Text = GenerateStatValue(1, -3);
                            MoveGD.Text = GenerateStatValue(2, 5);
                            MoveSharp.Text = GenerateStatValue(3, 23);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[2].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[2] == 1);
                            CanUnlock.Checked = (Mon_MoveUsed[1] >= 50);
                            PlaceHighlight(2);
                            MoveInfo.Text = "Sharp tech. Chains from 50 Jump Blows. Chains into 3 Jump Blows when used 50x.";
                            break;
                        case 4:
                            MoveName.Text = "3 Jump Blows"; MoveName.ForeColor = PowCol;
                            MoveGuts.Text = "35";
                            MoveDamage.Text = GenerateStatValue(0, 30);
                            MoveHit.Text = GenerateStatValue(1, -9);
                            MoveGD.Text = GenerateStatValue(2, 14);
                            MoveSharp.Text = GenerateStatValue(3, 30);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[3].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[3] == 1);
                            CanUnlock.Checked = (Mon_MoveUsed[2] >= 50 && Mon_Nature >= 20);
                            PlaceHighlight(3);
                            MoveInfo.Text = "Special tech. Chains from 50 uses of 2 Jump Blows. Requires Good (+20) Nature.";
                            break;
                        case 5:
                            MoveName.Text = "1-2-JumpBlow"; MoveName.ForeColor = PowCol;
                            MoveGuts.Text = "33";
                            MoveDamage.Text = GenerateStatValue(0, 29);
                            MoveHit.Text = GenerateStatValue(1, -9);
                            MoveGD.Text = GenerateStatValue(2, 18);
                            MoveSharp.Text = GenerateStatValue(3, 7);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[4].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[4] == 1);
                            CanUnlock.Checked = (Mon_Stats[3] + Mon_Stats[4] >= 800); //bedeg
                            PlaceHighlight(4);
                            MoveInfo.Text = @"Special tech. Chains into Hopper Combo when used 50x.
SKI + SPD should total over 800 to unlock.";
                            break;
                        case 6:
                            MoveName.Text = "Hopper Combo"; MoveName.ForeColor = PowCol;
                            MoveGuts.Text = "44";
                            MoveDamage.Text = GenerateStatValue(0, 49);
                            MoveHit.Text = GenerateStatValue(1, -15);
                            MoveGD.Text = GenerateStatValue(2, 21);
                            MoveSharp.Text = GenerateStatValue(3, 8);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[5].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[5] == 1);
                            CanUnlock.Checked = (Mon_MoveUsed[4] >= 50);
                            PlaceHighlight(5);
                            MoveInfo.Text = "Special tech. Chains from 50 1-2-JumpBlows.";
                            break;
                        case 7:
                            MoveName.Text = "Jab"; MoveName.ForeColor = PowCol;
                            MoveGuts.Text = "10";
                            MoveDamage.Text = GenerateStatValue(0, 8);
                            MoveHit.Text = GenerateStatValue(1, 8);
                            MoveGD.Text = "---";
                            MoveSharp.Text = "---";
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[6].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[6] == 1);
                            CanUnlock.Checked = true;
                            PlaceHighlight(6);
                            MoveInfo.Text = "Starting basic tech.";
                            break;
                        case 8:
                            MoveName.Text = "Flick"; MoveName.ForeColor = PowCol;
                            MoveGuts.Text = "17";
                            MoveDamage.Text = GenerateStatValue(0, 11);
                            MoveHit.Text = GenerateStatValue(1, 11);
                            MoveGD.Text = GenerateStatValue(2, 5);
                            MoveSharp.Text = GenerateStatValue(3, 5);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[12].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[12] == 1);
                            CanUnlock.Checked = (Mon_Stats[1] + Mon_Stats[3] >= 400);
                            PlaceHighlight(12);
                            MoveInfo.Text = "Hit tech. Chains into Rapid Flick when used 50x. POW + SKI should total over 400 to unlock.";
                            break;
                        case 9:
                            MoveName.Text = "Phantom Claw"; MoveName.ForeColor = PowCol;
                            MoveGuts.Text = "25";
                            MoveDamage.Text = GenerateStatValue(0, 15);
                            MoveHit.Text = GenerateStatValue(1, 15);
                            MoveGD.Text = GenerateStatValue(2, 5);
                            MoveSharp.Text = GenerateStatValue(3, 5);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[13].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[13] == 1);
                            CanUnlock.Checked = (Mon_Stats[1] >= 150 && Mon_Stats[1] + Mon_Stats[3] >= 400 && Mon_SubGenus == 16); //bedeg
                            PlaceHighlight(13);
                            MoveInfo.Text = "Hit tech. POW should be over 150, and POW + SKI should total over 400 to unlock.\r\nExclusive to Mustachios.";
                            break;
                        case 10:
                            MoveName.Text = "Rapid Flick"; MoveName.ForeColor = PowCol;
                            MoveGuts.Text = "30";
                            MoveDamage.Text = GenerateStatValue(0, 17);
                            MoveHit.Text = GenerateStatValue(1, 17);
                            MoveGD.Text = GenerateStatValue(2, 7);
                            MoveSharp.Text = GenerateStatValue(3, 7);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[18].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[18] == 1);
                            CanUnlock.Checked = (Mon_MoveUsed[12] >= 50);
                            PlaceHighlight(18);
                            MoveInfo.Text = @"Hit tech. Chains from 50 Flicks. Chains into Flick Combo when used 50x.";
                            break;
                        case 11:
                            MoveName.Text = "Flick Combo"; MoveName.ForeColor = PowCol;
                            MoveGuts.Text = "38";
                            MoveDamage.Text = GenerateStatValue(0, 22);
                            MoveHit.Text = GenerateStatValue(1, 7);
                            MoveGD.Text = GenerateStatValue(2, 16);
                            MoveSharp.Text = GenerateStatValue(3, 11);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[19].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[19] == 1);
                            CanUnlock.Checked = (Mon_MoveUsed[18] >= 50 && Mon_Nature <= -20); //bedeg
                            PlaceHighlight(19);
                            MoveInfo.Text = "Special tech. Chains from 50 Rapid Flicks. Requires Bad (-20) Nature.";
                            break;
                        case 12:
                            MoveName.Text = "Lightning"; MoveName.ForeColor = IntCol;
                            MoveGuts.Text = "30";
                            MoveDamage.Text = GenerateStatValue(0, 14);
                            MoveHit.Text = GenerateStatValue(1, -10);
                            MoveGD.Text = GenerateStatValue(2, 35);
                            MoveSharp.Text = GenerateStatValue(3, 5);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[20].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[20] == 1);
                            CanUnlock.Checked = ((Mon_Stats[2] >= 250) && Mon_SubGenus == 0);
                            PlaceHighlight(20);
                            MoveInfo.Text = @"Withering tech. INT should be over 250 to unlock.
Fairy Hopper exclusive. However, can be passed on to other breeds via combination.";
                            break;
                        case 13:
                            MoveName.Text = "Flame"; MoveName.ForeColor = IntCol;
                            MoveGuts.Text = "36";
                            MoveDamage.Text = GenerateStatValue(0, 35);
                            MoveHit.Text = GenerateStatValue(1, -16);
                            MoveGD.Text = GenerateStatValue(2, 25);
                            MoveSharp.Text = GenerateStatValue(3, 5);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[21].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[21] == 1);
                            CanUnlock.Checked = ((Mon_Stats[2] + Mon_Stats[3] + Mon_Stats[4] >= 1200) && Mon_SubGenus == 0);
                            PlaceHighlight(21);
                            MoveInfo.Text = "Special tech. Fairy Hopper exclusive. INT + SKI + SPD should total over 1200 to unlock.";
                            break;
                        default:
                            MoveName.Text = "No Move"; MoveName.ForeColor = Color.Black;
                            MoveGuts.Text = "---";
                            MoveDamage.Text = "---";
                            MoveHit.Text = "---";
                            MoveGD.Text = "---";
                            MoveSharp.Text = "---";
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = "---";
                            MoveUnlocked.Checked = false;
                            CanUnlock.Checked = false;
                            MoveInfo.Text = "Click on a move to learn more.";
                            break;
                    }
                    break;

                case 13: // Hare

                    switch (MoveSelected)
                    {
                        case 1:
                            MoveName.Text = "Straight";
                            LazyDataFill(false, 20, 15, 20, 5, 10, 0);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_Stats[1] + Mon_Stats[3] >= 400);
                            MoveInfo.Text = "Hit tech. POW + SKI should total over 400 to unlock.\r\nChains into HardStraight when used 50x.";
                            break;

                        case 2:
                            MoveName.Text = "HardStraight";
                            LazyDataFill(false, 27, 20, 16, 8, 12, 1);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_MoveUsed[0] >= 50);
                            MoveInfo.Text = "Hit tech. Chains from 50 Straight. No stat requirement.";
                            break;

                        case 3:
                            MoveName.Text = "Kung Fu Fist";
                            LazyDataFill(false, 18, 19, -9, 7, 24, 2);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_Stats[1] + Mon_Stats[4] >= 400);
                            MoveInfo.Text = "Sharp tech. POW + SPD should total over 400 to unlock.\r\nChains into Kung Fu Blow and Spin Kick when used 50x. Learning priority over High Kick.";
                            break;

                        case 4:
                            MoveName.Text = "Kung Fu Blow";
                            LazyDataFill(false, 25, 27, -11, 8, 28, 3);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_MoveUsed[2] >= 50);
                            MoveInfo.Text = "Sharp tech. Chains from 50 Kung Fu Fist. No stat requirement.";
                            break;

                        case 5:
                            MoveName.Text = "Bang";
                            LazyDataFill(false, 45, 32, 10, 22, 10, 4);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_Stats[1] + Mon_Stats[3] + Mon_Stats[4] >= 1200);
                            MoveInfo.Text = "Special tech. POW + SKI + SPD should total over 1200 to unlock.\r\nChains into Big Bang when used 50x.";
                            break;

                        case 6:
                            MoveName.Text = "Big Bang";
                            LazyDataFill(false, 50, 44, 5, 29, 5, 5);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_MoveUsed[4] >= 50);
                            MoveInfo.Text = "Special tech. Chains from 50 Bang. No stat requirement.";
                            break;

                        case 7:
                            MoveName.Text = "Back Blow";
                            LazyDataFill(false, 19, 29, -14, 7, 10, 6);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_Stats[1] >= 250);
                            MoveInfo.Text = "Heavy tech. POW should be over 250 to unlock.\r\nChains into Rolling Blow when used 50x. Learning priority over Smash.";
                            break;

                        case 8:
                            MoveName.Text = "Rolling Blow";
                            LazyDataFill(false, 27, 40, -15, 12, 11, 7);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_MoveUsed[6] >= 50);
                            MoveInfo.Text = "Heavy tech. Chains from 50 Back Blow. No stat requirement.";
                            break;

                        case 9:
                            MoveName.Text = "Smash";
                            LazyDataFill(false, 26, 45, -18, 9, 10, 8);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_Stats[1] >= 450);
                            MoveInfo.Text = "Heavy tech. POW should be over 450 to unlock.\r\nChains into Heavy Smash when used 50x.";
                            break;

                        case 10:
                            MoveName.Text = "Heavy Smash";
                            LazyDataFill(false, 33, 56, -19, 13, 12, 9);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_MoveUsed[8] >= 50);
                            MoveInfo.Text = "Heavy tech. Chains from 50 Smash. No stat requirement.";
                            break;

                        case 11:
                            MoveName.Text = "High Kick";
                            LazyDataFill(false, 31, 29, -5, 12, 21, 10);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_Stats[1] + Mon_Stats[4] >= 650);
                            MoveInfo.Text = "Sharp tech. POW + SPD should total over 650 to unlock.";
                            break;

                        case 12:
                            MoveName.Text = "Spin Kick";
                            LazyDataFill(false, 36, 35, -7, 14, 25, 11);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_MoveUsed[2] >= 50);
                            MoveInfo.Text = "Sharp tech. Chains from 50 Kung Fu Fist. No stat requirement.\r\nChains into Kung Fu Kick when used 50x.";
                            break;

                        case 13:
                            MoveName.Text = "1-2 Punch";
                            LazyDataFill(false, 10, 12, 15, 0, 5, 12);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = true;
                            MoveInfo.Text = "Starting basic tech.";
                            break;

                        case 14:
                            MoveName.Text = "Foul Gas";
                            LazyDataFill(true, 14, 0, 9, 27, 10, 13);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_Stats[2] >= 250);
                            MoveInfo.Text = "Withering tech. INT should be over 250 to unlock.";
                            break;

                        case 15:
                            MoveName.Text = "Kung Fu Kick";
                            LazyDataFill(false, 44, 41, -12, 18, 29, 14);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_MoveUsed[11] >= 50);
                            MoveInfo.Text = "Special tech. Chains from 50 Spin Kick. No stat requirement.";
                            break;

                        case 16:
                            MoveName.Text = "Gas";
                            LazyDataFill(true, 10, 7, 7, 7, 0, 15);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = true;
                            MoveInfo.Text = "Starting basic tech.";
                            break;

                        case 17:
                            MoveName.Text = "Stinking Gas";
                            LazyDataFill(true, 25, 5, 13, 35, 10, 18);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_Nature <= -20);
                            MoveInfo.Text = "Withering tech. Requires Bad (-20) Nature.";
                            break;

                        default:
                            MoveName.Text = "No Move"; MoveName.ForeColor = Color.Black;
                            MoveGuts.Text = "---";
                            MoveDamage.Text = "---";
                            MoveHit.Text = "---";
                            MoveGD.Text = "---";
                            MoveSharp.Text = "---";
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = "---";
                            MoveUnlocked.Checked = false;
                            CanUnlock.Checked = false;
                            MoveInfo.Text = "Click on a move to learn more.";
                            break;
                    }
                    break;

                case 14: // Baku
                    switch (MoveSelected)
                    {
                        case 1:
                            MoveName.Text = "Dust Cloud";
                            LazyDataFill(false, 10, 12, 5, 0, 0, 0);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = true;
                            MoveInfo.Text = "Starting basic tech.";
                            break;

                        case 2:
                            MoveName.Text = "Bite";
                            LazyDataFill(false, 15, 15, 0, 9, 5, 1);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_Stats[1] + Mon_Stats[3] >= 400);
                            MoveInfo.Text = "Hit tech. POW + SKI should total over 400 to unlock.\r\nChains into Two Bites when used 50x. Learning priority over Gust Breath.";
                            break;

                        case 3:
                            MoveName.Text = "Two Bites";
                            LazyDataFill(false, 25, 23, 7, 10, 5, 2);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_MoveUsed[1] >= 50);
                            MoveInfo.Text = "Hit tech. Chains from 50 Bite. No stat requirement.\r\nChains into Three Bites when used 50x.";
                            break;

                        case 4:
                            MoveName.Text = "Three Bites";
                            LazyDataFill(false, 33, 33, -5, 33, 5, 3);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_MoveUsed[2] >= 50);
                            MoveInfo.Text = "Special tech. Chains from 50 Two Bites. No stat requirement.";
                            break;

                        case 5:
                            MoveName.Text = "Tongue Slap";
                            LazyDataFill(false, 25, 41, -14, 11, 5, 4);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_Stats[1] >= 250);
                            MoveInfo.Text = "Heavy tech. POW should be over 250 to unlock.\r\nLearning priority over Diving Press.";
                            break;

                        case 6:
                            MoveName.Text = "Charge";
                            LazyDataFill(false, 13, 17, -5, 0, 5, 6);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = true;
                            MoveInfo.Text = "Starting basic tech.";
                            break;

                        case 7:
                            MoveName.Text = "Roar";
                            LazyDataFill(true, 19, 18, -2, 10, 15, 7);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_Stats[2] + Mon_Stats[4] >= 400);
                            MoveInfo.Text = "Sharp tech. INT + SPD should total over 400 to unlock.\r\nChains into Two Roars when used 50x. Learning priority over Hypnotism.";
                            break;

                        case 8:
                            MoveName.Text = "Two Roars";
                            LazyDataFill(true, 26, 25, -8, 8, 24, 8);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_MoveUsed[7] >= 50);
                            MoveInfo.Text = "Sharp tech. Chains from 50 Roar. No stat requirement.\r\nChains into MillionRoars when used 50x.";
                            break;

                        case 9:
                            MoveName.Text = "MillionRoars";
                            LazyDataFill(true, 34, 43, -12, 22, 15, 9);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_MoveUsed[8] >= 50);
                            MoveInfo.Text = "Special tech. Chains from 50 Two Roars. No stat requirement.";
                            break;

                        case 10:
                            MoveName.Text = "Diving Press";
                            LazyDataFill(false, 30, 60, -16, 28, 15, 12);
                            MoveSH.Text = GenerateStatValue(0, 20);
                            MoveSH.Show();
                            SHLabel.Text = "Self-Damage (Miss):";
                            SHLabel.Show();
                            CanUnlock.Checked = (Mon_Stats[1] + Mon_Stats[5] >= 650);
                            MoveInfo.Text = "Heavy tech. POW + DEF should total over 650 to unlock.";
                            break;

                        case 11:
                            MoveName.Text = "Sneeze";
                            LazyDataFill(true, 20, 16, -9, 24, 5, 13);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_Stats[2] >= 250);
                            MoveInfo.Text = "Withering tech. INT should be over 250 to unlock.\r\nLearning priority over Mating Song.";
                            break;

                        case 12:
                            MoveName.Text = "Mating Song";
                            LazyDataFill(true, 32, 19, -7, 33, 6, 14);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_Stats[2] >= 450);
                            MoveInfo.Text = "Withering tech. INT should be over 450 to unlock.";
                            break;

                        case 13:
                            MoveName.Text = "Foul Wind";
                            LazyDataFill(true, 35, 26, -13, 40, 7, 15);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_Stats[2] >= 450 && Mon_Nature <= -20);
                            MoveInfo.Text = "Special tech. INT should be over 450 to unlock. Requires Bad (-20) Nature.";
                            break;

                        case 14:
                            MoveName.Text = "Gust Breath";
                            LazyDataFill(false, 19, 21, 5, 5, 5, 18);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_Stats[1] + Mon_Stats[3] >= 650);
                            MoveInfo.Text = "Hit tech. POW + SKI should total over 650 to unlock.";
                            break;

                        case 15:
                            MoveName.Text = "Hypnotism";
                            LazyDataFill(true, 21, 16, -4, 16, 25, 19);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_Stats[2] + Mon_Stats[4] >= 650);
                            MoveInfo.Text = "Sharp tech. INT + SPD should total over 650 to unlock.";
                            break;

                        case 16:
                            MoveName.Text = "Nap";
                            LazyDataFill(true, 30, 0, -20, 0, 0, 20);
                            MoveSH.Show();
                            MoveSH.Text = GenerateStatValue(0, 30);
                            SHLabel.Show();
                            SHLabel.Text = "Recovery:";
                            CanUnlock.Checked = (Mon_Stats[2] >= 600 && Mon_Nature >= 30);
                            MoveInfo.Text = "Special tech. INT should be over 600 to unlock. Requires Good (+30) Nature.";
                            break;

                        default:
                            MoveName.Text = "No Move"; MoveName.ForeColor = Color.Black;
                            MoveGuts.Text = "---";
                            MoveDamage.Text = "---";
                            MoveHit.Text = "---";
                            MoveGD.Text = "---";
                            MoveSharp.Text = "---";
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = "---";
                            MoveUnlocked.Checked = false;
                            CanUnlock.Checked = false;
                            MoveInfo.Text = "Click on a move to learn more.";
                            break;
                    }
                    break;

                case 15: // Gali
                    switch (MoveSelected)
                    {
                        case 1:
                            MoveName.Text = "Back Blow";
                            LazyDataFill(false, 25, 19, 13, 12, 7, 0);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_Stats[1] + Mon_Stats[3] >= 650);
                            MoveInfo.Text = "Hit tech. POW + SKI should total over 650 to unlock.\r\nChains into Heavy Blow when used 50x.";
                            break;

                        case 2:
                            MoveName.Text = "Fire Wall";
                            LazyDataFill(true, 15, 14, 11, 5, 5, 1);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_Stats[2] + Mon_Stats[3] >= 400);
                            MoveInfo.Text = "Hit tech. INT + SKI should total over 400 to unlock.\r\nChains into Blaze Wall when used 50x.";
                            break;

                        case 3:
                            MoveName.Text = "Blaze Wall";
                            LazyDataFill(true, 27, 21, 16, 13, 5, 2);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_MoveUsed[1] >= 50);
                            MoveInfo.Text = "Hit tech. Chains from 50 Fire Wall.\r\nChains into Napalm when used 50x. No stat requirement.";
                            break;

                        case 4:
                            MoveName.Text = "Napalm";
                            LazyDataFill(true, 38, 34, 12, 14, 5, 3);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_MoveUsed[2] >= 50);
                            MoveInfo.Text = "Special tech. Chains from 50 Blaze Wall. No stat requirement.";
                            break;

                        case 5:
                            MoveName.Text = "Heavy Blow";
                            LazyDataFill(false, 35, 26, 15, 16, 8, 6);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_MoveUsed[0] >= 50);
                            MoveInfo.Text = "Hit tech. Chains from 50 Back Blow.\r\nChains into Giant Blow when used 50x. No stat requirement.";
                            break;

                        case 6:
                            MoveName.Text = "Thwack";
                            LazyDataFill(false, 17, 27, -11, 7, 5, 7);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_Stats[1] >= 250);
                            MoveInfo.Text = "Heavy tech. POW should be over 250 to unlock.\r\nChains into Smash Thwack when used 50x. Learning priority over Whirlwind.";
                            break;

                        case 7:
                            MoveName.Text = "Whirlwind";
                            LazyDataFill(true, 25, 35, -9, 12, 5, 8);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_Stats[1] >= 450);
                            MoveInfo.Text = "Heavy tech. POW (not a typo) should be over 450 to unlock.\r\nChains into Typhoon when used 50x.";
                            break;

                        case 8:
                            MoveName.Text = "Typhoon";
                            LazyDataFill(true, 34, 42, -12, 26, 10, 9);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_MoveUsed[8] >= 50);
                            MoveInfo.Text = "Heavy tech. Chains from 50 Whirlwind.\r\nChains into Hurricane when used 50x. No stat requirement.";
                            break;

                        case 9:
                            MoveName.Text = "Hurricane";
                            LazyDataFill(true, 41, 50, -10, 27, 15, 10);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_MoveUsed[9] >= 50);
                            MoveInfo.Text = "Special tech. Chains from 50 Typhoon. No stat requirement.";
                            break;

                        case 10:
                            MoveName.Text = "Spirit Blow";
                            LazyDataFill(false, 38, 36, 0, 25, 8, 11);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_Stats[1] >= 600);
                            MoveInfo.Text = "Special tech. POW should be over 600 to unlock.\r\nChains into Spirit Punch and Spirit Smash when used 50x.";
                            break;

                        case 11:
                            MoveName.Text = "Straight";
                            LazyDataFill(false, 10, 13, 6, 0, 0, 12);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = true;
                            MoveInfo.Text = "Starting basic tech.";
                            break;

                        case 12:
                            MoveName.Text = "Smash Thwack";
                            LazyDataFill(false, 35, 49, -13, 19, 10, 13);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_MoveUsed[7] >= 50);
                            MoveInfo.Text = "Heavy tech. Chains from 50 Thwack.\r\nChains into Giant Thwack when used 50x. No stat requirement.";
                            break;

                        case 13:
                            MoveName.Text = "Red Wisp";
                            LazyDataFill(true, 18, 10, -2, 27, 5, 14);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_Stats[2] >= 250 && Mon_Nature >= 20);
                            MoveInfo.Text = "Withering tech. INT should be over 250 to unlock. Requires Good (+20) Nature.";
                            break;

                        case 14:
                            MoveName.Text = "Blue Wisp";
                            LazyDataFill(true, 30, 18, -4, 44, 5, 15);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_Stats[2] >= 450 && Mon_Nature >= 50);
                            MoveInfo.Text = "Withering tech. INT should be over 450 to unlock. Requires Good (+50) Nature.";
                            break;

                        case 15:
                            MoveName.Text = "Flying Mask";
                            LazyDataFill(false, 17, 15, 1, 7, 19, 16);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_Stats[2] + Mon_Stats[4] >= 400);
                            MoveInfo.Text = "Sharp tech. INT + SPD should total over 400 to unlock.\r\nChains into Cutting Mask when used 50x.";
                            break;

                        case 16:
                            MoveName.Text = "Spirit Punch";
                            LazyDataFill(false, 43, 45, -5, 29, 9, 17);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_MoveUsed[11] >= 50 && Mon_Nature >= 20);
                            MoveInfo.Text = "Special tech. Chains from 50 Spirit Blow.\r\nRequires Good (+20) Nature. No stat requirement.";
                            break;

                        case 17:
                            MoveName.Text = "Thunderbolt";
                            LazyDataFill(true, 13, 14, 3, 5, 0, 18);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = true;
                            MoveInfo.Text = "Starting basic tech.";
                            break;

                        case 18:
                            MoveName.Text = "Giant Blow";
                            LazyDataFill(false, 41, 33, 14, 17, 6, 19);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_MoveUsed[6] >= 50);
                            MoveInfo.Text = "Special tech. Chains from 50 Heavy Blow. No stat requirement.";
                            break;

                        case 19:
                            MoveName.Text = "Giant Thwack";
                            LazyDataFill(false, 40, 54, -10, 24, 5, 20);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_MoveUsed[13] >= 50);
                            MoveInfo.Text = "Special tech. Chains from 50 Smash Thwack. No stat requirement.";
                            break;

                        case 20:
                            MoveName.Text = "Cutting Mask";
                            LazyDataFill(false, 24, 19, -1, 9, 23, 21);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_MoveUsed[16] >= 50);
                            MoveInfo.Text = "Sharp tech. Chains from 50 Flying Mask.\r\nChains into Hashing Mask when used 50x. No stat requirement.";
                            break;

                        case 21:
                            MoveName.Text = "Hashing Mask";
                            LazyDataFill(false, 38, 28, -3, 28, 28, 22);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_MoveUsed[21] >= 50);
                            MoveInfo.Text = "Special tech. Chains from 50 Cutting Mask. No stat requirement.";
                            break;

                        case 22:
                            MoveName.Text = "Spirit Smash";
                            LazyDataFill(false, 40, 33, -5, 45, 5, 23);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_MoveUsed[11] >= 50 && Mon_Nature <= -20);
                            MoveInfo.Text = "Special tech. Chains from 50 Spirit Blow.\r\nRequires Bad (-20) Nature. No stat requirement.";
                            break;

                        default:
                            MoveName.Text = "No Move"; MoveName.ForeColor = Color.Black;
                            MoveGuts.Text = "---";
                            MoveDamage.Text = "---";
                            MoveHit.Text = "---";
                            MoveGD.Text = "---";
                            MoveSharp.Text = "---";
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = "---";
                            MoveUnlocked.Checked = false;
                            CanUnlock.Checked = false;
                            MoveInfo.Text = "Click on a move to learn more.";
                            break;
                    }
                    break;

                case 16: // Kato
                    switch (MoveSelected)
                    {
                        case 1:
                            MoveName.Text = "Slash Claw";
                            LazyDataFill(false, 10, 13, 7, 0, 10, 0);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = true;
                            MoveInfo.Text = "Starting basic tech. Chains into Slash Claws when used 30x.";
                            break;

                        case 2:
                            MoveName.Text = "Slash Claws";
                            LazyDataFill(false, 17, 19, 4, 5, 10, 1);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_MoveUsed[0] >= 30);
                            MoveInfo.Text = "Basic tech. Chains from 30 Slash Claw.\r\nChains into Claw Combo when used 50x. No stat requirement.";
                            break;

                        case 3:
                            MoveName.Text = "Claw Combo";
                            LazyDataFill(false, 23, 36, -12, 10, 10, 2);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_MoveUsed[1] >= 50);
                            MoveInfo.Text = "Heavy tech. Chains from 50 Slash Claws. No stat requirement.";
                            break;

                        case 4:
                            MoveName.Text = "Smoke Breath";
                            LazyDataFill(true, 18, 8, 5, 24, 10, 3);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_Stats[2] >= 250 && Mon_Nature <= -20);
                            MoveInfo.Text = "Withering tech. INT should be over 250 to unlock. Requires Bad (-20) Nature.";
                            break;

                        case 5:
                            MoveName.Text = "Lick";
                            LazyDataFill(true, 19, 13, -7, 29, 5, 4);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_Stats[2] >= 250 && Mon_SubGenus == 23);
                            MoveInfo.Text = "Withering tech. INT should be over 250 to unlock. Exclusive to Citronie.";
                            break;

                        case 6:
                            MoveName.Text = "Licking";
                            LazyDataFill(true, 15, 13, -11, 24, 5, 5);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_Stats[2] >= 250 && Mon_SubGenus == 25);
                            MoveInfo.Text = "Withering tech. INT should be over 250 to unlock. Exclusive to Pink Kato.";
                            break;

                        case 7:
                            MoveName.Text = "Thrust Claw";
                            LazyDataFill(false, 12, 16, 3, 0, 10, 6);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = true;
                            MoveInfo.Text = "Starting basic tech.";
                            break;

                        case 8:
                            MoveName.Text = "Oil Spray";
                            LazyDataFill(true, 27, 23, -11, 43, 5, 7);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_Stats[2] >= 450 && Mon_Nature <= -50);
                            MoveInfo.Text = "Withering tech. INT should be over 450 to unlock. Requires Bad (-50) Nature.";
                            break;

                        case 9:
                            MoveName.Text = "Turn Claw";
                            LazyDataFill(false, 15, 19, -7, 12, 20, 8);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_Stats[1] + Mon_Stats[4] >= 400);
                            MoveInfo.Text = "Sharp tech. POW + SPD should total over 400 to unlock.\r\nChains into Turn Claws when used 50x. Learning priority over Drill Claw.";
                            break;

                        case 10:
                            MoveName.Text = "Turn Claws";
                            LazyDataFill(false, 28, 29, -10, 19, 25, 9);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_MoveUsed[8] >= 50);
                            MoveInfo.Text = "Sharp tech. Chains from 50 Turn Claw.\r\nChains into Rolling Claw when used 50x. No stat requirement.";
                            break;

                        case 11:
                            MoveName.Text = "Rolling Claw";
                            LazyDataFill(false, 30, 30, -13, 30, 30, 10);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_MoveUsed[9] >= 50 && Mon_Nature >= 20);
                            MoveInfo.Text = "Special tech. Chains from 50 Turn Claws.\r\nRequires Good (+20) Nature. No stat requirement.";
                            break;

                        case 12:
                            MoveName.Text = "Oil Fire";
                            LazyDataFill(true, 17, 28, -6, 7, 5, 12);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_Stats[1] + Mon_Stats[2] >= 650);
                            MoveInfo.Text = "Heavy tech. POW + INT should total over 650 to unlock.\r\nChains into Oil Flame when used 50x.";
                            break;

                        case 13:
                            MoveName.Text = "Oil Flame";
                            LazyDataFill(true, 30, 46, -15, 14, 10, 13);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_MoveUsed[12] >= 50 && Mon_Nature <= -30);
                            MoveInfo.Text = "Heavy tech. Chains from 50 Oil Fire.\r\nRequires Bad (-30) Nature. No stat requirement.";
                            break;

                        case 14:
                            MoveName.Text = "Drill Claw";
                            LazyDataFill(false, 20, 25, 0, 5, 25, 14);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_Stats[1] + Mon_Stats[4] >= 650);
                            MoveInfo.Text = "Sharp tech. POW + SPD should total over 650 to unlock.";
                            break;

                        case 15:
                            MoveName.Text = "Twister Claw";
                            LazyDataFill(true, 25, 20, 20, 7, 10, 15);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_Stats[2] + Mon_Stats[3] >= 650);
                            MoveInfo.Text = "Hit tech. INT + SKI should total over 650 to unlock.\r\nChains into Tornado Claw when used 50x.";
                            break;

                        case 16:
                            MoveName.Text = "Tornado Claw";
                            LazyDataFill(true, 35, 25, 20, 10, 15, 16);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_MoveUsed[15] >= 50);
                            MoveInfo.Text = "Hit tech. Chains from 50 Twister Claw. No stat requirement.";
                            break;

                        case 17:
                            MoveName.Text = "Phantom Claw";
                            LazyDataFill(false, 17, 17, 15, 5, 10, 18);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_Stats[1] + Mon_Stats[3] >= 400);
                            MoveInfo.Text = "Hit tech. POW + SKI should total over 400 to unlock.";
                            break;

                        case 18:
                            MoveName.Text = "Hopping Claw";
                            LazyDataFill(false, 17, 24, -5, 8, 10, 19);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_Stats[1] >= 250);
                            MoveInfo.Text = "Heavy tech. POW should be over 250 to unlock.\r\nChains into Jumping Claw when used 50x. Learning priority over Oil Fire.";
                            break;

                        case 19:
                            MoveName.Text = "Jumping Claw";
                            LazyDataFill(false, 22, 36, -18, 17, 10, 20);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_MoveUsed[19] >= 50);
                            MoveInfo.Text = "Heavy tech. Chains from 50 Hopping Claw.\r\nChains into Aerial Claw when used 50x. No stat requirement.";
                            break;

                        case 20:
                            MoveName.Text = "Aerial Claw";
                            LazyDataFill(false, 30, 43, -19, 27, 19, 21);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_MoveUsed[20] >= 50);
                            MoveInfo.Text = "Special tech. Chains from 50 Jumping Claw. No stat requirement.";
                            break;

                        case 21:
                            MoveName.Text = "Oil Drinking";
                            LazyDataFill(true, 50, 0, -15, 0, 0, 22);
                            MoveSH.Show();
                            MoveSH.Text = GenerateStatValue(0, 50);
                            SHLabel.Show();
                            SHLabel.Text = "Recovery:";
                            CanUnlock.Checked = (Mon_Stats[2] >= 600 && Mon_Nature >= 50);
                            MoveInfo.Text = "Special tech. INT should be over 600 to unlock. Requires Good (+50) Nature.";
                            break;

                        case 22:
                            MoveName.Text = "Bolt";
                            LazyDataFill(true, 11, 7, -2, 16, 10, 23);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_Stats[2] >= 250 && Mon_SubGenus == 11);
                            MoveInfo.Text = "Withering tech. INT should be over 250 to unlock. Exclusive to Blue Kato.";
                            break;


                        default:
                            MoveName.Text = "No Move"; MoveName.ForeColor = Color.Black;
                            MoveGuts.Text = "---";
                            MoveDamage.Text = "---";
                            MoveHit.Text = "---";
                            MoveGD.Text = "---";
                            MoveSharp.Text = "---";
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = "---";
                            MoveUnlocked.Checked = false;
                            CanUnlock.Checked = false;
                            MoveInfo.Text = "Click on a move to learn more.";
                            break;
                    }
                    break;

                case 17: // Godzilla
                    switch (MoveSelected)
                    {
                        case 1:
                            MoveName.Text = "Belly Attack"; MoveName.ForeColor = PowCol;
                            MoveGuts.Text = "12";
                            MoveDamage.Text = GenerateStatValue(0, 16);
                            MoveHit.Text = GenerateStatValue(1, 2);
                            MoveGD.Text = "---";
                            MoveSharp.Text = "---";
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[0].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[0] == 1);
                            CanUnlock.Checked = true;
                            PlaceHighlight(0);
                            MoveInfo.Text = "Starting basic tech.";
                            break;
                        case 2:
                            MoveName.Text = "Head Butt"; MoveName.ForeColor = PowCol;
                            MoveGuts.Text = "16";
                            MoveDamage.Text = GenerateStatValue(0, 17);
                            MoveHit.Text = GenerateStatValue(1, 8);
                            MoveGD.Text = GenerateStatValue(2, 5);
                            MoveSharp.Text = GenerateStatValue(3, 5);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[1].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[1] == 1);
                            CanUnlock.Checked = (Mon_Stats[1] + Mon_Stats[3] >= 400);
                            PlaceHighlight(1);
                            MoveInfo.Text = "Hit tech. POW + SKI should total over 400 to unlock.";
                            break;
                        case 3:
                            MoveName.Text = "Knocking Up"; MoveName.ForeColor = PowCol;
                            MoveGuts.Text = "19";
                            MoveDamage.Text = GenerateStatValue(0, 20);
                            MoveHit.Text = GenerateStatValue(1, 1);
                            MoveGD.Text = GenerateStatValue(2, 5);
                            MoveSharp.Text = GenerateStatValue(3, 16);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[2].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[2] == 1);
                            CanUnlock.Checked = (Mon_Stats[1] + Mon_Stats[4] >= 400);
                            PlaceHighlight(2);
                            MoveInfo.Text = "Sharp tech. POW + SPD should total over 400 to unlock.";
                            break;
                        case 4:
                            MoveName.Text = "Scratch"; MoveName.ForeColor = PowCol;
                            MoveGuts.Text = "10";
                            MoveDamage.Text = GenerateStatValue(0, 14);
                            MoveHit.Text = GenerateStatValue(1, -3);
                            MoveGD.Text = "---";
                            MoveSharp.Text = GenerateStatValue(3, 5);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[6].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[6] == 1);
                            CanUnlock.Checked = true;
                            PlaceHighlight(6);
                            MoveInfo.Text = "Starting basic tech.";
                            break;
                        case 5:
                            MoveName.Text = "Tail Lashes"; MoveName.ForeColor = PowCol;
                            MoveGuts.Text = "24";
                            MoveDamage.Text = GenerateStatValue(0, 22);
                            MoveHit.Text = GenerateStatValue(1, 13);
                            MoveGD.Text = GenerateStatValue(2, 8);
                            MoveSharp.Text = GenerateStatValue(3, 5);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[7].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[7] == 1);
                            CanUnlock.Checked = (Mon_Stats[1] + Mon_Stats[3] >= 600);
                            PlaceHighlight(7);
                            MoveInfo.Text = "Hit tech. POW + SKI should total over 600 to unlock."; //bedeg
                            break;
                        case 6:
                            MoveName.Text = "Sneeze"; MoveName.ForeColor = IntCol;
                            MoveGuts.Text = "17";
                            MoveDamage.Text = GenerateStatValue(0, 11);
                            MoveHit.Text = GenerateStatValue(1, -4);
                            MoveGD.Text = GenerateStatValue(2, 25);
                            MoveSharp.Text = GenerateStatValue(3, 5);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[8].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[8] == 1);
                            CanUnlock.Checked = (Mon_Stats[2] >= 250);
                            PlaceHighlight(8);
                            MoveInfo.Text = "Withering tech. INT should be over 250 to unlock.";
                            break;
                        case 7:
                            MoveName.Text = "Body Press"; MoveName.ForeColor = PowCol;
                            MoveGuts.Text = "40";
                            MoveDamage.Text = GenerateStatValue(0, 60);
                            MoveHit.Text = GenerateStatValue(1, -10);
                            MoveGD.Text = GenerateStatValue(2, 33);
                            MoveSharp.Text = GenerateStatValue(3, 5);
                            MoveSH.Text = GenerateStatValue(0, 15);
                            MoveSH.Show();
                            SHLabel.Text = "Self-Damage (Miss):";
                            SHLabel.Show();
                            MoveUses.Text = Mon_MoveUsed[9].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[9] == 1);
                            CanUnlock.Checked = (Mon_Stats[1] + Mon_Stats[5] >= 800);
                            PlaceHighlight(9);
                            MoveInfo.Text = @"Special tech. Chains into Wave Riding when used 50x. 
POW + DEF should total over 800 to unlock.";
                            break;
                        case 8:
                            MoveName.Text = "Wave Riding"; MoveName.ForeColor = PowCol;
                            MoveGuts.Text = "45";
                            MoveDamage.Text = GenerateStatValue(0, 69);
                            MoveHit.Text = GenerateStatValue(1, -12);
                            MoveGD.Text = GenerateStatValue(2, 41);
                            MoveSharp.Text = GenerateStatValue(3, 15);
                            MoveSH.Text = GenerateStatValue(0, 25);
                            MoveSH.Show();
                            SHLabel.Text = "Self-Damage (Miss):";
                            SHLabel.Show();
                            MoveUses.Text = Mon_MoveUsed[10].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[10] == 1);
                            CanUnlock.Checked = (Mon_MoveUsed[9] >= 50);
                            PlaceHighlight(10);
                            MoveInfo.Text = "Special tech. Chains from 50 Body Presses.";
                            break;
                        case 9:
                            MoveName.Text = "Earthquake"; MoveName.ForeColor = PowCol;
                            MoveGuts.Text = "29";
                            MoveDamage.Text = GenerateStatValue(0, 58);
                            MoveHit.Text = GenerateStatValue(1, -21);
                            MoveGD.Text = GenerateStatValue(2, 21);
                            MoveSharp.Text = GenerateStatValue(3, 5);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[12].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[12] == 1);
                            CanUnlock.Checked = (Mon_Stats[1] >= 450);
                            PlaceHighlight(12);
                            MoveInfo.Text = "Heavy tech. POW should be over 450 to unlock.";
                            break;
                        case 10:
                            MoveName.Text = "Bubbles"; MoveName.ForeColor = IntCol;
                            MoveGuts.Text = "30";
                            MoveDamage.Text = GenerateStatValue(0, 19);
                            MoveHit.Text = GenerateStatValue(1, -5);
                            MoveGD.Text = GenerateStatValue(2, 37);
                            MoveSharp.Text = GenerateStatValue(3, 5);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[13].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[13] == 1);
                            CanUnlock.Checked = (Mon_Stats[2] >= 450 && Mon_Nature <= -20); //bedeg
                            PlaceHighlight(13);
                            MoveInfo.Text = "Withering tech. INT should be over 450 to unlock. Requires Bad (-20) Nature.";
                            break;
                        case 11:
                            MoveName.Text = "Charge"; MoveName.ForeColor = PowCol;
                            MoveGuts.Text = "27";
                            MoveDamage.Text = GenerateStatValue(0, 24);
                            MoveHit.Text = GenerateStatValue(1, -1);
                            MoveGD.Text = GenerateStatValue(2, 14);
                            MoveSharp.Text = GenerateStatValue(3, 19);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[14].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[14] == 1);
                            CanUnlock.Checked = (Mon_Stats[1] + Mon_Stats[4] >= 650);
                            PlaceHighlight(14);
                            MoveInfo.Text = "Sharp tech. POW + SPD should total over 650 to unlock. Chains into Zilla Rush when used 50x.";
                            break;
                        case 12:
                            MoveName.Text = "Zilla Rush"; MoveName.ForeColor = PowCol;
                            MoveGuts.Text = "32";
                            MoveDamage.Text = GenerateStatValue(0, 27);
                            MoveHit.Text = GenerateStatValue(1, 0);
                            MoveGD.Text = GenerateStatValue(2, 16);
                            MoveSharp.Text = GenerateStatValue(3, 22);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[15].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[15] == 1);
                            CanUnlock.Checked = (Mon_MoveUsed[14] >= 50);
                            PlaceHighlight(15);
                            MoveInfo.Text = "Sharp tech. Chains from 50 Charges.";
                            break;
                        case 13:
                            MoveName.Text = "Roll Assault"; MoveName.ForeColor = PowCol;
                            MoveGuts.Text = "21";
                            MoveDamage.Text = GenerateStatValue(0, 35);
                            MoveHit.Text = GenerateStatValue(1, -12);
                            MoveGD.Text = GenerateStatValue(2, 7);
                            MoveSharp.Text = GenerateStatValue(3, 5);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[18].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[18] == 1);
                            CanUnlock.Checked = (Mon_Stats[1] >= 250);
                            PlaceHighlight(18);
                            MoveInfo.Text = "Heavy tech. POW should be over 250 to unlock.";
                            break;
                        case 14:
                            MoveName.Text = "Tidal Wave"; MoveName.ForeColor = IntCol;
                            MoveGuts.Text = "37";
                            MoveDamage.Text = GenerateStatValue(0, 42);
                            MoveHit.Text = GenerateStatValue(1, -9);
                            MoveGD.Text = GenerateStatValue(2, 24);
                            MoveSharp.Text = GenerateStatValue(3, 15);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[19].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[19] == 1);
                            CanUnlock.Checked = (Mon_Stats[0] + Mon_Stats[2] >= 800 && Mon_Nature >= 20);
                            PlaceHighlight(19);
                            MoveInfo.Text = "Special tech. LIF + INT should total over 800 to unlock. Requires Good (+20) Nature.";
                            break;
                        default:
                            MoveName.Text = "No Move"; MoveName.ForeColor = Color.Black;
                            MoveGuts.Text = "---";
                            MoveDamage.Text = "---";
                            MoveHit.Text = "---";
                            MoveGD.Text = "---";
                            MoveSharp.Text = "---";
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = "---";
                            MoveUnlocked.Checked = false;
                            CanUnlock.Checked = false;
                            MoveInfo.Text = "Click on a move to learn more.";
                            break;
                    }
                    break;
                case 18: // Bajarl
                    switch (MoveSelected)
                    {
                        case 1:
                            MoveName.Text = "Hook"; MoveName.ForeColor = PowCol;
                            MoveGuts.Text = "25";
                            MoveDamage.Text = GenerateStatValue(0, 39);
                            MoveHit.Text = GenerateStatValue(1, -16);
                            MoveGD.Text = GenerateStatValue(2, 11);
                            MoveSharp.Text = GenerateStatValue(3, 5);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[0].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[0] == 1);
                            CanUnlock.Checked = (Mon_MoveUsed[9] >= 50 && (Mon_Stats[1] > 450));
                            PlaceHighlight(0);
                            MoveInfo.Text = @"Heavy tech. Chains from 50 Straights. Chains into 1-2 Hook when used 50x.
POW should be over 450 to unlock.";
                            break;
                        case 2:
                            MoveName.Text = "Left Jab"; MoveName.ForeColor = PowCol;
                            MoveGuts.Text = "10";
                            MoveDamage.Text = GenerateStatValue(0, 8);
                            MoveHit.Text = GenerateStatValue(1, 9);
                            MoveGD.Text = "---";
                            MoveSharp.Text = "---";
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[6].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[6] == 1);
                            CanUnlock.Checked = true;
                            PlaceHighlight(6);
                            MoveInfo.Text = "Starting basic tech.";
                            break;
                        case 3:
                            MoveName.Text = "Right Jab"; MoveName.ForeColor = PowCol;
                            MoveGuts.Text = "12";
                            MoveDamage.Text = GenerateStatValue(0, 13);
                            MoveHit.Text = GenerateStatValue(1, 4);
                            MoveGD.Text = "---";
                            MoveSharp.Text = GenerateStatValue(3, 5);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[7].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[7] == 1);
                            CanUnlock.Checked = true;
                            PlaceHighlight(7);
                            MoveInfo.Text = "Starting basic tech. Chains into Straight (DX) when used 50x."; //bedeg
                            break;
                        case 4:
                            MoveName.Text = "1-2-Hook"; MoveName.ForeColor = PowCol;
                            MoveGuts.Text = "30";
                            MoveDamage.Text = GenerateStatValue(0, 46);
                            MoveHit.Text = GenerateStatValue(1, -16);
                            MoveGD.Text = GenerateStatValue(2, 15);
                            MoveSharp.Text = GenerateStatValue(3, 5);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[8].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[8] == 1);
                            CanUnlock.Checked = (Mon_MoveUsed[0] >= 50); //bedeg
                            PlaceHighlight(8);
                            MoveInfo.Text = "Heavy tech. Chains from 50 Hooks. Chains into 1-2-Smash (DX) when used 50x."; //bedeg
                            break;
                        case 5:
                            MoveName.Text = "Straight"; MoveName.ForeColor = PowCol;
                            MoveGuts.Text = "20";
                            MoveDamage.Text = GenerateStatValue(0, 28);
                            MoveHit.Text = GenerateStatValue(1, -12);
                            MoveGD.Text = GenerateStatValue(2, 8);
                            MoveSharp.Text = GenerateStatValue(3, 5);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[9].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[9] == 1);
                            CanUnlock.Checked = (Mon_Stats[1] >= 250 && Mon_MoveUsed[7] >= 50); //bedeg
                            PlaceHighlight(9);
                            MoveInfo.Text = @"Heavy tech. POW should be over 250 to unlock. Chains from 50 Right Jabs.
Chains into Hook (DX) when used 50x."; //bedeg
                            break;
                        case 6:
                            MoveName.Text = "Uppercut"; MoveName.ForeColor = PowCol;
                            MoveGuts.Text = "21";
                            MoveDamage.Text = GenerateStatValue(0, 17);
                            MoveHit.Text = GenerateStatValue(1, 0);
                            MoveGD.Text = GenerateStatValue(2, 9);
                            MoveSharp.Text = GenerateStatValue(3, 16);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[10].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[10] == 1);
                            CanUnlock.Checked = (Mon_Stats[1] + Mon_Stats[4] >= 400);
                            PlaceHighlight(10);
                            MoveInfo.Text = "Sharp tech. POW + SPD should total over 400 to unlock.\r\nChains into 1-2 Uppercut when used 50x.";
                            break;
                        case 7:
                            MoveName.Text = "1-2-Uppercut"; MoveName.ForeColor = PowCol;
                            MoveGuts.Text = "29";
                            MoveDamage.Text = GenerateStatValue(0, 22);
                            MoveHit.Text = GenerateStatValue(1, 2);
                            MoveGD.Text = GenerateStatValue(2, 12);
                            MoveSharp.Text = GenerateStatValue(3, 19);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[11].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[11] == 1);
                            CanUnlock.Checked = (Mon_MoveUsed[10] >= 50);
                            PlaceHighlight(11);
                            MoveInfo.Text = "Sharp tech. Chains from 50 Uppercuts. Chains into Mystic Combo when used 50x.";
                            break;
                        case 8:
                            MoveName.Text = "1-2-Smash"; MoveName.ForeColor = PowCol;
                            MoveGuts.Text = "36";
                            MoveDamage.Text = GenerateStatValue(0, 53);
                            MoveHit.Text = GenerateStatValue(1, -16);
                            MoveGD.Text = GenerateStatValue(2, 21);
                            MoveSharp.Text = GenerateStatValue(3, 5);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[12].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[12] == 1);
                            CanUnlock.Checked = (Mon_MoveUsed[8] >= 50 && Mon_Nature >= 20); //bedeg
                            PlaceHighlight(12);
                            MoveInfo.Text = "Special tech. Chains from 50 1-2-Hook (DX). Requires Good (+20) Nature."; //bedeg
                            break;
                        case 9:
                            MoveName.Text = "Magic Punch"; MoveName.ForeColor = PowCol;
                            MoveGuts.Text = "26";
                            MoveDamage.Text = GenerateStatValue(0, 34);
                            MoveHit.Text = GenerateStatValue(1, -9);
                            MoveGD.Text = GenerateStatValue(2, 9);
                            MoveSharp.Text = GenerateStatValue(3, 5);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[13].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[13] == 1);
                            CanUnlock.Checked = (Mon_MoveUsed[9] >= 50);
                            PlaceHighlight(13);
                            MoveInfo.Text = "Heavy tech. Chains from 50 Straights. Chains into Mystic Punch when used 50x.";
                            break;
                        case 10:
                            MoveName.Text = "Mystic Combo"; MoveName.ForeColor = PowCol;
                            MoveGuts.Text = "39";
                            MoveDamage.Text = GenerateStatValue(0, 33);
                            MoveHit.Text = GenerateStatValue(1, 2);
                            MoveGD.Text = GenerateStatValue(2, 12);
                            MoveSharp.Text = GenerateStatValue(3, 15);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[14].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[14] == 1);
                            CanUnlock.Checked = (Mon_MoveUsed[11] >= 50 && Mon_Nature >= 20);
                            PlaceHighlight(14);
                            MoveInfo.Text = @"Special tech. Chains from 50 1-2-Uppercuts. Requires Good (+20) Nature.";
                            break;
                        case 11:
                            MoveName.Text = "Mystic Punch"; MoveName.ForeColor = PowCol;
                            MoveGuts.Text = "34";
                            MoveDamage.Text = GenerateStatValue(0, 43);
                            MoveHit.Text = GenerateStatValue(1, -11);
                            MoveGD.Text = GenerateStatValue(2, 18);
                            MoveSharp.Text = GenerateStatValue(3, 5);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[18].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[18] == 1);
                            CanUnlock.Checked = (Mon_MoveUsed[13] >= 50 && Mon_Nature >= 20);
                            PlaceHighlight(18);
                            MoveInfo.Text = "Special tech. Chains from 50 Magic Punches. Requires Good (+20) Nature.";
                            break;
                        case 12:
                            MoveName.Text = "Magic Pot"; MoveName.ForeColor = IntCol;
                            MoveGuts.Text = "35";
                            MoveDamage.Text = GenerateStatValue(0, 20);
                            MoveHit.Text = GenerateStatValue(1, -5);
                            MoveGD.Text = "---";
                            MoveSharp.Text = "---";
                            MoveSH.Show(); MoveSH.Text = "100%";
                            SHLabel.Show(); SHLabel.Text = "HP Drain:";
                            MoveUses.Text = Mon_MoveUsed[19].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[19] == 1);
                            CanUnlock.Checked = (Mon_Stats[2] >= 450 && Mon_Nature <= -20); //bedeg
                            PlaceHighlight(19);
                            MoveInfo.Text = @"Special tech. INT should be over 450 to unlock. Requires Bad (-20) Nature.
Chains into Mystic Pot and Miracle Pot when used 50x.";
                            break;
                        case 13:
                            MoveName.Text = "Mystic Pot"; MoveName.ForeColor = IntCol;
                            MoveGuts.Text = "45";
                            MoveDamage.Text = "---";
                            MoveHit.Text = GenerateStatValue(1, -10);
                            MoveGD.Text = GenerateStatValue(2, 30);
                            MoveSharp.Text = "---";
                            MoveSH.Show(); MoveSH.Text = "100%";
                            SHLabel.Show(); SHLabel.Text = "Guts Drain:";
                            MoveUses.Text = Mon_MoveUsed[20].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[20] == 1);
                            CanUnlock.Checked = (Mon_MoveUsed[19] >= 50 && Mon_Nature <= -40); //bedeg
                            PlaceHighlight(20);
                            MoveInfo.Text = "Special tech. Chains from 50 Magic Pots. Requires Bad (-40) Nature.";
                            break;
                        case 14:
                            MoveName.Text = "Miracle Pot"; MoveName.ForeColor = IntCol;
                            MoveGuts.Text = "60";
                            MoveDamage.Text = GenerateStatValue(0, 30);
                            MoveHit.Text = GenerateStatValue(1, -15);
                            MoveGD.Text = GenerateStatValue(2, 30);
                            MoveSharp.Text = "---";
                            MoveSH.Show(); MoveSH.Text = "100%";
                            SHLabel.Show(); SHLabel.Text = "HP/Guts Drain:";
                            MoveUses.Text = Mon_MoveUsed[21].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[21] == 1);
                            CanUnlock.Checked = (Mon_MoveUsed[19] >= 50 && Mon_Nature <= -60); //bedeg
                            PlaceHighlight(21);
                            MoveInfo.Text = "Special tech. Chains from 50 Magic Pots. Requires Worst (-60) Nature.";
                            break;
                        case 15:
                            MoveName.Text = "Bajarl Beam"; MoveName.ForeColor = IntCol;
                            MoveGuts.Text = "40";
                            MoveDamage.Text = GenerateStatValue(0, 24);
                            MoveHit.Text = GenerateStatValue(1, 10);
                            MoveGD.Text = GenerateStatValue(2, 24);
                            MoveSharp.Text = GenerateStatValue(3, 5);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[22].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[22] == 1);
                            CanUnlock.Checked = (Mon_Stats[2] + Mon_Stats[3] >= 800);
                            PlaceHighlight(22);
                            MoveInfo.Text = "Special tech. INT + SKI should be over 800 to unlock.";
                            break;
                        default:
                            MoveName.Text = "No Move"; MoveName.ForeColor = Color.Black;
                            MoveGuts.Text = "---";
                            MoveDamage.Text = "---";
                            MoveHit.Text = "---";
                            MoveGD.Text = "---";
                            MoveSharp.Text = "---";
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = "---";
                            MoveUnlocked.Checked = false;
                            CanUnlock.Checked = false;
                            MoveInfo.Text = "Click on a move to learn more.";
                            break;
                    }
                    break;

                case 19: //Mew
                    switch (MoveSelected)
                    {
                        case 1:
                            MoveName.Text = "Punch";
                            LazyDataFill(false, 10, 9, 5, 0, 0, 0);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = true;
                            MoveInfo.Text = "Starting basic tech.";
                            break;

                        case 2:
                            MoveName.Text = "Head Butt";
                            LazyDataFill(false, 15, 15, 1, 5, 23, 1);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_Stats[1] + Mon_Stats[4] >= 400 && Mon_Nature >= 20);
                            MoveInfo.Text = "Sharp tech. POW + SPD should total over 400 to unlock.\r\nChains into Head Assault when used 50x. Requires Good (+20) Nature.";
                            break;

                        case 3:
                            MoveName.Text = "Head Assault";
                            LazyDataFill(false, 25, 20, 2, 10, 25, 2);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_MoveUsed[1] >= 50 && Mon_Nature >= 50);
                            MoveInfo.Text = "Sharp tech. Chains from 50 Head Butt.\r\nRequires Good (+50) Nature. No stat requirement.";
                            break;

                        case 4:
                            MoveName.Text = "Leaping Kick";
                            LazyDataFill(false, 12, 12, 0, 0, 5, 6);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = true;
                            MoveInfo.Text = "Starting basic tech.";
                            break;

                        case 5:
                            MoveName.Text = "Scratch";
                            LazyDataFill(false, 19, 15, 18, 6, 15, 7);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_Stats[1] + Mon_Stats[3] >= 400);
                            MoveInfo.Text = "Hit tech. POW + SKI should total over 400 to unlock.";
                            break;

                        case 6:
                            MoveName.Text = "Stab";
                            LazyDataFill(false, 19, 13, -2, 21, 10, 8);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_Stats[2] >= 250 && Mon_Nature <= -20);
                            MoveInfo.Text = "Withering tech. INT should be over 250 to unlock. Requires Bad (-20) Nature.";
                            break;

                        case 7:
                            MoveName.Text = "RushingPunch";
                            LazyDataFill(false, 30, 35, -3, 11, 10, 9);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_Stats[1] >= 450);
                            MoveInfo.Text = "Heavy tech. POW should be over 450 to unlock.";
                            break;

                        case 8:
                            MoveName.Text = "Diving Press";
                            LazyDataFill(false, 19, 25, -9, 7, 10, 12);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_Stats[1] >= 250);
                            MoveInfo.Text = "Heavy tech. POW should be over 250 to unlock.\r\nLearning priority over RushingPunch.";
                            break;

                        case 9:
                            MoveName.Text = "HundredBlows";
                            LazyDataFill(false, 50, 39, 11, 19, 10, 13);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_Stats[1] + Mon_Stats[3] >= 800);
                            MoveInfo.Text = "Special tech. POW + SKI should total over 800 to unlock.\r\nChains into MillionBlows when used 50x.";
                            break;

                        case 10:
                            MoveName.Text = "MillionBlows";
                            LazyDataFill(false, 55, 43, 10, 21, 15, 14);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_MoveUsed[13] >= 50);
                            MoveInfo.Text = "Special tech. Chains from 50 HundredBlows. No stat requirement.";
                            break;

                        case 11:
                            MoveName.Text = "Twiddling";
                            LazyDataFill(false, 23, 22, -4, 8, 19, 15);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_Stats[1] + Mon_Stats[4] >= 650);
                            MoveInfo.Text = "Sharp tech. POW + SPD should total over 650 to unlock.\r\nChains into Twiddling-2 when used 50x. Learning priority over Head Butt.";
                            break;

                        case 12:
                            MoveName.Text = "Twiddling-2";
                            LazyDataFill(false, 27, 30, -7, 8, 26, 16);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_MoveUsed[15] >= 50);
                            MoveInfo.Text = "Sharp tech. Chains from 50 Twiddling.\r\nChains into Twiddling-Z when used 50x. No stat requirement.";
                            break;

                        case 13:
                            MoveName.Text = "Twiddling-Z";
                            LazyDataFill(false, 39, 37, -10, 18, 30, 17);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_MoveUsed[16] >= 50);
                            MoveInfo.Text = "Special tech. Chains from 50 Twiddling-2. No stat requirement.";
                            break;

                        case 14:
                            MoveName.Text = "Miaow";
                            LazyDataFill(true, 30, 2, 15, 34, 5, 18);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_Stats[2] >= 450);
                            MoveInfo.Text = "Withering tech. INT should be over 450 to unlock.\r\nChains into Song Of Mew when used 50x. Learning priority over Stab.";
                            break;

                        case 15:
                            MoveName.Text = "Song Of Mew";
                            LazyDataFill(true, 39, 15, 6, 39, 5, 19);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_MoveUsed[18] >= 50);
                            MoveInfo.Text = "Withering tech. Chains from 50 Miaow.\r\nChains into Recital when used 50x. No stat requirement.";
                            break;

                        case 16:
                            MoveName.Text = "Recital";
                            LazyDataFill(true, 40, 25, -2, 45, 10, 20);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_MoveUsed[19] >= 50);
                            MoveInfo.Text = "Special tech. Chains from 50 Song Of Mew. No stat requirement.";
                            break;

                        case 17:
                            MoveName.Text = "Zap";
                            LazyDataFill(true, 40, 41, -12, 41, 10, 21);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_Stats[2] >= 500);
                            MoveInfo.Text = "Special tech. INT should be over 500 to unlock. Chains into Maximal Zap when used 50x.";
                            break;

                        case 18:
                            MoveName.Text = "Maximal Zap";
                            LazyDataFill(true, 50, 59, -16, 45, 20, 22);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_MoveUsed[21] >= 50);
                            MoveInfo.Text = "Special tech. Chains from 50 Zap. No stat requirement.";
                            break;

                        default:
                            MoveName.Text = "No Move"; MoveName.ForeColor = Color.Black;
                            MoveGuts.Text = "---";
                            MoveDamage.Text = "---";
                            MoveHit.Text = "---";
                            MoveGD.Text = "---";
                            MoveSharp.Text = "---";
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = "---";
                            MoveUnlocked.Checked = false;
                            CanUnlock.Checked = false;
                            MoveInfo.Text = "Click on a move to learn more.";
                            break;
                    }
                    break;

                case 20: // Phoenix
                    switch (MoveSelected)
                    {
                        case 1:
                            MoveName.Text = "Beak"; MoveName.ForeColor = PowCol;
                            MoveGuts.Text = "13";
                            MoveDamage.Text = GenerateStatValue(0, 19);
                            MoveHit.Text = GenerateStatValue(1, 0);
                            MoveGD.Text = "---";
                            MoveSharp.Text = "---";
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[0].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[0] == 1);
                            CanUnlock.Checked = true;
                            PlaceHighlight(0);
                            MoveInfo.Text = "Starting basic tech.";
                            break;
                        case 2:
                            MoveName.Text = "Talons"; MoveName.ForeColor = PowCol;
                            MoveGuts.Text = "11";
                            MoveDamage.Text = GenerateStatValue(0, 15);
                            MoveHit.Text = GenerateStatValue(1, 5);
                            MoveGD.Text = "---";
                            MoveSharp.Text = "---";
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[6].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[6] == 1);
                            CanUnlock.Checked = true;
                            PlaceHighlight(6);
                            MoveInfo.Text = "Starting basic tech.";
                            break;
                        case 3:
                            MoveName.Text = "Rapid Beaks"; MoveName.ForeColor = PowCol;
                            MoveGuts.Text = "23";
                            MoveDamage.Text = GenerateStatValue(0, 25); //bedeg
                            MoveHit.Text = GenerateStatValue(1, 10); //bedeg
                            MoveGD.Text = GenerateStatValue(2, 5); //bedeg
                            MoveSharp.Text = GenerateStatValue(3, 5); //bedeg
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[7].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[7] == 1);
                            CanUnlock.Checked = (Mon_Stats[1] + Mon_Stats[3] >= 400);
                            PlaceHighlight(7);
                            MoveInfo.Text = "Hit tech. POW + SKI should total over 400 to unlock.";
                            break;
                        case 4:
                            MoveName.Text = "Flame Shot"; MoveName.ForeColor = IntCol;
                            MoveGuts.Text = "19";
                            MoveDamage.Text = GenerateStatValue(0, 15);
                            MoveHit.Text = GenerateStatValue(1, -3);
                            MoveGD.Text = GenerateStatValue(2, 19);
                            MoveSharp.Text = GenerateStatValue(3, 5);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[12].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[12] == 1);
                            CanUnlock.Checked = (Mon_Stats[2] >= 250);
                            PlaceHighlight(12);
                            MoveInfo.Text = "Withering tech. INT should be over 250 to unlock. Chains into Flame Cannon when used 50x.";
                            break;
                        case 5:
                            MoveName.Text = "Flame Cannon"; MoveName.ForeColor = IntCol;
                            MoveGuts.Text = "30";
                            MoveDamage.Text = GenerateStatValue(0, 18);
                            MoveHit.Text = GenerateStatValue(1, -4);
                            MoveGD.Text = GenerateStatValue(2, 40);
                            MoveSharp.Text = GenerateStatValue(3, 5);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[13].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[13] == 1);
                            CanUnlock.Checked = (Mon_MoveUsed[12] >= 50 && Mon_Nature <= -20); //bedeg
                            PlaceHighlight(13);
                            MoveInfo.Text = "Withering tech. Chains from 50 Flame Shots. Requires Bad (-20) Nature.";
                            break;
                        case 6:
                            MoveName.Text = "Fire Twister"; MoveName.ForeColor = IntCol;
                            MoveGuts.Text = "20";
                            MoveDamage.Text = GenerateStatValue(0, 22);
                            MoveHit.Text = GenerateStatValue(1, -4);
                            MoveGD.Text = GenerateStatValue(2, 8);
                            MoveSharp.Text = GenerateStatValue(3, 16);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[14].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[14] == 1);
                            CanUnlock.Checked = (Mon_Stats[2] + Mon_Stats[4] >= 400);
                            PlaceHighlight(14);
                            MoveInfo.Text = "Sharp tech. INT + SPD should total over 400 to unlock. Chains into Fire Tornado when used 50x.";
                            break;
                        case 7:
                            MoveName.Text = "Fire Tornado"; MoveName.ForeColor = IntCol;
                            MoveGuts.Text = "26";
                            MoveDamage.Text = GenerateStatValue(0, 31);
                            MoveHit.Text = GenerateStatValue(1, -10);
                            MoveGD.Text = GenerateStatValue(2, 9);
                            MoveSharp.Text = GenerateStatValue(3, 22);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[15].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[15] == 1);
                            CanUnlock.Checked = (Mon_MoveUsed[14] >= 50 && Mon_Nature >= 20); //bedeg
                            PlaceHighlight(15);
                            MoveInfo.Text = "Sharp tech. Chains from 50 Fire Twisters. Requires Good (+20) Nature.";
                            break;
                        case 8:
                            MoveName.Text = "Heat Beam"; MoveName.ForeColor = IntCol;
                            MoveGuts.Text = "29";
                            MoveDamage.Text = GenerateStatValue(0, 46);
                            MoveHit.Text = GenerateStatValue(1, -13);
                            MoveGD.Text = GenerateStatValue(2, 8);
                            MoveSharp.Text = GenerateStatValue(3, 5);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[18].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[18] == 1);
                            CanUnlock.Checked = (Mon_Stats[2] >= 250);
                            PlaceHighlight(18);
                            MoveInfo.Text = "Heavy tech. INT should be over 250 to unlock.";
                            break;
                        case 9:
                            MoveName.Text = "Fire Stream"; MoveName.ForeColor = IntCol;
                            MoveGuts.Text = "45";
                            MoveDamage.Text = GenerateStatValue(0, 39);
                            MoveHit.Text = GenerateStatValue(1, 3);
                            MoveGD.Text = GenerateStatValue(2, 16);
                            MoveSharp.Text = GenerateStatValue(3, 10);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[19].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[19] == 1);
                            CanUnlock.Checked = (Mon_Stats[2] >= 500);
                            PlaceHighlight(19);
                            MoveInfo.Text = "Special tech. INT should be over 500 to unlock. Chains into Fire Wave when used 50x.";
                            break;
                        case 10:
                            MoveName.Text = "Fire Wave"; MoveName.ForeColor = IntCol;
                            MoveGuts.Text = "50";
                            MoveDamage.Text = GenerateStatValue(0, 50);
                            MoveHit.Text = GenerateStatValue(1, -1);
                            MoveGD.Text = GenerateStatValue(2, 18);
                            MoveSharp.Text = GenerateStatValue(3, 15);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[20].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[20] == 1);
                            CanUnlock.Checked = (Mon_MoveUsed[19] >= 50);
                            PlaceHighlight(20);
                            MoveInfo.Text = "Special tech. Chains from 50 Fire Streams.";
                            break;
                        default:
                            MoveName.Text = "No Move"; MoveName.ForeColor = Color.Black;
                            MoveGuts.Text = "---";
                            MoveDamage.Text = "---";
                            MoveHit.Text = "---";
                            MoveGD.Text = "---";
                            MoveSharp.Text = "---";
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = "---";
                            MoveUnlocked.Checked = false;
                            CanUnlock.Checked = false;
                            MoveInfo.Text = "Click on a move to learn more.";
                            break;
                    }
                    break;
                case 21: // Ghost
                    switch (MoveSelected)
                    {
                        case 1:
                            MoveName.Text = "Toy Hammer"; MoveName.ForeColor = PowCol;
                            MoveGuts.Text = "11";
                            MoveDamage.Text = GenerateStatValue(0, 12); //bedeg
                            MoveHit.Text = GenerateStatValue(1, -5);
                            MoveGD.Text = "---";
                            MoveSharp.Text = "---";
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[0].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[0] == 1);
                            CanUnlock.Checked = true;
                            PlaceHighlight(0);
                            MoveInfo.Text = "Starting basic tech.";
                            break;
                        case 2:
                            MoveName.Text = "Charge"; MoveName.ForeColor = PowCol;
                            MoveGuts.Text = "10";
                            MoveDamage.Text = GenerateStatValue(0, 9);
                            MoveHit.Text = GenerateStatValue(1, 3);
                            MoveGD.Text = "---";
                            MoveSharp.Text = "---";
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[1].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[1] == 1);
                            CanUnlock.Checked = true;
                            PlaceHighlight(1);
                            MoveInfo.Text = "Starting basic tech.";
                            break;
                        case 3:
                            MoveName.Text = "Uppercut"; MoveName.ForeColor = PowCol;
                            MoveGuts.Text = "50";
                            MoveDamage.Text = GenerateStatValue(0, 30);
                            MoveHit.Text = GenerateStatValue(1, 1);
                            MoveGD.Text = GenerateStatValue(2, 25);
                            MoveSharp.Text = GenerateStatValue(3, 5);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[2].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[2] == 1);
                            CanUnlock.Checked = (Mon_Stats[1] + Mon_Stats[2] >= 600);
                            PlaceHighlight(2);
                            MoveInfo.Text = "Special tech. Chains into Combination when used 25x. POW + INT should total over 600 to unlock.";
                            break;
                        case 4:
                            MoveName.Text = "Combination"; MoveName.ForeColor = PowCol;
                            MoveGuts.Text = "55";
                            MoveDamage.Text = GenerateStatValue(0, 35);
                            MoveHit.Text = GenerateStatValue(1, 1);
                            MoveGD.Text = GenerateStatValue(2, 30);
                            MoveSharp.Text = GenerateStatValue(3, 5);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[3].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[3] == 1);
                            CanUnlock.Checked = (Mon_MoveUsed[2] >= 25 && (Mon_Stats[1] + Mon_Stats[2] >= 800));
                            PlaceHighlight(3);
                            MoveInfo.Text = "Special tech. Chains from 25 Uppercuts. POW + INT should total over 800 to unlock.";
                            break;
                        case 5:
                            MoveName.Text = "Energy Shot"; MoveName.ForeColor = IntCol;
                            MoveGuts.Text = "16";
                            MoveDamage.Text = GenerateStatValue(0, 8);
                            MoveHit.Text = GenerateStatValue(1, 15);
                            MoveGD.Text = GenerateStatValue(2, 5);
                            MoveSharp.Text = GenerateStatValue(3, 5);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[6].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[6] == 1);
                            CanUnlock.Checked = (Mon_Stats[2] + Mon_Stats[3] >= 400);
                            PlaceHighlight(6);
                            MoveInfo.Text = "Hit tech. Chains into Necromancy when used 50x. INT + SKI should total over 400 to unlock.";
                            break;
                        case 6:
                            MoveName.Text = "Surprise"; MoveName.ForeColor = IntCol;
                            MoveGuts.Text = "17";
                            MoveDamage.Text = GenerateStatValue(0, 7); //bedeg
                            MoveHit.Text = GenerateStatValue(1, -10);
                            MoveGD.Text = GenerateStatValue(2, 20);
                            MoveSharp.Text = GenerateStatValue(3, 5);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[7].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[7] == 1);
                            CanUnlock.Checked = (Mon_Stats[2] >= 250);
                            PlaceHighlight(7);
                            MoveInfo.Text = "Withering tech. Chains into Astonishment when used 25x. INT should be over 250 to unlock.";
                            break;
                        case 7:
                            MoveName.Text = "Astonishment"; MoveName.ForeColor = IntCol;
                            MoveGuts.Text = "32";
                            MoveDamage.Text = GenerateStatValue(0, 11);
                            MoveHit.Text = GenerateStatValue(1, -7);
                            MoveGD.Text = GenerateStatValue(2, 36);
                            MoveSharp.Text = GenerateStatValue(3, 5);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[8].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[8] == 1);
                            CanUnlock.Checked = (Mon_MoveUsed[7] >= 25 && (Mon_Stats[2] >= 450));
                            PlaceHighlight(8);
                            MoveInfo.Text = "Withering tech. Chains from 25 Surprises. INT should be over 450 to unlock.";
                            break;
                        case 8:
                            MoveName.Text = "Necromancy"; MoveName.ForeColor = IntCol;
                            MoveGuts.Text = "25";
                            MoveDamage.Text = GenerateStatValue(0, 8);
                            MoveHit.Text = GenerateStatValue(1, 29);
                            MoveGD.Text = GenerateStatValue(2, 10);
                            MoveSharp.Text = GenerateStatValue(3, 5);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[12].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[12] == 1);
                            CanUnlock.Checked = (Mon_MoveUsed[6] >= 50 && (Mon_Stats[2] + Mon_Stats[3] >= 650));
                            PlaceHighlight(12);
                            MoveInfo.Text = "Hit tech. Chains from 50 Energy Shots. INT + SKI should total over 650 to unlock."; //bedeg
                            break;
                        case 9:
                            MoveName.Text = "Dove Bomb"; MoveName.ForeColor = IntCol;
                            MoveGuts.Text = "23";
                            MoveDamage.Text = GenerateStatValue(0, 25);
                            MoveHit.Text = GenerateStatValue(1, -17);
                            MoveGD.Text = GenerateStatValue(2, 10);
                            MoveSharp.Text = GenerateStatValue(3, 5);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[13].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[13] == 1);
                            CanUnlock.Checked = (Mon_Stats[2] >= 250);
                            PlaceHighlight(13);
                            MoveInfo.Text = "Heavy tech. Chains into Pigeon Bomb when used 50x. INT should be over 250 to unlock.";
                            break;
                        case 10:
                            MoveName.Text = "Pigeon Bomb"; MoveName.ForeColor = IntCol;
                            MoveGuts.Text = "30";
                            MoveDamage.Text = GenerateStatValue(0, 34);
                            MoveHit.Text = GenerateStatValue(1, -17);
                            MoveGD.Text = GenerateStatValue(2, 10);
                            MoveSharp.Text = GenerateStatValue(3, 5);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[14].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[14] == 1);
                            CanUnlock.Checked = (Mon_MoveUsed[13] >= 50 && (Mon_Stats[2] >= 450));
                            PlaceHighlight(14);
                            MoveInfo.Text = "Heavy tech. Chains from 50 Dove Bombs. INT should be over 450 to unlock.";
                            break;
                        case 11:
                            MoveName.Text = "Magic Card";
                            MoveName.ForeColor = IntCol;
                            MoveGuts.Text = "19";
                            MoveDamage.Text = GenerateStatValue(0, 11);
                            MoveHit.Text = GenerateStatValue(1, -5);
                            MoveGD.Text = GenerateStatValue(2, 10);
                            MoveSharp.Text = GenerateStatValue(3, 15);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[18].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[18] == 1);
                            CanUnlock.Checked = (Mon_Stats[2] + Mon_Stats[4] >= 400);
                            PlaceHighlight(18);
                            MoveInfo.Text = "Sharp tech. Chains into Magic Cards when used 25x. INT + SPD should total over 400 to unlock.";
                            break;
                        case 12:
                            MoveName.Text = "Magic Cards"; MoveName.ForeColor = IntCol;
                            MoveGuts.Text = "29";
                            MoveDamage.Text = GenerateStatValue(0, 21);
                            MoveHit.Text = GenerateStatValue(1, -10);
                            MoveGD.Text = GenerateStatValue(2, 10);
                            MoveSharp.Text = GenerateStatValue(3, 18);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[19].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[19] == 1);
                            CanUnlock.Checked = (Mon_MoveUsed[18] >= 25 && (Mon_Stats[2] + Mon_Stats[4] >= 650));
                            PlaceHighlight(19);
                            MoveInfo.Text = "Sharp tech. Chains from 50 uses of Magic Card. INT + SPD should total over 650 to unlock.";
                            break;
                        default:
                            MoveName.Text = "No Move"; MoveName.ForeColor = Color.Black;
                            MoveGuts.Text = "---";
                            MoveDamage.Text = "---";
                            MoveHit.Text = "---";
                            MoveGD.Text = "---";
                            MoveSharp.Text = "---";
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = "---";
                            MoveUnlocked.Checked = false;
                            CanUnlock.Checked = false;
                            MoveInfo.Text = "Click on a move to learn more.";
                            break;
                    }
                    break;
                case 22: // Metalner
                    switch (MoveSelected)
                    {
                        case 1:
                            MoveName.Text = "Left Slap"; MoveName.ForeColor = PowCol;
                            MoveGuts.Text = "10";
                            MoveDamage.Text = GenerateStatValue(0, 7);
                            MoveHit.Text = GenerateStatValue(1, 10);
                            MoveGD.Text = GenerateStatValue(2, 5);
                            MoveSharp.Text = "---";
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[0].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[0] == 1);
                            CanUnlock.Checked = true;
                            PlaceHighlight(0);
                            MoveInfo.Text = "Starting basic tech.";
                            break;
                        case 2:
                            MoveName.Text = "Back Charge"; MoveName.ForeColor = PowCol;
                            MoveGuts.Text = "29";
                            MoveDamage.Text = GenerateStatValue(0, 26);
                            MoveHit.Text = GenerateStatValue(1, -8);
                            MoveGD.Text = GenerateStatValue(2, 26);
                            MoveSharp.Text = GenerateStatValue(3, 35);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[1].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[1] == 1);
                            CanUnlock.Checked = (Mon_Stats[1] + Mon_Stats[4] >= 650);
                            PlaceHighlight(1);
                            MoveInfo.Text = "Sharp tech. POW + SPD should total over 650 to unlock.";
                            break;
                        case 3:
                            MoveName.Text = "Right Slap"; MoveName.ForeColor = PowCol;
                            MoveGuts.Text = "12";
                            MoveDamage.Text = GenerateStatValue(0, 10);
                            MoveHit.Text = GenerateStatValue(1, 8);
                            MoveGD.Text = GenerateStatValue(2, 5);
                            MoveSharp.Text = GenerateStatValue(3, 5);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[6].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[6] == 1);
                            CanUnlock.Checked = true;
                            PlaceHighlight(6);
                            MoveInfo.Text = "Starting basic tech.";
                            break;
                        case 4:
                            MoveName.Text = "Straight"; MoveName.ForeColor = PowCol;
                            MoveGuts.Text = "18";
                            MoveDamage.Text = GenerateStatValue(0, 15);
                            MoveHit.Text = GenerateStatValue(1, 10);
                            MoveGD.Text = GenerateStatValue(2, 10);
                            MoveSharp.Text = GenerateStatValue(3, 15);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[7].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[7] == 1);
                            CanUnlock.Checked = (Mon_Stats[1] + Mon_Stats[3] >= 400);
                            PlaceHighlight(7);
                            MoveInfo.Text = "Hit tech. Chains into Dash Straight when used 50x. POW + SKI should total over 450 to unlock.";
                            break;
                        case 5:
                            MoveName.Text = "High Kick"; MoveName.ForeColor = PowCol;
                            MoveGuts.Text = "20";
                            MoveDamage.Text = GenerateStatValue(0, 15);
                            MoveHit.Text = GenerateStatValue(1, 2);
                            MoveGD.Text = GenerateStatValue(2, 10);
                            MoveSharp.Text = GenerateStatValue(3, 25);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[8].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[8] == 1);
                            CanUnlock.Checked = (Mon_Stats[1] + Mon_Stats[4] >= 400);
                            PlaceHighlight(8);
                            MoveInfo.Text = "Sharp tech. Chains into Double Kicks when used 50x. POW + SPD should total over 400 to unlock.";
                            break;
                        case 6:
                            MoveName.Text = "Double Kicks"; MoveName.ForeColor = PowCol;
                            MoveGuts.Text = "25";
                            MoveDamage.Text = GenerateStatValue(0, 19);
                            MoveHit.Text = GenerateStatValue(1, 0);
                            MoveGD.Text = GenerateStatValue(2, 10);
                            MoveSharp.Text = GenerateStatValue(3, 30);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[9].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[9] == 1);
                            CanUnlock.Checked = (Mon_MoveUsed[8] >= 50);
                            PlaceHighlight(9);
                            MoveInfo.Text = "Sharp tech. Chains from 50 High Kicks.";
                            break;
                        case 7:
                            MoveName.Text = "Dash Straight"; MoveName.ForeColor = PowCol;
                            MoveGuts.Text = "27";
                            MoveDamage.Text = GenerateStatValue(0, 20);
                            MoveHit.Text = GenerateStatValue(1, 10);
                            MoveGD.Text = GenerateStatValue(2, 13);
                            MoveSharp.Text = GenerateStatValue(3, 15);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[12].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[12] == 1);
                            CanUnlock.Checked = (Mon_MoveUsed[7] >= 50);
                            PlaceHighlight(12);
                            MoveInfo.Text = "Hit tech. Chains from 50 Straights.";
                            break;
                        case 8:
                            MoveName.Text = "Elbow Strike"; MoveName.ForeColor = PowCol;
                            MoveGuts.Text = "30";
                            MoveDamage.Text = GenerateStatValue(0, 15);
                            MoveHit.Text = GenerateStatValue(1, 16);
                            MoveGD.Text = GenerateStatValue(2, 12);
                            MoveSharp.Text = GenerateStatValue(3, 15);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[13].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[13] == 1);
                            CanUnlock.Checked = (Mon_Stats[1] + Mon_Stats[3] >= 650);
                            PlaceHighlight(13);
                            MoveInfo.Text = "Hit tech. POW + SKI should total over 650 to unlock.";
                            break;
                        case 9:
                            MoveName.Text = "Double Palms"; MoveName.ForeColor = PowCol;
                            MoveGuts.Text = "20";
                            MoveDamage.Text = GenerateStatValue(0, 20);
                            MoveHit.Text = GenerateStatValue(1, -13);
                            MoveGD.Text = GenerateStatValue(2, 10);
                            MoveSharp.Text = GenerateStatValue(3, 15);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[14].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[14] == 1);
                            CanUnlock.Checked = (Mon_Stats[1] >= 250);
                            PlaceHighlight(14);
                            MoveInfo.Text = "Heavy tech. POW should be over 250 to unlock.";
                            break;
                        case 10:
                            MoveName.Text = "Palm Strike"; MoveName.ForeColor = PowCol;
                            MoveGuts.Text = "22";
                            MoveDamage.Text = GenerateStatValue(0, 14);
                            MoveHit.Text = GenerateStatValue(1, -8);
                            MoveGD.Text = GenerateStatValue(2, 25);
                            MoveSharp.Text = GenerateStatValue(3, 15);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[18].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[18] == 1);
                            CanUnlock.Checked = (Mon_Stats[1] + Mon_Stats[2] >= 400);
                            PlaceHighlight(18);
                            MoveInfo.Text = "Withering tech. POW + INT should total over 400 to unlock.";
                            break;
                        case 11:
                            MoveName.Text = "Metalner Ray"; MoveName.ForeColor = IntCol;
                            MoveGuts.Text = "50";
                            MoveDamage.Text = GenerateStatValue(0, 30);
                            MoveHit.Text = GenerateStatValue(1, -6);
                            MoveGD.Text = GenerateStatValue(2, 40);
                            MoveSharp.Text = GenerateStatValue(3, 15);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[19].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[19] == 1);
                            CanUnlock.Checked = (Mon_Stats[2] >= 500 && Mon_Nature <= -20); //bedeg
                            PlaceHighlight(19);
                            MoveInfo.Text = "Special tech. INT should be over 500 to unlock. Requires Bad (-20) Nature.";
                            break;
                        case 12:
                            MoveName.Text = "Burning Palm"; MoveName.ForeColor = PowCol;
                            MoveGuts.Text = "50";
                            MoveDamage.Text = GenerateStatValue(0, 45);
                            MoveHit.Text = GenerateStatValue(1, -11);
                            MoveGD.Text = GenerateStatValue(2, 25);
                            MoveSharp.Text = GenerateStatValue(3, 15);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[20].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[20] == 1);
                            CanUnlock.Checked = (Mon_Stats[1] >= 500 && Mon_Nature >= 20); //bedeg
                            PlaceHighlight(20);
                            MoveInfo.Text = "Special tech. POW should be over 500 to unlock. Requires Good (+20) Nature.";
                            break;
                        case 13:
                            MoveName.Text = "UFO Attack"; MoveName.ForeColor = PowCol;
                            MoveGuts.Text = "30";
                            MoveDamage.Text = GenerateStatValue(0, 50);
                            MoveHit.Text = GenerateStatValue(1, -22);
                            MoveGD.Text = GenerateStatValue(2, 17);
                            MoveSharp.Text = GenerateStatValue(3, 15);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[21].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[21] == 1);
                            CanUnlock.Checked = (Mon_Stats[1] >= 450);
                            PlaceHighlight(21);
                            MoveInfo.Text = "Heavy tech. POW should be over 450 to unlock.";
                            break;
                        default:
                            MoveName.Text = "No Move"; MoveName.ForeColor = Color.Black;
                            MoveGuts.Text = "---";
                            MoveDamage.Text = "---";
                            MoveHit.Text = "---";
                            MoveGD.Text = "---";
                            MoveSharp.Text = "---";
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = "---";
                            MoveUnlocked.Checked = false;
                            CanUnlock.Checked = false;
                            MoveInfo.Text = "Click on a move to learn more.";
                            break;
                    }
                    break;
                case 23: // Swayso
                    switch (MoveSelected)
                    {
                        case 1:
                            MoveName.Text = "Tail Assault"; MoveName.ForeColor = PowCol;
                            MoveGuts.Text = "12";
                            MoveDamage.Text = GenerateStatValue(0, 14);
                            MoveHit.Text = GenerateStatValue(1, -4);
                            MoveGD.Text = GenerateStatValue(2, 5);
                            MoveSharp.Text = "---";
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[0].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[0] == 1);
                            CanUnlock.Checked = true;
                            PlaceHighlight(0);
                            MoveInfo.Text = "Starting basic tech.";
                            break;
                        case 2:
                            MoveName.Text = "Tongue Slap"; MoveName.ForeColor = PowCol;
                            MoveGuts.Text = "33";
                            MoveDamage.Text = GenerateStatValue(0, 42);
                            MoveHit.Text = GenerateStatValue(1, -16);
                            MoveGD.Text = GenerateStatValue(2, 25);
                            MoveSharp.Text = GenerateStatValue(3, 5);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[1].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[1] == 1);
                            CanUnlock.Checked = (Mon_Stats[1] >= 450);
                            PlaceHighlight(1);
                            MoveInfo.Text = "Heavy tech. POW should be over 450 to unlock.";
                            break;
                        case 3:
                            MoveName.Text = "Kiss"; MoveName.ForeColor = IntCol;
                            MoveGuts.Text = "29"; //bedeg
                            MoveDamage.Text = GenerateStatValue(0, 16); //bedeg
                            MoveHit.Text = GenerateStatValue(1, -8); //bedeg
                            MoveGD.Text = GenerateStatValue(2, 39); //bedeg
                            MoveSharp.Text = GenerateStatValue(3, 5); //bedeg
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[2].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[2] == 1);
                            CanUnlock.Checked = (Mon_Stats[2] >= 450);
                            PlaceHighlight(2);
                            MoveInfo.Text = "Withering tech. INT should be over 450 to unlock.";
                            break;
                        case 4:
                            MoveName.Text = "Spit"; MoveName.ForeColor = IntCol;
                            MoveGuts.Text = "10";
                            MoveDamage.Text = GenerateStatValue(0, 10);
                            MoveHit.Text = GenerateStatValue(1, 9);
                            MoveGD.Text = GenerateStatValue(2, 5);
                            MoveSharp.Text = "---";
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[6].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[6] == 1);
                            CanUnlock.Checked = true;
                            PlaceHighlight(6);
                            MoveInfo.Text = "Starting basic tech.";
                            break;
                        case 5:
                            MoveName.Text = "Bite"; MoveName.ForeColor = PowCol;
                            MoveGuts.Text = "22";
                            MoveDamage.Text = GenerateStatValue(0, 25);
                            MoveHit.Text = GenerateStatValue(1, -12);
                            MoveGD.Text = GenerateStatValue(2, 15);
                            MoveSharp.Text = GenerateStatValue(3, 5);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[7].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[7] == 1);
                            CanUnlock.Checked = (Mon_Stats[1] >= 250); //bedeg
                            PlaceHighlight(7);
                            MoveInfo.Text = "Heavy tech. POW should be over 250 to unlock.";
                            break;
                        case 6:
                            MoveName.Text = "Lick"; MoveName.ForeColor = IntCol;
                            MoveGuts.Text = "22";
                            MoveDamage.Text = GenerateStatValue(0, 13);
                            MoveHit.Text = GenerateStatValue(1, -7);
                            MoveGD.Text = GenerateStatValue(2, 29);
                            MoveSharp.Text = GenerateStatValue(3, 5);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[8].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[8] == 1);
                            CanUnlock.Checked = (Mon_Stats[2] >= 250);
                            PlaceHighlight(8);
                            MoveInfo.Text = "Withering tech. INT should be over 250 to unlock.";
                            break;
                        case 7:
                            MoveName.Text = "Chewing"; MoveName.ForeColor = PowCol;
                            MoveGuts.Text = "45";
                            MoveDamage.Text = GenerateStatValue(0, 36);
                            MoveHit.Text = GenerateStatValue(1, 4);
                            MoveGD.Text = GenerateStatValue(2, 22);
                            MoveSharp.Text = GenerateStatValue(3, 5);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[9].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[9] == 1);
                            CanUnlock.Checked = (Mon_Stats[1] + Mon_Stats[2] >= 800);
                            PlaceHighlight(9);
                            MoveInfo.Text = "Special tech. POW + INT should total over 800 to unlock.";
                            break;
                        case 8:
                            MoveName.Text = "Teleport"; MoveName.ForeColor = PowCol;
                            MoveGuts.Text = "16";
                            MoveDamage.Text = GenerateStatValue(0, 12);
                            MoveHit.Text = GenerateStatValue(1, 10);
                            MoveGD.Text = GenerateStatValue(2, 10);
                            MoveSharp.Text = GenerateStatValue(3, 5);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[12].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[12] == 1);
                            CanUnlock.Checked = (Mon_Stats[1] + Mon_Stats[3] >= 400);
                            PlaceHighlight(12);
                            MoveInfo.Text = "Hit tech. POW + SKI should total over 400 to unlock.";
                            break;
                        case 9:
                            MoveName.Text = "Telekinesis"; MoveName.ForeColor = IntCol;
                            MoveGuts.Text = "23";
                            MoveDamage.Text = GenerateStatValue(0, 14);
                            MoveHit.Text = GenerateStatValue(1, 21);
                            MoveGD.Text = GenerateStatValue(2, 10);
                            MoveSharp.Text = GenerateStatValue(3, 5);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[13].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[13] == 1);
                            CanUnlock.Checked = (Mon_Stats[2] + Mon_Stats[3] >= 650);
                            PlaceHighlight(13);
                            MoveInfo.Text = "Hit tech. INT + SKI should total over 650 to unlock.";
                            break;
                        case 10:
                            MoveName.Text = "Telepathy"; MoveName.ForeColor = IntCol;
                            MoveGuts.Text = "21";
                            MoveDamage.Text = GenerateStatValue(0, 17);
                            MoveHit.Text = GenerateStatValue(1, -5);
                            MoveGD.Text = GenerateStatValue(2, 12);
                            MoveSharp.Text = GenerateStatValue(3, 15);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[14].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[14] == 1);
                            CanUnlock.Checked = (Mon_Stats[2] + Mon_Stats[4] >= 400);
                            PlaceHighlight(14);
                            MoveInfo.Text = "Sharp tech. INT + SPD should total over 400 to unlock.";
                            break;
                        case 11:
                            MoveName.Text = "Eye Beam"; MoveName.ForeColor = IntCol;
                            MoveGuts.Text = "31";
                            MoveDamage.Text = GenerateStatValue(0, 28);
                            MoveHit.Text = GenerateStatValue(1, -7);
                            MoveGD.Text = GenerateStatValue(2, 10);
                            MoveSharp.Text = GenerateStatValue(3, 22);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[18].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[18] == 1);
                            CanUnlock.Checked = (Mon_Stats[2] + Mon_Stats[4] >= 650);
                            PlaceHighlight(18);
                            MoveInfo.Text = "Sharp tech. INT + SPD should total over 650 to unlock.";
                            break;
                        case 12:
                            MoveName.Text = "Yodel"; MoveName.ForeColor = IntCol;
                            MoveGuts.Text = "40"; //bedeg
                            MoveDamage.Text = GenerateStatValue(0, 25);
                            MoveHit.Text = GenerateStatValue(1, -5);
                            MoveGD.Text = GenerateStatValue(2, 45);
                            MoveSharp.Text = GenerateStatValue(3, 5);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[19].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[19] == 1);
                            CanUnlock.Checked = (Mon_Stats[2] + Mon_Stats[4] >= 800);
                            PlaceHighlight(19);
                            MoveInfo.Text = "Special tech. INT + SPD should total over 800 to unlock.";
                            break;
                        default:
                            MoveName.Text = "No Move"; MoveName.ForeColor = Color.Black;
                            MoveGuts.Text = "---";
                            MoveDamage.Text = "---";
                            MoveHit.Text = "---";
                            MoveGD.Text = "---";
                            MoveSharp.Text = "---";
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = "---";
                            MoveUnlocked.Checked = false;
                            CanUnlock.Checked = false;
                            MoveInfo.Text = "Click on a move to learn more.";
                            break;
                    }
                    break;
                case 24: // Jill
                    switch (MoveSelected)
                    {
                        case 1:
                            MoveName.Text = "Double Slaps"; MoveName.ForeColor = PowCol;
                            MoveGuts.Text = "10";
                            MoveDamage.Text = GenerateStatValue(0, 13);
                            MoveHit.Text = GenerateStatValue(1, 0);
                            MoveGD.Text = GenerateStatValue(2, 5);
                            MoveSharp.Text = "---";
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[0].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[0] == 1);
                            CanUnlock.Checked = true;
                            PlaceHighlight(0);
                            MoveInfo.Text = "Starting basic tech.";
                            break;
                        case 2:
                            MoveName.Text = "Ice Spikes"; MoveName.ForeColor = IntCol;
                            MoveGuts.Text = "17";
                            MoveDamage.Text = GenerateStatValue(0, 20);
                            MoveHit.Text = GenerateStatValue(1, 9);
                            MoveGD.Text = "---";
                            MoveSharp.Text = GenerateStatValue(3, 5);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[1].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[1] == 1);
                            CanUnlock.Checked = (Mon_Stats[2] + Mon_Stats[3] >= 400);
                            PlaceHighlight(1);
                            MoveInfo.Text = "Hit tech. INT + SKI should total over 400 to unlock.";
                            break;
                        case 3:
                            MoveName.Text = "Clap Attack"; MoveName.ForeColor = PowCol;
                            MoveGuts.Text = "18";
                            MoveDamage.Text = GenerateStatValue(0, 15);
                            MoveHit.Text = GenerateStatValue(1, 1);
                            MoveGD.Text = GenerateStatValue(2, 16);
                            MoveSharp.Text = GenerateStatValue(3, 5);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[2].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[2] == 1);
                            CanUnlock.Checked = (Mon_Stats[2] >= 250);
                            PlaceHighlight(2);
                            MoveInfo.Text = "Withering tech. INT should be over 250 to unlock.";
                            break;
                        case 4:
                            MoveName.Text = "1-2-Straight"; MoveName.ForeColor = PowCol;
                            MoveGuts.Text = "12";
                            MoveDamage.Text = GenerateStatValue(0, 15);
                            MoveHit.Text = GenerateStatValue(1, 6);
                            MoveGD.Text = "---";
                            MoveSharp.Text = GenerateStatValue(3, 5);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[6].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[6] == 1);
                            CanUnlock.Checked = true;
                            PlaceHighlight(6);
                            MoveInfo.Text = "Starting basic tech.";
                            break;
                        case 5:
                            MoveName.Text = "Punch Combo"; MoveName.ForeColor = PowCol;
                            MoveGuts.Text = "29";
                            MoveDamage.Text = GenerateStatValue(0, 42);
                            MoveHit.Text = GenerateStatValue(1, -12);
                            MoveGD.Text = GenerateStatValue(2, 16);
                            MoveSharp.Text = GenerateStatValue(3, 6);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[7].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[7] == 1);
                            CanUnlock.Checked = (Mon_Stats[1] >= 450);
                            PlaceHighlight(7);
                            MoveInfo.Text = "Heavy tech. POW should be over 450 to unlock. Chains into Jill Combo when used 50x.";
                            break;
                        case 6:
                            MoveName.Text = "Slap Combo"; MoveName.ForeColor = PowCol;
                            MoveGuts.Text = "27";
                            MoveDamage.Text = GenerateStatValue(0, 21);
                            MoveHit.Text = GenerateStatValue(1, 1);
                            MoveGD.Text = GenerateStatValue(2, 25);
                            MoveSharp.Text = GenerateStatValue(3, 6);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[8].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[8] == 1);
                            CanUnlock.Checked = (Mon_Stats[1] + Mon_Stats[2] >= 650 && Mon_Nature <= -20);
                            PlaceHighlight(8);
                            MoveInfo.Text = "Withering tech. POW + INT should total over 650 to unlock. Requires Bad (-20) Nature.";
                            break;
                        case 7:
                            MoveName.Text = "Cold Breath"; MoveName.ForeColor = IntCol;
                            MoveGuts.Text = "26";
                            MoveDamage.Text = GenerateStatValue(0, 19);
                            MoveHit.Text = GenerateStatValue(1, 17);
                            MoveGD.Text = GenerateStatValue(2, 13);
                            MoveSharp.Text = GenerateStatValue(3, 5);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[12].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[12] == 1);
                            CanUnlock.Checked = (Mon_Stats[2] + Mon_Stats[3] >= 650);
                            PlaceHighlight(12);
                            MoveInfo.Text = "Hit tech. INT + SKI should total over 600 to unlock.";
                            break;
                        case 8:
                            MoveName.Text = "Ice Wave"; MoveName.ForeColor = IntCol;
                            MoveGuts.Text = "19";
                            MoveDamage.Text = GenerateStatValue(0, 33);
                            MoveHit.Text = GenerateStatValue(1, -10);
                            MoveGD.Text = "---";
                            MoveSharp.Text = GenerateStatValue(3, 5);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[13].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[13] == 1);
                            CanUnlock.Checked = (Mon_Stats[1] + Mon_Stats[2] >= 400);
                            PlaceHighlight(13);
                            MoveInfo.Text = "Heavy tech. POW + INT should total over 400 to unlock.";
                            break;
                        case 9:
                            MoveName.Text = "Frantic Rush"; MoveName.ForeColor = PowCol;
                            MoveGuts.Text = "19";
                            MoveDamage.Text = GenerateStatValue(0, 20);
                            MoveHit.Text = GenerateStatValue(1, -2);
                            MoveGD.Text = GenerateStatValue(2, 6);
                            MoveSharp.Text = GenerateStatValue(3, 16);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[14].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[14] == 1);
                            CanUnlock.Checked = (Mon_Stats[1] + Mon_Stats[4] >= 400);
                            PlaceHighlight(14);
                            MoveInfo.Text = "Sharp tech. POW + SPD should total over 400 to unlock.";
                            break;
                        case 10:
                            MoveName.Text = "Jill Combo"; MoveName.ForeColor = PowCol;
                            MoveGuts.Text = "45";
                            MoveDamage.Text = GenerateStatValue(0, 61);
                            MoveHit.Text = GenerateStatValue(1, -10);
                            MoveGD.Text = GenerateStatValue(2, 20);
                            MoveSharp.Text = GenerateStatValue(3, 8);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[15].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[15] == 1);
                            CanUnlock.Checked = ((Mon_Stats[1] >= 600) && Mon_MoveUsed[7] >= 50); //bedeg
                            PlaceHighlight(15);
                            MoveInfo.Text = "Special tech. Chains from 50 Punch Combos. POW should be over 600 to unlock.";
                            break;
                        case 11:
                            MoveName.Text = "Ice Meteor"; MoveName.ForeColor = IntCol;
                            MoveGuts.Text = "29";
                            MoveDamage.Text = GenerateStatValue(0, 29);
                            MoveHit.Text = GenerateStatValue(1, -5);
                            MoveGD.Text = GenerateStatValue(2, 13);
                            MoveSharp.Text = GenerateStatValue(3, 19);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[18].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[18] == 1);
                            CanUnlock.Checked = (Mon_Stats[2] + Mon_Stats[4] >= 650 && Mon_Nature >= 20); //bedeg
                            PlaceHighlight(18);
                            MoveInfo.Text = "Sharp tech. INT + SPD should total over 650 to unlock. Requires Good (+20) Nature.";
                            break;
                        case 12:
                            MoveName.Text = "Snowstorm"; MoveName.ForeColor = IntCol;
                            MoveGuts.Text = "45";
                            MoveDamage.Text = GenerateStatValue(0, 35);
                            MoveHit.Text = GenerateStatValue(1, 5);
                            MoveGD.Text = GenerateStatValue(2, 35);
                            MoveSharp.Text = GenerateStatValue(3, 5);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[19].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[19] == 1);
                            CanUnlock.Checked = (Mon_Stats[2] >= 500);
                            PlaceHighlight(19);
                            MoveInfo.Text = "Special tech. Requires 500+ INT to unlock.";
                            break;
                        default:
                            MoveName.Text = "No Move"; MoveName.ForeColor = Color.Black;
                            MoveGuts.Text = "---";
                            MoveDamage.Text = "---";
                            MoveHit.Text = "---";
                            MoveGD.Text = "---";
                            MoveSharp.Text = "---";
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = "---";
                            MoveUnlocked.Checked = false;
                            CanUnlock.Checked = false;
                            MoveInfo.Text = "Click on a move to learn more.";
                            break;
                    }
                    break;

                case 25: //Mocchi
                    switch (MoveSelected)
                    {
                        case 1:
                            MoveName.Text = "Slap";
                            LazyDataFill(false, 10, 9, 2, 5, 0, 0);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = true;
                            MoveInfo.Text = "Starting basic tech.";
                            break;

                        case 2:
                            MoveName.Text = "Thrust";
                            LazyDataFill(false, 15, 10, 6, 9, 6, 1);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_Stats[1] + Mon_Stats[3] >= 400);
                            MoveInfo.Text = "Hit tech. POW + SKI should total over 400 to unlock.\r\nChains into 1-2 Thrust when used 50x.";
                            break;

                        case 3:
                            MoveName.Text = "1-2 Thrust";
                            LazyDataFill(false, 27, 15, 13, 14, 7, 2);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_MoveUsed[1] >= 50);
                            MoveInfo.Text = "Hit tech. Chains from 50 Thrust.\r\nChains into Thrusts when used 50x. No stat requirement.";
                            break;

                        case 4:
                            MoveName.Text = "Thrusts";
                            LazyDataFill(false, 38, 38, -10, 22, 10, 3);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_MoveUsed[2] >= 50);
                            MoveInfo.Text = "Special tech. Chains from 50 1-2 Thrust. No stat requirement.";
                            break;

                        case 5:
                            MoveName.Text = "Head Butt";
                            LazyDataFill(false, 12, 12, -5, 5, 5, 6);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = true;
                            MoveInfo.Text = "Starting basic tech.";
                            break;

                        case 6:
                            MoveName.Text = "Licking";
                            LazyDataFill(true, 19, 13, -11, 24, 5, 7);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_Stats[2] >= 250);
                            MoveInfo.Text = "Withering tech. INT should be over 250 to unlock.";
                            break;

                        case 7:
                            MoveName.Text = "Press";
                            LazyDataFill(false, 24, 24, -9, 12, 9, 8);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_Stats[1] >= 400);
                            MoveInfo.Text = "Heavy tech. POW should be over 400 to unlock.\r\nChains into Diving Press when used 50x.";
                            break;

                        case 8:
                            MoveName.Text = "Diving Press";
                            LazyDataFill(false, 34, 39, -14, 19, 10, 9);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_MoveUsed[8] >= 50);
                            MoveInfo.Text = "Heavy tech. Chains from 50 Press.\r\nChains into Giant Press when used 50x. No stat requirement.";
                            break;

                        case 9:
                            MoveName.Text = "Giant Press";
                            LazyDataFill(false, 39, 49, -16, 20, 10, 10);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_MoveUsed[9] >= 50);
                            MoveInfo.Text = "Heavy tech. Chains from 50 Diving Press. No stat requirement.";
                            break;

                        case 10:
                            MoveName.Text = "Roll Attack";
                            LazyDataFill(false, 27, 19, -6, 19, 19, 12);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_Stats[1] + Mon_Stats[4] >= 650);
                            MoveInfo.Text = "Sharp tech. POW + SPD should total over 650 to unlock.\r\nChains into DazzlingRoll when used 50x.";
                            break;

                        case 11:
                            MoveName.Text = "DazzlingRoll";
                            LazyDataFill(false, 36, 28, -9, 20, 25, 13);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_MoveUsed[12] >= 50);
                            MoveInfo.Text = "Sharp tech. Chains from 50 Roll Attack. No stat requirement.";
                            break;

                        case 12:
                            MoveName.Text = "Petal Swirl";
                            LazyDataFill(true, 16, 14, -10, 13, 16, 14);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_Stats[2] + Mon_Stats[4] >= 400);
                            MoveInfo.Text = "Sharp tech. INT + SPD should total over 400 to unlock.\r\nChains into Petal Vortex and Petal Storm when used 50x. Learning priority over Roll Attack.";
                            break;

                        case 13:
                            MoveName.Text = "Petal Vortex";
                            LazyDataFill(true, 45, 0, -15, 0, 0, 15);
                            MoveSH.Show();
                            MoveSH.Text = GenerateStatValue(0, 40);
                            SHLabel.Show();
                            SHLabel.Text = "Recovery:";
                            CanUnlock.Checked = (Mon_Stats[2] >= 600 && Mon_MoveUsed[14] >= 50 && Mon_Nature >= 50);
                            MoveInfo.Text = "Special tech. INT should be over 600 to unlock.\r\nChains from 50 Petal Swirl. Requires Good (+50) Nature.";
                            break;

                        case 14:
                            MoveName.Text = "Petal Storm";
                            LazyDataFill(true, 40, 35, -15, 0, 0, 16);
                            MoveSH.Text = "100%";
                            MoveSH.Show();
                            SHLabel.Text = "HP Drain:";
                            SHLabel.Show();
                            CanUnlock.Checked = (Mon_Stats[2] >= 600 && Mon_MoveUsed[14] >= 50 && Mon_Nature <= -50);
                            MoveInfo.Text = "Special tech. INT should be over 600 to unlock.\r\nChains from 50 Petal Swirl. Requires Bad (-50) Nature.";
                            break;

                        case 15:
                            MoveName.Text = "Mocchi Ray";
                            LazyDataFill(true, 35, 20, 10, 20, 10, 18);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_Stats[2] + Mon_Stats[3] + Mon_Stats[4] >= 1000);
                            MoveInfo.Text = "Special tech. INT + SKI + SPD should total over 1000 to unlock.\r\nChains into Mocchi Beam when used 50x.";
                            break;

                        case 16:
                            MoveName.Text = "Mocchi Beam";
                            LazyDataFill(true, 40, 25, 3, 25, 15, 19);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_MoveUsed[18] >= 50);
                            MoveInfo.Text = "Special tech. Chains from 50 Mocchi Ray.\r\nChains into MocchiCannon when used 50x. No stat requirement.";
                            break;

                        case 17:
                            MoveName.Text = "MocchiCannon";
                            LazyDataFill(true, 50, 45, -5, 25, 20, 20);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_MoveUsed[19] >= 50 && Mon_Nature >= 20);
                            MoveInfo.Text = "Special tech. Chains from 50 Mocchi Beam.\r\nRequires Good (+20) Nature. No stat requirement.";
                            break;

                        case 18:
                            MoveName.Text = "Flame";
                            LazyDataFill(true, 38, 44, -13, 16, 10, 21);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_Stats[1] + Mon_Stats[2] >= 800 && Mon_SubGenus == 1);
                            MoveInfo.Text = "Special tech. POW + INT should total over 800 to unlock.\r\nExclusive to Draco Mocchi.";
                            break;

                        case 19:
                            MoveName.Text = "Roll Assault";
                            LazyDataFill(false, 19, 25, -12, 7, 5, 22);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_Stats[1] >= 250);
                            MoveInfo.Text = "Heavy tech. POW should be over 250 to unlock.\r\nChains into Petal Roll when used 50x. Learning priority over Press.";
                            break;

                        case 20:
                            MoveName.Text = "Petal Roll";
                            LazyDataFill(false, 25, 29, -13, 15, 7, 23);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_MoveUsed[22] >= 50);
                            MoveInfo.Text = "Heavy tech. Chains from 50 Roll Assault. No stat requirement.";
                            break;

                        default:
                            MoveName.Text = "No Move"; MoveName.ForeColor = Color.Black;
                            MoveGuts.Text = "---";
                            MoveDamage.Text = "---";
                            MoveHit.Text = "---";
                            MoveGD.Text = "---";
                            MoveSharp.Text = "---";
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = "---";
                            MoveUnlocked.Checked = false;
                            CanUnlock.Checked = false;
                            MoveInfo.Text = "Click on a move to learn more.";
                            break;
                    }
                    break;

                case 26: // Joker
                    switch (MoveSelected)
                    {
                        case 1:
                            MoveName.Text = "Death Punch"; MoveName.ForeColor = PowCol;
                            MoveGuts.Text = "12";
                            MoveDamage.Text = GenerateStatValue(0, 16);
                            MoveHit.Text = GenerateStatValue(1, -2);
                            MoveGD.Text = GenerateStatValue(2, 10);
                            MoveSharp.Text = "---";
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[0].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[0] == 1);
                            CanUnlock.Checked = true;
                            PlaceHighlight(0);
                            MoveInfo.Text = "Starting basic tech.";
                            break;
                        case 2:
                            MoveName.Text = "Death Slash"; MoveName.ForeColor = PowCol;
                            MoveGuts.Text = "29";
                            MoveDamage.Text = GenerateStatValue(0, 42);
                            MoveHit.Text = GenerateStatValue(1, -15);
                            MoveGD.Text = GenerateStatValue(2, 29);
                            MoveSharp.Text = GenerateStatValue(3, 11);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[6].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[6] == 1);
                            CanUnlock.Checked = (Mon_Stats[1] >= 450 && Mon_Nature <= -20); //bedeg
                            PlaceHighlight(6);
                            MoveInfo.Text = "Heavy tech. POW should be over 450 to unlock. Requires Bad (-20) Nature.";
                            break;
                        case 3:
                            MoveName.Text = "Death Smash"; MoveName.ForeColor = PowCol;
                            MoveGuts.Text = "15";
                            MoveDamage.Text = GenerateStatValue(0, 18);
                            MoveHit.Text = GenerateStatValue(1, -4);
                            MoveGD.Text = GenerateStatValue(2, 10);
                            MoveSharp.Text = GenerateStatValue(3, 5);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[7].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[7] == 1);
                            CanUnlock.Checked = true;
                            PlaceHighlight(7);
                            MoveInfo.Text = "Starting basic tech.";
                            break;
                        case 4:
                            MoveName.Text = "Death Cutter"; MoveName.ForeColor = IntCol;
                            MoveGuts.Text = "28";
                            MoveDamage.Text = GenerateStatValue(0, 24);
                            MoveHit.Text = GenerateStatValue(1, -5);
                            MoveGD.Text = GenerateStatValue(2, 24);
                            MoveSharp.Text = GenerateStatValue(3, 24);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[12].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[12] == 1);
                            CanUnlock.Checked = (Mon_Stats[2] + Mon_Stats[4] >= 650 && Mon_Nature <= -20); //bedeg
                            PlaceHighlight(12);
                            MoveInfo.Text = "Sharp tech. INT + SPD should total over 650 to unlock. Requires Bad (-20) Nature.";
                            break;
                        case 5:
                            MoveName.Text = "Death Energy"; MoveName.ForeColor = IntCol;
                            MoveGuts.Text = "28";
                            MoveDamage.Text = GenerateStatValue(0, 21);
                            MoveHit.Text = GenerateStatValue(1, 16);
                            MoveGD.Text = GenerateStatValue(2, 15);
                            MoveSharp.Text = GenerateStatValue(3, 10);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[18].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[18] == 1);
                            CanUnlock.Checked = (Mon_Stats[2] + Mon_Stats[3] >= 650 && Mon_Nature <= -20); //bedeg
                            PlaceHighlight(18);
                            MoveInfo.Text = "Hit tech. INT + SKI should total over 650 to unlock. Requires Bad (-20) Nature.";
                            break;
                        case 6:
                            MoveName.Text = "Death Final"; MoveName.ForeColor = PowCol;
                            MoveGuts.Text = "50";
                            MoveDamage.Text = GenerateStatValue(0, 50);
                            MoveHit.Text = GenerateStatValue(1, -15);
                            MoveGD.Text = GenerateStatValue(2, 50);
                            MoveSharp.Text = GenerateStatValue(3, 15);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[19].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[19] == 1);
                            CanUnlock.Checked = (Mon_Stats[1] + Mon_Stats[2] >= 800 && Mon_Nature <= -50); //bedeg
                            PlaceHighlight(19);
                            MoveInfo.Text = "Special tech. POW + INT should total over 800 to unlock. Requires Bad (-50) Nature.";
                            break;
                        default:
                            MoveName.Text = "No Move"; MoveName.ForeColor = Color.Black;
                            MoveGuts.Text = "---";
                            MoveDamage.Text = "---";
                            MoveHit.Text = "---";
                            MoveGD.Text = "---";
                            MoveSharp.Text = "---";
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = "---";
                            MoveUnlocked.Checked = false;
                            CanUnlock.Checked = false;
                            MoveInfo.Text = "Click on a move to learn more.";
                            break;
                    }
                    break;

                case 27: // Potats
                    switch (MoveSelected)
                    {
                        case 1:
                            MoveName.Text = "Slap";
                            LazyDataFill(false, 10, 12, 10, 0, 0, 0);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = true;
                            MoveInfo.Text = "Starting basic tech.";
                            break;

                        case 2:
                            MoveName.Text = "Acid Spit";
                            LazyDataFill(true, 35, 35, -15, 48, 5, 1);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_Stats[2] >= 500 && Mon_Nature <= -50);
                            MoveInfo.Text = "Special tech. INT should be over 500 to unlock.\r\nRequires Bad (-50) Nature.";
                            break;

                        case 3:
                            MoveName.Text = "Diving Press";
                            LazyDataFill(false, 18, 16, -3, 9, 16, 2);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_Stats[1] + Mon_Stats[4] >= 400);
                            MoveInfo.Text = "Sharp tech. POW + SPD should total over 400 to unlock.\r\nLearning priority over Jumping Chop.";
                            break;

                        case 4:
                            MoveName.Text = "Chop Combo";
                            LazyDataFill(false, 40, 44, 1, 10, 5, 3);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_Stats[1] + Mon_Stats[5] >= 800);
                            MoveInfo.Text = "Special tech. POW + DEF should total over 800 to unlock.\r\nLearning priority over Samurai Kick.";
                            break;

                        case 5:
                            MoveName.Text = "Samurai Kick";
                            LazyDataFill(false, 30, 25, 10, 25, 10, 4);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_Stats[1] >= 600);
                            MoveInfo.Text = "Special tech. POW should be over 600 to unlock.\r\nChains into Ninja Kick when used 50x.";
                            break;

                        case 6:
                            MoveName.Text = "Chop";
                            LazyDataFill(false, 11, 16, -4, 0, 5, 6);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = true;
                            MoveInfo.Text = "Starting basic tech. Chains into Rolling Chop when used 30x.";
                            break;

                        case 7:
                            MoveName.Text = "Rolling Chop";
                            LazyDataFill(false, 19, 20, -2, 10, 10, 7);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_MoveUsed[6] >= 30);
                            MoveInfo.Text = "Basic tech. Chains from 30 Chop.";
                            break;

                        case 8:
                            MoveName.Text = "Shock Wave";
                            LazyDataFill(false, 17, 14, 13, 7, 5, 8);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_Stats[1] + Mon_Stats[3] >= 400);
                            MoveInfo.Text = "Hit tech. POW + SKI should total over 400 to unlock.\r\nLearning priority over Long Punch.";
                            break;

                        case 9:
                            MoveName.Text = "Back Blow";
                            LazyDataFill(false, 14, 19, -13, 10, 5, 9);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_Stats[1] >= 250);
                            MoveInfo.Text = "Heavy tech. POW should be over 250 to unlock.\r\nChains into ElectricBlow when used 50x. Learning priority over Straight.";
                            break;

                        case 10:
                            MoveName.Text = "ElectricBlow";
                            LazyDataFill(false, 29, 41, -16, 17, 10, 10);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_Stats[1] >= 450 && Mon_MoveUsed[9] >= 50 && Mon_Nature >= 20);
                            MoveInfo.Text = "Heavy tech. POW should be over 450 to unlock.\r\nChains from 50 Back Blow. Requires Good (+20) Nature.";
                            break;

                        case 11:
                            MoveName.Text = "Ninja Kick";
                            LazyDataFill(false, 55, 55, -5, 30, 10, 11);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_MoveUsed[4] >= 50 && Mon_Nature <= -30);
                            MoveInfo.Text = "Special tech. Chains from 50 Samurai Kick. Requires Bad (-30) Nature.\r\nNo stat requirement.";
                            break;

                        case 12:
                            MoveName.Text = "Straight";
                            LazyDataFill(false, 20, 26, -9, 7, 10, 12);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_Stats[1] >= 450);
                            MoveInfo.Text = "Heavy tech. POW should be over 450 to unlock.";
                            break;

                        case 13:
                            MoveName.Text = "Cyclone";
                            LazyDataFill(true, 39, 39, -7, 22, 13, 13);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_Stats[2] + Mon_Stats[4] >= 800);
                            MoveInfo.Text = "Special tech. INT + SPD should total over 800 to unlock.\r\nLearning priority over Acid Spit.";
                            break;

                        case 14:
                            MoveName.Text = "Kiss";
                            LazyDataFill(true, 16, 5, 0, 25, 5, 14);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_Stats[2] >= 250);
                            MoveInfo.Text = "Withering tech. INT should be over 250 to unlock. Learning priority over Spit.";
                            break;

                        case 15:
                            MoveName.Text = "Long Punch";
                            LazyDataFill(false, 27, 19, 15, 13, 5, 18);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_Stats[1] + Mon_Stats[3] >= 650);
                            MoveInfo.Text = "Hit tech. POW + SKI should total over 650 to unlock.";
                            break;

                        case 16:
                            MoveName.Text = "Spit";
                            LazyDataFill(true, 24, 12, -5, 37, 5, 19);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_Stats[2] >= 450 && Mon_Nature <= -20);
                            MoveInfo.Text = "Withering tech. INT should be over 450 to unlock. Requires Bad (-20) Nature.";
                            break;

                        case 17:
                            MoveName.Text = "Jumping Chop";
                            LazyDataFill(false, 28, 24, -6, 16, 23, 20);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_Stats[1] + Mon_Stats[4] >= 650 && Mon_Nature >= 50);
                            MoveInfo.Text = "Sharp tech. POW + SPD should total over 650 to unlock. Requires Good (+50) Nature.";
                            break;

                        default:
                            MoveName.Text = "No Move"; MoveName.ForeColor = Color.Black;
                            MoveGuts.Text = "---";
                            MoveDamage.Text = "---";
                            MoveHit.Text = "---";
                            MoveGD.Text = "---";
                            MoveSharp.Text = "---";
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = "---";
                            MoveUnlocked.Checked = false;
                            CanUnlock.Checked = false;
                            MoveInfo.Text = "Click on a move to learn more.";
                            break;
                    }
                    break;

                case 28: // Jell
                    switch (MoveSelected)
                    {
                        case 1:
                            MoveName.Text = "Stab";
                            LazyDataFill(false, 11, 14, 0, 0, 5, 0);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = true;
                            MoveInfo.Text = "Starting basic tech. Chains into Pierce when used 30x.";
                            break;

                        case 2:
                            MoveName.Text = "Pierce";
                            LazyDataFill(false, 21, 20, 0, 0, 5, 1);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_MoveUsed[0] >= 30);
                            MoveInfo.Text = "Basic tech. Chains from 30 Stab. No stat requirement.";
                            break;

                        case 3:
                            MoveName.Text = "Suffocation";
                            LazyDataFill(false, 24, 20, 4, 8, 20, 2);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_Stats[1] + Mon_Stats[4] >= 650);
                            MoveInfo.Text = "Sharp tech. POW + SPD should total over 650 to unlock.";
                            break;

                        case 4:
                            MoveName.Text = "Bloodsuction";
                            LazyDataFill(false, 40, 30, -20, 0, 0, 3);
                            MoveSH.Text = "100%";
                            MoveSH.Show();
                            SHLabel.Text = "HP Drain:";
                            SHLabel.Show();
                            CanUnlock.Checked = (Mon_Stats[2] >= 500 && Mon_Nature <= -50);
                            MoveInfo.Text = "Special tech. INT should be over 500 to unlock. Requires Bad (-50) Nature.";
                            break;

                        case 5:
                            MoveName.Text = "Whip";
                            LazyDataFill(false, 14, 14, 6, 5, 0, 6);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = true;
                            MoveInfo.Text = "Starting basic tech. Chains into Two Whips when used 30x.";
                            break;

                        case 6:
                            MoveName.Text = "Two Whips";
                            LazyDataFill(false, 22, 21, 6, 11, 0, 7);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_MoveUsed[6] >= 30);
                            MoveInfo.Text = "Basic tech. Chains from 30 Whip. No stat requirement.";
                            break;

                        case 7:
                            MoveName.Text = "Jell Press";
                            LazyDataFill(false, 30, 28, 11, 7, 5, 8);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_Stats[1] + Mon_Stats[3] >= 650);
                            MoveInfo.Text = "Hit tech. POW + SKI should total over 650 to unlock.";
                            break;

                        case 8:
                            MoveName.Text = "Jell Cube";
                            LazyDataFill(false, 16, 29, -16, 5, 9, 9);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_Stats[1] >= 250);
                            MoveInfo.Text = "Heavy tech. POW should be over 250 to unlock. Chains into Three Cubes when used 50x.\r\nLearning priority over Cannon.";
                            break;

                        case 9:
                            MoveName.Text = "Three Cubes";
                            LazyDataFill(false, 23, 39, -10, 6, 9, 10);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_MoveUsed[9] >= 50);
                            MoveInfo.Text = "Heavy tech. Chains from 50 Jell Cube. No stat requirement.";
                            break;

                        case 10:
                            MoveName.Text = "Jell Top";
                            LazyDataFill(false, 17, 14, 15, 6, 5, 12);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_Stats[1] + Mon_Stats[3] >= 400);
                            MoveInfo.Text = "Hit tech. POW + SKI should total over 400 to unlock.\r\nChains into Spiked Top when used 50x. Learning priority over Jell Press.";
                            break;

                        case 11:
                            MoveName.Text = "Spiked Top";
                            LazyDataFill(false, 28, 22, 13, 8, 10, 13);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_MoveUsed[12] >= 50);
                            MoveInfo.Text = "Hit tech. Chains from 50 Jell Top. No stat requirement.";
                            break;

                        case 12:
                            MoveName.Text = "Fly Swatter";
                            LazyDataFill(false, 27, 15, -3, 31, 7, 14);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_Stats[1] + Mon_Stats[2] >= 650);
                            MoveInfo.Text = "Withering tech. POW + INT should total over 650 to unlock.\r\nChains into Fly Smasher when used 50x.";
                            break;

                        case 13:
                            MoveName.Text = "Fly Smasher";
                            LazyDataFill(false, 39, 26, -5, 41, 9, 15);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_MoveUsed[14] >= 50);
                            MoveInfo.Text = "Withering tech. Chains from 50 Fly Swatter. No stat requirement.";
                            break;

                        case 14:
                            MoveName.Text = "Beam Gun";
                            LazyDataFill(true, 18, 15, -2, 15, 19, 16);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_Stats[2] + Mon_Stats[4] >= 400);
                            MoveInfo.Text = "Sharp tech. INT + SPD should total over 400 to unlock.\r\nChains into Beam Cannon when used 50x.";
                            break;

                        case 15:
                            MoveName.Text = "Beam Cannon";
                            LazyDataFill(true, 35, 25, -2, 25, 25, 17);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_MoveUsed[16] >= 50);
                            MoveInfo.Text = "Sharp tech. Chains from 50 Beam Gun. No stat requirement.";
                            break;

                        case 16:
                            MoveName.Text = "Cannon";
                            LazyDataFill(true, 35, 50, -12, 10, 10, 18);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_Stats[1] >= 450);
                            MoveInfo.Text = "Heavy tech. POW should be over 450 to unlock.";
                            break;

                        case 17:
                            MoveName.Text = "Slingshot";
                            LazyDataFill(true, 18, 9, -8, 31, 9, 19);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_Stats[1] >= 250);
                            MoveInfo.Text = "Withering tech. INT should be over 250 to unlock.\r\nLearning priority over Fly Swatter.";
                            break;

                        case 18:
                            MoveName.Text = "Pyramid";
                            LazyDataFill(true, 40, 0, -20, 0, 0, 20);
                            MoveSH.Show();
                            MoveSH.Text = GenerateStatValue(0, 40);
                            SHLabel.Show();
                            SHLabel.Text = "Recovery:";
                            CanUnlock.Checked = (Mon_Stats[1] >= 500 && Mon_Nature >= 50);
                            MoveInfo.Text = "Special tech. POW should be over 500 to unlock. Requires Good (+50) Nature.";
                            break;

                        case 19:
                            MoveName.Text = "Gatling Gun";
                            LazyDataFill(true, 45, 30, 15, 15, 15, 21);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_Stats[3] + Mon_Stats[4] >= 800);
                            MoveInfo.Text = "Special tech. SKI + SPD should total over 800 to unlock.";
                            break;

                        case 20:
                            MoveName.Text = "Jell Copter";
                            LazyDataFill(true, 50, 50, 5, 15, 5, 22);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_Stats[2] >= 600);
                            MoveInfo.Text = "Special tech. INT should be over 600 to unlock.";
                            break;

                        default:
                            MoveName.Text = "No Move"; MoveName.ForeColor = Color.Black;
                            MoveGuts.Text = "---";
                            MoveDamage.Text = "---";
                            MoveHit.Text = "---";
                            MoveGD.Text = "---";
                            MoveSharp.Text = "---";
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = "---";
                            MoveUnlocked.Checked = false;
                            CanUnlock.Checked = false;
                            MoveInfo.Text = "Click on a move to learn more.";
                            break;
                    }
                    break;

                case 29: // Undine
                    switch (MoveSelected)
                    {
                        case 1:
                            MoveName.Text = "Ice Sword";
                            LazyDataFill(false, 10, 8, 10, 0, 5, 0);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = true;
                            MoveInfo.Text = "Starting basic tech. Chains into Ice Swords when used 30x.";
                            break;

                        case 2:
                            MoveName.Text = "Ice Swords";
                            LazyDataFill(false, 14, 15, 10, 0, 9, 1);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_MoveUsed[0] >= 30);
                            MoveInfo.Text = "Basic tech. Chains from 30 Ice Sword. No stat requirement.";
                            break;

                        case 3:
                            MoveName.Text = "Dolphin Blow";
                            LazyDataFill(false, 18, 23, -5, 5, 5, 2);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_Stats[1] >= 250);
                            if (rng.Next(100) > 5)
                                MoveInfo.Text = "Heavy tech. POW should be over 250 to unlock.";
                            else
                                MoveInfo.Text = "Tifa's Limit Break. POW should be over 250 to unlock.";
                            break;

                        case 4:
                            MoveName.Text = "Splash";
                            LazyDataFill(true, 27, 8, -8, 40, 5, 3);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_Stats[2] >= 450 && Mon_Nature >= 20);
                            MoveInfo.Text = "Withering tech. INT should be over 450 to unlock.\r\nRequires Good (+20) Nature.";
                            break;

                        case 5:
                            MoveName.Text = "Aqua Whip";
                            LazyDataFill(false, 19, 6, -9, 29, 5, 6);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_Stats[1] + Mon_Stats[2] >= 400 && Mon_Nature <= -20);
                            MoveInfo.Text = "Withering tech. POW + INT should total over 400 to unlock.\r\nChains into Two Whips when used 50x. Requires Bad (-20) Nature.";
                            break;

                        case 6:
                            MoveName.Text = "Two Whips";
                            LazyDataFill(false, 29, 11, -12, 39, 5, 7);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_MoveUsed[6] >= 50 && Mon_Nature <= -30);
                            MoveInfo.Text = "Withering tech. Chains from 50 Aqua Whip.\r\nRequires Bad (-30) Nature. No stat requirement.";
                            break;

                        case 7:
                            MoveName.Text = "Kiss";
                            LazyDataFill(true, 30, 15, 15, 14, 5, 8);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_Stats[2] + Mon_Stats[3] >= 650);
                            MoveInfo.Text = "Hit tech. INT + SKI should total over 650 to unlock.";
                            break;

                        case 8:
                            MoveName.Text = "Cold Fog";
                            LazyDataFill(true, 12, 10, 12, 0, 0, 9);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = true;
                            MoveInfo.Text = "Starting basic tech.";
                            break;

                        case 9:
                            MoveName.Text = "Arrow";
                            LazyDataFill(true, 18, 15, 2, 6, 20, 10);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_Stats[2] + Mon_Stats[4] >= 400 && Mon_Nature >= 20);
                            MoveInfo.Text = "Sharp tech. INT + SPD should total over 400 to unlock.\r\nChains into Ice Arrow when used 50x. Requires Good (+20) Nature.";
                            break;

                        case 10:
                            MoveName.Text = "Aqua Wave";
                            LazyDataFill(true, 19, 12, 20, 5, 5, 12);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_Stats[2] + Mon_Stats[3] >= 400);
                            MoveInfo.Text = "Hit tech. INT + SKI should total over 400 to unlock.\r\nChains into Aqua Waves when used 50x. Learning priority over Kiss.";
                            break;

                        case 11:
                            MoveName.Text = "Aqua Waves";
                            LazyDataFill(true, 28, 20, 15, 7, 5, 13);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_MoveUsed[12] >= 50);
                            MoveInfo.Text = "Hit tech. Chains from 50 Aqua Wave. No stat requirement.";
                            break;

                        case 12:
                            MoveName.Text = "Ice Coffin";
                            LazyDataFill(true, 30, 38, -15, 15, 10, 14);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_Stats[1] + Mon_Stats[2] >= 650);
                            MoveInfo.Text = "Heavy tech. POW + INT should total over 650 to unlock.";
                            break;

                        case 13:
                            MoveName.Text = "Ice Arrow";
                            LazyDataFill(true, 28, 20, 0, 12, 25, 15);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_MoveUsed[10] >= 50 && Mon_Nature >= 30);
                            MoveInfo.Text = "Sharp tech. Chains from 50 Arrow.\r\nRequires Good (+30) Nature. No stat requirement.";
                            break;

                        case 14:
                            MoveName.Text = "Cold Whirl";
                            LazyDataFill(true, 50, 39, -10, 39, 5, 16);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_Stats[2] >= 600);
                            MoveInfo.Text = "Special tech. INT should be over 600 to unlock.\r\nChains into Cold Storm when used 50x.";
                            break;

                        case 15:
                            MoveName.Text = "Water Gun";
                            LazyDataFill(true, 45, 20, 15, 30, 5, 17);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_Stats[2] + Mon_Stats[3] >= 800);
                            MoveInfo.Text = "Special tech. INT + SKI should total over 800 to unlock.\r\nChains into Water Cannon when used 50x. Learning priority over Cold Whirl.";
                            break;

                        case 16:
                            MoveName.Text = "Icicle Arrow";
                            LazyDataFill(true, 35, 30, -4, 15, 30, 18);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_MoveUsed[15] >= 50 && Mon_Nature >= 50);
                            MoveInfo.Text = "Special tech. Chains from 50 Ice Arrow.\r\nRequires Good (+50) Nature. No stat requirement.";
                            break;

                        case 17:
                            MoveName.Text = "Hailstorm";
                            LazyDataFill(true, 34, 23, 4, 5, 20, 19);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_Stats[2] + Mon_Stats[4] >= 650);
                            MoveInfo.Text = "Sharp tech. INT + SPD should total over 650 to unlock.";
                            break;

                        case 18:
                            MoveName.Text = "Cold Storm";
                            LazyDataFill(true, 55, 49, -15, 49, 10, 20);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_MoveUsed[16] >= 50);
                            MoveInfo.Text = "Special tech. Chains from 50 Cold Whirl. No stat requirement.";
                            break;

                        case 19:
                            MoveName.Text = "Vitalization";
                            LazyDataFill(true, 40, 0, -20, 0, 0, 21);
                            MoveSH.Show();
                            MoveSH.Text = GenerateStatValue(0, 30);
                            SHLabel.Show();
                            SHLabel.Text = "Recovery:";
                            CanUnlock.Checked = (Mon_Stats[2] >= 600 && Mon_Nature >= 50);
                            MoveInfo.Text = "Special tech. INT should be over 600 to unlock. Requires Good (+50) Nature.";
                            break;

                        case 20:
                            MoveName.Text = "Cold Geyser";
                            LazyDataFill(true, 16, 11, -2, 7, 30, 22);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_Stats[2] + Mon_Stats[4] >= 400);
                            MoveInfo.Text = "Sharp tech. INT + SPD should total over 400 to unlock.\r\nLearning priority over Hailstorm.";
                            break;

                        case 21:
                            MoveName.Text = "Water Cannon";
                            LazyDataFill(true, 50, 30, 5, 40, 15, 23);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_MoveUsed[17] >= 50);
                            MoveInfo.Text = "Special tech. Chains from 50 Water Gun. No stat requirement.";
                            break;

                        default:
                            MoveName.Text = "No Move"; MoveName.ForeColor = Color.Black;
                            MoveGuts.Text = "---";
                            MoveDamage.Text = "---";
                            MoveHit.Text = "---";
                            MoveGD.Text = "---";
                            MoveSharp.Text = "---";
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = "---";
                            MoveUnlocked.Checked = false;
                            CanUnlock.Checked = false;
                            MoveInfo.Text = "Click on a move to learn more.";
                            break;
                    }
                    break;

                case 30: // Niton
                    switch (MoveSelected)
                    {
                        case 1:
                            MoveName.Text = "Whip";
                            LazyDataFill(false, 10, 9, 8, 0, 0, 0);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = true;
                            MoveInfo.Text = "Starting basic tech.";
                            break;

                        case 2:
                            MoveName.Text = "Stab";
                            LazyDataFill(false, 12, 12, -1, 0, 0, 1);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = true;
                            MoveInfo.Text = "Starting basic tech. Chains into Numbing Stab when used 30x.";
                            break;

                        case 3:
                            MoveName.Text = "Numbing Stab";
                            LazyDataFill(false, 15, 15, -1, 0, 5, 2);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_MoveUsed[1] >= 30);
                            MoveInfo.Text = "Basic tech. Chains from 30 Stab.\r\nChains into ElectricStab when used 30x. No stat requirement.";
                            break;

                        case 4:
                            MoveName.Text = "ElectricStab";
                            LazyDataFill(false, 21, 22, -6, 6, 10, 3);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_MoveUsed[2] >= 30);
                            MoveInfo.Text = "Basic tech. Chains from 30 Numbing Stab. No stat requirement.";
                            break;

                        case 5:
                            MoveName.Text = "Sound Wave";
                            LazyDataFill(true, 16, 10, 16, 10, 9, 6);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_Stats[2] + Mon_Stats[3] >= 400);
                            MoveInfo.Text = "Hit tech. INT + SKI should total over 400 to unlock.\r\nChains into Sound Wave-L when used 50x.";
                            break;

                        case 6:
                            MoveName.Text = "Sound Wave-L";
                            LazyDataFill(true, 24, 16, 14, 12, 15, 7);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_MoveUsed[6] >= 50);
                            MoveInfo.Text = "Hit tech. Chains from 50 Sound Wave.\r\nChains into Sound Wave-X when used 50x. No stat requirement.";
                            break;

                        case 7:
                            MoveName.Text = "Sound Wave-X";
                            LazyDataFill(true, 35, 19, 15, 18, 15, 8);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_MoveUsed[7] >= 50);
                            MoveInfo.Text = "Special tech. Chains from 50 Sound Wave-L. No stat requirement.";
                            break;

                        case 8:
                            MoveName.Text = "Numbing Whip";
                            LazyDataFill(false, 19, 17, -7, 7, 25, 9);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_Stats[1] + Mon_Stats[4] >= 400);
                            MoveInfo.Text = "Sharp tech. POW + SPD should total over 400 to unlock.\r\nChains into ElectricWhip when used 50x.";
                            break;

                        case 9:
                            MoveName.Text = "ElectricWhip";
                            LazyDataFill(false, 28, 23, -10, 13, 29, 10);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_MoveUsed[9] >= 50);
                            MoveInfo.Text = "Sharp tech. Chains from 50 Numbing Whip.\r\nChains into MillionWhips when used 50x. No stat requirement.";
                            break;

                        case 10:
                            MoveName.Text = "MillionWhips";
                            LazyDataFill(false, 35, 31, -10, 15, 35, 11);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_MoveUsed[10] >= 50 && Mon_Nature >= 20);
                            MoveInfo.Text = "Special tech. Chains from 50 ElectricWhip.\r\nRequires Good (+20) Nature. No stat requirement.";
                            break;

                        case 11:
                            MoveName.Text = "Shock";
                            LazyDataFill(true, 19, 18, -10, 14, 10, 12);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_Stats[1] + Mon_Stats[2] >= 400);
                            MoveInfo.Text = "Heavy tech. POW + INT should total over 400 to unlock.\r\nChains into Severe Shock when used 50x.";
                            break;

                        case 12:
                            MoveName.Text = "Severe Shock";
                            LazyDataFill(true, 26, 28, -12, 15, 10, 13);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_MoveUsed[12] >= 50);
                            MoveInfo.Text = "Heavy tech. Chains from 50 Shock.\r\nChains into MaximalShock when used 50x. No stat requirement.";
                            break;

                        case 13:
                            MoveName.Text = "MaximalShock";
                            LazyDataFill(true, 35, 38, -12, 19, 10, 14);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_MoveUsed[13] >= 50 && Mon_Nature >= 20);
                            MoveInfo.Text = "Special tech. Chains from 50 Severe Shock.\r\nAlso obtainable from Papas Errantry. Requires Good (+20) Nature. No stat requirement.";
                            break;

                        case 14:
                            MoveName.Text = "Niton Ink";
                            LazyDataFill(true, 20, 2, 2, 32, 10, 15);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_Stats[2] >= 250 && Mon_Nature <= -20);
                            MoveInfo.Text = "Withering tech. INT should be over 250 to unlock. Requires Bad (-20) Nature.";
                            break;

                        case 15:
                            MoveName.Text = "Shell Attack";
                            LazyDataFill(false, 35, 30, 0, 5, 10, 18);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_Stats[1] + Mon_Stats[5] >= 800);
                            MoveInfo.Text = "Special tech. POW + DEF should total over 800 to unlock.\r\nChains into Spiked Shell when used 50x.";
                            break;

                        case 16:
                            MoveName.Text = "Spiked Shell";
                            LazyDataFill(false, 40, 35, -2, 5, 20, 19);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_MoveUsed[18] >= 50);
                            MoveInfo.Text = "Special tech. Chains from 50 Shell Attack.\r\nChains into ViolentShell when used 50x. No stat requirement.";
                            break;

                        case 17:
                            MoveName.Text = "ViolentShell";
                            LazyDataFill(false, 50, 40, -5, 35, 20, 20);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_MoveUsed[19] >= 50);
                            MoveInfo.Text = "Special tech. Chains from 50 Spiked Shell. No stat requirement.";
                            break;

                        default:
                            MoveName.Text = "No Move"; MoveName.ForeColor = Color.Black;
                            MoveGuts.Text = "---";
                            MoveDamage.Text = "---";
                            MoveHit.Text = "---";
                            MoveGD.Text = "---";
                            MoveSharp.Text = "---";
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = "---";
                            MoveUnlocked.Checked = false;
                            CanUnlock.Checked = false;
                            MoveInfo.Text = "Click on a move to learn more.";
                            break;
                    }
                    break;

                case 31: // Mock
                    switch (MoveSelected)
                    {
                        case 1:
                            MoveName.Text = "Head Butt"; MoveName.ForeColor = PowCol;
                            MoveGuts.Text = "15";
                            MoveDamage.Text = GenerateStatValue(0, 16);
                            MoveHit.Text = GenerateStatValue(1, 3);
                            MoveGD.Text = GenerateStatValue(2, 5);
                            MoveSharp.Text = "---";
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[0].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[0] == 1);
                            CanUnlock.Checked = true;
                            PlaceHighlight(0);
                            MoveInfo.Text = "Starting basic tech.";
                            break;
                        case 2:
                            MoveName.Text = "Leaf Cutter"; MoveName.ForeColor = IntCol;
                            MoveGuts.Text = "18";
                            MoveDamage.Text = GenerateStatValue(0, 14);
                            MoveHit.Text = GenerateStatValue(1, 15);
                            MoveGD.Text = GenerateStatValue(2, 5);
                            MoveSharp.Text = GenerateStatValue(3, 5);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[6].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[6] == 1);
                            CanUnlock.Checked = (Mon_Stats[2] + Mon_Stats[3] >= 400);
                            PlaceHighlight(6);
                            MoveInfo.Text = "Hit tech. INT + SKI should total over 400 to unlock.";
                            break;
                        case 3:
                            MoveName.Text = "Leaf Gun"; MoveName.ForeColor = IntCol; //bedeg
                            MoveGuts.Text = "12";
                            MoveDamage.Text = GenerateStatValue(0, 13);
                            MoveHit.Text = GenerateStatValue(1, 0);
                            MoveGD.Text = "---";
                            MoveSharp.Text = "---";
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[12].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[12] == 1);
                            CanUnlock.Checked = true;
                            PlaceHighlight(12);
                            MoveInfo.Text = "Starting basic tech. Chains into Leaf Gatling when used 50x.";
                            break;
                        case 4:
                            MoveName.Text = "Leaf Gatling"; MoveName.ForeColor = IntCol;
                            MoveGuts.Text = "23";
                            MoveDamage.Text = GenerateStatValue(0, 34);
                            MoveHit.Text = GenerateStatValue(1, -14);
                            MoveGD.Text = GenerateStatValue(2, 8);
                            MoveSharp.Text = GenerateStatValue(3, 5);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[13].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[13] == 1);
                            CanUnlock.Checked = (Mon_MoveUsed[12] >= 50 && (Mon_Stats[1] + Mon_Stats[2] >= 400));
                            PlaceHighlight(13);
                            MoveInfo.Text = "Heavy tech. Chains from 50 Leaf Guns. POW + INT should total over 400 to unlock.";
                            break;
                        case 5:
                            MoveName.Text = "Twig Gun"; MoveName.ForeColor = IntCol; //bedeg
                            MoveGuts.Text = "18";
                            MoveDamage.Text = GenerateStatValue(0, 10);
                            MoveHit.Text = GenerateStatValue(1, -6);
                            MoveGD.Text = GenerateStatValue(2, 25);
                            MoveSharp.Text = GenerateStatValue(3, 5);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[14].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[14] == 1);
                            CanUnlock.Checked = (Mon_Stats[2] >= 250);
                            PlaceHighlight(14);
                            MoveInfo.Text = "Withering tech. INT should be over 250 to unlock.";
                            break;
                        case 6:
                            MoveName.Text = "Twig Gatling"; MoveName.ForeColor = IntCol;
                            MoveGuts.Text = "30";
                            MoveDamage.Text = GenerateStatValue(0, 21);
                            MoveHit.Text = GenerateStatValue(1, -8);
                            MoveGD.Text = GenerateStatValue(2, 35);
                            MoveSharp.Text = GenerateStatValue(3, 5);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[15].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[15] == 1);
                            CanUnlock.Checked = true;
                            PlaceHighlight(15);
                            MoveInfo.Text = "Withering tech. No tech requirements.";
                            break;
                        case 7:
                            MoveName.Text = "Energy Steal"; MoveName.ForeColor = IntCol; //bedeg
                            MoveGuts.Text = "50";
                            MoveDamage.Text = GenerateStatValue(0, 40);
                            MoveHit.Text = GenerateStatValue(1, -20);
                            MoveGD.Text = GenerateStatValue(2, 40);
                            MoveSharp.Text = "---";
                            MoveSH.Text = "50%";
                            MoveSH.Show();
                            SHLabel.Text = "Guts/HP Drain:";
                            SHLabel.Show();
                            MoveUses.Text = Mon_MoveUsed[18].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[18] == 1);
                            CanUnlock.Checked = (Mon_Stats[2] >= 600 && Mon_Nature <= -50);
                            PlaceHighlight(18);
                            MoveInfo.Text = "Special tech. INT should be over 600 to unlock. Requires Bad (-50) Nature.";
                            break;
                        case 8:
                            MoveName.Text = "Twister"; MoveName.ForeColor = IntCol; //bedeg
                            MoveGuts.Text = "45";
                            MoveDamage.Text = GenerateStatValue(0, 44);
                            MoveHit.Text = GenerateStatValue(1, -10);
                            MoveGD.Text = GenerateStatValue(2, 33);
                            MoveSharp.Text = GenerateStatValue(3, 5);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[19].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[19] == 1);
                            CanUnlock.Checked = (Mon_Stats[2] > 600);
                            PlaceHighlight(19);
                            MoveInfo.Text = "Special tech. INT should be over 600 to unlock. Chains into Twisters when used 50x.";
                            break;
                        case 9:
                            MoveName.Text = "Twisters"; MoveName.ForeColor = IntCol;
                            MoveGuts.Text = "50";
                            MoveDamage.Text = GenerateStatValue(0, 55);
                            MoveHit.Text = GenerateStatValue(1, -13); //bedeg
                            MoveGD.Text = GenerateStatValue(2, 44);
                            MoveSharp.Text = GenerateStatValue(3, 5);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[20].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[20] == 1);
                            CanUnlock.Checked = (Mon_MoveUsed[19] >= 50);
                            PlaceHighlight(20);
                            MoveInfo.Text = "Special tech. Chains from 50 Twister uses.";
                            break;
                        default:
                            MoveName.Text = "No Move"; MoveName.ForeColor = Color.Black;
                            MoveGuts.Text = "---";
                            MoveDamage.Text = "---";
                            MoveHit.Text = "---";
                            MoveGD.Text = "---";
                            MoveSharp.Text = "---";
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = "---";
                            MoveUnlocked.Checked = false;
                            CanUnlock.Checked = false;
                            MoveInfo.Text = "Click on a move to learn more.";
                            break;
                    }
                    break;

                case 32: // Ducken
                    switch (MoveSelected)
                    {
                        case 1:
                            MoveName.Text = "Flutter Slap";
                            LazyDataFill(false, 11, 8, 5, 5, 0, 0);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = true;
                            MoveInfo.Text = "Starting basic tech.";
                            break;

                        case 2:
                            MoveName.Text = "Explosion";
                            LazyDataFill(false, 50, 60, -20, 55, 10, 1);
                            MoveSH.Text = GenerateStatValue(0, 60);
                            MoveSH.Show();
                            SHLabel.Text = "Self-Damage:";
                            SHLabel.Show();
                            CanUnlock.Checked = (Mon_Stats[0] + Mon_Stats[1] + Mon_Stats[2] + Mon_Stats[3] + Mon_Stats[4] + Mon_Stats[5] >= 1800 && Mon_Nature <= -20);
                            MoveInfo.Text = "Special tech. LIF + POW + INT + SKI + SPD + DEF should total over 1800 to unlock.\r\nRequires Bad (-20) Nature.";
                            break;

                        case 3:
                            MoveName.Text = "Beak Thrust";
                            LazyDataFill(false, 15, 14, 0, 0, 5, 6);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = true;
                            MoveInfo.Text = "Starting basic tech.";
                            break;

                        case 4:
                            MoveName.Text = "Ducken Dance";
                            LazyDataFill(true, 19, 8, -9, 23, 5, 7);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_Stats[2] >= 250);
                            MoveInfo.Text = "Withering tech. INT should be over 250 to unlock. Learning priority over Surprise.";
                            break;

                        case 5:
                            MoveName.Text = "Surprise";
                            LazyDataFill(true, 27, 7, -8, 39, 5, 8);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_Stats[2] >= 450);
                            MoveInfo.Text = "Withering tech. INT should be over 450 to unlock.";
                            break;

                        case 6:
                            MoveName.Text = "Bound Charge";
                            LazyDataFill(false, 30, 20, 15, 8, 10, 9);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_MoveUsed[12] >= 50);
                            MoveInfo.Text = "Hit tech. Chains from 50 Bound.\r\nChains into Bound Stamp when used 50x. No stat requirement.";
                            break;

                        case 7:
                            MoveName.Text = "Bound Stamp";
                            LazyDataFill(false, 39, 31, 6, 8, 10, 10);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_MoveUsed[9] >= 50);
                            MoveInfo.Text = "Special tech. Chains from 50 Bound Charge. No stat requirement.";
                            break;

                        case 8:
                            MoveName.Text = "Bound";
                            LazyDataFill(false, 15, 10, 12, 5, 10, 12);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_Stats[1] + Mon_Stats[3] >= 400);
                            MoveInfo.Text = "Hit tech. POW + SKI should total over 400 to unlock.\r\nChains into Bound Charge when used 50x.";
                            break;

                        case 9:
                            MoveName.Text = "Eye Beam";
                            LazyDataFill(true, 26, 15, 8, 15, 10, 13);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_Stats[2] + Mon_Stats[3] >= 650);
                            MoveInfo.Text = "Hit tech. INT + SKI should total over 650 to unlock.\r\nChains into Beam Shower when used 25x.";
                            break;

                        case 10:
                            MoveName.Text = "Beam Shower";
                            LazyDataFill(true, 35, 20, 15, 18, 10, 14);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_MoveUsed[13] >= 25);
                            MoveInfo.Text = "Hit tech. Chains from 25 Eye Beam.\r\nChains into Maximal Beam when used 50x. No stat requirement.";
                            break;

                        case 11:
                            MoveName.Text = "Maximal Beam";
                            LazyDataFill(true, 40, 31, 0, 31, 10, 15);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_MoveUsed[14] >= 50 && Mon_Nature >= 20);
                            MoveInfo.Text = "Special tech. Chains from 50 Beam Shower.\r\nRequires Good (+20) Nature. No stat requirement.";
                            break;

                        case 12:
                            MoveName.Text = "Bombing";
                            LazyDataFill(true, 33, 43, -18, 10, 10, 16);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_Stats[1] + Mon_Stats[2] >= 650);
                            MoveInfo.Text = "Heavy tech. POW + INT should total over 650 to unlock.";
                            break;

                        case 13:
                            MoveName.Text = "Boomerang";
                            LazyDataFill(true, 28, 19, -3, 11, 26, 17);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_Stats[2] + Mon_Stats[4] >= 650);
                            MoveInfo.Text = "Sharp tech. INT + SPD should total over 650 to unlock.";
                            break;

                        case 14:
                            MoveName.Text = "Missile";
                            LazyDataFill(true, 19, 29, -20, 5, 5, 18);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_Stats[1] + Mon_Stats[2] >= 400);
                            MoveInfo.Text = "Heavy tech. POW + INT should total over 400 to unlock.\r\nChains into Two Missiles and Big Missile when used 50x. Learning priority over Bombing.";
                            break;

                        case 15:
                            MoveName.Text = "Two Missiles";
                            LazyDataFill(true, 31, 28, -8, 10, 30, 19);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_MoveUsed[18] >= 50 && Mon_Nature >= 20);
                            MoveInfo.Text = "Sharp tech. Chains from 50 Missile.\r\nRequires Good (+20) Nature. No stat requirement.";
                            break;

                        case 16:
                            MoveName.Text = "Big Missile";
                            LazyDataFill(true, 39, 55, -22, 19, 5, 20);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_MoveUsed[18] >= 50 && Mon_Nature <= -20);
                            MoveInfo.Text = "Heavy tech. Chains from 50 Missile.\r\nRequires Bad (-20) Nature. No stat requirement.";
                            break;

                        case 17:
                            MoveName.Text = "Falling Beak";
                            LazyDataFill(false, 17, 15, -11, 5, 21, 21);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_Stats[1] + Mon_Stats[4] >= 400);
                            MoveInfo.Text = "Sharp tech. POW + SPD should total over 400 to unlock.";
                            break;

                        case 18:
                            MoveName.Text = "Frantic Beam";
                            LazyDataFill(true, 45, 25, 2, 25, 25, 22);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_Stats[2] + Mon_Stats[4] >= 800 && Mon_Nature >= 20);
                            MoveInfo.Text = "Special tech. INT + SPD should total over 800 to unlock.\r\nRequires Good (+20) Nature.";
                            break;

                        default:
                            MoveName.Text = "No Move"; MoveName.ForeColor = Color.Black;
                            MoveGuts.Text = "---";
                            MoveDamage.Text = "---";
                            MoveHit.Text = "---";
                            MoveGD.Text = "---";
                            MoveSharp.Text = "---";
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = "---";
                            MoveUnlocked.Checked = false;
                            CanUnlock.Checked = false;
                            MoveInfo.Text = "Click on a move to learn more.";
                            break;
                    }
                    break;

                case 33: // Plant
                    switch (MoveSelected)
                    {
                        case 1:
                            MoveName.Text = "Slap"; MoveName.ForeColor = PowCol;
                            MoveGuts.Text = "11";
                            MoveDamage.Text = GenerateStatValue(0, 7);
                            MoveHit.Text = GenerateStatValue(1, 8);
                            MoveGD.Text = "---";
                            MoveSharp.Text = "---";
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[0].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[0] == 1);
                            CanUnlock.Checked = true;
                            PlaceHighlight(0);
                            MoveInfo.Text = "Starting basic tech.";
                            break;
                        case 2:
                            MoveName.Text = "Root Attack"; MoveName.ForeColor = PowCol;
                            MoveGuts.Text = "13";
                            MoveDamage.Text = GenerateStatValue(0, 6);
                            MoveHit.Text = GenerateStatValue(1, 22);
                            MoveGD.Text = GenerateStatValue(2, 5);
                            MoveSharp.Text = GenerateStatValue(3, 5);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[1].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[1] == 1);
                            CanUnlock.Checked = (Mon_Stats[1] + Mon_Stats[3] >= 400);
                            PlaceHighlight(1);
                            MoveInfo.Text = "Hit tech. POW + SKI should total over 400 to unlock. Chains into Root Combo when used 25x.";
                            break;
                        case 3:
                            MoveName.Text = "Root Combo"; MoveName.ForeColor = PowCol;
                            MoveGuts.Text = "21";
                            MoveDamage.Text = GenerateStatValue(0, 12);
                            MoveHit.Text = GenerateStatValue(1, 21);
                            MoveGD.Text = GenerateStatValue(2, 5);
                            MoveSharp.Text = GenerateStatValue(3, 5);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[2].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[2] == 1);
                            CanUnlock.Checked = (Mon_MoveUsed[1] >= 25 && (Mon_Stats[1] + Mon_Stats[3] >= 650));
                            PlaceHighlight(2);
                            MoveInfo.Text = "Hit tech. Chains from 25 Root Attacks. POW + SKI should total over 650 to unlock.";
                            break;
                        case 4:
                            MoveName.Text = "Life Steal"; MoveName.ForeColor = IntCol;
                            MoveGuts.Text = "50";
                            MoveDamage.Text = GenerateStatValue(0, 25);
                            MoveHit.Text = GenerateStatValue(1, 0);
                            MoveGD.Text = "---";
                            MoveSharp.Text = "---";
                            MoveSH.Text = "100%";
                            MoveSH.Show();
                            SHLabel.Text = "HP Drain:";
                            SHLabel.Show();
                            MoveUses.Text = Mon_MoveUsed[3].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[3] == 1);
                            CanUnlock.Checked = (Mon_Stats[2] >= 600 && Mon_Nature <= -50); //bedeg
                            PlaceHighlight(3);
                            MoveInfo.Text = "Special tech. INT should be over 600 to unlock. Requires Bad (-50) Nature.";
                            break;
                        case 5:
                            MoveName.Text = "Jab"; MoveName.ForeColor = PowCol;
                            MoveGuts.Text = "10";
                            MoveDamage.Text = GenerateStatValue(0, 6);
                            MoveHit.Text = GenerateStatValue(1, 11);
                            MoveGD.Text = "---";
                            MoveSharp.Text = "---";
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[6].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[6] == 1);
                            CanUnlock.Checked = true;
                            PlaceHighlight(6);
                            MoveInfo.Text = "Starting basic tech.";
                            break;
                        case 6:
                            MoveName.Text = "Jab Combo"; MoveName.ForeColor = PowCol;
                            MoveGuts.Text = "18";
                            MoveDamage.Text = GenerateStatValue(0, 15);
                            MoveHit.Text = GenerateStatValue(1, -1);
                            MoveGD.Text = GenerateStatValue(2, 5);
                            MoveSharp.Text = GenerateStatValue(3, 5);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[7].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[7] == 1);
                            CanUnlock.Checked = (Mon_Stats[1] >= 250);
                            PlaceHighlight(7);
                            MoveInfo.Text = "Heavy tech. POW should be over 250 to unlock.";
                            break;
                        case 7:
                            MoveName.Text = "Plant Combo"; MoveName.ForeColor = PowCol;
                            MoveGuts.Text = "25";
                            MoveDamage.Text = GenerateStatValue(0, 25);
                            MoveHit.Text = GenerateStatValue(1, -11);
                            MoveGD.Text = GenerateStatValue(2, 10);
                            MoveSharp.Text = GenerateStatValue(3, 5);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[8].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[8] == 1);
                            CanUnlock.Checked = (Mon_Stats[1] >= 450);
                            PlaceHighlight(8);
                            MoveInfo.Text = "Heavy tech. POW should be over 450 to unlock.";
                            break;
                        case 8:
                            MoveName.Text = "Toxic Nectar"; MoveName.ForeColor = IntCol;
                            MoveGuts.Text = "26";
                            MoveDamage.Text = GenerateStatValue(0, 4);
                            MoveHit.Text = GenerateStatValue(1, 9);
                            MoveGD.Text = GenerateStatValue(2, 22);
                            MoveSharp.Text = GenerateStatValue(3, 5);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[12].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[12] == 1);
                            CanUnlock.Checked = (Mon_Stats[2] >= 250);
                            PlaceHighlight(12);
                            MoveInfo.Text = "Withering tech. INT should be over 250 to unlock.";
                            break;
                        case 9:
                            MoveName.Text = "Toxic Pollen"; MoveName.ForeColor = IntCol;
                            MoveGuts.Text = "35";
                            MoveDamage.Text = GenerateStatValue(0, 8);
                            MoveHit.Text = GenerateStatValue(1, -1);
                            MoveGD.Text = GenerateStatValue(2, 33);
                            MoveSharp.Text = GenerateStatValue(3, 5);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[13].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[13] == 1);
                            CanUnlock.Checked = (Mon_Stats[2] >= 450);
                            PlaceHighlight(13);
                            MoveInfo.Text = "Withering tech. INT should be over 450 to unlock.";
                            break;
                        case 10:
                            MoveName.Text = "Face Drill"; MoveName.ForeColor = PowCol;
                            MoveGuts.Text = "35";
                            MoveDamage.Text = GenerateStatValue(0, 21);
                            MoveHit.Text = GenerateStatValue(1, 10);
                            MoveGD.Text = GenerateStatValue(2, 17);
                            MoveSharp.Text = GenerateStatValue(3, 5);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[14].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[14] == 1);
                            CanUnlock.Checked = (Mon_Stats[1] + Mon_Stats[3] >= 800); //bedeg
                            PlaceHighlight(14);
                            MoveInfo.Text = "Special tech. POW + SKI should total over 800 to unlock.";
                            break;
                        case 11:
                            MoveName.Text = "Seed Gun"; MoveName.ForeColor = IntCol;
                            MoveGuts.Text = "21";
                            MoveDamage.Text = GenerateStatValue(0, 12);
                            MoveHit.Text = GenerateStatValue(1, 1);
                            MoveGD.Text = GenerateStatValue(2, 10);
                            MoveSharp.Text = GenerateStatValue(3, 19);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[18].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[18] == 1);
                            CanUnlock.Checked = (Mon_Stats[2] + Mon_Stats[4] >= 400);
                            PlaceHighlight(18);
                            MoveInfo.Text = "Sharp tech. INT + SPD should total over 400 to unlock. Chains into Seed Gatling when used 25x.";
                            break;
                        case 12:
                            MoveName.Text = "Seed Gatling"; MoveName.ForeColor = IntCol;
                            MoveGuts.Text = "27";
                            MoveDamage.Text = GenerateStatValue(0, 20);
                            MoveHit.Text = GenerateStatValue(1, -4);
                            MoveGD.Text = GenerateStatValue(2, 10);
                            MoveSharp.Text = GenerateStatValue(3, 25);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[19].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[19] == 1);
                            CanUnlock.Checked = (Mon_MoveUsed[18] >= 25 && (Mon_Stats[2] + Mon_Stats[4] >= 650) && Mon_Nature >= 20); //bedeg
                            PlaceHighlight(19);
                            MoveInfo.Text = @"Sharp tech. Chains from 25 Seed Guns. INT + SPD should total over 650 to unlock.
Requires Good (+20) Nature.";
                            break;
                        default:
                            MoveName.Text = "No Move"; MoveName.ForeColor = Color.Black;
                            MoveGuts.Text = "---";
                            MoveDamage.Text = "---";
                            MoveHit.Text = "---";
                            MoveGD.Text = "---";
                            MoveSharp.Text = "---";
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = "---";
                            MoveUnlocked.Checked = false;
                            CanUnlock.Checked = false;
                            MoveInfo.Text = "Click on a move to learn more.";
                            break;
                    }
                    break;

                case 34: // Monol
                    switch (MoveSelected)
                    {
                        case 1:
                            MoveName.Text = "Charge";
                            LazyDataFill(false, 10, 12, 10, 0, 0, 0);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = true;
                            MoveInfo.Text = "Starting basic tech.";
                            break;

                        case 2:
                            MoveName.Text = "Needle Stabs";
                            LazyDataFill(false, 21, 27, -2, 10, 5, 1);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_Stats[1] >= 250);
                            MoveInfo.Text = "Heavy tech. POW should be over 250 to unlock.\r\nChains into Spike Stabs when used 50x. Learning priority over Spike Bite.";
                            break;

                        case 3:
                            MoveName.Text = "Spike Stabs";
                            LazyDataFill(false, 27, 34, -4, 11, 8, 2);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_MoveUsed[1] >= 50);
                            MoveInfo.Text = "Heavy tech. Chains from 50 Needle Stabs. No stat requirement.";
                            break;

                        case 4:
                            MoveName.Text = "Ray";
                            LazyDataFill(true, 35, 25, 4, 35, 5, 3);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_Stats[2] >= 600);
                            MoveInfo.Text = "Special tech. INT should be over 600 to unlock.\r\nChains into Double Rays when used 50x.";
                            break;

                        case 5:
                            MoveName.Text = "Double Rays";
                            LazyDataFill(true, 40, 30, 1, 40, 5, 4);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_MoveUsed[3] >= 50);
                            MoveInfo.Text = "Special tech. Chains from 50 Ray.\r\nChains into Triple Rays when used 50x. No stat requirement.";
                            break;

                        case 6:
                            MoveName.Text = "Triple Rays";
                            LazyDataFill(true, 45, 35, 0, 45, 10, 5);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_MoveUsed[4] >= 50);
                            MoveInfo.Text = "Special tech. Chains from 50 Double Rays. No stat requirement.";
                            break;

                        case 7:
                            MoveName.Text = "Flattening";
                            LazyDataFill(false, 12, 16, 5, 0, 0, 6);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = true;
                            MoveInfo.Text = "Starting basic tech. Chains into Flattening-L when used 30x.";
                            break;

                        case 8:
                            MoveName.Text = "Spike Bite";
                            LazyDataFill(false, 29, 39, -12, 20, 10, 7);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_Stats[1] >= 450);
                            MoveInfo.Text = "Heavy tech. POW should be over 450 to unlock.";
                            break;

                        case 9:
                            MoveName.Text = "Scratch";
                            LazyDataFill(false, 20, 16, -2, 24, 5, 8);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_Stats[1] + Mon_Stats[2] >= 400);
                            MoveInfo.Text = "Withering tech. POW + INT should total over 400 to unlock.";
                            break;

                        case 10:
                            MoveName.Text = "Knock";
                            LazyDataFill(false, 35, 35, 10, 10, 5, 9);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_Stats[1] + Mon_Stats[5] >= 800);
                            MoveInfo.Text = "Special tech. POW + DEF should total over 800 to unlock.\r\nChains into Two Knocks when used 50x.";
                            break;

                        case 11:
                            MoveName.Text = "Two Knocks";
                            LazyDataFill(false, 45, 40, 15, 15, 5, 10);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_MoveUsed[9] >= 50);
                            MoveInfo.Text = "Special tech. Chains from 50 Knock.\r\nChains into Three Knocks when used 50x. No stat requirement.";
                            break;

                        case 12:
                            MoveName.Text = "Three Knocks";
                            LazyDataFill(false, 50, 42, 15, 21, 5, 11);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_MoveUsed[10] >= 50);
                            MoveInfo.Text = "Special tech. Chains from 50 Two Knocks. No stat requirement.";
                            break;

                        case 13:
                            MoveName.Text = "Flattening-L";
                            LazyDataFill(false, 15, 19, 5, 5, 0, 12);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_MoveUsed[6] >= 30);
                            MoveInfo.Text = "Basic tech. Chains from 30 Flattening.\r\nChains into Flattening-X when used 30x. No stat requirement.";
                            break;

                        case 14:
                            MoveName.Text = "Screech";
                            LazyDataFill(true, 25, 16, 24, 14, 5, 13);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_Stats[2] + Mon_Stats[3] >= 650);
                            MoveInfo.Text = "Hit tech. INT + SKI should total over 650 to unlock.";
                            break;

                        case 15:
                            MoveName.Text = "StrangeLight";
                            LazyDataFill(true, 18, 23, -7, 6, 15, 14);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_Stats[2] + Mon_Stats[4] >= 400);
                            MoveInfo.Text = "Sharp tech. INT + SPD should total over 400 to unlock.\r\nLearning priority over Beam.";
                            break;

                        case 16:
                            MoveName.Text = "Flattening-X";
                            LazyDataFill(false, 20, 24, 2, 5, 5, 18);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_MoveUsed[12] >= 30);
                            MoveInfo.Text = "Basic tech. Chains from 30 Flattening-L. No stat requirement.";
                            break;

                        case 17:
                            MoveName.Text = "Sound Wave";
                            LazyDataFill(true, 17, 15, 18, 5, 5, 19);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_Stats[2] + Mon_Stats[3] >= 400);
                            MoveInfo.Text = "Hit tech. INT + SKI should total over 400 to unlock.\r\nLearning priority over Screech.";
                            break;

                        case 18:
                            MoveName.Text = "Tentacles";
                            LazyDataFill(false, 37, 27, 0, 41, 5, 20);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_Stats[1] + Mon_Stats[2] >= 650 && Mon_Nature <= -20);
                            MoveInfo.Text = "Withering tech. POW + INT should total over 650 to unlock.\r\nRequires Bad (-20) Nature.";
                            break;

                        case 19:
                            MoveName.Text = "Beam";
                            LazyDataFill(true, 24, 28, -3, 5, 17, 21);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_Stats[2] + Mon_Stats[4] >= 650);
                            MoveInfo.Text = "Sharp tech. INT + SPD should total over 650 to unlock.\r\nChains into Double Beams when used 50x.";
                            break;

                        case 20:
                            MoveName.Text = "Double Beams";
                            LazyDataFill(true, 29, 31, -6, 14, 19, 22);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_MoveUsed[21] >= 50);
                            MoveInfo.Text = "Sharp tech. Chains from 50 Beam.\r\nChains into Triple Beams when used 50x. No stat requirement.";
                            break;

                        case 21:
                            MoveName.Text = "Triple Beams";
                            LazyDataFill(true, 40, 38, -6, 22, 25, 23);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_MoveUsed[22] >= 50 && Mon_Nature >= 20);
                            MoveInfo.Text = "Special tech. Chains from 50 Double Beams.\r\nRequires Good (+20) Nature. No stat requirement.";
                            break;

                        default:
                            MoveName.Text = "No Move"; MoveName.ForeColor = Color.Black;
                            MoveGuts.Text = "---";
                            MoveDamage.Text = "---";
                            MoveHit.Text = "---";
                            MoveGD.Text = "---";
                            MoveSharp.Text = "---";
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = "---";
                            MoveUnlocked.Checked = false;
                            CanUnlock.Checked = false;
                            MoveInfo.Text = "Click on a move to learn more.";
                            break;
                    }
                    break;

                case 35: // Ape
                    switch (MoveSelected)
                    {
                        case 1:
                            MoveName.Text = "Slap"; MoveName.ForeColor = PowCol;
                            MoveGuts.Text = "13";
                            MoveDamage.Text = GenerateStatValue(0, 15);
                            MoveHit.Text = GenerateStatValue(1, 8);
                            MoveGD.Text = GenerateStatValue(2, 5);
                            MoveSharp.Text = "---";
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[0].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[0] == 1);
                            CanUnlock.Checked = true;
                            PlaceHighlight(0);
                            MoveInfo.Text = "Starting basic tech.";
                            break;
                        case 2:
                            MoveName.Text = "Sneeze"; MoveName.ForeColor = IntCol;
                            MoveGuts.Text = "16";
                            MoveDamage.Text = GenerateStatValue(0, 10);
                            MoveHit.Text = GenerateStatValue(1, -6);
                            MoveGD.Text = GenerateStatValue(2, 26);
                            MoveSharp.Text = GenerateStatValue(3, 5);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[1].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[1] == 1);
                            CanUnlock.Checked = (Mon_Stats[2] >= 250);
                            PlaceHighlight(1);
                            MoveInfo.Text = "Withering tech. INT should be over 250 to unlock.";
                            break;
                        case 3:
                            MoveName.Text = "Swing-Throw"; MoveName.ForeColor = PowCol;
                            MoveGuts.Text = "50";
                            MoveDamage.Text = GenerateStatValue(0, 61);
                            MoveHit.Text = GenerateStatValue(1, -10);
                            MoveGD.Text = GenerateStatValue(2, 37);
                            MoveSharp.Text = GenerateStatValue(3, 10);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[2].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[2] == 1);
                            CanUnlock.Checked = (Mon_Stats[1] >= 600);
                            PlaceHighlight(2);
                            MoveInfo.Text = "Special tech. POW should be over 600 to unlock.";
                            break;
                        case 4:
                            MoveName.Text = "Thwack"; MoveName.ForeColor = PowCol;
                            MoveGuts.Text = "16";
                            MoveDamage.Text = GenerateStatValue(0, 20);
                            MoveHit.Text = GenerateStatValue(1, 3);
                            MoveGD.Text = GenerateStatValue(2, 5);
                            MoveSharp.Text = "---";
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[6].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[6] == 1);
                            CanUnlock.Checked = true;
                            PlaceHighlight(6);
                            MoveInfo.Text = "Starting basic tech.";
                            break;
                        case 5:
                            MoveName.Text = "Blast"; MoveName.ForeColor = IntCol;
                            MoveGuts.Text = "26";
                            MoveDamage.Text = GenerateStatValue(0, 21);
                            MoveHit.Text = GenerateStatValue(1, -8);
                            MoveGD.Text = GenerateStatValue(2, 38);
                            MoveSharp.Text = GenerateStatValue(3, 8);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[7].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[7] == 1);
                            CanUnlock.Checked = (Mon_Stats[2] >= 450 && Mon_Nature <= -20); //bedeg
                            PlaceHighlight(7);
                            MoveInfo.Text = "Withering tech. INT should be over 450 to unlock. Requires Bad (-20) Nature.";
                            break;
                        case 6:
                            MoveName.Text = "Boomerang"; MoveName.ForeColor = IntCol;
                            MoveGuts.Text = "18";
                            MoveDamage.Text = GenerateStatValue(0, 15);
                            MoveHit.Text = GenerateStatValue(1, 14);
                            MoveGD.Text = GenerateStatValue(2, 8);
                            MoveSharp.Text = GenerateStatValue(3, 8);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[12].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[12] == 1);
                            CanUnlock.Checked = (Mon_Stats[2] + Mon_Stats[3] >= 400);
                            PlaceHighlight(12);
                            MoveInfo.Text = "Hit tech. INT + SKI should total over 400 to unlock.";
                            break;
                        case 7:
                            MoveName.Text = "Grab-Throw"; MoveName.ForeColor = PowCol;
                            MoveGuts.Text = "18";
                            MoveDamage.Text = GenerateStatValue(0, 31);
                            MoveHit.Text = GenerateStatValue(1, -11);
                            MoveGD.Text = GenerateStatValue(2, 5);
                            MoveSharp.Text = GenerateStatValue(3, 5);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[13].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[13] == 1);
                            CanUnlock.Checked = (Mon_Stats[1] >= 250);
                            PlaceHighlight(13);
                            MoveInfo.Text = "Heavy tech. POW should be over 250 to unlock.";
                            break;
                        case 8:
                            MoveName.Text = "Big Banana"; MoveName.ForeColor = IntCol;
                            MoveGuts.Text = "28"; //bedeg
                            MoveDamage.Text = GenerateStatValue(0, 19);
                            MoveHit.Text = GenerateStatValue(1, 18);
                            MoveGD.Text = GenerateStatValue(2, 16);
                            MoveSharp.Text = GenerateStatValue(3, 8);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[18].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[18] == 1);
                            CanUnlock.Checked = (Mon_Stats[2] + Mon_Stats[3] >= 650);
                            PlaceHighlight(18);
                            MoveInfo.Text = "Hit tech. INT + SKI should total over 650 to unlock.";
                            break;
                        case 9:
                            MoveName.Text = "Roll Assault"; MoveName.ForeColor = PowCol;
                            MoveGuts.Text = "27";
                            MoveDamage.Text = GenerateStatValue(0, 41);
                            MoveHit.Text = GenerateStatValue(1, -9);
                            MoveGD.Text = GenerateStatValue(2, 11);
                            MoveSharp.Text = GenerateStatValue(3, 5);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[19].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[19] == 1);
                            CanUnlock.Checked = (Mon_Stats[1] >= 450);
                            PlaceHighlight(19);
                            MoveInfo.Text = "Heavy tech. POW should be over 450 to unlock.";
                            break;
                        case 10:
                            MoveName.Text = "Bomb"; MoveName.ForeColor = IntCol;
                            MoveGuts.Text = "19";
                            MoveDamage.Text = GenerateStatValue(0, 22);
                            MoveHit.Text = GenerateStatValue(1, -7);
                            MoveGD.Text = GenerateStatValue(2, 10);
                            MoveSharp.Text = GenerateStatValue(3, 16);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[20].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[20] == 1);
                            CanUnlock.Checked = (Mon_Stats[2] + Mon_Stats[4] >= 400);
                            PlaceHighlight(20);
                            MoveInfo.Text = "Sharp tech. Chains into Big Bomb when used 50x. INT + SPD should total over 400 to unlock.";
                            break;
                        case 11:
                            MoveName.Text = "Big Bomb"; MoveName.ForeColor = IntCol;
                            MoveGuts.Text = "26";
                            MoveDamage.Text = GenerateStatValue(0, 24);
                            MoveHit.Text = GenerateStatValue(1, 1);
                            MoveGD.Text = GenerateStatValue(2, 12);
                            MoveSharp.Text = GenerateStatValue(3, 18);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[21].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[21] == 1);
                            CanUnlock.Checked = (Mon_Stats[2] + Mon_Stats[4] >= 650 && Mon_Nature <= -30); //bedeg
                            PlaceHighlight(21);
                            MoveInfo.Text = @"Sharp tech. Chains from 50 Bombs. INT + SPD should total over 650 to unlock.
Requires Bad (-30) Nature.";
                            break;
                        case 12:
                            MoveName.Text = "Tasty Banana"; MoveName.ForeColor = IntCol;
                            MoveGuts.Text = "40";
                            MoveDamage.Text = "---";
                            MoveHit.Text = GenerateStatValue(1, -25);
                            MoveGD.Text = "---";
                            MoveSharp.Text = "---";
                            MoveSH.Show();
                            MoveSH.Text = GenerateStatValue(0, 30);
                            SHLabel.Show();
                            SHLabel.Text = "Recovery:";
                            MoveUses.Text = Mon_MoveUsed[22].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[22] == 1);
                            CanUnlock.Checked = (Mon_Stats[2] >= 500 && Mon_Nature >= 50); //bedeg
                            PlaceHighlight(22);
                            MoveInfo.Text = "Special tech. INT should be over 500 to unlock. Requires Good (+50) Nature.";
                            break;
                        default:
                            MoveName.Text = "No Move"; MoveName.ForeColor = Color.Black;
                            MoveGuts.Text = "---";
                            MoveDamage.Text = "---";
                            MoveHit.Text = "---";
                            MoveGD.Text = "---";
                            MoveSharp.Text = "---";
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = "---";
                            MoveUnlocked.Checked = false;
                            CanUnlock.Checked = false;
                            MoveInfo.Text = "Click on a move to learn more.";
                            break;
                    }
                    break;
                case 36: // Worm--
                    switch (MoveSelected)
                    {
                        case 1:
                            MoveName.Text = "Bite"; MoveName.ForeColor = PowCol;
                            MoveGuts.Text = "13";
                            MoveDamage.Text = GenerateStatValue(0, 14);
                            MoveHit.Text = GenerateStatValue(1, 5);
                            MoveGD.Text = "---";
                            MoveSharp.Text = "---";
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[0].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[0] == 1);
                            CanUnlock.Checked = true;
                            PlaceHighlight(0);
                            MoveInfo.Text = "Starting basic tech.";
                            break;
                        case 2:
                            MoveName.Text = "Somersault"; MoveName.ForeColor = PowCol;
                            MoveGuts.Text = "26";
                            MoveDamage.Text = GenerateStatValue(0, 20);
                            MoveHit.Text = GenerateStatValue(1, 18);
                            MoveGD.Text = GenerateStatValue(2, 9);
                            MoveSharp.Text = GenerateStatValue(3, 5);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[1].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[1] == 1);
                            CanUnlock.Checked = (Mon_Stats[1] + Mon_Stats[3] >= 650);
                            PlaceHighlight(1);
                            MoveInfo.Text = "Hit tech. POW + SKI should total over 650 to unlock.\r\nChains into Somersaults when used 50x.";
                            if (rng.Next(100) < 10) //bedeg
                                MoveInfo.Text += " 🠫 ↑+K";
                            break;
                        case 3:
                            MoveName.Text = "Somersaults"; MoveName.ForeColor = PowCol;
                            MoveGuts.Text = "30";
                            MoveDamage.Text = GenerateStatValue(0, 24);
                            MoveHit.Text = GenerateStatValue(1, 18);
                            MoveGD.Text = GenerateStatValue(2, 11);
                            MoveSharp.Text = GenerateStatValue(3, 5);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[2].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[2] == 1);
                            CanUnlock.Checked = (Mon_MoveUsed[1] >= 50);
                            PlaceHighlight(2);
                            MoveInfo.Text = "Hit tech. Chains from 50 uses of Somersault.";
                            break;
                        case 4:
                            MoveName.Text = "Sting"; MoveName.ForeColor = PowCol;
                            MoveGuts.Text = "10";
                            MoveDamage.Text = GenerateStatValue(0, 10);
                            MoveHit.Text = GenerateStatValue(1, 7);
                            MoveGD.Text = "---";
                            MoveSharp.Text = GenerateStatValue(3, 5);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[6].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[6] == 1);
                            CanUnlock.Checked = true;
                            PlaceHighlight(6);
                            MoveInfo.Text = "Starting basic tech.";
                            break;
                        case 5:
                            MoveName.Text = "Tail Lash"; MoveName.ForeColor = PowCol;
                            MoveGuts.Text = "19";
                            MoveDamage.Text = GenerateStatValue(0, 15);
                            MoveHit.Text = GenerateStatValue(1, 20);
                            MoveGD.Text = GenerateStatValue(2, 5);
                            MoveSharp.Text = GenerateStatValue(3, 5);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[7].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[7] == 1);
                            CanUnlock.Checked = (Mon_Stats[1] + Mon_Stats[3] >= 400);
                            PlaceHighlight(7);
                            MoveInfo.Text = "Hit tech. POW + SKI should total over 400 to unlock. Chains into Two Lashes when used 50x.";
                            break;
                        case 6:
                            MoveName.Text = "Two Lashes"; MoveName.ForeColor = PowCol;
                            MoveGuts.Text = "27";
                            MoveDamage.Text = GenerateStatValue(0, 19);
                            MoveHit.Text = GenerateStatValue(1, 16);
                            MoveGD.Text = GenerateStatValue(2, 13);
                            MoveSharp.Text = GenerateStatValue(3, 5);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[8].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[8] == 1);
                            CanUnlock.Checked = (Mon_MoveUsed[7] >= 50);
                            PlaceHighlight(8);
                            MoveInfo.Text = "Hit tech. Chains from 50 Tail Lashes. Chains into Three Lashes when used 50x.";
                            break;
                        case 7:
                            MoveName.Text = "Three Lashes"; MoveName.ForeColor = PowCol;
                            MoveGuts.Text = "34";
                            MoveDamage.Text = GenerateStatValue(0, 26);
                            MoveHit.Text = GenerateStatValue(1, 13);
                            MoveGD.Text = GenerateStatValue(2, 15);
                            MoveSharp.Text = GenerateStatValue(3, 5);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[9].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[9] == 1);
                            CanUnlock.Checked = (Mon_MoveUsed[8] >= 50);
                            PlaceHighlight(9);
                            MoveInfo.Text = "Special tech. Chains from 50 Two Lashes uses.";
                            break;
                        case 8:
                            MoveName.Text = "Roll Assault"; MoveName.ForeColor = PowCol;
                            MoveGuts.Text = "50";
                            MoveDamage.Text = GenerateStatValue(0, 50);
                            MoveHit.Text = GenerateStatValue(1, -10);
                            MoveGD.Text = GenerateStatValue(2, 25);
                            MoveSharp.Text = GenerateStatValue(3, 5);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[10].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[10] == 1);
                            CanUnlock.Checked = (Mon_Stats[1] >= 600);
                            PlaceHighlight(10);
                            MoveInfo.Text = "Special tech. POW should be over 600 to unlock."; //bedeg
                            break;
                        case 9:
                            MoveName.Text = "Pierce-Throw"; MoveName.ForeColor = PowCol;
                            MoveGuts.Text = "20";
                            MoveDamage.Text = GenerateStatValue(0, 28);
                            MoveHit.Text = GenerateStatValue(1, -8);
                            MoveGD.Text = GenerateStatValue(2, 7);
                            MoveSharp.Text = GenerateStatValue(3, 5);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[11].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[11] == 1);
                            CanUnlock.Checked = (Mon_Stats[1] >= 250);
                            PlaceHighlight(11);
                            MoveInfo.Text = "Heavy tech. POW should be over 250 to unlock.";
                            break;
                        case 10:
                            MoveName.Text = "Pinch-Throw"; MoveName.ForeColor = PowCol;
                            MoveGuts.Text = "29";
                            MoveDamage.Text = GenerateStatValue(0, 40);
                            MoveHit.Text = GenerateStatValue(1, -8);
                            MoveGD.Text = GenerateStatValue(2, 10);
                            MoveSharp.Text = GenerateStatValue(3, 5);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[12].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[12] == 1);
                            CanUnlock.Checked = (Mon_Stats[1] >= 450);
                            PlaceHighlight(12);
                            MoveInfo.Text = "Heavy tech. POW should be over 450 to unlock.";
                            break;
                        case 11:
                            MoveName.Text = "Pierce"; MoveName.ForeColor = PowCol;
                            MoveGuts.Text = "18";
                            MoveDamage.Text = GenerateStatValue(0, 12);
                            MoveHit.Text = GenerateStatValue(1, -5);
                            MoveGD.Text = GenerateStatValue(2, 23);
                            MoveSharp.Text = GenerateStatValue(3, 5);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[13].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[13] == 1);
                            CanUnlock.Checked = (Mon_Stats[2] >= 250);
                            PlaceHighlight(13);
                            MoveInfo.Text = "Withering tech. INT should be over 250 to unlock. Despite being a POW tech. :|";
                            break;
                        case 12:
                            MoveName.Text = "Tusk Slash"; MoveName.ForeColor = PowCol;
                            MoveGuts.Text = "17";
                            MoveDamage.Text = GenerateStatValue(0, 16);
                            MoveHit.Text = GenerateStatValue(1, 4);
                            MoveGD.Text = GenerateStatValue(2, 5);
                            MoveSharp.Text = GenerateStatValue(3, 17);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[14].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[14] == 1);
                            CanUnlock.Checked = (Mon_Stats[1] + Mon_Stats[4] >= 400);
                            PlaceHighlight(14);
                            MoveInfo.Text = "Sharp tech. POW + SPD should total over 400 to unlock.";
                            break;
                        case 13:
                            MoveName.Text = "Injection"; MoveName.ForeColor = IntCol;
                            MoveGuts.Text = "28";
                            MoveDamage.Text = GenerateStatValue(0, 25);
                            MoveHit.Text = GenerateStatValue(1, -11);
                            MoveGD.Text = GenerateStatValue(2, 23);
                            MoveSharp.Text = GenerateStatValue(3, 25);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[15].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[15] == 1);
                            CanUnlock.Checked = (Mon_Stats[2] + Mon_Stats[4] >= 650);
                            PlaceHighlight(15);
                            MoveInfo.Text = "Sharp tech. INT + SPD should total over 650 to unlock."; //bedeg
                            break;
                        case 14:
                            MoveName.Text = "Poison Gas"; MoveName.ForeColor = IntCol;
                            MoveGuts.Text = "28";
                            MoveDamage.Text = GenerateStatValue(0, 20);
                            MoveHit.Text = GenerateStatValue(1, -7);
                            MoveGD.Text = GenerateStatValue(2, 37);
                            MoveSharp.Text = GenerateStatValue(3, 5);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[18].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[18] == 1);
                            CanUnlock.Checked = (Mon_Stats[2] >= 450);
                            PlaceHighlight(18);
                            MoveInfo.Text = "Withering tech. INT should be over 450 to unlock.";
                            break;
                        case 15:
                            MoveName.Text = "Wheel Attack"; MoveName.ForeColor = PowCol;
                            MoveGuts.Text = "44";
                            MoveDamage.Text = GenerateStatValue(0, 35);
                            MoveHit.Text = GenerateStatValue(1, -4); //bedeg
                            MoveGD.Text = GenerateStatValue(2, 35);
                            MoveSharp.Text = GenerateStatValue(3, 15); //bedeg
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[19].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[19] == 1);
                            CanUnlock.Checked = (Mon_Stats[1] + Mon_Stats[2] + Mon_Stats[4] >= 1200);
                            PlaceHighlight(19);
                            MoveInfo.Text = "Special tech. POW + INT + SPD should total over 1200 to unlock.";
                            break;
                        default:
                            MoveName.Text = "No Move"; MoveName.ForeColor = Color.Black;
                            MoveGuts.Text = "---";
                            MoveDamage.Text = "---";
                            MoveHit.Text = "---";
                            MoveGD.Text = "---";
                            MoveSharp.Text = "---";
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = "---";
                            MoveUnlocked.Checked = false;
                            CanUnlock.Checked = false;
                            MoveInfo.Text = "Click on a move to learn more.";
                            break;
                    }
                    break;
                case 37: // Naga
                    switch (MoveSelected)
                    {
                        case 1:
                            MoveName.Text = "Thwack"; MoveName.ForeColor = PowCol;
                            MoveGuts.Text = "10";
                            MoveDamage.Text = GenerateStatValue(0, 12);
                            MoveHit.Text = GenerateStatValue(1, -5);
                            MoveGD.Text = "---";
                            MoveSharp.Text = "---";
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[0].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[0] == 1);
                            CanUnlock.Checked = true;
                            PlaceHighlight(0);
                            MoveInfo.Text = "Starting basic tech.";
                            break;
                        case 2:
                            MoveName.Text = "Stab"; MoveName.ForeColor = PowCol;
                            MoveGuts.Text = "14";
                            MoveDamage.Text = GenerateStatValue(0, 15);
                            MoveHit.Text = GenerateStatValue(1, 8);
                            MoveGD.Text = GenerateStatValue(2, 5);
                            MoveSharp.Text = GenerateStatValue(3, 5);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[1].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[1] == 1);
                            CanUnlock.Checked = (Mon_Stats[1] + Mon_Stats[3] >= 400);
                            PlaceHighlight(1);
                            MoveInfo.Text = "Hit tech. POW + SKI should total over 400 to unlock.";
                            break;
                        case 3:
                            MoveName.Text = "Belly Punch"; MoveName.ForeColor = PowCol;
                            MoveGuts.Text = "10"; //bedeg
                            MoveDamage.Text = GenerateStatValue(0, 14);
                            MoveHit.Text = GenerateStatValue(1, -8);
                            MoveGD.Text = GenerateStatValue(2, 5);
                            MoveSharp.Text = "---";
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[6].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[6] == 1); //bedeg
                            CanUnlock.Checked = true;
                            PlaceHighlight(6);
                            MoveInfo.Text = "Starting basic tech.";
                            break;
                        case 4:
                            MoveName.Text = "Pierce"; MoveName.ForeColor = PowCol;
                            MoveGuts.Text = "19";
                            MoveDamage.Text = GenerateStatValue(0, 18);
                            MoveHit.Text = GenerateStatValue(1, 8);
                            MoveGD.Text = GenerateStatValue(2, 10);
                            MoveSharp.Text = GenerateStatValue(3, 5);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[7].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[7] == 1);
                            CanUnlock.Checked = (Mon_Stats[1] + Mon_Stats[3] >= 650);
                            PlaceHighlight(7);
                            MoveInfo.Text = "Hit tech. POW + SKI should total over 650 to unlock.";
                            break;
                        case 5:
                            MoveName.Text = "Tail Assault"; MoveName.ForeColor = PowCol;
                            MoveGuts.Text = "15";
                            MoveDamage.Text = GenerateStatValue(0, 27);
                            MoveHit.Text = GenerateStatValue(1, -13);
                            MoveGD.Text = GenerateStatValue(2, 5);
                            MoveSharp.Text = GenerateStatValue(3, 5);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[8].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[8] == 1);
                            CanUnlock.Checked = (Mon_Stats[1] >= 250);
                            PlaceHighlight(8);
                            MoveInfo.Text = "Heavy tech. POW should be over 250 to unlock.";
                            break;
                        case 6:
                            MoveName.Text = "Life Steal"; MoveName.ForeColor = IntCol;
                            MoveGuts.Text = "50";
                            MoveDamage.Text = GenerateStatValue(0, 30);
                            MoveHit.Text = GenerateStatValue(1, -19);
                            MoveGD.Text = "---";
                            MoveSharp.Text = "---";
                            MoveSH.Show(); //bedeg
                            MoveSH.Text = "100%";
                            SHLabel.Show();
                            SHLabel.Text = "HP Drain:";
                            MoveUses.Text = Mon_MoveUsed[9].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[9] == 1);
                            CanUnlock.Checked = (Mon_Stats[2] >= 500 && Mon_Nature <= -50);
                            PlaceHighlight(9);
                            MoveInfo.Text = "Special tech. INT should be over 500 to unlock. Requires Bad (-50) Nature.";
                            break;
                        case 7:
                            MoveName.Text = "Poison Gas"; MoveName.ForeColor = IntCol;
                            MoveGuts.Text = "15";
                            MoveDamage.Text = GenerateStatValue(0, 12);
                            MoveHit.Text = GenerateStatValue(1, -4);
                            MoveGD.Text = GenerateStatValue(2, 19);
                            MoveSharp.Text = GenerateStatValue(3, 8);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[12].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[12] == 1);
                            CanUnlock.Checked = (Mon_Stats[2] >= 250);
                            PlaceHighlight(12);
                            MoveInfo.Text = "Withering tech. INT should be over 250 to unlock.";
                            break;
                        case 8:
                            MoveName.Text = "Energy Shot"; MoveName.ForeColor = IntCol;
                            MoveGuts.Text = "17";
                            MoveDamage.Text = GenerateStatValue(0, 21);
                            MoveHit.Text = GenerateStatValue(1, -13);
                            MoveGD.Text = GenerateStatValue(2, 15);
                            MoveSharp.Text = GenerateStatValue(3, 15);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[13].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[13] == 1);
                            CanUnlock.Checked = (Mon_Stats[2] + Mon_Stats[4] >= 400);
                            PlaceHighlight(13);
                            MoveInfo.Text = "Sharp tech. INT + SPD should total over 400 to unlock. Chains into Energy Shots when used 25x.";
                            break;
                        case 9:
                            MoveName.Text = "Turn Assault"; MoveName.ForeColor = PowCol;
                            MoveGuts.Text = "45";
                            MoveDamage.Text = GenerateStatValue(0, 39);
                            MoveHit.Text = GenerateStatValue(1, 5);
                            MoveGD.Text = GenerateStatValue(2, 29);
                            MoveSharp.Text = GenerateStatValue(3, 7);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[14].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[14] == 1);
                            CanUnlock.Checked = (Mon_Stats[1] >= 600);
                            PlaceHighlight(14);
                            MoveInfo.Text = "Special tech. POW should be over 600 to unlock.";
                            break;
                        case 10:
                            MoveName.Text = "Drill Attack"; MoveName.ForeColor = PowCol;
                            MoveGuts.Text = "23";
                            MoveDamage.Text = GenerateStatValue(0, 38);
                            MoveHit.Text = GenerateStatValue(1, -13);
                            MoveGD.Text = GenerateStatValue(2, 12);
                            MoveSharp.Text = GenerateStatValue(3, 5);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[18].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[18] == 1);
                            CanUnlock.Checked = (Mon_Stats[1] >= 450);
                            PlaceHighlight(18);
                            MoveInfo.Text = "Heavy tech. POW should be over 450 to unlock.";
                            break;
                        case 11:
                            MoveName.Text = "Eye Beam"; MoveName.ForeColor = IntCol;
                            MoveGuts.Text = "29";
                            MoveDamage.Text = GenerateStatValue(0, 31);
                            MoveHit.Text = GenerateStatValue(1, -11);
                            MoveGD.Text = GenerateStatValue(2, 23);
                            MoveSharp.Text = GenerateStatValue(3, 21);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[19].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[19] == 1);
                            CanUnlock.Checked = (Mon_Stats[2] + Mon_Stats[4] >= 650);
                            PlaceHighlight(19);
                            MoveInfo.Text = "Sharp tech. INT + SPD should total over 650 to unlock. Requires Good (+20) Nature.";
                            break;
                        case 12:
                            MoveName.Text = "Energy Shots"; MoveName.ForeColor = IntCol;
                            MoveGuts.Text = "24";
                            MoveDamage.Text = GenerateStatValue(0, 27);
                            MoveHit.Text = GenerateStatValue(1, -9);
                            MoveGD.Text = GenerateStatValue(2, 15);
                            MoveSharp.Text = GenerateStatValue(3, 19);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[20].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[20] == 1);
                            CanUnlock.Checked = ((Mon_Stats[2] + Mon_Stats[4] >= 650) && Mon_MoveUsed[13] >= 25);
                            PlaceHighlight(20);
                            MoveInfo.Text = "Sharp tech. Chains from 25 Energy Shot uses. INT + SPD should total over 650 to unlock.";
                            break;
                        default:
                            MoveName.Text = "No Move"; MoveName.ForeColor = Color.Black;
                            MoveGuts.Text = "---";
                            MoveDamage.Text = "---";
                            MoveHit.Text = "---";
                            MoveGD.Text = "---";
                            MoveSharp.Text = "---";
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = "---";
                            MoveUnlocked.Checked = false;
                            CanUnlock.Checked = false;
                            MoveInfo.Text = "Click on a move to learn more.";
                            break;
                    }
                    break;
                default: // Unfinished Monsters:
                    MoveName.Text = "Unrecognised Monster"; MoveName.ForeColor = Color.Black;
                    MoveGuts.Text = "---";
                    MoveDamage.Text = "---";
                    MoveHit.Text = "---";
                    MoveGD.Text = "---";
                    MoveSharp.Text = "---";
                    MoveSH.Hide();
                    SHLabel.Hide();
                    MoveSH.Text = "---";
                    MoveUses.Text = "---";
                    MoveInfo.Text = "This monster isn't currently recognised by the Move Viewer.";
                    break;
            }
            SHBoxLoc = SHLabel.Width + SHLabel.Left;
            if (SHBoxLoc > 315)
                MoveSH.Left = SHBoxLoc;
            else
                MoveSH.Left = 315;
        }
    }
}
