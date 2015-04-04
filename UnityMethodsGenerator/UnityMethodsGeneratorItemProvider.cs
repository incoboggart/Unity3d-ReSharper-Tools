using System.Collections.Generic;
using JetBrains.Application.DataContext;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Feature.Services.Generate.Actions;
using JetBrains.ReSharper.Psi;
using DataConstants = JetBrains.ProjectModel.DataContext.DataConstants;

namespace ReSharperPlugins.UnityMethodsGenerator
{
    [GenerateProvider]
    public class UnityMethodsGeneratorItemProvider : IGenerateActionProvider
    {
        public IEnumerable<IGenerateActionWorkflow> CreateWorkflow(IDataContext dataContext)
        {
            ISolution solution = dataContext.GetData(DataConstants.SOLUTION);
            var iconManager = solution.GetComponent<PsiIconManager>();
            var icon = iconManager.GetImage(CLRDeclaredElementType.METHOD);
            yield return new UnityMethodsGeneratorActionWorkflow(icon);
        }
    }
}