using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using HealthCareObjects.Dashboard;
using PatientLibrary.BusinessFlow;

namespace PatientServices
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "DoctorDashboardService" in code, svc and config file together.
    public class DoctorDashboardService : IDoctorDashboardService
    {
        public List<DoctorDashboardClass> Patientlocations(string Fromdate, string Todate)
        {
            DoctorDashboardMgr ddmgr = new DoctorDashboardMgr();
            return ddmgr.Patientlocations(Fromdate, Todate);
        }
        public List<DoctorDashboardClass> Gender(string Fromdate, string Todate)
        {
            DoctorDashboardMgr ddmgr = new DoctorDashboardMgr();
            return ddmgr.Gender(Fromdate, Todate);
        }
        public List<DoctorDashboardClass> PatientTypes(string Fromdate, string Todate)
        {
            DoctorDashboardMgr ddmgr = new DoctorDashboardMgr();
            return ddmgr.PatientTypes(Fromdate, Todate);
        }
        public List<DoctorDashboardClass> PatientCategory(string Fromdate, string Todate)
        {
            DoctorDashboardMgr ddmgr = new DoctorDashboardMgr();
            return ddmgr.PatientCategory(Fromdate, Todate);
        }
        public List<DoctorDashboardClass> Racepatients(string Fromdate, string Todate)
        {
            DoctorDashboardMgr ddmgr = new DoctorDashboardMgr();
            return ddmgr.Racepatients(Fromdate, Todate);
        }
        public List<DoctorDashboardClass> Agegroupforpatients(string Fromdate, string Todate)
        {
            DoctorDashboardMgr ddmgr = new DoctorDashboardMgr();
            return ddmgr.Agegroupforpatients(Fromdate, Todate);
        }
        public List<DoctorDashboardClass> Refferedto(string Fromdate, string Todate)
        {
            DoctorDashboardMgr ddmgr = new DoctorDashboardMgr();
            return ddmgr.Refferedto(Fromdate, Todate);
        }
        public List<DoctorDashboardClass> Referedbyclinic(string Fromdate, string Todate)
        {
            DoctorDashboardMgr ddmgr = new DoctorDashboardMgr();
            return ddmgr.Referedbyclinic(Fromdate, Todate);
        }
        public List<DoctorDashboardClass> Reference(string Fromdate, string Todate)
        {
            DoctorDashboardMgr ddmgr = new DoctorDashboardMgr();
            return ddmgr.Reference(Fromdate, Todate);
        }
        public List<DoctorDashboardClass> Referedtowhom(string Fromdate, string Todate)
        {
            DoctorDashboardMgr ddmgr = new DoctorDashboardMgr();
            return ddmgr.Referedtowhom(Fromdate, Todate);
        }
        public List<DoctorDashboardClass> Patientwaitingtimes(string Fromdate)
        {
            DoctorDashboardMgr ddmgr = new DoctorDashboardMgr();
            return ddmgr.Patientwaitingtimes(Fromdate);
        }

        public List<DoctorDashboardClass> Patientwaitinginterval(string Fromdate)
        {
            DoctorDashboardMgr ddmgr = new DoctorDashboardMgr();
            return ddmgr.Patientwaitinginterval(Fromdate);
        }
        public List<DoctorDashboardClass> Overallnewexist(string Fromdate)
        {
            DoctorDashboardMgr ddmgr = new DoctorDashboardMgr();
            return ddmgr.Overallnewexist(Fromdate);
        }
        public List<DoctorDashboardClass> Surgerydetails(string Fromdate)
        {
            DoctorDashboardMgr ddmgr = new DoctorDashboardMgr();
            return ddmgr.Surgerydetails(Fromdate);
        }
        public List<DoctorDashboardClass> Doctorsharepharmacy(string Fromdate)
        {
            DoctorDashboardMgr ddmgr = new DoctorDashboardMgr();
            return ddmgr.Doctorsharepharmacy(Fromdate);
        }
        public List<DoctorDashboardClass> Sharesinxray(string Fromdate)
        {
            DoctorDashboardMgr ddmgr = new DoctorDashboardMgr();
            return ddmgr.Sharesinxray(Fromdate);
        }
        public List<DoctorDashboardClass> Sharesinscan(string Fromdate)
        {
            DoctorDashboardMgr ddmgr = new DoctorDashboardMgr();
            return ddmgr.Sharesinscan(Fromdate);
        }
        public List<DoctorDashboardClass> Sharesinlab(string Fromdate)
        {
            DoctorDashboardMgr ddmgr = new DoctorDashboardMgr();
            return ddmgr.Sharesinlab(Fromdate);
        }
    }
}
