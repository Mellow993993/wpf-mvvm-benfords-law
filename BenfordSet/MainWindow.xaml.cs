using BenfordSet.ViewModel;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BenfordSet
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            Timeline.DesiredFrameRateProperty.OverrideMetadata(typeof(Timeline),
               new FrameworkPropertyMetadata { DefaultValue = 60 });
            InitializeComponent();
            ((MainWindowViewModel)DataContext).InProgress += OpenProgressWindow;
        }

        public void OpenProgressWindow(object sender, EventArgs e)
        {
            Progressbar dw = new Progressbar();
            dw.ShowDialog();
        }

    }
}
