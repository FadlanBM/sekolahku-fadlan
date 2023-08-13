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
    public partial class MapelForm : Form
    {
        private MapelDal dal;
        public MapelForm()
        {
            dal= new MapelDal();
            InitializeComponent();
        }

        private void MapelForm_Load(object sender, EventArgs e)
        {
            loadData();
        }
        private void clearData() { 
            tb_mapelid.Text="";
            tb_mapelName.Text = "";
            tb_mapelid.Enabled =true;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (tb_mapelid.Text==""||tb_mapelName.Text=="")
            {
                MessageBox.Show(null, "Form belum terisi semua", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var data=dal.GetData(tb_mapelid.Text);
            var mapel = new MapelModel()
            {
                Mapel_Id = tb_mapelid.Text,
                Mapel_Name = tb_mapelName.Text,
            };
            if (data==null)
            {          
                dal.insert(mapel);
                MessageBox.Show( null, "Berhasil Input data", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information) ;
                clearData();
               
            }
            else
            {
                dal.update(mapel);
                MessageBox.Show( null, "Berhasil Update data", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                clearData();
            }
            loadData();
            return;
        }

        public void loadData() { 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.RowHeadersVisible= false;
            if (dal.ListData()?.ToList()!=null)
            {
                var data = dal.ListData().ToList();
                dataGridView1.Rows.Clear();
                foreach (var item in data)
                {
                    var num = dataGridView1.Rows.Add();
                    dataGridView1.Rows[num].Cells[0].Value = item.Mapel_Id;
                    dataGridView1.Rows[num].Cells[1].Value = item.Mapel_Name;
                }
            }
           
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var id = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            if (e.ColumnIndex==2)
            {
                var data=dal.GetData(id);
                tb_mapelid.Text = data.Mapel_Id;
                tb_mapelName.Text= data.Mapel_Name;
                tb_mapelid.Enabled= false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (tb_mapelid.Text=="")
            {
                var dialog = MessageBox.Show(null, "Belum ada data yang di pilih", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var data = dal.GetData(tb_mapelid.Text);
            if (data != null) {
            var dialog = MessageBox.Show(null, "Apakah Anda yakin ingin menghapus data ini ?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dialog == DialogResult.Yes)
                {
                    dal.delete(tb_mapelid.Text);
                    MessageBox.Show(null, "Berhasil menghapus data", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    clearData();
                    loadData();
                    return;
                }
            }
            else
            {
                MessageBox.Show(null, "Data tidak di temukan", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            clearData();    
        }
    }
}
