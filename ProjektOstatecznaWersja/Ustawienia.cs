using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjektOstatecznaWersja
{
    public partial class Ustawienia : Form
    {
        public float natezenieSwiatla { get; private set; }
        public bool wlaczenieSwiatla { get; private set; }
        public float kierunekSwiatlaX { get; private set; }
        public float kierunekSwiatlaY { get; private set; }
        public float kierunekSwiatlaZ { get; private set; }
        
        public Ustawienia()
        {
            InitializeComponent();

            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            wlaczenieSwiatla = true;

            natezenieSwiatla = 0.5f;
            kierunekSwiatlaX = 1.0f;
            kierunekSwiatlaY = 0.9f;
            kierunekSwiatlaZ = 0f;
        }

        public Ustawienia(bool wlaczenieSwiatla, float natezenieSwiatla, float kierunekSwiatlaX, float kierunekSwiatlaY, float kierunekSwiatlaZ)
        {
            InitializeComponent();

            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            this.wlaczenieSwiatla = wlaczenieSwiatla;
            this.natezenieSwiatla = natezenieSwiatla;
            this.kierunekSwiatlaX = kierunekSwiatlaX;
            this.kierunekSwiatlaY = kierunekSwiatlaY;
            this.kierunekSwiatlaZ = kierunekSwiatlaZ;

            natezenieSwiatlaTB.Text = natezenieSwiatla.ToString();
            ksXTB.Text = kierunekSwiatlaX.ToString();
            ksYTB.Text = kierunekSwiatlaY.ToString();
            ksZTB.Text = kierunekSwiatlaZ.ToString();
            lightCheckBox.Checked = wlaczenieSwiatla;
               
        }

        private void ObslugaZnakow(KeyPressEventArgs e, TextBox textBox)
        {
            char ch = e.KeyChar;

            if (ch == 44 && textBox.Text.IndexOf(',') != -1)
            {
                e.Handled = true;
                return;
            }

            if (!Char.IsDigit(ch) && ch != 8 && ch != 44)
            {
                e.Handled = true;
                MessageBox.Show("Podaj liczbę!", "Błąd!");
            }
        }

        private void natezenieSwiatlaTB_KeyPress(object sender, KeyPressEventArgs e)
        {
            ObslugaZnakow(e, natezenieSwiatlaTB);
        }

        private void ksXTB_KeyPress(object sender, KeyPressEventArgs e)
        {
            ObslugaZnakow(e, ksXTB);
        }

        private void ksYTB_KeyPress(object sender, KeyPressEventArgs e)
        {
            ObslugaZnakow(e, ksYTB);
        }

        private void ksZTB_KeyPress(object sender, KeyPressEventArgs e)
        {
            ObslugaZnakow(e, ksZTB);
        }

        private void zapiszBTN_Click(object sender, EventArgs e)
        {
            //Pobieranie danych z boxów
            wlaczenieSwiatla = lightCheckBox.Checked;
            natezenieSwiatla = float.Parse(natezenieSwiatlaTB.Text);
            kierunekSwiatlaX = float.Parse(ksXTB.Text);
            kierunekSwiatlaY = float.Parse(ksYTB.Text);
            kierunekSwiatlaZ = float.Parse(ksZTB.Text);

            Close();
        }

        private void anulujBTN_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
