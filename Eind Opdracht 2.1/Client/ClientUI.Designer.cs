namespace Client
{
    partial class ClientUI
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer componentsClient = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (componentsClient != null))
            {
                componentsClient.Dispose();
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ClientUI));
            this.lblName = new System.Windows.Forms.Label();
            this.lblReleaseDate = new System.Windows.Forms.Label();
            this.lblFreeToPlay = new System.Windows.Forms.Label();
            this.lblDeveloper = new System.Windows.Forms.RichTextBox();
            this.pictureHeader = new System.Windows.Forms.PictureBox();
            this.lblPublisher = new System.Windows.Forms.RichTextBox();
            this.lblNotes = new System.Windows.Forms.Label();
            this.btnBack = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureHeader)).BeginInit();
            this.SuspendLayout();
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(12, 9);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(111, 18);
            this.lblName.TabIndex = 1;
            this.lblName.Text = "Name of game";
            // 
            // lblReleaseDate
            // 
            this.lblReleaseDate.AutoSize = true;
            this.lblReleaseDate.Location = new System.Drawing.Point(12, 42);
            this.lblReleaseDate.Name = "lblReleaseDate";
            this.lblReleaseDate.Size = new System.Drawing.Size(178, 18);
            this.lblReleaseDate.TabIndex = 2;
            this.lblReleaseDate.Text = "Release date (years old)";
            // 
            // lblFreeToPlay
            // 
            this.lblFreeToPlay.AutoSize = true;
            this.lblFreeToPlay.Location = new System.Drawing.Point(12, 80);
            this.lblFreeToPlay.Name = "lblFreeToPlay";
            this.lblFreeToPlay.Size = new System.Drawing.Size(78, 18);
            this.lblFreeToPlay.TabIndex = 3;
            this.lblFreeToPlay.Text = "free or not";
            // 
            // lblDeveloper
            // 
            this.lblDeveloper.Location = new System.Drawing.Point(256, 9);
            this.lblDeveloper.Name = "lblDeveloper";
            this.lblDeveloper.Size = new System.Drawing.Size(190, 51);
            this.lblDeveloper.TabIndex = 4;
            this.lblDeveloper.Text = "Developer(s):";
            // 
            // pictureHeader
            // 
            this.pictureHeader.Location = new System.Drawing.Point(452, 9);
            this.pictureHeader.Name = "pictureHeader";
            this.pictureHeader.Size = new System.Drawing.Size(597, 304);
            this.pictureHeader.TabIndex = 6;
            this.pictureHeader.TabStop = false;
            // 
            // lblPublisher
            // 
            this.lblPublisher.Location = new System.Drawing.Point(256, 91);
            this.lblPublisher.Name = "lblPublisher";
            this.lblPublisher.Size = new System.Drawing.Size(190, 51);
            this.lblPublisher.TabIndex = 7;
            this.lblPublisher.Text = "Publisher(s):";
            // 
            // lblNotes
            // 
            this.lblNotes.AutoSize = true;
            this.lblNotes.Location = new System.Drawing.Point(12, 233);
            this.lblNotes.MaximumSize = new System.Drawing.Size(450, 0);
            this.lblNotes.Name = "lblNotes";
            this.lblNotes.Size = new System.Drawing.Size(49, 18);
            this.lblNotes.TabIndex = 8;
            this.lblNotes.Text = "About";
            // 
            // btnBack
            // 
            this.btnBack.Location = new System.Drawing.Point(12, 597);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(75, 23);
            this.btnBack.TabIndex = 9;
            this.btnBack.Text = "Back";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.BtnBack_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(962, 599);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(87, 18);
            this.label2.TabIndex = 10;
            this.label2.Text = "Version 1.3";
            // 
            // ClientUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1264, 681);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.lblNotes);
            this.Controls.Add(this.lblPublisher);
            this.Controls.Add(this.pictureHeader);
            this.Controls.Add(this.lblDeveloper);
            this.Controls.Add(this.lblFreeToPlay);
            this.Controls.Add(this.lblReleaseDate);
            this.Controls.Add(this.lblName);
            this.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "ClientUI";
            this.Text = "Steam Game Data Requester - Data";
            this.Load += new System.EventHandler(this.ClientUI_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureHeader)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblReleaseDate;
        private System.Windows.Forms.Label lblFreeToPlay;
        private System.Windows.Forms.RichTextBox lblDeveloper;
        private System.Windows.Forms.PictureBox pictureHeader;
        private System.Windows.Forms.RichTextBox lblPublisher;
        private System.Windows.Forms.Label lblNotes;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Label label2;
    }
}