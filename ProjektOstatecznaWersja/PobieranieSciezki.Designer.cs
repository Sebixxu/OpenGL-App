namespace ProjektOstatecznaWersja
{
    partial class PobieranieSciezki
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
            this.LadowanieObiektuLabel = new System.Windows.Forms.Label();
            this.obiektFilePathTB = new System.Windows.Forms.TextBox();
            this.OtworzObiektBtn = new System.Windows.Forms.Button();
            this.UruchomBtn = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.opcjeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ładowaniePlikuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ustawieniaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.informacjeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // LadowanieObiektuLabel
            // 
            this.LadowanieObiektuLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.LadowanieObiektuLabel.Location = new System.Drawing.Point(12, 37);
            this.LadowanieObiektuLabel.Name = "LadowanieObiektuLabel";
            this.LadowanieObiektuLabel.Size = new System.Drawing.Size(155, 25);
            this.LadowanieObiektuLabel.TabIndex = 2;
            this.LadowanieObiektuLabel.Text = "Ładowanie obiektu";
            // 
            // obiektFilePathTB
            // 
            this.obiektFilePathTB.Location = new System.Drawing.Point(182, 37);
            this.obiektFilePathTB.Name = "obiektFilePathTB";
            this.obiektFilePathTB.ReadOnly = true;
            this.obiektFilePathTB.Size = new System.Drawing.Size(175, 20);
            this.obiektFilePathTB.TabIndex = 6;
            // 
            // OtworzObiektBtn
            // 
            this.OtworzObiektBtn.Location = new System.Drawing.Point(363, 37);
            this.OtworzObiektBtn.Name = "OtworzObiektBtn";
            this.OtworzObiektBtn.Size = new System.Drawing.Size(75, 23);
            this.OtworzObiektBtn.TabIndex = 9;
            this.OtworzObiektBtn.Text = "Otwórz";
            this.OtworzObiektBtn.UseVisualStyleBackColor = true;
            this.OtworzObiektBtn.Click += new System.EventHandler(this.OtworzObiektBtn_Click);
            // 
            // UruchomBtn
            // 
            this.UruchomBtn.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.UruchomBtn.Location = new System.Drawing.Point(166, 65);
            this.UruchomBtn.Name = "UruchomBtn";
            this.UruchomBtn.Size = new System.Drawing.Size(89, 39);
            this.UruchomBtn.TabIndex = 15;
            this.UruchomBtn.Text = "Uruchom";
            this.UruchomBtn.UseVisualStyleBackColor = true;
            this.UruchomBtn.Click += new System.EventHandler(this.UruchomBtn_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.opcjeToolStripMenuItem,
            this.informacjeToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(448, 24);
            this.menuStrip1.TabIndex = 16;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // opcjeToolStripMenuItem
            // 
            this.opcjeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ładowaniePlikuToolStripMenuItem,
            this.ustawieniaToolStripMenuItem});
            this.opcjeToolStripMenuItem.Name = "opcjeToolStripMenuItem";
            this.opcjeToolStripMenuItem.Size = new System.Drawing.Size(50, 20);
            this.opcjeToolStripMenuItem.Text = "Opcje";
            // 
            // ładowaniePlikuToolStripMenuItem
            // 
            this.ładowaniePlikuToolStripMenuItem.Name = "ładowaniePlikuToolStripMenuItem";
            this.ładowaniePlikuToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.ładowaniePlikuToolStripMenuItem.Text = "Ładowanie pliku";
            this.ładowaniePlikuToolStripMenuItem.Click += new System.EventHandler(this.ładowaniePlikuToolStripMenuItem_Click);
            // 
            // ustawieniaToolStripMenuItem
            // 
            this.ustawieniaToolStripMenuItem.Name = "ustawieniaToolStripMenuItem";
            this.ustawieniaToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.ustawieniaToolStripMenuItem.Text = "Ustawienia";
            this.ustawieniaToolStripMenuItem.Click += new System.EventHandler(this.ustawieniaToolStripMenuItem_Click);
            // 
            // informacjeToolStripMenuItem
            // 
            this.informacjeToolStripMenuItem.Name = "informacjeToolStripMenuItem";
            this.informacjeToolStripMenuItem.Size = new System.Drawing.Size(76, 20);
            this.informacjeToolStripMenuItem.Text = "Informacje";
            this.informacjeToolStripMenuItem.Click += new System.EventHandler(this.informacjeToolStripMenuItem_Click);
            // 
            // PobieranieSciezki
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(448, 115);
            this.Controls.Add(this.UruchomBtn);
            this.Controls.Add(this.OtworzObiektBtn);
            this.Controls.Add(this.obiektFilePathTB);
            this.Controls.Add(this.LadowanieObiektuLabel);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "PobieranieSciezki";
            this.Text = "PobieranieSciezki";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label LadowanieObiektuLabel;
        private System.Windows.Forms.TextBox obiektFilePathTB;
        private System.Windows.Forms.Button OtworzObiektBtn;
        private System.Windows.Forms.Button UruchomBtn;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem opcjeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ładowaniePlikuToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ustawieniaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem informacjeToolStripMenuItem;
    }
}