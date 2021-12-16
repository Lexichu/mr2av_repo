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
    public partial class MonMoveWindow : Form
    {
        public int[] Mon_Moves = new int[24];
        public int[] Mon_MoveUsed = new int[24];
        public int Mon_Genus, Mon_SubGenus;
        public int oldGenus = -1;
        public int MoveSelected = -1;
        public int rowSelected, colSelected;
        public int[] Mon_Stats = new int[6];
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
                            MoveInfo.Text = "Special tech. INT + SPD should total over 800 to unlock. Chains into Big Bang when used 50x.";
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
                            CanUnlock.Checked = (Mon_Stats[1] + Mon_Stats[3] > 300 && Mon_SubGenus == 16);
                            MoveInfo.Text = "Hit tech. Unique to Kitten. POW + SKI should total over 300 to unlock.";
                            break;
                        case 10:
                            MoveName.Text = "Death Final";
                            LazyDataFill(false, 52, 44, -18, 44, 5, 11);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_Stats[1] + Mon_Stats[2] > 1000 && Mon_SubGenus == 26);
                            MoveInfo.Text = "Special tech. Unique to Lilim. POW + INT should total over 1000 to unlock.";
                            break;
                        case 11:
                            MoveName.Text = "Bolt";
                            LazyDataFill(true, 16, 9, 13, 5, 3, 12);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            CanUnlock.Checked = (Mon_Stats[2] + Mon_Stats[3] > 300);
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
                            CanUnlock.Checked = (Mon_Stats[2] > 200);
                            MoveInfo.Text = "Withering tech. INT should be over 200 to unlock.";
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
                            CanUnlock.Checked = (Mon_Stats[2] > 600);
                            MoveInfo.Text = "Special tech. INT should be over 600 to unlock. Requires Worst (-50) Nature.";
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
                            CanUnlock.Checked = (Mon_Stats[2] > 600);
                            MoveInfo.Text = "Special tech. INT should be over 600 to unlock. Requires Best (+50) Nature.";
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
                            CanUnlock.Checked = (Mon_Stats[1] + Mon_Stats[2] > 1000);
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
                            CanUnlock.Checked = (Mon_Stats[1] + Mon_Stats[2] > 300 && Mon_SubGenus != 24);
                            MoveInfo.Text = @"Heavy tech. Unlearnable by Snowy. POW + INT should total over 300 to unlock.
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
                            CanUnlock.Checked = (Mon_Stats[2] + Mon_Stats[4] > 300);
                            MoveInfo.Text = "Sharp tech. INT + SPD should total over 300 to unlock. Chains into Megaray when used 50x.";
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
                            CanUnlock.Checked = (Mon_MoveUsed[22] >= 50);
                            MoveInfo.Text = "Sharp tech. Chains from 50 Megarays. Requires Good (+20) Nature.";
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
                            CanUnlock.Checked = (Mon_Stats[1] + Mon_Stats[4] > 350);
                            MoveInfo.Text = "Sharp tech. POW + SPD should total over 350 to unlock.";
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
                            CanUnlock.Checked = (Mon_Stats[1] + Mon_Stats[3] > 350);
                            MoveInfo.Text = "Hit tech. Pow + SKI should total over 350 to unlock. Chains into Wing Combo when used 50x.";
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
                            MoveInfo.Text = "Hit tech. Chains from 50 Wing Attacks. No stat requirement. Learning priority over Flutters.";
                            break;
                        case 8:
                            MoveName.Text = "Claw Combo"; MoveName.ForeColor = PowCol;
                            MoveGuts.Text = "18";
                            MoveDamage.Text = GenerateStatValue(0, 30);
                            MoveHit.Text = GenerateStatValue(1, -10);
                            MoveGD.Text = GenerateStatValue(2, 9);
                            MoveSharp.Text = GenerateStatValue(3, 5);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[8].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[8] == 1);
                            CanUnlock.Checked = (Mon_Stats[1] > 250);
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
                            CanUnlock.Checked = (Mon_Stats[1] + Mon_Stats[4] > 600);
                            MoveInfo.Text = @"Sharp tech. POW + SPD should total over 600 to unlock. Requires Bad (-20) Nature. 
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
                            CanUnlock.Checked = (Mon_MoveUsed[9] >= 50);
                            MoveInfo.Text = "Sharp tech. Chains from 50 Claws. Requires Worst (-50) Nature.";
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
                            CanUnlock.Checked = (Mon_Stats[2] + Mon_Stats[3] > 600);
                            MoveInfo.Text = "Hit tech. INT + SKI should total over 600 to unlock. Chains into Flutters when used 50x.";
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
                            CanUnlock.Checked = (Mon_Stats[2] > 450);
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
                            CanUnlock.Checked = (Mon_Stats[2] > 250);
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
                            CanUnlock.Checked = (Mon_Stats[1] + Mon_Stats[2] > 1000);
                            MoveInfo.Text = "Special tech. POW + INT should total over 1000 to unlock. Requires Best (+50) Nature.";
                            break;
                        case 16:
                            MoveName.Text = "Inferno"; MoveName.ForeColor = IntCol;
                            MoveGuts.Text = "29";
                            MoveDamage.Text = GenerateStatValue(0, 27);
                            MoveHit.Text = GenerateStatValue(1, -8);
                            MoveGD.Text = GenerateStatValue(2, 39);
                            MoveSharp.Text = GenerateStatValue(3, 8);
                            MoveDamage.Text = "C (27)";
                            MoveHit.Text = "D (-8)";
                            MoveGD.Text = "B (39)";
                            MoveSharp.Text = "E (8)";
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[18].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[18] == 1);
                            CanUnlock.Checked = (Mon_MoveUsed[15] >= 50 && Mon_Stats[2] > 400);
                            MoveInfo.Text = "Withering tech. Requires 50 Fire Breaths, and INT of over 400.";
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
                            CanUnlock.Checked = (Mon_Stats[1] + Mon_Stats[4] > 600);
                            MoveInfo.Text = "Sharp tech. POW + SPD should total over 600 to unlock. Requires Good (20) Nature.";
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
                            CanUnlock.Checked = (Mon_Stats[1] + Mon_Stats[2] > 800);
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
                            CanUnlock.Checked = (Mon_Stats[1] > 450 && Mon_MoveUsed[8] >= 50);
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
                            CanUnlock.Checked = (Mon_MoveUsed[1] > 30);
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
                            CanUnlock.Checked = (Mon_Stats[1] + Mon_Stats[3] > 600);
                            MoveInfo.Text = "Hit tech. POW + SKI should total over 600 to unlock.";
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
                            CanUnlock.Checked = (Mon_Stats[1] > 400);
                            MoveInfo.Text = "Heavy tech. POW should be over 400 to unlock. Chains into Death Thrust when used 50x.";
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
                            CanUnlock.Checked = (Mon_MoveUsed[9] >= 50);
                            MoveInfo.Text = "Special tech. Chains from 50 Cross Slashes. Requires Best (+50) Nature.";
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
                            CanUnlock.Checked = (Mon_Stats[1] + Mon_Stats[3] > 350);
                            MoveInfo.Text = "Hit tech. POW + SKI should total over 350 to unlock. Learning priority over Triple Stabs";
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
                            CanUnlock.Checked = (Mon_Stats[2] > 400);
                            MoveInfo.Text = "Withering tech. INT should be over 400 to unlock. Chains into Mind Blast when used 50x.";
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
                            CanUnlock.Checked = (Mon_Stats[1] + Mon_Stats[3] + Mon_Stats[5] > 1200);
                            MoveInfo.Text = @"Special tech. POW + SKI + DEF should total over 1200 to unlock. Requires Good (+20) Nature.
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
                            CanUnlock.Checked = (Mon_Stats[2] > 250);
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
                            CanUnlock.Checked = (Mon_Stats[1] + Mon_Stats[4] > 350);
                            MoveInfo.Text = "Sharp tech. POW + SPD should total over 350 to unlock.";
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
                            CanUnlock.Checked = (Mon_Moves[3] >= 50 && Mon_Stats[1] > 600);
                            MoveInfo.Text = "Heavy tech. Chains from 50 Stab-Throws. Requires 600 POW, and Worst (-50) Nature.";
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
                            CanUnlock.Checked = (Mon_Stats[1] > 250);
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
                            CanUnlock.Checked = (Mon_MoveUsed[14] >= 50 && (Mon_Stats[1] + Mon_Stats[4] > 600));
                            MoveInfo.Text = "Sharp tech. Chains from 50 Javelins. POW + SPD should total over 600 to unlock.";
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
                            CanUnlock.Checked = (Mon_Stats[1] + Mon_Stats[4] + Mon_Stats[5] > 1200);
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
                            MoveHit.Text = GenerateStatValue(1, 5);
                            MoveGD.Text = "---";
                            MoveSharp.Text = "---";
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[0].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[0] == 1);
                            CanUnlock.Checked = true;
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
                            CanUnlock.Checked = (Mon_Stats[1] > 250);
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
                            MoveInfo.Text = "Starting basic tech. Chains into Double Swing when used 30x.";
                            break;
                        case 4:
                            MoveName.Text = "Double Swing"; MoveName.ForeColor = PowCol;
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
                            MoveInfo.Text = "Basic tech. Chains from 30 Tail Swings. Only available from Torble Sea Errantry.";
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
                            SHLabel.Text = "Self-Damage:";
                            SHLabel.Show();
                            MoveUses.Text = Mon_MoveUsed[8].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[8] == 1);
                            CanUnlock.Checked = (Mon_Stats[1] + Mon_Stats[3] > 550);
                            MoveInfo.Text = "Hit tech. POW + SKI should total over 550 to unlock.";
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
                            CanUnlock.Checked = (Mon_Stats[2] > 600);
                            MoveInfo.Text = "Special tech. INT should be over 600 to unlock. Requires Worst (-50) Nature.";
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
                            CanUnlock.Checked = (Mon_Stats[2] > 400);
                            MoveInfo.Text = "Withering tech. INT should be over 400 to unlock. Chains into Megacracker when used 50x.";
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
                            CanUnlock.Checked = (Mon_MoveUsed[7] >= 50);
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
                            CanUnlock.Checked = (Mon_Stats[1] + Mon_Stats[4] > 350);
                            MoveInfo.Text = "Sharp tech. POW + SPD should total over 350 to unlock. Chains into Delta Attack when used 50x.";
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
                            CanUnlock.Checked = (Mon_MoveUsed[14] >= 50 && (Mon_Stats[2] + Mon_Stats[3] + Mon_Stats[4] > 1200));
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
                            CanUnlock.Checked = (Mon_Stats[2] + Mon_Stats[3] > 350);
                            MoveInfo.Text = @"Hit tech. INT + SKI should total over 350 to unlock. Chains into Megashotgun when used 50x.
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
                            CanUnlock.Checked = (Mon_Stats[1] > 450);
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
                            CanUnlock.Checked = (Mon_Stats[1] + Mon_Stats[4] > 600);
                            MoveInfo.Text = "Sharp tech. POW + SPD should be over 600 to unlock.";
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
                            CanUnlock.Checked = (Mon_Stats[0] + Mon_Stats[5] > 800);
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
                            MoveInfo.Text = "Starting basic tech. Chains into Horn Attack when used 50x. (may be 30x, unsure)";
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
                            MoveInfo.Text = "Basic tech. Chains from 50(30?) Horn Strikes.";
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
                            CanUnlock.Checked = (Mon_Stats[1] + Mon_Stats[2] > 800);
                            MoveInfo.Text = "Withering tech. POW + INT should total over 800 to unlock. Requires Bad (-20) Nature.";
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
                            CanUnlock.Checked = (Mon_Stats[1] + Mon_Stats[4] > 800);
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
                            CanUnlock.Checked = (Mon_Stats[1] > 250);
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
                            CanUnlock.Checked = (Mon_Stats[2] > 250);
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
                            CanUnlock.Checked = (Mon_Stats[1] + Mon_Stats[4] > 600);
                            MoveInfo.Text = "Sharp tech. POW + SPD should total over 600 to unlock.";
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
                            CanUnlock.Checked = (Mon_Stats[1] + Mon_Stats[3] + Mon_Stats[4] > 1200);
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
                            CanUnlock.Checked = ((Mon_Stats[2] + Mon_Stats[5] > 800) && Mon_SubGenus == 7 && Mon_MoveUsed[14] >= 50);
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
                            CanUnlock.Checked = (Mon_Stats[1] + Mon_Stats[3] > 350);
                            MoveInfo.Text = "Hit tech. POW + SKI should total over 350 to unlock.";
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
                            CanUnlock.Checked = (Mon_Stats[1] + Mon_Stats[3] > 600);
                            MoveInfo.Text = "Hit tech. POW + SKI should total over 600 to unlock. Requires Good (+20) Nature.";
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
                            CanUnlock.Checked = (Mon_Stats[1] > 450);
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
                            CanUnlock.Checked = (Mon_Stats[2] + Mon_Stats[4] > 350);
                            MoveInfo.Text = "Sharp tech. INT + SPD should total over 350 to unlock. Learning priority over Horn Combo.";
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
                            CanUnlock.Checked = (Mon_MoveUsed[16] > 50);
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
                            CanUnlock.Checked = ((Mon_Stats[1] + Mon_Stats[4] > 800) && Mon_SubGenus == 5);
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
                            MoveInfo.Text = "Starting basic tech. Chains into Heavy Chop when used 30x.";
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
                            MoveInfo.Text = "Basic tech. Chains from 30 Low Kicks.";
                            break;
                        case 3:
                            MoveName.Text = "Heavy Chop"; MoveName.ForeColor = PowCol;
                            MoveGuts.Text = "10";
                            MoveDamage.Text = GenerateStatValue(0, 18);
                            MoveHit.Text = GenerateStatValue(1, 3);
                            MoveGD.Text = GenerateStatValue(2, 6);
                            MoveSharp.Text = "---";
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[2].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[2] == 1);
                            CanUnlock.Checked = (Mon_MoveUsed[0] >= 30);
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
                            CanUnlock.Checked = (Mon_Stats[1] > 450);
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
                            CanUnlock.Checked = (Mon_Stats[1] + Mon_Stats[2] > 350);
                            MoveInfo.Text = @"Withering tech. POW + INT should total over 350 to unlock.
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
                            CanUnlock.Checked = (Mon_Stats[1] + Mon_Stats[4] > 800);
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
                            MoveInfo.Text = "Special tech. Chains from 50 Laser Sword uses.";
                            break;
                        case 9:
                            MoveName.Text = "Two Cutters"; MoveName.ForeColor = PowCol;
                            MoveGuts.Text = "50";
                            MoveDamage.Text = GenerateStatValue(0, 50);
                            MoveHit.Text = GenerateStatValue(1, -18);
                            MoveGD.Text = GenerateStatValue(2, 10);
                            MoveSharp.Text = GenerateStatValue(3, 10);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[10].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[10] == 1);
                            CanUnlock.Checked = (Mon_MoveUsed[6] >= 50);
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
                            CanUnlock.Checked = (Mon_Stats[2] + Mon_Stats[3] > 350);
                            MoveInfo.Text = "Hit tech. INT + SKI should total over 350 to unlock. Chains into Napalm Shot when used 50x.";
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
                            CanUnlock.Checked = (Mon_MoveUsed[12] > 50 && (Mon_Stats[2] + Mon_Stats[3] > 600));
                            MoveInfo.Text = @"Hit tech. Chains from 50 Arm Cannons. Chains into Burst Cannon when used 50x.
