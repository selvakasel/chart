using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Configuration;
using HealthCareInMvc4.DoctorDashboardServiceRef;
using System.Data.SqlClient;
using System.Data;
using HealthCareInMvc4.Models;

namespace HealthCareInMvc4.Areas.Store.Controllers
{
    public class OPDashboardController : Controller
    {
        //
        // GET: /Store/OPDashboard/

        public ActionResult ADDNEWPARTIALVIEW()
        {
            var drp1 = dropdownlist();
            ViewBag.list1 = new SelectList(drp1.AsEnumerable(), "", "Name");
            var drp2 = dropdownlist1();
            ViewBag.list2 = new SelectList(drp2.AsEnumerable(), "", "Name");
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
                using (cmd = new SqlCommand("dropdown_dep", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {

                        s = new DoctorDashboardClass();
                        s.Name = DBNull.Value.Equals(dr["DepartmentName"]) ? "" : Convert.ToString(dr["DepartmentName"]);
                        obj.Add(s);
                    }
                }

            }
            return obj;
        }
        public List<DoctorDashboardClass> dropdownlist1()
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
                using (cmd = new SqlCommand("Sp_dropdownweek", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {

                        s = new DoctorDashboardClass();
                        s.Name = DBNull.Value.Equals(dr["Weeks"]) ? "" : Convert.ToString(dr["Weeks"]);
                        obj.Add(s);
                    }
                }

            }
            return obj;
        }

