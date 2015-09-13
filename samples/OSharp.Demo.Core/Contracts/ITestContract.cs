using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OSharp.Core.Dependency;


namespace OSharp.Demo.Contracts
{
    public interface ITestContract : ILifetimeScopeDependency
    {
        void Test();
    }
}
