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
    public partial class ListaUslug : Form
    {
        Baza baza = new Baza();

        bool Selection { get; set; }

        public int UslugaId { get; set; }

        public ListaUslug(bool selection = false)
        {
            InitializeComponent();

            Selection = selection;

            button2.Enabled = !selection;
        }

        private void ListaPracownikow_Shown(object sender, EventArgs e)
        {
            Laduj();
        }

        private void Laduj()
        {
            dataGridView1.DataSource = null;

            baza = new Baza();
            baza.Usluga.Load();

            List<Usluga> lista = baza.Usluga.ToList();

            dataGridView1.DataSource = lista;

            if(dataGridView1.Columns["Id"] != null)
                dataGridView1.Columns["Id"].Visible = false;
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int id;
            if(int.TryParse(dataGridView1.Rows[e.RowIndex].Cells["Id"].Value.ToString(), out id))
            {
                if (Selection)
                {
                    UslugaId = id;

                    DialogResult = DialogResult.OK;
                }
                else
                {
                    EdycjaUslugi ep = new EdycjaUslugi(id);
                    if (ep.ShowDialog(this) == DialogResult.OK)
                    {
                        Laduj();
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            EdycjaUslugi ep = new EdycjaUslugi();
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
