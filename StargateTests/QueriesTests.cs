using StargateAPI.Business.Data;
using StargateAPI.Business.Queries;

namespace StargateTests
{
    // These tests are simple and make a lot of large assumptions, but I am not interacting with the data too much right now so these are fine
    public class QueriesTests
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
        public async Task GetPeople_ReturnsPeople_IfTheyExist()
        {
            // Arrange
            var handler = new GetPeopleHandler(context);

            // Act
            var result = await handler.Handle(new GetPeople { }, new CancellationToken());

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result.People, Is.Not.Null);
                // This assertion is obviously not very realistic, but helpful for my purposes
                Assert.That(result.People, Has.Count.EqualTo(2));
            });
        }

        [Test]
        public async Task GetPersonById_ReturnsPerson_IfTheyExist()
        {
            // Arrange
            var handler = new GetPersonByIdHandler(context);
            int testId = 1;

            // Act
            var result = await handler.Handle(new GetPersonById { Id = testId }, new CancellationToken());

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result.Person, Is.Not.Null);
                Assert.That(result.Person.PersonId, Is.EqualTo(testId));
            });
        }
    }
}
