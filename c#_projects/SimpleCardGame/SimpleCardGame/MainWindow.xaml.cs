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
using System.Windows.Threading;


namespace SimpleCardGame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string[] arr = { "a1", "a2", "trump" };
        int timerCount = 0;
        int score = 0;
        Brush statusTxtColor;
        DispatcherTimer timer = new DispatcherTimer();

        public MainWindow()
        {
            InitializeComponent();
            enableCard(false);
            statusTxt.Text = "";
            timer.Tick += Timer_Tick;
            timer.Interval = new TimeSpan(0, 0, 0, 0, 100);
            qLbl1.Visibility = qLbl2.Visibility = qLbl3.Visibility = Visibility.Collapsed;
            card_0.Source = card_1.Source = card_2.Source = getImage("back");
        }

        // Timer tick
        private void Timer_Tick(object sender, EventArgs e)
        {
            setCard();
        }
        // Place card
        private void setCard()
        {
            timerCount++;
            Shuffle(arr);
            if (timerCount < 8)
            {
                openCard();
            }
            else
            {
                endShuffle();
            }
        }

        //Get image
        private BitmapImage getImage(string img)
        {
            BitmapImage image = new BitmapImage();
            image.BeginInit();
            image.UriSource = new Uri("assets/" + img + ".png", UriKind.Relative);
            image.EndInit();
            return image;
        }
        // End shuffle
        private void endShuffle()
        {
            card_0.Source = card_1.Source = card_2.Source = getImage("back");
            qLbl1.Visibility = qLbl2.Visibility = qLbl3.Visibility = Visibility.Visible;
            statusTxt.Text = "Where is Trump?";
            timer.Stop();
            timerCount = 0;
            startBtn.IsEnabled = true;
            enableCard(true);
        }

        //Shufle array
        private void Shuffle(string[] array)
        {
            Random random = new Random();
            int n = array.Length;
            for (int i = 0; i < n; i++)
            {
                int r = i + random.Next(n - i);
                string t = array[r];
                array[r] = array[i];
                array[i] = t;
            }
            arr = array;
        }
        //Click card event
        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            openCard();
            enableCard(false);
            Image card = (Image)sender;
            char[] splitChar = { '_' };
            string[] numberString = card.Name.Split(splitChar);
            int cardNumber = int.Parse(numberString[1]);
            checkResult(cardNumber);
        }

        // Check result
        private void checkResult(int number)
        {
            string result = "";

            if (arr[number] == "trump")
            {
                statusTxtColor = Brushes.GreenYellow;
                result = "YES ... Success";
                score += 10;
            }
            else
            {
                statusTxtColor = Brushes.Red;
                result = "False ... Sorry";
                score -= 5;
                if (score <= 0) { score = 0; }
            }
            statusTxt.Foreground = statusTxtColor;
            statusTxt.Text = result;
            scoreTxt.Text = "Score: " + score.ToString();
        }

        private void startBtn_Click(object sender, RoutedEventArgs e)
        {
            timer.Start();
            statusTxtColor = Brushes.White;
            statusTxt.Foreground = statusTxtColor;
            statusTxt.Text = "Wait";
            enableCard(false);
            startBtn.IsEnabled = false;
            qLbl1.Visibility = qLbl2.Visibility = qLbl3.Visibility = Visibility.Collapsed;
        }

        // Enable card click
        private void enableCard(bool enable)
        {
            card_0.IsEnabled = card_1.IsEnabled = card_2.IsEnabled = enable;
        }


        //Open card
        private void openCard()
        {
            card_0.Source = getImage(arr[0]);
            card_1.Source = getImage(arr[1]);
            card_2.Source = getImage(arr[2]);
        }
        // Close card
        private void closeCard()
        {
            card_0.Source = card_1.Source = card_2.Source = getImage("back");
        }

    }
}
