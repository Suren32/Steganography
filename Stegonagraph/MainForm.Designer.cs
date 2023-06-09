namespace Stegonagraph
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.pbUnhide = new System.Windows.Forms.PictureBox();
            this.pbHide = new System.Windows.Forms.PictureBox();
            this.button1 = new System.Windows.Forms.Button();
            this.StegonagraphyKeyLabel = new System.Windows.Forms.Label();
            this.textBoxStegKey = new System.Windows.Forms.TextBox();
            this.hideLabel = new System.Windows.Forms.Label();
            this.unhideLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pbUnhide)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbHide)).BeginInit();
            this.SuspendLayout();
            // 
            // pbUnhide
            // 
            this.pbUnhide.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pbUnhide.BackgroundImage")));
            this.pbUnhide.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pbUnhide.Location = new System.Drawing.Point(188, 136);
            this.pbUnhide.Name = "pbUnhide";
            this.pbUnhide.Size = new System.Drawing.Size(104, 120);
            this.pbUnhide.TabIndex = 18;
            this.pbUnhide.TabStop = false;
            this.pbUnhide.Click += new System.EventHandler(this.PbUnhide_Click);
            // 
            // pbHide
            // 
            this.pbHide.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pbHide.BackgroundImage")));
            this.pbHide.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pbHide.Location = new System.Drawing.Point(81, 135);
            this.pbHide.Name = "pbHide";
            this.pbHide.Size = new System.Drawing.Size(101, 120);
            this.pbHide.TabIndex = 19;
            this.pbHide.TabStop = false;
            this.pbHide.Click += new System.EventHandler(this.PbHide_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button1.Location = new System.Drawing.Point(12, 22);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(364, 40);
            this.button1.TabIndex = 53;
            this.button1.Text = "About Application";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // StegonagraphyKeyLabel
            // 
            this.StegonagraphyKeyLabel.AutoSize = true;
            this.StegonagraphyKeyLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.StegonagraphyKeyLabel.Location = new System.Drawing.Point(12, 85);
            this.StegonagraphyKeyLabel.Name = "StegonagraphyKeyLabel";
            this.StegonagraphyKeyLabel.Size = new System.Drawing.Size(148, 20);
            this.StegonagraphyKeyLabel.TabIndex = 59;
            this.StegonagraphyKeyLabel.Text = "Stegonagraphy Key";
            // 
            // textBoxStegKey
            // 
            this.textBoxStegKey.BackColor = System.Drawing.Color.WhiteSmoke;
            this.textBoxStegKey.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxStegKey.Location = new System.Drawing.Point(166, 85);
            this.textBoxStegKey.Name = "textBoxStegKey";
            this.textBoxStegKey.Size = new System.Drawing.Size(210, 26);
            this.textBoxStegKey.TabIndex = 58;
            this.textBoxStegKey.TextChanged += new System.EventHandler(this.textBoxStegKey_TextChanged);
            // 
            // hideLabel
            // 
            this.hideLabel.AutoSize = true;
            this.hideLabel.BackColor = System.Drawing.Color.Transparent;
            this.hideLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.hideLabel.Location = new System.Drawing.Point(103, 259);
            this.hideLabel.Name = "hideLabel";
            this.hideLabel.Size = new System.Drawing.Size(57, 26);
            this.hideLabel.TabIndex = 60;
            this.hideLabel.Text = "Hide";
            // 
            // unhideLabel
            // 
            this.unhideLabel.AutoSize = true;
            this.unhideLabel.BackColor = System.Drawing.Color.Transparent;
            this.unhideLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.unhideLabel.Location = new System.Drawing.Point(201, 259);
            this.unhideLabel.Name = "unhideLabel";
            this.unhideLabel.Size = new System.Drawing.Size(81, 26);
            this.unhideLabel.TabIndex = 61;
            this.unhideLabel.Text = "Unhide";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gainsboro;
            this.ClientSize = new System.Drawing.Size(394, 299);
            this.Controls.Add(this.unhideLabel);
            this.Controls.Add(this.hideLabel);
            this.Controls.Add(this.StegonagraphyKeyLabel);
            this.Controls.Add(this.textBoxStegKey);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.pbHide);
            this.Controls.Add(this.pbUnhide);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Steganography";
            ((System.ComponentModel.ISupportInitialize)(this.pbUnhide)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbHide)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.PictureBox pbUnhide;
        private System.Windows.Forms.PictureBox pbHide;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label StegonagraphyKeyLabel;
        private System.Windows.Forms.TextBox textBoxStegKey;
        private System.Windows.Forms.Label hideLabel;
        private System.Windows.Forms.Label unhideLabel;
    }
}