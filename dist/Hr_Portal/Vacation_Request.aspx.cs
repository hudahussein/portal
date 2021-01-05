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

public partial class _Vacation_Request : basePageSessionExpire
{
	public static int NewId = 0;
	public static int Emp_code = 0;
	public static int Type_id = 0;
	
    protected void Page_Load(object sender, EventArgs e)
    {
/*
        string config = Convert.ToString(ConfigurationManager.ConnectionStrings["dbcon"]);

        SqlConnection con = new SqlConnection(config);

        con.Open();

        using (SqlCommand cmd = new SqlCommand("delete from HR_Vacations_Requests", con))
                  {
                    	cmd.ExecuteReader();
                  }

        con.Close();*/
    }
	
	
	// get adminusers
    [WebMethod]
    public static string getadminusersData()

    {
        string config = Convert.ToString(ConfigurationManager.ConnectionStrings["dbcon"]);

        List<adminusers> adminusers = new List<adminusers>();

        SqlConnection con = new SqlConnection(config);

        con.Open();

        using (SqlCommand cmd = new SqlCommand("Select * from adminusers where Dept_id = " + HttpContext.Current.Session["Dept_id"], con))

        {

            SqlDataReader idr = cmd.ExecuteReader();

            if (idr.HasRows)

            {

                adminusers = populateadminusersLisst(idr, con);

            }

        }

        con.Close();

        return JsonConvert.SerializeObject(adminusers);

    }
    public static List<adminusers> populateadminusersLisst(SqlDataReader idr, SqlConnection con)

    {

        List<adminusers> adminusersI = new List<adminusers>();

        while (idr.Read())

        {

            adminusersI.Add(new adminusers
							
            {
				Emp_ID          = idr["Emp_ID"] != DBNull.Value ? Convert.ToInt32(idr["Emp_ID"]) : 0,
                FullName        = idr["FullName"] != DBNull.Value ? Convert.ToString(idr["FullName"]) : String.Empty,
				Branch_name     = idr["Branch_name"] != DBNull.Value ? Convert.ToString(idr["Branch_name"]) : String.Empty,
				Dept_name       = idr["Dept_name"] != DBNull.Value ? Convert.ToString(idr["Dept_name"]) : String.Empty,
            });

        }

        return adminusersI;

    }

    public class adminusers

    {
		public int? Emp_ID { get; set; }     
		public string FullName { get; set; }
		public string Branch_name { get; set; }
		public string Dept_name { get; set; }
    }
	
	// get HR_Vacation_Type
	
	[WebMethod]
    public static string gettypesData()

    {
        string config = Convert.ToString(ConfigurationManager.ConnectionStrings["dbcon"]);

        List<types> types = new List<types>();

        SqlConnection con = new SqlConnection(config);

        con.Open();

        using (SqlCommand cmd = new SqlCommand("Select id,Type_Name from HR_Vacation_Type where Hide <> 'True'", con))

        {

            SqlDataReader idr = cmd.ExecuteReader();

            if (idr.HasRows)

            {

                types = populatetypesLisst(idr, con);

            }

        }

        con.Close();

        return JsonConvert.SerializeObject(types);

    }

    public static List<types> populatetypesLisst(SqlDataReader idr, SqlConnection con)

    {

        List<types> typesI = new List<types>();

        while (idr.Read())

        {

            typesI.Add(new types

            {
			    id         = idr["id"] != DBNull.Value ? Convert.ToInt32(idr["id"]) : 0,
				Type_Name  = idr["Type_Name"] != DBNull.Value ? Convert.ToString(idr["Type_Name"]) : String.Empty
            });

        }

        return typesI;

    }

	public class types
		
    {
      	public int? id { get; set; }
        public string Type_Name { get; set; }
    }
	
	// get work request
	[WebMethod]
    public static string getDaysOffData(daysOff detail)

