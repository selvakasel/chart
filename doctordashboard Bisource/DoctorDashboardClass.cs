using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace HealthCareObjects.Dashboard
{
    [DataContract]
   public class DoctorDashboardClass
   {
       private string _Name;
       private Int64 _AdvAmt;
       private Int64 _Id;
       private Int64 _Count;
       private Int64 _countval;

       [DataMember]
       public string Name
       {
           get { return _Name; }
           set { _Name = value; }
       }
       [DataMember]
       public Int64 AdvAmt
       {
           get { return _AdvAmt; }
           set { _AdvAmt = value; }
       }
       [DataMember]
       public Int64 Id
       {
           get { return _Id; }
           set { _Id = value; }
       }
       [DataMember]
       public Int64 Count
       {
           get { return _Count; }
           set { _Count = value; }
       }
       [DataMember]
       public Int64 countval
       {
           get { return _countval; }
           set { _countval = value; }
       }
        
    }
}
