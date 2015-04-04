using System;
using System.Linq;
using JetBrains.ReSharper.Psi;

namespace ReSharperPlugins.UnityMethodsGenerator.CodeGeneration.MethodProviding.Implementations
{
    public sealed class TextParameterDescription : IParameterDescription
    {
        private readonly String _name;
        private readonly String[] _types;

        public TextParameterDescription(String name, params String[] types)
        {
            _name = name;
            _types = types;
        }

        public TextParameterDescription(String name, String type)
        {
            _name = name;
            _types = new[] {type};
        }

        public String Name
        {
            get { return _name; }
        }

        public string GetTypeName()
        {
            return _types[0];
        }

        public Boolean Match(IParameter parameter)
        {
            var presentableName = parameter.Type.GetPresentableName(parameter.PresentationLanguage);
            return _types.Any(type => type.Equals(presentableName));
        }
    }
}
