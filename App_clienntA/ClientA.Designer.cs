﻿namespace App_clienntA
{
    partial class ClientA
    {
        /// <summary>
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Windows Form Designer

        /// <summary>
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ClientA));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.listBoxApps = new System.Windows.Forms.ListBox();
            this.contextMenuStripApp = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.refreshToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.buttonCreateApps = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.listBoxContainers = new System.Windows.Forms.ListBox();
            this.contextMenuStripContainer = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.refreshToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.buttonCreateContainer = new System.Windows.Forms.Button();
            this.listBoxSubscriptions = new System.Windows.Forms.ListBox();
            this.buttonCreateSubscription = new System.Windows.Forms.Button();
            this.labelData = new System.Windows.Forms.Label();
            this.listBoxData = new System.Windows.Forms.ListBox();
            this.buttonData = new System.Windows.Forms.Button();
            this.contextMenuStripSubscription = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.refreshToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStripData = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.refreshToolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStripApp.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.contextMenuStripContainer.SuspendLayout();
            this.contextMenuStripSubscription.SuspendLayout();
            this.contextMenuStripData.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // listBoxApps
            // 
            resources.ApplyResources(this.listBoxApps, "listBoxApps");
            this.listBoxApps.ContextMenuStrip = this.contextMenuStripApp;
            this.listBoxApps.FormattingEnabled = true;
            this.listBoxApps.Name = "listBoxApps";
            this.listBoxApps.SelectedIndexChanged += new System.EventHandler(this.listBoxApps_SelectedIndexChanged);
            // 
            // contextMenuStripApp
            // 
            this.contextMenuStripApp.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.refreshToolStripMenuItem,
            this.editToolStripMenuItem,
            this.deleteToolStripMenuItem});
            this.contextMenuStripApp.Name = "contextMenuStripApp";
            resources.ApplyResources(this.contextMenuStripApp, "contextMenuStripApp");
            // 
            // refreshToolStripMenuItem
            // 
            this.refreshToolStripMenuItem.Name = "refreshToolStripMenuItem";
            resources.ApplyResources(this.refreshToolStripMenuItem, "refreshToolStripMenuItem");
            this.refreshToolStripMenuItem.Click += new System.EventHandler(this.refreshToolStripMenuItem_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            resources.ApplyResources(this.editToolStripMenuItem, "editToolStripMenuItem");
            this.editToolStripMenuItem.Click += new System.EventHandler(this.editToolStripMenuItem_Click);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            resources.ApplyResources(this.deleteToolStripMenuItem, "deleteToolStripMenuItem");
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // buttonCreateApps
            // 
            resources.ApplyResources(this.buttonCreateApps, "buttonCreateApps");
            this.buttonCreateApps.Name = "buttonCreateApps";
            this.buttonCreateApps.UseVisualStyleBackColor = true;
            this.buttonCreateApps.Click += new System.EventHandler(this.buttonCreateApps_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripProgressBar1,
            this.toolStripStatusLabel1});
            resources.ApplyResources(this.statusStrip1, "statusStrip1");
            this.statusStrip1.Name = "statusStrip1";
            // 
            // toolStripProgressBar1
            // 
            this.toolStripProgressBar1.Name = "toolStripProgressBar1";
            resources.ApplyResources(this.toolStripProgressBar1, "toolStripProgressBar1");
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            resources.ApplyResources(this.toolStripStatusLabel1, "toolStripStatusLabel1");
            // 
            // listBoxContainers
            // 
            resources.ApplyResources(this.listBoxContainers, "listBoxContainers");
            this.listBoxContainers.ContextMenuStrip = this.contextMenuStripContainer;
            this.listBoxContainers.FormattingEnabled = true;
            this.listBoxContainers.Name = "listBoxContainers";
            this.listBoxContainers.SelectedIndexChanged += new System.EventHandler(this.listBoxContainers_SelectedIndexChanged);
            // 
            // contextMenuStripContainer
            // 
            this.contextMenuStripContainer.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.refreshToolStripMenuItem1,
            this.editToolStripMenuItem1,
            this.deleteToolStripMenuItem1});
            this.contextMenuStripContainer.Name = "contextMenuStripContainer";
            resources.ApplyResources(this.contextMenuStripContainer, "contextMenuStripContainer");
            // 
            // refreshToolStripMenuItem1
            // 
            this.refreshToolStripMenuItem1.Name = "refreshToolStripMenuItem1";
            resources.ApplyResources(this.refreshToolStripMenuItem1, "refreshToolStripMenuItem1");
            this.refreshToolStripMenuItem1.Click += new System.EventHandler(this.refreshToolStripMenuItem1_Click);
            // 
            // editToolStripMenuItem1
            // 
            this.editToolStripMenuItem1.Name = "editToolStripMenuItem1";
            resources.ApplyResources(this.editToolStripMenuItem1, "editToolStripMenuItem1");
            this.editToolStripMenuItem1.Click += new System.EventHandler(this.editToolStripMenuItem1_Click);
            // 
            // deleteToolStripMenuItem1
            // 
            this.deleteToolStripMenuItem1.Name = "deleteToolStripMenuItem1";
            resources.ApplyResources(this.deleteToolStripMenuItem1, "deleteToolStripMenuItem1");
            this.deleteToolStripMenuItem1.Click += new System.EventHandler(this.deleteToolStripMenuItem1_Click);
            // 
            // buttonCreateContainer
            // 
            resources.ApplyResources(this.buttonCreateContainer, "buttonCreateContainer");
            this.buttonCreateContainer.Name = "buttonCreateContainer";
            this.buttonCreateContainer.UseVisualStyleBackColor = true;
            this.buttonCreateContainer.Click += new System.EventHandler(this.buttonCreateContainer_Click);
            // 
            // listBoxSubscriptions
            // 
            resources.ApplyResources(this.listBoxSubscriptions, "listBoxSubscriptions");
            this.listBoxSubscriptions.ContextMenuStrip = this.contextMenuStripSubscription;
            this.listBoxSubscriptions.FormattingEnabled = true;
            this.listBoxSubscriptions.Name = "listBoxSubscriptions";
            // 
            // buttonCreateSubscription
            // 
            resources.ApplyResources(this.buttonCreateSubscription, "buttonCreateSubscription");
            this.buttonCreateSubscription.Name = "buttonCreateSubscription";
            this.buttonCreateSubscription.UseVisualStyleBackColor = true;
            this.buttonCreateSubscription.Click += new System.EventHandler(this.buttonCreateSubscription_Click);
            // 
            // labelData
            // 
            resources.ApplyResources(this.labelData, "labelData");
            this.labelData.Name = "labelData";
            // 
            // listBoxData
            // 
            resources.ApplyResources(this.listBoxData, "listBoxData");
            this.listBoxData.ContextMenuStrip = this.contextMenuStripData;
            this.listBoxData.FormattingEnabled = true;
            this.listBoxData.Name = "listBoxData";
            // 
            // buttonData
            // 
            resources.ApplyResources(this.buttonData, "buttonData");
            this.buttonData.Name = "buttonData";
            this.buttonData.UseVisualStyleBackColor = true;
            this.buttonData.Click += new System.EventHandler(this.buttonData_Click);
            // 
            // contextMenuStripSubscription
            // 
            this.contextMenuStripSubscription.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.refreshToolStripMenuItem2,
            this.deleteToolStripMenuItem2});
            this.contextMenuStripSubscription.Name = "contextMenuStripSubscription";
            resources.ApplyResources(this.contextMenuStripSubscription, "contextMenuStripSubscription");
            // 
            // refreshToolStripMenuItem2
            // 
            this.refreshToolStripMenuItem2.Name = "refreshToolStripMenuItem2";
            resources.ApplyResources(this.refreshToolStripMenuItem2, "refreshToolStripMenuItem2");
            this.refreshToolStripMenuItem2.Click += new System.EventHandler(this.refreshToolStripMenuItem2_Click);
            // 
            // deleteToolStripMenuItem2
            // 
            this.deleteToolStripMenuItem2.Name = "deleteToolStripMenuItem2";
            resources.ApplyResources(this.deleteToolStripMenuItem2, "deleteToolStripMenuItem2");
            this.deleteToolStripMenuItem2.Click += new System.EventHandler(this.deleteToolStripMenuItem2_Click);
            // 
            // contextMenuStripData
            // 
            this.contextMenuStripData.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.refreshToolStripMenuItem3,
            this.deleteToolStripMenuItem3});
            this.contextMenuStripData.Name = "contextMenuStripData";
            resources.ApplyResources(this.contextMenuStripData, "contextMenuStripData");
            // 
            // refreshToolStripMenuItem3
            // 
            this.refreshToolStripMenuItem3.Name = "refreshToolStripMenuItem3";
            resources.ApplyResources(this.refreshToolStripMenuItem3, "refreshToolStripMenuItem3");
            this.refreshToolStripMenuItem3.Click += new System.EventHandler(this.refreshToolStripMenuItem3_Click);
            // 
            // deleteToolStripMenuItem3
            // 
            this.deleteToolStripMenuItem3.Name = "deleteToolStripMenuItem3";
            resources.ApplyResources(this.deleteToolStripMenuItem3, "deleteToolStripMenuItem3");
            this.deleteToolStripMenuItem3.Click += new System.EventHandler(this.deleteToolStripMenuItem3_Click);
            // 
            // ClientA
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.buttonData);
            this.Controls.Add(this.listBoxData);
            this.Controls.Add(this.labelData);
            this.Controls.Add(this.buttonCreateSubscription);
            this.Controls.Add(this.listBoxSubscriptions);
            this.Controls.Add(this.buttonCreateContainer);
            this.Controls.Add(this.listBoxContainers);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.buttonCreateApps);
            this.Controls.Add(this.listBoxApps);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "ClientA";
            this.Load += new System.EventHandler(this.ClientA_Load);
            this.contextMenuStripApp.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.contextMenuStripContainer.ResumeLayout(false);
            this.contextMenuStripSubscription.ResumeLayout(false);
            this.contextMenuStripData.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListBox listBoxApps;
        private System.Windows.Forms.Button buttonCreateApps;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ListBox listBoxContainers;
        private System.Windows.Forms.Button buttonCreateContainer;
        private System.Windows.Forms.ListBox listBoxSubscriptions;
        private System.Windows.Forms.Button buttonCreateSubscription;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripApp;
        private System.Windows.Forms.ToolStripMenuItem refreshToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripContainer;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem refreshToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem1;
        private System.Windows.Forms.Label labelData;
        private System.Windows.Forms.ListBox listBoxData;
        private System.Windows.Forms.Button buttonData;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripSubscription;
        private System.Windows.Forms.ToolStripMenuItem refreshToolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem2;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripData;
        private System.Windows.Forms.ToolStripMenuItem refreshToolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem3;
    }
}