    {
        string config = Convert.ToString(ConfigurationManager.ConnectionStrings["dbcon"]);

        List<daysOff> daysOff = new List<daysOff>();

        SqlConnection con = new SqlConnection(config);

        con.Open();

        using (SqlCommand cmd = new SqlCommand("Select * from HR_DaysOFF_Requests where Emp_id = @Emp_id and ExpirationDate > GETDATE() and Canceled <> 'True' and Status = 4 and Dept_id =" + HttpContext.Current.Session["Dept_id"], con))

        {
			cmd.Parameters.AddWithValue("@Emp_id", detail.Emp_id);
            SqlDataReader idr = cmd.ExecuteReader();

            if (idr.HasRows)

            {
                daysOff = populatedaysOffLisst(idr, con);
            }

        }

        con.Close();

        return JsonConvert.SerializeObject(daysOff);

    }

    public static List<daysOff> populatedaysOffLisst(SqlDataReader idr, SqlConnection con)

    {

        List<daysOff> daysOffI = new List<daysOff>();

        while (idr.Read())

        {

            daysOffI.Add(new daysOff

            {
				id         = idr["id"] != DBNull.Value ? Convert.ToInt32(idr["id"]) : 0,
			    Emp_id     = idr["Emp_id"] != DBNull.Value ? Convert.ToInt32(idr["Emp_id"]) : 0,
				Date_From  = idr["Date_From"] != DBNull.Value ? Convert.ToString(idr["Date_From"]) : String.Empty,
				Status     = idr["Status"] != DBNull.Value ? Convert.ToInt32(idr["Status"]) : 0
            });

        }

        return daysOffI;

    }
	
    // update off days status
	   [WebMethod]
    public static string updateOffDays(daysOff updateDays)

    {
        string config = Convert.ToString(ConfigurationManager.ConnectionStrings["dbcon"]);

        List<daysOff> daysOff = new List<daysOff>();

        SqlConnection con = new SqlConnection(config);

        con.Open();

        using (SqlCommand cmd = new SqlCommand("UPDATE HR_DaysOFF_Requests SET Status = @Status WHERE id = @id; ", con))
                    {

						cmd.Parameters.Add("@id", SqlDbType.Int).Value = updateDays.id;
			            cmd.Parameters.Add("@Status", SqlDbType.Int).Value = updateDays.Status;
			            cmd.ExecuteNonQuery();
                  }

        con.Close();
		return JsonConvert.SerializeObject(daysOff);
    }
	
    public class daysOff

    {
		public int id { get; set; }
        public int Emp_id { get; set; }
		public string Date_From { get; set; }
        public int Status { get; set; }
      
    }
	
	// get HR_Vacations_Period
	[WebMethod]
    public static string getPeriodsData(HR_Vacations_Period detail)

    {
        string config = Convert.ToString(ConfigurationManager.ConnectionStrings["dbcon"]);

        List<HR_Vacations_Period> HR_Vacations_Period = new List<HR_Vacations_Period>();

        SqlConnection con = new SqlConnection(config);

        con.Open();

        using (SqlCommand cmd = new SqlCommand("SELECT Date_From FROM HR_Vacations_Period WHERE Emp_code = @Emp_id and Request_id IN (SELECT id FROM HR_Vacations_Requests WHERE Vacation_Type_name = 'Casual' and Status = 4)", con))

        {
			cmd.Parameters.AddWithValue("@Emp_id", detail.Emp_id);
            SqlDataReader idr = cmd.ExecuteReader();

            if (idr.HasRows)

            {
                HR_Vacations_Period = populateHR_Vacations_PeriodLisst(idr, con);
            }

        }

        con.Close();

        return JsonConvert.SerializeObject(HR_Vacations_Period);

    }

    public static List<HR_Vacations_Period> populateHR_Vacations_PeriodLisst(SqlDataReader idr, SqlConnection con)

    {

        List<HR_Vacations_Period> HR_Vacations_PeriodI = new List<HR_Vacations_Period>();

        while (idr.Read())

        {

            HR_Vacations_PeriodI.Add(new HR_Vacations_Period

            {
				Date_From        = idr["Date_From"] != DBNull.Value ? Convert.ToString(idr["Date_From"]) : String.Empty
            });

        }

        return HR_Vacations_PeriodI;

    }
	
