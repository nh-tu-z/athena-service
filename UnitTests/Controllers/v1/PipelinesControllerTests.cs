using Dapper;
using FluentAssertions;
using Moq;
using Xunit;
using AutoMapper;
using AthenaService.AutoMappers;
using AthenaService.Persistence;
using AthenaService.Services;
using AthenaService.Domain.ViewModels;

using static AthenaService.Domain.Base.Enums;

namespace AthenaService.UnitTests.Controllers
{
    public class PipelinesControllerTests
    {
        private Mock<IPersistenceService> _persistenceService = new();
        private PipelineService _pipelineService;
        private IMapper _mapper;

        public PipelinesControllerTests()
        {
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new TagProfile());
            });

            _mapper = mapperConfig.CreateMapper();

            _pipelineService = new PipelineService(_mapper, _persistenceService.Object);
            
        }

        [Fact]
        public async Task CreatePipelineAsync_AllValidArgumet_ShouldReturnValue()
        {
            // Arrage
            var createPipelineInputModel = new CreatePipelineDetailInputModel()
            {
                PipelineName = "Pipeline-1",
                SecurityScore = 50,
                Criticality = CriticalityEnums.Low,
                State = StateEnums.Production,
                LastActivity = "Deployment added",
                LastActivityTimeStamp = DateTime.UtcNow
            };

            _persistenceService.Setup(svc => svc.QuerySingleOrDefaultAsync<Guid>(It.IsAny<string>(), It.IsAny<DynamicParameters>()))
                .ReturnsAsync(Guid.NewGuid())
                .Verifiable();

            // Act
            var result = await _pipelineService.CreatePipelineAsync(createPipelineInputModel);

            // Assert
            result.Should().NotBe(Guid.Empty);
            _persistenceService.Verify(svc => svc.QuerySingleOrDefaultAsync<Guid>(It.IsAny<string>(), It.IsAny<DynamicParameters>()), Times.Once());
        }

        [Fact]
        public void PassingTest()
        {
            Assert.Equal(4, Add(2, 2));
        }

        [Fact]
        public void FailingTest()
        {
            Assert.Equal(5, Add(3, 2));
        }

        int Add(int x, int y)
        {
            return x + y;
        }

        [Theory]
        [InlineData(3)]
        [InlineData(5)]
        [InlineData(7)]
        public void MyFirstTheory(int value)
        {
            Assert.True(IsOdd(value));
        }

        bool IsOdd(int value)
        {
            return value % 2 == 1;
        }
    }
}
