using JetBrains.Application.DataContext;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Feature.Services.Generate;
using JetBrains.ReSharper.Feature.Services.Generate.Actions;
using JetBrains.ReSharper.Psi;
using JetBrains.UI.Icons;
using DataConstants = JetBrains.ProjectModel.DataContext.DataConstants;

namespace ReSharperPlugins.UnityMethodsGenerator
{
    public class UnityMethodsGeneratorActionWorkflow : StandardGenerateActionWorkflow
    {
        public UnityMethodsGeneratorActionWorkflow(IconId icon) :
            base("UnityMethodsGenerator", icon, "Unity methods", GenerateActionGroup.CLR_LANGUAGE,
                "Unity methods", "This methods can be included in your script", "ReSharperPlugins.UnityMethodsGenerator.Generator")
        {
        }

        public override double Order
        {
            get { return 500; }
        }

        /// <summary>
        /// This method is redefined in order to get rid of the IsKindAllowed() check at the end.
        /// </summary>
        public override bool IsAvailable(IDataContext dataContext)
        {
            ISolution solution = dataContext.GetData(DataConstants.SOLUTION);
            if (solution == null)
                return false;

            GeneratorManager generatorManager = GeneratorManager.GetInstance(solution);
            if (generatorManager == null)
                return false;

            PsiLanguageType languageType = generatorManager.GetPsiLanguageFromContext(dataContext);
            if (languageType == null)
                return false;

            var generatorContextFactory = LanguageManager.Instance.TryGetService<IGeneratorContextFactory>(languageType);
            return generatorContextFactory != null;
        }
    }
}