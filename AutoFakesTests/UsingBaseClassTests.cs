using AutoFakes.FakeItEasy;
using FakeItEasy;
using NUnit.Framework;

namespace AutoFakesTests
{
    [AutoFakeItEasy]
    public class TestBase
    {
        [FakeIt]
        protected IDependency _fakeDependencyBase;
    }

    [TestFixture]
    public class UsingBaseClassTests : TestBase
    {
        [FakeIt]
        protected IDependency _fakeDependencyDerived;

        [Test]
        public void CanSetExpectationsOnFakesOfBaseClass()
        {
            A.CallTo(() => _fakeDependencyBase.Func()).Returns(42);

            var result = _fakeDependencyBase.Func();

            Assert.That(result, Is.EqualTo(42));
        }

        [Test]
        public void CanSetExpectationsOnFakesOfDerivedClass()
        {
            A.CallTo(() => _fakeDependencyDerived.Func()).Returns(42);

            var result = _fakeDependencyDerived.Func();

            Assert.That(result, Is.EqualTo(42));
        }
    }
}
