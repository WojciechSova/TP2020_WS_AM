using System;
using System.Windows;
using View.DI;
using ViewModel;

namespace View
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
            MainViewModel mc = (MainViewModel)DataContext;
            mc.WindowResolver = new CreditCardDetailsResolver();
        }
    }
}
