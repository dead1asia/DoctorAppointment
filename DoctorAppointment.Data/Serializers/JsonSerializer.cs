using DoctorAppointment.Data.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorAppointment.Data.Serializers
{
    public class SerializerJson : ISerializer
    {
        public string Serialize(object data)
        {
            return JsonConvert.SerializeObject(data, Formatting.Indented);
        }
        public TSource Deserialize<TSource>(string data)
        {
            return JsonConvert.DeserializeObject<TSource>(data)!;
        }
    }
}
