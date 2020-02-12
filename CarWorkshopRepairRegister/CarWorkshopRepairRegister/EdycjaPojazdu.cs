using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CarWorkshopRepairRegister
{
    public partial class EdycjaPojazdu : Form
    {
        Baza baza = new Baza();

        public Pojazd p = new Pojazd();

        public EdycjaPojazdu(int id = 0)
        {
            InitializeComponent();

            if (id > 0)
            {
                p = baza.Pojazd.FirstOrDefault(x => x.Id == id);

                button3.Visible = true;
            }

            LadujKlientow();
        }

        private void LadujKlientow()
        {
            comboBox1.Items.Clear();
            baza.Osoba.Load();
            comboBox1.Items.AddRange(baza.Osoba.Select(x => x.Nazwa).ToList().ToArray());
        }

        private void UstawKlienta(string nazwa)
        {
            comboBox1.SelectedItem = nazwa;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Czy chcesz usunąć pojazd?", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (p.Id > 0)
                    baza.Pojazd.Remove(p);

                baza.SaveChanges();

                DialogResult = DialogResult.OK;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem == null)
            {
                MessageBox.Show("Wybierz właściciela!");
                return;
            }

            var klient = baza.Osoba.FirstOrDefault(x => x.Nazwa == comboBox1.SelectedItem.ToString());

            if (klient == null)
            {
                MessageBox.Show("Wybierz właściciela!");
                return;
            }

            if(string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("Podaj markę pojazdu!");
                return;
            }

            if (string.IsNullOrEmpty(textBox2.Text))
            {
                MessageBox.Show("Podaj model pojazdu!");
                return;
            }

            if (string.IsNullOrEmpty(textBox3.Text))
            {
                MessageBox.Show("Podaj VIN pojazdu!");
                return;
            }

            if (string.IsNullOrEmpty(textBox4.Text))
            {
                MessageBox.Show("Podaj numer rejestracyjny pojazdu!");
                return;
            }

            if (string.IsNullOrEmpty(textBox5.Text))
            {
                MessageBox.Show("Podaj rok produkcji pojazdu!");
                return;
            }

            p.Marka = textBox1.Text;
            p.Model = textBox2.Text;
            p.VIN = textBox3.Text;
            p.NrRej = textBox4.Text;
            p.RokProd = int.Parse(textBox5.Text);
            p.WlascicielId = klient.Id;

            if(p.Id == 0)
            {
                baza.Pojazd.Add(p);
            }

            baza.SaveChanges();

            DialogResult = DialogResult.OK;
        }

        private void EdycjaPracownika_Shown(object sender, EventArgs e)
        {
            if(p.Id > 0)
            {
                textBox1.Text = p.Marka;
                textBox2.Text = p.Model;
                textBox3.Text = p.VIN;
                textBox4.Text = p.NrRej;
                textBox5.Text = p.RokProd.ToString();

                UstawKlienta(baza.Osoba.FirstOrDefault(x => x.Id == p.WlascicielId).Nazwa);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            EdycjaOsoby eo = new EdycjaOsoby();
            if (eo.ShowDialog(this) == DialogResult.OK)
            {
                LadujKlientow();

                UstawKlienta(eo.p.Nazwa);
            }
        }
    }
}
