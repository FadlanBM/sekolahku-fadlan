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
        public GuruForm()
        {
            context = new AppDbContextDataContext();
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (tb_guruId.Text == "" || tb_guruname.Text == "")
            {
                MessageBox.Show(null, "Form harus di isi secara lengkap", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var data = context.tb_gurus.Where(g => g.guru_id == tb_guruId.Text).FirstOrDefault();

            if (data == null)
            {
                tb_guru guru = new tb_guru();
                guru.guru_id = tb_guruId.Text;
                guru.guru_name = tb_guruname.Text;
                context.tb_gurus.InsertOnSubmit(guru);
                context.SubmitChanges();
                MessageBox.Show(null, "Berhasil menginput data", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cleareData();
                loadData();
                return;
            }
            else
            {
                data.guru_name = tb_guruname.Text;
                context.SubmitChanges();
                MessageBox.Show(null, "Update data menginput data", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cleareData();
                loadData();
                return;
            }

        }

        private void loadData()
        {
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.Rows.Clear();
            int i = 0;
            var data = (from c in context.tb_gurus
                        orderby c.guru_name ascending
                        select new
                        {
                            guru_id = c.guru_id,
                            guru_name = c.guru_name
                        }).ToList();
            foreach (var item in data)
            {
                var num = dataGridView1.Rows.Add();
                i++;
                dataGridView1.Rows[num].Cells[0].Value = i;
                dataGridView1.Rows[num].Cells[1].Value = item.guru_id;
                dataGridView1.Rows[num].Cells[2].Value = item.guru_name;
            }

        }

        private void GuruForm_Load(object sender, EventArgs e)
        {
            loadData();
        }

        private void cleareData() {
            tb_guruId.Text = "";
            tb_guruname.Text = "";
            tb_guruId.Enabled = true;
            tb_guruname.Enabled = true;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string id = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            if (e.ColumnIndex == 3)
            {
                var data = context.tb_gurus.Where(g => g.guru_id == id).FirstOrDefault();
                tb_guruId.Text = data.guru_id;
                tb_guruname.Text = data.guru_name.ToString();
                tb_guruId.Enabled = false;
            }

        }

        private void disableTB() {

            tb_guruId.Enabled = false;
            tb_guruname.Enabled = false;
        }
        private void enabelTb()
        {
            tb_guruname.Enabled = true;
        }
        private void button3_Click(object sender, EventArgs e)
        {
            cleareData();
        }

        private void button2_Click(object sender, EventArgs e)
        {

            if (tb_guruId.Text!="")
            {
                var data = context.tb_gurus.Where(g => g.guru_id == tb_guruId.Text).FirstOrDefault();                
                disableTB();
                DialogResult dialogResult = MessageBox.Show(null, "Apakah Anda yakin ingin menghapus data ini ?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dialogResult == DialogResult.Yes)
                {
                    context.tb_gurus.DeleteOnSubmit(data);
                    context.SubmitChanges();
                    MessageBox.Show(null, "Berhasil Menghapus data !", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cleareData();
                    loadData();
                }
                else
                {
                    enabelTb();
                }
            }
            else
            {
                    MessageBox.Show(null, "Pilih dulu data yang mau di uodate", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    enabelTb();
            }
        }
    }
}
