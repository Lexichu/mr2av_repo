namespace MR2AdvancedViewer.Forms
{
    partial class PlayerInfo
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
            this.components = new System.ComponentModel.Container();
            this.NameLabel = new System.Windows.Forms.Label();
            this.PlayerName = new System.Windows.Forms.TextBox();
            this.EditSaveBtn = new System.Windows.Forms.Button();
            this.CancelBtn = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // NameLabel
            // 
            this.NameLabel.AutoSize = true;
            this.NameLabel.Location = new System.Drawing.Point(12, 9);
            this.NameLabel.Name = "NameLabel";
            this.NameLabel.Size = new System.Drawing.Size(38, 13);
            this.NameLabel.TabIndex = 0;
            this.NameLabel.Text = "Name:";
            // 
            // PlayerName
            // 
            this.PlayerName.Location = new System.Drawing.Point(51, 6);
            this.PlayerName.MaxLength = 12;
            this.PlayerName.Name = "PlayerName";
            this.PlayerName.ReadOnly = true;
            this.PlayerName.Size = new System.Drawing.Size(100, 20);
            this.PlayerName.TabIndex = 1;
            this.PlayerName.TextChanged += new System.EventHandler(this.PlayerName_TextChanged);
            // 
            // EditSaveBtn
            // 
            this.EditSaveBtn.Location = new System.Drawing.Point(158, 4);
            this.EditSaveBtn.Name = "EditSaveBtn";
            this.EditSaveBtn.Size = new System.Drawing.Size(48, 23);
            this.EditSaveBtn.TabIndex = 2;
            this.EditSaveBtn.Text = "Edit";
            this.EditSaveBtn.UseVisualStyleBackColor = true;
            this.EditSaveBtn.Click += new System.EventHandler(this.EditSaveBtn_Click);
            // 
            // CancelBtn
            // 
            this.CancelBtn.Enabled = false;
            this.CancelBtn.Location = new System.Drawing.Point(208, 4);
            this.CancelBtn.Name = "CancelBtn";
            this.CancelBtn.Size = new System.Drawing.Size(56, 23);
            this.CancelBtn.TabIndex = 3;
            this.CancelBtn.Text = "Cancel";
            this.CancelBtn.UseVisualStyleBackColor = true;
            this.CancelBtn.Visible = false;
            this.CancelBtn.Click += new System.EventHandler(this.CancelBtn_Click);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 125;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // PlayerInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(304, 41);
            this.Controls.Add(this.CancelBtn);
            this.Controls.Add(this.EditSaveBtn);
            this.Controls.Add(this.PlayerName);
            this.Controls.Add(this.NameLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "PlayerInfo";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Player Info:";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label NameLabel;
        private System.Windows.Forms.TextBox PlayerName;
        private System.Windows.Forms.Button EditSaveBtn;
        private System.Windows.Forms.Button CancelBtn;
        private System.Windows.Forms.Timer timer1;
    }
}