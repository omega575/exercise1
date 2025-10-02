using Microsoft.EntityFrameworkCore;
using StargateAPI.Business.Commands;
using StargateAPI.Business.Data;
using StargateAPI.Business.Dtos;
using StargateAPI.Business.Queries;

namespace StargateTests
{
    public static class Helpers
    {
        static string ConnectionString = "Server=(localdb)\\mssqllocaldb;Database=starbase;Trusted_Connection=True;";
        public static StargateContext GetContext()
        {
            var options = new DbContextOptionsBuilder<StargateContext>()
                .UseSqlServer(ConnectionString)
                .Options;

            return new StargateContext(options);
        }

        public async static Task DeletePersonInDatabaseById(int id)
        {
            DeletePersonHandler handler = new(GetContext());

            await handler.Handle(new DeletePerson { Id = id }, new CancellationToken());
        }

        public async static Task<int> CreatePersonInDatabaseByName(string name)
        {
            CreatePersonHandler handler = new(GetContext());

            var result = await handler.Handle(new CreatePerson { Name = name }, new CancellationToken());

            return result.Id;
        }

        public async static Task<bool> PersonExistsInDatabaseById(int id)
        {
            var person = await GetPersonInDatbaseById(id);

            return person is not null;
        }

        public async static Task<PersonAstronaut?> GetPersonInDatbaseById(int id)
        {
            GetPersonByIdHandler handler = new(GetContext());

            var result = await handler.Handle(new GetPersonById { Id = id }, new CancellationToken());

            return result.Person;
        }
    }
}
