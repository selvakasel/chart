using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Configuration;
using HealthCareObjects.Patient.InPatient;

namespace HealthCareInMvc4.Areas.Store.Controllers
{
    public class GribblesDashboardController : Controller
    {
        //
        // GET: /Store/GribblesDashboard/
        string Servicepath = Convert.ToString(WebConfigurationManager.AppSettings["ServicePath"]);
        string connstring = Convert.ToString(System.Web.Configuration.WebConfigurationManager.AppSettings["ConnectionString"]);


        public ActionResult ADDNEWPARTIALVIEW()  
        {
            var drp1 = dropdownlist();
            ViewBag.list1 = new SelectList(drp1.AsEnumerable(), "", "BaseEntityName");

            return PartialView("ADDNEWPARTIALVIEW"); 
        }
        public ActionResult MenuNamepartialview()
        {
            return PartialView("MenuNamepartialview");
        }

        public ActionResult Gridpartialview()
        {
            return PartialView("Gridpartialview");
        }
        public List<DischargeSummary> dropdownlist()
        {
            SqlConnection con = null;
            SqlCommand cmd = null;
            SqlDataReader dr = null;
            List<DischargeSummary> obj = new List<DischargeSummary>();
            DischargeSummary s;

            using (con = new SqlConnection(connstring))
            {
                con.Open();
                con.CreateCommand();
                using (cmd = new SqlCommand("Sp_dropdownyear", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {

                        s = new DischargeSummary();
                        s.BaseEntityName = DBNull.Value.Equals(dr["years"]) ? "" : Convert.ToString(dr["years"]);
                        obj.Add(s);
                    }
                }

            }
            return obj;
        } 



        public ActionResult Departmenttestanalysis(string Fromdate, string Todate)  
        {
            SqlConnection con = null;
            SqlCommand cmd = null;
            SqlDataReader dr;
            List<DischargeSummary> obj = new List<DischargeSummary>();
            DischargeSummary s;
            // decimal lm = 0;

            using (con = new SqlConnection(connstring))
            {
                con.Open();
                con.CreateCommand();
                using (cmd = new SqlCommand("Sp_gribbles_Dept_test", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@FromDate", Fromdate));
                    cmd.Parameters.Add(new SqlParameter("@ToDate", Todate));
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {

                        s = new DischargeSummary();

                        s.BaseEntityName = DBNull.Value.Equals(dr["DepartmentName"]) ? "" : Convert.ToString(dr["DepartmentName"]);
                        s.BedID = DBNull.Value.Equals(dr["countval"]) ? 0 : Convert.ToInt64(dr["countval"]);
                        obj.Add(s);
                    }
                }
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult Statewisetestanalysis(string Fromdate, string Todate)   
        {
            SqlConnection con = null;
            SqlCommand cmd = null;
            SqlDataReader dr;
            List<DischargeSummary> obj = new List<DischargeSummary>();
            DischargeSummary s;
            // decimal lm = 0;

            using (con = new SqlConnection(connstring))
            {
                con.Open();
                con.CreateCommand();
                using (cmd = new SqlCommand("sp_gribbles_state", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@FromDate", Fromdate));
                    cmd.Parameters.Add(new SqlParameter("@ToDate", Todate));
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {

                        s = new DischargeSummary();

                        s.BaseEntityName = DBNull.Value.Equals(dr["NAME"]) ? "" : Convert.ToString(dr["NAME"]);
                        s.BedID = DBNull.Value.Equals(dr["CountVal"]) ? 0 : Convert.ToInt64(dr["CountVal"]);
                        obj.Add(s);
                    }
                }
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult shareingribble(string Fromdate)
        {
            SqlConnection con = null;
            SqlCommand cmd = null;
            SqlDataReader dr;
            List<DischargeSummary> obj = new List<DischargeSummary>();
            DischargeSummary s;
         
            using (con = new SqlConnection(connstring))
            {
                con.Open();
                con.CreateCommand();
                using (cmd = new SqlCommand("gribble_lab_share", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@FromDate", Fromdate));
                  
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {

                        s = new DischargeSummary();

                        s.BaseEntityName = DBNull.Value.Equals(dr["NAME"]) ? "" : Convert.ToString(dr["NAME"]);
                        s.BedID = DBNull.Value.Equals(dr["countGribble"]) ? 0 : Convert.ToInt64(dr["countGribble"]);
                        s.AdvAmt = DBNull.Value.Equals(dr["countHospitalshare"]) ? 0 : Convert.ToInt64(dr["countHospitalshare"]);

                        obj.Add(s);
                    }
                }
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult Drlabonedoctorstatstice(string Fromdate)
        {
            SqlConnection con = null;
            SqlCommand cmd = null;
            SqlDataReader dr;
            List<DischargeSummary> obj = new List<DischargeSummary>();
            DischargeSummary s;
          

            using (con = new SqlConnection(connstring))
            {
                con.Open();
                con.CreateCommand();
                using (cmd = new SqlCommand("SP_labonedoctor_Statstics_gribbles", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@FromDate", Fromdate));
                   
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {

                        s = new DischargeSummary();
                        s.BaseEntityName = DBNull.Value.Equals(dr["NAME"]) ? "" : Convert.ToString(dr["NAME"]);
                        s.BedID = DBNull.Value.Equals(dr["CountVal"]) ? 0 : Convert.ToInt64(dr["CountVal"]);
                       
                        obj.Add(s);
                    }
                }
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult Salutiation(string Fromdate)
        {
            SqlConnection con = null;
            SqlCommand cmd = null;
            SqlDataReader dr;
            List<DischargeSummary> obj = new List<DischargeSummary>();
            DischargeSummary s;

            using (con = new SqlConnection(connstring))
            {
                con.Open();
                con.CreateCommand();
                using (cmd = new SqlCommand("sp_Salutation_gribble", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@FromDate", Fromdate));
                   
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {

                        s = new DischargeSummary();
                        s.BaseEntityName = DBNull.Value.Equals(dr["NAME"]) ? "" : Convert.ToString(dr["NAME"]);
                        s.BedID = DBNull.Value.Equals(dr["Male"]) ? 0 : Convert.ToInt64(dr["Male"]);
                        s.AdvAmt = DBNull.Value.Equals(dr["Female"]) ? 0 : Convert.ToInt64(dr["Female"]);
                        obj.Add(s);
                    }
                }
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult Racepatients(string Fromdate, string Todate) 
        {
            SqlConnection con = null;
            SqlCommand cmd = null;
            SqlDataReader dr;
            List<DischargeSummary> obj = new List<DischargeSummary>();
            DischargeSummary s;
            // decimal lm = 0;

            using (con = new SqlConnection(connstring))
            {
                con.Open();
                con.CreateCommand();
                using (cmd = new SqlCommand("Sp_gribbles_Race", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@FromDate", Fromdate));
                    cmd.Parameters.Add(new SqlParameter("@ToDate", Todate));
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {

                        s = new DischargeSummary();

                        s.BaseEntityName = DBNull.Value.Equals(dr["NAME"]) ? "" : Convert.ToString(dr["NAME"]);
                        s.BedID = DBNull.Value.Equals(dr["CountVal"]) ? 0 : Convert.ToInt64(dr["CountVal"]);
                        obj.Add(s);
                    }
                }
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult Newdoctorsactivated(string Fromdate) 
        {
            SqlConnection con = null;
            SqlCommand cmd = null;
            SqlDataReader dr;
            List<DischargeSummary> obj = new List<DischargeSummary>();
            DischargeSummary s;
            // decimal lm = 0;

            using (con = new SqlConnection(connstring))
            {
                con.Open();
                con.CreateCommand();
                using (cmd = new SqlCommand("Sp_NewDoctorStatistics", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@FromDate", Fromdate));
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {

                        s = new DischargeSummary();

                        s.BaseEntityName = DBNull.Value.Equals(dr["Months"]) ? "" : Convert.ToString(dr["Months"]);
                        s.BedID = DBNull.Value.Equals(dr["Doctor"]) ? 0 : Convert.ToInt64(dr["Doctor"]);
                        obj.Add(s);
                    }
                }
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult Lostdoctorstatistics(string Fromdate)
        {
            SqlConnection con = null;
            SqlCommand cmd = null;
            SqlDataReader dr;
            List<DischargeSummary> obj = new List<DischargeSummary>();
            DischargeSummary s;
            // decimal lm = 0;

            using (con = new SqlConnection(connstring))
            {
                con.Open();
                con.CreateCommand();
                using (cmd = new SqlCommand("Sp_LostDoctorStatistics", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@FromDate", Fromdate));
                   
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {

                        s = new DischargeSummary();

                        s.BaseEntityName = DBNull.Value.Equals(dr["Months"]) ? "" : Convert.ToString(dr["Months"]);
                        s.BedID = DBNull.Value.Equals(dr["Doctor"]) ? 0 : Convert.ToInt64(dr["Doctor"]);
                        obj.Add(s);
                    }
                }
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult Drlabonedoctoranalysis(string Fromdate, string Todate)
        {
            SqlConnection con = null;
            SqlCommand cmd = null;
            SqlDataReader dr;
            List<DischargeSummary> obj = new List<DischargeSummary>();
            DischargeSummary s;
            // decimal lm = 0;

            using (con = new SqlConnection(connstring))
            {
                con.Open();
                con.CreateCommand();
                using (cmd = new SqlCommand("Sp_Dr_Dept_analysis", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@FromDate", Fromdate));
                    cmd.Parameters.Add(new SqlParameter("@ToDate", Todate));
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {

                        s = new DischargeSummary();

                        s.BaseEntityName = DBNull.Value.Equals(dr["DepartmentName"]) ? "" : Convert.ToString(dr["DepartmentName"]);
                        s.BedID = DBNull.Value.Equals(dr["patient"]) ? 0 : Convert.ToInt64(dr["patient"]);
                        obj.Add(s);
                    }
                }
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
        }

    }
}
