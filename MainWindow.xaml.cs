using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace WPFAsyncChat
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        bool isBotActive;
        const string RemovePattern = @"\W+";

        public MainWindow()
        {
            InitializeComponent();
        }

        private void SendMessageBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(MyCurrentMessageText.Text))
            {
                MyLastMessageText.Text = MyCurrentMessageText.Text;
                MyCurrentMessageText.Text = string.Empty;
                OnUserSendMessage(MyLastMessageText.Text);
            }
            else
            {
                MessageBox.Show("Пустое сообщение!");
            }
        }

        private async void OnUserSendMessage(string userMessage)
        {
            if (userMessage == "/start")
            {
                if (!isBotActive)
                {
                    isBotActive = true;
                    BotLastMessageText.Text = "Бот активирован!";
                }
            }
            else if (isBotActive)
            {
                BotLastMessageText.Text = "Бот думает...";
                BotLastMessageText.Text = await GetBotAnswer(userMessage);
            }
        }

        private async Task<string> GetBotAnswer(string userMessage)
        {
            await Task.Delay(1000);
            userMessage = ProccesMessage(userMessage);
            switch (userMessage)
            {
                case "привет":
                    return "Как дела?";
                case "как дела":
                    return "У меня все хорошо";
                case "нормально":
                    return "Рад что у вас все в порядке";
                case "пока":
                    return "Пока! Буду ждать нашего следующего разговора";
                default:
                    return "Я не знаю как на это ответить";
            }
        }

        private string ProccesMessage(string message)
        {
            return Regex.Replace(message, RemovePattern, "").ToLower();
        }
    }
}