using DoctorAppointment.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorAppointment.Data.Configuration
{
    public static class Constants
    {
        private static readonly Dictionary<SaveLocation, string> FilePaths = new()
        {
            { SaveLocation.Doctors,      "..\\..\\..\\..\\DoctorAppointment.Data\\MockedDatabase\\doctors" },
            { SaveLocation.Patients,     "..\\..\\..\\..\\DoctorAppointment.Data\\MockedDatabase\\patients" },
            { SaveLocation.Appointments, "..\\..\\..\\..\\DoctorAppointment.Data\\MockedDatabase\\appointments" },
            { SaveLocation.LastId,       "..\\..\\..\\..\\DoctorAppointment.Data\\MockedDatabase\\last_id" }
        };
        public static string GetPath(SaveLocation location, FileExtension extension)
        {
            if (FilePaths.TryGetValue(location, out var path))
            {
                string _extension = extension switch
                {
                    FileExtension.Json => ".json",
                    FileExtension.Xml => ".xml",
                    _ => throw new ArgumentException("Incorrect format")
                };
                return path + _extension;
            }
            throw new ArgumentException("Incorrect location");
        }
    }
}
