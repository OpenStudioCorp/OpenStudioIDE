using System;
using System.IO;
using System.Windows;

namespace OpenStudioIDE
{
    public partial class Window2 : Window
    {
        public Window2()//shell
        {
            InitializeComponent();
            Welcome();
        }

        private void Welcome()
        {
            PrintToOutput("Welcome to the PythonicOS shell designed for the OpenStudioIDE!");
            PrintToOutput("To get started just type 'help' and then press enter!");
        }

        private void InputTextBox_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                ProcessInput(InputTextBox.Text);
                InputTextBox.Clear();
            }
        }

        private void ProcessInput(string input)
        {
            PrintToOutput($"> {input}");

            var command = input.Split(' ')[0].ToLower();

            switch (command)
            {
                case "cd":
                    ChangeDirectory(input);
                    break;

                case "help":
                    Help();
                    break;

                case "ls":
                    ListDirectory(input);
                    break;

                case "exit":
                    Close();
                    break;

                default:
                    PrintToOutput("Invalid command");
                    break;
            }
        }

        private void PrintToOutput(string text)
        {
            OutputTextBox.AppendText(text + Environment.NewLine);
            OutputTextBox.ScrollToEnd();
        }

        private void Help()
        {
            PrintToOutput("cd: changes directory");
            PrintToOutput("ls: list files in directory");
            PrintToOutput("run: runs 'dotnet run' on your computer in the local folder that the shell is in");
        }

        private void ChangeDirectory(string input)
        {
            var arguments = input.Split(' ')[1];

            try
            {
                Directory.SetCurrentDirectory(arguments);
            }
            catch (Exception e)
            {
                PrintToOutput($"Error changing directory: {e.Message}");
            }
        }

        private void ListDirectory(string input)
        {
            var arguments = input.Split(' ').Length > 1 ? input.Split(' ')[1] : string.Empty;
            var directory = string.IsNullOrEmpty(arguments) ? Directory.GetCurrentDirectory() : arguments;

            try
            {
                var files = Directory.GetFiles(directory);
                var directories = Directory.GetDirectories(directory);

                PrintToOutput("Files:");
                foreach (var file in files)
                {
                    PrintToOutput(file);
                }

                PrintToOutput("Directories:");
                foreach (var dir in directories)
                {
                    PrintToOutput(dir);
                }
            }
            catch (Exception e)
            {
                PrintToOutput($"Error listing directory: {e.Message}");
            }
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("woops? seems like this aint implemented yet, just goahead and close the window by pressing the x on the right of the window at the top! cheers!");
        }
    }
}
