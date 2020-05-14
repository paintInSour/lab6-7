namespace lab4_5
{
    partial class Form1
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
            lab4_5.UserControl1 userControl11;
            userControl11 = new lab4_5.UserControl1();
            this.SuspendLayout();
            // 
            // userControl11
            // 
            userControl11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            userControl11.Dock = System.Windows.Forms.DockStyle.Top;
            userControl11.Location = new System.Drawing.Point(0, 0);
            userControl11.Name = "userControl11";
            userControl11.Size = new System.Drawing.Size(994, 473);
            userControl11.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(994, 539);
            this.Controls.Add(userControl11);
            this.Name = "Form1";
            this.Text = "7";
            this.ResumeLayout(false);

        }

        #endregion
    }
}

