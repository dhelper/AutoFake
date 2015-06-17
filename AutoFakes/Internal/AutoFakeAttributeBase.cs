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
        private IEnumerable<FieldInfo> _fakeFields;
        private IEnumerable<FieldInfo> _createFields;

        protected AutoFakeAttributeBase(IFakeHelper fakeHelper)
        {
            _fakeHelper = fakeHelper;

        }

        public void BeforeTest(TestDetails details)
        {
            var isTestFixture = details.Method == null;

            if (isTestFixture)
            {
                DiscoverFieldsToFake(details);

                return;
            }

            foreach (var fakeField in _fakeFields)
            {
                var fakeObject = _fakeHelper.DynamicallyCreateFakeObject(fakeField.FieldType);

                fakeField.SetValue(details.Fixture, fakeObject);
            }

            foreach (var createField in _createFields)
            {
                var constructorInfo = createField.FieldType.GetConstructors().First();
                var argumentsTypes = constructorInfo.GetParameters().Select(info => info.ParameterType);

                var objects = argumentsTypes.Select(argumentsType => _fakeHelper.DynamicallyCreateFakeObject(argumentsType));

                var instance = Activator.CreateInstance(createField.FieldType, objects.ToArray());
                createField.SetValue(details.Fixture, instance);
            }
        }

        public void AfterTest(TestDetails details)
        {
        }

        private void DiscoverFieldsToFake(TestDetails details)
        {
            var allfields = details.Fixture.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

            _fakeFields = allfields.Where(testField => testField.CustomAttributes.Any(data => data.AttributeType == typeof(FakeItAttribute)));
            _createFields = allfields.Where(testField => testField.CustomAttributes.Any(data => data.AttributeType == typeof(CreateItAttribute)));
        }

        public ActionTargets Targets
        {
            get { return ActionTargets.Test | ActionTargets.Suite; }
        }
    }
}