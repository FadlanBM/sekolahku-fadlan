using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sekolahku_jude.Forms
{
    public partial class BrowserDataForm<T> : Form
    {
        public string ResultValue { get; private set; }
        public BrowserDataForm(IEnumerable<T> listData,Form form)
        {
           this.MdiParent = form;
            InitializeComponent();
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.Rows.Clear();
            var binding = new BindingSource();
            binding.DataSource = listData.ToList();
            dataGridView1.DataSource = binding;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
            if (e.ColumnIndex==0)
            {
                var nama = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                DialogResult dialog= MessageBox.Show(null, "Apakah anda ingin memilih guru " + nama, "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (DialogResult.Yes == dialog) { 
                ResultValue = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                DialogResult = DialogResult.OK;
                  this.Close();
                }                
            }
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode==Keys.Escape)               
            {
                DialogResult= DialogResult.Cancel;
                this.Close();
            }
        }
    }
}
