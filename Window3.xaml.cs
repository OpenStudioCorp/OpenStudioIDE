using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace OpenStudioIDE
{
    /// <summary>
    /// Interaction logic for Window3.xaml
    /// </summary>
    
    public partial class Window3 : Window
    {
        // project creation page
        public string folderpath { get; set; }
        public string Projectname { get; set; }
        public string Projecttype { get; set; }
        public string Projectlocation { get; set; }
        public string Projectlanguage { get; set; }
        public string Projectframework { get; set; }
        public string Projecttemplate { get; set; }
        public string Projecttargetframework { get; set; }
        public string Projectoutputtype { get; set; }
        public string Projectstartupobject { get; set; }
        public string Projectdefaultnamespace { get; set; }
        public string Projectrootnamespace { get; set; }
        public string Projectassemblyname { get; set; }
        public string Projectguid { get; set; }
        public string Projectauthor { get; set; }
        public string Projectdescription { get; set; }
        public string Projectversion { get; set; }
        public string Projectpackage { get; set; }
        public string Projectpackageversion { get; set; }
        public string Projectpackageauthor { get; set; }
        public string Projectpackageowners { get; set; }
        public string Projectpackageprojecturl { get; set; }
        public string Projectpackagelicenseurl { get; set; }
        
        public Window3(string folderpath)
        {
            InitializeComponent();
            this.folderpath = folderpath;
            this.Projectlocation = folderpath;
            this.Projectname = projectname.Text;
            this.Projecttype = projecttype.Text;
      


        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // project t
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ComboBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            // project language selection box
            for (int i = 0; i < projectlanguage.Items.Count; i++)
            {
                if (projectlanguage.SelectedIndex == i)
                {
                    Projectlanguage = projectlanguage.Items[i].ToString();
                }
            }
        }
    }
}
