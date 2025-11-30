using DoctorAppointment.Data.Configuration;
using DoctorAppointment.Data.Interfaces;
using DoctorAppointment.Domain.Enums;
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
        private readonly FileExtension _extension;
        public PatientRepository(ISerializer serializer, FileExtension extension)
            : base(serializer)
        {
            Path = Constants.GetPath(SaveLocation.Patients, extension);
            LastId = LastIdManager.Load(extension).PatientsLastId;
            _extension = extension;
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
            LastIdData data = LastIdManager.Load(_extension);
            data.PatientsLastId = LastId;
            LastIdManager.Save(data, _extension);
        }
    }
}
