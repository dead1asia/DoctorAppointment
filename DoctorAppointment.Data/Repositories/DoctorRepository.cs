using DoctorAppointment.Data.Configuration;
using DoctorAppointment.Data.Interfaces;
using DoctorAppointment.Domain.Enums;
using MyDoctorAppointment.Data.Interfaces;
using MyDoctorAppointment.Domain.Entities;
using Newtonsoft.Json;
using System.Text;
using System.Xml.Serialization;

namespace MyDoctorAppointment.Data.Repositories
{
    public class DoctorRepository : GenericRepository<Doctor>, IDoctorRepository
    {
        public override string Path { get; set; }
        public override int LastId { get; set; }
        private readonly FileExtension _extension;

        public DoctorRepository(ISerializer serializer, FileExtension extension)
            : base(serializer)
        {
            Path = Constants.GetPath(SaveLocation.Doctors, extension);
            LastId = LastIdManager.Load(extension).DoctorsLastId;
            _extension = extension;
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
            LastIdData data = LastIdManager.Load(_extension);
            data.DoctorsLastId = LastId;
            LastIdManager.Save(data, _extension);
        }
    }
}