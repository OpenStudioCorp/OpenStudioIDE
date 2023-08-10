using Microsoft.Build.Exceptions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Text;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace OpenStudioIDE
{
    public partial class MainWindow : Window
    {
        private string selectedSolutionFilePath; // Store the selected .sln file path here
        private string selectedFilePath;
        private string recentProjectsFilePath = "recient.txt";
        public bool LoadedProject { get; private set; }
        private string selectedFolderPath;
        public MainWindow(string folderPath)
        {
            InitializeComponent();
            // Show the welcome window
            selectedFolderPath = folderPath;
            DisplayFolderContents(selectedFolderPath);

            LoadRecentProjects();

        }

        private void Open_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Solution files (*.sln)|*.sln"; // Only show .sln files in the file dialog

            if (openFileDialog.ShowDialog() == true)
            {
                string filePath = openFileDialog.FileName;
                string extension = Path.GetExtension(filePath);

                if (extension.Equals(".sln", StringComparison.OrdinalIgnoreCase))
                {
                    // If the selected file is a .sln file, store its path for later use
                    selectedSolutionFilePath = filePath;
                }
                else
                {
                    // Handle the case when a non-.sln file is selected (optional)
                    MessageBox.Show("Please select a .sln file.", "Invalid File", MessageBoxButton.OK, MessageBoxImage.Warning);
                }

                // Load the file content into the editor (if needed)
                string fileContent = File.ReadAllText(filePath);
                txtEditor.Text = fileContent;
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(selectedFilePath))
            {
                File.WriteAllText(selectedFilePath, txtEditor.Text);
            }
            else
            {
                // Handle the case when no file is selected (optional)
                MessageBox.Show("Please select a file before saving.", "No File Selected", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }


        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        public class CodeChecker
        {
            public static IEnumerable<Diagnostic> CheckCode(string code)
            {
                var syntaxTree = SyntaxFactory.ParseSyntaxTree(SourceText.From(code));
                var compilation = CSharpCompilation.Create("MyCompilation")
                    .AddReferences(MetadataReference.CreateFromFile(typeof(object).Assembly.Location))
                    .AddSyntaxTrees(syntaxTree);

                var diagnostics = compilation.GetDiagnostics();
                return diagnostics.Where(d => d.Severity == DiagnosticSeverity.Error);
            }
        }
        private void txtEditor_TextChanged(object sender, RoutedEventArgs e)
        {

        }
        private void CheckCode_Click(object sender, RoutedEventArgs e)
        {
            string code = txtEditor.Text;
            var diagnostics = CodeChecker.CheckCode(code);

            if (diagnostics.Any())
            {
                // Display the errors to the user
                foreach (var diagnostic in diagnostics)
                {
                    MessageBox.Show($"error? {diagnostic}");
                }
            }
            else
            {
                // Code is error-free, you can provide some feedback to the user.
            }
        }
        private void lstFolderContents_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            FileSystemItem selectedItem = lstFolderContents.SelectedItem as FileSystemItem;
            if (selectedItem != null && selectedItem.IsFolder)
            {
                // Navigate into the selected folder
                DisplayFolderContents(selectedItem.Path);
            }
        }


        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {

        }
        private void Build_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(selectedSolutionFilePath))
            {
                // Call the BuildSolution method with the stored solution file path
                BuildSolution(selectedSolutionFilePath);
            }
            else
            {
                // Handle the case when no .sln file is selected (optional)
                MessageBox.Show("Please select a .sln file before building.", "No .sln File Selected", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        private void Save_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Save_Click(sender, e);
        }
        private void Window_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.S && (Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
            {
                Save_Click(sender, e);
                e.Handled = true; // Mark the event as handled to prevent further processing
            }
        }



        private void LoadRecentProjects()
        {
            if (File.Exists(recentProjectsFilePath))
            {
                // Read recent project paths from the text file
                var recentProjects = File.ReadAllLines(recentProjectsFilePath);

                // Clear the ListBox and add recent project paths

            }
        }

        private void OpenFolder_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog();

            if (dialog.ShowDialog() == true)
            {
                // Get the selected folder path
                string folderPath = Path.GetDirectoryName(dialog.FileName);

                // Display folder contents in the ListBox
                DisplayFolderContents(folderPath);
            }
        }

        private void DisplayFolderContents(string folderPath)
        {
            lstFolderContents.Items.Clear();

            if (!string.IsNullOrEmpty(folderPath))
            {
                // Add the ".." item to navigate back to the parent folder
                string parentFolderPath = Directory.GetParent(folderPath)?.FullName;
                lstFolderContents.Items.Add(new FileSystemItem("..", parentFolderPath, true));

                // Get the files and folders in the selected folder
                string[] filePaths = Directory.GetFiles(folderPath);
                string[] folderPaths = Directory.GetDirectories(folderPath);

                // Add folders to the ListBox
                foreach (string folder in folderPaths)
                {
                    string folderName = System.IO.Path.GetFileName(folder);
                    lstFolderContents.Items.Add(new FileSystemItem(folderName, folder, true));
                }

                // Add files to the ListBox
                foreach (string file in filePaths)
                {
                    string fileName = System.IO.Path.GetFileName(file);
                    lstFolderContents.Items.Add(new FileSystemItem(fileName, file, false));
                }
            }
        }



        private void SaveRecentProject(string projectPath)
        {
            // Save all recent project paths to the text file


            // Reload the recent projects list
            LoadRecentProjects();
        }

        private void txtEditor_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {

        }
        private void lstFolderContents_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstFolderContents.SelectedItem != null)
            {
                FileSystemItem selectedItem = lstFolderContents.SelectedItem as FileSystemItem;

                if (selectedItem != null && File.Exists(selectedItem.Path))
                {
                    selectedFilePath = selectedItem.Path; // Store the selected file path
                    string fileContent = File.ReadAllText(selectedItem.Path);
                    txtEditor.Text = fileContent;
                }
            }
        }

        public void BuildSolution(string solutionFilePath)
        {
            try
            {
                Process.Start($"dotnet run {solutionFilePath}");
                MessageBox.Show(solutionFilePath);
            }
            catch (InvalidProjectFileException ex)
            {
                // Handle any exception related to the project file
                Console.WriteLine($"InvalidProjectFileException: {ex.Message}");
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                Console.WriteLine($"Exception: {ex.Message}");
            }
        }

    }
}
