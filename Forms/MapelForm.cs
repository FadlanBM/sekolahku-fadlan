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

        AppDbContextDataContext context;
        public MapelForm()
        {
            context = new AppDbContextDataContext();
            InitializeComponent();
        }

        private void clearForm() {
            tb_mapelId.Text = "";
            tb_mapelName.Text = "";
            tb_mapelId.Enabled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (tb_mapelId.Text==""||tb_mapelName.Text=="")
            {
                MessageBox.Show(null, "Form harus lengkap", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (conndata(tb_mapelId.Text)==null)
            {
                tb_mapel mapel = new tb_mapel();
                mapel.mapel_id = tb_mapelId.Text;
                mapel.mapel_name = tb_mapelName.Text;
                context.tb_mapels.InsertOnSubmit(mapel);
                context.SubmitChanges();
                MessageBox.Show(null, "Berhasil input data mapel", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                clearForm();
                loaddata();
                return;
            }
            else
            {
                var con=conndata(tb_mapelId.Text);
                con.mapel_id= tb_mapelId.Text;
                con.mapel_name= tb_mapelName.Text;
                context.SubmitChanges();
                MessageBox.Show(null, "Berhasil update data mapel", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                clearForm() ;
                loaddata();
                return;
            }

        }

        private void loaddata() { 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.Rows.Clear();
            int i = 0;
            var data=(from m in context.tb_mapels
                      orderby m.mapel_name ascending
                      select new { 
                          id=m.mapel_id,
                        name=m.mapel_name
                      }).ToList();

            foreach (var item in data)
            {
                i++;
                var num = dataGridView1.Rows.Add();
                dataGridView1.Rows[num].Cells[0].Value = i;
                dataGridView1.Rows[num].Cells[1].Value = item.id;
                dataGridView1.Rows[num].Cells[2].Value = item.name;
            }
        }

        private void MapelForm_Load(object sender, EventArgs e)
        {
            loaddata();
           
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var id = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            if (e.ColumnIndex==3)
            {                
                tb_mapelId.Text = conndata(id).mapel_id;
                tb_mapelName.Text=conndata(id).mapel_name;
                tb_mapelId.Enabled = false;
            }
        }

        private tb_mapel conndata(string id) { 
            var data=context.tb_mapels.Where(m=>m.mapel_id==id).FirstOrDefault();
            return data;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            clearForm();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult dialog = MessageBox.Show(null, "Apakah anda yakin ingin menghapus data ini ?", "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (dialog==DialogResult.Yes)
            {
                context.tb_mapels.DeleteOnSubmit(conndata(tb_mapelId.Text));
                context.SubmitChanges();
                MessageBox.Show(null, "Berhasil Menghapus data", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                loaddata();
                clearForm();
                return;
            }
        }
    }
}
