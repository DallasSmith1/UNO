namespace UNO
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnOrdered = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnRandom = new System.Windows.Forms.Button();
            this.lbxCards = new System.Windows.Forms.ListBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnOrdered
            // 
            this.btnOrdered.Location = new System.Drawing.Point(231, 1078);
            this.btnOrdered.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnOrdered.Name = "btnOrdered";
            this.btnOrdered.Size = new System.Drawing.Size(296, 45);
            this.btnOrdered.TabIndex = 0;
            this.btnOrdered.Text = "Create Ordered";
            this.btnOrdered.UseVisualStyleBackColor = true;
            this.btnOrdered.Click += new System.EventHandler(this.btnOrdered_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnRandom);
            this.panel1.Controls.Add(this.lbxCards);
            this.panel1.Controls.Add(this.btnOrdered);
            this.panel1.Location = new System.Drawing.Point(27, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1393, 1158);
            this.panel1.TabIndex = 2;
            // 
            // btnRandom
            // 
            this.btnRandom.Location = new System.Drawing.Point(835, 1078);
            this.btnRandom.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnRandom.Name = "btnRandom";
            this.btnRandom.Size = new System.Drawing.Size(296, 45);
            this.btnRandom.TabIndex = 3;
            this.btnRandom.Text = "Create Random";
            this.btnRandom.UseVisualStyleBackColor = true;
            this.btnRandom.Click += new System.EventHandler(this.btnRandom_Click);
            // 
            // lbxCards
            // 
            this.lbxCards.FormattingEnabled = true;
            this.lbxCards.ItemHeight = 32;
            this.lbxCards.Location = new System.Drawing.Point(42, 55);
            this.lbxCards.Name = "lbxCards";
            this.lbxCards.Size = new System.Drawing.Size(1299, 996);
            this.lbxCards.TabIndex = 2;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 32F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1475, 1211);
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "Form1";
            this.Text = "Form1";
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Button btnOrdered;
        private Panel panel1;
        private Button btnRandom;
        private ListBox lbxCards;
    }
}