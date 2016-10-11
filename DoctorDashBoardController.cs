using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.ServiceModel;
using System.Web.Configuration;

//using HealthCareInMvc4.DischargeSummaryServiceRef;
using HealthCareObjects.Patient.InPatient;
using HealthCareInMvc4.Areas.Lab.Controllers;
namespace HealthCareInMvc4.Areas.Store.Controllers
{
  
    public class DoctorDashBoardController : Controller
    {
        //
        // GET: /Store/DoctorDashBoard/
            string Servicepath = Convert.ToString(WebConfigurationManager.AppSettings["ServicePath"]);

        string connstring = Convert.ToString(System.Web.Configuration.WebConfigurationManager.AppSettings["ConnectionString"]);

     
        
        public ActionResult ADDNEWPARTIALVIEW()
        {

           var drp1 = dropdownlist();
           ViewBag.list1 = new SelectList(drp1.AsEnumerable(), "","BaseEntityName");
          
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

       

        public ActionResult Gender(string Fromdate, string Todate) 
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
                using (cmd = new SqlCommand("sp_Salutationchart", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@FromDate", Fromdate));
                    cmd.Parameters.Add(new SqlParameter("@ToDate", Todate));
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {

                       s =new DischargeSummary();
                        s.BaseEntityName = DBNull.Value.Equals(dr["NAME"]) ? "" : Convert.ToString(dr["NAME"]);
                        s.BedID = DBNull.Value.Equals(dr["CountVal"]) ? 0 : Convert.ToInt64(dr["CountVal"]);
                       
                        obj.Add(s);
                     }
                   }
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
        } 
        public ActionResult PatientTypes(string Fromdate, string Todate) 
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
                using (cmd = new SqlCommand("check_count_ss", con))
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
        public ActionResult PatientCategory(string Fromdate, string Todate) 
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
                using (cmd = new SqlCommand("sp_Get_Panel_Cash_count", con))
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
        public ActionResult Racepatients(string Fromdate, string Todate) 
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
                using (cmd = new SqlCommand("COUNT_race", con))
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
        public ActionResult Agegroupforpatients(string Fromdate, string Todate)
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
                using (cmd = new SqlCommand("count_Age_diff", con))
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
        public ActionResult Patientlocations(string Fromdate, string Todate)
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
                using (cmd = new SqlCommand("COUNT_Locations", con))
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
        public ActionResult Referedbyclinic(string Fromdate, string Todate)
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
        public ActionResult Reference(string Fromdate, string Todate)
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
        public ActionResult Referedtowhom(string Fromdate, string Todate)
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
        public ActionResult Refferedto(string Fromdate, string Todate)
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

        public ActionResult Surgerydetails(string Fromdate)
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
                    cmd.Parameters.Add(new SqlParameter("@FromDate", Fromdate));
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
        public ActionResult Overallnewexist(string Fromdate)
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
                    cmd.Parameters.Add(new SqlParameter("@FromDate", Fromdate));
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
        public ActionResult Patientwaitingtimes(string Fromdate) 
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
                    cmd.Parameters.Add(new SqlParameter("@FromDate", Fromdate));
                   
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
        public ActionResult Doctorsharepharmacy(string Fromdate) 
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
                    cmd.Parameters.Add(new SqlParameter("@FromDate", Fromdate));
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
        public ActionResult Patientwaitinginterval(string Fromdate) 
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
                    cmd.Parameters.Add(new SqlParameter("@FromDate", Fromdate));
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
        public ActionResult Sharesinxray(string Fromdate) 
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
                    cmd.Parameters.Add(new SqlParameter("@FromDate", Fromdate));
                    
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
        public ActionResult Sharesinscan(string Fromdate) 
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
                    cmd.Parameters.Add(new SqlParameter("FromDate", Fromdate));
                    
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
        public ActionResult Sharesinlab(string Fromdate) 
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
                    cmd.Parameters.Add(new SqlParameter("@FromDate", Fromdate));
                   
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







//public ActionResult salutation1()
//        {
//            SqlConnection con = null;
//            SqlCommand cmd = null;
//            SqlDataReader dr;
//            List<DischargeSummary> obj = new List<DischargeSummary>();
//            DischargeSummary s;

//            using (con = new SqlConnection(connstring))
//            {
//                con.Open();
//                con.CreateCommand();
//                using (cmd = new SqlCommand("sp_Salutationchart1", con))
//                {
//                    cmd.CommandType = CommandType.StoredProcedure;
//                    //cmd.Parameters.Add(new SqlParameter("@FromDate", Fromdate));
//                    //cmd.Parameters.Add(new SqlParameter("@ToDate", Todate));
//                    dr = cmd.ExecuteReader();
//                    while (dr.Read())
//                    {

//                        s = new DischargeSummary();
//                        s.BaseEntityName = DBNull.Value.Equals(dr["NAME"]) ? "" : Convert.ToString(dr["NAME"]);
//                        s.BedID = DBNull.Value.Equals(dr["CountVal"]) ? 0 : Convert.ToInt64(dr["CountVal"]);

//                        obj.Add(s);
//                    }
//                }
//                return Json(obj, JsonRequestBehavior.AllowGet);
//            }
//        }
//        public ActionResult PatientTypes1()
//        {
//            SqlConnection con = null;
//            SqlCommand cmd = null;
//            SqlDataReader dr;
//            List<DischargeSummary> obj = new List<DischargeSummary>();
//            DischargeSummary s;

//            using (con = new SqlConnection(connstring))
//            {
//                con.Open();
//                con.CreateCommand();
//                using (cmd = new SqlCommand("check_count_ss1", con))
//                {
//                    cmd.CommandType = CommandType.StoredProcedure;
//                    //cmd.Parameters.Add(new SqlParameter("@FromDate", Fromdate));
//                    //cmd.Parameters.Add(new SqlParameter("@ToDate", Todate));
//                    dr = cmd.ExecuteReader();
//                    while (dr.Read())
//                    {

//                        s = new DischargeSummary();
//                        s.BaseEntityName = DBNull.Value.Equals(dr["NAME"]) ? "" : Convert.ToString(dr["NAME"]);
//                        s.BedID = DBNull.Value.Equals(dr["CountVal"]) ? 0 : Convert.ToInt64(dr["CountVal"]);

//                        obj.Add(s);
//                    }
//                }
//                return Json(obj, JsonRequestBehavior.AllowGet);
//            }
//        }
//        public ActionResult PatientCategory1()
//        {
//            SqlConnection con = null;
//            SqlCommand cmd = null;
//            SqlDataReader dr;
//            List<DischargeSummary> obj = new List<DischargeSummary>();
//            DischargeSummary s;

//            using (con = new SqlConnection(connstring))
//            {
//                con.Open();
//                con.CreateCommand();
//                using (cmd = new SqlCommand("sp_Get_Panel_Cash_count1", con))
//                {
//                    cmd.CommandType = CommandType.StoredProcedure;
//                    // cmd.Parameters.Add(new SqlParameter("@FromDate", Fromdate));
//                    // cmd.Parameters.Add(new SqlParameter("@ToDate", Todate));
//                    dr = cmd.ExecuteReader();
//                    while (dr.Read())
//                    {

//                        s = new DischargeSummary();
//                        s.BaseEntityName = DBNull.Value.Equals(dr["NAME"]) ? "" : Convert.ToString(dr["NAME"]);
//                        s.BedID = DBNull.Value.Equals(dr["CountVal"]) ? 0 : Convert.ToInt64(dr["CountVal"]);

//                        obj.Add(s);
//                    }
//                }
//                return Json(obj, JsonRequestBehavior.AllowGet);
//            }
//        }
//        public ActionResult Overallnew1()
//        {
//            SqlConnection con = null;
//            SqlCommand cmd = null;
//            SqlDataReader dr;
//            List<DischargeSummary> obj = new List<DischargeSummary>();
//            DischargeSummary s;

//            using (con = new SqlConnection(connstring))
//            {
//                con.Open();
//                con.CreateCommand();
//                using (cmd = new SqlCommand("check_count_New1", con))
//                {
//                    cmd.CommandType = CommandType.StoredProcedure;
//                    // cmd.Parameters.Add(new SqlParameter("@FromDate", Fromdate));
//                    // cmd.Parameters.Add(new SqlParameter("@ToDate", Todate));
//                    dr = cmd.ExecuteReader();
//                    while (dr.Read())
//                    {

//                        s = new DischargeSummary();
//                        s.BaseEntityName = DBNull.Value.Equals(dr["NAME"]) ? "" : Convert.ToString(dr["NAME"]);
//                        s.BedID = DBNull.Value.Equals(dr["CountVal"]) ? 0 : Convert.ToInt64(dr["CountVal"]);

//                        obj.Add(s);
//                    }
//                }
//                return Json(obj, JsonRequestBehavior.AllowGet);
//            }
//        }
//        public ActionResult Overallexisting1()
//        {
//            SqlConnection con = null;
//            SqlCommand cmd = null;
//            SqlDataReader dr;
//            List<DischargeSummary> obj = new List<DischargeSummary>();
//            DischargeSummary s;

//            using (con = new SqlConnection(connstring))
//            {
//                con.Open();
//                con.CreateCommand();
//                using (cmd = new SqlCommand("check_count_Exist1", con))
//                {
//                    cmd.CommandType = CommandType.StoredProcedure;
//                    // cmd.Parameters.Add(new SqlParameter("@FromDate", Fromdate));
//                    // cmd.Parameters.Add(new SqlParameter("@ToDate", Todate));
//                    dr = cmd.ExecuteReader();
//                    while (dr.Read())
//                    {

//                        s = new DischargeSummary();
//                        s.BaseEntityName = DBNull.Value.Equals(dr["NAME"]) ? "" : Convert.ToString(dr["NAME"]);
//                        s.BedID = DBNull.Value.Equals(dr["CountVal"]) ? 0 : Convert.ToInt64(dr["CountVal"]);

//                        obj.Add(s);
//                    }
//                }
//                return Json(obj, JsonRequestBehavior.AllowGet);
//            }
//        }
//        public ActionResult Racepatients1()
//        {
//            SqlConnection con = null;
//            SqlCommand cmd = null;
//            SqlDataReader dr;
//            List<DischargeSummary> obj = new List<DischargeSummary>();
//            DischargeSummary s;

//            using (con = new SqlConnection(connstring))
//            {
//                con.Open();
//                con.CreateCommand();
//                using (cmd = new SqlCommand("COUNT_race1", con))
//                {
//                    cmd.CommandType = CommandType.StoredProcedure;
//                    // cmd.Parameters.Add(new SqlParameter("@FromDate", Fromdate));
//                    //cmd.Parameters.Add(new SqlParameter("@ToDate", Todate));
//                    dr = cmd.ExecuteReader();
//                    while (dr.Read())
//                    {

//                        s = new DischargeSummary();
//                        s.BaseEntityName = DBNull.Value.Equals(dr["NAME"]) ? "" : Convert.ToString(dr["NAME"]);
//                        s.BedID = DBNull.Value.Equals(dr["CountVal"]) ? 0 : Convert.ToInt64(dr["CountVal"]);
//                        obj.Add(s);
//                    }
//                }
//                return Json(obj, JsonRequestBehavior.AllowGet);
//            }
//        }
//        public ActionResult Agegroupforpatients1()
//        {
//            SqlConnection con = null;
//            SqlCommand cmd = null;
//            SqlDataReader dr;
//            List<DischargeSummary> obj = new List<DischargeSummary>();
//            DischargeSummary s;

//            using (con = new SqlConnection(connstring))
//            {
//                con.Open();
//                con.CreateCommand();
//                using (cmd = new SqlCommand("count_Age_diff1", con))
//                {
//                    cmd.CommandType = CommandType.StoredProcedure;
//                    // cmd.Parameters.Add(new SqlParameter("@FromDate", Fromdate));
//                    // cmd.Parameters.Add(new SqlParameter("@ToDate", Todate));
//                    dr = cmd.ExecuteReader();
//                    while (dr.Read())
//                    {

//                        s = new DischargeSummary();
//                        s.BaseEntityName = DBNull.Value.Equals(dr["NAME"]) ? "" : Convert.ToString(dr["NAME"]);
//                        s.BedID = DBNull.Value.Equals(dr["CountVal"]) ? 0 : Convert.ToInt64(dr["CountVal"]);
//                        obj.Add(s);
//                    }
//                }
//                return Json(obj, JsonRequestBehavior.AllowGet);
//            }
//        }
//        public ActionResult Overallnewexist1()
//        {
//            SqlConnection con = null;
//            SqlCommand cmd = null;
//            SqlDataReader dr;
//            List<DischargeSummary> obj = new List<DischargeSummary>();
//            DischargeSummary s;

//            using (con = new SqlConnection(connstring))
//            {
//                con.Open();
//                con.CreateCommand();
//                using (cmd = new SqlCommand("count_Exist_and_New1", con))
//                {
//                    cmd.CommandType = CommandType.StoredProcedure;
//                    // cmd.Parameters.Add(new SqlParameter("@FromDate", Fromdate));
//                    // cmd.Parameters.Add(new SqlParameter("@ToDate", Todate));
//                    dr = cmd.ExecuteReader();
//                    while (dr.Read())
//                    {

//                        s = new DischargeSummary();
//                        s.BaseEntityName = DBNull.Value.Equals(dr["NAME"]) ? "" : Convert.ToString(dr["NAME"]);
//                        s.BedID = DBNull.Value.Equals(dr["CountNew"]) ? 0 : Convert.ToInt64(dr["CountNew"]);
//                        s.AdvAmt = DBNull.Value.Equals(dr["CountExist"]) ? 0 : Convert.ToInt64(dr["CountExist"]);

//                        obj.Add(s);
//                    }
//                }
//                return Json(obj, JsonRequestBehavior.AllowGet);
//            }
//        }
//        public ActionResult Overallnew(string Fromdate)
//        {
//            SqlConnection con = null;
//            SqlCommand cmd = null;
//            SqlDataReader dr;
//            List<DischargeSummary> obj = new List<DischargeSummary>();
//            DischargeSummary s;

//            using (con = new SqlConnection(connstring))
//            {
//                con.Open();
//                con.CreateCommand();
//                using (cmd = new SqlCommand("check_count_New", con))
//                {
//                    cmd.CommandType = CommandType.StoredProcedure;
//                    cmd.Parameters.Add(new SqlParameter("@FromDate", Fromdate));
//                    // cmd.Parameters.Add(new SqlParameter("@ToDate", Todate));
//                    dr = cmd.ExecuteReader();
//                    while (dr.Read())
//                    {

//                        s = new DischargeSummary();
//                        s.BaseEntityName = DBNull.Value.Equals(dr["NAME"]) ? "" : Convert.ToString(dr["NAME"]);
//                        s.BedID = DBNull.Value.Equals(dr["CountVal"]) ? 0 : Convert.ToInt64(dr["CountVal"]);

//                        obj.Add(s);
//                    }
//                }
//                return Json(obj, JsonRequestBehavior.AllowGet);
//            }
//        }
//        public ActionResult Overallexisting(string Fromdate)
//        {
//            SqlConnection con = null;
//            SqlCommand cmd = null;
//            SqlDataReader dr;
//            List<DischargeSummary> obj = new List<DischargeSummary>();
//            DischargeSummary s;

//            using (con = new SqlConnection(connstring))
//            {
//                con.Open();
//                con.CreateCommand();
//                using (cmd = new SqlCommand("check_count_Exist", con))
//                {
//                    cmd.CommandType = CommandType.StoredProcedure;
//                    cmd.Parameters.Add(new SqlParameter("@FromDate", Fromdate));
//                    // cmd.Parameters.Add(new SqlParameter("@ToDate", Todate));
//                    dr = cmd.ExecuteReader();
//                    while (dr.Read())
//                    {

//                        s = new DischargeSummary();
//                        s.BaseEntityName = DBNull.Value.Equals(dr["NAME"]) ? "" : Convert.ToString(dr["NAME"]);
//                        s.BedID = DBNull.Value.Equals(dr["CountVal"]) ? 0 : Convert.ToInt64(dr["CountVal"]);

//                        obj.Add(s);
//                    }
//                }
//                return Json(obj, JsonRequestBehavior.AllowGet);
//            }
//        }
      
