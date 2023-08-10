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
        private GuruDal dal;
        private string id;
        public GuruForm()
        {
            dal = new GuruDal();
            InitializeComponent();
        }

        private void loadData() {
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.AllowUserToAddRows = false;   
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.Rows.Clear(); 
            try
            {
                var data = dal.ListData().ToList();
                if (data != null)
                {
                    foreach (var item in data)
                    {
                        var num = dataGridView1.Rows.Add();
                        dataGridView1.Rows[num].Cells[0].Value = item.GuruId;
                        dataGridView1.Rows[num].Cells[1].Value = item.GuruName;
                    }
                }
            }
            catch (Exception)
            {
                return;
            }            

        }

        private void clearForm() {
            tb_guruId.Text = "";
            tb_guruName.Text = "";
            tb_guruId.Enabled = true;
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

            var gurudb = dal.getData(tb_guruId.Text);
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
            clearForm();
        }

        private void GuruForm_Load(object sender, EventArgs e)
        {
            loadData();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            id = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            var data=dal.getData(id);
            if (data != null)
            {
               tb_guruId.Text=data.GuruId;
                tb_guruName.Text=data.GuruName;
                tb_guruId.Enabled = false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (tb_guruId.Text == "")
            {
                MessageBox.Show(null, "Belum ada data yang terpilih", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            DialogResult result= MessageBox.Show(null, "Apakah Anda yakin ingin menghapus data ini", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result==DialogResult.Yes)
            {
                dal.Delete(id);
                MessageBox.Show(null,"Berhasil Menghapus data","Information",MessageBoxButtons.OK,MessageBoxIcon.Information);
                loadData();
                clearForm() ;
            }
        }
    }
}
