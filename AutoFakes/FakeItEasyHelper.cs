using System;
using AutoFakes.FakeItEasy.Internal;
using FakeItEasy;

namespace AutoFakes.FakeItEasy
{
    internal class FakeItEasyHelper : IFakeHelper
    {
        public object DynamicallyCreateFakeObject(Type type)
        {
            var createFakeMethod = typeof(A).GetMethod("Fake", new Type[0]);
            var createFakeGeneric = createFakeMethod.MakeGenericMethod(type);
            var fakeObject = createFakeGeneric.Invoke(null, null);

            return fakeObject;
        }
    }
}