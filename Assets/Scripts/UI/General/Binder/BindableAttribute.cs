using System;
using UnityEngine.Scripting;

namespace UI.General.Binder
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Method)]
    public class BindableAttribute : PreserveAttribute
    {
    }
}