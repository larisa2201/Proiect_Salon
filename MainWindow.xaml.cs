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
using SalonModel;
using System.Data.Entity;
using System.Data;

namespace Proiect_Salon
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    enum ActionState
    {
        New,
        Edit,
        Delete,
        Nothing
    }
    public partial class MainWindow : Window
    {

        ActionState action = ActionState.Nothing;
        SalonEntitiesModel ctx = new SalonEntitiesModel();
        CollectionViewSource clientVSource, serviciuVSource, facturaVSource;
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            clientVSource =
                ((System.Windows.Data.CollectionViewSource)(this.FindResource("clientiViewSource")));
            clientVSource.Source = ctx.Clientis.Local;
            ctx.Clientis.Load();
            serviciuVSource =
                 ((System.Windows.Data.CollectionViewSource)(this.FindResource("serviciiViewSource")));
           
            facturaVSource =
                 ((System.Windows.Data.CollectionViewSource)(this.FindResource("facturiViewSource")));
            
            ctx.Facturis.Load();
            
            ctx.Serviciis.Load();
            
            serviciuVSource.Source = ctx.Serviciis.Local;
            facturaVSource.Source = ctx.Facturis.Local;
            
            System.Windows.Data.CollectionViewSource programariViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("programariViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // programariViewSource.Source = [generic data source]
            System.Windows.Data.CollectionViewSource serviciiViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("serviciiViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // serviciiViewSource.Source = [generic data source]
            System.Windows.Data.CollectionViewSource angajatiViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("angajatiViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // angajatiViewSource.Source = [generic data source]
            System.Windows.Data.CollectionViewSource clientiViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("clientiViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // clientiViewSource.Source = [generic data source]
            System.Windows.Data.CollectionViewSource facturiViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("facturiViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // facturiViewSource.Source = [generic data source]
        }

        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            action = ActionState.New;
           
            SetValidationBinding();
        }
        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            action = ActionState.Edit;
           BindingOperations.ClearBinding(numeTextBox, TextBox.TextProperty);
           BindingOperations.ClearBinding(prenumeTextBox, TextBox.TextProperty);
            BindingOperations.ClearBinding(denumireTextBox, TextBox.TextProperty);
            SetValidationBinding();

        }
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            action = ActionState.Delete;
        }

        private void BtnPrevS_Click(object sender, RoutedEventArgs e)
        {
            serviciuVSource.View.MoveCurrentToPrevious();
        }

        

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            facturaVSource.View.MoveCurrentToPrevious();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            facturaVSource.View.MoveCurrentToNext();
        }

       

        private void btnNext1_Click(object sender, RoutedEventArgs e)
        {
            clientVSource.View.MoveCurrentToNext();
        }

        private void btnPrev_Click(object sender, RoutedEventArgs e)
        {
            clientVSource.View.MoveCurrentToPrevious();
        }
        private void SaveClienti()
        {
            Clienti client = null;
            if (action == ActionState.New)
            {
                try
                {
                   
                    client = new Clienti()
                    {
                        Nume = numeTextBox.Text.Trim(),
                        Prenume = prenumeTextBox.Text.Trim(),
                        E_mail=e_mailTextBox.Text.Trim(),
                        Parola=parolaTextBox.Text.Trim(),
                        Telefon=telefonTextBox.Text.Trim()

                    };
                   
                    ctx.Clientis.Add(client);
                    clientVSource.View.Refresh();
                    
                    ctx.SaveChanges();
                }
               
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
if (action == ActionState.Edit)
            {
                try
                {
                    client = (Clienti)clientiDataGrid.SelectedItem;
                    client.Nume = numeTextBox.Text.Trim();
                    client.Prenume = prenumeTextBox.Text.Trim();
                    
                    
                    ctx.SaveChanges();
                }
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else if (action == ActionState.Delete)
            {
                try
                {
                    client = (Clienti)clientiDataGrid.SelectedItem;
                    ctx.Clientis.Remove(client);
                    ctx.SaveChanges();
                }
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                clientVSource.View.Refresh();

            }
        }

        private void btnNextS_Click(object sender, RoutedEventArgs e)
        {
            serviciuVSource.View.MoveCurrentToNext();
        }
        private void Operatii_Click(object sender, RoutedEventArgs e)
        {
            Button SelectedButton = (Button)e.OriginalSource;
            Panel panel = (Panel)SelectedButton.Parent;
            foreach (Button B in panel.Children.OfType<Button>())
            {
                if (B != SelectedButton)
                    B.IsEnabled = false;
            }
            Actiuni.IsEnabled = true;
        }
        private void ReInitialize()
        {
            Panel panel = Operatii.Content as Panel;
            foreach (Button B in panel.Children.OfType<Button>())
            {
                B.IsEnabled = true;
            }
            Actiuni.IsEnabled = false;
        }
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            ReInitialize();
        }
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            TabItem ti = tbCtrl.SelectedItem as TabItem;
            switch (ti.Header)
            {
               
                case "Servicii":
                    SaveServicii();
                    break;
                case "Clienti":
                    SaveClienti();
                    break;
                
                case "Facturi":
                    SaveFacturi();
                    break;
            }
            ReInitialize();
        }

        private void SaveServicii()
        {
            Servicii serviciu = null;
            if (action == ActionState.New)
            {
                try
                {
                   
                    serviciu = new Servicii()
                    {
                        Denumire = denumireTextBox.Text.Trim(),
                        Pret=Decimal.Parse(pretTextBox.Text.Trim()),
                        Timp_Executie=TimeSpan.Parse(timp_ExecutieTextBox.Text.Trim())
                     };
                    
                    ctx.Serviciis.Add(serviciu);
                    serviciuVSource.View.Refresh();
                    
                    ctx.SaveChanges();
                }
                
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            if (action == ActionState.Edit)
            {
                try
                {
                    serviciu = (Servicii)serviciiDataGrid.SelectedItem;
                    serviciu.Denumire = denumireTextBox.Text.Trim();

                    ctx.SaveChanges();
                }
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else if (action == ActionState.Delete)
            {
                try
                {
                    serviciu = (Servicii)serviciiDataGrid.SelectedItem;
                    ctx.Serviciis.Remove(serviciu);
                    ctx.SaveChanges();
                }
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                serviciuVSource.View.Refresh();
            }
        }
      
        private void SaveFacturi()
        {
            Facturi factura = null;
            if (action == ActionState.New)
            {
                try
                {
                    
                    factura = new Facturi()
                    {
                        Data = DateTime.Parse(dataDatePicker.Text.Trim()),
                        Id_client = int.Parse(id_clientTextBox.Text),
                        Id_serviciu=int.Parse(id_serviciuTextBox.Text),
                        Total=decimal.Parse(totalTextBox.Text)
                    };
                    
                    ctx.Facturis.Add(factura);
                    facturaVSource.View.Refresh();
                    
                    ctx.SaveChanges();
                }
                
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
           if (action == ActionState.Edit)
            {
                try
                {
                    factura = (Facturi)facturiDataGrid.SelectedItem;
                    factura.Data = DateTime.Parse(dataDatePicker.Text);

                    ctx.SaveChanges();
                }
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else if (action == ActionState.Delete)
            {
                try
                {
                    factura = (Facturi)facturiDataGrid.SelectedItem;
                    ctx.Facturis.Remove(factura);
                    ctx.SaveChanges();
                }
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                facturaVSource.View.Refresh();
            }

        }

        private void SetValidationBinding()
        {
            Binding firstNameValidationBinding = new Binding();
            firstNameValidationBinding.Source = clientVSource;
            firstNameValidationBinding.Path = new PropertyPath("Prenume");
            firstNameValidationBinding.NotifyOnValidationError = true;
            firstNameValidationBinding.Mode = BindingMode.TwoWay;
            firstNameValidationBinding.UpdateSourceTrigger =
           UpdateSourceTrigger.PropertyChanged;
            
            firstNameValidationBinding.ValidationRules.Add(new StringNotEmpty());
            prenumeTextBox.SetBinding(TextBox.TextProperty,
           firstNameValidationBinding);

            Binding lastNameValidationBinding = new Binding();
            lastNameValidationBinding.Source = clientVSource;
            lastNameValidationBinding.Path = new PropertyPath("Nume");
            lastNameValidationBinding.NotifyOnValidationError = true;
            lastNameValidationBinding.Mode = BindingMode.TwoWay;
            lastNameValidationBinding.UpdateSourceTrigger =
           UpdateSourceTrigger.PropertyChanged;
            
            lastNameValidationBinding.ValidationRules.Add(new
           StringMinLengthValidator());
            numeTextBox.SetBinding(TextBox.TextProperty,
           lastNameValidationBinding); 

            Binding denumireValidationBinding = new Binding();
            denumireValidationBinding.Source = serviciuVSource;
            denumireValidationBinding.Path = new PropertyPath("Denumire");
            denumireValidationBinding.NotifyOnValidationError = true;
            denumireValidationBinding.Mode = BindingMode.TwoWay;
            denumireValidationBinding.UpdateSourceTrigger =
                UpdateSourceTrigger.PropertyChanged;
            denumireValidationBinding.ValidationRules.Add(new StringNotEmpty());
            denumireTextBox.SetBinding(TextBox.TextProperty, denumireValidationBinding);
        } 
    }
}
