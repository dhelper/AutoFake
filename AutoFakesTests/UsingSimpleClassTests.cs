using AutoFakes.FakeItEasy;
using FakeItEasy;
using NUnit.Framework;

namespace AutoFakesTests
{
    [TestFixture, AutoFakeItEasy]
    public class UsingSimpleClassTests
    {
        [FakeIt]
        private IDependency _fakeDependency;

        private IDependency _uninitializedDependency;

        [Test]
        public void FakesCreatedAutomatically()
        {
            Assert.That(_fakeDependency, Is.Not.Null);
        }

        [Test]
        public void FieldsWithoutAttributesAreNotInitialized()
        {
            Assert.That(_uninitializedDependency, Is.Null);
        }

        [Test]
        public void CanSetExpectationsOnFakesCreatedAutomatically()
        {
            A.CallTo(() => _fakeDependency.Func()).Returns(42);

            var result = _fakeDependency.Func();

            Assert.That(result, Is.EqualTo(42));
        }

        [TestCase(1)]
        [TestCase(2)]
        public void CanSetDifferentExpectationsOnFakesCreatedAutomatically(int expected)
        {
            A.CallTo(() => _fakeDependency.Func()).Returns(expected);

            var result = _fakeDependency.Func();

            Assert.That(result, Is.EqualTo(expected));
        }
    }

}
