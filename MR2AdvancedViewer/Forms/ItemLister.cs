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
    public partial class ItemLister : Form
    {
        public Button[] ItemButtons = new Button[20];
        public int[] itemIDs = new int[20];
        public int selectedButton;
        public ViewerWindow AVW;
        public string[] rawdata_item = null;

        public ItemLister()
        {
            InitializeComponent();
        }

        public void ParseItemList(int arrayslot)
        {
            if (rawdata_item.Length >= arrayslot)
            {
                string[] scratchdata = rawdata_item[arrayslot].Split('|');
                ItemTypeDesc.Text = scratchdata[1];
                ItemDescription.Text = scratchdata[2];
                ItemEffect.Text = scratchdata[3];
                PurchasePrice.Text = scratchdata[5];
                SalePrice.Text = scratchdata[4];
            }
        }

        public string ParseButtonName(int ItemID)
        {
            if (rawdata_item.Length >= ItemID + 1)
            {
                string[] scratchdata_ex = rawdata_item[ItemID].Split('|');
                return scratchdata_ex[0];
            }
            return "-----";
        }


        private void ItemLister_Load(object sender, EventArgs e)
        {
            Button button;
            for (int i = 0; i < 20; i++)
            {
                button = new Button
                {
                    Width = 120
                };
                ButtonPanel.Controls.Add(button);
                ItemButtons[i] = button;
                ItemButtons[i].Tag = "BTN_ID_" + i;
            }
            if (rawdata_item == null)
            {
                rawdata_item = AVW.MR2ReadDataFile(@"data\en_van\mr2_item_list.txt");
            }
        }

        public void ItemListUpdate()
        {
            if (AVW != null)
            {
                if(rawdata_item != null)
                {
                    for (int i = 0; i < 20; i++)
                    {
                        if (ItemButtons[i].Text == "")
                            ItemButtons[i].Text = "-----";
                        else
                            ItemButtons[i].Text = ParseButtonName(itemIDs[i]);
                        ItemButtons[i].Click += new System.EventHandler(ClickButton);
                    }
                }
            }
        }

        public void ClickButton(Object sender, System.EventArgs e)
        {
            Button btn = (Button)sender;
            string Str_Numeric = string.Empty;
            foreach (var c in (string)btn.Tag)
            {
                if ((c >= '0' && c <= '9'))
                {
                    Str_Numeric = string.Concat(Str_Numeric, c.ToString());
                }
            }
            selectedButton = Int32.Parse(Str_Numeric);

            if (itemIDs[selectedButton] >= 178)
            {
                SalePrice.Text = "-----";
                PurchasePrice.Text = "-----";
                ItemTypeDesc.Text = "No Item";
                ItemDescription.Text = "No item selected.";
                ItemEffect.Text = "This is a blank slot.";
            }
            else
            {
                ParseItemList(itemIDs[selectedButton]);
            }
        }
    }
}