    public class HR_Vacations_Period

    {
		public string Date_From { get; set; }
		public int Emp_id { get; set; }
    }

	
// insert to HR_Vacations_Requests
	
   [WebMethod]

    public static string NewHR_Vacations_Requests(HR_Vacations_Requests HR_Vacations_Request)

    {

        string config = Convert.ToString(ConfigurationManager.ConnectionStrings["dbcon"]);

        List<HR_Vacations_Requests> HR_Vacations_Requests = new List<HR_Vacations_Requests>();

        SqlConnection con = new SqlConnection(config);

        con.Open();

        using (SqlCommand cmd = new SqlCommand("insert into HR_Vacations_Requests (Requester_id,Delegation_EmpName,Delegation_EmpCode,Deduction_name,Deduction_id,Vacation_Type_name,Vacation_Type_id,Vacation_Type_Others,Total_Days,Emergency_Contact,Requester_Comment,Attach_File,Status, Dept_id,OFF_Day) values (@Requester_id,@Delegation_EmpName,@Delegation_EmpCode,@Deduction_name,@Deduction_id,@Vacation_Type_name,@Vacation_Type_id,@Vacation_Type_Others,@Total_Days,@Emergency_Contact,@Requester_Comment,@Attach_File,@Status, @Dept_id,@OFF_Day) select @@identity", con))
                    {

						cmd.Parameters.Add("@Requester_id", SqlDbType.Int).Value = HR_Vacations_Request.Requester_id;
			            cmd.Parameters.Add("@Delegation_EmpName", SqlDbType.VarChar).Value = HR_Vacations_Request.Delegation_EmpName;  
						cmd.Parameters.Add("@Delegation_EmpCode", SqlDbType.Int).Value = HR_Vacations_Request.Delegation_EmpCode;
                        cmd.Parameters.Add("@Deduction_name", SqlDbType.VarChar).Value = HR_Vacations_Request.Deduction_name;
						cmd.Parameters.Add("@Deduction_id", SqlDbType.Int).Value = HR_Vacations_Request.Deduction_id;
                        cmd.Parameters.Add("@Vacation_Type_name", SqlDbType.VarChar).Value = HR_Vacations_Request.Vacation_Type_name;  
						cmd.Parameters.Add("@Vacation_Type_id", SqlDbType.Int).Value = HR_Vacations_Request.Vacation_Type_id;
                        cmd.Parameters.Add("@Vacation_Type_Others", SqlDbType.VarChar).Value = HR_Vacations_Request.Vacation_Type_Others; 
						cmd.Parameters.Add("@Total_Days", SqlDbType.Int).Value = HR_Vacations_Request.Total_Days;
                        cmd.Parameters.Add("@Emergency_Contact", SqlDbType.VarChar).Value = HR_Vacations_Request.Emergency_Contact;  
						cmd.Parameters.Add("@Requester_Comment", SqlDbType.VarChar).Value = HR_Vacations_Request.Requester_Comment;
						cmd.Parameters.Add("@Attach_File", SqlDbType.VarChar).Value = HR_Vacations_Request.Attach_File;
						cmd.Parameters.Add("@Status", SqlDbType.Int).Value = 1;
						cmd.Parameters.Add("@Dept_id", SqlDbType.Int).Value = HttpContext.Current.Session["Dept_id"];
						cmd.Parameters.Add("@OFF_Day", SqlDbType.Int).Value = HR_Vacations_Request.OFF_Day;
  
                    	NewId = Convert.ToInt32(cmd.ExecuteScalar());
			            Emp_code = HR_Vacations_Request.Requester_id;
			            Type_id = HR_Vacations_Request.Vacation_Type_id;
                  }

        con.Close();
		return JsonConvert.SerializeObject(HR_Vacations_Requests);
    }
	

    public class HR_Vacations_Requests

