using OSharp.Core.Dependency;


namespace OSharp.Demo.Contracts
{
    public interface ITestContract : IScopeDependency
    {
        void Test();
    }
}