INT + SKI should total over 600 to unlock.";
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
                            CanUnlock.Checked = (Mon_Stats[1] > 250);
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
                            CanUnlock.Checked = (Mon_Stats[2] > 450);
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
                            CanUnlock.Checked = (Mon_Stats[1] + Mon_Stats[4] > 350);
                            MoveInfo.Text = @"Sharp tech. POW + SPD should total over 350 to unlock. Chains into Drill Shot when used 50x.
Requires Good (+20) Nature.";
                            break;
                        case 18:
                            MoveName.Text = "Drill Shot"; MoveName.ForeColor = IntCol;
                            MoveGuts.Text = "27";
                            MoveDamage.Text = GenerateStatValue(0, 22);
                            MoveHit.Text = GenerateStatValue(1, 0);
                            MoveGD.Text = GenerateStatValue(2, 12);
                            MoveSharp.Text = GenerateStatValue(3, 25);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[20].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[20] == 1);
                            CanUnlock.Checked = (Mon_MoveUsed[19] >= 50);
                            MoveInfo.Text = @"Sharp tech. Chains from 50 Fist Missiles. Chains into Drill Shots when used 50x.
Requires Good (~+30) Nature.";
                            break;
                        case 19:
                            MoveName.Text = "Drill Shots"; MoveName.ForeColor = IntCol;
                            MoveGuts.Text = "45";
                            MoveDamage.Text = GenerateStatValue(0, 35);
                            MoveHit.Text = GenerateStatValue(1, 4);
                            MoveGD.Text = GenerateStatValue(2, 14);
                            MoveSharp.Text = GenerateStatValue(3, 30);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[21].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[21] == 1);
                            CanUnlock.Checked = (Mon_MoveUsed[20] >= 50);
                            MoveInfo.Text = "Special tech. Chains from 50 Drill Shot uses. Requires Best (+50) Nature.";
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
                            CanUnlock.Checked = (Mon_MoveUsed[0] > 50 && (Mon_Stats[2] + Mon_Stats[4] > 600));
                            MoveInfo.Text = "Sharp tech. Chains from 50 Punches(?!). INT + SPD should be over 600 to unlock.";
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
                            CanUnlock.Checked = (Mon_Stats[2] > 250);
                            MoveInfo.Text = "Withering tech. Requires 250+ INT to unlock.";
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
                            MoveInfo.Text = "Starting basic tech.";
                            break;
                        case 4:
                            MoveName.Text = "One-Two"; MoveName.ForeColor = PowCol;
                            MoveGuts.Text = "18";
                            MoveDamage.Text = GenerateStatValue(0, 19);
                            MoveHit.Text = GenerateStatValue(1, 9);
                            MoveGD.Text = GenerateStatValue(2, 5);
                            MoveSharp.Text = GenerateStatValue(3, 20);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[7].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[7] == 1);
                            CanUnlock.Checked = (Mon_Stats[1] > 250);
                            MoveInfo.Text = "Heavy tech. Requires 250+ POW to unlock.";
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
                            CanUnlock.Checked = (Mon_Stats[2] > 450);
                            MoveInfo.Text = "Withering tech. Requires 400+ INT to unlock.";
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
                            CanUnlock.Checked = (Mon_Stats[1] + Mon_Stats[3] > 350);
                            MoveInfo.Text = "Hit tech. POW + SKI should total over 350 to unlock.";
                            break;
                        case 7:
                            MoveName.Text = "Combination"; MoveName.ForeColor = PowCol;
                            MoveGuts.Text = "25";
                            MoveDamage.Text = GenerateStatValue(0, 19);
                            MoveHit.Text = GenerateStatValue(1, 17);
                            MoveGD.Text = GenerateStatValue(2, 13);
                            MoveSharp.Text = GenerateStatValue(3, 5);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[13].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[13] == 1);
                            CanUnlock.Checked = (Mon_Stats[1] > 450);
                            MoveInfo.Text = "Heavy tech. Requires 450+ POW to unlock.";
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
                            CanUnlock.Checked = (Mon_Stats[2] + Mon_Stats[4] > 350);
                            MoveInfo.Text = "Sharp tech. INT + SPD should total over 350 to unlock.";
                            break;
                        case 9:
                            MoveName.Text = "Frantic Rush"; MoveName.ForeColor = PowCol;
                            MoveGuts.Text = "19";
                            MoveDamage.Text = GenerateStatValue(0, 34);
                            MoveHit.Text = GenerateStatValue(1, 0);
                            MoveGD.Text = GenerateStatValue(2, 17);
                            MoveSharp.Text = GenerateStatValue(3, 25);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[15].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[15] == 1);
                            CanUnlock.Checked = (Mon_Stats[1] + Mon_Stats[4] > 800);
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
                            CanUnlock.Checked = (Mon_Stats[1] + Mon_Stats[3] > 600);
                            MoveInfo.Text = "Hit tech. POW + SKI should total over 600 to unlock.";
                            break;
                        case 11:
                            MoveName.Text = "Blizzard"; MoveName.ForeColor = IntCol;
                            MoveGuts.Text = "29";
                            MoveDamage.Text = GenerateStatValue(0, 20);
                            MoveHit.Text = GenerateStatValue(1, 2);
                            MoveGD.Text = GenerateStatValue(2, 11);
                            MoveSharp.Text = GenerateStatValue(3, 35);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[19].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[19] == 1);
                            CanUnlock.Checked = (Mon_Stats[2] + Mon_Stats[4] > 600);
                            MoveInfo.Text = "Sharp tech. INT + SPD should total over 600 to unlock.";
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
                            CanUnlock.Checked = (Mon_Stats[2] > 600);
                            MoveInfo.Text = "Special tech. Requires 600+ INT to unlock.";
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
                            CanUnlock.Checked = (Mon_Stats[1] + Mon_Stats[4] > 500);
                            MoveInfo.Text = "Sharp tech. Chains into 2 Jump Blows when used 50x. POW + SPD should total over 500 to unlock.";
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
                            CanUnlock.Checked = (Mon_MoveUsed[2] >= 50);
                            MoveInfo.Text = "Special tech. Chains from 50 uses of 2 Jump Blows. Requires Good (+20) Nature.";
                            break;
                        case 5:
                            MoveName.Text = "1-2 Jump Blow"; MoveName.ForeColor = PowCol;
                            MoveGuts.Text = "33";
                            MoveDamage.Text = GenerateStatValue(0, 29);
                            MoveHit.Text = GenerateStatValue(1, -9);
                            MoveGD.Text = GenerateStatValue(2, 18);
                            MoveSharp.Text = GenerateStatValue(3, 7);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[4].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[4] == 1);
                            CanUnlock.Checked = (Mon_Stats[1] + Mon_Stats[3] + Mon_Stats[4] > 800);
                            MoveInfo.Text = @"Special tech. Chains into Hopper Combo when used 50x.
