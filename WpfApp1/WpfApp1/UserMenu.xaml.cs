﻿using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for UserMenu.xaml
    /// </summary>
    public partial class UserMenu : Window
    {
        // private SqlConnection myCon = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\ii-proj\Developer-s-Work\WpfApp1\WpfApp1\PCDB.mdf;Integrated Security=True");
        private SqlConnection myCon = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\ii-proj\Developer-s-Work\WpfApp1\WpfApp1\PCDB.mdf;Integrated Security=True");

        private Utilizator utilizator;
        private List<Utilizator> dealers= new List<Utilizator>();
        private Masini newMasina =new Masini();



        public UserMenu(Utilizator utilizator)
        {
            InitializeComponent();
            this.utilizator = utilizator;
            emailTextBlock.Text = utilizator.email;
            myCon.Open();
            if (this.utilizator.isAdmin == 1)
            {
                infoDealearItem.Visibility = Visibility.Visible;
                addCars_Item.Visibility = Visibility.Visible;
                List<Utilizator> utilizatori = new List<Utilizator>();
                DataSet dataset = new DataSet();
                SqlDataAdapter dataAdapter = new SqlDataAdapter("SELECT * FROM [Admin]", myCon);
                dataAdapter.Fill(dataset, "[Admin]");
                foreach (DataRow dr in dataset.Tables["[Admin]"].Rows)
                {


                    String firstName = dr.ItemArray.GetValue(1).ToString();
                    String lastName = dr.ItemArray.GetValue(2).ToString();
                    int admin = Convert.ToInt32(dr.ItemArray.GetValue(3).ToString());
                    String emailRead = dr.ItemArray.GetValue(4).ToString();
                    String passRead = dr.ItemArray.GetValue(5).ToString();

                    utilizatori.Add(new Utilizator(emailRead, passRead, admin, lastName, firstName));

                }

                foreach (Utilizator utilizator1 in utilizatori)
                {
                    if (utilizator1.email == utilizator.email)
                    {
                        utilizator.nume = utilizator1.nume;
                        utilizator.prenume = utilizator1.prenume;

                    }
                }
            }
            else
            {
                List<Utilizator> utilizatori = new List<Utilizator>();
                DataSet dataset = new DataSet();
                SqlDataAdapter dataAdapter = new SqlDataAdapter("SELECT * FROM [Dealer]", myCon);
                dataAdapter.Fill(dataset, "[Dealer]");
                foreach (DataRow dr in dataset.Tables["[Dealer]"].Rows)
                {


                    String firstName = dr.ItemArray.GetValue(1).ToString();
                    String lastName = dr.ItemArray.GetValue(2).ToString();
                    int admin = Convert.ToInt32(dr.ItemArray.GetValue(3).ToString());
                    int sales = Convert.ToInt32(dr.ItemArray.GetValue(4).ToString());
                    String emailRead = dr.ItemArray.GetValue(5).ToString();
                    String passRead = dr.ItemArray.GetValue(6).ToString();

                    utilizatori.Add(new Utilizator(emailRead, passRead, admin, lastName, firstName, sales));

                }

                foreach (Utilizator utilizator1 in utilizatori)
                {
                    if (utilizator1.email == utilizator.email)
                    {
                        utilizator.nume = utilizator1.nume;
                        utilizator.prenume = utilizator1.prenume;
                        utilizator.salesNumber = utilizator1.salesNumber;

                    }
                }

            }
            myCon.Close();

        }

        public List<Utilizator> ReadDealers()
        {
            List<Utilizator> dealers = new List<Utilizator>();
            myCon.Open();
            DataSet dataset = new DataSet();
            SqlDataAdapter dataAdapter = new SqlDataAdapter("SELECT * FROM [user]", myCon);
            dataAdapter.Fill(dataset, "[user]");
            foreach (DataRow dr in dataset.Tables["[user]"].Rows)
            {
                int admin = Convert.ToInt32(dr.ItemArray.GetValue(3).ToString());
                String emailRead = dr.ItemArray.GetValue(1).ToString();
                String passRead = dr.ItemArray.GetValue(2).ToString();
                double salary = Convert.ToDouble(dr.ItemArray.GetValue(4).ToString());
                if(admin ==0)
                    dealers.Add(new Utilizator(emailRead, passRead, admin, salary));
            }

            List<Utilizator> utilizatori = new List<Utilizator>();
            dataAdapter = new SqlDataAdapter("SELECT * FROM [Dealer]", myCon);
            dataAdapter.Fill(dataset, "[Dealer]");
            foreach (DataRow dr in dataset.Tables["[Dealer]"].Rows)
            {

                String firstName = dr.ItemArray.GetValue(1).ToString();
                String lastName = dr.ItemArray.GetValue(2).ToString();
                int admin = Convert.ToInt32(dr.ItemArray.GetValue(3).ToString());
                int sales = Convert.ToInt32(dr.ItemArray.GetValue(4).ToString());
                String emailRead = dr.ItemArray.GetValue(5).ToString();
                String passRead = dr.ItemArray.GetValue(6).ToString();
                utilizatori.Add(new Utilizator(emailRead, passRead, admin, lastName, firstName, sales));
            }
            foreach (Utilizator dealar in dealers)
            {
                foreach (Utilizator utilizator1 in utilizatori)
                {
                    if (utilizator1.email == dealar.email)
                    {
                        dealar.nume = utilizator1.nume;
                        dealar.prenume = utilizator1.prenume;
                        dealar.salesNumber = utilizator1.salesNumber;

                    }
                }
            }
            myCon.Close();
            return dealers;

        }

        private void ButtonLogOut_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void ButtonOpenMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonCloseMenu.Visibility = Visibility.Visible;
            ButtonOpenMenu.Visibility = Visibility.Collapsed;

        }

        private void ButtonCloseMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonOpenMenu.Visibility = Visibility.Visible;
            ButtonCloseMenu.Visibility = Visibility.Collapsed;
        }

        private void PackIcon_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void CarsImg_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
           
        }

        private List<Masini> GetMasini()
        {

            List<Masini> masini = new List<Masini>();
            myCon.Open();
            try
            {
                
                DataSet dataset = new DataSet();
                SqlDataAdapter dataAdapter = new SqlDataAdapter("SELECT * FROM [Car]", myCon);
                dataAdapter.Fill(dataset, "[Car]");
                foreach (DataRow dr in dataset.Tables["[Car]"].Rows)
                {
                    
                    int id = Convert.ToInt32(dr.ItemArray.GetValue(0).ToString());
                    String make = dr.ItemArray.GetValue(1).ToString();
                    String model = dr.ItemArray.GetValue(2).ToString();
                    double price = Convert.ToDouble(dr.ItemArray.GetValue(3).ToString());
                    int caryear = Convert.ToInt32(dr.ItemArray.GetValue(4).ToString());
                    bool issold = Convert.ToBoolean(dr.ItemArray.GetValue(5).ToString());
                    String[] url = dr.ItemArray.GetValue(6).ToString().Split(' ');
                    int hp = Convert.ToInt32(dr.ItemArray.GetValue(7).ToString());
                    String ft = dr.ItemArray.GetValue(8).ToString();

                    List<String> img = new List<string>();
                    for(int i = 0; i < url.Length; i++)
                    {
                        img.Add(url[i]);
                    }
                    if (issold == false)
                    {
                        masini.Add(new Masini(id, make, model, price, caryear, issold, img, hp, ft));
                    }
                }
                
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            myCon.Close();

            return masini;
            
        }

        private void CarText_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
        }

        private void CarStack_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void CarItem_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            gridCarsImage.Visibility = Visibility.Visible;
            gridParking.Visibility = Visibility.Hidden;
            gridHome.Visibility = Visibility.Hidden;
            gridDealerInfo.Visibility = Visibility.Hidden;
            gridAddCar.Visibility = Visibility.Hidden;


            var masini = GetMasini();
            if (masini.Count > 0)
                ListViewProducts.ItemsSource = masini;
            CollectionView collectionView = (CollectionView)CollectionViewSource.GetDefaultView(ListViewProducts.ItemsSource);
            collectionView.Filter = UserFilter;
        }

        private bool UserFilter(object item)
        {
            if (String.IsNullOrEmpty(QSTextBox.Text))
                return true;
            else
                return ((item as Masini).name.IndexOf(QSTextBox.Text, StringComparison.OrdinalIgnoreCase) >= 0);
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(ListViewProducts.ItemsSource).Refresh();
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(1);
        }

        private void ParkingItem_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            gridCarsImage.Visibility = Visibility.Hidden;
            gridParking.Visibility = Visibility.Visible;
            gridHome.Visibility = Visibility.Hidden;
            gridDealerInfo.Visibility = Visibility.Hidden;
            gridAddCar.Visibility = Visibility.Hidden;

        }

        private void ListViewProducts_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            Masini masina = (Masini)ListViewProducts.SelectedItem;
            if (ListViewProducts.SelectedItem != null)
            {
                try
                {
                    if (masina.isSold == true)
                    {
                        new MessageBoxPoni("Masina Cumparata!").Show();
                        return;
                    }
                }
                catch (Exception ex)
                {
                }
                ProductPrezentation product = new ProductPrezentation(utilizator, masina);
                List<Masini> masinis = new List<Masini>();
                product.ShowDialog();
                // update User 
                utilizator = product.GetUtilizator();
                // update lista masini
                masina = product.GetMasini();
                product.Close();
            }
     
           
        }

        

        private void ButtonAccInfo_Click(object sender, RoutedEventArgs e)
        {
            AccInfo accInfo = new AccInfo(utilizator);
            accInfo.Show();
            this.Close();

        }

        private void ButtonHome_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            gridCarsImage.Visibility = Visibility.Hidden;
            gridParking.Visibility = Visibility.Hidden;
            gridHome.Visibility = Visibility.Visible;
            gridDealerInfo.Visibility = Visibility.Hidden;
            gridAddCar.Visibility = Visibility.Hidden;
        }

        private void infoDealearItem_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            gridCarsImage.Visibility = Visibility.Hidden;
            gridParking.Visibility = Visibility.Hidden;
            gridHome.Visibility = Visibility.Hidden;
            gridDealerInfo.Visibility = Visibility.Visible;
            gridAddCar.Visibility = Visibility.Hidden;
            dealers.Clear();
            dealrLB.Items.Clear();
            dealers  = ReadDealers();
            foreach (Utilizator dealer in dealers)
            {
                dealrLB.Items.Add(dealer.nume +" "+ dealer.prenume);
            }
        }
        private void dealrLB_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                SalaryTB.Text = "" + dealers.ElementAt(dealrLB.SelectedIndex).salary;
                SalesNumberTB.Text = "" + dealers.ElementAt(dealrLB.SelectedIndex).salesNumber;
            }
            catch (Exception EX)
            {
                MessageBox.Show("bug");
            }
        }

        private void dealerFireBt_Click(object sender, RoutedEventArgs e)
        {
            bool ok = true;
            myCon.Open();
            Utilizator delDealer = dealers.ElementAt(dealrLB.SelectedIndex);
            dealrLB.Items.Clear();
            SqlCommand cmd = new SqlCommand();
            
            try
            {
                
                cmd = new SqlCommand("DELETE FROM [Dealer] WHERE Email= @email", myCon);
                cmd.Parameters.AddWithValue("email", delDealer.email);
                cmd.ExecuteNonQuery();
                cmd = new SqlCommand("DELETE FROM [user] WHERE Email= @email", myCon);
                cmd.Parameters.AddWithValue("email", delDealer.email);
                cmd.ExecuteNonQuery();
                ok = true;

            }
            catch (Exception ex)
            {
                new MessageBoxPoni("Error").Show();
                ok = false;
            }
            myCon.Close();

            dealers.Clear();
            dealers = ReadDealers();
            foreach(Utilizator utilizator in dealers)
            {
                dealrLB.Items.Add(utilizator.nume + " " + utilizator.prenume);
            }
            if (ok)
            {
                new MessageBoxPoni("Dealer Fired").Show();

            }
        }

        private void addDealerBt_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Utilizator test = null;
                SignUp signUp = new SignUp(test);
                signUp.ShowDialog();
                int i=0;
                
                test = signUp.getUtil();
                signUp.Close();
                dealers.Add(test);
                dealrLB.Items.Add(test.nume+' '+test.prenume);
             
            }
            catch(Exception ex)
            {
                new MessageBoxPoni("Dealer Aded").Show();
            }
        }

        private void promoteDealerBt_Click(object sender, RoutedEventArgs e)
        {
            gridChangeS.Visibility = Visibility.Visible;
        }
        private void ChangeButton_Click(object sender, RoutedEventArgs e)
        {
            
            SqlCommand cmd = new SqlCommand();
            bool ok = false;
            try
           {
                Utilizator upDealer = dealers.ElementAt(dealrLB.SelectedIndex);
                myCon.Open();
                cmd = new SqlCommand("UPDATE [user] SET Salary=@Salary WHERE [Email]=@Email", myCon);
                cmd.Parameters.AddWithValue("@Email",upDealer.email);
                cmd.Parameters.AddWithValue("@Salary", SumTB.Text);
                cmd.ExecuteNonQuery();
                ok = true;
            }
            catch (Exception ex)
            {
                new MessageBoxPoni("Dealer not selected").Show();
                ok = false;
            }
            myCon.Close();
            SumTB.Text ="";
            gridChangeS.Visibility = Visibility.Hidden;
            dealers.Clear();
            dealers = ReadDealers();
            dealrLB.Items.Clear();
            foreach (Utilizator utilizator in dealers)
            {
                dealrLB.Items.Add(utilizator.nume + " " + utilizator.prenume);
            }
            if (ok)
            {
                new MessageBoxPoni("Salary Changed").Show();

            }
        }
    

        private void ApplyButton_Click(object sender, RoutedEventArgs e)
        {
            List<Masini> masini = GetMasini();
            if (prRB1.IsChecked==true)
            {
                for(int i=0;i<masini.Count;i++)
                {
                    if(masini[i].carPrice>=4000 && masini[i].carPrice <= 10000)
                    {

                    }
                    else
                    {
                        masini.RemoveAt(i);
                        i--;
                    }
                }
            }
            if (prRB2.IsChecked == true)
            {
                for (int i = 0; i < masini.Count; i++)
                {
                    if (masini[i].carPrice >= 10000 && masini[i].carPrice <= 20000)
                    {

                    }
                    else
                    {
                        masini.RemoveAt(i);
                        i--;
                    }
                }
            }
            if (prRB3.IsChecked == true)
            {
                for (int i = 0; i < masini.Count; i++)
                {
                    if (masini[i].carPrice >= 20000 && masini[i].carPrice <= 30000)
                    {

                    }
                    else
                    {
                        masini.RemoveAt(i);
                        i--;
                    }
                }
            }
            if (prRB4.IsChecked == true)
            {
                for (int i = 0; i < masini.Count; i++)
                {
                    if (masini[i].carPrice >= 30000 && masini[i].carPrice <= 40000)
                    {

                    }
                    else
                    {
                        masini.RemoveAt(i);
                        i--;
                    }
                }
            }
            if (prRB5.IsChecked == true)
            {
                for (int i = 0; i < masini.Count; i++)
                {
                    if (masini[i].carPrice >= 40000)
                    {

                    }
                    else
                    {
                        masini.RemoveAt(i);
                        i--;
                    }
                }
            }

            if (ftRB1.IsChecked == true)
            {
                for (int i = 0; i < masini.Count; i++)
                {
                    if (masini[i].FuelType.Equals("Diesel"))
                    {

                    }
                    else
                    {
                        masini.RemoveAt(i);
                        i--;
                    }
                }
            }

            if (ftRB2.IsChecked == true)
            {
                for (int i = 0; i < masini.Count; i++)
                {
                    if (masini[i].FuelType.Equals("Petrol"))
                    {

                    }
                    else
                    {
                        masini.RemoveAt(i);
                        i--;
                    }
                }
            }

            if (hpRB1.IsChecked == true)
            {
                for (int i = 0; i < masini.Count; i++)
                {
                    if (masini[i].HorsePower>=75 && masini[i].HorsePower<=105)
                    {

                    }
                    else
                    {
                        masini.RemoveAt(i);
                        i--;
                    }
                }
            }

            if (hpRB2.IsChecked == true)
            {
                for (int i = 0; i < masini.Count; i++)
                {
                    if (masini[i].HorsePower > 105 && masini[i].HorsePower <= 195)
                    {

                    }
                    else
                    {
                        masini.RemoveAt(i);
                        i--;
                    }
                }
            }

            if (hpRB3.IsChecked == true)
            {
                for (int i = 0; i < masini.Count; i++)
                {
                    if (masini[i].HorsePower>195)
                    {

                    }
                    else
                    {
                        masini.RemoveAt(i);
                        i--;
                    }
                }
            }
            hpRB3.IsChecked = false;
            hpRB2.IsChecked = false;
            hpRB1.IsChecked = false;
            ftRB2.IsChecked = false;
            ftRB1.IsChecked = false;
            prRB5.IsChecked = false;
            prRB4.IsChecked = false;
            prRB3.IsChecked = false;
            prRB2.IsChecked = false;
            prRB1.IsChecked = false;

            if (masini.Count > 0)
            {
                ListViewProducts.ItemsSource = masini;
                CollectionView collectionView = (CollectionView)CollectionViewSource.GetDefaultView(ListViewProducts.ItemsSource);
                collectionView.Filter = UserFilter;
            }
            else new MessageBoxPoni("There are no car with that configuration!").Show();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void addFeature_Click(object sender, RoutedEventArgs e)
        {
            newMasina.Features.Add(featureACTB.Text);
            countFACL.Visibility = Visibility.Visible;
            countFACL.Content = Convert.ToInt32(countFACL.Content) + 1;
            featureACTB.Text = "";

        }

        private void addPhotosACB_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";
            try
            {
                openFileDialog.InitialDirectory = @"C:\Users\Castoleru\OneDrive - Technical University of Cluj-Napoca\Desktop\cars";
            }catch(Exception ex)
            {

            }
            if (openFileDialog.ShowDialog() == true)
            {
                // read img file and add to the path we want in the project
                try
                {
                    string[] files = openFileDialog.FileName.Split('\\');
                    string photoName = files[files.Length - 1];
                    string photoPath = "";
                    for (int i = 0; i < files.Length - 1; i++)
                    {
                        photoPath = photoPath + files[i] + '\\';
                    }
                    photoPath = photoPath.Remove(photoPath.Length - 1);
                    string projectFile = @"D:\ii-proj\Developer-s-Work\WpfApp1\WpfApp1\Assests";
                    string sourceFile = System.IO.Path.Combine(photoPath, photoName);
                    string destFile = System.IO.Path.Combine(projectFile, photoName);
                    System.IO.File.Copy(sourceFile, destFile, true);
                    string dbFile = @"Assests/" + photoName;
                    newMasina.URL.Add(dbFile);
                    countPACL.Visibility = Visibility.Visible;
                    countPACL.Content = Convert.ToInt32(countPACL.Content) + 1;
                }catch(Exception ex)
                {
                    new MessageBoxPoni("Doar o poza poate fi \n adaugata pe un rand").Show();
                }

            }

        }
        private bool TextsCheck()
        {
            // true - at least one box is empty
            // false - no box is empty
            return (Convert.ToInt32(countFACL.Content) == 0 && Convert.ToInt32(countPACL.Content) == 0 && makeACTB.Text == "" && modelACTB.Text == "" && priceACTB.Text == "" && yearACTB.Text == "" && horsePowerACTB.Text == "" && FuelTypeACTB.Text == "" && colorACTB.Text == "" && Co2EACTB.Text == "" && parkingSpotACTB.Text == "" && cosumptionACTB.Text == "" && tractionACTB.Text == "" && CilindricalACTB.Text == "");

        }
        private void addCarACB_Click(object sender, RoutedEventArgs e)
        {
            
            if(TextsCheck() == true)
            {
                new MessageBoxPoni("All boxex must be completed \n" +
                    "and you need to add a photo").Show();
            }
            else
            {
                string feautersFormat="";
                foreach(string feat in newMasina.Features)
                {
                        feautersFormat = feat + "@" + feautersFormat;
                }
                string urlFormat = "";
                foreach (string url in newMasina.URL)
                {
                        urlFormat = url + " " + urlFormat;
                }

                newMasina.make = makeACTB.Text;
                newMasina.model = modelACTB.Text;
                newMasina.isSold = isSoldACCB.IsChecked == true;
                newMasina.FuelType = FuelTypeACTB.Text;
                newMasina.carPrice = Convert.ToDouble(priceACTB.Text);
                newMasina.carYear = Convert.ToInt32(yearACTB.Text);
                newMasina.color = colorACTB.Text;
                newMasina.Co2E = Convert.ToInt32(Co2EACTB.Text);
                newMasina.ParkingSpot = parkingSpotACTB.Text;
                newMasina.Consumption =Convert.ToInt32(cosumptionACTB.Text);
                newMasina.Traction = tractionACTB.Text;
                newMasina.CilindricalCap = Convert.ToInt32(CilindricalACTB.Text);
                newMasina.HorsePower =Convert.ToInt32(horsePowerACTB.Text);
                SqlCommand cmd = new SqlCommand();
                myCon.Open();
                
                    cmd = new SqlCommand("INSERT INTO [Car] (Make,[Model],CarPrice,CarYear,IsSold,link,HorsePower,FuelType) VALUES (@Make,@Model,@CarPrice,@CarYear,@IsSold,@link,@HorsePower,@FuelType) ", myCon);
                    cmd.Parameters.AddWithValue("@Make", newMasina.make);
                    cmd.Parameters.AddWithValue("@Model", newMasina.model);
                    cmd.Parameters.AddWithValue("@CarPrice", newMasina.carPrice);
                    cmd.Parameters.AddWithValue("@CarYear", newMasina.carYear);
                    cmd.Parameters.AddWithValue("@IsSold", newMasina.isSold);
                    cmd.Parameters.AddWithValue("@link", urlFormat);
                    cmd.Parameters.AddWithValue("@HorsePower", newMasina.HorsePower);
                    cmd.Parameters.AddWithValue("@FuelType", newMasina.FuelType);
                    cmd.ExecuteNonQuery();
                    int carID = 0; 
                    DataSet dataset = new DataSet();
                    SqlDataAdapter dataAdapter = new SqlDataAdapter("SELECT * FROM [Car]", myCon);
                    dataAdapter.Fill(dataset, "[Car]");
                    foreach (DataRow dr in dataset.Tables["[Car]"].Rows)
                    {

                        carID = Convert.ToInt32(dr.ItemArray.GetValue(0).ToString());
       
                    }

                    cmd = new SqlCommand("INSERT INTO [SpecificationCar] (CarId,Color,Co2E,ParkingSpot,Consumption,Traction,CilindricalCap,Features) VALUES (@CarId,@Color,@Co2E,@ParkingSpot,@Consumption,@Traction,@CilindricalCap,@Features)  ", myCon);
                    cmd.Parameters.AddWithValue("@CarId", carID);
                    cmd.Parameters.AddWithValue("@Color", newMasina.color);
                    cmd.Parameters.AddWithValue("@Co2E", newMasina.Co2E);
                    cmd.Parameters.AddWithValue("@ParkingSpot", newMasina.ParkingSpot);
                    cmd.Parameters.AddWithValue("@Consumption", newMasina.Consumption);
                    cmd.Parameters.AddWithValue("@Traction", newMasina.Traction);
                    cmd.Parameters.AddWithValue("@CilindricalCap", newMasina.CilindricalCap);
                    cmd.Parameters.AddWithValue("@Features", feautersFormat);
                    cmd.ExecuteNonQuery();

                    newMasina = new Masini();
                    featureACTB.Text = "";
                    makeACTB.Text = "";
                    modelACTB.Text = "";
                    priceACTB.Text = "";
                    yearACTB.Text = "";
                    horsePowerACTB.Text = "";
                    FuelTypeACTB.Text = "";
                    isSoldACCB.IsChecked = false;
                    colorACTB.Text = "";
                    Co2EACTB.Text = "";
                    parkingSpotACTB.Text = "";
                    cosumptionACTB.Text = "";
                    tractionACTB.Text = "";
                    CilindricalACTB.Text = "";
                    countFACL.Content = "0";
                    countPACL.Content = "0";
                    countFACL.Visibility = Visibility.Hidden;
                    countPACL.Visibility = Visibility.Hidden;


                myCon.Close();
            }

        }

        private void addCars_Item_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            gridCarsImage.Visibility = Visibility.Hidden;
            gridParking.Visibility = Visibility.Hidden;
            gridHome.Visibility = Visibility.Hidden;
            gridDealerInfo.Visibility = Visibility.Hidden;
            gridAddCar.Visibility = Visibility.Visible;
            newMasina = new Masini();
            featureACTB.Text = "";
            makeACTB.Text = "";
            modelACTB.Text = "";
            priceACTB.Text = "";
            yearACTB.Text = "";
            horsePowerACTB.Text = "";
            FuelTypeACTB.Text = "";
            isSoldACCB.IsChecked = false;
            colorACTB.Text = "";
            Co2EACTB.Text = "";
            parkingSpotACTB.Text = "";
            cosumptionACTB.Text = "";
            tractionACTB.Text = "";
            CilindricalACTB.Text = "";
            countFACL.Content = "0";
            countPACL.Content = "0";
            countFACL.Visibility = Visibility.Hidden;
            countPACL.Visibility = Visibility.Hidden;
        }
    }
}
