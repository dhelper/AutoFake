using AutoFakes.FakeItEasy.Internal;

namespace AutoFakes.FakeItEasy
{
    public class AutoFakeItEasyAttribute : AutoFakeAttributeBase
    {
        public AutoFakeItEasyAttribute() : base(new FakeItEasyHelper())
        {
        }
    }
}