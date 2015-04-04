using System;

using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using ReSharperPlugins.UnityMethodsGenerator.CodeGeneration.Options;

namespace ReSharperPlugins.UnityMethodsGenerator.CodeGeneration.MethodProviding
{
    internal interface IMethodProvider
    {
        void Initialize();
        IMethodDeclaration GetShortDeclaration(CSharpElementFactory factory);
        IMethodDeclaration GetFullDeclaration(CSharpElementFactory factory, AccessModifier accessModifier);
        Boolean RecognizeMethod(IMethod method);
    }
}