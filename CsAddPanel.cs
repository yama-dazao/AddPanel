using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System.Reflection;
using System.Windows.Media.Imaging;

namespace Walkthrough
{
    /// <remarks>
    /// This application's main class. The class must be Public.
    /// </remarks>
    public class CsAddPanel : IExternalApplication
    {
        // Both OnStartup and OnShutdown must be implemented as public method
        public Result OnStartup(UIControlledApplication application)
        {
            // Add a new ribbon panel
            RibbonPanel ribbonPanel = application.CreateRibbonPanel("NewRibbonPanel");

            // Create a push button to trigger a command add it to the ribbon panel.
            string thisAssemblyPath = Assembly.GetExecutingAssembly().Location;
            PushButtonData buttonData = new PushButtonData("cmdHelloWorld",
               "Hello World", thisAssemblyPath, "Walkthrough.HelloWorld");

            Autodesk.Revit.UI.PushButton? pushButton = ribbonPanel.AddItem(buttonData) as Autodesk.Revit.UI.PushButton;

            if (pushButton == null)
            {
                throw new InvalidOperationException("Failed to create PushButton.");
            }

            // ToolTipの設定
            pushButton.ToolTip = "Say hello to the entire world.";


            // b) large bitmap
            Uri uriImage = new Uri(@"C:\Users\kentaro.yamasaki\source\repos\AddPanel\Add.webp");
            BitmapImage largeImage = new BitmapImage(uriImage);
            pushButton.LargeImage = largeImage;

            return Result.Succeeded;
        }

        public Result OnShutdown(UIControlledApplication application)
        {
            // nothing to clean up in this simple case
            return Result.Succeeded;
        }
    }
    /// <remarks>
    /// The "HelloWorld" external command. The class must be Public.
    /// </remarks>
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    public class HelloWorld : IExternalCommand
    {
        // The main Execute method (inherited from IExternalCommand) must be public
        public Autodesk.Revit.UI.Result Execute(ExternalCommandData revit,
            ref string message, ElementSet elements)
        {
            Autodesk.Revit.UI.TaskDialog.Show("Revit", "Hello World");
            return Autodesk.Revit.UI.Result.Succeeded;
        }
    }
}