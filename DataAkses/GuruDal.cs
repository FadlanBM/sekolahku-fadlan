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
    public class GuruDal
    {
        private string _conn;
        public GuruDal() {
            _conn = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }

        public void Insert(GuruModel model)
        {
            //  QUERY
            const string sql = @"
                INSERT INTO tb_guru VALUES (@GuruId, @GuruName)";

            //  PARAM
            var dp = new DynamicParameters();
            dp.AddParam("@GuruId", model.GuruId, System.Data.SqlDbType.VarChar);
            dp.AddParam("@GuruName", model.GuruName, System.Data.SqlDbType.VarChar);

            //  EXECUTE
            using (var conn = new SqlConnection(_conn))
            {
                conn.Execute(sql, dp);
            }

        }
        public void Update(GuruModel model)
        {
            //  QUERY
            const string sql = @"
                Update 
                tb_guru
                set 
                guru_name=@GuruName
                where 
                guru_id=@GuruId";

            //  PARAM
            var dp = new DynamicParameters();
            dp.AddParam("@GuruId", model.GuruId, System.Data.SqlDbType.VarChar);
            dp.AddParam("@GuruName", model.GuruName, System.Data.SqlDbType.VarChar);

            //  EXECUTE
            using (var conn = new SqlConnection(_conn))
            {
                conn.Execute(sql, dp);
            }
        }

        public void Delete(GuruModel model)
        {
            const string sql = @"Delete Form tb_guru where guru_id=@GuruId";


            var dp = new DynamicParameters();
            dp.AddParam("@GuruID", model.GuruId, System.Data.SqlDbType.VarChar);

            using (var conn = new SqlConnection(_conn))
            {
                conn.Execute(sql, dp);
            }
        }

        public GuruModel getData(string id)
        {
            const string sql = @"select * from tb_guru where guru_id=@GuruId";

            var dp = new DynamicParameters();
            dp.AddParam("@GuruId", id, System.Data.SqlDbType.VarChar);

            using (var conn = new SqlConnection(_conn))
            {
                return conn.ReadSingle<GuruModel>(sql, dp);
            }
        }

        public IEnumerable<GuruModel> ListData()
        {
            const string sql = @"select guru_id, guru_name from tb_guru";

            using (var conn = new SqlConnection(_conn))
            {
                return conn.Read<GuruModel>(sql);
            }
        }
    }
}