POW + SKI + SPD should total over 800 to unlock.";
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
                            MoveInfo.Text = "Special tech. Chains from 50 1-2 Jump Blows.";
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
                            CanUnlock.Checked = (Mon_Stats[1] + Mon_Stats[3] > 300);
                            MoveInfo.Text = "Hit tech. Chains into Rapid Flick when used 50x. POW + SKI should total over 300 to unlock.";
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
                            CanUnlock.Checked = (Mon_SubGenus == 16);
                            MoveInfo.Text = "Hit tech. Exclusive to Mustachios.";
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
                            CanUnlock.Checked = (Mon_MoveUsed[18] >= 50);
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
                            CanUnlock.Checked = ((Mon_Stats[2] > 250) && Mon_SubGenus == 0);
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
                            CanUnlock.Checked = ((Mon_Stats[2] + Mon_Stats[3] + Mon_Stats[4] > 1200) && Mon_SubGenus == 0);
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
                            CanUnlock.Checked = (Mon_Stats[1] + Mon_Stats[3] > 350);
                            MoveInfo.Text = "Hit tech. POW + SKI should total over 350 to unlock.";
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
                            CanUnlock.Checked = (Mon_Stats[1] + Mon_Stats[4] > 400);
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
                            CanUnlock.Checked = (Mon_Stats[1] + Mon_Stats[3] > 600);
                            MoveInfo.Text = "Sharp tech. POW + SKI should total over 600 to unlock.";
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
                            CanUnlock.Checked = (Mon_Stats[2] > 250);
                            MoveInfo.Text = "Withering tech. Requires 250+ INT to unlock.";
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
                            CanUnlock.Checked = (Mon_Stats[1] + Mon_Stats[5] > 800);
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
                            CanUnlock.Checked = (Mon_Stats[1] > 450);
                            MoveInfo.Text = "Heavy tech. Requires 450+ POW to unlock.";
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
                            CanUnlock.Checked = (Mon_Stats[2] > 450);
                            MoveInfo.Text = "Withering tech. Requires 450+ INT and Bad (-20) Nature to unlock.";
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
                            CanUnlock.Checked = (Mon_Stats[1] + Mon_Stats[4] > 600);
                            MoveInfo.Text = "Sharp tech. POW + SPD should total over 600 to unlock. Chains into Zilla Rush when used 50x.";
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
                            CanUnlock.Checked = (Mon_Stats[1] > 200);
                            MoveInfo.Text = "Heavy tech. Requires 200+ POW to unlock.";
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
                            CanUnlock.Checked = (Mon_Stats[0] + Mon_Stats[2] > 800);
                            MoveInfo.Text = "Special tech. LIF + INT should total over 800 to unlock.";
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
                            MoveInfo.Text = @"Heavy tech. Chains from 50 Straights. Chains into 1-2 Hook when used 50x.
