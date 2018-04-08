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
    public partial class PobieranieSciezki : Form
    {
        private static string opis = "Sterowanie:\r\n'w' - Kamera w przód.\r\n's' - Kamera w tył.\r\n'a' - Kamera w lewo.\r\n'd' - Kamera w prawo.\r\n'l' - Obsługa oświetlenia\r\n+ Mysz.\r\n'esc' - Wyjście.";
        private string objPath;

        public bool wlaczenieSw { get; private set; }
        public float natezenieSw { get; private set; }
        public float kierunekSwX { get; private set; }
        public float kierunekSwY { get; private set; }
        public float kierunekSwZ { get; private set; }

        public bool startOpenGL { get; private set; }

        public PobieranieSciezki()
        {
            InitializeComponent();

            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            wlaczenieSw = true;

            natezenieSw = 0.5f;
            kierunekSwX = 1.0f;
            kierunekSwY = 0.9f;
            kierunekSwZ = 0f;
        }

        public string GetObjPath()
        {
            return objPath;
        }

        private void OtworzObiektBtn_Click(object sender, EventArgs e)
        {
            {
                try
                {
                    OpenDialogWindow(".obj", "OBJ|*.obj", obiektFilePathTB);
                }
                catch (Exception ex)
                {

                    throw;
                }
            }
        }

        private void OpenDialogWindow(string rozszerzenie, string filtr, TextBox textBox)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = filtr,
                DefaultExt = rozszerzenie,
                Multiselect = false
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string fileName = openFileDialog.FileName;
                if (openFileDialog.CheckFileExists == true) textBox.Text = fileName;
            }
        }

        private void UruchomBtn_Click(object sender, EventArgs e)
        {
            try
            {
                objPath = obiektFilePathTB.Text;

                if (objPath != string.Empty)
                {
                    Close();

                    startOpenGL = true;
                }
                else
                {
                    string wiadomosc = "Nie zostala wybrana ścieżka do pliku .obj!";
                    string tytul = "Błąd!";
                    MessageBoxButtons button = MessageBoxButtons.OK;
                    DialogResult result = MessageBox.Show(wiadomosc, tytul, button);
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private void ładowaniePlikuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                OpenDialogWindow(".obj", "OBJ|*.obj", obiektFilePathTB);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private void ustawieniaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Ustawienia oknoUstawien = new Ustawienia(wlaczenieSw, natezenieSw, kierunekSwX, kierunekSwY, kierunekSwZ);

            oknoUstawien.ShowDialog();

            wlaczenieSw = oknoUstawien.wlaczenieSwiatla;
            natezenieSw = oknoUstawien.natezenieSwiatla;
            kierunekSwX = oknoUstawien.kierunekSwiatlaX;
            kierunekSwY = oknoUstawien.kierunekSwiatlaY;
            kierunekSwZ = oknoUstawien.kierunekSwiatlaZ;
        }

        private void informacjeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutApp aboutApp = new AboutApp("OpenGL App", "1.0.0", "-----------", "Sebastian Tomczak, Politechnika Lubelska ", opis);

            aboutApp.ShowDialog();
        }
    }
}