    {
		public int Requester_id { get; set; }
		public string Delegation_EmpName { get; set; }
      	public int Delegation_EmpCode { get; set; }
        public string Deduction_name { get; set; }
      	public int Deduction_id { get; set; }
        public string Vacation_Type_name { get; set; }
		public int Vacation_Type_id { get; set; }
        public string Vacation_Type_Others { get; set; }
		public int Total_Days { get; set; }
        public string Emergency_Contact { get; set; }
		public string Requester_Comment { get; set; }
		public string Attach_File { get; set; }
		public int OFF_Day { get; set; }
		public DateTime Entry_date { get; set; }
    }
	

   // insert to NewHR_Vacations_Days
	
   [WebMethod]

    public static string NewHR_Vacations_Days(Vacations_Days Vacations_Days)

    {

        string config = Convert.ToString(ConfigurationManager.ConnectionStrings["dbcon"]);

        List<Vacations_Days> Vacations_Daysi = new List<Vacations_Days>();

        SqlConnection con = new SqlConnection(config);

        con.Open();

        using (SqlCommand cmd = new SqlCommand("insert into HR_Vacations_Period (Request_id,Emp_code,Type_id,Date_from,Date_to) values (@Request_id,@Emp_code,@Type_id,@Date_from,@Date_to)", con))
		{
			cmd.Parameters.Add("@Request_id", SqlDbType.Int).Value = NewId;
			cmd.Parameters.Add("@Emp_code", SqlDbType.Int).Value = Emp_code;
			cmd.Parameters.Add("@Type_id", SqlDbType.Int).Value = Type_id;
			cmd.Parameters.Add("@Date_from", SqlDbType.VarChar).Value = Vacations_Days.Date_from;  
			cmd.Parameters.Add("@Date_to", SqlDbType.VarChar).Value = Vacations_Days.Date_to;
			cmd.ExecuteNonQuery();
		}

		con.Close();
		return JsonConvert.SerializeObject(Vacations_Daysi);
    }
	
    public class Vacations_Days

    {
        public string Date_from { get; set; }
        public string Date_to { get; set; }
    }
	
	// upload file
	protected void btnUpload_Click(object sender, EventArgs e)
    {
        //Files is folder Name
        fuSample.SaveAs(Server.MapPath("Files") + "//" + NewId + '-' + fuSample.FileName);
    }
	
	// check balance
	[WebMethod]

	public static string checkBalance(HR_Employees_Vacation_Balance detail)

	{
		List<HR_Employees_Vacation_Balance> HR_Employees_Vacation_Balance = new List<HR_Employees_Vacation_Balance>();
		string config = Convert.ToString(ConfigurationManager.ConnectionStrings["dbcon"]);

		SqlConnection con = new SqlConnection(config);

		con.Open();

		using (SqlCommand cmd = new SqlCommand("select * from  HR_Employees_Vacation_Balance where Emp_id = @Emp_id", con))

        {
			 cmd.Parameters.AddWithValue("@Emp_id", detail.Emp_id);
             SqlDataReader idr = cmd.ExecuteReader();

            if (idr.HasRows)

            {
                HR_Employees_Vacation_Balance = populateHR_Employees_Vacation_BalanceLisst(idr, con);

            }

        }

		con.Close();

		return JsonConvert.SerializeObject(HR_Employees_Vacation_Balance);
	}

	public static List<HR_Employees_Vacation_Balance> populateHR_Employees_Vacation_BalanceLisst(SqlDataReader idr, SqlConnection con)

