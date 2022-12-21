using BenfordSet.Common;
using BenfordSet.ViewModel;
using log4net.Repository.Hierarchy;
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
        public MainWindow()
        {
            Mutex mutex = new Mutex(true,"benford-analyse",out bool aquiredNew);
            if(aquiredNew)
            {
                Timeline.DesiredFrameRateProperty.OverrideMetadata(typeof(Timeline),
                   new FrameworkPropertyMetadata { DefaultValue = 60 });
                MainWindowViewModel mainwindowviewmodel = new MainWindowViewModel();
                DataContext = mainwindowviewmodel;
                mainwindowviewmodel.OpenMessageBox = (title,text) =>
                {
                    return MessageBox.Show(title,text,MessageBoxButton.YesNo,MessageBoxImage.Question);
                };
                InitializeComponent();
            }
            else
            {
                MessageBox.Show("The app is already running.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            mutex.ReleaseMutex();
        }
    }
}