        public ActionResult department(Int64 Appointmentstatus, string Department)
        {
            SqlConnection con = null;
            SqlCommand cmd = null;
            SqlDataReader dr;
            List<DoctorDashboardClass> obj = new List<DoctorDashboardClass>();
            DoctorDashboardClass s;

            using (con = new SqlConnection("Data Source=WEB-SERVER\\SQLEXPRESS;Initial Catalog=Putralive;Integrated Security=True"))
            {
                con.Open();
                con.CreateCommand();
                using (cmd = new SqlCommand("appoiment_dep_gen", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Department", Department));
                    cmd.Parameters.Add(new SqlParameter("@Appointmentstatus", Appointmentstatus));

                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {

                        s = new DoctorDashboardClass();
                        s.Name = DBNull.Value.Equals(dr["NAME"]) ? "" : Convert.ToString(dr["NAME"]);
                        s.Id = DBNull.Value.Equals(dr["CountVal"]) ? 0 : Convert.ToInt64(dr["CountVal"]);

                        obj.Add(s);
                    }
                }
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult Genderday(string Fromdate, string Todate)
        {
            SqlConnection con = null;
            SqlCommand cmd = null;
            SqlDataReader dr;
            List<DoctorDashboardClass> obj = new List<DoctorDashboardClass>();
            DoctorDashboardClass s;

            using (con = new SqlConnection("Data Source=WEB-SERVER\\SQLEXPRESS;Initial Catalog=Putralive;Integrated Security=True"))
            {
                con.Open();
                con.CreateCommand();
                using (cmd = new SqlCommand("sp_opgenderday", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@FromDate", Fromdate));
                    cmd.Parameters.Add(new SqlParameter("@Todate", Todate));

                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {

                        s = new DoctorDashboardClass();
                        s.Name = DBNull.Value.Equals(dr["NAME"]) ? "" : Convert.ToString(dr["NAME"]);
                        s.Id = DBNull.Value.Equals(dr["CountVal"]) ? 0 : Convert.ToInt64(dr["CountVal"]);

                        obj.Add(s);
                    }
                }
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult PatientTypesday(string Fromdate, string Todate, string set)
        {
            SqlConnection con = null;
            SqlCommand cmd = null;
            SqlDataReader dr;
            List<DoctorDashboardClass> obj = new List<DoctorDashboardClass>();
            DoctorDashboardClass s;

            using (con = new SqlConnection("Data Source=WEB-SERVER\\SQLEXPRESS;Initial Catalog=Putralive;Integrated Security=True"))
            {
                con.Open();
                con.CreateCommand();
                using (cmd = new SqlCommand("sp_pattypesday", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@FromDate", Fromdate));
                    cmd.Parameters.Add(new SqlParameter("@Todate", Todate));
                    cmd.Parameters.Add(new SqlParameter("@set", set));

                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {

                        s = new DoctorDashboardClass();
                        s.Name = DBNull.Value.Equals(dr["NAME"]) ? "" : Convert.ToString(dr["NAME"]);
                        s.Id = DBNull.Value.Equals(dr["CountVal"]) ? 0 : Convert.ToInt64(dr["CountVal"]);

                        obj.Add(s);
                    }
                }
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult refgender(string set, string Fromdate, string Todate)
        {
            SqlConnection con = null;
            SqlCommand cmd = null;
            SqlDataReader dr;
            List<DoctorDashboardClass> obj = new List<DoctorDashboardClass>();
            DoctorDashboardClass s;

            using (con = new SqlConnection("Data Source=WEB-SERVER\\SQLEXPRESS;Initial Catalog=Putralive;Integrated Security=True"))
            {
                con.Open();
                con.CreateCommand();
                using (cmd = new SqlCommand("refund_sex", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@set", set));
                    cmd.Parameters.Add(new SqlParameter("@FromDate", Fromdate));
                    cmd.Parameters.Add(new SqlParameter("@Todate", Todate));

                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {

                        s = new DoctorDashboardClass();
                        s.Name = DBNull.Value.Equals(dr["NAME"]) ? "" : Convert.ToString(dr["NAME"]);
                        s.Id = DBNull.Value.Equals(dr["CountVal"]) ? 0 : Convert.ToInt64(dr["CountVal"]);

                        obj.Add(s);
                    }
                }
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult paneltypecol(string set, string Fromdate, string Todate)
        {
            SqlConnection con = null;
            SqlCommand cmd = null;
            SqlDataReader dr;
            List<DoctorDashboardClass> obj = new List<DoctorDashboardClass>();
            DoctorDashboardClass s;

            using (con = new SqlConnection("Data Source=WEB-SERVER\\SQLEXPRESS;Initial Catalog=Putralive;Integrated Security=True"))
            {
                con.Open();
                con.CreateCommand();
                using (cmd = new SqlCommand("op_panel_collection_type", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@set", set));
                    cmd.Parameters.Add(new SqlParameter("@FromDate", Fromdate));
                    cmd.Parameters.Add(new SqlParameter("@Todate", Todate));

                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {

                        s = new DoctorDashboardClass();
                        s.Name = DBNull.Value.Equals(dr["NAME"]) ? "" : Convert.ToString(dr["NAME"]);
                        s.Id = DBNull.Value.Equals(dr["CountVal"]) ? 0 : Convert.ToInt64(dr["CountVal"]);

                        obj.Add(s);
                    }
                }
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult PatientCategoriesday(string Fromdate, string Todate)
        {
            SqlConnection con = null;
            SqlCommand cmd = null;
            SqlDataReader dr;
            List<DoctorDashboardClass> obj = new List<DoctorDashboardClass>();
            DoctorDashboardClass s;

            using (con = new SqlConnection("Data Source=WEB-SERVER\\SQLEXPRESS;Initial Catalog=Putralive;Integrated Security=True"))
            {
                con.Open();
                con.CreateCommand();
                using (cmd = new SqlCommand("sp_op_panal", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@FromDate", Fromdate));
                    cmd.Parameters.Add(new SqlParameter("@Todate", Todate));

                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {

                        s = new DoctorDashboardClass();
                        s.Name = DBNull.Value.Equals(dr["NAME"]) ? "" : Convert.ToString(dr["NAME"]);
                        s.Id = DBNull.Value.Equals(dr["CountVal"]) ? 0 : Convert.ToInt64(dr["CountVal"]);

                        obj.Add(s);
                    }
                }
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult finalstatus(string Fromdate, string Todate)
        {
            SqlConnection con = null;
            SqlCommand cmd = null;
            SqlDataReader dr;
            List<DoctorDashboardClass> obj = new List<DoctorDashboardClass>();
            DoctorDashboardClass s;

            using (con = new SqlConnection("Data Source=WEB-SERVER\\SQLEXPRESS;Initial Catalog=Putralive;Integrated Security=True"))
            {
                con.Open();
                con.CreateCommand();
                using (cmd = new SqlCommand("op_final_status", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@FromDate", Fromdate));
                    cmd.Parameters.Add(new SqlParameter("@Todate", Todate));

                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {

                        s = new DoctorDashboardClass();
                        s.Name = DBNull.Value.Equals(dr["NAME"]) ? "" : Convert.ToString(dr["NAME"]);
                        s.Id = DBNull.Value.Equals(dr["CountVal"]) ? 0 : Convert.ToInt64(dr["CountVal"]);

                        obj.Add(s);
                    }
                }
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult patientgentercat(string set, string Fromdate, string Todate)
        {
            SqlConnection con = null;
            SqlCommand cmd = null;
            SqlDataReader dr;
            List<DoctorDashboardClass> obj = new List<DoctorDashboardClass>();
            DoctorDashboardClass s;

            using (con = new SqlConnection("Data Source=WEB-SERVER\\SQLEXPRESS;Initial Catalog=Putralive;Integrated Security=True"))
            {
                con.Open();
                con.CreateCommand();
                using (cmd = new SqlCommand("cat_panel_sex", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                   
                    cmd.Parameters.Add(new SqlParameter("@set", set));
                    cmd.Parameters.Add(new SqlParameter("@FromDate", Fromdate));
                    cmd.Parameters.Add(new SqlParameter("@Todate", Todate));
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {

                        s = new DoctorDashboardClass();
                        s.Name = DBNull.Value.Equals(dr["NAME"]) ? "" : Convert.ToString(dr["NAME"]);
                        s.Id = DBNull.Value.Equals(dr["CountVal"]) ? 0 : Convert.ToInt64(dr["CountVal"]);

                        obj.Add(s);
                    }
                }
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult Raceday(string Fromdate, string Todate)
        {
            SqlConnection con = null;
            SqlCommand cmd = null;
            SqlDataReader dr;
            List<DoctorDashboardClass> obj = new List<DoctorDashboardClass>();
            DoctorDashboardClass s;

            using (con = new SqlConnection("Data Source=WEB-SERVER\\SQLEXPRESS;Initial Catalog=Putralive;Integrated Security=True"))
            {
                con.Open();
                con.CreateCommand();
                using (cmd = new SqlCommand("sp_count_race", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@FromDate", Fromdate));
                    cmd.Parameters.Add(new SqlParameter("@Todate", Todate));

                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {

                        s = new DoctorDashboardClass();
                        s.Name = DBNull.Value.Equals(dr["NAME"]) ? "" : Convert.ToString(dr["NAME"]);
                        s.Id = DBNull.Value.Equals(dr["CountVal"]) ? 0 : Convert.ToInt64(dr["CountVal"]);

                        obj.Add(s);
                    }
                }
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult collectiondep(string Fromdate, string Todate)
        {
            SqlConnection con = null;
            SqlCommand cmd = null;
            SqlDataReader dr;
            List<DoctorDashboardClass> obj = new List<DoctorDashboardClass>();
            DoctorDashboardClass s;

            using (con = new SqlConnection("Data Source=WEB-SERVER\\SQLEXPRESS;Initial Catalog=Putralive;Integrated Security=True"))
            {
                con.Open();
                con.CreateCommand();
                using (cmd = new SqlCommand("op_dep_final", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@FromDate", Fromdate));
                    cmd.Parameters.Add(new SqlParameter("@Todate", Todate));

                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {

                        s = new DoctorDashboardClass();
                        s.Name = DBNull.Value.Equals(dr["NAME"]) ? "" : Convert.ToString(dr["NAME"]);
                        s.Id = DBNull.Value.Equals(dr["CountVal"]) ? 0 : Convert.ToInt64(dr["CountVal"]);

                        obj.Add(s);
                    }
                }
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult depositday(string Fromdate, string Todate)
        {
            SqlConnection con = null;
            SqlCommand cmd = null;
            SqlDataReader dr;
            List<DoctorDashboardClass> obj = new List<DoctorDashboardClass>();
            DoctorDashboardClass s;

            using (con = new SqlConnection("Data Source=WEB-SERVER\\SQLEXPRESS;Initial Catalog=Putralive;Integrated Security=True"))
            {
                con.Open();
                con.CreateCommand();
                using (cmd = new SqlCommand("deposit_month_op", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@FromDate", Fromdate));
                    cmd.Parameters.Add(new SqlParameter("@Todate", Todate));

                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {

                        s = new DoctorDashboardClass();
                        s.Name = DBNull.Value.Equals(dr["NAME"]) ? "" : Convert.ToString(dr["NAME"]);
                        s.Id = DBNull.Value.Equals(dr["CountVal"]) ? 0 : Convert.ToInt64(dr["CountVal"]);

                        obj.Add(s);
                    }
                }
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult Panalamt(string Fromdate, string Todate)
        {
            SqlConnection con = null;
            SqlCommand cmd = null;
            SqlDataReader dr;
            List<DoctorDashboardClass> obj = new List<DoctorDashboardClass>();
            DoctorDashboardClass s;

            using (con = new SqlConnection("Data Source=WEB-SERVER\\SQLEXPRESS;Initial Catalog=Putralive;Integrated Security=True"))
            {
                con.Open();
                con.CreateCommand();
                using (cmd = new SqlCommand("panelcliam_sp", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Fromdate", Fromdate));
                    cmd.Parameters.Add(new SqlParameter("@Todate", Todate));

                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {

                        s = new DoctorDashboardClass();
                        s.Name = DBNull.Value.Equals(dr["NAME"]) ? "" : Convert.ToString(dr["NAME"]);
                        s.Id = DBNull.Value.Equals(dr["Countval"]) ? 0 : Convert.ToInt64(dr["Countval"]);

                        obj.Add(s);
                    }
                }
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult billday(string Fromdate, string Todate)
        {
            SqlConnection con = null;
            SqlCommand cmd = null;
            SqlDataReader dr;
            List<DoctorDashboardClass> obj = new List<DoctorDashboardClass>();
            DoctorDashboardClass s;

            using (con = new SqlConnection("Data Source=WEB-SERVER\\SQLEXPRESS;Initial Catalog=Putralive;Integrated Security=True"))
            {
                con.Open();
                con.CreateCommand();
                using (cmd = new SqlCommand("finnal_day_op", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@FromDate", Fromdate));
                    cmd.Parameters.Add(new SqlParameter("@Todate", Todate));

                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {

                        s = new DoctorDashboardClass();
                        s.Name = DBNull.Value.Equals(dr["NAME"]) ? "" : Convert.ToString(dr["NAME"]);
                        s.Id = DBNull.Value.Equals(dr["CountVal"]) ? 0 : Convert.ToInt64(dr["CountVal"]);

                        obj.Add(s);
                    }
                }
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult statusday(string Fromdate, string Todate)
        {
            SqlConnection con = null;
            SqlCommand cmd = null;
            SqlDataReader dr;
            List<DoctorDashboardClass> obj = new List<DoctorDashboardClass>();
            DoctorDashboardClass s;

            using (con = new SqlConnection("Data Source=WEB-SERVER\\SQLEXPRESS;Initial Catalog=Putralive;Integrated Security=True"))
            {
                con.Open();
                con.CreateCommand();
                using (cmd = new SqlCommand("appoiment_day", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@FromDate", Fromdate));
                    cmd.Parameters.Add(new SqlParameter("@Todate", Todate));

                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {

                        s = new DoctorDashboardClass();
                        s.Name = DBNull.Value.Equals(dr["NAME"]) ? "" : Convert.ToString(dr["NAME"]);
                        s.Id = DBNull.Value.Equals(dr["CountVal"]) ? 0 : Convert.ToInt64(dr["CountVal"]);

                        obj.Add(s);
                    }
                }
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult patientfrom(string Fromdate, string Todate)
        {
            SqlConnection con = null;
            SqlCommand cmd = null;
            SqlDataReader dr;
            List<DoctorDashboardClass> obj = new List<DoctorDashboardClass>();
            DoctorDashboardClass s;

            using (con = new SqlConnection("Data Source=WEB-SERVER\\SQLEXPRESS;Initial Catalog=Putralive;Integrated Security=True"))
            {
                con.Open();
                con.CreateCommand();
                using (cmd = new SqlCommand("citizen_sp", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@FromDate", Fromdate));
                    cmd.Parameters.Add(new SqlParameter("@Todate", Todate));

                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {

                        s = new DoctorDashboardClass();
                        s.Name = DBNull.Value.Equals(dr["NAME"]) ? "" : Convert.ToString(dr["NAME"]);
                        s.Id = DBNull.Value.Equals(dr["CountVal"]) ? 0 : Convert.ToInt64(dr["CountVal"]);

                        obj.Add(s);
                    }
                }
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult patientvisttime(string Fromdate)
        {
            SqlConnection con = null;
            SqlCommand cmd = null;
            SqlDataReader dr;
            List<DoctorDashboardClass> obj = new List<DoctorDashboardClass>();
            DoctorDashboardClass s;

            using (con = new SqlConnection("Data Source=WEB-SERVER\\SQLEXPRESS;Initial Catalog=Putralive;Integrated Security=True"))
            {
                con.Open();
                con.CreateCommand();
                using (cmd = new SqlCommand("Appoiment_day_time", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@FromDate", Fromdate));
                    

                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {

                        s = new DoctorDashboardClass();
                        s.Name = DBNull.Value.Equals(dr["NAME"]) ? "" : Convert.ToString(dr["NAME"]);
                        s.Id = DBNull.Value.Equals(dr["CountVal"]) ? 0 : Convert.ToInt64(dr["CountVal"]);

                        obj.Add(s);
                    }
                }
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult Patientlocationday(string Fromdate, string Todate)
        {
            SqlConnection con = null;
            SqlCommand cmd = null;
            SqlDataReader dr;
            List<DoctorDashboardClass> obj = new List<DoctorDashboardClass>();
            DoctorDashboardClass s;

            using (con = new SqlConnection("Data Source=WEB-SERVER\\SQLEXPRESS;Initial Catalog=Putralive;Integrated Security=True"))
            {
                con.Open();
                con.CreateCommand();
                using (cmd = new SqlCommand("sp_location_day", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@FromDate", Fromdate));
                    cmd.Parameters.Add(new SqlParameter("@Todate", Todate));

                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {

                        s = new DoctorDashboardClass();
                        s.Name = DBNull.Value.Equals(dr["NAME"]) ? "" : Convert.ToString(dr["NAME"]);
                        s.Id = DBNull.Value.Equals(dr["CountVal"]) ? 0 : Convert.ToInt64(dr["CountVal"]);

                        obj.Add(s);
                    }
                }
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult refundday(string Fromdate, string Todate)
        {
            SqlConnection con = null;
            SqlCommand cmd = null;
            SqlDataReader dr;
            List<DoctorDashboardClass> obj = new List<DoctorDashboardClass>();
            DoctorDashboardClass s;

            using (con = new SqlConnection("Data Source=WEB-SERVER\\SQLEXPRESS;Initial Catalog=Putralive;Integrated Security=True"))
            {
                con.Open();
                con.CreateCommand();
                using (cmd = new SqlCommand("oprefund_dash", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@FromDate", Fromdate));
                    cmd.Parameters.Add(new SqlParameter("@Todate", Todate));

                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {

                        s = new DoctorDashboardClass();
                        s.Name = DBNull.Value.Equals(dr["NAME"]) ? "" : Convert.ToString(dr["NAME"]);
                        s.Id = DBNull.Value.Equals(dr["CountVal"]) ? 0 : Convert.ToInt64(dr["CountVal"]);

                        obj.Add(s);
                    }
                }
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult panealcollection(string Fromdate, string Todate)
        {
            SqlConnection con = null;
            SqlCommand cmd = null;
            SqlDataReader dr;
            List<DoctorDashboardClass> obj = new List<DoctorDashboardClass>();
            DoctorDashboardClass s;

            using (con = new SqlConnection("Data Source=WEB-SERVER\\SQLEXPRESS;Initial Catalog=Putralive;Integrated Security=True"))
            {
                con.Open();
                con.CreateCommand();
                using (cmd = new SqlCommand("op_panel_collection", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@FromDate", Fromdate));
                    cmd.Parameters.Add(new SqlParameter("@Todate", Todate));

                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {

                        s = new DoctorDashboardClass();
                        s.Name = DBNull.Value.Equals(dr["NAME"]) ? "" : Convert.ToString(dr["NAME"]);
                        s.Id = DBNull.Value.Equals(dr["CountVal"]) ? 0 : Convert.ToInt64(dr["CountVal"]);

                        obj.Add(s);
                    }
                }
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult Registrationtype(string Fromdate, string Todate)
        {
            SqlConnection con = null;
            SqlCommand cmd = null;
            SqlDataReader dr;
            List<DoctorDashboardClass> obj = new List<DoctorDashboardClass>();
            DoctorDashboardClass s;

            using (con = new SqlConnection("Data Source=WEB-SERVER\\SQLEXPRESS;Initial Catalog=Putralive;Integrated Security=True"))
            {
                con.Open();
                con.CreateCommand();
                using (cmd = new SqlCommand("Registration_Type_sp", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@FromDate", Fromdate));
                    cmd.Parameters.Add(new SqlParameter("@Todate", Todate));

                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {

                        s = new DoctorDashboardClass();
                        s.Name = DBNull.Value.Equals(dr["NAME"]) ? "" : Convert.ToString(dr["NAME"]);
                        s.Id = DBNull.Value.Equals(dr["CountVal"]) ? 0 : Convert.ToInt64(dr["CountVal"]);

                        obj.Add(s);
                    }
                }
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult ipcollection(string Fromdate, string Todate)
        {
            SqlConnection con = null;
            SqlCommand cmd = null;
            SqlDataReader dr;
            List<DoctorDashboardClass> obj = new List<DoctorDashboardClass>();
            DoctorDashboardClass s;

            using (con = new SqlConnection("Data Source=WEB-SERVER\\SQLEXPRESS;Initial Catalog=Putralive;Integrated Security=True"))
            {
                con.Open();
                con.CreateCommand();
                using (cmd = new SqlCommand("ip_collection", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@FromDate", Fromdate));
                    cmd.Parameters.Add(new SqlParameter("@Todate", Todate));

                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {

                        s = new DoctorDashboardClass();
                        s.Name = DBNull.Value.Equals(dr["NAME"]) ? "" : Convert.ToString(dr["NAME"]);
                        s.Id = DBNull.Value.Equals(dr["CountVal"]) ? 0 : Convert.ToInt64(dr["CountVal"]);

                        obj.Add(s);
                    }
                }
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult paymenttype(string Fromdate, string Todate)
        {
            SqlConnection con = null;
            SqlCommand cmd = null;
            SqlDataReader dr;
            List<DoctorDashboardClass> obj = new List<DoctorDashboardClass>();
            DoctorDashboardClass s;

            using (con = new SqlConnection("Data Source=WEB-SERVER\\SQLEXPRESS;Initial Catalog=Putralive;Integrated Security=True"))
            {
                con.Open();
                con.CreateCommand();
                using (cmd = new SqlCommand("ip_deposit_pay", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@FromDate", Fromdate));
                    cmd.Parameters.Add(new SqlParameter("@Todate", Todate));

                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {

                        s = new DoctorDashboardClass();
                        s.Name = DBNull.Value.Equals(dr["NAME"]) ? "" : Convert.ToString(dr["NAME"]);
                        s.Id = DBNull.Value.Equals(dr["CountVal"]) ? 0 : Convert.ToInt64(dr["CountVal"]);

                        obj.Add(s);
                    }
                }
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult waitingtime(string Fromdate, string Todate)
        {
            SqlConnection con = null;
            SqlCommand cmd = null;
            SqlDataReader dr;
            List<DoctorDashboardClass> obj = new List<DoctorDashboardClass>();
            DoctorDashboardClass s;

            using (con = new SqlConnection("Data Source=WEB-SERVER\\SQLEXPRESS;Initial Catalog=Putralive;Integrated Security=True"))
            {
                con.Open();
                con.CreateCommand();
                using (cmd = new SqlCommand("patient_waiting_time_bi", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@FromDate", Fromdate));
                    cmd.Parameters.Add(new SqlParameter("@Todate", Todate));

                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {

                        s = new DoctorDashboardClass();
                        s.Name = DBNull.Value.Equals(dr["Patient"]) ? "" : Convert.ToString(dr["Patient"]);
                        s.Id = DBNull.Value.Equals(dr["Minutes"]) ? 0 : Convert.ToInt64(dr["Minutes"]);

                        obj.Add(s);
                    }
                }
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
        }















        public ActionResult Patientlocations(string Fromdate, string Todate)
        {
            DoctorDashboardServiceRef.DoctorDashboardServiceClient serv = new DoctorDashboardServiceRef.DoctorDashboardServiceClient();
            return Json(serv.Patientlocations(Fromdate, Todate), JsonRequestBehavior.AllowGet);
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
