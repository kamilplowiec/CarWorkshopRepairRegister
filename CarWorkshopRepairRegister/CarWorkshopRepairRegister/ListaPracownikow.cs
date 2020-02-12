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
    public partial class ListaPracownikow : Form
    {
        Baza baza = new Baza();

        public ListaPracownikow()
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
            baza.Pracownik.Load();
            
            List<Pracownik> lista = baza.Pracownik.ToList();

            dataGridView1.DataSource = lista;

            if(dataGridView1.Columns["Id"] != null)
                dataGridView1.Columns["Id"].Visible = false;

            dataGridView1.Refresh();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int pracownikId;
            if(int.TryParse(dataGridView1.Rows[e.RowIndex].Cells["Id"].Value.ToString(), out pracownikId))
            {
                EdycjaPracownika ep = new EdycjaPracownika(pracownikId);
                if(ep.ShowDialog(this) == DialogResult.OK)
                {
                    Laduj();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            EdycjaPracownika ep = new EdycjaPracownika();
            if(ep.ShowDialog(this) == DialogResult.OK)
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
