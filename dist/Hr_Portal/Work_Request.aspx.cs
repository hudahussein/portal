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

public partial class _Work_Request : basePageSessionExpire
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
	
	 // insert to HR_DaysOFF_Requests
	
   [WebMethod]

    public static string NewHR_DaysOFF_Requests(HR_DaysOFF_Requests HR_DayOFF_Request)

    {

        string config = Convert.ToString(ConfigurationManager.ConnectionStrings["dbcon"]);

        List<HR_DaysOFF_Requests> HR_DaysOFF_Requests = new List<HR_DaysOFF_Requests>();

        SqlConnection con = new SqlConnection(config);

        con.Open();

        using (SqlCommand cmd = new SqlCommand("insert into HR_DaysOFF_Requests (Emp_id,Date_From,Date_To,Requester_Comment,Status,Dept_id) values (@Emp_id,@Date_From,@Date_To,@Requester_Comment,@Status,@Dept_id)", con))
                    {
                        cmd.Parameters.Add("@Emp_id", SqlDbType.Int).Value = 1;
						cmd.Parameters.Add("@Date_From", SqlDbType.VarChar).Value = HR_DayOFF_Request.Date_From;
						cmd.Parameters.Add("@Date_To", SqlDbType.VarChar).Value = HR_DayOFF_Request.Date_To;
						cmd.Parameters.Add("@Requester_Comment", SqlDbType.VarChar).Value = HR_DayOFF_Request.Requester_Comment;
						cmd.Parameters.Add("@Status", SqlDbType.Int).Value = HR_DayOFF_Request.Status;
			            cmd.Parameters.Add("@Dept_id", SqlDbType.Int).Value = 1;
			
                    	cmd.ExecuteNonQuery();
                  }

            con.Close();
		
    return JsonConvert.SerializeObject(HR_DaysOFF_Requests);
    }
        public class HR_DaysOFF_Requests

    {
        public int? Emp_id { get; set; }
		public string Date_From { get; set; }
		public string Date_To { get; set; }
		public string Requester_Comment { get; set; }
		public int Status { get; set; }
      
    }	
	 
	// get HR_DaysOFF_Requests
	
	[WebMethod]
    public static string getrequestsData(HR_DaysOFF_Requests user)

    {
        string config = Convert.ToString(ConfigurationManager.ConnectionStrings["dbcon"]);

        List<HR_DaysOFF_Requests> HR_DaysOFF_Requests = new List<HR_DaysOFF_Requests>();
        List<requests> requests = new List<requests>();

        SqlConnection con = new SqlConnection(config);

        con.Open();

        using (SqlCommand cmd = new SqlCommand("Select * from HR_DaysOFF_Requests where Emp_id = @Requester_id", con))

        {
			cmd.Parameters.Add("@Requester_id", SqlDbType.Int).Value = user.Emp_id;

            SqlDataReader idr = cmd.ExecuteReader();

            if (idr.HasRows)

            {

                requests = populaterequestsLisst(idr, con);

            }

        }

        con.Close();

        return JsonConvert.SerializeObject(requests);

    }

    public static List<requests> populaterequestsLisst(SqlDataReader idr, SqlConnection con)

    {

        List<requests> requestsI = new List<requests>();

        while (idr.Read())

        {

            requestsI.Add(new requests

            {
				id         = idr["id"] != DBNull.Value ? Convert.ToInt32(idr["id"]) : 0,
			    Status     = idr["Status"] != DBNull.Value ? Convert.ToInt32(idr["Status"]) : 0,
				Date_From  = idr["Date_From"] != DBNull.Value ? Convert.ToString(idr["Date_From"]) : String.Empty,
			    Canceled   = idr["Canceled"] != DBNull.Value ? Convert.ToBoolean(idr["Canceled"]) : false
            });

        }

        return requestsI;

    }

	public class requests
		
    {
		public int? id { get; set; }
      	public int? Status { get; set; }
        public string Date_From { get; set; }
        public bool Canceled { get; set; }
    }
	
	// delete Work Request
	
	[WebMethod]

	public static string updateHide(HideDetails detail)

	{
		string config = Convert.ToString(ConfigurationManager.ConnectionStrings["dbcon"]);

		List<HideDetails> HideDetails = new List<HideDetails>();

		SqlConnection con = new SqlConnection(config);

		con.Open();

		        using (SqlCommand cmd = new SqlCommand("update HR_DaysOFF_Requests set Canceled= @Canceled where id = @id;", con))

        {

			cmd.Parameters.AddWithValue("@id", detail.id);
			cmd.Parameters.AddWithValue("@Canceled", detail.Canceled);
			cmd.ExecuteNonQuery();
        }

		con.Close();

		return JsonConvert.SerializeObject(HideDetails);
	}

	public static List<HideDetails> populateHideDetailsLisst(SqlDataReader idr, SqlConnection con)

	{

		List<HideDetails> HideDetailsI = new List<HideDetails>();

		while (idr.Read())

		{

			HideDetailsI.Add(new HideDetails

			{
			   Canceled     = Convert.ToBoolean(idr["Canceled"]),
			    id          = Convert.ToInt32(idr["id"])
			});
		}
		return HideDetailsI;
	}
	
	public class HideDetails

	{
		public Boolean?  Canceled { get; set; }
		public int?  id { get; set; }

	}
	
	
}
	