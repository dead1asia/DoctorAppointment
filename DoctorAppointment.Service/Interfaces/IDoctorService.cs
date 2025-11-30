using MyDoctorAppointment.Domain.Entities;
using MyDoctorAppointment.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorAppointment.Service.Interfaces
{
    public interface IDoctorService : IService<Doctor>
    {
        void ShowInfo(Doctor doctor);
    }
}
