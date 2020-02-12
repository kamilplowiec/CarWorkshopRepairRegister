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
    public partial class ListaNapraw : Form
    {
        Baza baza = new Baza();

        public ListaNapraw()
        {
            InitializeComponent();
        }

        private void ListaPracownikow_Shown(object sender, EventArgs e)
        {
            Laduj();
        }

        private void Laduj()
        {
            dataGridView1.DataSource = null;

            baza = new Baza();
            baza.Pojazd.Load();

            var lista = baza.RejestrNapraw.ToList().Select(x =>
            new {
                x.Id,
                Klient = baza.Osoba.FirstOrDefault(o => o.Id == baza.Pojazd.FirstOrDefault(p => p.Id == x.PojazdId).WlascicielId).Nazwa,
                Pojazd = baza.Pojazd.FirstOrDefault(p => p.Id == x.PojazdId).Marka + " " + baza.Pojazd.FirstOrDefault(p => p.Id == x.PojazdId).Model + " " + baza.Pojazd.FirstOrDefault(p => p.Id == x.PojazdId).NrRej,
                Przyjecie = x.DataPrzyjecia,
                Odbior = (x.DataOdbioru.HasValue ? x.DataOdbioru.ToString() : ""),
                Wykonane = x.CzyWykonane,
                Przyjmujacy = baza.Pracownik.FirstOrDefault(pr => pr.Id == x.PrzyjmujacyPracownikId).Nazwa,
                Oddajacy = (x.OddajacyPracownikId.HasValue ? baza.Pracownik.FirstOrDefault(pr => pr.Id == x.OddajacyPracownikId).Nazwa : ""),
                Kwota = baza.RejestrNaprawUslugi.Where(r => r.RejestrNaprawId == x.Id).Sum(r => baza.Usluga.Where(u => u.Id == r.UslugaId).Sum(u => u.Cena))
            }).ToList();

            dataGridView1.DataSource = lista;

            if(dataGridView1.Columns["Id"] != null)
                dataGridView1.Columns["Id"].Visible = false;
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int id;
            if(int.TryParse(dataGridView1.Rows[e.RowIndex].Cells["Id"].Value.ToString(), out id))
            {
                EdycjaNaprawy ep = new EdycjaNaprawy(id);
                if (ep.ShowDialog(this) == DialogResult.OK)
                {
                    Laduj();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            EdycjaNaprawy ep = new EdycjaNaprawy();
            if (ep.ShowDialog(this) == DialogResult.OK)
            {
                Laduj();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
