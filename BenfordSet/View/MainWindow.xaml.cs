using BenfordSet.Common;
using BenfordSet.View;
using BenfordSet.ViewModel;
using log4net.Repository.Hierarchy;
using System;
using System.Threading;
using System.Windows;
using System.Windows.Media.Animation;

namespace BenfordSet
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        internal MainWindowViewModel? mainwindowviewmodel;
        internal MainWindow()
        {
            Mutex mutex = new Mutex(true,"benford-analyse",out bool aquiredNew);
            if(aquiredNew)
            {
                Timeline.DesiredFrameRateProperty.OverrideMetadata(typeof(Timeline),
                   new FrameworkPropertyMetadata { DefaultValue = 60 });
                mainwindowviewmodel = new MainWindowViewModel();
                DataContext = mainwindowviewmodel;
                mainwindowviewmodel.OpenMessageBox = (title,text) =>
                {
                    return MessageBox.Show(title,text,MessageBoxButton.YesNo,MessageBoxImage.Question);
                };

                InitializeComponent();
            }
            else
                MessageBox.Show("The app is already running.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            mutex.ReleaseMutex();
        }

        private void Window_Loaded(object sender,RoutedEventArgs e)
        {
            if(mainwindowviewmodel == null)
                throw new ArgumentNullException(nameof(mainwindowviewmodel));

            mainwindowviewmodel.OpenSettingView += (s,ev) =>
            {
                Settings settings = new Settings();
                settings.ShowDialog();
                var settingsViewModel = ((SettingsViewModel)settings.DataContext);
            };
        }
    }
}
