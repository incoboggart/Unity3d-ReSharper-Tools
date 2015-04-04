using System.Drawing;
using JetBrains.ActionManagement;
using JetBrains.ReSharper.Feature.Services.Generate.Actions;
using JetBrains.UI.RichText;

namespace ReSharperPlugins.UnityMethodsGenerator
{
    [ActionHandler("ReSharperPlugins.UnityMethodsGenerator.Generator")]
    public class UnityMethodsGeneratorAction : GenerateActionBase<UnityMethodsGeneratorItemProvider>
    {
        protected override bool ShowMenuWithOneItem
        {
            get { return true; }
        }

        protected override RichText Caption
        {
            get { return "Generate Unity methods"; }
        }
    }
}
