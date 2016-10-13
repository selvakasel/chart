using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Configuration;
using HealthCareInMvc4.DoctorDashboardServiceRef;
using System.Data.SqlClient;
using System.Data;

namespace HealthCareInMvc4.Areas.Store.Controllers
{
    public class DoctorDashboardController : Controller
    {

        string Servicepath = Convert.ToString(WebConfigurationManager.AppSettings["ServicePath"]);
        string connstring = Convert.ToString(System.Web.Configuration.WebConfigurationManager.AppSettings["ConnectionString"]);

        // GET: /Store/DoctorDashboard/

        public ActionResult ADDNEWPARTIALVIEW()
        {
            var drp1 = dropdownlist();
            ViewBag.list1 = new SelectList(drp1.AsEnumerable(), "", "Name");
            return View();
        }
        public ActionResult Gridpartialview()
        {
            return View();
        }
        public ActionResult MenuNamepartialview()
        {
            return View();
        }

        public List<DoctorDashboardClass> dropdownlist()
        {
            SqlConnection con = null;
            SqlCommand cmd = null;
            SqlDataReader dr = null;
            List<DoctorDashboardClass> obj = new List<DoctorDashboardClass>();
            DoctorDashboardClass s;

            using (con = new SqlConnection("Data Source=WEB-SERVER\\SQLEXPRESS;Initial Catalog=Putralive;Integrated Security=True"))
            {
                con.Open();
                con.CreateCommand();
                using (cmd = new SqlCommand("Sp_dropdownyear", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {

                        s = new DoctorDashboardClass();
                        s.Name = DBNull.Value.Equals(dr["years"]) ? "" : Convert.ToString(dr["years"]);
                        obj.Add(s);
                    }
                }

            }
            return obj;
        }

        //public ActionResult Gender(string Fromdate, string Todate)
        //{
        //    SqlConnection con = null;
        //    SqlCommand cmd = null;
        //    SqlDataReader dr;
        //    List<DoctorDashboardClass> obj = new List<DoctorDashboardClass>();
        //    DoctorDashboardClass s;

        //    using (con = new SqlConnection("Data Source=WEB-SERVER\\SQLEXPRESS;Initial Catalog=Putralive;Integrated Security=True"))
        //    {
        //        con.Open();
        //        con.CreateCommand();
        //        using (cmd = new SqlCommand("sp_Salutationchart", con))
        //        {
        //            cmd.CommandType = CommandType.StoredProcedure;
        //            cmd.Parameters.Add(new SqlParameter("@FromDate", Fromdate));
        //            cmd.Parameters.Add(new SqlParameter("@ToDate", Todate));
        //            dr = cmd.ExecuteReader();
        //            while (dr.Read())
        //            {

        //                s = new DoctorDashboardClass();
        //                s.Name = DBNull.Value.Equals(dr["NAME"]) ? "" : Convert.ToString(dr["NAME"]);
        //                s.Id = DBNull.Value.Equals(dr["CountVal"]) ? 0 : Convert.ToInt64(dr["CountVal"]);

        //                obj.Add(s);
        //            }
        //        }
        //        return Json(obj, JsonRequestBehavior.AllowGet);
        //    }
        //}
        //public ActionResult PatientTypes(string Fromdate, string Todate)
        //{
        //    SqlConnection con = null;
        //    SqlCommand cmd = null;
        //    SqlDataReader dr;
        //    List<DoctorDashboardClass> obj = new List<DoctorDashboardClass>();
        //    DoctorDashboardClass s;

        //    using (con = new SqlConnection("Data Source=WEB-SERVER\\SQLEXPRESS;Initial Catalog=Putralive;Integrated Security=True"))
        //    {
        //        con.Open();
        //        con.CreateCommand();
        //        using (cmd = new SqlCommand("check_count_ss", con))
        //        {
        //            cmd.CommandType = CommandType.StoredProcedure;
        //            cmd.Parameters.Add(new SqlParameter("@FromDate", Fromdate));
        //            cmd.Parameters.Add(new SqlParameter("@ToDate", Todate));
        //            dr = cmd.ExecuteReader();
        //            while (dr.Read())
        //            {

        //                s = new DoctorDashboardClass();
        //                s.Name = DBNull.Value.Equals(dr["NAME"]) ? "" : Convert.ToString(dr["NAME"]);
        //                s.Id = DBNull.Value.Equals(dr["CountVal"]) ? 0 : Convert.ToInt64(dr["CountVal"]);

        //                obj.Add(s);
        //            }
        //        }
        //        return Json(obj, JsonRequestBehavior.AllowGet);
        //    }
        //}


        public ActionResult Patientlocations(string Fromdate, string Todate)
        {
            DoctorDashboardServiceRef.DoctorDashboardServiceClient serv = new DoctorDashboardServiceRef.DoctorDashboardServiceClient();
            return Json(serv.Patientlocations(Fromdate, Todate), JsonRequestBehavior.AllowGet);
        }
        public ActionResult Gender(string Fromdate, string Todate)
        {
            DoctorDashboardServiceRef.DoctorDashboardServiceClient serv = new DoctorDashboardServiceRef.DoctorDashboardServiceClient();
            return Json(serv.Gender(Fromdate, Todate), JsonRequestBehavior.AllowGet);
        }

        public ActionResult PatientTypes(string Fromdate, string Todate)
        {
            DoctorDashboardServiceRef.DoctorDashboardServiceClient serv = new DoctorDashboardServiceRef.DoctorDashboardServiceClient();
            return Json(serv.PatientTypes(Fromdate, Todate), JsonRequestBehavior.AllowGet);
        }
        public ActionResult PatientCategory(string Fromdate, string Todate)
        {
            DoctorDashboardServiceRef.DoctorDashboardServiceClient serv = new DoctorDashboardServiceRef.DoctorDashboardServiceClient();
            return Json(serv.PatientCategory(Fromdate, Todate), JsonRequestBehavior.AllowGet);
        }
        public ActionResult Racepatients(string Fromdate, string Todate)
        {
            DoctorDashboardServiceRef.DoctorDashboardServiceClient serv = new DoctorDashboardServiceRef.DoctorDashboardServiceClient();
            return Json(serv.Racepatients(Fromdate, Todate), JsonRequestBehavior.AllowGet);
        }
        public ActionResult Overallnewexist(string Fromdate)
        {
            DoctorDashboardServiceRef.DoctorDashboardServiceClient serv = new DoctorDashboardServiceRef.DoctorDashboardServiceClient();
            return Json(serv.Overallnewexist(Fromdate), JsonRequestBehavior.AllowGet);
        }
        public ActionResult Agegroupforpatients(string Fromdate, string Todate)
        {
            DoctorDashboardServiceRef.DoctorDashboardServiceClient serv = new DoctorDashboardServiceRef.DoctorDashboardServiceClient();
            return Json(serv.Agegroupforpatients(Fromdate, Todate), JsonRequestBehavior.AllowGet);
        }
        public ActionResult Surgerydetails(string Fromdate)
        {
            DoctorDashboardServiceRef.DoctorDashboardServiceClient serv = new DoctorDashboardServiceRef.DoctorDashboardServiceClient();
            return Json(serv.Surgerydetails(Fromdate), JsonRequestBehavior.AllowGet);
        }
        public ActionResult Referedbyclinic(string Fromdate, string Todate)
        {
            DoctorDashboardServiceRef.DoctorDashboardServiceClient serv = new DoctorDashboardServiceRef.DoctorDashboardServiceClient();
            return Json(serv.Referedbyclinic(Fromdate, Todate), JsonRequestBehavior.AllowGet);
        }
        public ActionResult Reference(string Fromdate, string Todate)
        {
            DoctorDashboardServiceRef.DoctorDashboardServiceClient serv = new DoctorDashboardServiceRef.DoctorDashboardServiceClient();
            return Json(serv.Reference(Fromdate, Todate), JsonRequestBehavior.AllowGet);
        }
        public ActionResult Patientwaitingtimes(string Fromdate)
        {
            DoctorDashboardServiceRef.DoctorDashboardServiceClient serv = new DoctorDashboardServiceRef.DoctorDashboardServiceClient();
            return Json(serv.Patientwaitingtimes(Fromdate), JsonRequestBehavior.AllowGet);
        }
        public ActionResult Doctorsharepharmacy(string Fromdate)
        {
            DoctorDashboardServiceRef.DoctorDashboardServiceClient serv = new DoctorDashboardServiceRef.DoctorDashboardServiceClient();
            return Json(serv.Doctorsharepharmacy(Fromdate), JsonRequestBehavior.AllowGet);
        }
        public ActionResult Referedtowhom(string Fromdate, string Todate)
        {
            DoctorDashboardServiceRef.DoctorDashboardServiceClient serv = new DoctorDashboardServiceRef.DoctorDashboardServiceClient();
            return Json(serv.Referedtowhom(Fromdate, Todate), JsonRequestBehavior.AllowGet);
        }
        public ActionResult Refferedto(string Fromdate, string Todate)
        {
            DoctorDashboardServiceRef.DoctorDashboardServiceClient serv = new DoctorDashboardServiceRef.DoctorDashboardServiceClient();
            return Json(serv.Refferedto(Fromdate, Todate), JsonRequestBehavior.AllowGet);
        }
        public ActionResult Patientwaitinginterval(string Fromdate)
        {
            DoctorDashboardServiceRef.DoctorDashboardServiceClient serv = new DoctorDashboardServiceRef.DoctorDashboardServiceClient();
            return Json(serv.Patientwaitinginterval(Fromdate), JsonRequestBehavior.AllowGet);
        }
        public ActionResult Sharesinxray(string Fromdate)
        {
            DoctorDashboardServiceRef.DoctorDashboardServiceClient serv = new DoctorDashboardServiceRef.DoctorDashboardServiceClient();
            return Json(serv.Sharesinxray(Fromdate), JsonRequestBehavior.AllowGet);
        }
        public ActionResult Sharesinscan(string Fromdate)
        {
            DoctorDashboardServiceRef.DoctorDashboardServiceClient serv = new DoctorDashboardServiceRef.DoctorDashboardServiceClient();
            return Json(serv.Sharesinscan(Fromdate), JsonRequestBehavior.AllowGet);
        }
        public ActionResult Sharesinlab(string Fromdate)
        {
            DoctorDashboardServiceRef.DoctorDashboardServiceClient serv = new DoctorDashboardServiceRef.DoctorDashboardServiceClient();
            return Json(serv.Sharesinlab(Fromdate), JsonRequestBehavior.AllowGet);
        }







    }
}
