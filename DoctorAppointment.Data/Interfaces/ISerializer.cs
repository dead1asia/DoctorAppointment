using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorAppointment.Data.Interfaces
{
    public interface ISerializer
    {
        string Serialize(object data);
        TSource Deserialize<TSource>(string data);
    }
}
