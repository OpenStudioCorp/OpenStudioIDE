using System.Collections.Generic;
using System.IO;
using System.Windows;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Win32;

namespace OpenStudioIDE
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Open_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                string filePath = openFileDialog.FileName;
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
    }
}
