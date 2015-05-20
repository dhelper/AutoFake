using System;

namespace AutoFakes.FakeItEasy
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class FakeItAttribute : Attribute
    {
    }
}