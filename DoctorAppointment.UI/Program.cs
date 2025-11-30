using DoctorAppointment.Data.Interfaces;
using DoctorAppointment.Data.Repositories;
using DoctorAppointment.Data.Serializers;
using DoctorAppointment.Domain.Enums;
using DoctorAppointment.Service.Services;
using MyDoctorAppointment.Data.Repositories;
using MyDoctorAppointment.Domain.Entities;
using MyDoctorAppointment.Domain.Enums;
using MyDoctorAppointment.Service.Interfaces;
using MyDoctorAppointment.Service.Services;
using System.Net.WebSockets;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using DataGenerator;
using DoctorAppointment.Service.Interfaces;

namespace MyDoctorAppointment
{
    public static class Program
    {
        public static void Main()
        {
            FileExtension chosenExtension = DoctorAppointment.ChooseFileFormat();

            ISerializer serializer = chosenExtension switch
            {
                FileExtension.Xml => new SerializerXml(),
                FileExtension.Json => new SerializerJson(),
                _ => throw new NotImplementedException(),
            };
            var doctorAppointment = new DoctorAppointment(serializer, chosenExtension);
            doctorAppointment.Menu();
        }
    }
    public class DoctorAppointment
    {
        private readonly IDoctorService _doctorService;
        private readonly IPatientService _patientService;
        private readonly IAppointmentService _appointmentService;

        public DoctorAppointment(ISerializer serializer, FileExtension extension)
        {
            _doctorService = new DoctorService(serializer, extension);
            _patientService = new PatientService(serializer, extension);
            _appointmentService = new AppointmentService(serializer, extension);
        }
        public static FileExtension ChooseFileFormat()
        {
            while (true)
            {
                Console.WriteLine("===========================================");
                Console.WriteLine("         Preferred data format");
                Console.WriteLine("===========================================");
                Console.WriteLine("1. JSON");
                Console.WriteLine("2. XML");
                Console.WriteLine("3. Exit");
                Console.Write("Choode option: ");
                string? input = Console.ReadLine();
                if (int.TryParse(input, out int inputChoice) && inputChoice >= 1 && inputChoice <= 3)
                {
                    switch (inputChoice)
                    {
                        case 1: return FileExtension.Json;
                        case 2: return FileExtension.Xml;
                        case 3: Environment.Exit(0); break;
                    }
                }
                else
                {
                    Console.WriteLine("Incorrect input");
                }
            }
        }
        public void Menu()
        {
            bool switcher = true;
            while (switcher)
            {
                var sb = new StringBuilder();
                sb.AppendLine("===========================================");
                sb.AppendLine("         DOCTOR APPOINTMENT MENU");
                sb.AppendLine("===========================================");
                sb.AppendLine("1. Add new doctor");
                sb.AppendLine("2. Add new patient");
                sb.AppendLine("3. Add new appointment");
                sb.AppendLine("4. Show all doctors");
                sb.AppendLine("5. Show all patients");
                sb.AppendLine("6. Show all appointments");
                sb.AppendLine("7. Exit");
                Console.WriteLine(sb);
                Console.Write("Choose option: ");
                string? input = Console.ReadLine();
                if (int.TryParse(input, out int inputChoice))
                {
                    MenuOptions menuChoice = (MenuOptions)inputChoice;
                    switch (menuChoice)
                    {
                        case MenuOptions.AddDoctor: AddDoctor(); break;
                        case MenuOptions.AddPatients: AddPatient(); break;
                        case MenuOptions.AddAppointments: AddAppointment(); break;
                        case MenuOptions.ShowDoctors: Showdoctors(); break;
                        case MenuOptions.ShowPatients: ShowPatients(); break;
                        case MenuOptions.ShowAppointments: ShowAppointments(); break;
                        case MenuOptions.Exit: switcher = false; break;
                        default: Console.WriteLine("The selected option number is not valid. Please try again."); break;
                    }
                }
                else
                {
                    Console.WriteLine("Incorrect input");
                }

            }
        }
        private readonly Random rnd = new();
        private void AddDoctor()
        {
            var doctor = new Doctor
            {
                Name = Generator.GetFirstName(),
                Surname = Generator.GetLastName(),
                Experience = (byte)rnd.Next(1, 25),
                DoctorType = (DoctorTypes)rnd.Next(1, 4),
                Salary = rnd.Next(2500, 10500),
                Phone = "+173644001",
                Email = "acd@gmail.com"
            };
            _doctorService.Create(doctor);
            Console.WriteLine($"Added doctor {doctor.Name} {doctor.Surname}");
        }
        private void AddPatient()
        {
            var patient = new Patient
            {
                Name = Generator.GetFirstName(),
                Surname = Generator.GetLastName(),
                Phone = "+44 7700 900351",
                Email = "abcd@example.com",
                IllnessType = (IllnessTypes)rnd.Next(1, 5),
                AdditionalInfo = "Some important additional info",
                Address = "24 Richmond Road, London, SW15 1JH, UK",
                CreatedAt = DateTime.Now,
            };
            _patientService.Create(patient);
            Console.WriteLine($"Added patient {patient.Name} {patient.Surname}");
        }
        private void AddAppointment()
        {
            var allDoctors = _doctorService.GetAll().ToList();
            var allPatients = _patientService.GetAll().ToList();
            if (allDoctors.Count == 0 || allPatients.Count == 0)
            {
                Console.WriteLine("There are no records in the database");
                return;
            }
            var doctor = allDoctors[rnd.Next(allDoctors.Count)];
            var patient = allPatients[rnd.Next(allPatients.Count)];

            var appointment = new Appointment
            {
                PatientId = patient.Id,
                DoctorId = doctor.Id,
                DateTimeFrom = new DateTime(2026, 01, 15, 14, 00, 00),
                DateTimeTo = new DateTime(2026, 01, 15, 14, 45, 00),
                Description = "Some interesting description :)",
                CreatedAt = DateTime.Now,
            };
            _appointmentService.Create(appointment);

            Console.WriteLine(
                $"Added new appointment for {patient.Name} {patient.Surname}\n" +
                $"doctor: {doctor.Name} {doctor.Surname}");
        }
        private void Showdoctors()
        {
            var allDoctors = _doctorService.GetAll();
            foreach (var doctor in allDoctors)
                _doctorService.ShowInfo(doctor);
        }
        private void ShowPatients()
        {
            var allPatients = _patientService.GetAll();
            foreach (var patient in allPatients)
                _patientService.ShowInfo(patient);
        }
        public void ShowAppointments()
        {
            var doctors = _doctorService.GetAll();
            var patients = _patientService.GetAll();
            var allAppointments = _appointmentService.GetAll();
            foreach (var appointment in allAppointments)
            {
                var doctor = doctors.FirstOrDefault(x => x.Id == appointment.DoctorId)!;
                var patient = patients.FirstOrDefault(x => x.Id == appointment.PatientId)!;
                _appointmentService.ShowInfo(appointment, doctor, patient);
            }
        }
    }
}