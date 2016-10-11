using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using HealthCareInMvc4.PurchaseOrderServiceRef;
using HealthCareInMvc4.IndentRequestServiceRef;
using HealthCareInMvc4.MainStoreSubStoreServiceRef;
using HealthCareInMvc4.RoleProfileServiceRef;
using System.Configuration;
using System.Data;
using HealthCareInMvc4.Controllers.Common;
using System.ServiceModel;
using HealthCareInMvc4.SalesOrderServicceRef;
using HealthCareInMvc4.Areas.Store.Models;

namespace HealthCareInMvc4.Areas.Store.Controllers
{
    public class DashboardController : Controller
    {
        //
        // GET: /Store/Dashboard/

        string Servicepath = Convert.ToString(System.Web.Configuration.WebConfigurationManager.AppSettings["ServicePath"]);
        // private string ServicePath;
        public static string cs = ConfigurationManager.AppSettings["ConnectionString"];
        SqlConnection conn = null;
        SqlCommand cmd = null;
        SqlDataReader dr;
        #region  Category
        #region All Sub Category


        public ActionResult initial()
        {
            HealthCareInMvc4.RoleProfileServiceRef.RoleProfile objuser = new HealthCareInMvc4.RoleProfileServiceRef.RoleProfile();
            objuser = (HealthCareInMvc4.RoleProfileServiceRef.RoleProfile)Session["UserList"];
            Int64 UserID = objuser.UserID;
            Session["userid"] = UserID;

            Int64 ClientID = objuser.ClientID;
            Session["clientid"] = ClientID;
            MainStoreSubStoreServiceClient objresult = new MainStoreSubStoreServiceClient(RIAGlobal.GetBinding(), new EndpointAddress(RIAGlobal.GetServicePath(Servicepath, "MainStoreSubStoreService.svc")));
            var list = objresult.MainStoreListByModuleID(5, Convert.ToString(ClientID), UserID);
            // ViewBag.list = new SelectList(list.AsEnumerable(), "MainStoreID", "MainStoreName");
            if (list.Count() != 0)
            {
                ViewBag.list = new SelectList(list.AsEnumerable(), "MainStoreID", "MainStoreName", list.FirstOrDefault().MainStoreID);
            }
            else
            {
                ViewBag.list = new SelectList(list.AsEnumerable(), "MainStoreID", "MainStoreName");
            }

            IndentRequestServiceClient indentlist = new IndentRequestServiceClient(RIAGlobal.GetBinding(), new EndpointAddress(RIAGlobal.GetServicePath(Servicepath, "IndentRequestService.svc")));
            var sublist = indentlist.ListSubstore(5, 1, ClientID);
            ViewBag.sublist = new SelectList(sublist.AsEnumerable(), "SubStoreID", "SubStoreName", sublist.FirstOrDefault().SubStoreID);

            ChartViewModel _ChartViewModel = new ChartViewModel();
            _ChartViewModel.SalesOrderProduct = substore1();
            _ChartViewModel.SalesOrderDayWise = daywisesubstpro();



            return View(_ChartViewModel);
        }
        public ActionResult initialSales()
        {
            SqlConnection con = null;
            SqlCommand cmd = null;
            SqlDataReader dr;
            List<PurchaseOrder> li = new List<PurchaseOrder>();
            PurchaseOrder obj1;
            using (con = new SqlConnection(cs))
            {
                con.Open();
                con.CreateCommand();
                using (cmd = new SqlCommand("sp_salesinitial", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        obj1 = new PurchaseOrder();
                        obj1.SubStoreName = Convert.ToString(dr["SubStoreName"]);
                        obj1.InvoiceSubTotal = DBNull.Value.Equals(dr["InvoiceSubTotal"]) ? 0 : Convert.ToInt64(dr["InvoiceSubTotal"]);
                        li.Add(obj1);
                    }
                }
                return Json(li, JsonRequestBehavior.AllowGet);
            }

        }

        public ActionResult initialIncome()
        {
            SqlConnection con = null;
            SqlCommand cmd = null;
            SqlDataReader dr;
            List<PurchaseOrder> li = new List<PurchaseOrder>();
            PurchaseOrder obj1;
            using (con = new SqlConnection(cs))
            {
                con.Open();
                con.CreateCommand();
                using (cmd = new SqlCommand("sp_incomeinitial", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        obj1 = new PurchaseOrder();
                        obj1.IPsales = DBNull.Value.Equals(dr["IPsales"]) ? 0 : Convert.ToInt64(dr["IPsales"]);
                        obj1.Opsales = DBNull.Value.Equals(dr["Opsales"]) ? 0 : Convert.ToInt64(dr["Opsales"]);
                        obj1.Doctorsales = DBNull.Value.Equals(dr["Doctorsales"]) ? 0 : Convert.ToInt64(dr["Doctorsales"]);
                        obj1.Genralsales = DBNull.Value.Equals(dr["Genralsales"]) ? 0 : Convert.ToInt64(dr["Genralsales"]);
                        li.Add(obj1);
                    }
                }
                return Json(li, JsonRequestBehavior.AllowGet);
            }

        }

        public ActionResult initialTosalesProduct()
        {
            SqlConnection con = null;
            SqlCommand cmd = null;
            SqlDataReader dr;
            List<PurchaseOrder> li = new List<PurchaseOrder>();
            PurchaseOrder obj1;
            using (con = new SqlConnection(cs))
            {
                con.Open();
                con.CreateCommand();
                using (cmd = new SqlCommand("Sp_TopsalesProductdash", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        obj1 = new PurchaseOrder();
                        obj1.NAME = Convert.ToString(dr["NAME"]);
                        obj1.TotalCOunt = DBNull.Value.Equals(dr["TotalCOunt"]) ? 0 : Convert.ToInt64(dr["TotalCOunt"]); //Convert.ToInt64(dr["SubTotal"]);

                        //    obj1.value8 = DBNull.Value.Equals(dr["total"]) ? 0 : Convert.ToInt64(dr["total"]); // Convert.ToInt16(dr["ClosedFlag"]);

                        li.Add(obj1);
                    }
                }
                return Json(li, JsonRequestBehavior.AllowGet);
            }
        }


        public ActionResult Index()
        {
            return View();
        }
        public ActionResult MenuNamepartialview()
        {
            return View();
        }
        public ActionResult ADDNEWPARTIALVIEW()
        {
            HealthCareInMvc4.RoleProfileServiceRef.RoleProfile objuser = new HealthCareInMvc4.RoleProfileServiceRef.RoleProfile();
            objuser = (HealthCareInMvc4.RoleProfileServiceRef.RoleProfile)Session["UserList"];
            Int64 UserID = objuser.UserID;
            Session["userid"] = UserID;

            Int64 ClientID = objuser.ClientID;
            Session["clientid"] = ClientID;
            MainStoreSubStoreServiceClient objresult = new MainStoreSubStoreServiceClient(RIAGlobal.GetBinding(), new EndpointAddress(RIAGlobal.GetServicePath(Servicepath, "MainStoreSubStoreService.svc")));
            var list = objresult.MainStoreListByModuleID(5, Convert.ToString(ClientID), UserID);
            ViewBag.list = new SelectList(list.AsEnumerable(), "MainStoreID", "MainStoreName", list.FirstOrDefault().MainStoreID);

            IndentRequestServiceClient indentlist = new IndentRequestServiceClient(RIAGlobal.GetBinding(), new EndpointAddress(RIAGlobal.GetServicePath(Servicepath, "IndentRequestService.svc")));
            var sublist = indentlist.ListSubstore(5, 1, ClientID);
            ViewBag.sublist = new SelectList(sublist.AsEnumerable(), "SubStoreID", "SubStoreName", sublist.FirstOrDefault().SubStoreID);

            return View();
        }
        public ActionResult Gridpartialview()
        {
            //var d1=
            //return Json(new{data1=d1,data2=d2}
            //)
            return View();
        }

