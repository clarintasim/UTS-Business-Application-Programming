namespace WindowsFormsApp1
{
    partial class Admin_Control
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
            this.DaftarMenu_btn = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.LapPenjualan_btn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // DaftarMenu_btn
            // 
            this.DaftarMenu_btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DaftarMenu_btn.Location = new System.Drawing.Point(51, 58);
            this.DaftarMenu_btn.Name = "DaftarMenu_btn";
            this.DaftarMenu_btn.Size = new System.Drawing.Size(104, 60);
            this.DaftarMenu_btn.TabIndex = 0;
            this.DaftarMenu_btn.Text = "Daftar Menu";
            this.DaftarMenu_btn.UseVisualStyleBackColor = true;
            this.DaftarMenu_btn.Click += new System.EventHandler(this.DaftarMenu_btn_Click);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(191, 58);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(74, 60);
            this.button1.TabIndex = 1;
            this.button1.Text = "Kasir";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // LapPenjualan_btn
            // 
            this.LapPenjualan_btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LapPenjualan_btn.Location = new System.Drawing.Point(299, 58);
            this.LapPenjualan_btn.Name = "LapPenjualan_btn";
            this.LapPenjualan_btn.Size = new System.Drawing.Size(96, 60);
            this.LapPenjualan_btn.TabIndex = 2;
            this.LapPenjualan_btn.Text = "Laporan Penjualan";
            this.LapPenjualan_btn.UseVisualStyleBackColor = true;
            this.LapPenjualan_btn.Click += new System.EventHandler(this.LapPenjualan_btn_Click);
            // 
            // Admin_Control
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(447, 190);
            this.Controls.Add(this.LapPenjualan_btn);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.DaftarMenu_btn);
            this.MaximizeBox = false;
            this.Name = "Admin_Control";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Admin_Control";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button DaftarMenu_btn;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button LapPenjualan_btn;
    }
}