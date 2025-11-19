using DoctorAppointment.Data.Configuration;
using MyDoctorAppointment.Data.Configuration;
using MyDoctorAppointment.Data.Interfaces;
using MyDoctorAppointment.Domain.Entities;
using Newtonsoft.Json;
using System.Text;

namespace MyDoctorAppointment.Data.Repositories
{
    public class DoctorRepository : GenericRepository<Doctor>, IDoctorRepository
    {
        public override string Path { get; set; }

        public override int LastId { get; set; }

        public DoctorRepository()
        {
            AppSettings settings = ReadFromAppSettings();

            Path = settings.DataBase.Doctors.Path;
            LastId = settings.DataBase.Doctors.LastId;
        }

        public override void ShowInfo(Doctor doctor)
        {
            var sb = new StringBuilder();

            sb.AppendLine("===========================================");
            sb.AppendLine($" Doctor Information (ID: {doctor.Id})");
            sb.AppendLine("===========================================");
            sb.AppendLine($"Name:           {doctor.Name} {doctor.Surname}");
            sb.AppendLine($"Specialization: {doctor.DoctorType}");
            sb.AppendLine($"Experience (years): {doctor.Experience}");
            sb.AppendLine($"Salary:         {doctor.Salary} $");
            sb.AppendLine("--- Contacts ---");
            sb.AppendLine($"Phone:          {doctor.Phone ?? "Not specified"}");
            sb.AppendLine($"Email:          {doctor.Email ?? "Not specified"}");
            sb.AppendLine("===========================================");
            Console.WriteLine(sb);
        }

        protected override void SaveLastId()
        {
            AppSettings settings = ReadFromAppSettings();
            settings.DataBase.Doctors.LastId = LastId;
            string updatedSettings = JsonConvert.SerializeObject(settings, Formatting.Indented);
            File.WriteAllText(Constants.AppSettingsPath, updatedSettings);
        }
    }
}
