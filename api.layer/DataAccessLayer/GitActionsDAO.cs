using api.layer.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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

        public async Task<string> FetchRaitingReport()
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
                        if(ds.Tables.Count > 0)
                            return JsonConvert.SerializeObject(ds.Tables["Table"], Formatting.None);

                        return string.Empty;
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

    }
}
