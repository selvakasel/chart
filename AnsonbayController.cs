using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.ServiceModel;
using System.Web.Configuration;
using HealthCareInMvc4.DischargeSummaryServiceRef;
namespace HealthCareInMvc4.Areas.Store.Controllers
{
    public class AnsonbayController : Controller
    {
        //
        // GET: /Store/Ansonbay/

        string Servicepath = Convert.ToString(WebConfigurationManager.AppSettings["ServicePath"]);
        string connstring = Convert.ToString(System.Web.Configuration.WebConfigurationManager.AppSettings["ConnectionString"]);
        string connstring1 = Convert.ToString(System.Web.Configuration.WebConfigurationManager.AppSettings["ConnectionString1"]);
        public ActionResult ADDNEWPARTIALVIEW()
        {         
            return PartialView("ADDNEWPARTIALVIEW");
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult MenuNamepartialview()
        {
            return PartialView("MenuNamepartialview");
        }

        public ActionResult Gridpartialview()
        {
            return PartialView("Gridpartialview");
        }

        public ActionResult Gender()
        {
            SqlConnection con = null;
            SqlCommand cmd = null;
            SqlDataReader dr;
            List<DischargeSummary> obj = new List<DischargeSummary>();
            DischargeSummary s;

            using (con = new SqlConnection(connstring1))
            {
                con.Open();
                con.CreateCommand();
                using (cmd = new SqlCommand("Gender_patient", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    //cmd.Parameters.Add(new SqlParameter("@FromDate", Fromdate));
                    //cmd.Parameters.Add(new SqlParameter("@ToDate", Todate));
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
        public ActionResult PatientTypes()
        {
            SqlConnection con = null;
            SqlCommand cmd = null;
            SqlDataReader dr;
            List<DischargeSummary> obj = new List<DischargeSummary>();
            DischargeSummary s;

            using (con = new SqlConnection(connstring1))
            {
                con.Open();
                con.CreateCommand();
                using (cmd = new SqlCommand("Patient_type", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    //cmd.Parameters.Add(new SqlParameter("@FromDate", Fromdate));
                    //cmd.Parameters.Add(new SqlParameter("@ToDate", Todate));
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
        public ActionResult PatientCategory()
        {
            SqlConnection con = null;
            SqlCommand cmd = null;
            SqlDataReader dr;
            List<DischargeSummary> obj = new List<DischargeSummary>();
            DischargeSummary s;

            using (con = new SqlConnection(connstring1))
            {
                con.Open();
                con.CreateCommand();
                using (cmd = new SqlCommand("Patient_category", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    //cmd.Parameters.Add(new SqlParameter("@FromDate", Fromdate));
                    //cmd.Parameters.Add(new SqlParameter("@ToDate", Todate));
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
        public ActionResult Racepatients()
        {
            SqlConnection con = null;
            SqlCommand cmd = null;
            SqlDataReader dr;
            List<DischargeSummary> obj = new List<DischargeSummary>();
            DischargeSummary s;

            using (con = new SqlConnection(connstring1))
            {
                con.Open();
                con.CreateCommand();
                using (cmd = new SqlCommand("Race_Patient", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    // cmd.Parameters.Add(new SqlParameter("@FromDate", Fromdate));
                    //cmd.Parameters.Add(new SqlParameter("@ToDate", Todate));
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
        public ActionResult Agegroupforpatients()
        {
            SqlConnection con = null;
            SqlCommand cmd = null;
            SqlDataReader dr;
            List<DischargeSummary> obj = new List<DischargeSummary>();
            DischargeSummary s;

            using (con = new SqlConnection(connstring1))
            {
                con.Open();
                con.CreateCommand();
                using (cmd = new SqlCommand("Patient_Age", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    // cmd.Parameters.Add(new SqlParameter("@FromDate", Fromdate));
                    // cmd.Parameters.Add(new SqlParameter("@ToDate", Todate));
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
        public ActionResult Patientlocations()
        {
            SqlConnection con = null;
            SqlCommand cmd = null;
            SqlDataReader dr;
            List<DischargeSummary> obj = new List<DischargeSummary>();
            DischargeSummary s;

            using (con = new SqlConnection(connstring1))
            {
                con.Open();
                con.CreateCommand();
                using (cmd = new SqlCommand("Location_patient", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    // cmd.Parameters.Add(new SqlParameter("@FromDate", Fromdate));
                    // cmd.Parameters.Add(new SqlParameter("@ToDate", Todate));
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


        public ActionResult Overallnewexist()
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
                using (cmd = new SqlCommand("count_Exist_and_New", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    //  cmd.Parameters.Add(new SqlParameter("@FromDate", Fromdate));
                    // cmd.Parameters.Add(new SqlParameter("@ToDate", Todate));
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {

                        s = new DischargeSummary();
                        s.BaseEntityName = DBNull.Value.Equals(dr["NAME"]) ? "" : Convert.ToString(dr["NAME"]);
                        s.BedID = DBNull.Value.Equals(dr["CountNew"]) ? 0 : Convert.ToInt64(dr["CountNew"]);
                        s.AdvAmt = DBNull.Value.Equals(dr["CountExist"]) ? 0 : Convert.ToInt64(dr["CountExist"]);

                        obj.Add(s);
                    }
                }
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult Surgerydetails()
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
                using (cmd = new SqlCommand("sp_list_Surgerycount_by_month", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    // cmd.Parameters.Add(new SqlParameter("@FromDate", Fromdate));
                    // cmd.Parameters.Add(new SqlParameter("@ToDate", Todate));
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
        public ActionResult Referedbyclinic()
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
                using (cmd = new SqlCommand("Reference_count_ss", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    // cmd.Parameters.Add(new SqlParameter("@FromDate", Fromdate));
                    // cmd.Parameters.Add(new SqlParameter("@ToDate", Todate));
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
        public ActionResult Reference()
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
                using (cmd = new SqlCommand("Reference_count_by", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    // cmd.Parameters.Add(new SqlParameter("@FromDate", Fromdate));
                    // cmd.Parameters.Add(new SqlParameter("@ToDate", Todate));
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
        public ActionResult Patientwaitingtimes()
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
                using (cmd = new SqlCommand("patient_waiting_time", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    // cmd.Parameters.Add(new SqlParameter("@FromDate", Fromdate));

                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {

                        s = new DischargeSummary();
                        s.BaseEntityName = DBNull.Value.Equals(dr["Patient"]) ? "" : Convert.ToString(dr["Patient"]);
                        //s.BedID = DBNull.Value.Equals(dr["Patient"]) ? 0 : Convert.ToInt64(dr["Patient"]);
                        s.AdvAmt = DBNull.Value.Equals(dr["Minutes"]) ? 0 : Convert.ToInt64(dr["Minutes"]);

                        obj.Add(s);
                    }
                }
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult Doctorsharepharmacy()
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
                using (cmd = new SqlCommand("sp_doctor_share", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    // cmd.Parameters.Add(new SqlParameter("@FromDate", Fromdate));
                    //cmd.Parameters.Add(new SqlParameter("@ToDate", Todate));
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {

                        s = new DischargeSummary();

                        s.BaseEntityName = DBNull.Value.Equals(dr["NAME"]) ? "" : Convert.ToString(dr["NAME"]);
                        s.BedID = DBNull.Value.Equals(dr["countHospital"]) ? 0 : Convert.ToInt64(dr["countHospital"]);
                        s.AdvAmt = DBNull.Value.Equals(dr["countDoctorshare"]) ? 0 : Convert.ToInt64(dr["countDoctorshare"]);

                        // s.Clientaddress = DBNull.Value.Equals(dr["CountVal"]) ? "" : Convert.ToString(dr["CountVal"]);
                        // s.percentage = decimal.Parse(s.Clientaddress);
                        //s.percentage = Int64.Parse(s.Clientaddress.ToString());
                        obj.Add(s);
                    }
                }
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult Referedtowhom()
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
                using (cmd = new SqlCommand("Refered_To_Whom", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    // cmd.Parameters.Add(new SqlParameter("@FromDate", Fromdate));
                    //  cmd.Parameters.Add(new SqlParameter("@ToDate", Todate));
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
        public ActionResult Refferedto()
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
                using (cmd = new SqlCommand("Reffered_To", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    // cmd.Parameters.Add(new SqlParameter("@FromDate", Fromdate));
                    // cmd.Parameters.Add(new SqlParameter("@ToDate", Todate));
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
        public ActionResult Patientwaitinginterval()
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
                using (cmd = new SqlCommand("count_patient_inter", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    // cmd.Parameters.Add(new SqlParameter("@FromDate", Fromdate));
                    // cmd.Parameters.Add(new SqlParameter("@ToDate", Todate));
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
        public ActionResult Sharesinxray()
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
                using (cmd = new SqlCommand("sp_xray_share", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    //  cmd.Parameters.Add(new SqlParameter("@FromDate", Fromdate));

                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        s = new DischargeSummary();
                        s.BaseEntityName = DBNull.Value.Equals(dr["NAME"]) ? "" : Convert.ToString(dr["NAME"]);
                        s.BedID = DBNull.Value.Equals(dr["countHospital"]) ? 0 : Convert.ToInt64(dr["countHospital"]);
                        s.AdvAmt = DBNull.Value.Equals(dr["countDoctorshare"]) ? 0 : Convert.ToInt64(dr["countDoctorshare"]);
                        obj.Add(s);

                    }
                }
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult Sharesinscan()
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
                using (cmd = new SqlCommand("sp_scan_share", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    // cmd.Parameters.Add(new SqlParameter("FromDate", Fromdate));

                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        s = new DischargeSummary();
                        s.BaseEntityName = DBNull.Value.Equals(dr["NAME"]) ? "" : Convert.ToString(dr["NAME"]);
                        s.BedID = DBNull.Value.Equals(dr["countHospital"]) ? 0 : Convert.ToInt64(dr["countHospital"]);
                        s.AdvAmt = DBNull.Value.Equals(dr["countDoctorshare"]) ? 0 : Convert.ToInt64(dr["countDoctorshare"]);
                        obj.Add(s);

                    }
                }
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult Sharesinlab()
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
                using (cmd = new SqlCommand("sp_lab_share", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    //  cmd.Parameters.Add(new SqlParameter("@FromDate", Fromdate));

                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        s = new DischargeSummary();
                        s.BaseEntityName = DBNull.Value.Equals(dr["NAME"]) ? "" : Convert.ToString(dr["NAME"]);
                        s.BedID = DBNull.Value.Equals(dr["countHospital"]) ? 0 : Convert.ToInt64(dr["countHospital"]);
                        s.AdvAmt = DBNull.Value.Equals(dr["countDoctorshare"]) ? 0 : Convert.ToInt64(dr["countDoctorshare"]);
                        obj.Add(s);

                    }
                }
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
        } 



    }
}
