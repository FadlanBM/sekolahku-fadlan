using sekolahku_jude.DataAkses;
using sekolahku_jude.Models;
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
    public partial class GuruForm : Form
    {
        AppDbContextDataContext context;
        private GuruDal dal;
        public GuruForm()
        {
            context = new AppDbContextDataContext();
            dal = new GuruDal();
            InitializeComponent();
        }

        private void loadData() { 
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.AllowUserToAddRows = false;
         
            var data =(from c in context.tb_gurus
                       orderby c.guru_id ascending
                       select c).ToList();
            var binding = new BindingSource();
            binding.DataSource = data;
            dataGridView1.DataSource = binding;

        }

        private void clearForm() {
            tb_guruId.Text = "";
            tb_guruName.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (tb_guruId.Text == "" || tb_guruName.Text =="")
            {
                MessageBox.Show(null, "Form Tidak Boleh kosong", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (tb_guruId.Text.Length>3)
            {
                MessageBox.Show(null, "Input harus di bawah 3 kata", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (tb_guruName.Text.Length>30)
            {
                MessageBox.Show(null, "Input harus di bawah 30 kata", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var guru = new GuruModel
            {
                GuruId = tb_guruId.Text,
                GuruName = tb_guruName.Text,
            };

            var gurudb = dal.getData(guru.GuruId);
            if (gurudb != null)
            {
                dal.Update(guru);
                MessageBox.Show(null, "Berhasil Update data","Information",MessageBoxButtons.OK,MessageBoxIcon.Information);
                loadData();
               clearForm();
            }
            else
            {
                dal.Insert(guru);
                MessageBox.Show(null, "Berhasil Insert data", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                loadData();
                clearForm();
            }
        }
        
        private void button3_Click(object sender, EventArgs e)
        {
            
        }

        private void GuruForm_Load(object sender, EventArgs e)
        {
            loadData();
        }
    }
}
