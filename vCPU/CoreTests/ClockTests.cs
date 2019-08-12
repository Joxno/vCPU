using System;
using Core.Components;
using Core.Interfaces;
using Core.Utility.Extensions;
using CoreTests.Mocks;
using FluentAssertions;
using NUnit.Framework;
using Moq;

namespace CoreTests
{
    [TestFixture]
    public class ClockTests
    {
        private Clock m_Clock = null;
        private MockTickable m_Tickable = null;

        [Test]
        public void SetFrequencyAndStartClock()
        {
            m_Clock.Add(m_Tickable);

            m_Clock.SetFrequency(new TimeSpan(0));
            m_Clock.Start();
            m_Clock.Oscillate();

            m_Tickable.Ticks
                .Should().Be(1);
        }

        [Test]
        public void StartStopClock()
        {
            m_Clock.Add(m_Tickable);

            m_Clock.SetFrequency(new TimeSpan().FromMicroseconds(1000));
            m_Clock.Start();
            m_Clock.Stop();
            m_Clock.Oscillate();

            m_Tickable.Ticks
                .Should().Be(0);
        }

        [Test]
        public void AddAndRemoveTickable()
        {
            m_Clock.SetFrequency(new TimeSpan(0));
            m_Clock.Start();

            m_Clock.Add(m_Tickable);
            m_Clock.Remove(m_Tickable);

            m_Clock.Oscillate();

            m_Tickable.Ticks
                .Should().Be(0);

        }

        [SetUp]
        public void Initialize()
        {
            m_Clock = new Clock();
            m_Tickable = new MockTickable();
        }
    }
}
