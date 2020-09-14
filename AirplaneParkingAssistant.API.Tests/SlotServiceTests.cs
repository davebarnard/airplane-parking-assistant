using AirplaneParkingAsistant.API.Exceptions;
using AirplaneParkingAsistant.API.Models;
using AirplaneParkingAsistant.API.Providers;
using AirplaneParkingAsistant.API.Repositories;
using AirplaneParkingAsistant.API.Service;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirplaneParkingAssistant.API.Tests
{
    [TestFixture]
    public class SlotServiceTests
    {
        private Mock<ISlotScoreProvider> _mockScoreProvider;
        private Mock<ISlotRepository> _mockSlotRepository;

        [SetUp]
        public void Setup()
        {
            _mockScoreProvider = new Mock<ISlotScoreProvider>();
            _mockSlotRepository = new Mock<ISlotRepository>();
        }

        [Test]
        public void GetRecommendedSlot_WhenNoAvailableSlot_ThrowsNoAvailableSlotsException()
        {
            var airplane = new Airplane();
            var slots = Enumerable.Empty<Slot>();
            _mockSlotRepository.Setup(r => r.GetAvailableSlots()).ReturnsAsync(slots);
            var sut = new SlotService(_mockScoreProvider.Object, _mockSlotRepository.Object);

            Assert.ThrowsAsync<NoAvailableSlotsException>( async () => await sut.GetRecommendedSlot(airplane));
        }

        [Test]
        public void GetRecommendedSlot_WhenNoAppropriateAvailableSlot_ThrowsNoAppropriateAvailableSlotsException()
        {
            var airplane = new Airplane { AirplaneId = It.IsAny<int>(), Type = new AirplaneType { Name = "777", Size = 5 } };
            var emptySlot = new Slot { IsEmpty = true, Size = 1, SlotId = It.IsAny<int>() };
            var slots = new List<Slot> {emptySlot};
            _mockSlotRepository.Setup(r => r.GetAvailableSlots()).ReturnsAsync(slots);
            _mockScoreProvider.Setup(s => s.ScoreSlot(emptySlot, airplane)).Returns(new ScoredSlot { Score = 0, Slot = emptySlot });

            var sut = new SlotService(_mockScoreProvider.Object, _mockSlotRepository.Object);

            Assert.ThrowsAsync<NoAppropriateAvailableSlotsException>(async () => await sut.GetRecommendedSlot(airplane));
        }

        [Test]
        public async Task GetRecommendedSlot_WhenOneAppropriateAvailableSlot_ReturnSlot()
        {
            var airplane = new Airplane { AirplaneId = It.IsAny<int>(), Type = new AirplaneType { Name = "777", Size = 5 } };
            var emptySlot = new Slot { IsEmpty = true, Size = 1, SlotId = It.IsAny<int>() };
            var slots = new List<Slot> { emptySlot };
            _mockSlotRepository.Setup(r => r.GetAvailableSlots()).ReturnsAsync(slots);
            _mockScoreProvider.Setup(s => s.ScoreSlot(emptySlot, airplane)).Returns(new ScoredSlot { Score = 1, Slot = emptySlot });

            var sut = new SlotService(_mockScoreProvider.Object, _mockSlotRepository.Object);

            var result = await sut.GetRecommendedSlot(airplane);

            Assert.AreEqual(emptySlot, result);
        }

        [Test]
        public async Task GetRecommendedSlot_WhenMultipleAppropriateAvailableSlots_ReturnSlotWithHighestScore()
        {
            var airplane = new Airplane { AirplaneId = It.IsAny<int>(), Type = new AirplaneType { Name = "777", Size = 5 } };
            var emptySlot1 = new Slot { IsEmpty = true, Size = 1, SlotId = It.IsAny<int>() };
            var emptySlot2 = new Slot { IsEmpty = true, Size = 5, SlotId = It.IsAny<int>() };
            var emptySlot3 = new Slot { IsEmpty = true, Size = 10, SlotId = It.IsAny<int>() };
            var slots = new List<Slot> { emptySlot1, emptySlot2, emptySlot3 };
            _mockSlotRepository.Setup(r => r.GetAvailableSlots()).ReturnsAsync(slots);
            _mockScoreProvider.Setup(s => s.ScoreSlot(emptySlot1, airplane)).Returns(new ScoredSlot { Score = 0, Slot = emptySlot1 });
            _mockScoreProvider.Setup(s => s.ScoreSlot(emptySlot2, airplane)).Returns(new ScoredSlot { Score = 5, Slot = emptySlot2 });
            _mockScoreProvider.Setup(s => s.ScoreSlot(emptySlot3, airplane)).Returns(new ScoredSlot { Score = 1, Slot = emptySlot3 });

            var sut = new SlotService(_mockScoreProvider.Object, _mockSlotRepository.Object);

            var result = await sut.GetRecommendedSlot(airplane);

            Assert.AreEqual(emptySlot2, result);
        }
    }
}