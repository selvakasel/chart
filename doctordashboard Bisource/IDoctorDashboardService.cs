using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using HealthCareObjects.Dashboard;

namespace PatientServices
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IDoctorDashboardService" in both code and config file together.
    [ServiceContract]
    public interface IDoctorDashboardService
    {
        [OperationContract]
        List<DoctorDashboardClass> Patientlocations(string Fromdate, string Todate);

        [OperationContract]
        List<DoctorDashboardClass> Gender(string Fromdate, string Todate);

        [OperationContract]
        List<DoctorDashboardClass> PatientTypes(string Fromdate, string Todate);

        [OperationContract]
        List<DoctorDashboardClass> PatientCategory(string Fromdate, string Todate);

        [OperationContract]
        List<DoctorDashboardClass> Racepatients(string Fromdate, string Todate);

        [OperationContract]
        List<DoctorDashboardClass> Agegroupforpatients(string Fromdate, string Todate);

        [OperationContract]
        List<DoctorDashboardClass> Refferedto(string Fromdate, string Todate);

        [OperationContract]
        List<DoctorDashboardClass> Referedbyclinic(string Fromdate, string Todate);

        [OperationContract]
        List<DoctorDashboardClass> Reference(string Fromdate, string Todate);

        [OperationContract]
        List<DoctorDashboardClass> Patientwaitingtimes(string Fromdate);

        [OperationContract]
        List<DoctorDashboardClass> Referedtowhom(string Fromdate, string Todate);

        [OperationContract]
        List<DoctorDashboardClass> Patientwaitinginterval(string Fromdate);

        [OperationContract]
        List<DoctorDashboardClass> Overallnewexist(string Fromdate);

        [OperationContract]
        List<DoctorDashboardClass> Surgerydetails(string Fromdate);

        [OperationContract]
        List<DoctorDashboardClass> Doctorsharepharmacy(string Fromdate);


        [OperationContract]
        List<DoctorDashboardClass> Sharesinxray(string Fromdate);

        [OperationContract]
        List<DoctorDashboardClass> Sharesinscan(string Fromdate);

        [OperationContract]
        List<DoctorDashboardClass> Sharesinlab(string Fromdate);
    }
}
