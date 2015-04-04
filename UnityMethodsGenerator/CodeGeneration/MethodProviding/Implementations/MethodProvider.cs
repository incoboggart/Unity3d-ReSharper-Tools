using System;
using System.Collections.Generic;
using System.Text;

using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp;
using JetBrains.ReSharper.Psi.CSharp.Tree;

using ReSharperPlugins.UnityMethodsGenerator.CodeGeneration.Options;

namespace ReSharperPlugins.UnityMethodsGenerator.CodeGeneration.MethodProviding.Implementations
{
    internal sealed class MethodProvider : IMethodProvider
    {
        public String DefaultComment;

        private readonly String _methodName;
        private readonly IList<IParameterDescription> _parameterDescriptions;
        private readonly String _methodDeclarationFormat;

        public MethodProvider(String methodName, params IParameterDescription[] parameters)
        {
            _methodName = methodName;
            _parameterDescriptions = parameters;

            var isFirst = true;
            var sb = new StringBuilder();
            sb.AppendFormat("void {0}(", _methodName);
            foreach (var parameterDescription in _parameterDescriptions)
            {
                if (isFirst)
                {
                    isFirst = false;
                }
                else
                {
                    sb.Append(",");
                }

                sb.AppendFormat("{0} {1}", parameterDescription.GetTypeName(), parameterDescription.Name);
            }
            _methodDeclarationFormat = sb.Append(")").ToString();
        }

        public void Initialize() {}

        public IMethodDeclaration GetShortDeclaration(CSharpElementFactory factory)
        {
            return (IMethodDeclaration) factory.CreateTypeMemberDeclaration(_methodDeclarationFormat);
        }

        public IMethodDeclaration GetFullDeclaration(CSharpElementFactory factory, AccessModifier accessModifier)
        {
            var isFirst = true;
            var sb = new StringBuilder();
            sb.AppendFormat("{1} void {0}(", _methodName, accessModifier);
            foreach (var parameterDescription in _parameterDescriptions)
            {
                if (isFirst)
                {
                    isFirst = false;
                }
                else
                {
                    sb.Append(",");
                }

                sb.AppendFormat("{0} {1}", parameterDescription.GetTypeName(), parameterDescription.Name);
            }
            return (IMethodDeclaration) factory.CreateTypeMemberDeclaration(sb.Append(")").ToString());
        }

        public Boolean RecognizeMethod(IMethod method)
        {
            if (method.ShortName.Equals(_methodName))
            {
                var parameters = method.Parameters;

                if (parameters.Count == _parameterDescriptions.Count)
                {
                    for (Int32 i = 0, c = parameters.Count; i < c; i++)
                    {
                        var parameter = parameters[i];

                        if (!_parameterDescriptions[i].Match(parameter))
                        {
                            return false;
                        }
                    }

                    return true;
                }
            }

            return false;
        }
    }
}
