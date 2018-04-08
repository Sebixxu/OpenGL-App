namespace ProjektOstatecznaWersja
{
    partial class Ustawienia
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
            this.label1 = new System.Windows.Forms.Label();
            this.natSwiatlaLabel = new System.Windows.Forms.Label();
            this.kierSwiatlaLabel = new System.Windows.Forms.Label();
            this.wlaSwiatlaLabel = new System.Windows.Forms.Label();
            this.zapiszBTN = new System.Windows.Forms.Button();
            this.anulujBTN = new System.Windows.Forms.Button();
            this.natezenieSwiatlaTB = new System.Windows.Forms.TextBox();
            this.lightCheckBox = new System.Windows.Forms.CheckBox();
            this.ksXTB = new System.Windows.Forms.TextBox();
            this.ksYTB = new System.Windows.Forms.TextBox();
            this.ksZTB = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(169, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(117, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Ustawienia";
            // 
            // natSwiatlaLabel
            // 
            this.natSwiatlaLabel.AutoSize = true;
            this.natSwiatlaLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.natSwiatlaLabel.Location = new System.Drawing.Point(12, 47);
            this.natSwiatlaLabel.Name = "natSwiatlaLabel";
            this.natSwiatlaLabel.Size = new System.Drawing.Size(172, 20);
            this.natSwiatlaLabel.TabIndex = 1;
            this.natSwiatlaLabel.Text = "Nateżenie oświetlenia";
            // 
            // kierSwiatlaLabel
            // 
            this.kierSwiatlaLabel.AutoSize = true;
            this.kierSwiatlaLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.kierSwiatlaLabel.Location = new System.Drawing.Point(12, 123);
            this.kierSwiatlaLabel.Name = "kierSwiatlaLabel";
            this.kierSwiatlaLabel.Size = new System.Drawing.Size(162, 20);
            this.kierSwiatlaLabel.TabIndex = 3;
            this.kierSwiatlaLabel.Text = "Kierunek oświetlenia";
            // 
            // wlaSwiatlaLabel
            // 
            this.wlaSwiatlaLabel.AutoSize = true;
            this.wlaSwiatlaLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.wlaSwiatlaLabel.Location = new System.Drawing.Point(12, 86);
            this.wlaSwiatlaLabel.Name = "wlaSwiatlaLabel";
            this.wlaSwiatlaLabel.Size = new System.Drawing.Size(175, 20);
            this.wlaSwiatlaLabel.TabIndex = 4;
            this.wlaSwiatlaLabel.Text = "Włączenie oświetlenia";
            // 
            // zapiszBTN
            // 
            this.zapiszBTN.Location = new System.Drawing.Point(112, 157);
            this.zapiszBTN.Name = "zapiszBTN";
            this.zapiszBTN.Size = new System.Drawing.Size(75, 23);
            this.zapiszBTN.TabIndex = 5;
            this.zapiszBTN.Text = "Zapisz";
            this.zapiszBTN.UseVisualStyleBackColor = true;
            this.zapiszBTN.Click += new System.EventHandler(this.zapiszBTN_Click);
            // 
            // anulujBTN
            // 
            this.anulujBTN.Location = new System.Drawing.Point(253, 157);
            this.anulujBTN.Name = "anulujBTN";
            this.anulujBTN.Size = new System.Drawing.Size(75, 23);
            this.anulujBTN.TabIndex = 6;
            this.anulujBTN.Text = "Anuluj";
            this.anulujBTN.UseVisualStyleBackColor = true;
            this.anulujBTN.Click += new System.EventHandler(this.anulujBTN_Click);
            // 
            // natezenieSwiatlaTB
            // 
            this.natezenieSwiatlaTB.Location = new System.Drawing.Point(312, 47);
            this.natezenieSwiatlaTB.Name = "natezenieSwiatlaTB";
            this.natezenieSwiatlaTB.Size = new System.Drawing.Size(63, 20);
            this.natezenieSwiatlaTB.TabIndex = 9;
            this.natezenieSwiatlaTB.Text = "0,5";
            this.natezenieSwiatlaTB.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.natezenieSwiatlaTB_KeyPress);
            // 
            // lightCheckBox
            // 
            this.lightCheckBox.AutoSize = true;
            this.lightCheckBox.Checked = true;
            this.lightCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.lightCheckBox.Location = new System.Drawing.Point(312, 86);
            this.lightCheckBox.Name = "lightCheckBox";
            this.lightCheckBox.Size = new System.Drawing.Size(76, 17);
            this.lightCheckBox.TabIndex = 10;
            this.lightCheckBox.Text = "Włączone";
            this.lightCheckBox.UseVisualStyleBackColor = true;
            // 
            // ksXTB
            // 
            this.ksXTB.Location = new System.Drawing.Point(312, 123);
            this.ksXTB.Name = "ksXTB";
            this.ksXTB.Size = new System.Drawing.Size(25, 20);
            this.ksXTB.TabIndex = 11;
            this.ksXTB.Text = "1";
            this.ksXTB.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ksXTB_KeyPress);
            // 
            // ksYTB
            // 
            this.ksYTB.Location = new System.Drawing.Point(350, 123);
            this.ksYTB.Name = "ksYTB";
            this.ksYTB.Size = new System.Drawing.Size(25, 20);
            this.ksYTB.TabIndex = 12;
            this.ksYTB.Text = "0,9";
            this.ksYTB.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ksYTB_KeyPress);
            // 
            // ksZTB
            // 
            this.ksZTB.Location = new System.Drawing.Point(387, 123);
            this.ksZTB.Name = "ksZTB";
            this.ksZTB.Size = new System.Drawing.Size(25, 20);
            this.ksZTB.TabIndex = 13;
            this.ksZTB.Text = "0";
            this.ksZTB.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ksZTB_KeyPress);
            // 
            // Ustawienia
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(454, 192);
            this.Controls.Add(this.ksZTB);
            this.Controls.Add(this.ksYTB);
            this.Controls.Add(this.ksXTB);
            this.Controls.Add(this.lightCheckBox);
            this.Controls.Add(this.natezenieSwiatlaTB);
            this.Controls.Add(this.anulujBTN);
            this.Controls.Add(this.zapiszBTN);
            this.Controls.Add(this.wlaSwiatlaLabel);
            this.Controls.Add(this.kierSwiatlaLabel);
            this.Controls.Add(this.natSwiatlaLabel);
            this.Controls.Add(this.label1);
            this.Name = "Ustawienia";
            this.Text = "Ustawienia";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label natSwiatlaLabel;
        private System.Windows.Forms.Label kierSwiatlaLabel;
        private System.Windows.Forms.Label wlaSwiatlaLabel;
        private System.Windows.Forms.Button zapiszBTN;
        private System.Windows.Forms.Button anulujBTN;
        private System.Windows.Forms.TextBox natezenieSwiatlaTB;
        private System.Windows.Forms.CheckBox lightCheckBox;
        private System.Windows.Forms.TextBox ksXTB;
        private System.Windows.Forms.TextBox ksYTB;
        private System.Windows.Forms.TextBox ksZTB;
    }
}