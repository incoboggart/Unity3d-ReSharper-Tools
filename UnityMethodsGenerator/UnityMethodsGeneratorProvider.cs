using System;
using System.Collections.Generic;
using System.Linq;

using JetBrains.ReSharper.Feature.Services.CSharp.Generate;
using JetBrains.ReSharper.Feature.Services.Generate;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp;

using ReSharperPlugins.UnityMethodsGenerator.CodeGeneration.MethodProviding;
using ReSharperPlugins.UnityMethodsGenerator.CodeGeneration.Options;
using ReSharperPlugins.UnityMethodsGenerator.Utils;

namespace ReSharperPlugins.UnityMethodsGenerator
{
    [GeneratorElementProvider("UnityMethodsGenerator", typeof (CSharpLanguage))]
    public class UnityMethodsGeneratorProvider : GeneratorProviderBase<CSharpGeneratorContext>
    {
        public override double Priority
        {
            get { return 0; }
        }

        public override void Populate(CSharpGeneratorContext context)
        {
            // use context.ProvidedElements.AddRange to add new
            // generator elements (e.g., GeneratorDeclaredElement<T>)
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

            if (methodProviders == null)
            {
                return;
            }

            var elementFactory = CSharpElementFactory.GetInstance(context.Root.GetPsiModule());
            var methods = cls.Methods.ToList();

            for (Int32 methodProviderIndex = 0; methodProviderIndex < methodProviders.Count; methodProviderIndex++)
            {
                var methodProvider = methodProviders[methodProviderIndex];
                var recognizedMethodIndex = -1;

                for (Int32 methodIndex = 0; methodIndex < methods.Count; methodIndex++)
                {
                    var method = methods[methodIndex];

                    if (methodProvider.RecognizeMethod(method))
                    {
                        recognizedMethodIndex = methodIndex;
                        break;
                    }
                }

                if (recognizedMethodIndex >= 0)
                {
                    methods.RemoveAt(recognizedMethodIndex);
                    continue;
                }

                var declaration = methodProvider.GetShortDeclaration(elementFactory);
                var declaredElement = declaration.DeclaredElement;

                if (declaredElement != null)
                {
                    context.ProvidedElements.Add(new GeneratorDeclaredElement<IMethod>(declaredElement));
                }
            }

            context.AccessModifierGlobalOption();
            context.AddDefaultCommentsGlobalOption();
        }
    }
}