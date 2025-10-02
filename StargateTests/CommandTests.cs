using Microsoft.AspNetCore.Http;
using StargateAPI.Business.Commands;
using StargateAPI.Business.Data;

namespace StargateTests
{
    public class CommandTests
    {
        private StargateContext context;
        private const string TEST_NAME = "Test";
        private const string TEST_DUTY_TITLE = "Title";
        private const string TEST_RANK = "Rank";

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
            var request = new CreatePerson { Name = TEST_NAME };

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
        public async Task CreatePerson_ThrowsException_IfNameAlreadyExists()
        {
            // Arrange
            int testId = await Helpers.CreatePersonInDatabaseByName(TEST_NAME);
            var testPerson = await Helpers.GetPersonInDatbaseById(testId);
            var preProcessor = new CreatePersonPreProcessor(context);

            // Act
            // Assert
            Assert.Throws<BadHttpRequestException>(() => preProcessor.Process(new CreatePerson { Name = testPerson.Name }, new CancellationToken()));

            // Cleanup
            await Helpers.DeletePersonInDatabaseById(testId);
        }

        [Test]
        public async Task UpdatePerson_UpdaetesPerson_WithNewName()
        {
            // Arrange
            string expectedName = "Updated";
            int testId = await Helpers.CreatePersonInDatabaseByName(TEST_NAME);
            var originalPerson = await Helpers.GetPersonInDatbaseById(testId);
            var handler = new UpdatePersonHandler(context);
            var request = new UpdatePerson { Id = testId, Name = expectedName };

            // Act
            var result = await handler.Handle(request, new CancellationToken());
            var actual = await Helpers.GetPersonInDatbaseById(result.Id);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result.Success, Is.True);
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
            int testId = await Helpers.CreatePersonInDatabaseByName(TEST_NAME);
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

        // Astronaut Duties
        [Test]
        public async Task CreateDuty_CreatesDuty_ForExistingPerson_WithNoPreviousDuty()
        {
            // Arrange
            var testPersonId = await Helpers.CreatePersonInDatabaseByName(TEST_NAME);
            var handler = new CreateAstronautDutyHandler(context);
            var request = new CreateAstronautDuty { Name = TEST_NAME, DutyTitle = TEST_DUTY_TITLE, Rank = TEST_RANK };

            // Act
            var result = await handler.Handle(request, new CancellationToken());
            var actualPerson = await Helpers.GetPersonInDatbaseById(testPersonId);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result.Success, Is.True);
                Assert.That(actualPerson.CurrentRank, Is.EqualTo(TEST_RANK));
                Assert.That(actualPerson.CurrentDutyTitle, Is.EqualTo(TEST_DUTY_TITLE));
            });

            // Cleanup
            await Helpers.DeletePersonInDatabaseById(testPersonId);
        }

        [Test]
        public async Task CreateDuty_ThrowsException_IfPersonDoesNotExist()
        {
            // Arrange
            var preProcessor = new CreateAstronautDutyPreProcessor(context);
            var request = new CreateAstronautDuty { Name = TEST_NAME, DutyTitle = TEST_DUTY_TITLE, Rank = TEST_RANK };

            // Act
            // Assert
            Assert.Throws<BadHttpRequestException>(() => preProcessor.Process(request, new CancellationToken()));
        }

        [Test]
        public async Task CreateDuty_ThrowsException_IfNoPreviousDuty()
        {
            // Arrange
            var testPersonId = await Helpers.CreatePersonInDatabaseByName(TEST_NAME);
            var preProcessor = new CreateAstronautDutyPreProcessor(context);
            // hard-coding for simplicity
            var request = new CreateAstronautDuty { Name = TEST_NAME, DutyStartDate = DateTime.Parse("2025-10-02 00:00:00.0000000"), DutyTitle = "Test", Rank = TEST_RANK };

            // Act
            // Assert
            Assert.Throws<BadHttpRequestException>(() => preProcessor.Process(request, new CancellationToken()));

            // Cleanup
            await Helpers.DeletePersonInDatabaseById(testPersonId);
        }

    }
}