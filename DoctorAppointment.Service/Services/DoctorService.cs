using MyDoctorAppointment.Data.Interfaces;
using MyDoctorAppointment.Data.Repositories;
using MyDoctorAppointment.Domain.Entities;
using MyDoctorAppointment.Service.Interfaces;

namespace MyDoctorAppointment.Service.Services
{
    public class DoctorService : IService<Doctor> 
    {
        private readonly IDoctorRepository doctorRepository;

        public DoctorService()
        {
            doctorRepository = new DoctorRepository();
        }

        public Doctor Create(Doctor doctor)
        {
            return doctorRepository.Create(doctor);
        }

        public bool Delete(int id)
        {
            return doctorRepository.Delete(id);
        }

        public Doctor? Get(int id)
        {
            return doctorRepository.GetById(id);
        }

        public IEnumerable<Doctor> GetAll()
        {
            return doctorRepository.GetAll();
        }

        public Doctor Update(int id, Doctor doctor)
        {
            return doctorRepository.Update(id, doctor);
        }
        public void ShowInfo(Doctor doctor)
        {
            doctorRepository.ShowInfo(doctor);
        }
    }
}
