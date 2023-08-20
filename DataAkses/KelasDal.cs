using Dapper;
using Nuna.Lib.DataAccessHelper;
using sekolahku_jude.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sekolahku_jude.DataAkses
{  
    public class KelasDal
    {
        private string conn;
        public KelasDal() {
            conn = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }

        public void Insert(KelasModel model) {
            const string sql = @"insert into tb_kelas values(@kelas_id,@kelas_name,@waliKelas_id)";

            var dp = new DynamicParameters();
            dp.AddParam("kelas_id", model.kelas_id, System.Data.SqlDbType.VarChar);
            dp.AddParam("kelas_name", model.kelas_name, System.Data.SqlDbType.VarChar);
            dp.AddParam("waliKelas_id", model.waliKelas_id, System.Data.SqlDbType.VarChar);

            using (var con=new SqlConnection(conn))
            {
                con.Execute(sql, dp);
            }
        }

        public void Update(KelasModel model) {
            const string sql = @"update tb_kelas 
                               set kelas_name=@kelas_name,waliKelas_id=@waliKelas_id
                               where kelas_id=@kelas_id";
            var dp = new DynamicParameters();
            dp.AddParam("kelas_id", model.kelas_id, System.Data.SqlDbType.VarChar);
            dp.AddParam("kelas_name", model.kelas_name, System.Data.SqlDbType.VarChar);
            dp.AddParam("waliKelas_id", model.waliKelas_id, System.Data.SqlDbType.VarChar);

            using (var con =new SqlConnection(conn))
            {
                con.Execute(sql, dp);
            }
        }

        public void delete(string id) {
            const string sql = @"delete from tb_kelas where kelas_id=@kelas_id";

            var dp = new DynamicParameters();
            dp.AddParam("kelas_id", id, System.Data.SqlDbType.VarChar);

            using (var con=new SqlConnection(conn))
            {
                con.Execute(sql, dp);
            }
        }

        public KelasModel getKelas(string id) {
            const string sql = @"select kelas_id,kelas_name ,waliKelas_id,
                                ISNULL(guru_name,'')waliKelas_name
                                from tb_kelas
                                left join tb_guru on waliKelas_id=guru_id
                                where kelas_id=@kelas_id";

            var dp = new DynamicParameters();
            dp.AddParam("kelas_id", id, System.Data.SqlDbType.VarChar);
            using (var con=new SqlConnection(conn))
            {
                 return  con.ReadSingle<KelasModel>(sql, dp);
            }
        }
        public IEnumerable<KelasModel> ToListAll() {
            const string sql = @"select kelas_id,kelas_name,waliKelas_id,
                                ISNULL(guru_name,'')waliKelas_name
                                from tb_kelas
                                left join tb_guru on waliKelas_id=guru_id";
            using (var con =new SqlConnection(conn))
            {
                return con.Read<KelasModel>(sql);
            }
        }
    }
}
