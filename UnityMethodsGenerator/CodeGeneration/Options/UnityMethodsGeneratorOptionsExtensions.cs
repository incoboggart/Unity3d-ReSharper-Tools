using System;
using System.Collections.Generic;

using JetBrains.ReSharper.Feature.Services.CSharp.Generate;
using JetBrains.ReSharper.Feature.Services.Generate;

namespace ReSharperPlugins.UnityMethodsGenerator.CodeGeneration.Options
{
    public static class UnityMethodsGeneratorOptionsExtensions
    {
        public static void AccessModifierGlobalOption(this CSharpGeneratorContext context)
        {
            context.GlobalOptions.Add(new GeneratorOptionSelector(typeof (AccessModifier).FullName, "Access modifier",
                AccessModifier.@private.ToString(), new[]
                {
                    AccessModifier.@public.ToString(),
                    AccessModifier.@internal.ToString(),
                    AccessModifier.@protected.ToString(),
                    AccessModifier.@private.ToString()
                }));
        }

        public static AccessModifier GetAccessModifierGlobalOption(this CSharpGeneratorContext context)
        {
            AccessModifier value;
            return AccessModifier.TryParse(context.GetGlobalOptionValue(typeof (AccessModifier).FullName), out value)
                ? value
                : AccessModifier.@private;
        }

        private const String AddDefaultCommentsGlobalOptionKey = "addDefaultReferenceComments";

        public static void AddDefaultCommentsGlobalOption(this CSharpGeneratorContext context)
        {
            context.GlobalOptions.Add(new GeneratorOptionBoolean(AddDefaultCommentsGlobalOptionKey,
                "Add default comments", false));
        }

        public static Boolean GetAddDefaultCommentsGlobalOption(this CSharpGeneratorContext context)
        {
            Boolean value;
            Boolean.TryParse(context.GetGlobalOptionValue(AddDefaultCommentsGlobalOptionKey), out value);
            return value;
        }

        public static void DeclarationModifierGlobalOption(this CSharpGeneratorContext context)
        {
            var cls = context.ClassDeclaration;
            var list = new List<String>(4)
            {
                DeclarationModifier.none.ToString(),
                DeclarationModifier.@sealed.ToString()
            };

            if (cls.IsAbstract)
            {
                list.Add(DeclarationModifier.@abstract.ToString());
            }

            if (!cls.IsSealed)
            {
                list.Add(DeclarationModifier.@virtual.ToString());
            }

            context.GlobalOptions.Add(new GeneratorOptionSelector(typeof (DeclarationModifier).FullName,
                "Declaration modifier", list[0], list.ToArray()));
        }

        public static DeclarationModifier GetDeclarationModifierGlobalOption(this CSharpGeneratorContext context)
        {
            DeclarationModifier value;
            return DeclarationModifier.TryParse(context.GetGlobalOptionValue(typeof (DeclarationModifier).FullName),
                out value)
                ? value
                : DeclarationModifier.none;
        }
    }
}
