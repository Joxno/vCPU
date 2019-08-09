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
        public void TickClock()
        {
            m_Clock.Add(m_Tickable);

            m_Clock.Tick();

            m_Tickable.Ticks
                .Should().Be(1);
            m_Clock.Ticks
                .Should().Be(1);
        }

        [Test]
        public void SuspendAndTickClock()
        {
            m_Clock.Add(m_Tickable);

            m_Clock.Suspend();
            m_Clock.Tick();

            m_Tickable.Ticks
                .Should().Be(0);
            m_Clock.IsSuspended
                .Should().BeTrue();
        }

        [Test]
        public void ForceTickClock()
        {
            m_Clock.Add(m_Tickable);

            m_Clock.Suspend();
            m_Clock.ForceTick();

            m_Tickable.Ticks
                .Should().Be(1);
        }

        [Test]
        public void AddAndRemoveTickableAndTickClock()
        {
            m_Clock.Add(m_Tickable);
            m_Clock.Remove(m_Tickable);
            m_Clock.Tick();

            m_Tickable.Ticks
                .Should().Be(0);
        }

        [Test]
        public void SuspendAndForceTick()
        {
            m_Clock.Add(m_Tickable);

            m_Clock.Suspend();
            m_Clock.ForceTick();

            m_Tickable.Ticks
                .Should().Be(1);
        }

        [Test]
        public void SuspendTickAndResumeTick()
        {
            m_Clock.Add(m_Tickable);

            m_Clock.Suspend();
            m_Clock.Tick();
            m_Clock.Resume();
            m_Clock.Tick();

            m_Tickable.Ticks
                .Should().Be(1);
        }

        [Test]
        public void SetClockSpeed()
        {
            m_Clock.SetFrequency(new TimeSpan().FromMicroseconds(1000));
        }

        [SetUp]
        public void Initialize()
        {
            m_Clock = new Clock();
            m_Tickable = new MockTickable();
        }
    }
}
