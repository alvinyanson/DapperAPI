using DapperData.Data;
using DapperData.Models;
using System;

namespace DapperData.Repository
{
    public class PersonRepository : IPersonRepository
    {
        private readonly IDataAccess _db;

        public PersonRepository(IDataAccess dataAccess)
        {
            _db = dataAccess;
        }

        public async Task<bool> AddPerson(Person person)
        {
            try
            {
                string query = "INSERT INTO dbo.Person (Name, Email) VALUES (@Name, @Email)";
                await _db.SaveData<Person>(query, new Person { Name = person.Name, Email = person.Email });

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> DeletePerson(int id)
        {
            try
            {
                string query = "DELETE FROM dbo.Person WHERE id=@Id";

                await _db.SaveData(query, new { Id = id });

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<IEnumerable<Person>> GetPeople()
        {
            string query = "SELECT * FROM dbo.Person";

            var people = await _db.GetData<Person, dynamic>(query, new { });

            return people;
        }

        public async Task<Person?> GetPersonById(int id)
        {
            string query = "SELECT * FROM dbo.Person WHERE id = @Id";

            var result = await _db.GetData<Person, dynamic>(query, new { Id = id});

            return result.FirstOrDefault();
        }

        public async Task<bool> UpdatePerson(Person person)
        {
            try
            {
                string query = "UPDATE dbo.Person SET name=@Name, email=@Email WHERE id=@Id";

                await _db.SaveData<Person>(query, person);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
