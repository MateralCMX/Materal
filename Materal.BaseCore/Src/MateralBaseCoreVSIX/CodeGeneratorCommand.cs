using EnvDTE;
using EnvDTE80;
using MateralBaseCoreVSIX.Models;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.Threading;
using System;
using System.ComponentModel.Design;
using Task = System.Threading.Tasks.Task;

namespace MateralBaseCoreVSIX
{
    /// <summary>
    /// Command handler
    /// </summary>
    internal sealed class CodeGeneratorCommand
    {
        /// <summary>
        /// Command ID.
        /// </summary>
        public const int CommandId = 0x0100;

        /// <summary>
        /// Command menu group (command set GUID).
        /// </summary>
        public static readonly Guid CommandSet = new Guid("a306f0ad-2434-466e-b531-4c63bf3b1f84");

        /// <summary>
        /// VS Package that provides this command, not null.
        /// </summary>
        private readonly AsyncPackage package;

        /// <summary>
        /// Initializes a new instance of the <see cref="CodeGeneratorCommand"/> class.
        /// Adds our command handlers for menu (commands must exist in the command table file)
        /// </summary>
        /// <param name="package">Owner package, not null.</param>
        /// <param name="commandService">Command service to add command to, not null.</param>
        private CodeGeneratorCommand(AsyncPackage package, OleMenuCommandService commandService)
        {
            this.package = package ?? throw new ArgumentNullException(nameof(package));
            commandService = commandService ?? throw new ArgumentNullException(nameof(commandService));

            var menuCommandID = new CommandID(CommandSet, CommandId);
            var menuItem = new MenuCommand(this.Execute, menuCommandID);
            commandService.AddCommand(menuItem);
        }

        /// <summary>
        /// Gets the instance of the command.
        /// </summary>
        public static CodeGeneratorCommand Instance
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the service provider from the owner package.
        /// </summary>
        private Microsoft.VisualStudio.Shell.IAsyncServiceProvider ServiceProvider
        {
            get
            {
                return this.package;
            }
        }

        /// <summary>
        /// Initializes the singleton instance of the command.
        /// </summary>
        /// <param name="package">Owner package, not null.</param>
        public static async Task InitializeAsync(AsyncPackage package)
        {
            // Switch to the main thread - the call to AddCommand in CodeGeneratorCommand's constructor requires
            // the UI thread.
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync(package.DisposalToken);

            OleMenuCommandService commandService = await package.GetServiceAsync(typeof(IMenuCommandService)) as OleMenuCommandService;
            Instance = new CodeGeneratorCommand(package, commandService);
        }

        /// <summary>
        /// This function is the callback used to execute the command when the menu item is clicked.
        /// See the constructor to see how the menu item is associated with this function using
        /// OleMenuCommandService service and MenuCommand class.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">Event args.</param>
        private void Execute(object sender, EventArgs e)
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            string message = "代码生成成功";
            try
            {
                object temp = ServiceProvider.GetServiceAsync(typeof(DTE)).Result;
                if(temp is DTE dte && dte.ActiveSolutionProjects != null && dte.ActiveSolutionProjects is object[] activeObjects)
                {
                    bool isGenerator = false;
                    foreach (object activeItem in activeObjects)
                    {
                        if (activeItem is Project project && project.Name.EndsWith(".Domain"))
                        {
                            DTE2 dte2 = Package.GetGlobalService(typeof(DTE)) as DTE2;
                            var solution = new SolutionModel(dte2.Solution, project);
                            solution.CreateCodeFiles();
                            isGenerator = true;
                        }
                    }
                    if (!isGenerator)
                    {
                        throw new VSIXException("激活项目不是Domain");
                    }
                }
                else
                {
                    throw new VSIXException("没有激活的项目");
                }
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }
            VsShellUtilities.ShowMessageBox(
                package,
                message,
                "Materal.VsHelper",
                OLEMSGICON.OLEMSGICON_INFO,
                OLEMSGBUTTON.OLEMSGBUTTON_OK,
                OLEMSGDEFBUTTON.OLEMSGDEFBUTTON_FIRST);
        }
    }
}
