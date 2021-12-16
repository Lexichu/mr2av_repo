namespace MR2AdvancedViewer
{
    partial class MonLifeIndexWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MonLifeIndexWindow));
            this.LIFatigueBox = new System.Windows.Forms.TextBox();
            this.LIStressBox = new System.Windows.Forms.TextBox();
            this.LILifeIndexBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.LITimer = new System.Windows.Forms.Timer(this.components);
            this.LILifeLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // LIFatigueBox
            // 
            this.LIFatigueBox.Font = new System.Drawing.Font("Liberation Sans", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LIFatigueBox.Location = new System.Drawing.Point(0, 30);
            this.LIFatigueBox.Name = "LIFatigueBox";
            this.LIFatigueBox.ReadOnly = true;
            this.LIFatigueBox.Size = new System.Drawing.Size(150, 63);
            this.LIFatigueBox.TabIndex = 0;
            this.LIFatigueBox.TabStop = false;
            this.LIFatigueBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // LIStressBox
            // 
            this.LIStressBox.Font = new System.Drawing.Font("Liberation Sans", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LIStressBox.Location = new System.Drawing.Point(154, 30);
            this.LIStressBox.Name = "LIStressBox";
            this.LIStressBox.ReadOnly = true;
            this.LIStressBox.Size = new System.Drawing.Size(150, 63);
            this.LIStressBox.TabIndex = 1;
            this.LIStressBox.TabStop = false;
            this.LIStressBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // LILifeIndexBox
            // 
            this.LILifeIndexBox.Font = new System.Drawing.Font("Liberation Sans", 32.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LILifeIndexBox.Location = new System.Drawing.Point(0, 122);
            this.LILifeIndexBox.Name = "LILifeIndexBox";
            this.LILifeIndexBox.ReadOnly = true;
            this.LILifeIndexBox.Size = new System.Drawing.Size(304, 57);
            this.LILifeIndexBox.TabIndex = 2;
            this.LILifeIndexBox.TabStop = false;
            this.LILifeIndexBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Liberation Sans", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(36, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 23);
            this.label1.TabIndex = 3;
            this.label1.Text = "Fatigue";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Liberation Sans", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(194, 4);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 23);
            this.label2.TabIndex = 4;
            this.label2.Text = "Stress";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Liberation Sans", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(104, 96);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(101, 23);
            this.label3.TabIndex = 5;
            this.label3.Text = "Life Index";
            // 
            // LITimer
            // 
            this.LITimer.Tick += new System.EventHandler(this.LITimer_Tick);
            // 
            // LILifeLabel
            // 
            this.LILifeLabel.AutoSize = true;
            this.LILifeLabel.Font = new System.Drawing.Font("Liberation Sans", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LILifeLabel.Location = new System.Drawing.Point(37, 182);
            this.LILifeLabel.Name = "LILifeLabel";
            this.LILifeLabel.Size = new System.Drawing.Size(151, 17);
            this.LILifeLabel.TabIndex = 6;
            this.LILifeLabel.Text = "Lifespan Remaining:";
            // 
            // MonLifeIndexWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(304, 202);
            this.Controls.Add(this.LILifeLabel);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.LILifeIndexBox);
            this.Controls.Add(this.LIStressBox);
            this.Controls.Add(this.LIFatigueBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MonLifeIndexWindow";
            this.Text = "Life Index Display";
            this.Load += new System.EventHandler(this.MonLifeIndexWindow_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox LIFatigueBox;
        private System.Windows.Forms.TextBox LIStressBox;
        private System.Windows.Forms.TextBox LILifeIndexBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Timer LITimer;
        private System.Windows.Forms.Label LILifeLabel;
    }
}