using AirplaneParkingAsistant.API.Models;
using AirplaneParkingAsistant.API.Providers;
using FluentAssertions;
using FluentAssertions.Execution;
using Moq;
using NUnit.Framework;

namespace AirplaneParkingAssistant.API.Tests
{
    [TestFixture]
    public class SlotSizeScoreProviderTests
    {
        [Test]
        public void ScoreSlots_WhenSlotIsTooSmall_ReturnZero()
        {
            var airplane = new Airplane { AirplaneId = It.IsAny<int>(), Type = new AirplaneType { Name = "777", Size = 5 } };
            var slot = new Slot { IsEmpty = true, Size = 1, SlotId = It.IsAny<int>() };
            var expected = new ScoredSlot { Score = 0, Slot = slot };
            var sut = new SlotSizeScoreProvider();

            var result = sut.ScoreSlot(slot, airplane);

            using (new AssertionScope())
            {
                result.Should().BeOfType(typeof(ScoredSlot));
                result.Score.Should().Be(expected.Score);
            }
        }

        [Test]
        public void ScoreSlots_WhenSlotIsLargerThanNecessary_ReturnOne()
        {
            var airplane = new Airplane { AirplaneId = It.IsAny<int>(), Type = new AirplaneType { Name = "777", Size = 5 } };
            var slot = new Slot { IsEmpty = true, Size = 10, SlotId = It.IsAny<int>() };
            var expected = new ScoredSlot { Score = 1, Slot = slot };
            var sut = new SlotSizeScoreProvider();

            var result = sut.ScoreSlot(slot, airplane);

            using (new AssertionScope())
            {
                result.Should().BeOfType(typeof(ScoredSlot));
                result.Score.Should().Be(expected.Score);
            }
        }

        [Test]
        public void ScoreSlots_WhenSlotIsBestFit_ReturnFive()
        {
            var airplane = new Airplane { AirplaneId = It.IsAny<int>(), Type = new AirplaneType { Name = "777", Size = 7 } };
            var slot = new Slot { IsEmpty = true, Size = 7, SlotId = It.IsAny<int>() };
            var expected = new ScoredSlot { Score = 5, Slot = slot };
            var sut = new SlotSizeScoreProvider();

            var result = sut.ScoreSlot(slot, airplane);

            using (new AssertionScope())
            {
                result.Should().BeOfType(typeof(ScoredSlot));
                result.Score.Should().Be(expected.Score);
            }
        }
    }
}
