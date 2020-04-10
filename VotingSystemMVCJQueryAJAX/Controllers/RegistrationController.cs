using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Data;
using VotingSystemMVCJQueryAJAX.Models;

namespace VotingSystemMVCJQueryAJAX.Controllers
{
    public class RegistrationController : Controller
    {
        string SN = string.Empty;
        string VotersId = string.Empty;
        string Vot = "ESC";
        // GET: Registration

        HashCode hc = new HashCode();
        public ActionResult RegistationPage()
        {
            return View();
        }

        [HttpPost]
        public void Reg(Registration res)
        {
            try
            {
                SqlConnection sqlConn = new SqlConnection("Server=.;Database=VotingSystemMVCJQueryAjax;User Id=muiz;Password=oluwadamilare;");
                //Registration
                if (sqlConn.State == System.Data.ConnectionState.Closed)
                    sqlConn.Open();
                SqlCommand sqlcmd = new SqlCommand("SaveVotersRecord", sqlConn);
                sqlcmd.CommandType = System.Data.CommandType.StoredProcedure;

                //Casting the VotersId returned in the GetVotersID() method into the txtvoterID.Text
                //txtVoterID.Text = GetVotersId();

                //casting the hashed password returned in SaltHashPassword() method into the txtPassword.Text
                //txtPassword.Text = SaltHashPassword();

                sqlcmd.Parameters.AddWithValue("@VoterID", res.VoterID.Trim());
                sqlcmd.Parameters.AddWithValue("@Name", res.Name.Trim());
                sqlcmd.Parameters.AddWithValue("@Email", res.Email.Trim());
                sqlcmd.Parameters.AddWithValue("@Password", hc.PassHash(res.Password.Trim()));
                sqlcmd.Parameters.AddWithValue("@BloodGroup", res.BloodGroup.Trim());
                sqlcmd.Parameters.AddWithValue("@Address", res.Address.Trim());
                sqlcmd.Parameters.AddWithValue("@DOB", res.DOB.Trim());
                sqlcmd.Parameters.AddWithValue("@Telephone", res.Telephone.Trim());
               
                //Sqlcmd.ExecuteNonQuery executes the sqlcommands
                sqlcmd.ExecuteNonQuery();

                //closes the sqlconnection
                sqlConn.Close();
            }
            catch (Exception ex)
            {
                string message;
                message = ex.Message;
            }
        }

        public string GetVotersId()
        {
            string message = string.Empty;
            try
            {
                
                // Declaring Connection String
                string conString = System.Configuration.ConfigurationManager.ConnectionStrings["VotingSytemConnString"].ConnectionString;
                SqlConnection con = new SqlConnection(conString);

                //Open Connection String
                con.Open();

                //Data Reader
                SqlDataReader getrecord1 = null;
                SqlCommand cmd = new SqlCommand();

                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "GetSN";

                getrecord1 = cmd.ExecuteReader();
                getrecord1.Read();

                if (getrecord1.HasRows == true)
                {
                    if (getrecord1["SerialNo"].Equals(DBNull.Value) == true)
                    {
                        //SN will be an Empty string if Value isnt found in the database table(SN): Column(SerialNo) 
                        SN = "";

                    }
                    else
                    {
                        //SN is the Value gotten from the database table(SN): Column(SerialNo) 
                        SN = getrecord1["SerialNo"].ToString();
                    }

                }
                else
                {
                    //Error If Row is not found in the database table(SN)
                    message = "SN not found in the database";
                }
                //Close Connection
                con.Close();

            }

            catch (Exception ex)
            {
                //Handle Error Exception and throw the ex.Message of the Error
                message = ex.Message;
            }

            //Updates the Database Table SN for the the next Record that will be entered
            updateSN();

            //Adds the String-value of Vot and SN value gotten from initially updated SN of the last Record entered
            VotersId = Vot + SN;

            //Returning VotersId
            //return Json(VotersId, JsonRequestBehavior.AllowGet);
            return VotersId;
        }

        public void updateSN()
        {
            SqlConnection sqlConn = new SqlConnection("Server=.;Database=VotingSystemMVCJQueryAjax;User Id=muiz;Password=oluwadamilare;");
            //Adds 1 to the SN value and updates the SN table: Column(serialNo)
            int AddSn = int.Parse(SN) + 1;
            if (sqlConn.State == System.Data.ConnectionState.Closed)
                sqlConn.Open();
            SqlCommand sqlcmd = new SqlCommand("UpdateSN", sqlConn);
            sqlcmd.CommandType = System.Data.CommandType.StoredProcedure;

            //inserts AddSn(updated value of SN) into the Database Table SN :Column(SerialNo)
            sqlcmd.Parameters.AddWithValue("@SN", AddSn);

            sqlcmd.ExecuteNonQuery();



            //sqlConn.Close();
        }


        [HttpPost]
        public string CheckUser(Registration res)
        {
            string message = string.Empty;
            try
            {
                string conString = System.Configuration.ConfigurationManager.ConnectionStrings["VotingSytemConnString"].ConnectionString;
                SqlConnection con = new SqlConnection(conString);
                con.Open();
                SqlCommand cmd = new SqlCommand();

                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "CheckUser";


                cmd.Parameters.Add("@Name", SqlDbType.VarChar).Value = res.Name;
                cmd.Parameters.Add("@Email", SqlDbType.VarChar).Value = res.Email;

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    //User Exists
                    //message = "1";
                    return "1"; ;
                }

                else
                {
                    //close the reader before Reg() is executed
                    reader.Close();
                    reader.Dispose();
                    message = "0";
                    return message;
                }
                con.Close();
            }

            catch (Exception ex)
            {
                return "Server Error";
            }

        }
    }
}