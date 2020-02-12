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
    public partial class Form1 : Form
    {
        public static Pracownik Zalogowany { get; set; }

        Baza baza = new Baza();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            Zaloguj();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Zaloguj();
        }

        private void Zaloguj()
        {
            label3.Text = "-";
            dataGridView1.DataSource = null;

            Logowanie l = new Logowanie();
            if (l.ShowDialog(this) == DialogResult.OK)
            {
                Zalogowany = l.Pracownik;

                label3.Text = Zalogowany.Nazwa;

                Laduj();
            }
            else
            {
                Close();
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            ListaPracownikow lp = new ListaPracownikow();
            lp.ShowDialog(this);

            Laduj();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ListaUslug lu = new ListaUslug();
            lu.ShowDialog(this);

            Laduj();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ListaPojazdow lp = new ListaPojazdow();
            lp.ShowDialog(this);

            Laduj();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ListaOsob lo = new ListaOsob();
            lo.ShowDialog(this);

            Laduj();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            ListaNapraw ln = new ListaNapraw();
            ln.ShowDialog(this);

            Laduj();
        }

        private void Laduj()
        {
            dataGridView1.DataSource = null;

            baza = new Baza();
            baza.Pojazd.Load();

            var lista = baza.RejestrNapraw.ToList().Where(x => x.CzyWykonane == false).Select(x =>
            new {
                x.Id,
                Klient = baza.Osoba.FirstOrDefault(o => o.Id == baza.Pojazd.FirstOrDefault(p => p.Id == x.PojazdId).WlascicielId).Nazwa,
                Pojazd = baza.Pojazd.FirstOrDefault(p => p.Id == x.PojazdId).Marka + " " + baza.Pojazd.FirstOrDefault(p => p.Id == x.PojazdId).Model + " " + baza.Pojazd.FirstOrDefault(p => p.Id == x.PojazdId).NrRej,
                Przyjecie = x.DataPrzyjecia
            }).ToList();

            dataGridView1.DataSource = lista;

            if (dataGridView1.Columns["Id"] != null)
                dataGridView1.Columns["Id"].Visible = false;
        }
    }
}
