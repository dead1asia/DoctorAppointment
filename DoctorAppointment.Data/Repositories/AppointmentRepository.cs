using DoctorAppointment.Data.Configuration;
using DoctorAppointment.Data.Interfaces;
using MyDoctorAppointment.Data.Configuration;
using MyDoctorAppointment.Data.Repositories;
using MyDoctorAppointment.Domain.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorAppointment.Data.Repositories
{
    public class AppointmentRepository : GenericRepository<Appointment>, IAppointmentRepository
    {
        public override string Path { get; set; }
        public override int LastId { get; set; }
        public AppointmentRepository()
        {
            AppSettings settings = ReadFromAppSettings();
            Path = settings.DataBase.Appointments.Path;
            LastId = settings.DataBase.Appointments.LastId;
        }
        public override void ShowInfo(Appointment appointment)
        {
            var sb = new StringBuilder();

            sb.AppendLine("===========================================");
            sb.AppendLine($" Appointment Information (ID: {appointment.Id})");
            sb.AppendLine("===========================================");
            sb.AppendLine($"Start Time:     {appointment.DateTimeFrom:dd.MM.yyyy HH:mm}");
            sb.AppendLine($"End Time:       {appointment.DateTimeTo:dd.MM.yyyy HH:mm}");
            sb.AppendLine($"Doctor:         {appointment.Doctor?.Name} {appointment.Doctor?.Surname ?? "Not assigned"}");
            sb.AppendLine($"Patient:        {appointment.Patient?.Name} {appointment.Patient?.Surname ?? "Not assigned"}");
            sb.AppendLine($"Description:    {appointment.Description ?? "No description"}");
            sb.AppendLine("--- System Data ---");
            sb.AppendLine($"Created:        {appointment.CreatedAt:dd.MM.yyyy HH:mm:ss}");
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
