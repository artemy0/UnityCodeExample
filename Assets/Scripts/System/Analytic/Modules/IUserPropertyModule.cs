using System.Analytic.Core;
using Cysharp.Threading.Tasks;

namespace System.Analytic.Modules
{
    public interface IUserPropertyModule : IUserPropertySetter
    {
        UniTask Initialize();
    }
}