using Microsoft.Extensions.Logging;
using Moq;
using RecruitmentTask.Controllers;
using RecruitmentTask.Models;
using RecruitmentTask.Repositories;
using RecruitmentTask.Services;
using System.Threading.Tasks;
using Xunit;

namespace RecruitmentTasksTests
{
    //Not finished
    public class ClientControllerTests
    {
        [Fact]
        public async Task WhenAddClient_called_Should_returnSuccessResponse()
        {
            var parameter = new ClientAddEditModel
            {
                Email = "jacek.pisak@email.com",
                FirstName = "Jacek",
                LastName = "Pisak"
            };

            // Arrange
            var mockRepo = new Mock<IClientRepository>();
            mockRepo.Setup(repo => repo.AddAsync(parameter))
                .ReturnsAsync(new ClientDto { Id = 1, Email = parameter.Email, FirstName = parameter.FirstName, LastName = parameter.LastName });

            var mockLogger = new Mock<ILogger<ClientService>>();

            var service = new ClientService(mockRepo.Object, mockLogger.Object);

            var controller = new ClientsController(service);

            // Act
            var result = await controller.AddAsync(parameter);

            // Assert
            Assert.NotNull(result.Value);
            Assert.Equal(parameter.Email, result.Value.Email);
        }
    }
}