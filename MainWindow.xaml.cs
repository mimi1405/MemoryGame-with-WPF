using System;
using System.Collections.Generic;
using System;
using System.Collections.Generic;
using System.IO;
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
using Path = System.IO.Path;
using System.Diagnostics;

namespace MemoryGame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {


        public MainWindow()
        {
            InitializeComponent();
            SetupGame();
        }

        private int matchedPairs = 0;
        private const int maxMatches = 8;
        private int moves = 0;
        private Random rnd = new Random();
        public bool IsMemoryImageVisible { get; set; }
        private Button firstButtonClicked = null;
        private Button secondButtonClicked = null;


        private void SetupGame()
        {
            moves = 0;
            matchedPairs = 0;

            List<String> paths = new()
        {
            "Csharp.png",
            "fsharp.png",
            "haskell.png",
            "java.png",
            "JS.png",
            "Python.png",
            "React.png",
            "ruby.png",
             "Csharp.png",
            "fsharp.png",
            "haskell.png",
            "java.png",
            "JS.png",
            "Python.png",
            "React.png",
            "ruby.png"
        };
            string imagePath = @"Images\";
            string iconPath = imagePath + "MemoryIcon.png";
            moveBtn.Content = "Moves: " + 0.ToString();
            matchedPairsBtn.Content = "Matched pairs: " + 0.ToString();

            foreach (Button btn in memoryGrid.Children.OfType<Button>())
            {
                StackPanel stackPanel = (StackPanel)btn.Content;
                Image memoryIcon = (Image)stackPanel.Children[0];
                Uri Iconuri = new Uri(iconPath, UriKind.RelativeOrAbsolute);
                BitmapImage iconBitmap = new BitmapImage(Iconuri);
                memoryicon.Visibility = Visibility.Visible;
                memoryIcon.Source = iconBitmap;

                int randomIndex = rnd.Next(0, paths.Count);
                string nextPath = imagePath + paths[randomIndex];
                paths.RemoveAt(randomIndex);
                Image languageIcon = (Image)stackPanel.Children[1];
                languageIcon.Visibility = Visibility.Collapsed;
                Uri languageUri = new Uri(nextPath, UriKind.RelativeOrAbsolute);
                BitmapImage languagesBitmap = new BitmapImage(languageUri);
                languageIcon.Source = languagesBitmap;
            }


        }
        private void HideImage(Image img)
        {
            img.Visibility = Visibility.Collapsed;
        }
        private void ShowImage(Image img)
        {
            img.Visibility = Visibility.Visible;
        }

        private void BtnResetGame_Click(object sender, RoutedEventArgs e)
        {
            SetupGame();
        }

        private bool ImagesMatch(Image image1, Image image2)
        {
            string firstPath = image1.Source.ToString();
            string secondPath = image2.Source.ToString();
            if (firstPath == secondPath)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            moves++;
            moveBtn.Content = "Moves: " + moves.ToString();
            Button clickedButton = sender as Button;
            StackPanel stackPanel = (StackPanel)clickedButton.Content;
            Image iconImage = (Image)stackPanel.Children[0];
            iconImage.Visibility = Visibility.Collapsed;
            Image languageImage = (Image)stackPanel.Children[1];
            languageImage.Visibility = Visibility.Visible;

            if (firstButtonClicked == null)
            {
                firstButtonClicked = clickedButton;
            }
            else if (secondButtonClicked == null)
            {
                secondButtonClicked = clickedButton;

                StackPanel firstStackPanel = (StackPanel)firstButtonClicked.Content;
                Image firstLanguage = (Image)firstStackPanel.Children[1];
                StackPanel secondStackPanel = (StackPanel)secondButtonClicked.Content;
                Image secondLanguage = (Image)secondStackPanel.Children[1];

                if (ImagesMatch(firstLanguage, secondLanguage))
                {
                    matchedPairs++;
                    matchedPairsBtn.Content = "Matched pairs: " + matchedPairs.ToString();

                    if (matchedPairs == maxMatches)
                    {
                        MessageBox.Show("You win!");
                        Application.Current.Shutdown();
                    }

                    firstButtonClicked = null;
                    secondButtonClicked = null;
                }
                else
                {
                    await Task.Delay(1000);

                    Image firstIconImage = (Image)firstStackPanel.Children[0];
                    Image secondIconImage = (Image)secondStackPanel.Children[0];
                    firstLanguage.Visibility = Visibility.Collapsed;
                    firstIconImage.Visibility = Visibility.Visible;
                    secondLanguage.Visibility = Visibility.Collapsed;
                    secondIconImage.Visibility = Visibility.Visible;

                    firstButtonClicked = null;
                    secondButtonClicked = null;
                }
            }

        }

        private void BtnEndGame_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
