using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WPFApp.Models;

namespace WPFApp
{
    public partial class MainWindow : Window
    {
        private ObservableCollection<Door> myDbList;
        private Door? selectedDoor;
        private DoorsDbContext db;
        public MainWindow()
        {
            InitializeComponent();
            db = new DoorsDbContext();
            myDbList = new ObservableCollection<Door>(db.Doors.ToList());
            myDoorsGrid.ItemsSource = myDbList;

        }


       

        private void add_reservation(object sender, RoutedEventArgs e)
        {
            if(TextB_Name.Text == "" 
                || TextB_Model.Text.Equals("")
                || TextB_Price.Text == "")
            {
                MessageBox.Show("Fill all fields!!!");
            }
            else
            {
                Door door = new Door { Dname = TextB_Name.Text, 
                        Dmodel =  TextB_Model.Text,
                        Dprice = decimal.Parse(TextB_Price.Text)};
                
                db.Add(door);
                int num = db.SaveChanges();
                if(num > 0)
                {
                    myDbList.Add(door);
                }
                
                
                clearFields();

            }
        }

        private void edit_reservation(object sender, RoutedEventArgs e)
        {
            selectedDoor = myDoorsGrid.SelectedItem as Door;
            if(selectedDoor != null)
            {
                TextB_Name.Text = selectedDoor.Dname;
                TextB_Model.Text = selectedDoor.Dmodel;
                TextB_Price.Text = selectedDoor.Dprice.ToString();
            }
            else
            {
                MessageBox.Show("Fill all fields!!!");
            }
        }

        private void delete_reservation(object sender, RoutedEventArgs e)
        {
            Door? doorToDelete = myDoorsGrid.SelectedItem as Door;

            if(doorToDelete != null)
            {
                var deletionResult = MessageBox.Show($"Are you sure U want delete? \n" +
                    $"name: {doorToDelete.Dname} \n" +
                    $"model: {doorToDelete.Dmodel},\n" +
                    $"price: {doorToDelete.Dprice}",
                    "Deletion warning",MessageBoxButton.YesNo,MessageBoxImage.Stop);
                if (deletionResult == MessageBoxResult.Yes)
                {
                    db.Remove(doorToDelete);
                    int num = db.SaveChanges();
                    if (num > 0)
                    {
                        myDbList.Remove(doorToDelete);
                    }
                    MessageBox.Show("Deletion completed!");
                }
                else
                {
                    MessageBox.Show("Deletion cancelled!");
                }
            }
            else
            {
                MessageBox.Show("Please select item to delete!!!");
            }

        }
        public void clearFields()
        {
            TextB_Name.Text = string.Empty;
            TextB_Model.Text = string.Empty;
            TextB_Price.Text = string.Empty;
        }

        private void update_reservation(object sender, RoutedEventArgs e)
        {
            Door? doorToEdit = myDoorsGrid.SelectedItem as Door;
            if (selectedDoor != null){
                var editResult = MessageBox.Show($"Are you sure U want edit this item? \n" +
                    $"name: {doorToEdit.Dname} \n" +
                    $"model: {doorToEdit.Dmodel},\n" +
                    $"price: {doorToEdit.Dprice}",
                    "Edition warning", MessageBoxButton.YesNo, MessageBoxImage.Stop);
                if (editResult == MessageBoxResult.Yes)
                {
                    
                    try
                    {
                        if (TextB_Name.Text == null
                                || TextB_Name.Text == string.Empty)
                        {
                            MessageBox.Show("Empty Name!!");
                            TextB_Name.Text = selectedDoor.Dname;
                        }
                        if (TextB_Model.Text == null
                                || TextB_Model.Text == string.Empty)
                        {
                            MessageBox.Show("Empty Model!!");
                            TextB_Model.Text = selectedDoor.Dmodel;
                        }
                        if (TextB_Price.Text == null
                                || TextB_Price.Text == string.Empty)
                        {
                            MessageBox.Show("Empty Price!!");
                            TextB_Price.Text = selectedDoor.Dprice.ToString();
                        }
                        selectedDoor.Dname = TextB_Name.Text;
                        selectedDoor.Dmodel = TextB_Model.Text;
                        selectedDoor.Dprice = Convert.ToDecimal(TextB_Price.Text);
                    }
                    catch (Exception exc)
                    {

                    }
                    db.Update(selectedDoor);
                    db.SaveChanges();
                    myDoorsGrid.Items.Refresh();
                    clearFields();
                }
                else
                {
                    MessageBox.Show("Edition cancelled!");
                }
            }
        }
    }
}