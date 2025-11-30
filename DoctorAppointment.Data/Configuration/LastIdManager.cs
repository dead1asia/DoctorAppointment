using DoctorAppointment.Domain.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DoctorAppointment.Data.Serializers;
using DoctorAppointment.Data.Interfaces;

namespace DoctorAppointment.Data.Configuration
{
    public static class LastIdManager
    {
        public static LastIdData Load(FileExtension extension)
        {
            ISerializer _serializer = extension switch
            {
                FileExtension.Json => new SerializerJson(),
                FileExtension.Xml => new SerializerXml(),
                _ => throw new ArgumentException("Incorrect file format")
            };
            string path = Constants.GetPath(SaveLocation.LastId, extension);
            string temp;
            if(!File.Exists(path))
            {
                var emptyFile = new LastIdData();
                temp = _serializer.Serialize(emptyFile);
                File.WriteAllText(path, temp);
                return emptyFile;
            }
            temp = File.ReadAllText(path);
            return _serializer.Deserialize<LastIdData>(temp)!;
        }
        public static void Save(LastIdData data, FileExtension extension)
        {
            ISerializer _serializer = extension switch
            {
                FileExtension.Json => new SerializerJson(),
                FileExtension.Xml => new SerializerXml(),
                _ => throw new ArgumentException("Incorrect file format")
            };
            string path = Constants.GetPath(SaveLocation.LastId, extension);
            var temp = _serializer.Serialize(data);
            File.WriteAllText(path, temp);
        }
    }
}
