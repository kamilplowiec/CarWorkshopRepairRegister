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
    public partial class EdycjaNaprawy : Form
    {
        Baza baza = new Baza();

        RejestrNapraw p = new RejestrNapraw();

        DataTable uslugiNaprawy;

        public EdycjaNaprawy(int id = 0)
        {
            InitializeComponent();

            LadujPojazdy();

            if (id > 0)
            {
                p = baza.RejestrNapraw.FirstOrDefault(x => x.Id == id);

                button3.Visible = true;
            }

            uslugiNaprawy = ConvertToDataTable(
                    baza.RejestrNaprawUslugi.ToList().Where(x => x.RejestrNaprawId == p.Id).Select(x =>
                         new
                         {
                             x.Id,
                             UslugaId = x.UslugaId,
                             Nazwa = baza.Usluga.FirstOrDefault(u => u.Id == x.UslugaId).Nazwa
                         }).ToList());
        }

        private void LadujPojazdy()
        {
            comboBox2.Items.Clear();
            baza.Pojazd.Load();
            comboBox2.Items.AddRange(baza.Pojazd.Select(x => x.Marka + " " + x.Model + " " + x.NrRej).ToList().ToArray());
        }

        private void UstawPojazd(string pojazd)
        {
            comboBox2.SelectedItem = pojazd;
        }

        private void LadujUslugiNaprawy()
        {
            dataGridView1.DataSource = uslugiNaprawy;
            if (dataGridView1.Columns["Id"] != null)
                dataGridView1.Columns["Id"].Visible = false;
            if (dataGridView1.Columns["UslugaId"] != null)
                dataGridView1.Columns["UslugaId"].Visible = false;
        }

        private void DodajDoUslug(int uslugaId)
        {
            bool istnieje = dataGridView1.Rows.Cast<DataGridViewRow>().Where(r => r.Cells["UslugaId"].Value.ToString().Equals(uslugaId.ToString())).Count() > 0;

            if (istnieje)
            {
                MessageBox.Show("Taka usługa jest już dodana do naprawy!");
                return;
            }

            var usluga = baza.Usluga.FirstOrDefault(x => x.Id == uslugaId);

            uslugiNaprawy.Rows.Add(0, usluga.Id, usluga.Nazwa);
            dataGridView1.Refresh();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Czy chcesz usunąć naprawę?", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (p.Id > 0)
                {
                    baza.RejestrNapraw.Remove(p);

                    baza.RejestrNaprawUslugi.RemoveRange(baza.RejestrNaprawUslugi.Where(x => x.RejestrNaprawId == p.Id));
                }

                baza.SaveChanges();

                DialogResult = DialogResult.OK;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox2.SelectedItem == null)
            {
                MessageBox.Show("Wybierz pojazd!");
                return;
            }

            var pojazd = baza.Pojazd.FirstOrDefault(x => x.Marka + " " + x.Model + " " + x.NrRej == comboBox2.SelectedItem.ToString());

            if(pojazd == null)
            {
                MessageBox.Show("Wybierz pojazd!");
                return;
            }

            DateTime przyjecie;
            if(!DateTime.TryParse(dateTimePicker1.Value.ToString(), out przyjecie))
            {
                MessageBox.Show("Wprowadź poprawną datę przyjęcia auta do serwisu!");
                return;
            }

            if(dataGridView1.Rows.Count == 0)
            {
                MessageBox.Show("Brak wybranych usług!");
                return;
            }

            DateTime zwrot;
            if (checkBox1.Checked)
            {
                if (!DateTime.TryParse(dateTimePicker2.Value.ToString(), out zwrot))
                {
                    MessageBox.Show("Wprowadź poprawną datę zwrotu auta klientowi!");
                    return;
                }

                if(!p.DataOdbioru.HasValue && zwrot != p.DataOdbioru)
                {
                    p.OddajacyPracownikId = Form1.Zalogowany.Id;
                }

                p.DataOdbioru = zwrot;
            }
            else
            {
                p.OddajacyPracownikId = null;
                p.DataOdbioru = null;
            }

            //zapis

            p.PojazdId = pojazd.Id;

            p.DataPrzyjecia = przyjecie;

            p.CzyWykonane = checkBox2.Checked;

            if (p.Id == 0)
            {
                p.PrzyjmujacyPracownikId = Form1.Zalogowany.Id;

                baza.RejestrNapraw.Add(p);
            }

            baza.SaveChanges();

            //zapis uslug

            if (p.Id > 0)
            {
                baza.RejestrNaprawUslugi.RemoveRange(baza.RejestrNaprawUslugi.Where(x => x.RejestrNaprawId == p.Id));
            }

            foreach (DataGridViewRow row in this.dataGridView1.Rows)
            {
                RejestrNaprawUslugi pz = new RejestrNaprawUslugi();
                pz.RejestrNaprawId = p.Id;
                pz.UslugaId = int.Parse(row.Cells["UslugaId"].Value.ToString());

                baza.RejestrNaprawUslugi.Add(pz);
            }

            baza.SaveChanges();

            DialogResult = DialogResult.OK;
        }

        private void EdycjaPracownika_Shown(object sender, EventArgs e)
        {
            if (p.Id > 0)
            {
                var pojazd = baza.Pojazd.FirstOrDefault(x => x.Id == p.PojazdId);
                if (pojazd != null)
                {
                    UstawPojazd(pojazd.Marka + " " + pojazd.Model + " " + pojazd.NrRej);
                }

                if (p.DataOdbioru.HasValue)
                {
                    checkBox1.Checked = true;
                    dateTimePicker2.Enabled = true;
                    dateTimePicker2.Value = p.DataOdbioru.Value;
                }

                dateTimePicker1.Value = p.DataPrzyjecia;

                checkBox2.Checked = p.CzyWykonane;
            }

            //laduj uslugi

            LadujUslugiNaprawy();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            EdycjaPojazdu ep = new EdycjaPojazdu();
            if (ep.ShowDialog(this) == DialogResult.OK)
            {
                LadujPojazdy();

                UstawPojazd(ep.p.Marka + " " + ep.p.Model + " " + ep.p.NrRej);
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            dateTimePicker2.Enabled = checkBox1.Checked;
        }

        public DataTable ConvertToDataTable<T>(IList<T> data)
        {
            PropertyDescriptorCollection properties =
                TypeDescriptor.GetProperties(typeof(T));

            DataTable table = new DataTable();

            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);

            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }
            return table;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            var selectedRow = dataGridView1.SelectedRows[0];

            if (MessageBox.Show("Czy chcesz usunąć usługę z naprawy?", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                this.dataGridView1.Rows.RemoveAt(selectedRow.Index);
                this.dataGridView1.Refresh();
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            ListaUslug lu = new ListaUslug(true);
            if(lu.ShowDialog(this) == DialogResult.OK)
            {
                DodajDoUslug(lu.UslugaId);
            }
        }
    }
}
