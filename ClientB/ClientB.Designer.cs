namespace ClientB
{
    partial class ClientB
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
            this.listBoxApps = new System.Windows.Forms.ListBox();
            this.listBoxContainers = new System.Windows.Forms.ListBox();
            this.buttonOpen = new System.Windows.Forms.Button();
            this.buttonClose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listBoxApps
            // 
            this.listBoxApps.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.listBoxApps.FormattingEnabled = true;
            this.listBoxApps.Location = new System.Drawing.Point(12, 19);
            this.listBoxApps.Name = "listBoxApps";
            this.listBoxApps.Size = new System.Drawing.Size(175, 186);
            this.listBoxApps.TabIndex = 7;
            this.listBoxApps.SelectedIndexChanged += new System.EventHandler(this.listBoxApps_SelectedIndexChanged);
            // 
            // listBoxContainers
            // 
            this.listBoxContainers.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.listBoxContainers.FormattingEnabled = true;
            this.listBoxContainers.Location = new System.Drawing.Point(278, 19);
            this.listBoxContainers.Name = "listBoxContainers";
            this.listBoxContainers.Size = new System.Drawing.Size(175, 186);
            this.listBoxContainers.TabIndex = 8;
            // 
            // buttonOpen
            // 
            this.buttonOpen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonOpen.Location = new System.Drawing.Point(144, 225);
            this.buttonOpen.Name = "buttonOpen";
            this.buttonOpen.Size = new System.Drawing.Size(175, 58);
            this.buttonOpen.TabIndex = 9;
            this.buttonOpen.Text = "Open";
            this.buttonOpen.UseVisualStyleBackColor = true;
            this.buttonOpen.Click += new System.EventHandler(this.buttonOpen_Click);
            // 
            // buttonClose
            // 
            this.buttonClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonClose.Location = new System.Drawing.Point(144, 305);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(175, 58);
            this.buttonClose.TabIndex = 10;
            this.buttonClose.Text = "Close";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // ClientB
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(465, 425);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.buttonOpen);
            this.Controls.Add(this.listBoxContainers);
            this.Controls.Add(this.listBoxApps);
            this.Name = "ClientB";
            this.Text = "ClientB";
            this.Load += new System.EventHandler(this.ClientB_Load);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ListBox listBoxApps;
        private System.Windows.Forms.ListBox listBoxContainers;
        private System.Windows.Forms.Button buttonOpen;
        private System.Windows.Forms.Button buttonClose;
    }
}

