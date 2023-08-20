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
    public partial class KelasForm : Form
    {
        private readonly KelasDal dal;
        private readonly GuruDal guru;
        private string idGuru;
        public KelasForm()
        {
            guru = new GuruDal();
            dal= new KelasDal();    
            InitializeComponent();
            idGuru = "";
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            var showdata=guru.ListData().ToList()?.ToList()?? new List<GuruModel>();
            var fBroserData = new BrowserDataForm<GuruModel>(showdata,this.MdiParent);
            fBroserData.StartPosition = FormStartPosition.CenterParent;
            fBroserData.FormClosing += (object dasa, FormClosingEventArgs ada) =>
            {
                if (DialogResult.OK == fBroserData.DialogResult)
                {
                    tb_wali_id.Text = fBroserData.ResultValue;
                    tb_waliname.Text = guru.getData(fBroserData.ResultValue).GuruName;
                }
            };
            fBroserData.Show();                        
        }

        private void KelasForm_Load(object sender, EventArgs e)
        {
            if (dal.ToListAll()!=null)
            {
            loadData();                
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (tb_kelasName.Text == "" || tb_kelasId.Text == "" || tb_waliname.Text == "" || tb_wali_id.Text == "")
            {
                MessageBox.Show(null, "Form belum terisi semua", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (tb_kelasId.Text.Length>3)
            {
                MessageBox.Show(null,"Kelas Id tidak boleh melebihi 3 digit","Warning",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                return;
            }
            if (tb_kelasName.Text.Length>16)
            {
                MessageBox.Show(null, "Kelas name tidak boleh melebihi 16 digit", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var kelas = new KelasModel() { 
                kelas_id = tb_kelasId.Text,
                kelas_name = tb_kelasName.Text,
                waliKelas_id=tb_wali_id.Text,
            };

            if (idGuru=="")
            {
                dal.Insert(kelas);
                MessageBox.Show("Berhasil Melakukan input", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                loadData();
                clearData();
                return;
            }
            else
            {
                dal.Update(kelas);
                MessageBox.Show("Berhasil Melakukan Update", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                loadData();
                clearData();
                return;
            }

        }

        private void loadData() { 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.Rows.Clear();
            var data=dal.ToListAll().ToList();
            int i=0;
            foreach (var item in data)
            {
                i++;
                var num = dataGridView1.Rows.Add();
                dataGridView1.Rows[num].Cells[0].Value = i;
                dataGridView1.Rows[num].Cells[1].Value = item.kelas_id;
                dataGridView1.Rows[num].Cells[2].Value = item.kelas_name;
                dataGridView1.Rows[num].Cells[3].Value = item.waliKelas_id;
                dataGridView1.Rows[num].Cells[4].Value = item.waliKelas_name;
            }
        }

        private void tb_wali_id_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void tb_wali_id_Validated(object sender, EventArgs e)
        {
            var data = guru.getData(tb_wali_id.Text);
            if (data == null)
            {
                tb_waliname.Text=string.Empty;
                return;
            }
            tb_waliname.Text = data.GuruName;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var id=string.Empty;
            if (e.ColumnIndex==5)
            {
                id = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                var data=dal.getKelas(id);
                tb_kelasId.Text = data.kelas_id;
                tb_kelasName.Text = data.kelas_name;
                tb_wali_id.Text=data.waliKelas_id;
                tb_waliname.Text=data.waliKelas_name;
                idGuru = id;
            }
        }

        private void clearData() {
            tb_kelasId.Text = "";
            tb_kelasName.Text = "";
            tb_waliname.Text = "";
            tb_wali_id.Text = "";
            idGuru = "";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            clearData();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (tb_kelasId.Text=="")
            {
                MessageBox.Show(null, "Belum ada data yang di pilih", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            DialogResult dialog = MessageBox.Show(null, "Apakah anda ingin menghapus data ini?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (DialogResult.Yes == dialog)
            {
                dal.delete(tb_kelasId.Text);
                MessageBox.Show(null, "Berhasil di delete", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                clearData();
                loadData();
                return;
            }
        }

        private void tb_wali_id_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                DialogResult dialog = MessageBox.Show(null, "Apakah anda ingin menghapus data id guru", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (DialogResult.Yes == dialog)
                {
                    tb_wali_id.Text = "";
                    tb_waliname.Text = "";
                }
            }
        }

        private void KelasForm_KeyDown(object sender, KeyEventArgs e)
        {
           
        }
    }
}
