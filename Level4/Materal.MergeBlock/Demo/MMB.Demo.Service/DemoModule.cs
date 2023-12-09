using MMB.Core.Abstractions;

namespace MMB.Demo.Service
{
    public class DemoModule : MMBModule, IMergeBlockModule
    {
        public DemoModule() : base("Demo") { }
    }
}
