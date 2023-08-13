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
    public class MapelDal
    {
        private string _conn;
        public MapelDal() {
            _conn = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }

        public void insert(MapelModel mapel) {

            const string sql = @"insert into tb_mapel values (@Mapel_id,@Mapel_Name)";

            var dp = new DynamicParameters();
            dp.AddParam("@Mapel_Id", mapel.Mapel_Id, System.Data.SqlDbType.VarChar);
            dp.AddParam("@Mapel_Name", mapel.Mapel_Name, System.Data.SqlDbType.VarChar);
            using (var conn=new SqlConnection(_conn))
            {
                conn.Execute(sql,dp);
            }
        }

        public void update(MapelModel mapel)
        {
            const string sql = @"update tb_mapel set mapel_name = @MapelName where mapel_id=@MapelId";

            var dp=new DynamicParameters();
            dp.AddParam("@MapelId", mapel.Mapel_Id, System.Data.SqlDbType.VarChar);
            dp.AddParam("@MapelName", mapel.Mapel_Name, System.Data.SqlDbType.VarChar);

            using (var conn=new SqlConnection(_conn))
            {
                conn.Execute(sql, dp);
            }
        }

        public void delete(string id)
        {
            const string sql = @"delete from tb_mapel where mapel_id=@MapelId";

            var dp = new DynamicParameters();
            dp.AddParam("@MapelId", id, System.Data.SqlDbType.VarChar);

            using (var conn=new SqlConnection(_conn))
            {
                conn.Execute(sql, dp);
            }
        }

        public MapelModel GetData(string id) {

            const string sql = @"select mapel_id As Mapel_Id,mapel_name As Mapel_Name from tb_mapel where mapel_id=@MapelId";

            var dp= new DynamicParameters();
            dp.AddParam("@MapelId", id, System.Data.SqlDbType.VarChar);
            using (var conn =new SqlConnection(_conn))
            {
                return conn.ReadSingle<MapelModel>(sql, dp);
            }
        }

        public IEnumerable<MapelModel> ListData()
        {
            const string sql = @"select mapel_id AS Mapel_Id , mapel_name AS Mapel_Name from tb_mapel";

            using (var conn=new SqlConnection(_conn))
            {
                return conn.Read<MapelModel>(sql);
            }
        }
    }
}
