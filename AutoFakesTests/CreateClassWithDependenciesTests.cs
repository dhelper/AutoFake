using AutoFakes.FakeItEasy;
using AutoFakesTests.TestClasses;
using NUnit.Framework;

namespace AutoFakesTests
{
    [TestFixture, AutoFakeItEasy]
    public class CreateClassWithDependenciesTests
    {
        [CreateIt]
        private ClassWithDependency concreteClass;

        [Test]
        public void ClassCreatedAutomaticallyWithFakedDependencies()
        {
            Assert.IsNotNull(concreteClass);
            Assert.IsNotNull(concreteClass.Dependency);
        }
    }
}
