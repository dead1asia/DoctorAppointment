using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorAppointment.Data.Configuration
{
    public class AppSettings
    {
        public DataBase DataBase { get; set; }
    }

    public class DataBase
    {
        public UserSettings Doctors { get; set; }
        public UserSettings Patients { get; set; }
        public UserSettings Appointments { get; set; }
    }
    public class UserSettings
    {
        public int LastId { get; set; }
        public string Path { get; set; }
    }
}
