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
    
    public class VoterPortalController : Controller
    {
        //string VoterID = HttpContext.Current.Session["VOterID"].ToString();
        // GET: VoterPortal
        public ActionResult VoterPortal()
        {
            string VoterID = Session["VOterID"].ToString();
            //casts the data gotten by its SN into a dataset
            DataSet dataset = GetDataBySN(VoterID);
            List<Registration> listreg = new List<Registration>();

            //accesses the row dataset and adding it to a list
            foreach (DataRow dr in dataset.Tables[0].Rows)
            {
                listreg.Add(new Registration
                {
                    VoterID = dr["VoterID"].ToString(),
                    Name = dr["Name"].ToString(),
                    Email = dr["Email"].ToString(),
                    Address = dr["Address"].ToString(),
                    BloodGroup = dr["BloodGroup"].ToString(),
                    DOB = dr["DateOfBirth"].ToString(),
                    Telephone = dr["Telephone"].ToString()
                });
            }
            return PartialView(listreg);
            
        }

        public JsonResult VoterDetails()
        {

            string VoterID = Session["VOterID"].ToString();
            //casts the data gotten by its SN into a dataset
            DataSet dataset = GetDataBySN(VoterID);
            List<Registration> listreg = new List<Registration>();

            //accesses the row dataset and adding it to a list
            foreach (DataRow dr in dataset.Tables[0].Rows)
            {
                listreg.Add(new Registration
                {
                    VoterID = dr["VoterID"].ToString(),
                    Name = dr["Name"].ToString(),
                    Email = dr["Email"].ToString(),
                    Address = dr["Address"].ToString(),
                    BloodGroup = dr["BloodGroup"].ToString(),
                    DOB = dr["DateOfBirth"].ToString(),
                    Telephone = dr["Telephone"].ToString()
                });
            }
            //returns the list in JSON format
            return Json(listreg, JsonRequestBehavior.AllowGet);
        }

        //Get record by Serial No
        public DataSet GetDataBySN(string VoterID)
        {
            string conString = System.Configuration.ConfigurationManager.ConnectionStrings["VotingSytemConnString"].ConnectionString;
            SqlConnection con = new SqlConnection(conString);
            con.Open();
            SqlCommand cmd = new SqlCommand("GetDataBySN", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@VoterID", VoterID);
            SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
            DataSet dataset = new DataSet();
            dataAdapter.Fill(dataset);

            return dataset;
        }
    }
}