Requires 450+ POW to unlock.";
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
                            MoveInfo.Text = "Starting basic tech. Chains into Straight and 1-2 Smash when used 50x.";
                            break;
                        case 4:
                            MoveName.Text = "1-2 Hook"; MoveName.ForeColor = PowCol;
                            MoveGuts.Text = "30";
                            MoveDamage.Text = GenerateStatValue(0, 46);
                            MoveHit.Text = GenerateStatValue(1, -16);
                            MoveGD.Text = GenerateStatValue(2, 15);
                            MoveSharp.Text = GenerateStatValue(3, 5);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[8].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[8] == 1);
                            CanUnlock.Checked = false;
                            MoveInfo.Text = "Heavy tech. Chains from 50 Hooks.";
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
                            CanUnlock.Checked = (Mon_Stats[1] > 250);
                            MoveInfo.Text = @"Heavy tech. Requires 250+ POW to unlock. Chains from 50 Right Jabs.
Chains into Hook and Magic Punch when used 50x.";
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
                            CanUnlock.Checked = (Mon_Stats[1] + Mon_Stats[4] > 350);
                            MoveInfo.Text = "Sharp tech. POW + SPD should total over 350 to unlock. Chains into 1-2 Uppercut when used 50x.";
                            break;
                        case 7:
                            MoveName.Text = "1-2 Uppercut"; MoveName.ForeColor = PowCol;
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
                            MoveInfo.Text = "Sharp tech. Chains from 50 Uppercuts. Chains into Mystic Combo when used 50x.";
                            break;
                        case 8:
                            MoveName.Text = "1-2 Smash"; MoveName.ForeColor = PowCol;
                            MoveGuts.Text = "36";
                            MoveDamage.Text = GenerateStatValue(0, 53);
                            MoveHit.Text = GenerateStatValue(1, -16);
                            MoveGD.Text = GenerateStatValue(2, 21);
                            MoveSharp.Text = GenerateStatValue(3, 5);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[12].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[12] == 1);
                            CanUnlock.Checked = (Mon_MoveUsed[7] >= 50);
                            MoveInfo.Text = "Special tech. Chains from Right Jab when used 50x. Requires Good (+20) Nature.";
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
                            CanUnlock.Checked = (Mon_MoveUsed[11] >= 50);
                            MoveInfo.Text = @"Special tech. Chains from 50 1-2 Uppercuts. Requires Good (+20) Nature.";
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
                            CanUnlock.Checked = (Mon_MoveUsed[13] >= 50);
                            MoveInfo.Text = "Special tech. Chains from 50 Magic Punches. Requires Good (+20[?]) Nature.";
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
                            CanUnlock.Checked = (Mon_Stats[2] > 500);
                            MoveInfo.Text = @"Special tech. INT should be over 500 to unlock. Requires Bad (-20) Nature.
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
                            CanUnlock.Checked = (Mon_MoveUsed[19] >= 50);
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
                            CanUnlock.Checked = (Mon_MoveUsed[19] >= 50);
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
                            CanUnlock.Checked = (Mon_Stats[2] + Mon_Stats[3] > 800);
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
                            MoveInfo.Text = "Starting basic tech.";
                            break;
                        case 3:
                            MoveName.Text = "Rapid Beaks"; MoveName.ForeColor = PowCol;
                            MoveGuts.Text = "23";
                            MoveDamage.Text = GenerateStatValue(0, 20);
                            MoveHit.Text = GenerateStatValue(1, -3);
                            MoveGD.Text = GenerateStatValue(2, 5);
                            MoveSharp.Text = GenerateStatValue(3, 23);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[7].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[7] == 1);
                            CanUnlock.Checked = (Mon_Stats[1] + Mon_Stats[3] > 350);
                            MoveInfo.Text = "Hit tech. POW + SKI should total over 350 to unlock.";
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
                            CanUnlock.Checked = (Mon_Stats[2] > 250);
                            MoveInfo.Text = "Withering tech. Requires 250+ INT to unlock. Chains into Flame Cannon when used 50x.";
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
                            CanUnlock.Checked = (Mon_MoveUsed[12] >= 50);
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
                            CanUnlock.Checked = (Mon_Stats[2] + Mon_Stats[4] > 350);
                            MoveInfo.Text = "Sharp tech. INT + SPD should total over 350 to unlock. Chains into Fire Tornado when used 50x.";
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
                            CanUnlock.Checked = (Mon_MoveUsed[14] >= 50);
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
                            CanUnlock.Checked = (Mon_Stats[2] > 250);
                            MoveInfo.Text = "Heavy tech. Requires 250+ INT to unlock.";
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
                            CanUnlock.Checked = (Mon_Stats[2] > 500);
                            MoveInfo.Text = "Special tech. Requires 500+ INT to unlock. Chains into Fire Wave when used 50x.";
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
                            MoveDamage.Text = GenerateStatValue(0, 11);
                            MoveHit.Text = GenerateStatValue(1, -5);
                            MoveGD.Text = "---";
                            MoveSharp.Text = "---";
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[0].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[0] == 1);
                            CanUnlock.Checked = true;
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
                            CanUnlock.Checked = (Mon_Stats[1] + Mon_Stats[2] > 600);
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
                            CanUnlock.Checked = (Mon_MoveUsed[2] >= 25 && (Mon_Stats[1] + Mon_Stats[2] > 800));
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
                            CanUnlock.Checked = (Mon_Stats[2] + Mon_Stats[3] > 350);
                            MoveInfo.Text = "Hit tech. Chains into Necromancy when used 50x. INT + SKI should total over 350 to unlock.";
                            break;
                        case 6:
                            MoveName.Text = "Surprise"; MoveName.ForeColor = IntCol;
                            MoveGuts.Text = "17";
                            MoveDamage.Text = GenerateStatValue(0, 9);
                            MoveHit.Text = GenerateStatValue(1, -10);
                            MoveGD.Text = GenerateStatValue(2, 20);
                            MoveSharp.Text = GenerateStatValue(3, 5);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[7].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[7] == 1);
                            CanUnlock.Checked = (Mon_Stats[2] > 250);
                            MoveInfo.Text = "Withering tech. Chains into Astonishment when used 25x. Requires 250+ INT to unlock.";
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
                            CanUnlock.Checked = (Mon_MoveUsed[7] >= 25 && (Mon_Stats[2] > 450));
                            MoveInfo.Text = "Withering tech. Chains from 25 Surprises. Requires 450+ INT to unlock.";
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
                            CanUnlock.Checked = (Mon_MoveUsed[6] >= 50 && (Mon_Stats[2] + Mon_Stats[3] > 550));
                            MoveInfo.Text = "Hit tech. Chains from 50 Energy Shots. POW + SKI should total over 550 to unlock.";
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
                            CanUnlock.Checked = (Mon_Stats[2] > 200);
                            MoveInfo.Text = "Heavy tech. Chains into Pigeon Bomb when used 50x. Requires 200+ INT to unlock.";
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
                            CanUnlock.Checked = (Mon_MoveUsed[13] >= 50 && (Mon_Stats[2] > 450));
                            MoveInfo.Text = "Heavy tech. Chains from 50 Dove Bombs. Requires 450+ INT to unlock.";
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
                            CanUnlock.Checked = (Mon_Stats[2] + Mon_Stats[4] > 350);
                            MoveInfo.Text = "Sharp tech. Chains into Magic Cards when used 25x. INT + SPD should total over 350 to unlock.";
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
                            CanUnlock.Checked = (Mon_MoveUsed[18] >= 25 && (Mon_Stats[2] + Mon_Stats[4] > 600));
                            MoveInfo.Text = "Sharp tech. Chains from 50 uses of Magic Card. INT + SPD should total over 600 to unlock.";
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
                            CanUnlock.Checked = (Mon_Stats[1] + Mon_Stats[4] > 650);
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
                            CanUnlock.Checked = (Mon_Stats[1] + Mon_Stats[3] > 350);
                            MoveInfo.Text = "Hit tech. Chains into Dash Straight when used 50x. POW + SKI should total over 350 to unlock.";
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
                            CanUnlock.Checked = (Mon_Stats[1] + Mon_Stats[4] > 350);
                            MoveInfo.Text = "Sharp tech. Chains into Double Kicks when used 50x. POW + SPD should total over 350 to unlock.";
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
                            CanUnlock.Checked = (Mon_Stats[1] + Mon_Stats[3] > 550);
                            MoveInfo.Text = "Hit tech. POW + SKI should total over 550 to unlock.";
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
                            CanUnlock.Checked = (Mon_Stats[1] > 250);
                            MoveInfo.Text = "Heavy tech. Requires 250+ POW to unlock.";
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
                            CanUnlock.Checked = (Mon_Stats[1] + Mon_Stats[2] > 400);
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
                            CanUnlock.Checked = (Mon_Stats[2] > 500);
                            MoveInfo.Text = "Special tech. Requires 500+ INT, and Bad (-20) Nature to unlock.";
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
                            CanUnlock.Checked = (Mon_Stats[1] > 500);
                            MoveInfo.Text = "Special tech. Requires 500+ POW, and Good (+20) Nature to unlock.";
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
                            CanUnlock.Checked = (Mon_Stats[1] > 400);
                            MoveInfo.Text = "Heavy tech. Requires 400+ POW to unlock.";
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
                            CanUnlock.Checked = (Mon_Stats[1] > 450);
                            MoveInfo.Text = "Heavy tech. Requires 450+ POW to unlock.";
                            break;
                        case 3:
                            MoveName.Text = "Kiss"; MoveName.ForeColor = IntCol;
                            MoveGuts.Text = "19";
                            MoveDamage.Text = GenerateStatValue(0, 20);
                            MoveHit.Text = GenerateStatValue(1, 1);
                            MoveGD.Text = GenerateStatValue(2, 5);
                            MoveSharp.Text = GenerateStatValue(3, 16);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[2].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[2] == 1);
                            CanUnlock.Checked = (Mon_Stats[2] > 450);
                            MoveInfo.Text = "Withering tech. Requires 450+ INT to unlock.";
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
                            CanUnlock.Checked = (Mon_Stats[1] > 600);
                            MoveInfo.Text = "Heavy tech. Requires 250+ POW to unlock.";
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
                            CanUnlock.Checked = (Mon_Stats[2] > 250);
                            MoveInfo.Text = "Withering tech. Requires 250+ INT to unlock.";
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
                            CanUnlock.Checked = (Mon_Stats[1] + Mon_Stats[2] > 800);
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
                            CanUnlock.Checked = (Mon_Stats[1] + Mon_Stats[3] > 350);
                            MoveInfo.Text = "Hit tech. POW + SKI should total over 350 to unlock.";
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
                            CanUnlock.Checked = (Mon_Stats[2] + Mon_Stats[3] > 600);
                            MoveInfo.Text = "Hit tech. INT + SKI should total over 600 to unlock.";
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
                            CanUnlock.Checked = (Mon_Stats[2] + Mon_Stats[4] > 350);
                            MoveInfo.Text = "Sharp tech. INT + SPD should total over 350 to unlock.";
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
                            CanUnlock.Checked = (Mon_Stats[2] + Mon_Stats[4] > 550);
                            MoveInfo.Text = "Sharp tech. INT + SPD should total over 550 to unlock.";
                            break;
                        case 12:
                            MoveName.Text = "Yodel"; MoveName.ForeColor = IntCol;
                            MoveGuts.Text = "32";
                            MoveDamage.Text = GenerateStatValue(0, 25);
                            MoveHit.Text = GenerateStatValue(1, -5);
                            MoveGD.Text = GenerateStatValue(2, 45);
                            MoveSharp.Text = GenerateStatValue(3, 5);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[19].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[19] == 1);
                            CanUnlock.Checked = (Mon_Stats[2] + Mon_Stats[4] > 750);
                            MoveInfo.Text = "Special tech. INT + SPD should total over 750 to unlock.";
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
                            CanUnlock.Checked = (Mon_Stats[2] + Mon_Stats[3] > 350);
                            MoveInfo.Text = "Hit tech. INT + SKI should total over 350 to unlock.";
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
                            CanUnlock.Checked = (Mon_Stats[2] > 250);
                            MoveInfo.Text = "Withering tech. Requires 250+ INT to unlock.";
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
                            CanUnlock.Checked = (Mon_Stats[1] > 400);
                            MoveInfo.Text = "Heavy tech. Requires 400+ POW to unlock. Chains into Jill Combo when used 50x.";
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
                            CanUnlock.Checked = (Mon_Stats[1] + Mon_Stats[2] > 600);
                            MoveInfo.Text = "Withering tech. POW + INT should total over 600 to unlock. Requires Bad (-20) Nature.";
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
                            CanUnlock.Checked = (Mon_Stats[2] + Mon_Stats[3] > 600);
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
                            CanUnlock.Checked = (Mon_Stats[1] + Mon_Stats[2] > 350);
                            MoveInfo.Text = "Heavy tech. POW + INT should total over 350 to unlock.";
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
                            CanUnlock.Checked = (Mon_Stats[1] + Mon_Stats[4] > 350);
                            MoveInfo.Text = "Sharp tech. POW + SPD should total over 350 to unlock.";
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
                            CanUnlock.Checked = ((Mon_Stats[1] + Mon_Stats[2] > 600) && Mon_MoveUsed[7] >= 50);
                            MoveInfo.Text = "Special tech. Chains from 50 Punch Combos. POW + INT should total over 600 to unlock.";
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
                            CanUnlock.Checked = (Mon_Stats[2] + Mon_Stats[4] > 600);
                            MoveInfo.Text = "Sharp tech. INT + SPD should total over 600 to unlock. Requires Good (+20) Nature.";
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
                            CanUnlock.Checked = (Mon_Stats[2] > 500);
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
                            CanUnlock.Checked = (Mon_Stats[1] > 400);
                            MoveInfo.Text = "Heavy tech. Requires Bad (-20) Nature and 400+ POW.";
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
                            CanUnlock.Checked = (Mon_Stats[2] + Mon_Stats[4] > 500);
                            MoveInfo.Text = "Sharp tech. INT + SPD should total over 500 to unlock. Requires Bad (-20) Nature.";
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
                            CanUnlock.Checked = (Mon_Stats[2] + Mon_Stats[3] > 550);
                            MoveInfo.Text = "Hit tech. INT + SKI should total over 550 to unlock. Requires Bad (-20) Nature.";
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
                            CanUnlock.Checked = (Mon_Stats[1] + Mon_Stats[2] > 800);
                            MoveInfo.Text = "Special tech. POW + INT should total over 800 to unlock. Requires Worst (-50) Nature.";
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
                            CanUnlock.Checked = (Mon_Stats[2] + Mon_Stats[3] > 400);
                            MoveInfo.Text = "Hit tech. INT + SKI should total over 350 to unlock.";
                            break;
                        case 3:
                            MoveName.Text = "Leaf Gun"; MoveName.ForeColor = PowCol;
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
                            CanUnlock.Checked = (Mon_MoveUsed[12] >= 50 && (Mon_Stats[1] + Mon_Stats[2] > 350));
                            MoveInfo.Text = "Heavy tech. Chains from 50 Leaf Guns. POW + INT should total over 350 to unlock.";
                            break;
                        case 5:
                            MoveName.Text = "Twig Gun"; MoveName.ForeColor = PowCol;
                            MoveGuts.Text = "18";
                            MoveDamage.Text = GenerateStatValue(0, 10);
                            MoveHit.Text = GenerateStatValue(1, -6);
                            MoveGD.Text = GenerateStatValue(2, 25);
                            MoveSharp.Text = GenerateStatValue(3, 5);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[14].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[14] == 1);
                            CanUnlock.Checked = (Mon_Stats[2] > 250);
                            MoveInfo.Text = "Withering tech. Requires 250+ INT to unlock.";
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
                            MoveInfo.Text = "Withering tech. No tech requirements.";
                            break;
                        case 7:
                            MoveName.Text = "Energy Steal"; MoveName.ForeColor = PowCol;
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
                            CanUnlock.Checked = (Mon_Stats[2] > 600);
                            MoveInfo.Text = "Special tech. Requires 600+ INT and Worst (-50) Nature.";
                            break;
                        case 8:
                            MoveName.Text = "Twister"; MoveName.ForeColor = PowCol;
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
                            MoveInfo.Text = "Special tech. Requires 600+ INT to unlock. Chains into Twisters when used 50x.";
                            break;
                        case 9:
                            MoveName.Text = "Twisters"; MoveName.ForeColor = IntCol;
                            MoveGuts.Text = "50";
                            MoveDamage.Text = GenerateStatValue(0, 55);
                            MoveHit.Text = GenerateStatValue(1, -3);
                            MoveGD.Text = GenerateStatValue(2, 44);
                            MoveSharp.Text = GenerateStatValue(3, 5);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[20].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[20] == 1);
                            CanUnlock.Checked = (Mon_MoveUsed[19] >= 50);
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
                            CanUnlock.Checked = (Mon_Stats[1] + Mon_Stats[3] > 400);
                            MoveInfo.Text = "Hit tech. POW + SKI should total over 400 to unlock.";
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
                            CanUnlock.Checked = (Mon_MoveUsed[1] >= 25 && (Mon_Stats[1] + Mon_Stats[3] > 600));
                            MoveInfo.Text = "Hit tech. Chains from 25 Roots. POW + SKI should total over 600 to unlock.";
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
                            CanUnlock.Checked = true;
                            MoveInfo.Text = "Special tech. INT should be over 600 to unlock. Requires Worst (-50) Nature.";
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
                            CanUnlock.Checked = (Mon_Stats[1] > 250);
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
                            CanUnlock.Checked = (Mon_Stats[1] > 450);
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
                            CanUnlock.Checked = (Mon_Stats[2] > 250);
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
                            CanUnlock.Checked = (Mon_Stats[2] > 400);
                            MoveInfo.Text = "Withering tech. INT should be over 400 to unlock.";
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
                            CanUnlock.Checked = (Mon_Stats[1] + Mon_Stats[2] > 800);
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
                            CanUnlock.Checked = (Mon_Stats[2] + Mon_Stats[4] > 350);
                            MoveInfo.Text = "Sharp tech. INT + SPD should total over 350 to unlock. Chains into Seed Gatling when used 25x.";
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
                            CanUnlock.Checked = (Mon_MoveUsed[18] >= 25 && (Mon_Stats[2] + Mon_Stats[4] > 600));
                            MoveInfo.Text = @"Sharp tech. Chains from 25 Seed Guns. INT + SPD should total over 600 to unlock.
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
                            CanUnlock.Checked = (Mon_Stats[2] > 250);
                            MoveInfo.Text = "Withering tech. Requires 250+ INT to unlock.";
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
                            CanUnlock.Checked = (Mon_Stats[1] > 600);
                            MoveInfo.Text = "Special tech. Requires 600+ POW to unlock.";
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
                            CanUnlock.Checked = (Mon_Stats[2] > 450);
                            MoveInfo.Text = "Withering tech. Requires 450+ INT, and Bad (-20) Nature to unlock.";
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
                            CanUnlock.Checked = (Mon_Stats[2] + Mon_Stats[3] > 350);
                            MoveInfo.Text = "Hit tech. INT + SKI should total over 350 to unlock.";
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
                            CanUnlock.Checked = (Mon_Stats[1] > 250);
                            MoveInfo.Text = "Heavy tech. Requires 250+ POW to unlock.";
                            break;
                        case 8:
                            MoveName.Text = "Big Banana"; MoveName.ForeColor = IntCol;
                            MoveGuts.Text = "19";
                            MoveDamage.Text = GenerateStatValue(0, 19);
                            MoveHit.Text = GenerateStatValue(1, 18);
                            MoveGD.Text = GenerateStatValue(2, 16);
                            MoveSharp.Text = GenerateStatValue(3, 8);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[18].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[18] == 1);
                            CanUnlock.Checked = (Mon_Stats[2] + Mon_Stats[3] > 600);
                            MoveInfo.Text = "Hit tech. INT + SKI should total over 600 to unlock.";
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
                            CanUnlock.Checked = (Mon_Stats[1] > 400);
                            MoveInfo.Text = "Heavy tech. Requires 400+ POW to unlock.";
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
                            CanUnlock.Checked = (Mon_Stats[2] + Mon_Stats[4] > 350);
                            MoveInfo.Text = "Sharp tech. Chains into Big Bomb when used 50x. INT + SPD should total over 350 to unlock.";
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
                            CanUnlock.Checked = (Mon_Stats[2] + Mon_Stats[4] > 600);
                            MoveInfo.Text = @"Sharp tech. Chains from 50 Bombs. INT + SPD should total over 600 to unlock.
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
                            CanUnlock.Checked = (Mon_Stats[2] > 500);
                            MoveInfo.Text = "Special tech. Requires Best (+50) Nature, and 500+ INT to unlock.";
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
                            CanUnlock.Checked = (Mon_Stats[1] + Mon_Stats[3] > 600);
                            MoveInfo.Text = "Hit tech. POW + SKI should total over 600 to unlock. Chains into Somersaults when used 50x.";
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
                            CanUnlock.Checked = (Mon_Stats[1] + Mon_Stats[3] > 350);
                            MoveInfo.Text = "Hit tech. POW + SKI should total over 350 to unlock. Chains into Two Lashes when used 50x.";
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
                            CanUnlock.Checked = (Mon_Stats[1] > 600);
                            MoveInfo.Text = "Heavy tech. Requires 600+ POW to unlock";
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
                            CanUnlock.Checked = (Mon_Stats[1] > 250);
                            MoveInfo.Text = "Heavy tech. Requires 250+ POW to unlock.";
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
                            CanUnlock.Checked = (Mon_Stats[1] > 450);
                            MoveInfo.Text = "Heavy tech. Requires 450+ POW to unlock.";
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
                            CanUnlock.Checked = (Mon_Stats[2] > 250);
                            MoveInfo.Text = "Withering tech. Requires 250+ INT to unlock. Despite being a POW tech. :|";
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
                            CanUnlock.Checked = (Mon_Stats[1] + Mon_Stats[4] > 350);
                            MoveInfo.Text = "Sharp tech. POW + SPD should total over 350 to unlock.";
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
                            CanUnlock.Checked = (Mon_Stats[2] + Mon_Stats[4] > 550);
                            MoveInfo.Text = "Sharp tech. POW + SPD should total over 550 to unlock.";
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
                            CanUnlock.Checked = (Mon_Stats[2] > 450);
                            MoveInfo.Text = "Withering tech. Requires 450+ INT to unlock.";
                            break;
                        case 15:
                            MoveName.Text = "Wheel Attack"; MoveName.ForeColor = PowCol;
                            MoveGuts.Text = "44";
                            MoveDamage.Text = GenerateStatValue(0, 35);
                            MoveHit.Text = GenerateStatValue(1, 5);
                            MoveGD.Text = GenerateStatValue(2, 35);
                            MoveSharp.Text = GenerateStatValue(3, 5);
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[19].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[19] == 1);
                            CanUnlock.Checked = (Mon_Stats[1] + Mon_Stats[2] + Mon_Stats[4] > 1200);
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
                            CanUnlock.Checked = (Mon_Stats[1] + Mon_Stats[3] > 350);
                            MoveInfo.Text = "Hit tech. POW + SKI should total over 350 to unlock.";
                            break;
                        case 3:
                            MoveName.Text = "Belly Punch"; MoveName.ForeColor = PowCol;
                            MoveGuts.Text = "18";
                            MoveDamage.Text = GenerateStatValue(0, 14);
                            MoveHit.Text = GenerateStatValue(1, -8);
                            MoveGD.Text = GenerateStatValue(2, 5);
                            MoveSharp.Text = "---";
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[6].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[2] == 1);
                            CanUnlock.Checked = true;
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
                            CanUnlock.Checked = (Mon_Stats[1] + Mon_Stats[3] > 550);
                            MoveInfo.Text = "Hit tech. POW + SKI should total over 550 to unlock.";
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
                            CanUnlock.Checked = (Mon_Stats[1] > 200);
                            MoveInfo.Text = "Heavy tech. Requires 200+ POW to be unlocked.";
                            break;
                        case 6:
                            MoveName.Text = "Life Steal"; MoveName.ForeColor = IntCol;
                            MoveGuts.Text = "50";
                            MoveDamage.Text = GenerateStatValue(0, 30);
                            MoveHit.Text = GenerateStatValue(1, -19);
                            MoveGD.Text = "---";
                            MoveSharp.Text = "---";
                            MoveSH.Hide();
                            SHLabel.Hide();
                            MoveUses.Text = Mon_MoveUsed[8].ToString();
                            MoveUnlocked.Checked = (Mon_Moves[8] == 1);
                            CanUnlock.Checked = (Mon_Stats[2] > 600);
                            MoveInfo.Text = "Special tech. Requires 600+ INT and Worst (-50) Nature.";
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
                            CanUnlock.Checked = (Mon_Stats[2] > 250);
                            MoveInfo.Text = "Withering tech. Requires 250+ INT to unlock.";
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
                            CanUnlock.Checked = (Mon_Stats[2] + Mon_Stats[4] > 350);
                            MoveInfo.Text = "Sharp tech. INT + SPD should total over 350 to unlock. Chains into Energy Shots when used 25x.";
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
                            CanUnlock.Checked = (Mon_Stats[1] > 600);
                            MoveInfo.Text = "Special tech. Requires 600+ POW to unlock.";
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
                            CanUnlock.Checked = (Mon_Stats[1] > 450);
                            MoveInfo.Text = "Heavy tech. Requires 450+ POW to unlock.";
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
                            CanUnlock.Checked = (Mon_Stats[2] + Mon_Stats[4] > 600);
                            MoveInfo.Text = "Sharp tech. INT + SPD should total over 600 to unlock. Requires Good (+20) Nature.";
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
                            CanUnlock.Checked = ((Mon_Stats[2] + Mon_Stats[4] > 600) && Mon_MoveUsed[13] >= 25);
                            MoveInfo.Text = "Sharp tech. Chains from 25 Energy Shot uses. INT + SPD should total over 600 to unlock.";
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
