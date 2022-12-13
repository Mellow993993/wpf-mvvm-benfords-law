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
                Messages meassages = new Messages();
                //Log.Error("Start of Debuggin");

                MainWindowViewModel mainwindowviewmodel = new MainWindowViewModel();

                DataContext = mainwindowviewmodel;

                InitializeComponent();
            }
            else
                MessageBox.Show("App läuft bereits");
            mutex.ReleaseMutex();
        }
    }
}
