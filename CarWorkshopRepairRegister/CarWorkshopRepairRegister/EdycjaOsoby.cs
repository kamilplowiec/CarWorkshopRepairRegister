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
    public partial class EdycjaOsoby : Form
    {
        Baza baza = new Baza();

        public Osoba p = new Osoba();

        public EdycjaOsoby(int id = 0)
        {
            InitializeComponent();

            if (id > 0)
            {
                p = baza.Osoba.FirstOrDefault(x => x.Id == id);

                button3.Visible = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Czy chcesz usunąć osobę?", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (p.Id > 0)
                    baza.Osoba.Remove(p);

                baza.SaveChanges();

                DialogResult = DialogResult.OK;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            p.Nazwa = textBox1.Text;
            p.Adres = textBox2.Text;

            if(p.Id == 0)
            {
                baza.Osoba.Add(p);
            }

            baza.SaveChanges();

            DialogResult = DialogResult.OK;
        }

        private void EdycjaPracownika_Shown(object sender, EventArgs e)
        {
            if(p.Id > 0)
            {
                textBox1.Text = p.Nazwa;
                textBox2.Text = p.Adres;
            }
        }
    }
}
