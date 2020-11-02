using api.layer.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Threading.Tasks;

namespace api.layer.DataAccessLayer
{
    public class GitActionsDAO: IGitActionsDAO
    {

        public async Task<bool> SavePullRequestDetails(PullRequestEntity pullRequestEntity)
        {
            using (SqlConnection sql = new SqlConnection(@"Data Source=MUM02L7746\SQLEXPRESS;Initial Catalog=TODO;Integrated Security=False;Persist Security Info=False;User ID=sa;Password=Password123"))
            {
                try
                {
                    using (SqlCommand cmd = new SqlCommand("SP_Code_Review", sql))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@PRId", pullRequestEntity.number));
                        cmd.Parameters.Add(new SqlParameter("@Action", pullRequestEntity.action));
                        cmd.Parameters.Add(new SqlParameter("@UserId", pullRequestEntity.userid));
                        cmd.Parameters.Add(new SqlParameter("@Bugs", 0));
                        cmd.Parameters.Add(new SqlParameter("@CodeSmells", 0));
                        cmd.Parameters.Add(new SqlParameter("@SecurityHotspots", 0));
                        cmd.Parameters.Add(new SqlParameter("@CoverageInfo", string.Empty));
                        cmd.Parameters.Add(new SqlParameter("@DuplicationInfo", string.Empty));
                        cmd.Parameters.Add(new SqlParameter("@PRCreatedTime", pullRequestEntity.created_at));
                        cmd.Parameters.Add(new SqlParameter("@PRUpdatedTime", pullRequestEntity.updated_at));
                        await sql.OpenAsync();
                        await cmd.ExecuteNonQueryAsync();
                        return true;
                    }
                }
                catch(Exception ex)
                {
                    throw ex;
                }
            }
        }

        public async Task<bool> SaveSonarDetails(int? PRId, Dictionary<string, dynamic> SonarMetic)
        {
            using (SqlConnection sql = new SqlConnection(@"Data Source=MUM02L7746\SQLEXPRESS;Initial Catalog=TODO;Integrated Security=False;Persist Security Info=False;User ID=sa;Password=Password123"))
            {
                try
                {
                    using (SqlCommand cmd = new SqlCommand("SP_Sonar_Update", sql))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@PRId", PRId));

                        foreach(var item in SonarMetic)
                        {
                            cmd.Parameters.Add(new SqlParameter( "@" + item.Key, item.Value));
                        }

                        await sql.OpenAsync();
                        await cmd.ExecuteNonQueryAsync();
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public async Task<List<RatingEntity>> FetchRaitingReport()
        {
            using (SqlConnection sql = new SqlConnection(@"Data Source=MUM02L7746\SQLEXPRESS;Initial Catalog=TODO;Integrated Security=False;Persist Security Info=False;User ID=sa;Password=Password123"))
            {
                try
                {
                    DataSet ds = new DataSet("Rating Report");
                    using (SqlCommand cmd = new SqlCommand("SP_Fetch_Reports", sql))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        SqlDataAdapter da = new SqlDataAdapter();
                        da.SelectCommand = cmd;
                        da.Fill(ds);
                        if (ds.Tables.Count > 0)
                            return BindList<RatingEntity>(ds.Tables["Table"]);

                        return new List<RatingEntity>();
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public static List<T> BindList<T>(DataTable dt)
        {
            var fields = typeof(T).GetProperties();

            List<T> lst = new List<T>();

            foreach (DataRow dr in dt.Rows)
            {
                // Create the object of T
                var ob = Activator.CreateInstance<T>();

                foreach (var fieldInfo in fields)
                {
                    foreach (DataColumn dc in dt.Columns)
                    {
                        // Matching the columns with fields
                        if (fieldInfo.Name == dc.ColumnName)
                        {
                            Type type = fieldInfo.PropertyType;

                            // Get the value from the datatable cell
                            object value = GetValue(dr[dc.ColumnName], type);

                            // Set the value into the object
                            fieldInfo.SetValue(ob, value);
                            break;
                        }
                    }
                }

                lst.Add(ob);
            }

            return lst;
        }

        static object GetValue(object ob, Type targetType)
        {
            if (targetType == null)
            {
                return null;
            }
            else if (targetType == typeof(String))
            {
                return ob + "";
            }
            else if (targetType == typeof(int))
            {
                int i = 0;
                int.TryParse(ob + "", out i);
                return i;
            }
            else if (targetType == typeof(short))
            {
                short i = 0;
                short.TryParse(ob + "", out i);
                return i;
            }
            else if (targetType == typeof(long))
            {
                long i = 0;
                long.TryParse(ob + "", out i);
                return i;
            }
            else if (targetType == typeof(ushort))
            {
                ushort i = 0;
                ushort.TryParse(ob + "", out i);
                return i;
            }
            else if (targetType == typeof(uint))
            {
                uint i = 0;
                uint.TryParse(ob + "", out i);
                return i;
            }
            else if (targetType == typeof(ulong))
            {
                ulong i = 0;
                ulong.TryParse(ob + "", out i);
                return i;
            }
            else if (targetType == typeof(double))
            {
                double i = 0;
                double.TryParse(ob + "", out i);
                return i;
            }
            else if (targetType == typeof(DateTime))
            {
                // do the parsing here...
            }
            else if (targetType == typeof(bool))
            {
                // do the parsing here...
            }
            else if (targetType == typeof(decimal))
            {
                // do the parsing here...
            }
            else if (targetType == typeof(float))
            {
                // do the parsing here...
            }
            else if (targetType == typeof(byte))
            {
                // do the parsing here...
            }
            else if (targetType == typeof(sbyte))
            {
                // do the parsing here...
            }
        
            return ob;
        }

    }
}
