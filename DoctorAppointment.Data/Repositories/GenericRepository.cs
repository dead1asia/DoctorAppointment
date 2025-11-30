using DoctorAppointment.Data.Configuration;
using DoctorAppointment.Data.Interfaces;
using DoctorAppointment.Data.Repositories;
using MyDoctorAppointment.Data.Interfaces;
using MyDoctorAppointment.Domain.Entities;
using Newtonsoft.Json;
using System.Xml.Linq;

namespace MyDoctorAppointment.Data.Repositories
{
    public abstract class GenericRepository<TSource> : IGenericRepository<TSource> where TSource : Auditable
    {
        private readonly ISerializer _serializer;
        public abstract string Path { get; set; }
        public abstract int LastId { get; set; }

        protected GenericRepository(ISerializer serializer) => _serializer = serializer;
        public TSource Create(TSource source)
        {
            source.Id = ++LastId;
            source.CreatedAt = DateTime.Now;
            var temp = GetAll().Append(source).ToList();
            File.WriteAllText(Path, _serializer.Serialize(temp));
            SaveLastId();

            return source;
        }

        public bool Delete(int id)
        {
            if (GetById(id) is null)
                return false;

            File.WriteAllText(Path, _serializer.Serialize(GetAll().Where(x => x.Id != id)));

            return true;
        }

        public IEnumerable<TSource> GetAll()
        {
            if (!File.Exists(Path))
            {
                return [];
            }

            var data = File.ReadAllText(Path);

            if (string.IsNullOrWhiteSpace(data))
            {
                return [];
            }
            return _serializer.Deserialize<List<TSource>>(data);
        }

        public TSource? GetById(int id)
        {
            return GetAll().FirstOrDefault(x => x.Id == id);
        }

        public TSource Update(int id, TSource source)
        {
            source.UpdatedAt = DateTime.Now;
            source.Id = id;

            File.WriteAllText(Path, _serializer.Serialize(GetAll().Select(x => x.Id == id ? source : x)));

            return source;
        }

        public abstract void ShowInfo(TSource source);

        protected abstract void SaveLastId();
    }
}
