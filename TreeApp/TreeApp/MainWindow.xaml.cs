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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace TreeApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            List<Family> families = new List<Family>();

            Family family1 = new Family() { Name = "The Doe's" };
            family1.Members.Add(new FamilyMember() { Name = "John Doe"});
            family1.Members.Add(new FamilyMember() { Name = "Jane Doe" });
            family1.Members.Add(new FamilyMember() { Name = "Sammy Doe"});
            families.Add(family1);

            Family family2 = new Family() { Name = "The Moe's" };
            family2.Members.Add(new FamilyMember() { Name = "Mark Moe" });
            family2.Members.Add(new FamilyMember() { Name = "Norma Moe" });
            families.Add(family2);


            //person2.IsExpanded = true;
            //person2.IsSelected = true;

            trvPersons.ItemsSource = families;
        }


        private void trvPersons_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (trvPersons.SelectedItem != null)
            {
                var list = (trvPersons.ItemsSource as List<Family>);
                for (int i = 0; i < list.Count; i++)
                {
                    int curIndex = list[i].Members.IndexOf(trvPersons.SelectedItem as FamilyMember);
                    if(curIndex>=0)
                    {
                        Page1 page = new Page1();
                        MessageBox.Show(list[i].Members[curIndex].Name);
                       
                        frameWin.Content = page.Page1ForWin;
                        return;
                    }

                }
            }
        }
    }

    public class Family : TreeViewItemBase
    {
        public Family()
        {
            this.Members = new ObservableCollection<FamilyMember>();
        }

        public string Name { get; set; }

        public ObservableCollection<FamilyMember> Members { get; set; }
    }

    public class FamilyMember
    {
        public string Name { get; set; }

    }

    public class TreeViewItemBase : INotifyPropertyChanged
    {
        private bool isSelected;
        public bool IsSelected
        {
            get { return this.isSelected; }
            set
            {
                if (value != this.isSelected)
                {
                    this.isSelected = value;
                    NotifyPropertyChanged("IsSelected");
                }
            }
        }

        private bool isExpanded;
        public bool IsExpanded
        {
            get { return this.isExpanded; }
            set
            {
                if (value != this.isExpanded)
                {
                    this.isExpanded = value;
                    NotifyPropertyChanged("IsExpanded");
                }
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
    }
}