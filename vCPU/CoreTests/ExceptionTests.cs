using System;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using Core.Exceptions;
using FluentAssertions;
using NUnit.Framework;

namespace CoreTests
{
    [TestFixture]
    public class ExceptionTests
    {
        [Test]
        public void AddressOccupiedMessage()
        {
            var t_Exception = new AddressOccupied("Foobar");

            t_Exception.Message
                .Should()
                .Be("Foobar");
        }

        [Test]
        public void AddressOccupiedMessageInnerException()
        {
            var t_Exception = new AddressOccupied("Foobar", new Exception("Foobar2"));

            t_Exception.Message
                .Should()
                .Be("Foobar");

            t_Exception.InnerException
                .Should()
                .NotBeNull();

            t_Exception.InnerException.Message
                .Should()
                .Be("Foobar2");
        }

        [Test]
        public void AddressOccupiedSerializable()
        {
            var t_Exception = new AddressOccupied();

            var t_Formatter = new BinaryFormatter(new SurrogateSelector(), 
                new StreamingContext(StreamingContextStates.Other) );
        }

        [Test]
        public void AddressOutOfRangeMessage()
        {
            var t_Exception = new AddressOutOfRange("Foobar");

            t_Exception.Message
                .Should()
                .Be("Foobar");
        }

        [Test]
        public void AddressOutOfRangeMessageInnerException()
        {
            var t_Exception = new AddressOutOfRange("Foobar", new Exception("Foobar2"));

            t_Exception.Message
                .Should()
                .Be("Foobar");

            t_Exception.InnerException
                .Should()
                .NotBeNull();

            t_Exception.InnerException.Message
                .Should()
                .Be("Foobar2");
        }

        [Test]
        public void UnknownOperationMessage()
        {
            var t_Exception = new UnknownOperation("Foobar");

            t_Exception.Message
                .Should()
                .Be("Foobar");
        }

        [Test]
        public void UnknownOperationMessageInnerException()
        {
            var t_Exception = new UnknownOperation("Foobar", new Exception("Foobar2"));

            t_Exception.Message
                .Should()
                .Be("Foobar");

            t_Exception.InnerException
                .Should()
                .NotBeNull();

            t_Exception.InnerException.Message
                .Should()
                .Be("Foobar2");
        }

        internal class MockFoobar
        {
            public string MockString { get; set; } = "";
        }
    }
}