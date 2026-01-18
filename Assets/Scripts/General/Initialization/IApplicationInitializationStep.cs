using Cysharp.Threading.Tasks;

namespace General.Initialization
{
    public interface IApplicationInitializationStep
    {
        public string Name { get; }
        public float Progress { get; }
        UniTask InitializeAsync();
    }
}