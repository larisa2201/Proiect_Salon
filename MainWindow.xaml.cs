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
        CollectionViewSource clientVSource, serviciuVSource, angajatVSource, programareVSource, facturaVSource;
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            clientVSource =
                ((System.Windows.Data.CollectionViewSource)(this.FindResource("clientViewSource")));
            clientVSource.Source = ctx.Clientis.Local;
            ctx.Clientis.Load();
            serviciuVSource =
                 ((System.Windows.Data.CollectionViewSource)(this.FindResource("serviciuViewSource")));
            angajatVSource =
                 ((System.Windows.Data.CollectionViewSource)(this.FindResource("angajatViewSource")));
            programareVSource =
                 ((System.Windows.Data.CollectionViewSource)(this.FindResource("programareViewSource")));
            facturaVSource =
                 ((System.Windows.Data.CollectionViewSource)(this.FindResource("facturaViewSource")));
            ctx.Angajatis.Load();
            ctx.Facturis.Load();
            ctx.Programaris.Load();
            ctx.Serviciis.Load();

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
        }
        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            action = ActionState.Edit;
        }
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            action = ActionState.Delete;
        }

        private void BtnPrevS_Click(object sender, RoutedEventArgs e)
        {
            serviciuVSource.View.MoveCurrentToPrevious();
        }

        private void btnPrevious_Click(object sender, RoutedEventArgs e)
        {
            angajatVSource.View.MoveCurrentToPrevious();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            angajatVSource.View.MoveCurrentToNext();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            facturaVSource.View.MoveCurrentToPrevious();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            facturaVSource.View.MoveCurrentToNext();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            programareVSource.View.MoveCurrentToPrevious();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            programareVSource.View.MoveCurrentToNext();
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
                    //instantiem Customer entity
                    client = new Clienti()
                    {
                        Nume = numeTextBox.Text.Trim(),
                        Prenume = prenumeTextBox.Text.Trim()
                    };
                    //adaugam entitatea nou creata in context
                    ctx.Clientis.Add(client);
                    clientVSource.View.Refresh();
                    //salvam modificarile
                    ctx.SaveChanges();
                }
                //using System.Data;
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
                    //salvam modificarile
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
            Panel panel = Actiuni.Content as Panel;
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
                case "Angajati":
                    SaveAngajati();
                    break;
                case "Servicii":
                    SaveServicii();
                    break;
                case "Clienti":
                    SaveClienti();
                    break;
                case "Programari":
                    SaveProgramari();
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
                    //instantiem Customer entity
                    serviciu = new Servicii()
                    {
                        Denumire = denumireTextBox.Text.Trim(),
                        
                    };
                    //adaugam entitatea nou creata in context
                    ctx.Serviciis.Add(serviciu);
                    serviciuVSource.View.Refresh();
                    //salvam modificarile
                    ctx.SaveChanges();
                }
                //using System.Data;
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
        private void SaveAngajati()
        {
            Angajati angajat = null;
            if (action == ActionState.New)
            {
                try
                {
                    //instantiem Customer entity
                    angajat = new Angajati()
                    {
                        Nume = numeTextBox.Text.Trim(),
                        Prenume = prenumeTextBox.Text.Trim()
                    };
                    //adaugam entitatea nou creata in context
                    ctx.Angajatis.Add(angajat);
                    angajatVSource.View.Refresh();
                    //salvam modificarile
                    ctx.SaveChanges();
                }
                //using System.Data;
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
                    angajat = (Angajati)angajatiDataGrid.SelectedItem;
                    angajat.Nume= numeTextBox.Text.Trim();
                    angajat.Prenume = prenumeTextBox.Text.Trim();
                    //salvam modificarile
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
                   angajat = (Angajati)angajatiDataGrid.SelectedItem;
                    ctx.Angajatis.Remove(angajat);
                    ctx.SaveChanges();
                }
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                angajatVSource.View.Refresh();
            }
        }
        private void SaveProgramari()
        {
            Programari programare = null;
            if (action == ActionState.New)
            {
                try
                {
                    //instantiem Programari entity
                    programare = new Programari()
                    {
                      Data  = dataTextBox.Text.Trim(),
                       
                    };
                    //adaugam entitatea nou creata in context
                    ctx.Programaris.Add(programare);
                    programareVSource.View.Refresh();
                    //salvam modificarile
                    ctx.SaveChanges();
                }
                //using System.Data;
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
                    programare = (Programari)programariDataGrid.SelectedItem;
                    programare.Data = dataTextBox.Text.Trim();
                    
                    //salvam modificarile
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
                    programare = (Programari)programariDataGrid.SelectedItem;
                    ctx.Programaris.Remove(programare);
                    ctx.SaveChanges();
                }
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                programareVSource.View.Refresh();
            }

        }
        private void SaveFacturi()
        {
            Facturi factura = null;
            if (action == ActionState.New)
            {
                try
                {
                    //instantiem Factura entity
                    factura = new Facturi()
                    {
                     //   Data =dataTextBox.Text.Trim()
                    };
                    //adaugam entitatea nou creata in context
                    ctx.Facturis.Add(factura);
                    facturaVSource.View.Refresh();
                    //salvam modificarile
                    ctx.SaveChanges();
                }
                //using System.Data;
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
                   // factura.Data = dataTextBox.Text.Trim();
                    //salvam modificarile
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

    }
}