        public ActionResult Purchase(string Fromdate, string Todate)
        {
            SqlConnection con = null;
            SqlCommand cmd = null;
            SqlDataReader dr;
            List<PurchaseOrder> li = new List<PurchaseOrder>();
            PurchaseOrder obj1;
            using (con = new SqlConnection(cs))
            {
                con.Open();
                con.CreateCommand();
                using (cmd = new SqlCommand("sp_Purchase", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@FromDate", Fromdate));
                    cmd.Parameters.Add(new SqlParameter("@ToDate", Todate));
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        obj1 = new PurchaseOrder();
                        obj1.POAprroved = DBNull.Value.Equals(dr["POAprroved"]) ? 0 : Convert.ToInt64(dr["POAprroved"]); //Convert.ToInt64(dr["SubTotal"]);
                        obj1.PORejected = DBNull.Value.Equals(dr["PORejected"]) ? 0 : Convert.ToInt64(dr["PORejected"]); // Convert.ToString(dr["PORejected"]);
                        obj1.POPending = DBNull.Value.Equals(dr["POPending"]) ? 0 : Convert.ToInt64(dr["POPending"]);// Convert.ToString(dr["POPending"]);

                        //    obj1.value8 = DBNull.Value.Equals(dr["total"]) ? 0 : Convert.ToInt64(dr["total"]); // Convert.ToInt16(dr["ClosedFlag"]);

                        li.Add(obj1);
                    }
                }
                return Json(li, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult TosalesProduct(string Fromdate, string Todate)
        {
            SqlConnection con = null;
            SqlCommand cmd = null;
            SqlDataReader dr;
            List<PurchaseOrder> li = new List<PurchaseOrder>();
            PurchaseOrder obj1;
            using (con = new SqlConnection(cs))
            {
                con.Open();
                con.CreateCommand();
                using (cmd = new SqlCommand("Sp_TopsalesProduct", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@FromDate", Fromdate));
                    cmd.Parameters.Add(new SqlParameter("@ToDate", Todate));
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        obj1 = new PurchaseOrder();
                        obj1.NAME = Convert.ToString(dr["NAME"]);
                        obj1.TotalCOunt = DBNull.Value.Equals(dr["TotalCOunt"]) ? 0 : Convert.ToInt64(dr["TotalCOunt"]); //Convert.ToInt64(dr["SubTotal"]);

                        //    obj1.value8 = DBNull.Value.Equals(dr["total"]) ? 0 : Convert.ToInt64(dr["total"]); // Convert.ToInt16(dr["ClosedFlag"]);

                        li.Add(obj1);
                    }
                }
                return Json(li, JsonRequestBehavior.AllowGet);
            }
        }




        //public ActionResult PurchaseReturn(string Fromdate, string Todate)
        //{
        //    SqlConnection con = null;
        //    SqlCommand cmd = null;
        //    SqlDataReader dr;
        //    List<PurchaseOrder> li = new List<PurchaseOrder>();
        //    PurchaseOrder obj1;
        //    using (con = new SqlConnection(cs))
        //    {
        //        con.Open();
        //        con.CreateCommand();
        //        using (cmd = new SqlCommand("sp_Purchasreturn", con))
        //        {
        //            cmd.CommandType = CommandType.StoredProcedure;
        //            cmd.Parameters.Add(new SqlParameter("@FromDate", Fromdate));
        //            cmd.Parameters.Add(new SqlParameter("@ToDate", Todate));
        //            dr = cmd.ExecuteReader();
        //            while (dr.Read())
        //            {
        //                obj1 = new PurchaseOrder();
        //                obj1.SubTotal = DBNull.Value.Equals(dr["Totalamountofpurreturn"]) ? 0 : Convert.ToInt64(dr["Totalamountofpurreturn"]); //Convert.ToInt64(dr["Totalamountofpurreturn"]);                       
        //                li.Add(obj1);
        //            }
        //        }
        //        return Json(li, JsonRequestBehavior.AllowGet);
        //    }
        //}


        public ActionResult GRN(string Fromdate, string Todate)
        {
            SqlConnection con = null;
            SqlCommand cmd = null;
            SqlDataReader dr;
            List<PurchaseOrder> li = new List<PurchaseOrder>();
            PurchaseOrder obj1;
            using (con = new SqlConnection(cs))
            {
                con.Open();
                con.CreateCommand();
                using (cmd = new SqlCommand("sp_grn", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@FromDate", Fromdate));
                    cmd.Parameters.Add(new SqlParameter("@ToDate", Todate));
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        obj1 = new PurchaseOrder();
                        obj1.NAME = Convert.ToString(dr["NAME"]);
                        obj1.TotalGrnRecieved = DBNull.Value.Equals(dr["TotalGrnRecieved"]) ? 0 : Convert.ToInt64(dr["TotalGrnRecieved"]);  // Convert.ToInt64(dr["SubTotal"]);
                        obj1.Subtotal = DBNull.Value.Equals(dr["Subtotal"]) ? 0 : Convert.ToInt64(dr["Subtotal"]);
                        li.Add(obj1);
                    }
                }
                return Json(li, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult topboughtproducts(string Fromdate, string Todate)
        {
            SqlConnection con = null;
            SqlCommand cmd = null;
            SqlDataReader dr;
            List<PurchaseOrder> li = new List<PurchaseOrder>();
            PurchaseOrder obj1;
            using (con = new SqlConnection(cs))
            {
                con.Open();
                con.CreateCommand();
                using (cmd = new SqlCommand("sp_topboughtproducts", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@FromDate", Fromdate));
                    cmd.Parameters.Add(new SqlParameter("@ToDate", Todate));
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        obj1 = new PurchaseOrder();
                        obj1.Productname = Convert.ToString(dr["Productname"]);
                        obj1.TotalCOunt = DBNull.Value.Equals(dr["Totalcount"]) ? 0 : Convert.ToInt64(dr["Totalcount"]);  // Convert.ToInt64(dr["SubTotal"]);
                        // obj1.Subtotal = DBNull.Value.Equals(dr["Subtotal"]) ? 0 : Convert.ToInt64(dr["Subtotal"]);
                        li.Add(obj1);
                    }
                }
                return Json(li, JsonRequestBehavior.AllowGet);
            }
        }





        //public ActionResult poclosed(String Fromdate, String Todate)
        //{
        //    SqlConnection con = null;
        //    SqlCommand cmd = null;
        //    SqlDataReader dr;
        //    List<PurchaseOrder> li = new List<PurchaseOrder>();
        //    PurchaseOrder obj1;
        //    using (con = new SqlConnection(cs))
        //    {
        //        con.Open();
        //        con.CreateCommand();
        //        using (cmd = new SqlCommand("sp_poclosed", con))
        //        {
        //            cmd.CommandType = CommandType.StoredProcedure;
        //            cmd.Parameters.Add(new SqlParameter("@fromdate", Fromdate));
        //            cmd.Parameters.Add(new SqlParameter("@todate", Todate));
        //            dr = cmd.ExecuteReader();
        //            while (dr.Read())
        //            {
        //                obj1 = new PurchaseOrder();
        //                obj1.VendorName = Convert.ToString(dr["VendorName"]);
        //                obj1.TotalPOCLosed = DBNull.Value.Equals(dr["TotalPOCLosed"]) ? 0 : Convert.ToInt64(dr["TotalPOCLosed"]); // Convert.ToInt64(dr["total"]);
        //                obj1.Subtotal = DBNull.Value.Equals(dr["Subtotal"]) ? 0 : Convert.ToInt64(dr["Subtotal"]); //Convert.ToInt64(dr["val"]);
        //                li.Add(obj1);
        //            }
        //        }
        //        return Json(li, JsonRequestBehavior.AllowGet);
        //    }
        //}



        public ActionResult Loss(String Fromdate, String Todate)
        {
            SqlConnection conn = null;
            SqlCommand cmd = null;
            SqlDataReader dr = null;
            List<PurchaseOrder> li = new List<PurchaseOrder>();
            PurchaseOrder obj1;
            using (conn = new SqlConnection(cs))
            {
                conn.Open();
                conn.CreateCommand();
                using (cmd = new SqlCommand("Sp_BrokenandExpiredtotal_JRP", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@FromDate", Fromdate);
                    cmd.Parameters.AddWithValue("@ToDate", Todate);
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        obj1 = new PurchaseOrder();
                        obj1.NAME = Convert.ToString(dr["NAME"]);
                        obj1.TotalExpenses = DBNull.Value.Equals(dr["TotalExpenses"]) ? 0 : Convert.ToInt64(dr["TotalExpenses"]);
                        li.Add(obj1);
                    }
                }

                return Json(li, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Sales(String Fromdate, String Todate)
        {
            SqlConnection con = null;
            SqlCommand cmd = null;
            SqlDataReader dr;
            List<PurchaseOrder> li = new List<PurchaseOrder>();
            PurchaseOrder obj1;
            using (con = new SqlConnection(cs))
            {
                con.Open();
                con.CreateCommand();
                using (cmd = new SqlCommand("sp_sales", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@fromdate", Fromdate));
                    cmd.Parameters.Add(new SqlParameter("@todate", Todate));
                    // cmd.Parameters.Add(new SqlParameter("@value", value));
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        obj1 = new PurchaseOrder();
                        obj1.SubStoreName = Convert.ToString(dr["SubStoreName"]);
                        // obj1.IPsales = DBNull.Value.Equals(dr["IPsales"]) ? 0 : Convert.ToInt64(dr["IPsales"]);
                        //obj1.Opsales = DBNull.Value.Equals(dr["Opsales"]) ? 0 : Convert.ToInt64(dr["Opsales"]);
                        // obj1.Doctorsales = DBNull.Value.Equals(dr["Doctorsales"]) ? 0 : Convert.ToInt64(dr["Doctorsales"]);
                        obj1.InvoiceSubTotal = DBNull.Value.Equals(dr["InvoiceSubTotal"]) ? 0 : Convert.ToInt64(dr["InvoiceSubTotal"]);
                        li.Add(obj1);
                    }
                }
                return Json(li, JsonRequestBehavior.AllowGet);
            }

        }

        public ActionResult Indentrequest(String Fromdate, String Todate)
        {
            SqlConnection con = null;
            SqlCommand cmd = null;
            SqlDataReader dr;
            List<PurchaseOrder> li = new List<PurchaseOrder>();
            PurchaseOrder obj1;
            using (con = new SqlConnection(cs))
            {
                con.Open();
                con.CreateCommand();
                using (cmd = new SqlCommand("sp_indentrequest", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@fromdate", Fromdate));
                    cmd.Parameters.Add(new SqlParameter("@todate", Todate));
                    // cmd.Parameters.Add(new SqlParameter("@value", value));
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        obj1 = new PurchaseOrder();
                        obj1.SubStoreName = Convert.ToString(dr["SubStoreName"]);
                        obj1.TotalRequest = DBNull.Value.Equals(dr["TotalRequest"]) ? 0 : Convert.ToInt64(dr["TotalRequest"]);
                        li.Add(obj1);
                    }
                }
                return Json(li, JsonRequestBehavior.AllowGet);
            }

        }

        //public ActionResult Income(String Fromdate, String Todate)
        //{
        //    SqlConnection con = null;
        //    SqlCommand cmd = null;
        //    SqlDataReader dr;
        //    List<PurchaseOrder> li = new List<PurchaseOrder>();
        //    PurchaseOrder obj1;
        //    using (con = new SqlConnection(cs))
        //    {
        //        con.Open();
        //        con.CreateCommand();
        //        using (cmd = new SqlCommand("sp_income", con))
        //        {
        //            cmd.CommandType = CommandType.StoredProcedure;
        //            cmd.Parameters.Add(new SqlParameter("@fromdate", Fromdate));
        //            cmd.Parameters.Add(new SqlParameter("@todate", Todate));
        //            //  cmd.Parameters.Add(new SqlParameter("@value", value));
        //            dr = cmd.ExecuteReader();
        //            while (dr.Read())
        //            {
        //                obj1 = new PurchaseOrder();
        //                obj1.LocationID = DBNull.Value.Equals(dr["total"]) ? 0 : Convert.ToInt64(dr["total"]);
        //                li.Add(obj1);
        //            }
        //        }
        //        return Json(li, JsonRequestBehavior.AllowGet);
        //    }

        //}

        public ActionResult Income(String Fromdate, String Todate)
        {
            SqlConnection con = null;
            SqlCommand cmd = null;
            SqlDataReader dr;
            List<PurchaseOrder> li = new List<PurchaseOrder>();
            PurchaseOrder obj1;
            using (con = new SqlConnection(cs))
            {
                con.Open();
                con.CreateCommand();
                using (cmd = new SqlCommand("sp_income", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@fromdate", Fromdate));
                    cmd.Parameters.Add(new SqlParameter("@todate", Todate));
                    // cmd.Parameters.Add(new SqlParameter("@value", value));
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        obj1 = new PurchaseOrder();
                        obj1.IPsales = DBNull.Value.Equals(dr["IPsales"]) ? 0 : Convert.ToInt64(dr["IPsales"]);
                        obj1.Opsales = DBNull.Value.Equals(dr["Opsales"]) ? 0 : Convert.ToInt64(dr["Opsales"]);
                        obj1.Doctorsales = DBNull.Value.Equals(dr["Doctorsales"]) ? 0 : Convert.ToInt64(dr["Doctorsales"]);
                        obj1.Genralsales = DBNull.Value.Equals(dr["Genralsales"]) ? 0 : Convert.ToInt64(dr["Genralsales"]);
                        li.Add(obj1);
                    }
                }
                return Json(li, JsonRequestBehavior.AllowGet);
            }

        }


        #endregion

        #region dashboard Ado.net

        #region Default Dashboard
        public List<SalesOrder> substore1()
        {
            List<SalesOrder> obj = new List<SalesOrder>();
            SqlConnection conn = null;
            SqlCommand cmd = null;
            SqlDataReader dr;
            using (conn = new SqlConnection(cs))
            {
                conn.Open();
                conn.CreateCommand();
                using (cmd = new SqlCommand("sp_storesales", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        SalesOrder obj1 = new SalesOrder();
                        obj1.BaseEntityName = DBNull.Value.Equals(dr["InvoiceSubTotal"]) ? "" : Convert.ToString(dr["SubStoreName"]);
                        obj1.BillAmount = DBNull.Value.Equals(dr["InvoiceSubTotal"]) ? 0.0m : Convert.ToDecimal(dr["InvoiceSubTotal"]);
                        obj.Add(obj1);

                    }
                }
            }
            return obj;
        }




        public ActionResult substore2()
        {
            SqlConnection con = null;
            SqlCommand cmd = null;
            SqlDataReader dr;
            List<SalesOrder> li = new List<SalesOrder>();
            SalesOrder obj1;
            using (con = new SqlConnection(cs))
            {
                con.Open();
                con.CreateCommand();
                using (cmd = new SqlCommand("sp_storesales", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        obj1 = new SalesOrder();
                        obj1.BillingState = DBNull.Value.Equals(dr["InvoiceSubTotal"]) ? "" : Convert.ToString(dr["SubStoreName"]);
                        obj1.BillAmount = DBNull.Value.Equals(dr["InvoiceSubTotal"]) ? 0.0m : Convert.ToDecimal(dr["InvoiceSubTotal"]);


                        li.Add(obj1);
                    }
                }
                return Json(li, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult daysubstore()
        {
            SqlConnection con = null;
            SqlCommand cmd = null;
            SqlDataReader dr;
            List<SalesOrder> li = new List<SalesOrder>();
            SalesOrder obj1;
            using (con = new SqlConnection(cs))
            {
                con.Open();
                con.CreateCommand();
                using (cmd = new SqlCommand("Sp_Datewisesale", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {

                        obj1 = new SalesOrder();
                        obj1.Clientname = DBNull.Value.Equals(dr["Date1"]) ? "" : Convert.ToString(dr["Date1"]);
                        obj1.BillAmount = DBNull.Value.Equals(dr["Total"]) ? 0.0m : Convert.ToDecimal(dr["Total"]);


                        li.Add(obj1);
                    }
                }
                return Json(li, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult yearsubstore()
        {
            SqlConnection con = null;
            SqlCommand cmd = null;
            SqlDataReader dr;
            List<SalesOrder> li = new List<SalesOrder>();
            SalesOrder obj1;
            using (con = new SqlConnection(cs))
            {
                con.Open();
                con.CreateCommand();
                using (cmd = new SqlCommand("sp_yearwisesales", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {

                        obj1 = new SalesOrder();

                        obj1.BillAmount = DBNull.Value.Equals(dr["Totalsales"]) ? 0.0m : Convert.ToDecimal(dr["Totalsales"]);
                        obj1.InvoiceBalance = DBNull.Value.Equals(dr["TotalPO"]) ? 0.0m : Convert.ToDecimal(dr["TotalPO"]);


                        li.Add(obj1);
                    }
                }
                return Json(li, JsonRequestBehavior.AllowGet);
            }
        }
        public List<SalesOrder> daywisesubstpro()
        {
            SqlConnection con = null;
            SqlCommand cmd = null;
            SqlDataReader dr;
            List<SalesOrder> li = new List<SalesOrder>();
            SalesOrder obj1;
            using (con = new SqlConnection(cs))
            {
                con.Open();
                con.CreateCommand();
                using (cmd = new SqlCommand("Sp_TopsoldProductdatewise", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        string dat;
                        obj1 = new SalesOrder();
                        obj1.Bankid = DBNull.Value.Equals(dr["NAME"]) ? "" : Convert.ToString(dr["NAME"]);
                        obj1.Clientname = DBNull.Value.Equals(dr["SubStoreName"]) ? "" : Convert.ToString(dr["SubStoreName"]);
                        obj1.BillAmount = DBNull.Value.Equals(dr["Total"]) ? 0.0m : Convert.ToDecimal(dr["Total"]);
                        dat = DBNull.Value.Equals(dr["Salesdate"]) ? "" : Convert.ToString(dr["Salesdate"]);
                        if (dat == "")
                        {
                            obj1.Age = dat;
                        }
                        else
                        {
                            string[] arrDate = dat.Split('-');
                            string day = arrDate[2].ToString();
                            string month = arrDate[1].ToString();
                            string year = arrDate[0].ToString();
                            obj1.Age = day + "-" + month + "-" + year;
                        }
                        li.Add(obj1);
                    }
                }
                return li;
            }
        }






        #endregion

        #region sales Dashboard

        #endregion

        #region Purchase Dashboard

        #endregion

        #endregion

        #region All partial view for dashboard


        public PartialViewResult _VendorWise()
        {
            return PartialView();
        }


        #endregion

        #endregion

        //.......PatientCategory.......//
        #region PatientCategory

        public ActionResult PatientCategory()
        {
            HealthCareInMvc4.RoleProfileServiceRef.RoleProfile objuser = new HealthCareInMvc4.RoleProfileServiceRef.RoleProfile();
            objuser = (HealthCareInMvc4.RoleProfileServiceRef.RoleProfile)Session["UserList"];
            Int64 UserID = objuser.UserID;
            Session["userid"] = UserID;

            Int64 ClientID = objuser.ClientID;
            Session["clientid"] = ClientID;
            MainStoreSubStoreServiceClient objresult = new MainStoreSubStoreServiceClient(RIAGlobal.GetBinding(), new EndpointAddress(RIAGlobal.GetServicePath(Servicepath, "MainStoreSubStoreService.svc")));
            var list = objresult.MainStoreListByModuleID(5, Convert.ToString(ClientID), UserID);
            ViewBag.list = new SelectList(list.AsEnumerable(), "MainStoreID", "MainStoreName", list.FirstOrDefault().MainStoreID);

            IndentRequestServiceClient indentlist = new IndentRequestServiceClient(RIAGlobal.GetBinding(), new EndpointAddress(RIAGlobal.GetServicePath(Servicepath, "IndentRequestService.svc")));
            var sublist = indentlist.ListSubstore(5, 1, ClientID);
            ViewBag.sublist = new SelectList(sublist.AsEnumerable(), "SubStoreID", "SubStoreName", sublist.FirstOrDefault().SubStoreID);

            ChartViewModel _ChartViewModel = new ChartViewModel();
            _ChartViewModel.paitents1 = paitents11();
            _ChartViewModel.paitents2 = paitents12();
            return View(_ChartViewModel);
        }
        public ActionResult Paitentcatagory(string Fromdate, string Todate)
        {
            SqlConnection con = null;
            SqlCommand cmd = null;
            SqlDataReader dr;
            List<SalesOrder> li = new List<SalesOrder>();
            SalesOrder obj1;
            using (con = new SqlConnection(cs))
            {
                con.Open();
                con.CreateCommand();
                using (cmd = new SqlCommand("sp_Paitentcatagory", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@fromdate", Fromdate));
                    cmd.Parameters.Add(new SqlParameter("@todate", Todate));
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        obj1 = new SalesOrder();
                        obj1.Amount = DBNull.Value.Equals(dr["Ip"]) ? 0 : Convert.ToInt64(dr["ip"]);
                        obj1.AmountPaid = DBNull.Value.Equals(dr["OP"]) ? 0 : Convert.ToInt64(dr["OP"]);
                        obj1.BillAmount = DBNull.Value.Equals(dr["Do"]) ? 0 : Convert.ToInt64(dr["Do"]);
                        obj1.Balance = DBNull.Value.Equals(dr["General"]) ? 0 : Convert.ToInt64(dr["General"]);
                        li.Add(obj1);
                    }
                }
                return Json(li, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult Paitentcatagory1(string Fromdate, string Todate)
        {
            SqlConnection con = null;
            SqlCommand cmd = null;
            SqlDataReader dr;
            List<SalesOrder> li = new List<SalesOrder>();
            SalesOrder obj1;
            using (con = new SqlConnection(cs))
            {
                con.Open();
                con.CreateCommand();
                using (cmd = new SqlCommand("Paitentcatagory1", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@fromdate", Fromdate));
                    cmd.Parameters.Add(new SqlParameter("@todate", Todate));
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        obj1 = new SalesOrder();

                        obj1.Amount = DBNull.Value.Equals(dr["Ip"]) ? 0 : Convert.ToInt64(dr["ip"]);
                        obj1.AmountPaid = DBNull.Value.Equals(dr["OP"]) ? 0 : Convert.ToInt64(dr["OP"]);
                        obj1.BillAmount = DBNull.Value.Equals(dr["Do"]) ? 0 : Convert.ToInt64(dr["Do"]);
                        obj1.Balance = DBNull.Value.Equals(dr["General"]) ? 0 : Convert.ToInt64(dr["General"]);
                        obj1.Barcode1 = DBNull.Value.Equals(dr["Ip1"]) ? 0 : Convert.ToInt64(dr["ip1"]);
                        obj1.ClientID = DBNull.Value.Equals(dr["OP1"]) ? 0 : Convert.ToInt64(dr["OP1"]);
                        obj1.ClientId1 = DBNull.Value.Equals(dr["Do1"]) ? 0 : Convert.ToInt64(dr["Do1"]);
                        obj1.CustomerID = DBNull.Value.Equals(dr["General1"]) ? 0 : Convert.ToInt16(dr["General1"]);
                        obj1.Clientname = DBNull.Value.Equals(dr["datee"]) ? "" : Convert.ToString(dr["datee"]);
                        obj1.Date = DBNull.Value.Equals(dr["datee1"]) ? "" : Convert.ToString(dr["datee1"]);
                   //     obj1.rate
                        obj1.Ip2 = DBNull.Value.Equals(dr["Ip2"]) ? 0 : Convert.ToInt64(dr["ip2"]);
                        obj1.Op2 = DBNull.Value.Equals(dr["OP2"]) ? 0 : Convert.ToInt64(dr["OP2"]);
                        obj1.Do2 = DBNull.Value.Equals(dr["Do2"]) ? 0 : Convert.ToInt64(dr["Do2"]);
                        obj1.General2 = DBNull.Value.Equals(dr["General2"]) ? 0 : Convert.ToInt64(dr["General2"]);
                        obj1.Ipp = DBNull.Value.Equals(dr["Ip3"]) ? 0 : Convert.ToInt64(dr["ip3"]);
                        obj1.Opp = DBNull.Value.Equals(dr["OP3"]) ? 0 : Convert.ToInt64(dr["OP3"]);
                        obj1.Do3 = DBNull.Value.Equals(dr["Do3"]) ? 0 : Convert.ToInt64(dr["Do3"]);
                        obj1.General3 = DBNull.Value.Equals(dr["General3"]) ? 0 : Convert.ToInt64(dr["General3"]);
                        obj1.Custom1 = DBNull.Value.Equals(dr["datee2"]) ? "" : Convert.ToString(dr["datee2"]);
                        obj1.Custom2 = DBNull.Value.Equals(dr["datee3"]) ? "" : Convert.ToString(dr["datee3"]);
                        li.Add(obj1);
                    }
                }
                return Json(li, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult Paitentcatag1()
        {
            SqlConnection con = null;
            SqlCommand cmd = null;
            SqlDataReader dr;
            List<SalesOrder> li = new List<SalesOrder>();
            SalesOrder obj1;
            using (con = new SqlConnection(cs))
            {
                con.Open();
                con.CreateCommand();
                using (cmd = new SqlCommand("sp_salescategory", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;                   
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        obj1 = new SalesOrder();
                        obj1.Amount = DBNull.Value.Equals(dr["Ip"]) ? 0 : Convert.ToInt64(dr["ip"]);
                        obj1.AmountPaid = DBNull.Value.Equals(dr["OP"]) ? 0 : Convert.ToInt64(dr["OP"]);
                        obj1.BillAmount = DBNull.Value.Equals(dr["Do"]) ? 0 : Convert.ToInt64(dr["Do"]);
                        obj1.Balance = DBNull.Value.Equals(dr["General"]) ? 0 : Convert.ToInt64(dr["General"]);
                        li.Add(obj1);
                    }
                }
                return Json(li, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult Paitentcatag2()
        {
            SqlConnection con = null;
            SqlCommand cmd = null;
            SqlDataReader dr;
            List<SalesOrder> li = new List<SalesOrder>();
            SalesOrder obj1;
            using (con = new SqlConnection(cs))
            {
                con.Open();
                con.CreateCommand();
                using (cmd = new SqlCommand("sp_Paitentcatag", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;                   
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        obj1 = new SalesOrder();

                        obj1.Amount = DBNull.Value.Equals(dr["Ip"]) ? 0 : Convert.ToInt64(dr["ip"]);
                        obj1.AmountPaid = DBNull.Value.Equals(dr["OP"]) ? 0 : Convert.ToInt64(dr["OP"]);
                        obj1.BillAmount = DBNull.Value.Equals(dr["Do"]) ? 0 : Convert.ToInt64(dr["Do"]);
                        obj1.Balance = DBNull.Value.Equals(dr["General"]) ? 0 : Convert.ToInt64(dr["General"]);
                        obj1.Barcode1 = DBNull.Value.Equals(dr["Ip1"]) ? 0 : Convert.ToInt64(dr["ip1"]);
                        obj1.ClientID = DBNull.Value.Equals(dr["OP1"]) ? 0 : Convert.ToInt64(dr["OP1"]);
                        obj1.ClientId1 = DBNull.Value.Equals(dr["Do1"]) ? 0 : Convert.ToInt64(dr["Do1"]);
                        obj1.CustomerID = DBNull.Value.Equals(dr["General1"]) ? 0 : Convert.ToInt16(dr["General1"]);
                        obj1.Date = DBNull.Value.Equals(dr["date1"]) ? "" : Convert.ToString(dr["date1"]);
                        obj1.Custom1 = DBNull.Value.Equals(dr["date2"]) ? "" : Convert.ToString(dr["date2"]);
                        obj1.Custom2 = DBNull.Value.Equals(dr["date3"]) ? "" : Convert.ToString(dr["date3"]);
                        obj1.Custom4 = DBNull.Value.Equals(dr["date4"]) ? "" : Convert.ToString(dr["date4"]);
                        
                        obj1.Ip2 = DBNull.Value.Equals(dr["Ip2"]) ? 0 : Convert.ToInt64(dr["ip2"]);
                        obj1.Op2 = DBNull.Value.Equals(dr["OP2"]) ? 0 : Convert.ToInt64(dr["OP2"]);
                        obj1.Do2 = DBNull.Value.Equals(dr["Do2"]) ? 0 : Convert.ToInt64(dr["Do2"]);
                        obj1.General2 = DBNull.Value.Equals(dr["General2"]) ? 0 : Convert.ToInt64(dr["General2"]);
                        obj1.Ipp = DBNull.Value.Equals(dr["Ip3"]) ? 0 : Convert.ToInt64(dr["ip3"]);
                        obj1.Opp = DBNull.Value.Equals(dr["OP3"]) ? 0 : Convert.ToInt64(dr["OP3"]);
                        obj1.Do3 = DBNull.Value.Equals(dr["Do3"]) ? 0 : Convert.ToInt64(dr["Do3"]);
                        obj1.General3 = DBNull.Value.Equals(dr["General3"]) ? 0 : Convert.ToInt64(dr["General3"]);
                        li.Add(obj1);
                    }
                }
                return Json(li, JsonRequestBehavior.AllowGet);
            }
        }

        public List<SalesOrder> paitents11()
        {
            List<SalesOrder> obj = new List<SalesOrder>();
            SqlConnection conn = null;
            SqlCommand cmd = null;
            SqlDataReader dr;
            using (conn = new SqlConnection(cs))
            {
                conn.Open();
                conn.CreateCommand();
                //using (cmd = new SqlCommand("select (select count(CustomerCategory) as CustomerCategory  from SO_SalesOrder where CreatedDate between  convert(datetime, '2015-06-16 08:25:46.693',102) and convert(datetime,'2015-06-16 13:50:11.970',102) and CustomerCategory=0) as Ip,(select count(CustomerCategory) as CustomerCategory  from SO_SalesOrder where  CustomerCategory =0 and CreatedDate between  convert(datetime, '2015-06-16 08:25:46.693',102) and convert(datetime,'2015-06-16 13:50:11.970',102)) as OP,(select count(CustomerCategory) as CustomerCategory  from SO_SalesOrder where  CustomerCategory =2 and CreatedDate between  convert(datetime, '2015-06-16 08:25:46.693',102) and convert(datetime,'2015-06-16 13:50:11.970',102)) as Do,(select count(CustomerCategory) as CustomerCategory  from SO_SalesOrder where  CustomerCategory =3 and CreatedDate between  convert(datetime, '2015-06-16 08:25:46.693',102) and convert(datetime,'2015-06-16 13:50:11.970',102)) as General", conn))
                using (cmd = new SqlCommand("sp_salesordet1", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        SalesOrder obj1 = new SalesOrder();
                        obj1.Amount = DBNull.Value.Equals(dr["ip"]) ? 0 : Convert.ToInt64(dr["ip"]);
                        obj1.AmountPaid = DBNull.Value.Equals(dr["OP"]) ? 0 : Convert.ToInt64(dr["OP"]);
                        obj1.BillAmount = DBNull.Value.Equals(dr["Do"]) ? 0 : Convert.ToInt64(dr["Do"]);
                        obj1.Balance = DBNull.Value.Equals(dr["General"]) ? 0 : Convert.ToInt64(dr["General"]);
                        obj.Add(obj1);
                    }
                }
            }
            return obj;
        }
        public List<SalesOrder> paitents12()
        {
            List<SalesOrder> obj = new List<SalesOrder>();
            SqlConnection conn = null;
            SqlCommand cmd = null;
            SqlDataReader dr;
            //var from = "2015-06-16";
            //var to = "2015-06-30";

            using (conn = new SqlConnection(cs))
            {
                conn.Open();
                conn.CreateCommand();
//                using (cmd = new SqlCommand("select (select sum(InvoiceSubTotal) as CustomerCategory  from SO_SalesOrder where CreatedDate between  convert(datetime, @fromdate,102) and convert(datetime,@todate,102) and CustomerCategory=0) as Ip,(select sum(InvoiceSubTotal) as CustomerCategory  from SO_SalesOrder where  CustomerCategory =0 and CreatedDate between  convert(datetime, @fromdate,102) and convert(datetime,@todate,102)) as OP,(select sum(InvoiceSubTotal) as CustomerCategory  from SO_SalesOrder where  CustomerCategory =2 and CreatedDate between  convert(datetime, @fromdate,102) and convert(datetime,@todate,102)) as Do,(select sum(InvoiceSubTotal) as CustomerCategory  from SO_SalesOrder where  CustomerCategory =3 and CreatedDate between  convert(datetime, @fromdate,102) and convert(datetime,@todate,102)) as General,(select sum(InvoiceSubTotal) as CustomerCategory  from SO_SalesOrder where CreatedDate between  convert(datetime, DATEADD(day, 1, @fromdate),102) and convert(datetime,@todate,102) and CustomerCategory=0) as Ip1,(select sum(InvoiceSubTotal) as CustomerCategory  from SO_SalesOrder where  CustomerCategory =0 and CreatedDate between  convert(datetime, DATEADD(day, 1, @fromdate),102) and convert(datetime,@todate,102)) as OP1,(select sum(InvoiceSubTotal) as CustomerCategory  from SO_SalesOrder where  CustomerCategory =2 and CreatedDate between  convert(datetime, DATEADD(day, 1, @fromdate),102) and convert(datetime,@todate,102)) as Do1,(select sum(InvoiceSubTotal) as CustomerCategory  from SO_SalesOrder where  CustomerCategory =3 and CreatedDate between  convert(datetime, DATEADD(day, 1, @fromdate),102) and convert(datetime,@todate,102)) as General1,(select sum(InvoiceSubTotal) as CustomerCategory  from SO_SalesOrder where CreatedDate between  convert(datetime, DATEADD(day, 2, @fromdate),102) and convert(datetime,@todate,102) and CustomerCategory=0) as Ip2,(select sum(InvoiceSubTotal) as CustomerCategory  from SO_SalesOrder where  CustomerCategory =0 and CreatedDate between  convert(datetime, DATEADD(day, 2, @fromdate),102) and convert(datetime,@todate,102)) as OP2,(select sum(InvoiceSubTotal) as CustomerCategory  from SO_SalesOrder where  CustomerCategory =2 and CreatedDate between  convert(datetime, DATEADD(day, 2, @fromdate),102) and convert(datetime,@todate,102)) as Do2,(select sum(InvoiceSubTotal) as CustomerCategory  from SO_SalesOrder where  CustomerCategory =3 and CreatedDate between  convert(datetime, DATEADD(day, 2, @fromdate),102) and convert(datetime,@todate,102)) as General2,(select sum(InvoiceSubTotal) as CustomerCategory  from SO_SalesOrder where CreatedDate between  convert(datetime, DATEADD(day, 3, @fromdate),102) and convert(datetime,@todate,102) and CustomerCategory=0) as Ip3,(select sum(InvoiceSubTotal) as CustomerCategory  from SO_SalesOrder where  CustomerCategory =0 and CreatedDate between  convert(datetime, DATEADD(day, 3, @fromdate),102) and convert(datetime,@todate,102)) as OP3,(select sum(InvoiceSubTotal) as CustomerCategory  from SO_SalesOrder where  CustomerCategory =2 and CreatedDate between  convert(datetime, DATEADD(day, 3, @fromdate),102) and convert(datetime,@todate,102)) as Do3,(select sum(InvoiceSubTotal) as CustomerCategory  from SO_SalesOrder where  CustomerCategory =3 and CreatedDate between  convert(datetime, DATEADD(day, 3, @fromdate),102) and convert(datetime,@todate,102)) as General3,(select @fromdate as date) as datee,(select convert(VARCHAR(10),DATEADD(day, 1, @fromdate),110)) as datee1,(select convert(VARCHAR(10),DATEADD(day, 2, @fromdate),110)) as datee2,(select convert(VARCHAR(10),DATEADD(day, 3, @fromdate),110)) as datee3", conn))
                using (cmd = new SqlCommand("sp_paitents12dsh", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    //cmd.Parameters.AddWithValue("@fromdate", from);
                    //cmd.Parameters.AddWithValue("@todate", to);
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        SalesOrder obj1 = new SalesOrder();
                        obj1.Amount = DBNull.Value.Equals(dr["Ip"]) ? 0 : Convert.ToInt64(dr["ip"]);
                        obj1.AmountPaid = DBNull.Value.Equals(dr["OP"]) ? 0 : Convert.ToInt64(dr["OP"]);
                        obj1.BillAmount = DBNull.Value.Equals(dr["Do"]) ? 0 : Convert.ToInt64(dr["Do"]);
                        obj1.Balance = DBNull.Value.Equals(dr["General"]) ? 0 : Convert.ToInt64(dr["General"]);
                        obj1.Barcode1 = DBNull.Value.Equals(dr["Ip1"]) ? 0 : Convert.ToInt64(dr["ip1"]);
                        obj1.ClientID = DBNull.Value.Equals(dr["OP1"]) ? 0 : Convert.ToInt64(dr["OP1"]);
                        obj1.ClientId1 = DBNull.Value.Equals(dr["Do1"]) ? 0 : Convert.ToInt64(dr["Do1"]);
                        obj1.CustomerID = DBNull.Value.Equals(dr["General1"]) ? 0 : Convert.ToInt16(dr["General1"]);
                        obj1.Clientname = DBNull.Value.Equals(dr["datee"]) ? "" : Convert.ToString(dr["datee"]);
                        obj1.Date = DBNull.Value.Equals(dr["datee1"]) ? "" : Convert.ToString(dr["datee1"]);

                        obj1.Ip2 = DBNull.Value.Equals(dr["Ip2"]) ? 0 : Convert.ToInt64(dr["ip2"]);
                        obj1.Op2 = DBNull.Value.Equals(dr["OP2"]) ? 0 : Convert.ToInt64(dr["OP2"]);
                        obj1.Do2 = DBNull.Value.Equals(dr["Do2"]) ? 0 : Convert.ToInt64(dr["Do2"]);
                        obj1.General2 = DBNull.Value.Equals(dr["General2"]) ? 0 : Convert.ToInt16(dr["General2"]);
                        obj1.Ipp = DBNull.Value.Equals(dr["Ip3"]) ? 0 : Convert.ToInt64(dr["ip3"]);
                        obj1.Opp = DBNull.Value.Equals(dr["OP3"]) ? 0 : Convert.ToInt64(dr["OP3"]);
                        obj1.Do3 = DBNull.Value.Equals(dr["Do3"]) ? 0 : Convert.ToInt64(dr["Do3"]);
                        obj1.General3 = DBNull.Value.Equals(dr["General3"]) ? 0 : Convert.ToInt16(dr["General3"]);
                        obj1.Custom1 = DBNull.Value.Equals(dr["datee2"]) ? "" : Convert.ToString(dr["datee2"]);
                        obj1.Custom2 = DBNull.Value.Equals(dr["datee3"]) ? "" : Convert.ToString(dr["datee3"]);
                        obj.Add(obj1);
                    }
                }
            }
            return obj;
        }



        #endregion


        //.........sales...........//


        #region of sales
        public ActionResult SalesCategory()
        {
            HealthCareInMvc4.RoleProfileServiceRef.RoleProfile objuser = new HealthCareInMvc4.RoleProfileServiceRef.RoleProfile();
            objuser = (HealthCareInMvc4.RoleProfileServiceRef.RoleProfile)Session["UserList"];
            Int64 UserID = objuser.UserID;
            Session["userid"] = UserID;

            Int64 ClientID = objuser.ClientID;
            Session["clientid"] = ClientID;
            MainStoreSubStoreServiceClient objresult = new MainStoreSubStoreServiceClient(RIAGlobal.GetBinding(), new EndpointAddress(RIAGlobal.GetServicePath(Servicepath, "MainStoreSubStoreService.svc")));
            var list = objresult.MainStoreListByModuleID(5, Convert.ToString(ClientID), UserID);
            ViewBag.list = new SelectList(list.AsEnumerable(), "MainStoreID", "MainStoreName", list.FirstOrDefault().MainStoreID);

            IndentRequestServiceClient indentlist = new IndentRequestServiceClient(RIAGlobal.GetBinding(), new EndpointAddress(RIAGlobal.GetServicePath(Servicepath, "IndentRequestService.svc")));
            var sublist = indentlist.ListSubstore(5, 1, ClientID);
            ViewBag.sublist = new SelectList(sublist.AsEnumerable(), "SubStoreID", "SubStoreName", sublist.FirstOrDefault().SubStoreID);

            ChartViewModel _ChartViewModel = new ChartViewModel();
            _ChartViewModel.substores1 = substores11();
            _ChartViewModel.substores2 = substores12();
            return View(_ChartViewModel);
        }

        //.........sales...........//

        public ActionResult substoree2(string Fromdate, string Todate)
        {
            SqlConnection con = null;
            SqlCommand cmd = null;
            SqlDataReader dr;
            List<SalesOrder> li = new List<SalesOrder>();
            SalesOrder obj1;
            using (con = new SqlConnection(cs))
            {
                con.Open();
                con.CreateCommand();
                using (cmd = new SqlCommand("sp_substore222", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@fromdate", Fromdate));
                    cmd.Parameters.Add(new SqlParameter("@todate", Todate));
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        obj1 = new SalesOrder();
                        obj1.Clientname = DBNull.Value.Equals(dr["Name"]) ? "" : Convert.ToString(dr["Name"]);
                        obj1.ClientID = DBNull.Value.Equals(dr["Sales"]) ? 0 : Convert.ToInt64(dr["Sales"]);
                        obj1.Amount = DBNull.Value.Equals(dr["quentity"]) ? 0 : Convert.ToInt64(dr["quentity"]);
                        obj1.Date = DBNull.Value.Equals(dr["date"]) ? "" : Convert.ToString(dr["date"]);
                        li.Add(obj1);
                    }
                }
                return Json(li, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult subst3(string Fromdate, string Todate, string name)
        {
            SqlConnection con = null;
            SqlCommand cmd = null;
            SqlDataReader dr;
            List<SalesOrder> li = new List<SalesOrder>();
            SalesOrder obj1;
            using (con = new SqlConnection(cs))
            {
                con.Open();
                con.CreateCommand();
                using (cmd = new SqlCommand("sp_subst3", con))
                //using (cmd = new SqlCommand("select sub.SubStoreName,a.Quantity,a.SubTotal as price,bp.Name from SO_SalesOrderInvoice_Line a inner join  BASE_Product bp on bp.ProdID=a.ProdID inner join Base_InventorySubStores sub on sub.SubStoreID=a.SubStoreID where sub.SubStoreName='OT STORES' and a.CreatedDate   between  '2014-04-10 08:25:46.693' and '2015-09-27 13:50:11.970' ", con))
                //using (cmd = new SqlCommand("productcategorystore", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@fromdate", Fromdate));
                    cmd.Parameters.Add(new SqlParameter("@todate", Todate));
                    cmd.Parameters.Add(new SqlParameter("@storename", name));
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        obj1 = new SalesOrder();
                        obj1.Clientname = DBNull.Value.Equals(dr["SubStoreName"]) ? "" : Convert.ToString(dr["SubStoreName"]);
                        obj1.ClientID = DBNull.Value.Equals(dr["price"]) ? 0 : Convert.ToInt64(dr["price"]);
                        obj1.Amount = DBNull.Value.Equals(dr["Quantity"]) ? 0 : Convert.ToInt64(dr["Quantity"]);
                        obj1.Date = DBNull.Value.Equals(dr["Name"]) ? "" : Convert.ToString(dr["Name"]);
                        li.Add(obj1);
                    }
                }
                return Json(li, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult subst311(string name)
        {
            SqlConnection con = null;
            SqlCommand cmd = null;
            SqlDataReader dr;
            List<SalesOrder> li = new List<SalesOrder>();
            SalesOrder obj1;
            using (con = new SqlConnection(cs))
            {
                con.Open();
                con.CreateCommand();
                using (cmd = new SqlCommand("sp_subst311", con))
                //using (cmd = new SqlCommand("select sub.SubStoreName,a.Quantity,a.SubTotal as price,bp.Name from SO_SalesOrderInvoice_Line a inner join  BASE_Product bp on bp.ProdID=a.ProdID inner join Base_InventorySubStores sub on sub.SubStoreID=a.SubStoreID where sub.SubStoreName='OT STORES' and a.CreatedDate   between  '2014-04-10 08:25:46.693' and '2015-09-27 13:50:11.970' ", con))
                //using (cmd = new SqlCommand("productcategorystore", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@storename", name));
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        obj1 = new SalesOrder();
                        obj1.Clientname = DBNull.Value.Equals(dr["SubStoreName"]) ? "" : Convert.ToString(dr["SubStoreName"]);
                        obj1.ClientID = DBNull.Value.Equals(dr["price"]) ? 0 : Convert.ToInt64(dr["price"]);
                        obj1.Amount = DBNull.Value.Equals(dr["Quantity"]) ? 0 : Convert.ToInt64(dr["Quantity"]);
                        obj1.Date = DBNull.Value.Equals(dr["Name"]) ? "" : Convert.ToString(dr["Name"]);
                        li.Add(obj1);
                    }
                }
                return Json(li, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult substoree3(string Fromdate, string Todate)
        {
            SqlConnection con = null;
            SqlCommand cmd = null;
            SqlDataReader dr;
            List<SalesOrder> li = new List<SalesOrder>();
            SalesOrder obj1;
            using (con = new SqlConnection(cs))
            {
                con.Open();
                con.CreateCommand();
                using (cmd = new SqlCommand("sp_substore33", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@fromdate", Fromdate));
                    cmd.Parameters.Add(new SqlParameter("@todate", Todate));
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        obj1 = new SalesOrder();
                        obj1.Clientname = DBNull.Value.Equals(dr["Name"]) ? "" : Convert.ToString(dr["Name"]);
                        obj1.ClientID = DBNull.Value.Equals(dr["Sales"]) ? 0 : Convert.ToInt64(dr["Sales"]);
                        li.Add(obj1);
                    }
                }
                return Json(li, JsonRequestBehavior.AllowGet);
            }
        }


        public ActionResult substoree21()
        {
            SqlConnection con = null;
            SqlCommand cmd = null;
            SqlDataReader dr;
            List<SalesOrder> li = new List<SalesOrder>();
            SalesOrder obj1;
            using (con = new SqlConnection(cs))
            {
                con.Open();
                con.CreateCommand();
                using (cmd = new SqlCommand("sp_substore21", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;                   
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        obj1 = new SalesOrder();
                        obj1.Clientname = DBNull.Value.Equals(dr["Name"]) ? "" : Convert.ToString(dr["Name"]);
                        obj1.ClientID = DBNull.Value.Equals(dr["Sales"]) ? 0 : Convert.ToInt64(dr["Sales"]);
                        obj1.Amount = DBNull.Value.Equals(dr["quentity"]) ? 0 : Convert.ToInt64(dr["quentity"]);
                        obj1.Date = DBNull.Value.Equals(dr["date"]) ? "" : Convert.ToString(dr["date"]);
                        li.Add(obj1);
                    }
                }
                return Json(li, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult substoree31()
        {
            SqlConnection con = null;
            SqlCommand cmd = null;
            SqlDataReader dr;
            List<SalesOrder> li = new List<SalesOrder>();
            SalesOrder obj1;
            using (con = new SqlConnection(cs))
            {
                con.Open();
                con.CreateCommand();
                using (cmd = new SqlCommand("sp_substore31", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        obj1 = new SalesOrder();
                        obj1.Clientname = DBNull.Value.Equals(dr["Name"]) ? "" : Convert.ToString(dr["Name"]);
                        obj1.ClientID = DBNull.Value.Equals(dr["Sales"]) ? 0 : Convert.ToInt64(dr["Sales"]);
                        li.Add(obj1);
                    }
                }
                return Json(li, JsonRequestBehavior.AllowGet);
            }
        }







        public List<SalesOrder> substores11()
        {
            List<SalesOrder> obj = new List<SalesOrder>();
            SqlConnection conn = null;
            SqlCommand cmd = null;
            SqlDataReader dr;
            using (conn = new SqlConnection(cs))
            {
                conn.Open();
                conn.CreateCommand();
                using (cmd = new SqlCommand("sp_substore45", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        SalesOrder obj1 = new SalesOrder();
                        obj1.Clientname = DBNull.Value.Equals(dr["SubStoreName"]) ? "" : Convert.ToString(dr["SubStoreName"]);
                        obj1.Amount = DBNull.Value.Equals(dr["InvoiceTotal"]) ? 0 : Convert.ToInt16(dr["InvoiceTotal"]);
                        obj.Add(obj1);
                    }
                }
            }
            return obj;
        }
        public List<SalesOrder> substores12()
        {
            List<SalesOrder> obj = new List<SalesOrder>();
            SqlConnection conn = null;
            SqlCommand cmd = null;
            SqlDataReader dr;
            using (conn = new SqlConnection(cs))
            {
                conn.Open();
                conn.CreateCommand();
                using (cmd = new SqlCommand("sp_substore48", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        SalesOrder obj1 = new SalesOrder();
                        obj1.Clientname = DBNull.Value.Equals(dr["ProductTypeName"]) ? "" : Convert.ToString(dr["ProductTypeName"]);
                        //obj1.Balance = DBNull.Value.Equals(dr["SubStoreID"]) ? 0.0m : Convert.ToDecimal(dr["SubStoreID"]);
                        obj1.Amount = DBNull.Value.Equals(dr["SubTotal"]) ? 0 : Convert.ToInt16(dr["SubTotal"]);
                        obj1.AmountPaid = DBNull.Value.Equals(dr["Quantity"]) ? 0 : Convert.ToInt16(dr["Quantity"]);
                        obj.Add(obj1);
                    }
                }
            }
            return obj;
        }




        #endregion


        #region---vendorewise---

        public ActionResult vendorewise()
        {
            Vendorwisepurchase();
            pur();
            Datewise();
            Purchas1();
            return View();

        }

        public ActionResult Vendorwisepurchase()
        {
            List<SalesOrder> g = new List<SalesOrder>();
            using (conn = new SqlConnection(cs))
            {
                conn.Open();
                cmd = conn.CreateCommand();
                using (cmd = new SqlCommand("sp_vendorePurchase", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        SalesOrder j = new SalesOrder();

                        j.Clientname = DBNull.Value.Equals(dr["NAME"]) ? " " : Convert.ToString(dr["NAME"]);
                        j.Amount = DBNull.Value.Equals(dr["PurchasePrise"]) ? 0 : Convert.ToInt16(dr["PurchasePrise"]);
                        g.Add(j);
                    }
                    return Json(g, JsonRequestBehavior.AllowGet);
                }
            }
        }
        public ActionResult VendoreName1(string Fromdate, string Todate)
        {
            List<SalesOrder> g = new List<SalesOrder>();
            using (conn = new SqlConnection(cs))
            {
                conn.Open();
                cmd = conn.CreateCommand();
                using (cmd = new SqlCommand("sp_VendoreName1", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@FromDate", Fromdate));
                    cmd.Parameters.Add(new SqlParameter("@ToDate", Todate));
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        SalesOrder j = new SalesOrder();

                        j.Clientname = DBNull.Value.Equals(dr["NAME"]) ? " " : Convert.ToString(dr["NAME"]);
                        j.Amount = DBNull.Value.Equals(dr["PurchasePrise"]) ? 0 : Convert.ToInt16(dr["PurchasePrise"]);
                        g.Add(j);
                    }
                    return Json(g, JsonRequestBehavior.AllowGet);
                }
            }
        }

        public ActionResult pur()
        {
            List<SalesOrder> g = new List<SalesOrder>();
            using (conn = new SqlConnection(cs))
            {
                conn.Open();
                cmd = conn.CreateCommand();

                using (cmd = new SqlCommand("sp_venrecdreturn", conn))
                {
                      cmd.CommandType = CommandType.StoredProcedure;
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        SalesOrder j = new SalesOrder();
                        j.Genericname = DBNull.Value.Equals(dr["NAME"]) ? "" : Convert.ToString(dr["NAME"]);
                        j.Amount = DBNull.Value.Equals(dr["RecvQty"]) ? 0 : Convert.ToInt16(dr["RecvQty"]);
                        j.AmountPaid = DBNull.Value.Equals(dr["ReturnedPack"]) ? 0 : Convert.ToInt16(dr["ReturnedPack"]);
                        g.Add(j);
                    }
                    return Json(g, JsonRequestBehavior.AllowGet);
                }
            }
        }
        public ActionResult pur1(string Fromdate, string Todate)
        {
            List<SalesOrder> g = new List<SalesOrder>();
            using (conn = new SqlConnection(cs))
            {
                conn.Open();
                cmd = conn.CreateCommand();

                using (cmd = new SqlCommand("sp_pur1", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@FromDate", Fromdate));
                    cmd.Parameters.Add(new SqlParameter("@ToDate", Todate));
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        SalesOrder j = new SalesOrder();
                        j.Genericname = DBNull.Value.Equals(dr["NAME"]) ? "" : Convert.ToString(dr["NAME"]);
                        j.Amount = DBNull.Value.Equals(dr["RecvQty"]) ? 0 : Convert.ToInt16(dr["RecvQty"]);
                        j.AmountPaid = DBNull.Value.Equals(dr["ReturnedPack"]) ? 0 : Convert.ToInt16(dr["ReturnedPack"]);
                        g.Add(j);
                    }
                    return Json(g, JsonRequestBehavior.AllowGet);
                }
            }
        }
        public ActionResult Datewise()
        {
            List<SalesOrder> g = new List<SalesOrder>();
            using (conn = new SqlConnection(cs))
            {
                conn.Open();
                cmd = conn.CreateCommand();

                using (cmd = new SqlCommand("sp_daywisePurchase", conn))
                {
                      cmd.CommandType = CommandType.StoredProcedure;
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        SalesOrder j = new SalesOrder();
                        j.Genericname = DBNull.Value.Equals(dr["Date1"]) ? "" : Convert.ToString(dr["Date1"]);
                        j.Amount = DBNull.Value.Equals(dr["PurchaseTotal"]) ? 0 : Convert.ToInt16(dr["PurchaseTotal"]);
                        // j.AmountPaid = DBNull.Value.Equals(dr["ReturnedPack"]) ? 0 : Convert.ToInt16(dr["ReturnedPack"]);
                        g.Add(j);
                    }
                    return Json(g, JsonRequestBehavior.AllowGet);
                }
            }
        }
        public ActionResult Datewise1(string Fromdate, string Todate)
        {
            List<SalesOrder> g = new List<SalesOrder>();
            using (conn = new SqlConnection(cs))
            {
                conn.Open();
                cmd = conn.CreateCommand();

                using (cmd = new SqlCommand("sp_daywisePurchase1", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@FromDate", Fromdate));
                    cmd.Parameters.Add(new SqlParameter("@ToDate", Todate));
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        SalesOrder j = new SalesOrder();
                        j.Genericname = DBNull.Value.Equals(dr["Date1"]) ? "" : Convert.ToString(dr["Date1"]);
                        j.Amount = DBNull.Value.Equals(dr["PurchaseTotal"]) ? 0 : Convert.ToInt16(dr["PurchaseTotal"]);
                        // j.AmountPaid = DBNull.Value.Equals(dr["ReturnedPack"]) ? 0 : Convert.ToInt16(dr["ReturnedPack"]);
                        g.Add(j);
                    }
                    return Json(g, JsonRequestBehavior.AllowGet);
                }
            }
        }

        public ActionResult Purchas1()
        {
            SqlConnection con = null;
            SqlCommand cmd = null;
            SqlDataReader dr;
            List<SalesOrder> li = new List<SalesOrder>();
            SalesOrder obj1;
            using (con = new SqlConnection(cs))
            {
                con.Open();
                con.CreateCommand();
                using (cmd = new SqlCommand("sp_purchasedash", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        obj1 = new SalesOrder();
                        obj1.Amount = DBNull.Value.Equals(dr["POAprroved"]) ? 0 : Convert.ToInt16(dr["POAprroved"]);
                        obj1.AmountPaid = DBNull.Value.Equals(dr["PORejected"]) ? 0 : Convert.ToInt16(dr["PORejected"]);
                        obj1.BillAmount = DBNull.Value.Equals(dr["POPending"]) ? 0 : Convert.ToInt16(dr["POPending"]);

                        li.Add(obj1);
                    }
                }
                return Json(li, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult Purchas11(string Fromdate, string Todate)
        {
            SqlConnection con = null;
            SqlCommand cmd = null;
            SqlDataReader dr;
            List<SalesOrder> li = new List<SalesOrder>();
            SalesOrder obj1;
            using (con = new SqlConnection(cs))
            {
                con.Open();
                con.CreateCommand();
                using (cmd = new SqlCommand("sp_Purchas11", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@FromDate", Fromdate));
                    cmd.Parameters.Add(new SqlParameter("@ToDate", Todate));
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        obj1 = new SalesOrder();
                        obj1.Amount = DBNull.Value.Equals(dr["POAprroved"]) ? 0 : Convert.ToInt16(dr["POAprroved"]);
                        obj1.AmountPaid = DBNull.Value.Equals(dr["PORejected"]) ? 0 : Convert.ToInt16(dr["PORejected"]);
                        obj1.BillAmount = DBNull.Value.Equals(dr["POPending"]) ? 0 : Convert.ToInt16(dr["POPending"]);

                        li.Add(obj1);
                    }
                }
                return Json(li, JsonRequestBehavior.AllowGet);
            }
        }


        #endregion

        //#region---Purchase status--//

        //public ActionResult purchasestatus()
        //{

        //    Purchas2();
        //    Purchas3();
        //    Purchas6();
        //    return View();
        //}



        //public ActionResult Purchas2()
        //{
        //    SqlConnection con = null;
        //    SqlCommand cmd = null;
        //    SqlDataReader dr;
        //    List<SalesOrder> li = new List<SalesOrder>();
        //    SalesOrder obj1;
        //    using (con = new SqlConnection(cs))
        //    {
        //        con.Open();
        //        con.CreateCommand();
        //        using (cmd = new SqlCommand("", con))
        //        {
        //            //cmd.CommandType = CommandType.StoredProcedure;
        //           // cmd.Parameters.Add(new SqlParameter("@fromdate", Fromdate));
        //           // cmd.Parameters.Add(new SqlParameter("@todate", Todate));
        //            dr = cmd.ExecuteReader();
        //            while (dr.Read())
        //            {
        //                obj1 = new SalesOrder();
        //                //obj1.Clientname = DBNull.Value.Equals(dr["Date1"]) ? "" : Convert.ToString(dr["Date1"]);
        //                obj1.BillAmount = DBNull.Value.Equals(dr["OrderTotal"]) ? 0.0m : Convert.ToDecimal(dr["OrderTotal"]);
        //                li.Add(obj1);
        //            }
        //        }
        //        return Json(li, JsonRequestBehavior.AllowGet);
        //    }
        //}



        //public ActionResult Purchas3()
        //{
        //    SqlConnection con = null;
        //    SqlCommand cmd = null;
        //    SqlDataReader dr;
        //    List<SalesOrder> li = new List<SalesOrder>();
        //    SalesOrder obj1;
        //    using (con = new SqlConnection(cs))
        //    {
        //        con.Open();
        //        con.CreateCommand();
        //        using (cmd = new SqlCommand("SELECT  sum(OrderTotal)as OrderTotal ,sum(Tax1Rate)as Tax1Rate,sum(PurchaseTotal)as PurchaseTotal from po_purchaseorder where ActiveFlag=1", con))
        //        {
        //            //cmd.CommandType = CommandType.StoredProcedure;
        //            //cmd.Parameters.Add(new SqlParameter("@fromdate", Fromdate));
        //            //cmd.Parameters.Add(new SqlParameter("@todate", Todate));
        //            dr = cmd.ExecuteReader();
        //            while (dr.Read())
        //            {
        //                obj1 = new SalesOrder();
        //                obj1.Amount = DBNull.Value.Equals(dr["PurchaseTotal"]) ? 0 : Convert.ToInt64(dr["PurchaseTotal"]);
        //                obj1.AmountPaid = DBNull.Value.Equals(dr["OrderTotal"]) ? 0 : Convert.ToInt64(dr["OrderTotal"]);
        //                obj1.BillAmount = DBNull.Value.Equals(dr["Tax1Rate"]) ? 0 : Convert.ToInt64(dr["Tax1Rate"]);
        //                li.Add(obj1);
        //            }
        //        }
        //        return Json(li, JsonRequestBehavior.AllowGet);
        //    }
        //}

        //public ActionResult Purchas6()
        //{
        //    SqlConnection con = null;
        //    SqlCommand cmd = null;
        //    SqlDataReader dr;
        //    List<SalesOrder> li = new List<SalesOrder>();
        //    SalesOrder obj1;
        //    using (con = new SqlConnection(cs))
        //    {
        //        con.Open();
        //        con.CreateCommand();
        //        using (cmd = new SqlCommand("SELECT  sum(InvoiceSubTotal)as InvoiceSubTotal ,sum(InvoiceTotal)as InvoiceTotal,sum(InvoiceBalance)as InvoiceBalance from SO_SalesOrder where ActiveFlag=1", con))
        //        {
        //            //cmd.CommandType = CommandType.StoredProcedure;
        //            // cmd.Parameters.Add(new SqlParameter("@fromdate", Fromdate));
        //            // cmd.Parameters.Add(new SqlParameter("@todate", Todate));
        //            dr = cmd.ExecuteReader();
        //            while (dr.Read())
        //            {
        //                obj1 = new SalesOrder();
        //                // obj1.Clientname = DBNull.Value.Equals(dr["date"]) ? "" : Convert.ToString(dr["date"]);
        //                obj1.Amount = DBNull.Value.Equals(dr["InvoiceSubTotal"]) ? 0 : Convert.ToInt64(dr["InvoiceSubTotal"]);
        //                obj1.AmountPaid = DBNull.Value.Equals(dr["InvoiceTotal"]) ? 0 : Convert.ToInt64(dr["InvoiceTotal"]);
        //                obj1.BillAmount = DBNull.Value.Equals(dr["InvoiceBalance"]) ? 0 : Convert.ToInt64(dr["InvoiceBalance"]);
        //               // obj1.Balance = DBNull.Value.Equals(dr["InvoiceBalance"]) ? 0 : Convert.ToInt16(dr["InvoiceBalance"]);
        //                li.Add(obj1);
        //            }
        //        }
        //        return Json(li, JsonRequestBehavior.AllowGet);
        //    }
        //}


        //public ActionResult Purchas21(string Fromdate, string Todate)
        //{
        //    SqlConnection con = null;
        //    SqlCommand cmd = null;
        //    SqlDataReader dr;
        //    List<SalesOrder> li = new List<SalesOrder>();
        //    SalesOrder obj1;
        //    using (con = new SqlConnection(cs))
        //    {
        //        con.Open();
        //        con.CreateCommand();
        //        using (cmd = new SqlCommand("sp_Purchas21", con))
        //        {
        //            cmd.CommandType = CommandType.StoredProcedure;
        //            cmd.Parameters.Add(new SqlParameter("@FromDate", Fromdate));
        //            cmd.Parameters.Add(new SqlParameter("@ToDate", Todate)); 
        //            dr = cmd.ExecuteReader();
        //            while (dr.Read())
        //            {
        //                obj1 = new SalesOrder();
        //                //obj1.Clientname = DBNull.Value.Equals(dr["Date1"]) ? "" : Convert.ToString(dr["Date1"]);
        //                obj1.BillAmount = DBNull.Value.Equals(dr["OrderTotal"]) ? 0.0m : Convert.ToDecimal(dr["OrderTotal"]);
        //                li.Add(obj1);
        //            }
        //        }
        //        return Json(li, JsonRequestBehavior.AllowGet);
        //    }
        //}
        //public ActionResult Purchas31(string Fromdate, string Todate)
        //{
        //    SqlConnection con = null;
        //    SqlCommand cmd = null;
        //    SqlDataReader dr;
        //    List<SalesOrder> li = new List<SalesOrder>();
        //    SalesOrder obj1;
        //    using (con = new SqlConnection(cs))
        //    {
        //        con.Open();
        //        con.CreateCommand();
        //        using (cmd = new SqlCommand("sp_Purchas31", con))
        //        {
        //            cmd.CommandType = CommandType.StoredProcedure;
        //            cmd.Parameters.Add(new SqlParameter("@FromDate", Fromdate));
        //            cmd.Parameters.Add(new SqlParameter("@ToDate", Todate)); 
        //            dr = cmd.ExecuteReader();
        //            while (dr.Read())
        //            {
        //                obj1 = new SalesOrder();
        //                obj1.Amount = DBNull.Value.Equals(dr["PurchaseTotal"]) ? 0 : Convert.ToInt16(dr["PurchaseTotal"]);
        //                obj1.AmountPaid = DBNull.Value.Equals(dr["OrderTotal"]) ? 0 : Convert.ToInt16(dr["OrderTotal"]);
        //                obj1.BillAmount = DBNull.Value.Equals(dr["Tax1Rate"]) ? 0 : Convert.ToInt16(dr["Tax1Rate"]);
        //                li.Add(obj1);
        //            }
        //        }
        //        return Json(li, JsonRequestBehavior.AllowGet);
        //    }
        //}
        //public ActionResult Purchas61(string Fromdate, string Todate)
        //{
        //    SqlConnection con = null;
        //    SqlCommand cmd = null;
        //    SqlDataReader dr;
        //    List<SalesOrder> li = new List<SalesOrder>();
        //    SalesOrder obj1;
        //    using (con = new SqlConnection(cs))
        //    {
        //        con.Open();
        //        con.CreateCommand();
        //        using (cmd = new SqlCommand("sp_Purchas61", con))
        //        {
        //            cmd.CommandType = CommandType.StoredProcedure;
        //            cmd.Parameters.Add(new SqlParameter("@FromDate", Fromdate));
        //            cmd.Parameters.Add(new SqlParameter("@ToDate", Todate));    
        //            dr = cmd.ExecuteReader();
        //            while (dr.Read())
        //            {
        //                obj1 = new SalesOrder();
        //                // obj1.Clientname = DBNull.Value.Equals(dr["date"]) ? "" : Convert.ToString(dr["date"]);
        //                obj1.Amount = DBNull.Value.Equals(dr["InvoiceSubTotal"]) ? 0 : Convert.ToInt16(dr["InvoiceSubTotal"]);
        //                obj1.AmountPaid = DBNull.Value.Equals(dr["InvoiceTotal"]) ? 0 : Convert.ToInt16(dr["InvoiceTotal"]);
        //                obj1.BillAmount = DBNull.Value.Equals(dr["InvoiceBalance"]) ? 0 : Convert.ToInt16(dr["InvoiceBalance"]);
        //                // obj1.Balance = DBNull.Value.Equals(dr["InvoiceBalance"]) ? 0 : Convert.ToInt16(dr["InvoiceBalance"]);
        //                li.Add(obj1);
        //            }
        //        }
        //        return Json(li, JsonRequestBehavior.AllowGet);
        //    }
        //}




        //#endregion

        #region---Approvalhistory---//

        public ActionResult Approvalhistoroy()
        {
            HealthCareInMvc4.RoleProfileServiceRef.RoleProfile objuser = new HealthCareInMvc4.RoleProfileServiceRef.RoleProfile();
            objuser = (HealthCareInMvc4.RoleProfileServiceRef.RoleProfile)Session["UserList"];
            Int64 UserID = objuser.UserID;
            Session["userid"] = UserID;

            Int64 ClientID = objuser.ClientID;
            Session["clientid"] = ClientID;
            MainStoreSubStoreServiceClient objresult = new MainStoreSubStoreServiceClient(RIAGlobal.GetBinding(), new EndpointAddress(RIAGlobal.GetServicePath(Servicepath, "MainStoreSubStoreService.svc")));
            var list = objresult.MainStoreListByModuleID(5, Convert.ToString(ClientID), UserID);
            ViewBag.list = new SelectList(list.AsEnumerable(), "MainStoreID", "MainStoreName", list.FirstOrDefault().MainStoreID);

            IndentRequestServiceClient indentlist = new IndentRequestServiceClient(RIAGlobal.GetBinding(), new EndpointAddress(RIAGlobal.GetServicePath(Servicepath, "IndentRequestService.svc")));
            var sublist = indentlist.ListSubstore(5, 1, ClientID);
            ViewBag.sublist = new SelectList(sublist.AsEnumerable(), "SubStoreID", "SubStoreName", sublist.FirstOrDefault().SubStoreID);

            ChartViewModel _ChartViewModel = new ChartViewModel();
            _ChartViewModel.SalesOrderProduct = Approvaldetaily();
            _ChartViewModel.SalesOrderDayWise = Approvaldetaily12();
            return View(_ChartViewModel);
        }

         public List<SalesOrder> Approvaldetaily()
        {
            List<SalesOrder> obj = new List<SalesOrder>();
            SqlConnection conn = null;
            SqlCommand cmd = null;
            SqlDataReader dr;
            using (conn = new SqlConnection(cs))
            {
                conn.Open();
                conn.CreateCommand();
                using (cmd = new SqlCommand("sp_approvaldetail", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        SalesOrder l = new SalesOrder();
                        l.AmountPaid = DBNull.Value.Equals(dr["POAprroved"]) ? 0 : Convert.ToInt32(dr["POAprroved"]);
                        l.Balance = DBNull.Value.Equals(dr["PORejected"]) ? 0 : Convert.ToInt32(dr["PORejected"]);
                        l.Amount = DBNull.Value.Equals(dr["POPending"]) ? 0 : Convert.ToInt32(dr["POPending"]);
                        obj.Add(l);
                    }
                }
            }
            return obj;
        }
         public List<SalesOrder> Approvaldetaily12()
         {
             List<SalesOrder> obj = new List<SalesOrder>();
             SqlConnection conn = null;
             SqlCommand cmd = null;
             SqlDataReader dr;
             using (conn = new SqlConnection(cs))
             {
                 conn.Open();
                 conn.CreateCommand();
                 //using (cmd = new SqlCommand("select (select count(CustomerCategory) as CustomerCategory  from SO_SalesOrder where CreatedDate between  convert(datetime, '2015-06-16 08:25:46.693',102) and convert(datetime,'2015-06-16 13:50:11.970',102) and CustomerCategory=0) as Ip,(select count(CustomerCategory) as CustomerCategory  from SO_SalesOrder where  CustomerCategory =0 and CreatedDate between  convert(datetime, '2015-06-16 08:25:46.693',102) and convert(datetime,'2015-06-16 13:50:11.970',102)) as OP,(select count(CustomerCategory) as CustomerCategory  from SO_SalesOrder where  CustomerCategory =2 and CreatedDate between  convert(datetime, '2015-06-16 08:25:46.693',102) and convert(datetime,'2015-06-16 13:50:11.970',102)) as Do,(select count(CustomerCategory) as CustomerCategory  from SO_SalesOrder where  CustomerCategory =3 and CreatedDate between  convert(datetime, '2015-06-16 08:25:46.693',102) and convert(datetime,'2015-06-16 13:50:11.970',102)) as General", conn))
                 using (cmd = new SqlCommand("sp_tabledash", conn))
                 {
                     cmd.CommandType = CommandType.StoredProcedure;
                     dr = cmd.ExecuteReader();
                     while (dr.Read())
                     {
                         SalesOrder obj1 = new SalesOrder();
                         obj1.Name = DBNull.Value.Equals(dr["Name"]) ? " " : Convert.ToString(dr["Name"]);
                         obj1.ProductName = DBNull.Value.Equals(dr["status"]) ? "" : Convert.ToString(dr["status"]);
                         obj1.BillAmount = DBNull.Value.Equals(dr["PurchasePrise"]) ? 0 : Convert.ToInt64(dr["PurchasePrise"]);

                         obj.Add(obj1);
                     }
                 }
             }
             return obj;
         }


        public ActionResult PoPending()
        {
            List<SalesOrder> k = new List<SalesOrder>();
            using (conn = new SqlConnection(cs))
            {
                conn.Open();
                cmd = conn.CreateCommand();
                using (cmd = new SqlCommand("sp_Popending", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        SalesOrder l = new SalesOrder();
                        l.Name = DBNull.Value.Equals(dr["Name"]) ? " " : Convert.ToString(dr["Name"]);
                        l.AmountPaid = DBNull.Value.Equals(dr["PurchasePrice"]) ? 0 : Convert.ToInt16(dr["PurchasePrice"]);
                        k.Add(l);
                    }
                    return Json(k, JsonRequestBehavior.AllowGet);
                }
            }
        }
        public ActionResult PoPending1(string Fromdate, string Todate)
        {
            List<SalesOrder> k = new List<SalesOrder>();
            using (conn = new SqlConnection(cs))
            {
                conn.Open();
                cmd = conn.CreateCommand();
                using (cmd = new SqlCommand("sp_PoPending1", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@FromDate", Fromdate));
                    cmd.Parameters.Add(new SqlParameter("@ToDate", Todate));
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        SalesOrder l = new SalesOrder();
                        l.Name = DBNull.Value.Equals(dr["Name"]) ? " " : Convert.ToString(dr["Name"]);
                        l.AmountPaid = DBNull.Value.Equals(dr["PurchasePrice"]) ? 0 : Convert.ToInt16(dr["PurchasePrice"]);
                        k.Add(l);
                    }
                    return Json(k, JsonRequestBehavior.AllowGet);
                }
            }
        }
        public ActionResult PORejected()
        {
            List<SalesOrder> k = new List<SalesOrder>();
            using (conn = new SqlConnection(cs))
            {
                conn.Open();
                cmd = conn.CreateCommand();
                using (cmd = new SqlCommand("sp_PORejected", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        SalesOrder l = new SalesOrder();
                        l.Name = DBNull.Value.Equals(dr["Name"]) ? " " : Convert.ToString(dr["Name"]);
                        l.AmountPaid = DBNull.Value.Equals(dr["PurchasePrice"]) ? 0 : Convert.ToInt16(dr["PurchasePrice"]);
                        k.Add(l);
                    }
                    return Json(k, JsonRequestBehavior.AllowGet);
                }
            }
        }
        public ActionResult PORejected1(string Fromdate, string Todate)
        {
            List<SalesOrder> k = new List<SalesOrder>();
            using (conn = new SqlConnection(cs))
            {
                conn.Open();
                cmd = conn.CreateCommand();
                using (cmd = new SqlCommand("PORejected1", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@FromDate", Fromdate));
                    cmd.Parameters.Add(new SqlParameter("@ToDate", Todate));
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        SalesOrder l = new SalesOrder();
                        l.Name = DBNull.Value.Equals(dr["Name"]) ? " " : Convert.ToString(dr["Name"]);
                        l.AmountPaid = DBNull.Value.Equals(dr["PurchasePrice"]) ? 0 : Convert.ToInt16(dr["PurchasePrice"]);
                        k.Add(l);
                    }
                    return Json(k, JsonRequestBehavior.AllowGet);
                }
            }
        }
        public ActionResult POapproval()
        {
            List<SalesOrder> k = new List<SalesOrder>();
            using (conn = new SqlConnection(cs))
            {
                conn.Open();
                cmd = conn.CreateCommand();
                using (cmd = new SqlCommand("sp_POapproval", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        SalesOrder l = new SalesOrder();
                        l.Name = DBNull.Value.Equals(dr["Name"]) ? " " : Convert.ToString(dr["Name"]);
                        l.AmountPaid = DBNull.Value.Equals(dr["PurchasePrice"]) ? 0 : Convert.ToInt16(dr["PurchasePrice"]);
                        k.Add(l);
                    }
                    return Json(k, JsonRequestBehavior.AllowGet);
                }
            }
        }
        public ActionResult POapproval1(string Fromdate, string Todate)
        {
            List<SalesOrder> k = new List<SalesOrder>();
            using (conn = new SqlConnection(cs))
            {
                conn.Open();
                cmd = conn.CreateCommand();
                using (cmd = new SqlCommand("sp_POapproval1", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@FromDate", Fromdate));
                    cmd.Parameters.Add(new SqlParameter("@ToDate", Todate));
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        SalesOrder l = new SalesOrder();
                        l.Name = DBNull.Value.Equals(dr["Name"]) ? " " : Convert.ToString(dr["Name"]);
                        l.AmountPaid = DBNull.Value.Equals(dr["PurchasePrice"]) ? 0 : Convert.ToInt16(dr["PurchasePrice"]);

                        k.Add(l);
                    }
                    return Json(k, JsonRequestBehavior.AllowGet);
                }
            }
        }
        public ActionResult approvaldetails()
        {
            List<SalesOrder> k = new List<SalesOrder>();
            using (conn = new SqlConnection(cs))
            {
                conn.Open();
                cmd = conn.CreateCommand();
                using (cmd = new SqlCommand("sp_approvaldetail", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        SalesOrder l = new SalesOrder();
                        l.AmountPaid = DBNull.Value.Equals(dr["POAprroved"]) ? 0 : Convert.ToInt32(dr["POAprroved"]);
                        l.Balance = DBNull.Value.Equals(dr["PORejected"]) ? 0 : Convert.ToInt32(dr["PORejected"]);
                        l.Amount = DBNull.Value.Equals(dr["POPending"]) ? 0 : Convert.ToInt32(dr["POPending"]);
                        k.Add(l);
                    }
                    return Json(k, JsonRequestBehavior.AllowGet);
                }
            }
        }
        public ActionResult approvaldetails1(string Fromdate, string Todate)
        {
            List<SalesOrder> k = new List<SalesOrder>();
            using (conn = new SqlConnection(cs))
            {
                conn.Open();
                cmd = conn.CreateCommand();
                using (cmd = new SqlCommand("sp_approvaldetails1", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@FromDate", Fromdate));
                    cmd.Parameters.Add(new SqlParameter("@ToDate", Todate));
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        SalesOrder l = new SalesOrder();
                        l.AmountPaid = DBNull.Value.Equals(dr["POAprroved"]) ? 0 : Convert.ToInt32(dr["POAprroved"]);
                        l.Balance = DBNull.Value.Equals(dr["PORejected"]) ? 0 : Convert.ToInt32(dr["PORejected"]);
                        l.Amount = DBNull.Value.Equals(dr["POPending"]) ? 0 : Convert.ToInt32(dr["POPending"]);
                        k.Add(l);
                    }
                    return Json(k, JsonRequestBehavior.AllowGet);
                }
            }
        }


        #endregion

        #region---currentstock---//

        public ActionResult Currentstock()
        {
            HealthCareInMvc4.RoleProfileServiceRef.RoleProfile objuser = new HealthCareInMvc4.RoleProfileServiceRef.RoleProfile();
            objuser = (HealthCareInMvc4.RoleProfileServiceRef.RoleProfile)Session["UserList"];
            Int64 UserID = objuser.UserID;
            Session["userid"] = UserID;

            Int64 ClientID = objuser.ClientID;
            Session["clientid"] = ClientID;
            MainStoreSubStoreServiceClient objresult = new MainStoreSubStoreServiceClient(RIAGlobal.GetBinding(), new EndpointAddress(RIAGlobal.GetServicePath(Servicepath, "MainStoreSubStoreService.svc")));
            var list = objresult.MainStoreListByModuleID(5, Convert.ToString(ClientID), UserID);
            ViewBag.list = new SelectList(list.AsEnumerable(), "MainStoreID", "MainStoreName", list.FirstOrDefault().MainStoreID);

            IndentRequestServiceClient indentlist = new IndentRequestServiceClient(RIAGlobal.GetBinding(), new EndpointAddress(RIAGlobal.GetServicePath(Servicepath, "IndentRequestService.svc")));
            var sublist = indentlist.ListSubstore(5, 1, ClientID);
            ViewBag.sublist = new SelectList(sublist.AsEnumerable(), "SubStoreID", "SubStoreName", sublist.FirstOrDefault().SubStoreID);

            ChartViewModel _ChartViewModel = new ChartViewModel();
            _ChartViewModel.SalesOrderProduct = Currentdetaily();
            _ChartViewModel.SalesOrderDayWise = Currentdetailydetaily12();



            return View(_ChartViewModel);
        }

        public List<SalesOrder> Currentdetaily()
        {
            List<SalesOrder> obj = new List<SalesOrder>();
            SqlConnection conn = null;
            SqlCommand cmd = null;
            SqlDataReader dr;
            using (conn = new SqlConnection(cs))
            {
                conn.Open();
                conn.CreateCommand();
                using (cmd = new SqlCommand("sp_tablecurrent12", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        SalesOrder obj1 = new SalesOrder();
                        obj1.Name = DBNull.Value.Equals(dr["ProductName"]) ? "" : Convert.ToString(dr["ProductName"]);
                        obj1.Cond = DBNull.Value.Equals(dr["Quantity"]) ? 0 : Convert.ToInt32(dr["Quantity"]);
                        obj1.CreatedBy = DBNull.Value.Equals(dr["SalesPrice"]) ? 0 : Convert.ToInt32(dr["SalesPrice"]);
                        obj.Add(obj1);
                    }
                }
            }
            return obj;
        }

        public List<SalesOrder> Currentdetailydetaily12()
        {
            List<SalesOrder> obj = new List<SalesOrder>();
            SqlConnection conn = null;
            SqlCommand cmd = null;
            SqlDataReader dr;
            using (conn = new SqlConnection(cs))
            {
                conn.Open();
                conn.CreateCommand();
                using (cmd = new SqlCommand("sp_tablecurrent", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        SalesOrder obj1 = new SalesOrder();
                        string dat;

                        obj1.Name = DBNull.Value.Equals(dr["SubStoreName"]) ? "" : Convert.ToString(dr["SubStoreName"]);
                        obj1.ProductName = DBNull.Value.Equals(dr["ProductName"]) ? "" : Convert.ToString(dr["ProductName"]);
                        obj1.Customercategory = DBNull.Value.Equals(dr["Quantity"]) ? 0 : Convert.ToInt32(dr["Quantity"]);
                        dat = DBNull.Value.Equals(dr["exprirydate"]) ? "" : Convert.ToString(dr["exprirydate"]);
                        //obj.Add(obj1);
                        if (dat == "")
                        {
                            obj1.Age = dat;
                        }
                        else
                        {
                            string[] arrDate = dat.Split('-');
                            string day = arrDate[2].ToString();
                            string month = arrDate[1].ToString();
                            string year = arrDate[0].ToString();
                            obj1.Age = day + "-" + month + "-" + year;
                        }
                        obj.Add(obj1);
                    }

                }
            }
            return obj;
        }





        public ActionResult currentstock2()
        {
            List<SalesOrder> h = new List<SalesOrder>();
            using (conn = new SqlConnection(cs))
            {
                conn.Open();
                cmd = conn.CreateCommand();
                using (cmd = new SqlCommand("sp_currentstockProd", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        SalesOrder m = new SalesOrder();
                        m.Clientname = DBNull.Value.Equals(dr["name"]) ? "" : Convert.ToString(dr["name"]);
                        m.AmountPaid = DBNull.Value.Equals(dr["MAINAvailableQty"]) ? 0 : Convert.ToInt16(dr["MAINAvailableQty"]);
                        h.Add(m);
                    }
                }
                return Json(h, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult currentstock1(string Fromdate, string Todate)
        {
            List<SalesOrder> h = new List<SalesOrder>();
            using (conn = new SqlConnection(cs))
            {
                conn.Open();
                cmd = conn.CreateCommand();
                using (cmd = new SqlCommand("sp_currentstockProd1", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@FromDate", Fromdate));
                    cmd.Parameters.Add(new SqlParameter("@ToDate", Todate));
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        SalesOrder m = new SalesOrder();
                        m.Clientname = DBNull.Value.Equals(dr["name"]) ? "" : Convert.ToString(dr["name"]);
                        m.AmountPaid = DBNull.Value.Equals(dr["MAINAvailableQty"]) ? 0 : Convert.ToInt16(dr["MAINAvailableQty"]);
                        h.Add(m);
                    }
                }
                return Json(h, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Quantityrate()
        {
            List<SalesOrder> h = new List<SalesOrder>();
            using (conn = new SqlConnection(cs))
            {
                conn.Open();
                cmd = conn.CreateCommand();
                using (cmd = new SqlCommand("sp_quantitysales", conn))
                {
                     cmd.CommandType = CommandType.StoredProcedure;
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        SalesOrder m = new SalesOrder();
                        m.Clientname = DBNull.Value.Equals(dr["ProductTypeName"]) ? "" : Convert.ToString(dr["ProductTypeName"]);
                        m.AmountPaid = DBNull.Value.Equals(dr["Quantity"]) ? 0 : Convert.ToInt16(dr["Quantity"]);
                        m.Amount = DBNull.Value.Equals(dr["Rate"]) ? 0 : Convert.ToInt16(dr["Rate"]);
                        h.Add(m);
                    }
                }
                return Json(h, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult Quantityrate1(string Fromdate, string Todate)
        {
            List<SalesOrder> h = new List<SalesOrder>();
            using (conn = new SqlConnection(cs))
            {
                conn.Open();
                cmd = conn.CreateCommand();
                using (cmd = new SqlCommand("sp_ProductRste23", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@FromDate", Fromdate));
                    cmd.Parameters.Add(new SqlParameter("@ToDate", Todate));
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        SalesOrder m = new SalesOrder();
                        m.Clientname = DBNull.Value.Equals(dr["ProductTypeName"]) ? "" : Convert.ToString(dr["ProductTypeName"]);
                        m.AmountPaid = DBNull.Value.Equals(dr["Quantity"]) ? 0 : Convert.ToInt16(dr["Quantity"]);
                        m.Amount = DBNull.Value.Equals(dr["Rate"]) ? 0 : Convert.ToInt16(dr["Rate"]);
                        h.Add(m);
                    }
                }
                return Json(h, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Storewisecurrentstock()
        {
            List<SalesOrder> h = new List<SalesOrder>();
            using (conn = new SqlConnection(cs))
            {
                conn.Open();
                cmd = conn.CreateCommand();
                using (cmd = new SqlCommand("sp_storewisecurrentstockdash", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        SalesOrder m = new SalesOrder();
                        m.BaseEntityName = DBNull.Value.Equals(dr["SubStoreName"]) ? "" : Convert.ToString(dr["SubStoreName"]);
                        m.Amount = DBNull.Value.Equals(dr["Quantity"]) ? 0 : Convert.ToInt16(dr["Quantity"]);
                        //  m.AmountPaid = DBNull.Value.Equals(dr["SalesPrice"]) ? 0 : Convert.ToInt16(dr["SalesPrice"]);
                        // m.InvoiceBalance = DBNull.Value.Equals(dr["Total"]) ? 0 : Convert.ToInt16(dr["Total"]);
                        h.Add(m);
                    }
                }
                return Json(h, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult Storewisecurrentstock1(string Fromdate, string Todate)
        {
            List<SalesOrder> h = new List<SalesOrder>();
            using (conn = new SqlConnection(cs))
            {
                conn.Open();
                cmd = conn.CreateCommand();
                using (cmd = new SqlCommand("sp_Product11", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@FromDate", Fromdate));
                    cmd.Parameters.Add(new SqlParameter("@ToDate", Todate));
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        SalesOrder m = new SalesOrder();
                        m.BaseEntityName = DBNull.Value.Equals(dr["SubStoreName"]) ? "" : Convert.ToString(dr["SubStoreName"]);
                        m.Amount = DBNull.Value.Equals(dr["Quantity"]) ? 0 : Convert.ToInt16(dr["Quantity"]);
                        //  m.AmountPaid = DBNull.Value.Equals(dr["SalesPrice"]) ? 0 : Convert.ToInt16(dr["SalesPrice"]);
                        // m.InvoiceBalance = DBNull.Value.Equals(dr["Total"]) ? 0 : Convert.ToInt16(dr["Total"]);
                        h.Add(m);
                    }
                }
                return Json(h, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult salesvalue()
        {
            List<SalesOrder> h = new List<SalesOrder>();
            using (conn = new SqlConnection(cs))
            {
                conn.Open();
                cmd = conn.CreateCommand();
                using (cmd = new SqlCommand("sp_salesvaluedash", conn))
                {
                     cmd.CommandType = CommandType.StoredProcedure;
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        SalesOrder m = new SalesOrder();
                        m.BaseEntityName = DBNull.Value.Equals(dr["NAME"]) ? "" : Convert.ToString(dr["NAME"]);
                        m.AmountPaid = DBNull.Value.Equals(dr["SalesPrise"]) ? 0 : Convert.ToInt16(dr["SalesPrise"]);
                        m.InvoiceBalance = DBNull.Value.Equals(dr["quantity"]) ? 0 : Convert.ToInt16(dr["quantity"]);
                        h.Add(m);
                    }
                }
                return Json(h, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult salesvalue1(string Fromdate, string Todate)
        {
            List<SalesOrder> h = new List<SalesOrder>();
            using (conn = new SqlConnection(cs))
            {
                conn.Open();
                cmd = conn.CreateCommand();
                using (cmd = new SqlCommand("sp_salesvaluedash1", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@FromDate", Fromdate));
                    cmd.Parameters.Add(new SqlParameter("@ToDate", Todate));
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        SalesOrder m = new SalesOrder();
                        m.BaseEntityName = DBNull.Value.Equals(dr["NAME"]) ? "" : Convert.ToString(dr["NAME"]);
                        m.AmountPaid = DBNull.Value.Equals(dr["SalesPrise"]) ? 0 : Convert.ToInt16(dr["SalesPrise"]);
                        m.InvoiceBalance = DBNull.Value.Equals(dr["quantity"]) ? 0 : Convert.ToInt16(dr["quantity"]);
                        h.Add(m);
                    }
                }
                return Json(h, JsonRequestBehavior.AllowGet);
            }
        }








        #endregion

        #region---Timewise Purchase---
        public ActionResult TimewisePurchase()
        {
            Timewisestore();
            dayTimesubstore();
            Indentrequest12();
            topboughtproducts12();
            return View();
        }

        public ActionResult Timewisestore()
        {
            List<SalesOrder> obj = new List<SalesOrder>();
            SqlConnection conn = null;
            SqlCommand cmd = null;
            SqlDataReader dr;
            using (conn = new SqlConnection(cs))
            {
                conn.Open();
                conn.CreateCommand();
                using (cmd = new SqlCommand("sp_storesales", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        SalesOrder obj1 = new SalesOrder();
                        obj1.Clientname = DBNull.Value.Equals(dr["InvoiceSubTotal"]) ? "" : Convert.ToString(dr["SubStoreName"]);
                        obj1.BillAmount = DBNull.Value.Equals(dr["InvoiceSubTotal"]) ? 0.0m : Convert.ToDecimal(dr["InvoiceSubTotal"]);
                        obj.Add(obj1);

                    }
                }
            }
            return Json(obj, JsonRequestBehavior.AllowGet);
        }
        public ActionResult dayTimesubstore()
        {
            SqlConnection con = null;
            SqlCommand cmd = null;
            SqlDataReader dr;
            List<SalesOrder> li = new List<SalesOrder>();
            SalesOrder obj1;
            using (con = new SqlConnection(cs))
            {
                con.Open();
                con.CreateCommand();
                using (cmd = new SqlCommand("Sp_Datewisesale", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {

                        obj1 = new SalesOrder();
                        obj1.Clientname = DBNull.Value.Equals(dr["Date1"]) ? "" : Convert.ToString(dr["Date1"]);
                        obj1.BillAmount = DBNull.Value.Equals(dr["Total"]) ? 0.0m : Convert.ToDecimal(dr["Total"]);


                        li.Add(obj1);
                    }
                }
                return Json(li, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult Indentrequest12()
        {
            SqlConnection con = null;
            SqlCommand cmd = null;
            SqlDataReader dr;
            List<PurchaseOrder> li = new List<PurchaseOrder>();
            PurchaseOrder obj1;
            using (con = new SqlConnection(cs))
            {
                con.Open();
                con.CreateCommand();
                using (cmd = new SqlCommand("sp_indentrequest12", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    // cmd.Parameters.Add(new SqlParameter("@value", value));
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        obj1 = new PurchaseOrder();
                        obj1.SubStoreName = Convert.ToString(dr["SubStoreName"]);
                        obj1.TotalRequest = DBNull.Value.Equals(dr["TotalRequest"]) ? 0 : Convert.ToInt64(dr["TotalRequest"]);
                        li.Add(obj1);
                    }
                }
                return Json(li, JsonRequestBehavior.AllowGet);
            }

        }
        public ActionResult topboughtproducts12()
        {
            SqlConnection con = null;
            SqlCommand cmd = null;
            SqlDataReader dr;
            List<PurchaseOrder> li = new List<PurchaseOrder>();
            PurchaseOrder obj1;
            using (con = new SqlConnection(cs))
            {
                con.Open();
                con.CreateCommand();
                using (cmd = new SqlCommand("sp_topboughtproducts12", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        obj1 = new PurchaseOrder();
                        obj1.Productname = Convert.ToString(dr["Productname"]);
                        obj1.TotalCOunt = DBNull.Value.Equals(dr["Totalcount"]) ? 0 : Convert.ToInt64(dr["Totalcount"]);  // Convert.ToInt64(dr["SubTotal"]);
                        // obj1.Subtotal = DBNull.Value.Equals(dr["Subtotal"]) ? 0 : Convert.ToInt64(dr["Subtotal"]);
                        li.Add(obj1);
                    }
                }
                return Json(li, JsonRequestBehavior.AllowGet);
            }
        }






        public ActionResult Timewisestore1(string Fromdate, string Todate)
        {
            List<SalesOrder> obj = new List<SalesOrder>();
            SqlConnection conn = null;
            SqlCommand cmd = null;
            SqlDataReader dr;
            using (conn = new SqlConnection(cs))
            {
                conn.Open();
                conn.CreateCommand();
                using (cmd = new SqlCommand("sp_storesalesdash", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@FromDate", Fromdate));
                    cmd.Parameters.Add(new SqlParameter("@ToDate", Todate));
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        SalesOrder obj1 = new SalesOrder();
                        obj1.Clientname = DBNull.Value.Equals(dr["InvoiceSubTotal"]) ? "" : Convert.ToString(dr["SubStoreName"]);
                        obj1.BillAmount = DBNull.Value.Equals(dr["InvoiceSubTotal"]) ? 0.0m : Convert.ToDecimal(dr["InvoiceSubTotal"]);
                        obj.Add(obj1);

                    }
                }
            }
            return Json(obj, JsonRequestBehavior.AllowGet);
        }
        public ActionResult dayTimesubstore1(string Fromdate, string Todate)
        {
            SqlConnection con = null;
            SqlCommand cmd = null;
            SqlDataReader dr;
            List<SalesOrder> li = new List<SalesOrder>();
            SalesOrder obj1;
            using (con = new SqlConnection(cs))
            {
                con.Open();
                con.CreateCommand();
                using (cmd = new SqlCommand("Sp_Datewisesaledas", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@FromDate", Fromdate));
                    cmd.Parameters.Add(new SqlParameter("@ToDate", Todate));
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {

                        obj1 = new SalesOrder();
                        obj1.Clientname = DBNull.Value.Equals(dr["Date1"]) ? "" : Convert.ToString(dr["Date1"]);
                        obj1.BillAmount = DBNull.Value.Equals(dr["Total"]) ? 0.0m : Convert.ToDecimal(dr["Total"]);


                        li.Add(obj1);
                    }
                }
                return Json(li, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult Indentrequestdashw(String Fromdate, String Todate)
        {
            SqlConnection con = null;
            SqlCommand cmd = null;
            SqlDataReader dr;
            List<PurchaseOrder> li = new List<PurchaseOrder>();
            PurchaseOrder obj1;
            using (con = new SqlConnection(cs))
            {
                con.Open();
                con.CreateCommand();
                using (cmd = new SqlCommand("sp_indentrequest", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@fromdate", Fromdate));
                    cmd.Parameters.Add(new SqlParameter("@todate", Todate));
                    // cmd.Parameters.Add(new SqlParameter("@value", value));
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        obj1 = new PurchaseOrder();
                        obj1.SubStoreName = Convert.ToString(dr["SubStoreName"]);
                        obj1.TotalRequest = DBNull.Value.Equals(dr["TotalRequest"]) ? 0 : Convert.ToInt64(dr["TotalRequest"]);
                        li.Add(obj1);
                    }
                }
                return Json(li, JsonRequestBehavior.AllowGet);
            }

        }
        public ActionResult topboughtproductsdash(string Fromdate, string Todate)
        {
            SqlConnection con = null;
            SqlCommand cmd = null;
            SqlDataReader dr;
            List<PurchaseOrder> li = new List<PurchaseOrder>();
            PurchaseOrder obj1;
            using (con = new SqlConnection(cs))
            {
                con.Open();
                con.CreateCommand();
                using (cmd = new SqlCommand("sp_topboughtproducts", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@FromDate", Fromdate));
                    cmd.Parameters.Add(new SqlParameter("@ToDate", Todate));
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        obj1 = new PurchaseOrder();
                        obj1.Productname = Convert.ToString(dr["Productname"]);
                        obj1.TotalCOunt = DBNull.Value.Equals(dr["Totalcount"]) ? 0 : Convert.ToInt64(dr["Totalcount"]);  // Convert.ToInt64(dr["SubTotal"]);
                        // obj1.Subtotal = DBNull.Value.Equals(dr["Subtotal"]) ? 0 : Convert.ToInt64(dr["Subtotal"]);
                        li.Add(obj1);
                    }
                }
                return Json(li, JsonRequestBehavior.AllowGet);
            }
        }




        #endregion

        //-----Lab result----

        public ActionResult Result()
        {
            ChartViewModel _ChartViewModel = new ChartViewModel();
            ViewBag.Result1 = resulttab1();
            ViewBag.Result2 = resulttab2();
            return View();
        }
        public ActionResult result1(string Fromdate, string Todate)
        {
            SqlConnection con = null;
            SqlCommand cmd = null;
            SqlDataReader dr;
            var resultapproval = new { ResultApproval = 0, Resultpending = 0, ResultRejected = 0 };

            using (con = new SqlConnection(cs))
            {
                con.Open();
                con.CreateCommand();
                using (cmd = new SqlCommand("sp_resultapprovalpiechart", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@FromDate", Fromdate));
                    cmd.Parameters.Add(new SqlParameter("@ToDate", Todate));
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        resultapproval = new
                        {
                            ResultApproval = DBNull.Value.Equals(dr["Resultapproval"]) ? 0 : Convert.ToInt32(dr["Resultapproval"]),
                            Resultpending = DBNull.Value.Equals(dr["ResultPending"]) ? 0 : Convert.ToInt32(dr["ResultPending"]),
                            ResultRejected = DBNull.Value.Equals(dr["ResultRejected"]) ? 0 : Convert.ToInt32(dr["ResultRejected"])
                        };

                    }
                }
                return Json(resultapproval, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult result2(string Fromdate, string Todate)
        {
            SqlConnection con = null;
            SqlCommand cmd = null;
            SqlDataReader dr;
            var resultapproval = new[] { new { TestName = "", NoOfresultPending = 0 } }.ToList();

            using (con = new SqlConnection(cs))
            {
                con.Open();
                con.CreateCommand();
                using (cmd = new SqlCommand("sp_pendingresulttestchart", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@FromDate", Fromdate));
                    cmd.Parameters.Add(new SqlParameter("@ToDate", Todate));
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        resultapproval.Add(new
                        {
                            TestName = DBNull.Value.Equals(dr["TestName"]) ? "" : Convert.ToString(dr["TestName"]),
                            NoOfresultPending = DBNull.Value.Equals(dr["NoOfresultPending"]) ? 0 : Convert.ToInt32(dr["NoOfresultPending"])
                        }
                    );
                    }
                }
                return Json(resultapproval, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult result3(string Fromdate, string Todate)
        {
            SqlConnection con = null;
            SqlCommand cmd = null;
            SqlDataReader dr;
            var resultapproval = new[] { new { DepartmentName = "", NoOfresultPending = 0 } }.ToList();

            using (con = new SqlConnection(cs))
            {
                con.Open();
                con.CreateCommand();
                using (cmd = new SqlCommand("sp_pendingresultdeptchart", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@FromDate", Fromdate));
                    cmd.Parameters.Add(new SqlParameter("@ToDate", Todate));
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        resultapproval.Add(new
                        {
                            DepartmentName = DBNull.Value.Equals(dr["DepartmentName"]) ? "" : Convert.ToString(dr["DepartmentName"]),
                            NoOfresultPending = DBNull.Value.Equals(dr["NoOfresultPending"]) ? 0 : Convert.ToInt32(dr["NoOfresultPending"])
                        }
                        );

                    }
                }
                return Json(resultapproval, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult result4(string Fromdate, string Todate)
        {
            SqlConnection con = null;
            SqlCommand cmd = null;
            SqlDataReader dr;
            var resultapproval = new { ResultEntered = 0, ResultNotEntered = 0 };

            using (con = new SqlConnection(cs))
            {
                con.Open();
                con.CreateCommand();
                using (cmd = new SqlCommand("sp_ResultEntrystatuschart", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@FromDate", Fromdate));
                    cmd.Parameters.Add(new SqlParameter("@ToDate", Todate));
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        resultapproval = new
                        {
                            ResultEntered = DBNull.Value.Equals(dr["ResultEntered"]) ? 0 : Convert.ToInt32(dr["ResultEntered"]),
                            ResultNotEntered = DBNull.Value.Equals(dr["ResultNotEntered"]) ? 0 : Convert.ToInt32(dr["ResultNotEntered"])
                        };
                    }
                }
                return Json(resultapproval, JsonRequestBehavior.AllowGet);
            }
        }
        //-----Lab due----
        public ActionResult Due()
        {
            ChartViewModel _ChartViewModel = new ChartViewModel();
            ViewBag.due1 = duetab1();
            ViewBag.due2 = duetab2();
            return View();
        }
        public ActionResult due1(string Fromdate, string Todate)
        {
            SqlConnection con = null;
            SqlCommand cmd = null;
            SqlDataReader dr;
            var resultapproval = new { Dues = 0, DueCollected = 0, Total = 0 };

            using (con = new SqlConnection(cs))
            {
                con.Open();
                con.CreateCommand();
                using (cmd = new SqlCommand("sp_duedetailpiechart ", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@FromDate", Fromdate));
                    cmd.Parameters.Add(new SqlParameter("@ToDate", Todate));
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        resultapproval = new
                        {
                            Dues = DBNull.Value.Equals(dr["Dues"]) ? 0 : Convert.ToInt32(dr["Dues"]),
                            DueCollected = DBNull.Value.Equals(dr["DueCollected"]) ? 0 : Convert.ToInt32(dr["DueCollected"]),
                            Total = DBNull.Value.Equals(dr["Total"]) ? 0 : Convert.ToInt32(dr["Total"])
                        };
                    }
                }
                return Json(resultapproval, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult due2(string Fromdate, string Todate)
        {
            SqlConnection con = null;
            SqlCommand cmd = null;
            SqlDataReader dr;
            var resultapproval = new[] { new { payment = "", no = 0 } }.ToList();

            using (con = new SqlConnection(cs))
            {
                con.Open();
                con.CreateCommand();
                using (cmd = new SqlCommand("sp_Paymentmodewisechart ", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    //cmd.Parameters.Add(new SqlParameter("@FromDate", Fromdate));
                    //cmd.Parameters.Add(new SqlParameter("@ToDate", Todate));
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        resultapproval.Add(new
                        {
                            payment = DBNull.Value.Equals(dr["payment"]) ? "" : Convert.ToString(dr["payment"]),
                            no = DBNull.Value.Equals(dr["no"]) ? 0 : Convert.ToInt32(dr["no"])
                        }
                        );
                    }
                }
                return Json(resultapproval, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult due3(string Fromdate, string Todate)
        {
            SqlConnection con = null;
            SqlCommand cmd = null;
            SqlDataReader dr;
            var resultapproval = new[] { new { Patientname = "", TotalAmount = 0 } }.ToList();

            using (con = new SqlConnection(cs))
            {
                con.Open();
                con.CreateCommand();
                using (cmd = new SqlCommand("sp_patientwisebillchart ", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@FromDate", Fromdate));
                    cmd.Parameters.Add(new SqlParameter("@ToDate", Todate));
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        resultapproval.Add(new
                        {
                            Patientname = DBNull.Value.Equals(dr["Patientname"]) ? "" : Convert.ToString(dr["Patientname"]),
                            TotalAmount = DBNull.Value.Equals(dr["TotalAmount"]) ? 0 : Convert.ToInt32(dr["TotalAmount"])
                        }
                        );
                    }
                }
                return Json(resultapproval, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult due4(string Fromdate, string Todate)
        {
            SqlConnection con = null;
            SqlCommand cmd = null;
            SqlDataReader dr;
            var resultapproval = new { NormalPay = 0, DiscountPay = 0 };

            using (con = new SqlConnection(cs))
            {
                con.Open();
                con.CreateCommand();
                using (cmd = new SqlCommand("sp_nofodiscountchart ", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@FromDate", Fromdate));
                    cmd.Parameters.Add(new SqlParameter("@ToDate", Todate));
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        resultapproval = new
                        {

                            NormalPay = DBNull.Value.Equals(dr["NormalPay"]) ? 0 : Convert.ToInt32(dr["NormalPay"]),
                            DiscountPay = DBNull.Value.Equals(dr["DiscountPay"]) ? 0 : Convert.ToInt32(dr["DiscountPay"])

                        };
                    }
                }
                return Json(resultapproval, JsonRequestBehavior.AllowGet);
            }
        }
        //-----Lab patient----
        public ActionResult Patient()
        {
            ChartViewModel _ChartViewModel = new ChartViewModel();
            ViewBag.patient1 = patienttab1();
            ViewBag.patient2 = patienttab2();
            return View();
        }
        public ActionResult Patient1(string Fromdate, string Todate)
        {
            SqlConnection con = null;
            SqlCommand cmd = null;
            SqlDataReader dr;
            var resultapproval = new[] { new { DepartmentName = "", Tested = 0 } }.ToList();
            using (con = new SqlConnection(cs))
            {
                con.Open();
                con.CreateCommand();
                using (cmd = new SqlCommand("sp_departmentwisepiechart", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@FromDate", Fromdate));
                    cmd.Parameters.Add(new SqlParameter("@ToDate", Todate));
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        resultapproval.Add(new
                        {
                            DepartmentName = DBNull.Value.Equals(dr["DepartmentName"]) ? "" : Convert.ToString(dr["DepartmentName"]),
                            Tested = DBNull.Value.Equals(dr["Tested"]) ? 0 : Convert.ToInt32(dr["Tested"])
                        }
                         );

                    }

                }
                return Json(resultapproval, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult Patient2(string Fromdate, string Todate)
        {
            SqlConnection con = null;
            SqlCommand cmd = null;
            SqlDataReader dr;
            var resultapproval = new[] { new { Testname = "", Total = 0 } }.ToList();
            using (con = new SqlConnection(cs))
            {
                con.Open();
                con.CreateCommand();
                using (cmd = new SqlCommand("sp_monthlytestwisechart", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@FromDate", Fromdate));
                    cmd.Parameters.Add(new SqlParameter("@ToDate", Todate));
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        resultapproval.Add(new
                        {
                            Testname = DBNull.Value.Equals(dr["Testname"]) ? "" : Convert.ToString(dr["Testname"]),
                            Total = DBNull.Value.Equals(dr["Total"]) ? 0 : Convert.ToInt32(dr["Total"])
                        });

                    }

                }
                return Json(resultapproval, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult Patient3(string Fromdate, string Todate)
        {
            SqlConnection con = null;
            SqlCommand cmd = null;
            SqlDataReader dr;
            var resultapproval = new[] { new { PatientName = "", NoOfTimes = 0 } }.ToList();
            using (con = new SqlConnection(cs))
            {
                con.Open();
                con.CreateCommand();
                using (cmd = new SqlCommand("sp_Repeatedpatientchart", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    //cmd.Parameters.Add(new SqlParameter("@FromDate", Fromdate));
                    //cmd.Parameters.Add(new SqlParameter("@ToDate", Todate));
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        resultapproval.Add(new
                        {
                            PatientName = DBNull.Value.Equals(dr["PatientName"]) ? "" : Convert.ToString(dr["PatientName"]),
                            NoOfTimes = DBNull.Value.Equals(dr["NoOfTimes"]) ? 0 : Convert.ToInt32(dr["NoOfTimes"])
                        });

                    }

                }
                return Json(resultapproval, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult Patient4(string Fromdate, string Todate)
        {
            SqlConnection con = null;
            SqlCommand cmd = null;
            SqlDataReader dr;
            var resultapproval = new[] { new { DoctorName = "", NOofpatient = 0 } }.ToList();
            using (con = new SqlConnection(cs))
            {
                con.Open();
                con.CreateCommand();
                using (cmd = new SqlCommand("sp_Docwisepatientchart", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    // cmd.Parameters.Add(new SqlParameter("@FromDate", Fromdate));
                    //cmd.Parameters.Add(new SqlParameter("@ToDate", Todate));
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        resultapproval.Add(new
                        {
                            DoctorName = DBNull.Value.Equals(dr["DoctorName"]) ? "" : Convert.ToString(dr["DoctorName"]),
                            NOofpatient = DBNull.Value.Equals(dr["NOofpatient"]) ? 0 : Convert.ToInt32(dr["NOofpatient"])
                        });

                    }

                }
                return Json(resultapproval, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult Patient5(string Fromdate, string Todate)
        {
            SqlConnection con = null;
            SqlCommand cmd = null;
            SqlDataReader dr;
            var resultapproval = new[] { new { time = "", NOofpatient = 0 } }.ToList();
            using (con = new SqlConnection(cs))
            {
                con.Open();
                con.CreateCommand();
                using (cmd = new SqlCommand("sp_Timewisepatientchart", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    // cmd.Parameters.Add(new SqlParameter("@FromDate", Fromdate));
                    // cmd.Parameters.Add(new SqlParameter("@ToDate", Todate));
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        resultapproval.Add(new
                        {
                            time = DBNull.Value.Equals(dr["time"]) ? "" : Convert.ToString(dr["time"]),
                            NOofpatient = DBNull.Value.Equals(dr["NOofpatient"]) ? 0 : Convert.ToInt32(dr["NOofpatient"])
                        });

                    }

                }
                return Json(resultapproval, JsonRequestBehavior.AllowGet);
            }
        }




        public List<SalesOrder> resulttab1()
        {
            List<SalesOrder> obj = new List<SalesOrder>();
            SqlConnection conn = null;
            SqlCommand cmd = null;
            SqlDataReader dr;
            var resultapproval = new[] { new { ResultApproval = 0, Resultpending = 0, ResultRejected = 0 } }.ToList();
            using (conn = new SqlConnection(cs))
            {
                conn.Open();
                conn.CreateCommand();
                using (cmd = new SqlCommand("sp_resultapprovalpiechart", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        SalesOrder obj1 = new SalesOrder();
                        obj1.Amount = DBNull.Value.Equals(dr["Resultapproval"]) ? 0 : Convert.ToInt32(dr["Resultapproval"]);
                        obj1.AmountPaid = DBNull.Value.Equals(dr["ResultPending"]) ? 0 : Convert.ToInt32(dr["ResultPending"]);
                        obj1.Barcode1 = DBNull.Value.Equals(dr["ResultRejected"]) ? 0 : Convert.ToInt32(dr["ResultRejected"]);
                        obj.Add(obj1);
                    }
                }
            }
            return obj;
        }
        public List<SalesOrder> resulttab2()
        {
            List<SalesOrder> obj = new List<SalesOrder>();
            SqlConnection conn = null;
            SqlCommand cmd = null;
            SqlDataReader dr;
            var resultapproval = new[] { new { ResultApproval = 0, Resultpending = 0, ResultRejected = 0 } }.ToList();
            using (conn = new SqlConnection(cs))
            {
                conn.Open();
                conn.CreateCommand();
                using (cmd = new SqlCommand("sp_ResultEntrystatuschart", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        SalesOrder obj1 = new SalesOrder();

                        obj1.Balance = DBNull.Value.Equals(dr["ResultEntered"]) ? 0 : Convert.ToInt32(dr["ResultEntered"]);
                        obj1.Barcode1 = DBNull.Value.Equals(dr["ResultNotEntered"]) ? 0 : Convert.ToInt32(dr["ResultNotEntered"]);
                        obj.Add(obj1);
                    }
                }
            }
            return obj;
        }
        public List<SalesOrder> patienttab1()
        {
            List<SalesOrder> obj = new List<SalesOrder>();
            SqlConnection conn = null;
            SqlCommand cmd = null;
            SqlDataReader dr;
            var resultapproval = new[] { new { ResultApproval = 0, Resultpending = 0, ResultRejected = 0 } }.ToList();
            using (conn = new SqlConnection(cs))
            {
                conn.Open();
                conn.CreateCommand();
                using (cmd = new SqlCommand("sp_monthlytestwisechart", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        SalesOrder obj1 = new SalesOrder();

                        obj1.Batch = DBNull.Value.Equals(dr["Testname"]) ? "" : Convert.ToString(dr["Testname"]);
                        obj1.BillAmount = DBNull.Value.Equals(dr["Total"]) ? 0 : Convert.ToInt32(dr["Total"]);
                        obj.Add(obj1);
                    }
                }
            }
            return obj;
        }
        public List<SalesOrder> patienttab2()
        {
            List<SalesOrder> obj = new List<SalesOrder>();
            SqlConnection conn = null;
            SqlCommand cmd = null;
            SqlDataReader dr;
            var resultapproval = new[] { new { ResultApproval = 0, Resultpending = 0, ResultRejected = 0 } }.ToList();
            using (conn = new SqlConnection(cs))
            {
                conn.Open();
                conn.CreateCommand();
                using (cmd = new SqlCommand("sp_Repeatedpatientchart", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        SalesOrder obj1 = new SalesOrder();

                        obj1.BillingPostalCode = DBNull.Value.Equals(dr["PatientName"]) ? "" : Convert.ToString(dr["PatientName"]);
                        obj1.ClientID = DBNull.Value.Equals(dr["NoOfTimes"]) ? 0 : Convert.ToInt32(dr["NoOfTimes"]);
                        obj.Add(obj1);
                    }
                }
            }
            return obj;
        }
        public List<SalesOrder> duetab1()
        {
            List<SalesOrder> obj = new List<SalesOrder>();
            SqlConnection conn = null;
            SqlCommand cmd = null;
            SqlDataReader dr;
            var resultapproval = new[] { new { ResultApproval = 0, Resultpending = 0, ResultRejected = 0 } }.ToList();
            using (conn = new SqlConnection(cs))
            {
                conn.Open();
                conn.CreateCommand();
                using (cmd = new SqlCommand("sp_duedetailpiechart", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        SalesOrder obj1 = new SalesOrder();
                        obj1.ClientId1 = DBNull.Value.Equals(dr["Dues"]) ? 0 : Convert.ToInt32(dr["Dues"]);
                        obj1.Cond = DBNull.Value.Equals(dr["DueCollected"]) ? 0 : Convert.ToInt32(dr["DueCollected"]);
                        obj1.CreatedBy = DBNull.Value.Equals(dr["Total"]) ? 0 : Convert.ToInt32(dr["Total"]);
                        obj.Add(obj1);
                    }
                }
            }
            return obj;
        }
        public List<SalesOrder> duetab2()
        {
            List<SalesOrder> obj = new List<SalesOrder>();
            SqlConnection conn = null;
            SqlCommand cmd = null;
            SqlDataReader dr;
            var resultapproval = new[] { new { ResultApproval = 0, Resultpending = 0, ResultRejected = 0 } }.ToList();
            using (conn = new SqlConnection(cs))
            {
                conn.Open();
                conn.CreateCommand();
                using (cmd = new SqlCommand("sp_Paymentmodewisechart", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        SalesOrder obj1 = new SalesOrder();

                        obj1.Custom1 = DBNull.Value.Equals(dr["payment"]) ? "" : Convert.ToString(dr["payment"]);
                        obj1.Customercategory = DBNull.Value.Equals(dr["no"]) ? 0 : Convert.ToInt32(dr["no"]);
                        obj.Add(obj1);
                    }
                }
            }
            return obj;
        }
    }
}
