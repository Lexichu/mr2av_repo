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
    public partial class MRDebug : Form
    {
        public bool bReadingMem, bReadOK;
        public ViewerWindow AVW;
        public int[] AddrINT = new int[4];
        public int[] DataINT = new int[4];
        public int[] DataMode = new int[4];

        public MRDebug()
        {
            InitializeComponent();
        }

        private void MRDebug_Load(object sender, EventArgs e)
        {
            dataType_1.SelectedIndex = 0;
            dataType_2.SelectedIndex = 0;
            dataType_3.SelectedIndex = 0;
            dataType_4.SelectedIndex = 0;
        }

        private void start_Debug_Click(object sender, EventArgs e)
        {
            if (!bReadingMem)
            {
                AddrINT[0] = int.Parse(textAddress_1.Text, System.Globalization.NumberStyles.HexNumber);
                AddrINT[1] = int.Parse(textAddress_2.Text, System.Globalization.NumberStyles.HexNumber);
                AddrINT[2] = int.Parse(textAddress_3.Text, System.Globalization.NumberStyles.HexNumber);
                AddrINT[3] = int.Parse(textAddress_4.Text, System.Globalization.NumberStyles.HexNumber);

                // Out of memory range protection

                if ((AddrINT[0] > 2097151 && dataType_1.SelectedIndex < 2) || (AddrINT[0] > 2097150 && dataType_1.SelectedIndex == 2) || (AddrINT[0] > 2097148 && dataType_1.SelectedIndex == 3))
                {
                    AddrINT[0] = -1;
                }
                if ((AddrINT[1] > 2097151 && dataType_2.SelectedIndex < 2) || (AddrINT[1] > 2097150 && dataType_2.SelectedIndex == 2) || (AddrINT[1] > 2097148 && dataType_2.SelectedIndex == 3))
                {
                    AddrINT[1] = -1;
                }
                if ((AddrINT[2] > 2097151 && dataType_3.SelectedIndex < 2) || (AddrINT[2] > 2097150 && dataType_3.SelectedIndex == 2) || (AddrINT[2] > 2097148 && dataType_3.SelectedIndex == 3))
                {
                    AddrINT[2] = -1;
                }
                if ((AddrINT[3] > 2097151 && dataType_4.SelectedIndex < 2) || (AddrINT[3] > 2097150 && dataType_4.SelectedIndex == 2) || (AddrINT[3] > 2097148 && dataType_4.SelectedIndex == 3))
                {
                    AddrINT[3] = -1;
                }
            }

            bReadingMem = !bReadingMem;

            textAddress_1.ReadOnly = bReadingMem;
            textAddress_2.ReadOnly = bReadingMem;
            textAddress_3.ReadOnly = bReadingMem;
            textAddress_4.ReadOnly = bReadingMem;
            dataType_1.Enabled = !bReadingMem;
            dataType_2.Enabled = !bReadingMem;
            dataType_3.Enabled = !bReadingMem;
            dataType_4.Enabled = !bReadingMem;
            DataMode[0] = dataType_1.SelectedIndex;
            DataMode[1] = dataType_2.SelectedIndex;
            DataMode[2] = dataType_3.SelectedIndex;
            DataMode[3] = dataType_4.SelectedIndex;

            if (bReadingMem)
                start_Debug.Text = "Stop Read";
            else
                start_Debug.Text = "Start Read";

            if (!bReadingMem || (bReadingMem && !bReadOK))
            {
                dataOut_1.Text = "[N/A]";
                dataOut_1H.Text = "[N/A]";
                dataOut_2.Text = "[N/A]";
                dataOut_2H.Text = "[N/A]";
                dataOut_3.Text = "[N/A]";
                dataOut_3H.Text = "[N/A]";
                dataOut_4.Text = "[N/A]";
                dataOut_4H.Text = "[N/A]";
                if (bReadOK)
                    bReadOK = false;
            }

            if (bReadingMem && !bReadOK)
            {
                start_Debug.Enabled = false;
            }
        }

        public void ProcessData()
        {
            if (!bReadingMem)
                return;

            bReadOK = true;
            start_Debug.Enabled = true;

            if(AddrINT[0] != -1)
            {
                if (dataType_1.SelectedIndex == 0)
                {
                    if (DataINT[0] == 0)
                    {
                        dataOut_1.Text = "FALSE";
                        dataOut_1H.Text = Convert.ToString(DataINT[0], 16);
                    }
                    else
                    {
                        dataOut_1.Text = "TRUE";
                        dataOut_1H.Text = Convert.ToString(DataINT[0], 16);
                    }
                }
                else
                {
                    dataOut_1.Text = DataINT[0].ToString();
                    dataOut_1H.Text = Convert.ToString(DataINT[0], 16);
                }
            }
            else
            {
                dataOut_1.Text = "[N/A]";
                dataOut_1H.Text = "[N/A]";
            }

            if (AddrINT[1] != -1)
            {
                if (dataType_2.SelectedIndex == 0)
                {
                    if (DataINT[1] == 0)
                    {
                        dataOut_2.Text = "FALSE";
                        dataOut_2H.Text = "00";
                    }
                    else
                    {
                        dataOut_2.Text = "TRUE";
                        dataOut_2H.Text = Convert.ToString(DataINT[1], 16);
                    }
                }
                else
                {
                    dataOut_2.Text = DataINT[1].ToString();
                    dataOut_2H.Text = Convert.ToString(DataINT[1], 16);
                }
            }
            else
            {
                dataOut_2.Text = "[N/A]";
                dataOut_2H.Text = "[N/A]";
            }

            if (AddrINT[2] != -1)
            {
                if (dataType_3.SelectedIndex == 0)
                {
                    if (DataINT[2] == 0)
                    {
                        dataOut_3.Text = "FALSE";
                        dataOut_3H.Text = "00";
                    }
                    else
                    {
                        dataOut_3.Text = "TRUE";
                        dataOut_3H.Text = Convert.ToString(DataINT[2], 16);
                    }
                }
                else
                {
                    dataOut_3.Text = DataINT[2].ToString();
                    dataOut_3H.Text = Convert.ToString(DataINT[2], 16);
                }
            }
            else
            {
                dataOut_3.Text = "[N/A]";
                dataOut_3H.Text = "[N/A]";
            }

            if (AddrINT[3] != -1)
            {
                if (dataType_4.SelectedIndex == 0)
                {
                    if (DataINT[3] == 0)
                    {
                        dataOut_4.Text = "FALSE";
                        dataOut_4H.Text = "00";
                    }
                    else
                    {
                        dataOut_4.Text = "TRUE";
                        dataOut_4H.Text = Convert.ToString(DataINT[3], 16);
                    }
                }
                else
                {
                    dataOut_4.Text = DataINT[3].ToString();
                    dataOut_4H.Text = Convert.ToString(DataINT[3], 16);
                }
            }
            else
            {
                dataOut_4.Text = "[N/A]";
                dataOut_4H.Text = "[N/A]";
            }
        }
    }
}
