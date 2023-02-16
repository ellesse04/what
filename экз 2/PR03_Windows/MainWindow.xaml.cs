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

namespace PR03_Windows
{
    public partial class MainWindow : Window
    {
        public string CorrectLogin = "1", CorrectPassword = "1";
        public int IncorrectAttemptsCount = 0;

        public MainWindow()
        {
            InitializeComponent();
            var timer = new DispatcherTimer
                (
                TimeSpan.FromMinutes(1),
                DispatcherPriority.SystemIdle,
                (s, e) => MessageBox.Show("Приложение не используется более минуты"),
                Application.Current.Dispatcher
                );
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void MoveBorder_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            if (UserLoginBox.Text != CorrectLogin || UserPasswordBox.Password != CorrectPassword)
            {
                LoginWarning.Opacity = 1;
                UserPasswordBox.BorderBrush = new SolidColorBrush(Color.FromRgb(232, 162, 175));
                UserLoginBox.BorderBrush = new SolidColorBrush(Color.FromRgb(232, 162, 175));
                IncorrectAttemptsCount++;
                if (IncorrectAttemptsCount == 3)
                {
                    LoginButton.IsEnabled = false;
                    MessageBox.Show("Совершено слишком много неверных попыток входа\n\nПовторите попытку через одну минуту");

                    DateTime now = DateTime.Now;
                    while (DateTime.Now.Subtract(now).Minutes < 1) { }
                    LoginButton.IsEnabled = true;
                    IncorrectAttemptsCount = 0;
                }
            }
            else
            {
                LoginWarning.Opacity = 0;
                UserPasswordBox.BorderBrush = new SolidColorBrush(Color.FromRgb(201, 203, 255));
                UserLoginBox.BorderBrush = new SolidColorBrush(Color.FromRgb(201, 203, 255));
                MessageBox.Show("Авторизация пройдена успешно");
                CreationWindow taskWindow = new CreationWindow();
                taskWindow.Show();
                Close();
            }
        }
    }
}
