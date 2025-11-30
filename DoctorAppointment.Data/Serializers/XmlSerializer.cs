using DoctorAppointment.Data.Configuration;
using DoctorAppointment.Data.Interfaces;
using DoctorAppointment.Domain.Enums;
using MyDoctorAppointment.Domain.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DoctorAppointment.Data.Serializers
{
    public class SerializerXml : ISerializer
    {
        public string Serialize(object data)
        {
            var serializer = new XmlSerializer(data.GetType());
            using var writer = new StringWriter();
            serializer.Serialize(writer, data);
            return writer.ToString();
        }

        public TSource Deserialize<TSource>(string data)
        {
            if (string.IsNullOrWhiteSpace(data))
            {
                return Activator.CreateInstance<TSource>();
            }
            var serializer = new XmlSerializer(typeof(TSource));
            using var reader = new StringReader(data);
            var result = serializer.Deserialize(reader)!;
            return (TSource)result;
        }
    }
}
