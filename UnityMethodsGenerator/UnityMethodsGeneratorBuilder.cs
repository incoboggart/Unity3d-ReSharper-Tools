using System;
using System.Linq;

using JetBrains.ReSharper.Feature.Services.CSharp.Generate;
using JetBrains.ReSharper.Feature.Services.Generate;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp;

using ReSharperPlugins.UnityMethodsGenerator.CodeGeneration.Options;
using ReSharperPlugins.UnityMethodsGenerator.Utils;

namespace ReSharperPlugins.UnityMethodsGenerator
{
    [GeneratorBuilder("UnityMethodsGenerator", typeof(CSharpLanguage))]
    public class UnityMethodsGeneratorBuilder : GeneratorBuilderBase<CSharpGeneratorContext>
    {
        public override double Priority
        {
            get { return 0; }
        }

        protected override void Process(CSharpGeneratorContext context)
        {
            // this is where you build new code
            // to modify, e.g., an existing class, use context.ClassDeclaration
            var typeDeclaration = context.ClassDeclaration;

            if (typeDeclaration.IsStatic)
            {
                return;
            }

            var typeElement = typeDeclaration.DeclaredElement;

            if (!(typeElement is IClass))
            {
                return;
            }

            var cls = typeElement as IClass;
            var methodProviders = cls.GetMethodProviders();
            
            var elementFactory = CSharpElementFactory.GetInstance(context.Root.GetPsiModule());
            var declaredElements = context.InputElements.OfType<GeneratorDeclaredElement<IMethod>>();

            var accessModifier = context.GetAccessModifierGlobalOption();

            var methods = declaredElements.Select(e => e.DeclaredElement).ToList();

            foreach (var methodProvider in methodProviders)
            {
                for (Int32 i = 0; i < methods.Count; i++)
                {
                    var method = methods[i];

                    if (!methodProvider.RecognizeMethod(method))
                    {
                        continue;
                    }

                    var declaration = methodProvider.GetFullDeclaration(elementFactory, accessModifier);
                    declaration.SetBody(elementFactory.CreateBlock("{ }"));
                    context.PutMemberDeclaration(declaration);
                    methods.RemoveAt(i);
                    break;
                }
            }
        }
    }
}