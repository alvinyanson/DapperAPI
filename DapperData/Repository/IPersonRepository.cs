
using DapperData.Models;

namespace DapperData.Repository
{
    public interface IPersonRepository
    {
        Task<bool> AddPerson(Person person);

        Task<bool> UpdatePerson(Person person);

        Task<bool> DeletePerson(int id);

        Task<Person?> GetPersonById(int id);

        Task<IEnumerable<Person>> GetPeople();
    }
}
