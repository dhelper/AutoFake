using AutoFakes.FakeItEasy;
using FakeItEasy;
using NUnit.Framework;

namespace AutoFakesTests
{
    [AutoFakeItEasy]
    public interface IAutoFake
    {
        
    }

    [TestFixture]
    public class UsingInterfaceTests : IAutoFake
    {
        [FakeIt]
        private IDependency _fakeDependency;

        [Test]
        public void CanSetExpectationsOnFieldsCreatedAutomatically()
        {
            A.CallTo(() => _fakeDependency.Func()).Returns(42);

            var result = _fakeDependency.Func();

            Assert.That(result, Is.EqualTo(42));
        }
    }
}
