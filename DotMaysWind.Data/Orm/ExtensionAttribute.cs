using System;

namespace System.Runtime.CompilerServices
{
    /// <summary>
    /// 扩展方法特性类
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class | AttributeTargets.Assembly)]
    public sealed class ExtensionAttribute : Attribute { }
}