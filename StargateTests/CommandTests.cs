using StargateAPI.Business.Commands;
using StargateAPI.Business.Data;

namespace StargateTests
{
    public class CommandTests
    {
        private StargateContext context;

        [SetUp]
        public void Setup()
        {
            context = Helpers.GetContext();
        }

        [TearDown]
        public void Teardown()
        {
            context.Dispose();
        }

        [Test]
        public async Task CreatePerson_CreatesPerson_GivenName()
        {
            // Arrange
            var handler = new CreatePersonHandler(context);
            var request = new CreatePerson { Name = "Test" };

            // Act
            var actual = await handler.Handle(request, new CancellationToken());

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(actual, Is.Not.Null);
                Assert.That(actual.Id, Is.GreaterThan(0));
            });

            // Cleanup data
            await Helpers.DeletePersonInDatabaseById(actual.Id);
        }

        [Test]
        public async Task UpdatePerson_UpdaetesPerson_WithNewName()
        {
            // Arrange
            string expectedName = "Updated";
            int testId = await Helpers.CreatePersonInDatabaseByName("Test");
            var originalPerson = await Helpers.GetPersonInDatbaseById(testId);
            var handler = new UpdatePersonHandler(context);
            var request = new UpdatePerson { Id = testId, Name = expectedName };

            // Act
            var result = await handler.Handle(request, new CancellationToken());
            var actual = await Helpers.GetPersonInDatbaseById(result.Id);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(actual.Name, Is.EqualTo(expectedName));
                Assert.That(actual.PersonId, Is.EqualTo(testId));
            });

            // Cleanup
            await Helpers.DeletePersonInDatabaseById(testId);
        }

        [Test]
        public async Task DeletePerson_DeletesPerson_GivenId()
        {
            // Arrange
            int testId = await Helpers.CreatePersonInDatabaseByName("Test");
            var handler = new DeletePersonHandler(context);
            var request = new DeletePerson { Id = testId };

            // Act
            var result = await handler.Handle(request, new CancellationToken());
            bool stillExists = await Helpers.PersonExistsInDatabaseById(testId);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result.Success, Is.True);
                Assert.That(stillExists, Is.False);
            });
        }
    }
}