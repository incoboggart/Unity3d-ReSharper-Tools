using System.Windows.Forms;
using JetBrains.ActionManagement;
using JetBrains.Application.DataContext;

namespace ReSharperPlugins.UnityMethodsGenerator
{
    [ActionHandler("UnityMethodsGenerator.About")]
    public class AboutAction : IActionHandler
    {
        public bool Update(IDataContext context, ActionPresentation presentation, DelegateUpdate nextUpdate)
        {
            // return true or false to enable/disable this action
            return false;
        }

        public void Execute(IDataContext context, DelegateExecute nextExecute)
        {
            MessageBox.Show(
              "Unity methods generator\nAcme Corp.\n\n",
              "About Unity methods generator",
              MessageBoxButtons.OK,
              MessageBoxIcon.Information);
        }
    }
}