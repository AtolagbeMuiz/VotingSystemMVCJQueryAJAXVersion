using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VotingSystemMVCJQueryAJAX.Models;

namespace VotingSystemMVCJQueryAJAX.Controllers
{
    public class LoginController : Controller
    {

        HashCode hc = new HashCode();
        // GET: Login
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        public string logindetails(Registration res)
        {
            string message = string.Empty;
            try
            {
                //Declaring Connection string
                string conString = System.Configuration.ConfigurationManager.ConnectionStrings["VotingSytemConnString"].ConnectionString;
                SqlConnection con = new SqlConnection(conString);
                con.Open();
                SqlCommand cmd = new SqlCommand();

                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "LoginUser";


                cmd.Parameters.Add("@VoterID", SqlDbType.VarChar).Value = res.VoterID.Trim();
                cmd.Parameters.Add("@Password", SqlDbType.VarChar).Value = hc.PassHash(res.Password.Trim());

                Session["VoterID"] = res.VoterID;
                ViewData["VoterID"] = res.VoterID;
                ViewBag.MyData = res.VoterID;
                int codereturn = (int)cmd.ExecuteScalar();

                if (codereturn == 1)
                {
                    message = "success";
                    return "1";
                     //Response.Redirect("VotersPortal.aspx");
                     
                }
                else
                {
                    message = "Failed";
                    return "0";
                }

                con.Close();
            }
            catch (Exception ex)
            {
                message = "server Error";
                return "2";
            }
        }

        //[HttpPost]
        //public ActionResult Login(Registration res)
        //{
        //    string message = string.Empty;
        //    try
        //    {
        //        Registration res1 = new Registration();
        //        //Declaring Connection string
        //        string conString = System.Configuration.ConfigurationManager.ConnectionStrings["VotingSytemConnString"].ConnectionString;
        //        SqlConnection con = new SqlConnection(conString);
        //        con.Open();
        //        SqlCommand cmd = new SqlCommand();

        //        cmd.Connection = con;
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.CommandText = "LoginUser";


        //        cmd.Parameters.Add("@VoterID", SqlDbType.VarChar).Value = res.VoterID.Trim();
        //        cmd.Parameters.Add("@Password", SqlDbType.VarChar).Value = hc.PassHash(res.Password.Trim());

        //        SqlDataReader dr = cmd.ExecuteReader();
        //        dr.Read();
        //        int codereturn = (int)cmd.ExecuteScalar();
                

        //        if (codereturn == 1)
        //        {
        //            message = "success";
        //            //return "1";

        //            res1.VoterID = dr["VoterID"].ToString();
        //            res1.Name = dr["Name"].ToString();
        //            res1.Email = dr["Email"].ToString();
        //            res1.Address = dr["Address"].ToString();
        //            res1.BloodGroup = dr["BloodGroup"].ToString();
        //            res1.DOB = dr["DateOfBirth"].ToString();
        //            res1.Telephone = dr["Telephone"].ToString();
        //            //Response.Redirect("VotersPortal.aspx");
        //            return View(res1);
        //        }
        //        else
        //        {
        //            message = "Failed";
        //           // return "0";
        //        }

        //        con.Close();
        //    }
        //    catch (Exception ex)
        //    {
        //        message = "server Error";
        //        //return "2";
        //    }
        //    return View();
        //}



    }
}