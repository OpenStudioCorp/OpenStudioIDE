using System.Windows;

namespace OpenStudioIDE
{
    public partial class App : Application
    {

        private string folderPath;

        public bool LoadedProject { get; private set; }
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Show the welcome window
            Window1 welcomeWindow = new Window1();
            welcomeWindow.ShowDialog();

            // Check if the user chose to load or create a project
            if (welcomeWindow.LoadedProject)
            {
                // Create and show the main window
                MainWindow mainWindow = new MainWindow(folderPath);
                mainWindow.Show();
            }
            else
            {
                // Exit the application if the user didn't choose to load a project
                Shutdown();
            }
        }
    }
}
