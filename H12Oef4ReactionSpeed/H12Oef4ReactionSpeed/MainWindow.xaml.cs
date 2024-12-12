using Microsoft.VisualBasic;
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
using System.Windows.Threading;

namespace H12Oef4ReactionSpeed
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();   
        }

        DispatcherTimer timer = new DispatcherTimer();
        DispatcherTimer dateTimeTimer = new DispatcherTimer();
        DateTime timeClicked;
        DateTime imageVisible;
        Random random = new Random();
        int randomImage = -1;
        bool isMonsterFirstLoaded = false;
        bool isMonsterLoaded = false;
        int points;
        int index = 0;

        string[,] scores = new string[5, 2];

        string player;

        private void startButton_Click(object sender, RoutedEventArgs e)
        {
            timer.Interval = new TimeSpan(0, 0, 0, 0, 250);
            timer.Tick += Timer_Tick;
            timer.Start();

            imageButton.IsEnabled = true;
        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            randomImage = random.Next(0, 4);

            switch (randomImage)
            {
                case 0:
                    image.Source = new BitmapImage(new Uri(@"Images/baby.png", UriKind.Relative));
                    isMonsterLoaded = false;
                    break;

                case 1:
                    image.Source = new BitmapImage(new Uri(@"Images/cat.png", UriKind.Relative));
                    isMonsterLoaded = false;
                    break;

                case 2:
                    image.Source = new BitmapImage(new Uri(@"Images/dog.png", UriKind.Relative));
                    isMonsterLoaded = false;
                    break;

                case 3:
                    image.Source = new BitmapImage(new Uri(@"Images/click_monster.png", UriKind.Relative));
                    isMonsterLoaded = true;                  

                    if (isMonsterFirstLoaded == false)
                    {
                        imageVisible = DateTime.Now;
                        isMonsterFirstLoaded = true;
                    }
                    break;
            }
        }

        private void imageButton_Click(object sender, RoutedEventArgs e)
        {
            if (isMonsterLoaded == false)
            {
                points += 5;
            }
            else if (isMonsterLoaded == true)
            {                
                timer.Stop();

                isMonsterFirstLoaded = false;
                
                timeClicked = DateTime.Now;
                TimeSpan timeDifference = timeClicked - imageVisible;
                points += (int)timeDifference.TotalSeconds;
                player = Interaction.InputBox("", "U heeft het monster gevonden. Enter uw naam om uw score te bewaren");

                scores[index, 0] = player;
                scores[index, 1] = points.ToString(); 
               
                resultLabel.Content += $"{scores[index, 0]} : {scores[index, 1]} seconds \n";
                
                index++;
                points = 0;

                if (index <= 4)
                {
                    startButton.IsEnabled = true;
                    imageButton.IsEnabled = true;
                }
                else
                {
                    startButton.IsEnabled = false;
                    imageButton.IsEnabled = false;
                }

                imageButton.IsEnabled = false;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            dateTimeTimer.Interval = new TimeSpan(0, 0, 1);
            dateTimeTimer.Tick += DateTimeTimer_Tick;
            dateTimeTimer.Start();
        }

        private void DateTimeTimer_Tick(object? sender, EventArgs e)
        {
            dateTimeLabel.Content = DateTime.Now.ToString("dddd dd MMMM yyyy HH:mm:ss");
        }
    }
}