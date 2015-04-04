using System;

using JetBrains.ReSharper.Psi;

namespace ReSharperPlugins.UnityMethodsGenerator.CodeGeneration.MethodProviding
{
    public interface IParameterDescription
    {
        String Name { get; }
        String GetTypeName();
        Boolean Match(IParameter parameter);
    }
}