namespace Client
{
    partial class InputUI
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer componentsInput = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (componentsInput != null))
            {
                componentsInput.Dispose();
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InputUI));
            this.txtAppID = new System.Windows.Forms.TextBox();
            this.lblAppID = new System.Windows.Forms.Label();
            this.lblMadeBy = new System.Windows.Forms.Label();
            this.lblVersion = new System.Windows.Forms.Label();
            this.btnRequestData = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtAppID
            // 
            resources.ApplyResources(this.txtAppID, "txtAppID");
            this.txtAppID.Name = "txtAppID";
            // 
            // lblAppID
            // 
            resources.ApplyResources(this.lblAppID, "lblAppID");
            this.lblAppID.Name = "lblAppID";
            // 
            // lblMadeBy
            // 
            resources.ApplyResources(this.lblMadeBy, "lblMadeBy");
            this.lblMadeBy.Name = "lblMadeBy";
            // 
            // lblVersion
            // 
            resources.ApplyResources(this.lblVersion, "lblVersion");
            this.lblVersion.Name = "lblVersion";
            // 
            // btnRequestData
            // 
            resources.ApplyResources(this.btnRequestData, "btnRequestData");
            this.btnRequestData.Name = "btnRequestData";
            this.btnRequestData.UseVisualStyleBackColor = true;
            this.btnRequestData.Click += new System.EventHandler(this.BtnRequestData_Click);
            // 
            // btnExit
            // 
            resources.ApplyResources(this.btnExit, "btnExit");
            this.btnExit.Name = "btnExit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.BtnExit_Click);
            // 
            // InputUI
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnRequestData);
            this.Controls.Add(this.lblVersion);
            this.Controls.Add(this.lblMadeBy);
            this.Controls.Add(this.lblAppID);
            this.Controls.Add(this.txtAppID);
            this.Name = "InputUI";
            this.Load += new System.EventHandler(this.InputUI_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtAppID;
        private System.Windows.Forms.Label lblAppID;
        private System.Windows.Forms.Label lblMadeBy;
        private System.Windows.Forms.Label lblVersion;
        private System.Windows.Forms.Button btnRequestData;
        private System.Windows.Forms.Button btnExit;
    }
}