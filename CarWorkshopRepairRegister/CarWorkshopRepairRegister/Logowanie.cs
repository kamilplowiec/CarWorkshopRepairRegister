using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CarWorkshopRepairRegister
{
    public partial class Logowanie : Form
    {
        Baza baza = new Baza();

        public Pracownik Pracownik { get; set; }

        public Logowanie()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string Login = textBox1.Text;
            string Haslo = textBox2.Text;

            var p = baza.Pracownik.FirstOrDefault(x => x.Login == Login && x.Haslo == Haslo);

            if(p == null)
            {
                MessageBox.Show("Niepoprawne dane logowania!");
                return;
            }

            Pracownik = p;

            DialogResult = DialogResult.OK;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close(); 
        }
    }
}
