namespace Gui
{
    partial class LanderGui
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
            this.canvas = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // canvas
            // 
            this.canvas.BackColor = System.Drawing.Color.Black;
            this.canvas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.canvas.Location = new System.Drawing.Point(0, 0);
            this.canvas.Name = "canvas";
            this.canvas.Size = new System.Drawing.Size(1325, 700);
            this.canvas.TabIndex = 0;
            // 
            // LanderGui
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1325, 700);
            this.Controls.Add(this.canvas);
            this.Name = "LanderGui";
            this.Text = "LanderGui";
            this.SizeChanged += new System.EventHandler(this.LanderGui_SizeChanged);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.LanderGui_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.LanderGui_KeyUp);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel canvas;
    }
}

