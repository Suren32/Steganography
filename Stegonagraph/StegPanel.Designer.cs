namespace Stegonagraph
{
    partial class StegPanel
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StegPanel));
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.Name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Size = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.path = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnRemove = new System.Windows.Forms.Button();
            this.labelContainer = new System.Windows.Forms.Label();
            this.labelHide = new System.Windows.Forms.Label();
            this.SelectItemBtn = new System.Windows.Forms.Button();
            this.ContainerBtn = new System.Windows.Forms.Button();
            this.pbPicture = new System.Windows.Forms.PictureBox();
            this.btnStart = new System.Windows.Forms.Button();
            this.containerGridView = new System.Windows.Forms.DataGridView();
            this.cName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cSize = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cPath = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnContainerRmove = new System.Windows.Forms.Button();
            this.checkBox = new System.Windows.Forms.CheckBox();
            this.encryptTextBox = new System.Windows.Forms.TextBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPicture)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.containerGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView
            // 
            this.dataGridView.AllowUserToAddRows = false;
            this.dataGridView.AllowUserToDeleteRows = false;
            this.dataGridView.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Name,
            this.Size,
            this.path});
            this.dataGridView.GridColor = System.Drawing.Color.WhiteSmoke;
            this.dataGridView.Location = new System.Drawing.Point(420, 55);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.ReadOnly = true;
            this.dataGridView.Size = new System.Drawing.Size(400, 400);
            this.dataGridView.TabIndex = 35;
            // 
            // Name
            // 
            this.Name.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Name.HeaderText = "Name";
            this.Name.Name = "Name";
            this.Name.ReadOnly = true;
            this.Name.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // Size
            // 
            this.Size.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.Size.HeaderText = "Size(Byte)";
            this.Size.Name = "Size";
            this.Size.ReadOnly = true;
            this.Size.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Size.Width = 79;
            // 
            // path
            // 
            this.path.HeaderText = "path";
            this.path.Name = "path";
            this.path.ReadOnly = true;
            this.path.Visible = false;
            // 
            // btnRemove
            // 
            this.btnRemove.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnRemove.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.btnRemove.Location = new System.Drawing.Point(420, 461);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(187, 34);
            this.btnRemove.TabIndex = 34;
            this.btnRemove.Text = "Remove Selected Item";
            this.btnRemove.UseVisualStyleBackColor = false;
            this.btnRemove.Click += new System.EventHandler(this.BtnDataRemove_Click);
            // 
            // labelContainer
            // 
            this.labelContainer.AutoSize = true;
            this.labelContainer.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.labelContainer.Location = new System.Drawing.Point(11, 498);
            this.labelContainer.Name = "labelContainer";
            this.labelContainer.Size = new System.Drawing.Size(101, 20);
            this.labelContainer.TabIndex = 33;
            this.labelContainer.Text = "Can Hide: 0b";
            // 
            // labelHide
            // 
            this.labelHide.AutoSize = true;
            this.labelHide.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.labelHide.Location = new System.Drawing.Point(416, 498);
            this.labelHide.Name = "labelHide";
            this.labelHide.Size = new System.Drawing.Size(95, 20);
            this.labelHide.TabIndex = 32;
            this.labelHide.Text = "File Size: 0b";
            // 
            // SelectItemBtn
            // 
            this.SelectItemBtn.BackColor = System.Drawing.Color.WhiteSmoke;
            this.SelectItemBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.SelectItemBtn.Location = new System.Drawing.Point(420, 15);
            this.SelectItemBtn.Name = "SelectItemBtn";
            this.SelectItemBtn.Size = new System.Drawing.Size(400, 34);
            this.SelectItemBtn.TabIndex = 31;
            this.SelectItemBtn.Text = "Select Secret Files";
            this.SelectItemBtn.UseVisualStyleBackColor = false;
            this.SelectItemBtn.Click += new System.EventHandler(this.SelectDataBtn_Click);
            // 
            // ContainerBtn
            // 
            this.ContainerBtn.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ContainerBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.ContainerBtn.Location = new System.Drawing.Point(15, 15);
            this.ContainerBtn.Name = "ContainerBtn";
            this.ContainerBtn.Size = new System.Drawing.Size(399, 34);
            this.ContainerBtn.TabIndex = 30;
            this.ContainerBtn.Text = "Select Containers";
            this.ContainerBtn.UseVisualStyleBackColor = false;
            this.ContainerBtn.Click += new System.EventHandler(this.SelectContainerBtn_Click);
            // 
            // pbPicture
            // 
            this.pbPicture.Location = new System.Drawing.Point(380, 461);
            this.pbPicture.Name = "pbPicture";
            this.pbPicture.Size = new System.Drawing.Size(35, 35);
            this.pbPicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbPicture.TabIndex = 28;
            this.pbPicture.TabStop = false;
            // 
            // btnStart
            // 
            this.btnStart.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnStart.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.btnStart.Location = new System.Drawing.Point(719, 461);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(91, 45);
            this.btnStart.TabIndex = 40;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = false;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // containerGridView
            // 
            this.containerGridView.AllowUserToAddRows = false;
            this.containerGridView.AllowUserToDeleteRows = false;
            this.containerGridView.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.containerGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.containerGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.cName,
            this.cSize,
            this.cPath});
            this.containerGridView.GridColor = System.Drawing.Color.WhiteSmoke;
            this.containerGridView.Location = new System.Drawing.Point(15, 55);
            this.containerGridView.Name = "containerGridView";
            this.containerGridView.Size = new System.Drawing.Size(400, 400);
            this.containerGridView.TabIndex = 41;
            // 
            // cName
            // 
            this.cName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.cName.HeaderText = "Name";
            this.cName.Name = "cName";
            this.cName.ReadOnly = true;
            // 
            // cSize
            // 
            this.cSize.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.cSize.HeaderText = "Size(Byte)";
            this.cSize.Name = "cSize";
            this.cSize.Width = 79;
            // 
            // cPath
            // 
            this.cPath.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.cPath.HeaderText = "Path";
            this.cPath.Name = "cPath";
            this.cPath.Visible = false;
            // 
            // btnContainerRmove
            // 
            this.btnContainerRmove.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnContainerRmove.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.btnContainerRmove.Location = new System.Drawing.Point(15, 461);
            this.btnContainerRmove.Name = "btnContainerRmove";
            this.btnContainerRmove.Size = new System.Drawing.Size(187, 34);
            this.btnContainerRmove.TabIndex = 43;
            this.btnContainerRmove.Text = "Remove Selected Item";
            this.btnContainerRmove.UseVisualStyleBackColor = false;
            this.btnContainerRmove.Click += new System.EventHandler(this.BtnContainerRmove_Click);
            // 
            // checkBox
            // 
            this.checkBox.AutoSize = true;
            this.checkBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.checkBox.Location = new System.Drawing.Point(15, 540);
            this.checkBox.Name = "checkBox";
            this.checkBox.Size = new System.Drawing.Size(103, 24);
            this.checkBox.TabIndex = 46;
            this.checkBox.Text = "Encryption";
            this.checkBox.UseVisualStyleBackColor = true;
            this.checkBox.CheckedChanged += new System.EventHandler(this.CheckBox_CheckedChanged);
            // 
            // encryptTextBox
            // 
            this.encryptTextBox.BackColor = System.Drawing.Color.WhiteSmoke;
            this.encryptTextBox.Enabled = false;
            this.encryptTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.encryptTextBox.Location = new System.Drawing.Point(12, 570);
            this.encryptTextBox.Name = "encryptTextBox";
            this.encryptTextBox.Size = new System.Drawing.Size(203, 26);
            this.encryptTextBox.TabIndex = 47;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Multiselect = true;
            this.openFileDialog1.Title = "Select Items!";
            // 
            // folderBrowserDialog1
            // 
            this.folderBrowserDialog1.Description = "Select Folder!";
            // 
            // StegPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gainsboro;
            this.ClientSize = new System.Drawing.Size(826, 618);
            this.Controls.Add(this.encryptTextBox);
            this.Controls.Add(this.checkBox);
            this.Controls.Add(this.btnContainerRmove);
            this.Controls.Add(this.containerGridView);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.dataGridView);
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.labelContainer);
            this.Controls.Add(this.labelHide);
            this.Controls.Add(this.SelectItemBtn);
            this.Controls.Add(this.ContainerBtn);
            this.Controls.Add(this.pbPicture);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Steganography";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.StegPanel_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPicture)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.containerGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn Name;
        private System.Windows.Forms.DataGridViewTextBoxColumn Size;
        private System.Windows.Forms.DataGridViewTextBoxColumn path;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.Label labelContainer;
        private System.Windows.Forms.Label labelHide;
        private System.Windows.Forms.Button SelectItemBtn;
        private System.Windows.Forms.Button ContainerBtn;
        private System.Windows.Forms.PictureBox pbPicture;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.DataGridView containerGridView;
        private System.Windows.Forms.Button btnContainerRmove;
        private System.Windows.Forms.CheckBox checkBox;
        private System.Windows.Forms.TextBox encryptTextBox;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.DataGridViewTextBoxColumn cName;
        private System.Windows.Forms.DataGridViewTextBoxColumn cSize;
        private System.Windows.Forms.DataGridViewTextBoxColumn cPath;
    }
}