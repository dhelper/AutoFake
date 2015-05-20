using System;

namespace AutoFakes.FakeItEasy.Internal
{
    public interface IFakeHelper
    {
        object DynamicallyCreateFakeObject(Type type);
    }
}