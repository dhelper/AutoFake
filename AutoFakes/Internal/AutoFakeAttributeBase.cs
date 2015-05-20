using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NUnit.Framework;

namespace AutoFakes.FakeItEasy.Internal
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, AllowMultiple = false)]
    public abstract class AutoFakeAttributeBase : Attribute, ITestAction
    {
        private readonly IFakeHelper _fakeHelper;

        protected AutoFakeAttributeBase(IFakeHelper fakeHelper)
        {
            _fakeHelper = fakeHelper;
        }

        private IEnumerable<FieldInfo> _testFields;

        public void BeforeTest(TestDetails details)
        {
            var isTestFixture = details.Method == null;

            if (isTestFixture)
            {
                DiscoverFieldsToFake(details);

                return;
            }

            foreach (var testField in _testFields)
            {
                var fakeObject = _fakeHelper.DynamicallyCreateFakeObject(testField.FieldType);

                testField.SetValue(details.Fixture, fakeObject);
            }
        }

        public void AfterTest(TestDetails details)
        {
        }

        private void DiscoverFieldsToFake(TestDetails details)
        {
            _testFields = details.Fixture.GetType()
                .GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                .Where(testField => testField.CustomAttributes.Any(data => data.AttributeType == typeof(FakeItAttribute)));
        }

        public ActionTargets Targets
        {
            get { return ActionTargets.Test | ActionTargets.Suite; }
        }
    }
}