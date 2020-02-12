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
    public partial class EdycjaUslugi : Form
    {
        Baza baza = new Baza();

        Usluga p = new Usluga();

        public EdycjaUslugi(int id = 0)
        {
            InitializeComponent();

            if (id > 0)
            {
                p = baza.Usluga.FirstOrDefault(x => x.Id == id);

                button3.Visible = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Czy chcesz usunąć usługę?", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (p.Id > 0)
                    baza.Usluga.Remove(p);

                baza.SaveChanges();

                DialogResult = DialogResult.OK;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            decimal cena;
            if(!decimal.TryParse(textBox2.Text, out cena))
            {
                MessageBox.Show("Podaj poprawną cenę usługi!");
                return;
            }

            p.Nazwa = textBox1.Text;
            p.Cena = cena;

            if(p.Id == 0)
            {
                baza.Usluga.Add(p);
            }

            baza.SaveChanges();

            DialogResult = DialogResult.OK;
        }

        private void EdycjaPracownika_Shown(object sender, EventArgs e)
        {
            if(p.Id > 0)
            {
                textBox1.Text = p.Nazwa;
                textBox2.Text = p.Cena.ToString();
            }
        }
    }
}
