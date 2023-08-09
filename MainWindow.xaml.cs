using System.Collections.Generic;
using System.IO;
using System.Windows;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Text;
using Microsoft.Build.Locator;
using Microsoft.Build.Logging;
using Microsoft.Build.Execution;
using System.Linq;
using Microsoft.Win32;
using System.Diagnostics;
using System;
using Microsoft.Build.Exceptions;
using Microsoft.Build.Framework;

namespace OpenStudioIDE
{
    public partial class MainWindow : Window
    {
        private string selectedSolutionFilePath; // Store the selected .sln file path here
        private string recentProjectsFilePath = "recient.txt";

        public MainWindow()
        {
            InitializeComponent();
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
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "seesharp files (*.cs)|*.cs| window files (*.xaml)|*.xaml| snake files (*.py)|*.py "; // Set the filter
            saveFileDialog.DefaultExt = ".cs";
            if (saveFileDialog.ShowDialog() == true)
            {
                string filePath = saveFileDialog.FileName;
                File.WriteAllText(filePath, txtEditor.Text);
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
        private void LoadRecentProjects()
        {
            if (File.Exists(recentProjectsFilePath))
            {
                // Read recent project paths from the text file
                var recentProjects = File.ReadAllLines(recentProjectsFilePath);

                // Add recent project paths to the ListBox
                
            }
        }

        private void SaveRecentProject(string projectPath)
        {
            // Save the recent project path to the text file
            File.AppendAllText(recentProjectsFilePath, $"{projectPath}{Environment.NewLine}");

            // Reload the recent projects list
            LoadRecentProjects();
        }

        private void txtEditor_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {

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
