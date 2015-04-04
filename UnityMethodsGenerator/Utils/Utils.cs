using System;
using System.Collections.Generic;

using JetBrains.ReSharper.Psi;

using ReSharperPlugins.UnityMethodsGenerator.CodeGeneration.MethodProviding;

namespace ReSharperPlugins.UnityMethodsGenerator.Utils
{
    internal static class Utils
    {
        public static IList<IMethodProvider> GetMethodProviders(this IClass cls)
        {
            if (cls == null)
            {
                return null;
            }

            var monoBehaviour = GetMonoBehaviourSuperType(cls);

            if (monoBehaviour != null)
            {
                return MethodProviders.MonoBehaviour;
            }

            var editorWindow = GetEditorWindowSuperType(cls);

            if (editorWindow != null)
            {
                return MethodProviders.EditorWindow;
            }

            return null;
        }

        private static IDeclaredType GetMonoBehaviourSuperType(ITypeElement typeElement)
        {
            const String monoBehaviourFullName = "UnityEngine.MonoBehaviour";
            return GetSuperType(typeElement, monoBehaviourFullName);
        }

        private static IDeclaredType GetEditorWindowSuperType(ITypeElement typeElement)
        {
            const String editorWindow = "UnityEditor.EditorWindow";
            return GetSuperType(typeElement, editorWindow);
        }

        private static IDeclaredType GetSuperType(ITypeElement typeElement, String clrName)
        {
            var superTypes = typeElement.GetSuperTypes();

            for (Int32 i = 0, c = superTypes.Count; i < c; i++)
            {
                var superType = superTypes[i];
                var superTypeClrName = superType.GetClrName();

                if (superTypeClrName.FullName.Equals(clrName))
                {
                    return superType;
                }
            }

            return null;
        }
    }
}
