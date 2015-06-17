using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoFakesTests.TestClasses
{
    public class ClassWithDependency
    {
        public IDependency Dependency { get; set; }

        public ClassWithDependency(IDependency dependency)
        {
            Dependency = dependency;
        }
    }
}
