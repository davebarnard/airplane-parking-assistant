using AirplaneParkingAsistant.API.Exceptions;
using AirplaneParkingAsistant.API.Models;
using AirplaneParkingAsistant.API.Providers;
using AirplaneParkingAsistant.API.Repositories;
using AirplaneParkingAsistant.API.Service;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using System;
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
            var startTime = DateTime.Now;
            var duration = 9;
            var airplane = new Airplane();
            var slots = Enumerable.Empty<Slot>();
            _mockSlotRepository.Setup(r => r.GetAvailableSlots()).ReturnsAsync(slots);
            var sut = new SlotService(_mockScoreProvider.Object, _mockSlotRepository.Object);

            Func<Task> result = async () 
                => await sut.GetRecommendedSlot(startTime, duration, airplane);

            result.Should().ThrowAsync<NoAvailableSlotsException>();
        }

        [Test]
        public void GetRecommendedSlot_WhenNoAppropriateAvailableSlot_ThrowsNoAppropriateAvailableSlotsException()
        {
            var startTime = DateTime.Now;
            var duration = 9;
            var airplane = new Airplane { AirplaneId = It.IsAny<int>(), Type = new AirplaneType { Name = "777", Size = 5 } };
            var emptySlot = new ScoredSlot { IsEmpty = true, Size = 1, SlotId = It.IsAny<int>(), Score = 0 };
            var slots = new List<Slot> {emptySlot};
            _mockSlotRepository.Setup(r => r.GetAvailableSlots()).ReturnsAsync(slots);
            _mockScoreProvider.Setup(s => s.ScoreSlot(emptySlot, airplane)).Returns(emptySlot);

            var sut = new SlotService(_mockScoreProvider.Object, _mockSlotRepository.Object);

            Func<Task> result = async ()
                => await sut.GetRecommendedSlot(startTime, duration, airplane);

            result.Should().ThrowAsync<NoAppropriateAvailableSlotsException>();
        }

        [Test]
        public async Task GetRecommendedSlot_WhenOneAppropriateAvailableSlot_ReturnSlot()
        {
            var startTime = DateTime.Now;
            var duration = 9;
            var airplane = new Airplane { AirplaneId = It.IsAny<int>(), Type = new AirplaneType { Name = "777", Size = 5 } };
            var emptySlot = new ScoredSlot { IsEmpty = true, Size = 1, SlotId = It.IsAny<int>(), Score = 1 };
            var slots = new List<Slot> { emptySlot };
            _mockSlotRepository.Setup(r => r.GetAvailableSlots()).ReturnsAsync(slots);
            _mockScoreProvider.Setup(s => s.ScoreSlot(emptySlot, airplane)).Returns(emptySlot);

            var sut = new SlotService(_mockScoreProvider.Object, _mockSlotRepository.Object);

            var result = await sut.GetRecommendedSlot(startTime, duration, airplane);

            result.Should().Be(emptySlot);
        }

        [Test]
        public async Task GetRecommendedSlot_WhenMultipleAppropriateAvailableSlots_ReturnSlotWithHighestScore()
        {
            var startTime = DateTime.Now;
            var duration = 9;
            var airplane = new Airplane { AirplaneId = It.IsAny<int>(), Type = new AirplaneType { Name = "777", Size = 5 } };
            var emptySlot1 = new ScoredSlot { IsEmpty = true, Size = 1, SlotId = It.IsAny<int>(), Score = 0 };
            var emptySlot2 = new ScoredSlot { IsEmpty = true, Size = 5, SlotId = It.IsAny<int>(), Score = 5 };
            var emptySlot3 = new ScoredSlot { IsEmpty = true, Size = 10, SlotId = It.IsAny<int>(), Score = 1 };
            var slots = new List<ScoredSlot> { emptySlot1, emptySlot2, emptySlot3 };
            _mockSlotRepository.Setup(r => r.GetAvailableSlots()).ReturnsAsync(slots);
            _mockScoreProvider.Setup(s => s.ScoreSlot(emptySlot1, airplane)).Returns(emptySlot1);
            _mockScoreProvider.Setup(s => s.ScoreSlot(emptySlot2, airplane)).Returns(emptySlot2);
            _mockScoreProvider.Setup(s => s.ScoreSlot(emptySlot3, airplane)).Returns(emptySlot3);

            var sut = new SlotService(_mockScoreProvider.Object, _mockSlotRepository.Object);

            var result = await sut.GetRecommendedSlot(startTime, duration, airplane);

            result.Should().Be(emptySlot2);
        }

        [Test]
        public void ReserveSlot_WhenSlotAlreadyReserved_ThrowsSlotAlreadyReservedException()
        {
            var airplane = new Airplane();
            var slotId = It.IsAny<int>();
            var reservedSlot = new ReservedSlot { IsEmpty = true, Size = 1, SlotId = slotId, StartTime = DateTime.Now, ExpiryTime = DateTime.Now.AddHours(7) };
            _mockSlotRepository.Setup(r => r.IsSlotEmpty(slotId)).ReturnsAsync(false);
            var sut = new SlotService(_mockScoreProvider.Object, _mockSlotRepository.Object);

            Func<Task> result = async () => await sut.ReserveSlot(reservedSlot, airplane);

            result.Should().ThrowAsync<SlotAlreadyReservedException>();
        }
    }
}