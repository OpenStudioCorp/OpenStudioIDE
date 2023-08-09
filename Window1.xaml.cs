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
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        private string recentProjectsFilePath = "recient.txt";
        public bool LoadedProject { get; private set; }
        public Window1()
        {
            InitializeComponent();
        }
        private void consoleapp_Click(object sender, RoutedEventArgs e)
        {

        }

        private void WPFapp_Click(object sender, RoutedEventArgs e)
        {

        }
        private void Python_Click(object sender, RoutedEventArgs e)
        {

        }
        private void load_Click(object sender, RoutedEventArgs e)
        {
            
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            

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
