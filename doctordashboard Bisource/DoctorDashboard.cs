using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HealthCareObjects.Dashboard;
using System.Data;
using System.Data.SqlClient;
using HospitalManagement;

namespace PatientLibrary.DataAccess
{


    public class DoctorDashboard
    {
        public List<DoctorDashboardClass> Patientlocations(string Fromdate, string Todate)
        {
            IDbConnection con = null;
            IDbCommand cmd = null;
            IDataReader dr;
            List<DoctorDashboardClass> obj = new List<DoctorDashboardClass>();
            DoctorDashboardClass s;

            using (con = DataFactory.CreateConnection1())
            {
                con.Open();
                con.CreateCommand();
                using (cmd = DataFactory.CreateCommand("COUNT_Locations", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@FromDate", Fromdate));
                    cmd.Parameters.Add(new SqlParameter("@ToDate", Todate));
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {

                        s = new DoctorDashboardClass();
                        s.Name = DBNull.Value.Equals(dr["NAME"]) ? "" : Convert.ToString(dr["NAME"]);
                        s.countval = DBNull.Value.Equals(dr["CountVal"]) ? 0 : Convert.ToInt64(dr["CountVal"]);
                        obj.Add(s);
                    }
                }
                return obj;
            }
        }
        public List<DoctorDashboardClass> Gender(string Fromdate, string Todate)
        {
            IDbConnection con = null;
            IDbCommand cmd = null;
            IDataReader dr;
            List<DoctorDashboardClass> obj = new List<DoctorDashboardClass>();
            DoctorDashboardClass s;

            using (con = DataFactory.CreateConnection1())
            {
                con.Open();
                con.CreateCommand();
                using (cmd = DataFactory.CreateCommand("sp_Salutationchart", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@FromDate", Fromdate));
                    cmd.Parameters.Add(new SqlParameter("@ToDate", Todate));
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {

                        s = new DoctorDashboardClass();
                        s.Name = DBNull.Value.Equals(dr["NAME"]) ? "" : Convert.ToString(dr["NAME"]);
                        s.countval = DBNull.Value.Equals(dr["CountVal"]) ? 0 : Convert.ToInt64(dr["CountVal"]);

                        obj.Add(s);
                    }
                }
                return obj;
            }
        }
        public List<DoctorDashboardClass> PatientTypes(string Fromdate, string Todate)
        {
            IDbConnection con = null;
            IDbCommand cmd = null;
            IDataReader dr;
            List<DoctorDashboardClass> obj = new List<DoctorDashboardClass>();
            DoctorDashboardClass s;

            using (con = DataFactory.CreateConnection1())
            {
                con.Open();
                con.CreateCommand();
                using (cmd = DataFactory.CreateCommand("check_count_ss", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@FromDate", Fromdate));
                    cmd.Parameters.Add(new SqlParameter("@ToDate", Todate));
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {

                        s = new DoctorDashboardClass();
                        s.Name = DBNull.Value.Equals(dr["NAME"]) ? "" : Convert.ToString(dr["NAME"]);
                        s.countval = DBNull.Value.Equals(dr["CountVal"]) ? 0 : Convert.ToInt64(dr["CountVal"]);

                        obj.Add(s);
                    }
                }
                return obj;
            }
        }
        public List<DoctorDashboardClass> PatientCategory(string Fromdate, string Todate)
        {
            IDbConnection con = null;
            IDbCommand cmd = null;
            IDataReader dr;
            List<DoctorDashboardClass> obj = new List<DoctorDashboardClass>();
            DoctorDashboardClass s;

            using (con = DataFactory.CreateConnection1())
            {
                con.Open();
                con.CreateCommand();
                using (cmd = DataFactory.CreateCommand("sp_Get_Panel_Cash_count", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@FromDate", Fromdate));
                    cmd.Parameters.Add(new SqlParameter("@ToDate", Todate));
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {

                        s = new DoctorDashboardClass();
                        s.Name = DBNull.Value.Equals(dr["NAME"]) ? "" : Convert.ToString(dr["NAME"]);
                        s.countval = DBNull.Value.Equals(dr["CountVal"]) ? 0 : Convert.ToInt64(dr["CountVal"]);

                        obj.Add(s);
                    }
                }
                return obj;
            }
        }
        public List<DoctorDashboardClass> Racepatients(string Fromdate, string Todate)
        {
            IDbConnection con = null;
            IDbCommand cmd = null;
            IDataReader dr;
            List<DoctorDashboardClass> obj = new List<DoctorDashboardClass>();
            DoctorDashboardClass s;

            using (con = DataFactory.CreateConnection1())
            {
                con.Open();
                con.CreateCommand();
                using (cmd = DataFactory.CreateCommand("COUNT_race", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@FromDate", Fromdate));
                    cmd.Parameters.Add(new SqlParameter("@ToDate", Todate));
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {

                        s = new DoctorDashboardClass();
                        s.Name = DBNull.Value.Equals(dr["NAME"]) ? "" : Convert.ToString(dr["NAME"]);
                        s.countval = DBNull.Value.Equals(dr["CountVal"]) ? 0 : Convert.ToInt64(dr["CountVal"]);
                        obj.Add(s);
                    }
                }
                return obj;
            }
        }
        public List<DoctorDashboardClass> Agegroupforpatients(string Fromdate, string Todate)
        {
            IDbConnection con = null;
            IDbCommand cmd = null;
            IDataReader dr;
            List<DoctorDashboardClass> obj = new List<DoctorDashboardClass>();
            DoctorDashboardClass s;

            using (con = DataFactory.CreateConnection1())
            {
                con.Open();
                con.CreateCommand();
                using (cmd = DataFactory.CreateCommand("count_Age_diff", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@FromDate", Fromdate));
                    cmd.Parameters.Add(new SqlParameter("@ToDate", Todate));
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {

                        s = new DoctorDashboardClass();
                        s.Name = DBNull.Value.Equals(dr["NAME"]) ? "" : Convert.ToString(dr["NAME"]);
                        s.countval = DBNull.Value.Equals(dr["CountVal"]) ? 0 : Convert.ToInt64(dr["CountVal"]);
                        obj.Add(s);
                    }
                }
                return obj;
            }
        }
        public List<DoctorDashboardClass> Refferedto(string Fromdate, string Todate)
        {
            IDbConnection con = null;
            IDbCommand cmd = null;
            IDataReader dr;
            List<DoctorDashboardClass> obj = new List<DoctorDashboardClass>();
            DoctorDashboardClass s;

            using (con = DataFactory.CreateConnection1())
            {
                con.Open();
                con.CreateCommand();
                using (cmd = DataFactory.CreateCommand("Reffered_To", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@FromDate", Fromdate));
                    cmd.Parameters.Add(new SqlParameter("@ToDate", Todate));
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        s = new DoctorDashboardClass();
                        s.Name = DBNull.Value.Equals(dr["NAME"]) ? "" : Convert.ToString(dr["NAME"]);
                        s.countval = DBNull.Value.Equals(dr["CountVal"]) ? 0 : Convert.ToInt64(dr["CountVal"]);
                        obj.Add(s);

                    }
                }
                return obj;
            }
        }
        public List<DoctorDashboardClass> Referedbyclinic(string Fromdate, string Todate)
        {
            IDbConnection con = null;
            IDbCommand cmd = null;
            IDataReader dr;
            List<DoctorDashboardClass> obj = new List<DoctorDashboardClass>();
            DoctorDashboardClass s;

            using (con = DataFactory.CreateConnection1())
            {
                con.Open();
                con.CreateCommand();
                using (cmd = DataFactory.CreateCommand("Reference_count_ss", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@FromDate", Fromdate));
                    cmd.Parameters.Add(new SqlParameter("@ToDate", Todate));
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        s = new DoctorDashboardClass();
                        s.Name = DBNull.Value.Equals(dr["NAME"]) ? "" : Convert.ToString(dr["NAME"]);
                        s.countval = DBNull.Value.Equals(dr["CountVal"]) ? 0 : Convert.ToInt64(dr["CountVal"]);
                        obj.Add(s);

                    }
                }
                return obj;
            }
        }
        public List<DoctorDashboardClass> Reference(string Fromdate, string Todate)
        {
            IDbConnection con = null;
            IDbCommand cmd = null;
            IDataReader dr;
            List<DoctorDashboardClass> obj = new List<DoctorDashboardClass>();
            DoctorDashboardClass s;

            using (con = DataFactory.CreateConnection1())
            {
                con.Open();
                con.CreateCommand();
                using (cmd = DataFactory.CreateCommand("Reference_count_by", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@FromDate", Fromdate));
                    cmd.Parameters.Add(new SqlParameter("@ToDate", Todate));
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        s = new DoctorDashboardClass();
                        s.Name = DBNull.Value.Equals(dr["NAME"]) ? "" : Convert.ToString(dr["NAME"]);
                        s.countval = DBNull.Value.Equals(dr["CountVal"]) ? 0 : Convert.ToInt64(dr["CountVal"]);
                        obj.Add(s);

                    }
                }

                return obj;
            }
        }
        public List<DoctorDashboardClass> Referedtowhom(string Fromdate, string Todate)
        {
            IDbConnection con = null;
            IDbCommand cmd = null;
            IDataReader dr;
            List<DoctorDashboardClass> obj = new List<DoctorDashboardClass>();
            DoctorDashboardClass s;

            using (con = DataFactory.CreateConnection1())
            {
                con.Open();
                con.CreateCommand();
                using (cmd = DataFactory.CreateCommand("Refered_To_Whom", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@FromDate", Fromdate));
                    cmd.Parameters.Add(new SqlParameter("@ToDate", Todate));
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        s = new DoctorDashboardClass();
                        s.Name = DBNull.Value.Equals(dr["NAME"]) ? "" : Convert.ToString(dr["NAME"]);
                        s.countval = DBNull.Value.Equals(dr["CountVal"]) ? 0 : Convert.ToInt64(dr["CountVal"]);
                        obj.Add(s);

                    }
                }
                return obj;
            }
        }

        public List<DoctorDashboardClass> Patientwaitingtimes(string Fromdate)
        {
            IDbConnection con = null;
            IDbCommand cmd = null;
            IDataReader dr;
            List<DoctorDashboardClass> obj = new List<DoctorDashboardClass>();
            DoctorDashboardClass s;

            using (con = DataFactory.CreateConnection1())
            {
                con.Open();
                con.CreateCommand();
                using (cmd = DataFactory.CreateCommand("patient_waiting_time", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@FromDate", Fromdate));

                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {

                        s = new DoctorDashboardClass();
                        s.Name = DBNull.Value.Equals(dr["Patient"]) ? "" : Convert.ToString(dr["Patient"]);
                        //s.Id = DBNull.Value.Equals(dr["Patient"]) ? 0 : Convert.ToInt64(dr["Patient"]);
                        s.countval = DBNull.Value.Equals(dr["Minutes"]) ? 0 : Convert.ToInt64(dr["Minutes"]);

                        obj.Add(s);
                    }
                }
                return obj;
            }
        }
        public List<DoctorDashboardClass> Patientwaitinginterval(string Fromdate)
        {
            IDbConnection con = null;
            IDbCommand cmd = null;
            IDataReader dr;
            List<DoctorDashboardClass> obj = new List<DoctorDashboardClass>();
            DoctorDashboardClass s;

            using (con = DataFactory.CreateConnection1())
            {
                con.Open();
                con.CreateCommand();
                using (cmd = DataFactory.CreateCommand("count_patient_inter", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@FromDate", Fromdate));
                    // cmd.Parameters.Add(new SqlParameter("@ToDate", Todate));
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {

                        s = new DoctorDashboardClass();
                        s.Name = DBNull.Value.Equals(dr["NAME"]) ? "" : Convert.ToString(dr["NAME"]);
                        s.countval = DBNull.Value.Equals(dr["CountVal"]) ? 0 : Convert.ToInt64(dr["CountVal"]);


                        obj.Add(s);
                    }
                }
                return obj;
            }
        }
        public List<DoctorDashboardClass> Overallnewexist(string Fromdate)
        {
            IDbConnection con = null;
            IDbCommand cmd = null;
            IDataReader dr;
            List<DoctorDashboardClass> obj = new List<DoctorDashboardClass>();
            DoctorDashboardClass s;

            using (con = DataFactory.CreateConnection1())
            {
                con.Open();
                con.CreateCommand();
                using (cmd = DataFactory.CreateCommand("count_Exist_and_New", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@FromDate", Fromdate));
                    // cmd.Parameters.Add(new SqlParameter("@ToDate", Todate));
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {

                        s = new DoctorDashboardClass();
                        s.Name = DBNull.Value.Equals(dr["NAME"]) ? "" : Convert.ToString(dr["NAME"]);
                        s.countval = DBNull.Value.Equals(dr["CountNew"]) ? 0 : Convert.ToInt64(dr["CountNew"]);
                        s.AdvAmt = DBNull.Value.Equals(dr["CountExist"]) ? 0 : Convert.ToInt64(dr["CountExist"]);

                        obj.Add(s);
                    }
                }
                return obj;
            }
        }
        public List<DoctorDashboardClass> Surgerydetails(string Fromdate)
        {
            IDbConnection con = null;
            IDbCommand cmd = null;
            IDataReader dr;
            List<DoctorDashboardClass> obj = new List<DoctorDashboardClass>();
            DoctorDashboardClass s;

            using (con = DataFactory.CreateConnection1())
            {
                con.Open();
                con.CreateCommand();
                using (cmd = DataFactory.CreateCommand("sp_list_Surgerycount_by_month", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@FromDate", Fromdate));
                    // cmd.Parameters.Add(new SqlParameter("@ToDate", Todate));
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {

                        s = new DoctorDashboardClass();
                        s.Name = DBNull.Value.Equals(dr["NAME"]) ? "" : Convert.ToString(dr["NAME"]);
                        s.countval = DBNull.Value.Equals(dr["CountVal"]) ? 0 : Convert.ToInt64(dr["CountVal"]);
                        obj.Add(s);
                    }
                }
                return obj;
            }
        }
        public List<DoctorDashboardClass> Doctorsharepharmacy(string Fromdate)
        {
            IDbConnection con = null;
            IDbCommand cmd = null;
            IDataReader dr;
            List<DoctorDashboardClass> obj = new List<DoctorDashboardClass>();
            DoctorDashboardClass s;
            // decimal lm = 0;

            using (con = DataFactory.CreateConnection1())
            {
                con.Open();
                con.CreateCommand();
                using (cmd = DataFactory.CreateCommand("sp_doctor_share", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@FromDate", Fromdate));
                    //cmd.Parameters.Add(new SqlParameter("@ToDate", Todate));
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {

                        s = new DoctorDashboardClass();

                        s.Name = DBNull.Value.Equals(dr["NAME"]) ? "" : Convert.ToString(dr["NAME"]);
                        s.countval = DBNull.Value.Equals(dr["countHospital"]) ? 0 : Convert.ToInt64(dr["countHospital"]);
                        s.AdvAmt = DBNull.Value.Equals(dr["countDoctorshare"]) ? 0 : Convert.ToInt64(dr["countDoctorshare"]);

                        // s.Clientaddress = DBNull.Value.Equals(dr["CountVal"]) ? "" : Convert.ToString(dr["CountVal"]);
                        // s.percentage = decimal.Parse(s.Clientaddress);
                        //s.percentage = Int64.Parse(s.Clientaddress.ToString());
                        obj.Add(s);
                    }
                }
                return obj;
            }
        }
        public List<DoctorDashboardClass> Sharesinxray(string Fromdate)
        {
            IDbConnection con = null;
            IDbCommand cmd = null;
            IDataReader dr;
            List<DoctorDashboardClass> obj = new List<DoctorDashboardClass>();
            DoctorDashboardClass s;

            using (con = DataFactory.CreateConnection1())
            {
                con.Open();
                con.CreateCommand();
                using (cmd = DataFactory.CreateCommand("sp_xray_share", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@FromDate", Fromdate));

                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        s = new DoctorDashboardClass();
                        s.Name = DBNull.Value.Equals(dr["NAME"]) ? "" : Convert.ToString(dr["NAME"]);
                        s.countval = DBNull.Value.Equals(dr["countHospital"]) ? 0 : Convert.ToInt64(dr["countHospital"]);
                        s.AdvAmt = DBNull.Value.Equals(dr["countDoctorshare"]) ? 0 : Convert.ToInt64(dr["countDoctorshare"]);
                        obj.Add(s);

                    }
                }
                return obj;
            }
        }
        public List<DoctorDashboardClass> Sharesinscan(string Fromdate)
        {
            IDbConnection con = null;
            IDbCommand cmd = null;
            IDataReader dr;
            List<DoctorDashboardClass> obj = new List<DoctorDashboardClass>();
            DoctorDashboardClass s;

            using (con = DataFactory.CreateConnection1())
            {
                con.Open();
                con.CreateCommand();
                using (cmd = DataFactory.CreateCommand("sp_scan_share", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("FromDate", Fromdate));

                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        s = new DoctorDashboardClass();
                        s.Name = DBNull.Value.Equals(dr["NAME"]) ? "" : Convert.ToString(dr["NAME"]);
                        s.countval = DBNull.Value.Equals(dr["countHospital"]) ? 0 : Convert.ToInt64(dr["countHospital"]);
                        s.AdvAmt = DBNull.Value.Equals(dr["countDoctorshare"]) ? 0 : Convert.ToInt64(dr["countDoctorshare"]);
                        obj.Add(s);

                    }
                }
                return obj;
            }
        }
        public List<DoctorDashboardClass> Sharesinlab(string Fromdate)
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
                using (cmd = new SqlCommand("sp_lab_share", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@FromDate", Fromdate));

                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        s = new DoctorDashboardClass();
                        s.Name = DBNull.Value.Equals(dr["NAME"]) ? "" : Convert.ToString(dr["NAME"]);
                        s.countval = DBNull.Value.Equals(dr["countHospital"]) ? 0 : Convert.ToInt64(dr["countHospital"]);
                        s.AdvAmt = DBNull.Value.Equals(dr["countDoctorshare"]) ? 0 : Convert.ToInt64(dr["countDoctorshare"]);
                        obj.Add(s);

                    }
                }
                return obj;
            }
        }
    }
}
