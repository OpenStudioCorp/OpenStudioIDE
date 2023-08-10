using System;
using System.Diagnostics;
using System.IO;
using System.Windows;

namespace OpenStudioIDE
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        private string recentProjectsFilePath = "recient.txt";
        private string folderPath;

        public bool LoadedProject { get; private set; }
        public Window1()
        {
            InitializeComponent();
        }
        private void consoleapp_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new System.Windows.Forms.FolderBrowserDialog();

            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string selectedFolderPath = dialog.SelectedPath;

                if (!string.IsNullOrEmpty(selectedFolderPath))
                {
                    try
                    {
                        Process.Start("dotnet new console --output", selectedFolderPath);
                    }
                    catch (Exception ex)
                    {
                        // Handle exceptions if the process fails to start
                        Console.WriteLine($"Error: {ex.Message}");
                    }
                }
                else
                {
                    // Handle the case where no folder was selected
                }
            }
        }

        private void WPFapp_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new System.Windows.Forms.FolderBrowserDialog();

            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string selectedFolderPath = dialog.SelectedPath;

                if (!string.IsNullOrEmpty(selectedFolderPath))
                {
                    try
                    {
                        Process.Start("dotnet new wpf --output", selectedFolderPath);
                        // After creating the project folder, open the Main Window
                        MainWindow mainWindow = new MainWindow(selectedFolderPath);
                        mainWindow.Show();

                        // Close the Welcome Window
                        this.Close();
                    }
                    catch (Exception ex)
                    {
                        // Handle exceptions if the process fails to start
                        Console.WriteLine($"Error: {ex.Message}");
                    }
                }
                else
                {
                    // Handle the case where no folder was selected
                }
            }
        }
        private void Python_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.MessageBox.Show("this feature is not in the code yet, you can create a python project just by making a folder with a python script and opening the folder in the main ide");
        }
        private void load_Click(object sender, RoutedEventArgs e)
        {

            MainWindow mainWindow = new MainWindow(folderPath);
            mainWindow.Show();



        }
        private void OpenFolderAndExecuteCommand()
        {
            var dialog = new System.Windows.Forms.FolderBrowserDialog();

            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string selectedFolderPath = dialog.SelectedPath;

                if (!string.IsNullOrEmpty(selectedFolderPath))
                {
                    try
                    {
                        Process.Start("dotnet new console", selectedFolderPath);
                    }
                    catch (Exception ex)
                    {
                        // Handle exceptions if the process fails to start
                        Console.WriteLine($"Error: {ex.Message}");
                    }
                }
                else
                {
                    // Handle the case where no folder was selected
                }
            }
        }




        //private void Build_Click(object sender, RoutedEventArgs e)
        //{

        //}
        //private void Build_Click(object sender, RoutedEventArgs e)
        //{

        //}

        private void LoadRecentProjects()
        {
            if (File.Exists(recentProjectsFilePath))
            {
                // Read recent project paths from the text file
                var recentProjects = File.ReadAllLines(recentProjectsFilePath);

                // Clear the ListBox and add recent project paths
                lstRecentProjects.Items.Clear();
                lstRecentProjects.Items.Add(recentProjects);
            }
        }



    }
}
