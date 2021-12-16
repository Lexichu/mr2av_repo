namespace MR2AdvancedViewer
{
    partial class MR2ControllerView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MR2ControllerView));
            this.Label_Shoulder = new System.Windows.Forms.Label();
            this.Label_Face = new System.Windows.Forms.Label();
            this.Label_Misc = new System.Windows.Forms.Label();
            this.Label_DPad = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Label_Shoulder
            // 
            this.Label_Shoulder.AutoSize = true;
            this.Label_Shoulder.Font = new System.Drawing.Font("DejaVu Sans Mono", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label_Shoulder.ForeColor = System.Drawing.Color.Lime;
            this.Label_Shoulder.Location = new System.Drawing.Point(12, 9);
            this.Label_Shoulder.Name = "Label_Shoulder";
            this.Label_Shoulder.Size = new System.Drawing.Size(78, 31);
            this.Label_Shoulder.TabIndex = 0;
            this.Label_Shoulder.Text = "    ";
            // 
            // Label_Face
            // 
            this.Label_Face.AutoSize = true;
            this.Label_Face.Font = new System.Drawing.Font("DejaVu Sans Mono", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label_Face.ForeColor = System.Drawing.Color.Lime;
            this.Label_Face.Location = new System.Drawing.Point(12, 40);
            this.Label_Face.Name = "Label_Face";
            this.Label_Face.Size = new System.Drawing.Size(78, 31);
            this.Label_Face.TabIndex = 1;
            this.Label_Face.Text = "    ";
            // 
            // Label_Misc
            // 
            this.Label_Misc.AutoSize = true;
            this.Label_Misc.Font = new System.Drawing.Font("DejaVu Sans Mono", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label_Misc.ForeColor = System.Drawing.Color.Lime;
            this.Label_Misc.Location = new System.Drawing.Point(12, 71);
            this.Label_Misc.Name = "Label_Misc";
            this.Label_Misc.Size = new System.Drawing.Size(78, 31);
            this.Label_Misc.TabIndex = 2;
            this.Label_Misc.Text = "    ";
            // 
            // Label_DPad
            // 
            this.Label_DPad.AutoSize = true;
            this.Label_DPad.Font = new System.Drawing.Font("DejaVu Sans Mono", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label_DPad.ForeColor = System.Drawing.Color.Lime;
            this.Label_DPad.Location = new System.Drawing.Point(12, 102);
            this.Label_DPad.Name = "Label_DPad";
            this.Label_DPad.Size = new System.Drawing.Size(78, 31);
            this.Label_DPad.TabIndex = 3;
            this.Label_DPad.Text = "    ";
            this.Label_DPad.Click += new System.EventHandler(this.Label_DPad_Click);
            // 
            // MR2ControllerView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Magenta;
            this.ClientSize = new System.Drawing.Size(240, 218);
            this.Controls.Add(this.Label_DPad);
            this.Controls.Add(this.Label_Misc);
            this.Controls.Add(this.Label_Face);
            this.Controls.Add(this.Label_Shoulder);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MR2ControllerView";
            this.Text = "Controller View";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label Label_Shoulder;
        private System.Windows.Forms.Label Label_Face;
        private System.Windows.Forms.Label Label_Misc;
        private System.Windows.Forms.Label Label_DPad;
    }
}