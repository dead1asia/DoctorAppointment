using DoctorAppointment.Data.Interfaces;
using DoctorAppointment.Data.Repositories;
using MyDoctorAppointment.Domain.Entities;
using MyDoctorAppointment.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorAppointment.Service.Services
{
    public class PatientService : IService<Patient>
    {
        private readonly IPatientRepository patientRepository;

        public PatientService()
        {
            patientRepository = new PatientRepository();
        }

        public Patient Create(Patient patient)
        {
            return patientRepository.Create(patient);
        }

        public bool Delete(int id)
        {
            return patientRepository.Delete(id);
        }

        public Patient? Get(int id)
        {
            return patientRepository.GetById(id);
        }

        public IEnumerable<Patient> GetAll()
        {
            return patientRepository.GetAll();
        }

        public Patient Update(int id, Patient patient)
        {
            return patientRepository.Update(id, patient);
        }
        public void ShowInfo(Patient patient)
        {
            patientRepository.ShowInfo(patient);
        }
    }
}
