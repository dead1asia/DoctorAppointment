using DoctorAppointment.Data.Configuration;
using DoctorAppointment.Data.Interfaces;
using DoctorAppointment.Domain.Enums;
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
        private readonly FileExtension _extension;
        public AppointmentRepository(ISerializer serializer, FileExtension extension)
            : base(serializer)
        {
            Path = Constants.GetPath(SaveLocation.Appointments, extension);
            LastId = LastIdManager.Load(extension).AppointmentsLastId;
            _extension = extension;
        }

        public void ShowInfo(Appointment appointment, Doctor doctor, Patient patient)
        {
            
            var sb = new StringBuilder();

            sb.AppendLine("===========================================");
            sb.AppendLine($" Appointment Information (ID: {appointment.Id})");
            sb.AppendLine("===========================================");
            sb.AppendLine($"Start Time:     {appointment.DateTimeFrom:dd.MM.yyyy HH:mm}");
            sb.AppendLine($"End Time:       {appointment.DateTimeTo:dd.MM.yyyy HH:mm}");
            sb.AppendLine($"Doctor:         {doctor.Name} {doctor.Surname}");
            sb.AppendLine($"Patient:        {patient.Name} {patient.Surname}");
            sb.AppendLine($"Description:    {appointment.Description ?? "No description"}");
            sb.AppendLine("--- System Data ---");
            sb.AppendLine($"Created:        {appointment.CreatedAt:dd.MM.yyyy HH:mm:ss}");
            sb.AppendLine("===========================================");
            Console.WriteLine(sb);
        }
        public override void ShowInfo(Appointment source) { }
        protected override void SaveLastId()
        {
            LastIdData data = LastIdManager.Load(_extension);
            data.AppointmentsLastId = LastId;
            LastIdManager.Save(data, _extension);
        }
    }
}
