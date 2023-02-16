using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Shapes;

namespace PR03_Windows
{
    public partial class CreationWindow : Window
    {
        public CreationWindow()
        {
            InitializeComponent();
        }

        public void FileWrite(object sender, RoutedEventArgs e)
        {

            string id = TextBoxId.Text;
            string firstname = TextBoxImya.Text;
            string secondname = TextBoxFamiliya.Text;
            string thirdname = TextBoxOtchestvo.Text;
            string passport = TextBoxPassport.Text;
            string phone = TextBoxPhone.Text;
            string email = TextBoxEmail.Text;

            if (check_id(id) && check_firstname(firstname) && check_secondname(secondname) && check_thirdname(thirdname) && 
                check_passport(passport) && check_phone(phone) && check_email(email))
            {
                string res = id + "\t" + firstname + "\t" + secondname + "\t" + thirdname + "\t" + passport + "\t" + phone + "\t" + email;
                using (StreamWriter writer = File.AppendText("employee.txt"))
                {
                    writer.WriteLine(res);
                    writer.Close();
                }
                MessageBox.Show("Сотрудник успешно зарегистрирован");
            }
            else
            {
                MessageBox.Show("Ошибка.");
            }

        }

        public void AppClose(object sender, RoutedEventArgs e)
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

        private void BackAuthorizationButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow taskWindow = new MainWindow();
            taskWindow.Show();
            Close();
        }

        private bool check_id(string value)
        {
            List<string> UsedId = new List<string>();
            using (StreamReader reader = new StreamReader("employee.txt"))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    UsedId.Add(line.Substring(0, line.IndexOf("\t")));
                }
            }

            for (int i = 0; i < UsedId.Count; i++)
            {
                if (UsedId[i] == value)
                    return false;
                    //throw new ArgumentException("Данный идентификатор занят");
            }

            foreach (char a in value)
            {
                if (char.IsLetter(a))
                    return false;
                    //throw new ArgumentException("Строка содержит не только цифры");
            }
            return true;
        }

        private bool check_secondname(string value)
        {
            if (string.IsNullOrEmpty(value))
                return false;
            else if (!char.IsUpper(value[0]))
                return false;
            else if (!Regex.IsMatch(value, "^[А-Яа-я]+$"))
                return false;
            else
            {
                foreach (char a in value)
                {
                    if (!char.IsLetter(a))
                        return false;       
                }
            }
            return true;
        }

        private bool check_firstname(string value)
        {
            if (string.IsNullOrEmpty(value))
                return false;
            else if (!char.IsUpper(value[0]))
                return false;
            else if (!Regex.IsMatch(value, "^[А-Яа-я]+$"))
                return false;
            else
            {
                foreach (char a in value)
                {
                    if (!char.IsLetter(a))
                        return false;
                }
            }
            return true;
        }

        private bool check_thirdname(string value)
        {
            if (string.IsNullOrEmpty(value))
                return false;
            else if (!char.IsUpper(value[0]))
                return false;
            else if (!Regex.IsMatch(value, "^[А-Яа-я]+$"))
                return false;
            else
            {
                foreach (char a in value)
                {
                    if (!char.IsLetter(a))
                        return false;
                }
            }
            return true;
        }
        
        private bool check_passport(string value)
        {
            return Regex.IsMatch(value, @"^[0-9]{10}$");
        }

        private bool check_phone(string value)
        {
            return Regex.IsMatch(value, @"^[+]7[0-9]{10}$|^8[0-9]{10}$");
        }
    
        private bool check_email(string value)
        {
            if (!Regex.IsMatch(value, @"^\D\w+[@]firma[.]ru$"))
                return false;
            else if (!char.IsLetter(value[0]))
                return false;
            else
                foreach (char a in value)
                {
                    if (Regex.IsMatch(a.ToString(), "^[А-Яа-я]+$"))
                        return false;
                }
            return true;
        }
    }
}
