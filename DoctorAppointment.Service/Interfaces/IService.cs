using MyDoctorAppointment.Domain.Entities;

namespace MyDoctorAppointment.Service.Interfaces
{
    public interface IService<TSource>
    {
        TSource Create(TSource entity);

        IEnumerable<TSource> GetAll();

        TSource? Get(int id);

        bool Delete(int id);

        TSource Update(int id, TSource entity);
        void ShowInfo(TSource entity);
    }
}
