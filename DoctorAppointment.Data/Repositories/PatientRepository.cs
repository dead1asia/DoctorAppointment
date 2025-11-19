using DoctorAppointment.Data.Configuration;
using DoctorAppointment.Data.Interfaces;
using MyDoctorAppointment.Data.Configuration;
using MyDoctorAppointment.Data.Repositories;
using MyDoctorAppointment.Domain.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace DoctorAppointment.Data.Repositories
{
    public class PatientRepository : GenericRepository<Patient>, IPatientRepository
    {
        public override string Path { get; set; }
        public override int LastId { get; set; }
        public PatientRepository()
        {
            AppSettings settings = ReadFromAppSettings();
            Path = settings.DataBase.Patients.Path;
            LastId = settings.DataBase.Patients.LastId;
        }

        public override void ShowInfo(Patient patient)
        {
            var sb = new StringBuilder();

            sb.AppendLine("===========================================");
            sb.AppendLine($" Patient Information (ID: {patient.Id})");
            sb.AppendLine("===========================================");
            sb.AppendLine($"Name:           {patient.Name} {patient.Surname}");
            sb.AppendLine($"Illness Type:   {patient.IllnessType}");
            sb.AppendLine("--- Contacts ---");
            sb.AppendLine($"Phone:          {patient.Phone ?? "Not specified"}");
            sb.AppendLine($"Email:          {patient.Email ?? "Not specified"}");
            sb.AppendLine($"Address:        {patient.Address ?? "Not specified"}");
            sb.AppendLine("--- Additional Info ---");
            sb.AppendLine($"Notes:          {patient.AdditionalInfo ?? "None"}");
            sb.AppendLine("===========================================");
            Console.WriteLine(sb);
        }
        protected override void SaveLastId()
        {
            AppSettings settings = ReadFromAppSettings();
            settings.DataBase.Patients.LastId = LastId;
            string updatedSettings = JsonConvert.SerializeObject(settings, Formatting.Indented);
            File.WriteAllText(Constants.AppSettingsPath, updatedSettings);
        }
    }
}
