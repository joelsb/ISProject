namespace App_clienntA
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
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.textBoxOutput = new System.Windows.Forms.TextBox();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.richTextBoxShowApp = new System.Windows.Forms.RichTextBox();
            this.button7 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.richTextShowContainer = new System.Windows.Forms.RichTextBox();
            this.button9 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 33);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(118, 44);
            this.button1.TabIndex = 0;
            this.button1.Text = "POSTNewApp";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.POSTNewApp);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(18, 86);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "Name:";
            // 
            // textBoxName
            // 
            this.textBoxName.Location = new System.Drawing.Point(84, 84);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(100, 22);
            this.textBoxName.TabIndex = 2;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(228, 33);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(118, 44);
            this.button2.TabIndex = 3;
            this.button2.Text = "UPDATEApp";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.UPDATEApp);
            // 
            // textBoxOutput
            // 
            this.textBoxOutput.Location = new System.Drawing.Point(191, 84);
            this.textBoxOutput.Name = "textBoxOutput";
            this.textBoxOutput.Size = new System.Drawing.Size(197, 22);
            this.textBoxOutput.TabIndex = 4;
            this.textBoxOutput.Text = "textBoxOutput";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(15, 136);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(137, 44);
            this.button3.TabIndex = 5;
            this.button3.Text = "POSTNewContainer";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.POSTNewContainer);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(11, 195);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(141, 37);
            this.button4.TabIndex = 6;
            this.button4.Text = "POSTNewSubcription";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.POSTNewSubcription);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(161, 137);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(134, 43);
            this.button5.TabIndex = 7;
            this.button5.Text = "UPDATEContainer";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.UPDATEContainer);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(138, 33);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(80, 44);
            this.button6.TabIndex = 8;
            this.button6.Text = "DELApp";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.DELApp);
            // 
            // richTextBoxShowApp
            // 
            this.richTextBoxShowApp.Location = new System.Drawing.Point(524, 54);
            this.richTextBoxShowApp.Name = "richTextBoxShowApp";
            this.richTextBoxShowApp.Size = new System.Drawing.Size(279, 153);
            this.richTextBoxShowApp.TabIndex = 9;
            this.richTextBoxShowApp.Text = "richTextBoxShowApp";
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(660, 6);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(132, 43);
            this.button7.TabIndex = 10;
            this.button7.Text = "GETAllApp";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.GETAllApp);
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(305, 137);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(99, 43);
            this.button8.TabIndex = 11;
            this.button8.Text = "DELContainer";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.DELContainer);
            // 
            // richTextShowContainer
            // 
            this.richTextShowContainer.Location = new System.Drawing.Point(526, 269);
            this.richTextShowContainer.Name = "richTextShowContainer";
            this.richTextShowContainer.Size = new System.Drawing.Size(272, 178);
            this.richTextShowContainer.TabIndex = 12;
            this.richTextShowContainer.Text = "richTextShowContainer";
            // 
            // button9
            // 
            this.button9.Location = new System.Drawing.Point(675, 222);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(122, 39);
            this.button9.TabIndex = 13;
            this.button9.Text = "GETAllContainer";
            this.button9.UseVisualStyleBackColor = true;
            this.button9.Click += new System.EventHandler(this.GETAllContainer);
            // 
            // ClientA
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.button9);
            this.Controls.Add(this.richTextShowContainer);
            this.Controls.Add(this.button8);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.richTextBoxShowApp);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.textBoxOutput);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.textBoxName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Name = "ClientA";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.ClientA_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox textBoxOutput;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.RichTextBox richTextBoxShowApp;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.RichTextBox richTextShowContainer;
        private System.Windows.Forms.Button button9;
    }
}

