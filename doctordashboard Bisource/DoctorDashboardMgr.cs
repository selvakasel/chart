using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HealthCareObjects.Dashboard;
using PatientLibrary.DataAccess;

namespace PatientLibrary.BusinessFlow
{
    public class DoctorDashboardMgr
    {

        public List<DoctorDashboardClass> Patientlocations(string Fromdate, string Todate)
        {
            DoctorDashboard ddob = new DoctorDashboard();
            return ddob.Patientlocations(Fromdate, Todate);
        }
        public List<DoctorDashboardClass> Gender(string Fromdate, string Todate)
        {
            DoctorDashboard ddob = new DoctorDashboard();
            return ddob.Gender(Fromdate, Todate);
        }
        public List<DoctorDashboardClass> PatientTypes(string Fromdate, string Todate)
        {
            DoctorDashboard ddob = new DoctorDashboard();
            return ddob.PatientTypes(Fromdate, Todate);
        }
        public List<DoctorDashboardClass> PatientCategory(string Fromdate, string Todate)
        {
            DoctorDashboard ddob = new DoctorDashboard();
            return ddob.PatientCategory(Fromdate, Todate);
        }
        public List<DoctorDashboardClass> Racepatients(string Fromdate, string Todate)
        {
            DoctorDashboard ddob = new DoctorDashboard();
            return ddob.Racepatients(Fromdate, Todate);
        }
        public List<DoctorDashboardClass> Agegroupforpatients(string Fromdate, string Todate)
        {
            DoctorDashboard ddob = new DoctorDashboard();
            return ddob.Agegroupforpatients(Fromdate, Todate);
        }
        public List<DoctorDashboardClass> Refferedto(string Fromdate, string Todate)
        {
            DoctorDashboard ddob = new DoctorDashboard();
            return ddob.Refferedto(Fromdate, Todate);
        }
        public List<DoctorDashboardClass> Referedbyclinic(string Fromdate, string Todate)
        {
            DoctorDashboard ddob = new DoctorDashboard();
            return ddob.Referedbyclinic(Fromdate, Todate);
        }
        public List<DoctorDashboardClass> Reference(string Fromdate, string Todate)
        {
            DoctorDashboard ddob = new DoctorDashboard();
            return ddob.Reference(Fromdate, Todate);
        }
        public List<DoctorDashboardClass> Referedtowhom(string Fromdate, string Todate)
        {
            DoctorDashboard ddob = new DoctorDashboard();
            return ddob.Referedtowhom(Fromdate, Todate);
        }


        public List<DoctorDashboardClass> Patientwaitingtimes(string Fromdate)
        {
            DoctorDashboard ddob = new DoctorDashboard();
            return ddob.Patientwaitingtimes(Fromdate);
        }
        public List<DoctorDashboardClass> Patientwaitinginterval(string Fromdate)
        {
            DoctorDashboard ddob = new DoctorDashboard();
            return ddob.Patientwaitinginterval(Fromdate);
        }
        public List<DoctorDashboardClass> Overallnewexist(string Fromdate)
        {
            DoctorDashboard ddob = new DoctorDashboard();
            return ddob.Overallnewexist(Fromdate);
        }
        public List<DoctorDashboardClass> Surgerydetails(string Fromdate)
        {
            DoctorDashboard ddob = new DoctorDashboard();
            return ddob.Surgerydetails(Fromdate);
        }
        public List<DoctorDashboardClass> Doctorsharepharmacy(string Fromdate)
        {
            DoctorDashboard ddob = new DoctorDashboard();
            return ddob.Doctorsharepharmacy(Fromdate);
        }
        public List<DoctorDashboardClass> Sharesinxray(string Fromdate)
        {
            DoctorDashboard ddob = new DoctorDashboard();
            return ddob.Sharesinxray(Fromdate);
        }
        public List<DoctorDashboardClass> Sharesinscan(string Fromdate)
        {
            DoctorDashboard ddob = new DoctorDashboard();
            return ddob.Sharesinscan(Fromdate);
        }
        public List<DoctorDashboardClass> Sharesinlab(string Fromdate)
        {
            DoctorDashboard ddob = new DoctorDashboard();
            return ddob.Sharesinlab(Fromdate);
        }

    }
}