	{

		List<HR_Employees_Vacation_Balance> EmpBalanceI = new List<HR_Employees_Vacation_Balance>();

		while (idr.Read())

		{

			EmpBalanceI.Add(new HR_Employees_Vacation_Balance
				  {
					 Emp_id         = Convert.ToInt32(idr["Emp_id"]),
					 Annual_Balance = Convert.ToInt32(idr["Annual_Balance"]),
					 Casual_Balance = Convert.ToInt32(idr["Casual_Balance"]),
					 Off_Balance    = idr["Off_Balance"] != DBNull.Value ? Convert.ToInt32(idr["Off_Balance"]) : 0 ,
					 Q1             = Convert.ToInt32(idr["Q1"]),
					 Q2             = Convert.ToInt32(idr["Q2"]),
					 Q3             = Convert.ToInt32(idr["Q3"]),
					 Q4             = Convert.ToInt32(idr["Q4"]),
					 Old_Balance    = Convert.ToInt32(idr["Old_Balance"]),
					 Old_Balance_ExpirationDate = Convert.ToString(idr["Old_Balance_ExpirationDate"])
				  });
		}
		return EmpBalanceI;
	}
	
	public class HR_Employees_Vacation_Balance

	{
		public int? Emp_id { get; set; }
		public int? Annual_Balance { get; set; }
		public int? Casual_Balance { get; set; }
		public int? Off_Balance { get; set; }
		public int? Q1 { get; set; }
		public int? Q2 { get; set; }
		public int? Q3 { get; set; }
		public int? Q4 { get; set; }
		public int? Old_Balance { get; set; }
		public string Old_Balance_ExpirationDate { get; set; }
	}
	

	/**************************/
	// get pages
       [WebMethod(EnableSession = true)]
    public static string getPages()

    {
        string config = Convert.ToString(ConfigurationManager.ConnectionStrings["dbcon"]);

        List<pages> pages = new List<pages>();

        SqlConnection con = new SqlConnection(config);

        con.Open();

        using (SqlCommand cmd = new SqlCommand("SELECT Menu.* FROM User_Authority INNER JOIN Menu ON User_Authority.menu_id = Menu.id and User_Authority.admin_id=@admin_id" , con))

        {
cmd.Parameters.Add("@admin_id", SqlDbType.Int).Value = HttpContext.Current.Session["admin_id"];
             SqlDataReader idr = cmd.ExecuteReader();

            if (idr.HasRows)

            {

                pages = populatepagesLisst(idr, con);

            }

        }



        con.Close();

        return JsonConvert.SerializeObject(pages);

    }
    public static List<pages> populatepagesLisst(SqlDataReader idr, SqlConnection con)

    {

        List<pages> pagesI = new List<pages>();

        while (idr.Read())

        {

            pagesI.Add(new pages

            {

                id = Convert.ToInt32(idr["id"]),
				CategoryId = Convert.ToInt32(idr["CategoryId"]),
                Name = Convert.ToString(idr["Name"]),
                Path = Convert.ToString(idr["Path"])


            });

        }



        return pagesI;

    }

    public class pages

    {

        public int id { get; set; }
		public int CategoryId { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }


    }
	
	// get categories
	[WebMethod]
    public static string getcatsData()

    {
        string config = Convert.ToString(ConfigurationManager.ConnectionStrings["dbcon"]);

        List<cats> cats = new List<cats>();

        SqlConnection con = new SqlConnection(config);

        con.Open();

        using (SqlCommand cmd = new SqlCommand("Select * from Categories", con))

        {

            SqlDataReader idr = cmd.ExecuteReader();

            if (idr.HasRows)

            {

                cats = populatecatsLisst(idr, con);

            }

        }

        con.Close();

        return JsonConvert.SerializeObject(cats);

    }

    public static List<cats> populatecatsLisst(SqlDataReader idr, SqlConnection con)

    {

        List<cats> catsI = new List<cats>();

        while (idr.Read())

        {

            catsI.Add(new cats

            {

			    id     = idr["id"] != DBNull.Value ? Convert.ToInt32(idr["id"]) : 0,
                Name   = idr["Name"] != DBNull.Value ? Convert.ToString(idr["Name"]) : String.Empty,
				Icon   = idr["Icon"] != DBNull.Value ? Convert.ToString(idr["Icon"]) : String.Empty,

            });

        }

        return catsI;

    }

	public class cats
		
    {
        public int id { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }
    }
	
}


	