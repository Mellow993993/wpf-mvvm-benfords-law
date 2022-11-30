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
            Timeline.DesiredFrameRateProperty.OverrideMetadata(typeof(Timeline),
               new FrameworkPropertyMetadata { DefaultValue = 60 });
            InitializeComponent();
        }
    }
}
