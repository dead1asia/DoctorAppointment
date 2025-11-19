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

namespace MyDoctorAppointment
{
    public static class Program
    {
        public static void Main()
        {
            var doctorAppointment = new DoctorAppointment();
            doctorAppointment.Menu();
        }
    }
    public class DoctorAppointment
    {
        private readonly IService<Doctor> doctorService;
        private readonly IService<Patient> patientService;
        private readonly IService<Appointment> appointmentService;

        public DoctorAppointment()
        {
            doctorService = new DoctorService();
            patientService = new PatientService();
            appointmentService = new AppointmentService();
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
        private void AddDoctor()
        {
            var doctor = new Doctor
            {
                Id = 1,
                Name = "Antuan",
                Surname = "Conde",
                Experience = 7,
                DoctorType = DoctorTypes.FamilyDoctor,
                Salary = 7560,
                Phone = "+173644001",
                Email = "acd@gmail.com"
            };
            doctorService.Create(doctor);
            Console.WriteLine($"Added doctor {doctor.Name} {doctor.Surname}");
        }
        private void AddPatient()
        {
            var patient = new Patient
            {
                Id = 1,
                Name = "Elizabeth",
                Surname = "Harrison",
                Phone = "+44 7700 900351",
                Email = "elizabeth.harrison@example.com",
                IllnessType = IllnessTypes.EyeDisease,
                AdditionalInfo = "Requires assistance climbing stairs. History of severe migraines.",
                Address = "24 Richmond Road, London, SW15 1JH, UK",
                CreatedAt = DateTime.Now,
            };
            patientService.Create(patient);
            Console.WriteLine($"Added patient {patient.Name} {patient.Surname}");
        }
        private void AddAppointment()
        {
            var appointment = new Appointment
            {
                Id = 42,
                Patient = new Patient { Name = "Karl", Surname = "Bexler" },
                Doctor = new Doctor { Name = "Henry", Surname = "Korven" },
                DateTimeFrom = new DateTime(2026, 01, 15, 14, 00, 00),
                DateTimeTo = new DateTime(2026, 01, 15, 14, 45, 00),
                Description = "Routine check-up and follow-up consultation regarding migraines.",
                CreatedAt = DateTime.Now,
            };
            appointmentService.Create(appointment);
            Console.WriteLine($"Added new appointment for {appointment.Patient.Name} {appointment.Patient.Surname}\n" +
                $"doctor: {appointment.Doctor.Name} {appointment.Doctor.Surname}");
        }
        private void Showdoctors()
        {
            var allDoctors = doctorService.GetAll();
            foreach (var doctor in allDoctors)
                doctorService.ShowInfo(doctor);
        }
        private void ShowPatients()
        {
            var allPatients = patientService.GetAll();
            foreach (var patient in allPatients)
                patientService.ShowInfo(patient);
        }
        private void ShowAppointments()
        {
            var allAppointments = appointmentService.GetAll();
            foreach (var appointment in allAppointments)
                appointmentService.ShowInfo(appointment);
        }
    }
}