using DoctorAppointment.Data.Interfaces;
using DoctorAppointment.Data.Repositories;
using MyDoctorAppointment.Data.Interfaces;
using MyDoctorAppointment.Data.Repositories;
using MyDoctorAppointment.Domain.Entities;
using MyDoctorAppointment.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorAppointment.Service.Services
{
    public class AppointmentService : IService<Appointment>
    {
        private readonly IAppointmentRepository appointmentRepository;

        public AppointmentService()
        {
            appointmentRepository = new AppointmentRepository();
        }

        public Appointment Create(Appointment appointment)
        {
            return appointmentRepository.Create(appointment);
        }

        public bool Delete(int id)
        {
            return appointmentRepository.Delete(id);
        }

        public Appointment? Get(int id)
        {
            return appointmentRepository.GetById(id);
        }

        public IEnumerable<Appointment> GetAll()
        {
            return appointmentRepository.GetAll();
        }

        public Appointment Update(int id, Appointment appointment)
        {
            return appointmentRepository.Update(id, appointment);
        }
        public void ShowInfo(Appointment appointment)
        {
            appointmentRepository.ShowInfo(appointment);
        }
    }
}
