namespace MR2AdvancedViewer.Forms
{
    partial class ItemLister
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ItemLister));
            this.ButtonPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.PurchasePrice = new System.Windows.Forms.Label();
            this.SalePrice = new System.Windows.Forms.Label();
            this.ItemTypeDesc = new System.Windows.Forms.Label();
            this.ItemDescription = new System.Windows.Forms.TextBox();
            this.ItemEffect = new System.Windows.Forms.TextBox();
            this.itemEff_Label = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // ButtonPanel
            // 
            this.ButtonPanel.AutoScroll = true;
            this.ButtonPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.ButtonPanel.Location = new System.Drawing.Point(8, 8);
            this.ButtonPanel.Name = "ButtonPanel";
            this.ButtonPanel.Size = new System.Drawing.Size(150, 146);
            this.ButtonPanel.TabIndex = 0;
            this.ButtonPanel.WrapContents = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(164, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Purchase Value:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(164, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Sale Value:";
            // 
            // PurchasePrice
            // 
            this.PurchasePrice.AutoSize = true;
            this.PurchasePrice.Font = new System.Drawing.Font("Consolas", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PurchasePrice.Location = new System.Drawing.Point(214, 22);
            this.PurchasePrice.Name = "PurchasePrice";
            this.PurchasePrice.Size = new System.Drawing.Size(70, 24);
            this.PurchasePrice.TabIndex = 3;
            this.PurchasePrice.Text = "-----";
            // 
            // SalePrice
            // 
            this.SalePrice.AutoSize = true;
            this.SalePrice.Font = new System.Drawing.Font("Consolas", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SalePrice.Location = new System.Drawing.Point(214, 61);
            this.SalePrice.Name = "SalePrice";
            this.SalePrice.Size = new System.Drawing.Size(70, 24);
            this.SalePrice.TabIndex = 4;
            this.SalePrice.Text = "-----";
            // 
            // ItemTypeDesc
            // 
            this.ItemTypeDesc.AutoSize = true;
            this.ItemTypeDesc.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ItemTypeDesc.Location = new System.Drawing.Point(7, 157);
            this.ItemTypeDesc.Name = "ItemTypeDesc";
            this.ItemTypeDesc.Size = new System.Drawing.Size(63, 16);
            this.ItemTypeDesc.TabIndex = 5;
            this.ItemTypeDesc.Text = "No Item";
            // 
            // ItemDescription
            // 
            this.ItemDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ItemDescription.Location = new System.Drawing.Point(8, 176);
            this.ItemDescription.Multiline = true;
            this.ItemDescription.Name = "ItemDescription";
            this.ItemDescription.ReadOnly = true;
            this.ItemDescription.Size = new System.Drawing.Size(284, 63);
            this.ItemDescription.TabIndex = 7;
            this.ItemDescription.Text = "No item selected.";
            // 
            // ItemEffect
            // 
            this.ItemEffect.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ItemEffect.Location = new System.Drawing.Point(8, 261);
            this.ItemEffect.Multiline = true;
            this.ItemEffect.Name = "ItemEffect";
            this.ItemEffect.ReadOnly = true;
            this.ItemEffect.Size = new System.Drawing.Size(284, 63);
            this.ItemEffect.TabIndex = 8;
            this.ItemEffect.Text = "No item selected.";
            // 
            // itemEff_Label
            // 
            this.itemEff_Label.AutoSize = true;
            this.itemEff_Label.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.itemEff_Label.Location = new System.Drawing.Point(7, 242);
            this.itemEff_Label.Name = "itemEff_Label";
            this.itemEff_Label.Size = new System.Drawing.Size(87, 16);
            this.itemEff_Label.TabIndex = 9;
            this.itemEff_Label.Text = "Item Effect:";
            // 
            // ItemLister
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(304, 361);
            this.Controls.Add(this.itemEff_Label);
            this.Controls.Add(this.ItemEffect);
            this.Controls.Add(this.ItemDescription);
            this.Controls.Add(this.ItemTypeDesc);
            this.Controls.Add(this.SalePrice);
            this.Controls.Add(this.PurchasePrice);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ButtonPanel);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ItemLister";
            this.Text = "Monster Rancher 2: Item Lister";
            this.Load += new System.EventHandler(this.ItemLister_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel ButtonPanel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label PurchasePrice;
        private System.Windows.Forms.Label SalePrice;
        private System.Windows.Forms.Label ItemTypeDesc;
        private System.Windows.Forms.TextBox ItemDescription;
        private System.Windows.Forms.TextBox ItemEffect;
        private System.Windows.Forms.Label itemEff_Label;
    }
}