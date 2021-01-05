using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using Newtonsoft.Json;
using System.Configuration;

public partial class Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    // get HR_DaysOFF_Requests
	
	[WebMethod]
    public static string login(auth user)

    {
        string config = Convert.ToString(ConfigurationManager.ConnectionStrings["dbcon"]);

        List<auth> auth = new List<auth>();
        List<info> info = new List<info>();

        SqlConnection con = new SqlConnection(config);

        con.Open();

        using (SqlCommand cmd = new SqlCommand("Select * from adminusers where user_name = @user_name and pass_word = @pass_word", con))

        {
			cmd.Parameters.Add("@user_name", SqlDbType.VarChar).Value = user.user_name;
			cmd.Parameters.Add("@pass_word", SqlDbType.VarChar).Value = user.pass_word;

            SqlDataReader idr = cmd.ExecuteReader();

            if (idr.HasRows)

            {
                info = populateinfoLisst(idr, con);
                con.Close();
                return JsonConvert.SerializeObject(info);
            } else {
                return "false";
            }

        }

    }

    public static List<info> populateinfoLisst(SqlDataReader idr, SqlConnection con)

    {

        List<info> infoI = new List<info>();

        while (idr.Read())

        {

            infoI.Add(new info

            {
				Emp_id    = idr["Emp_id"] != DBNull.Value ? Convert.ToInt32(idr["Emp_id"]) : 0,
			    FullName  = idr["FullName"] != DBNull.Value ? Convert.ToString(idr["FullName"]) : String.Empty,
				Email     = idr["Email"] != DBNull.Value ? Convert.ToString(idr["Email"]) : String.Empty,
			    Dept_id   = idr["Dept_id"] != DBNull.Value ? Convert.ToInt32(idr["Dept_id"]) : 0
            });

        }

        return infoI;

    }

	public class auth
		
    {
        public string user_name { get; set; }
        public string pass_word { get; set; }
    }

    	public class info
		
    {
		public int? Emp_id { get; set; }
      	public string FullName { get; set; }
        public string Email { get; set; }
        public int? Dept_id { get; set; }
    }
}


