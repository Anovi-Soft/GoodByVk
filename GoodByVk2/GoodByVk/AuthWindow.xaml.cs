using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace GoodByVk
{
    /// <summary>
    /// Interaction logic for AuthWindow.xaml
    /// </summary>
    public partial class AuthWindow : Window
    {
        private Status status;
        private string result;
        private HashSet<string> keys = new HashSet<string>
        {
            "access_token",
            "error"
        };
        private AuthWindow()
        {
            InitializeComponent();
        }

        public static async Task<string> Auth(int id, int scope)
        {
            var window = new AuthWindow { status = Status.Work };
            window.Show();
            var uri =
                "https://oauth.vk.com/authorize?client_id=" + id +
                "&scope=" + scope +
                @"&redirect_uri=https://oauth.vk.com/blank.html&display=popup&v=5.52&response_type=token&revoke=0";
            window.Browser.Navigate(new Uri(uri));
            await Task.Run(() =>
            {
                while (window.status == Status.Work)
                    Thread.Sleep(100);
            });
            window.Close();
            if (window.status != Status.Sucess)
                throw new Exception(window.result);
            return window.result;
        }

        private void Browser_LoadCompleted(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            if (keys.All(x => e.Uri.ToString().IndexOf(x, StringComparison.Ordinal) == -1)) return;
            var pattern = @"(?<name>[\w\d\x5f]+)=(?<value>[^\x26\s]+)";
            var regex = new Regex(pattern, RegexOptions.IgnoreCase | RegexOptions.Singleline);
            var match = regex.Matches(e.Uri.ToString())
                .Cast<Match>()
                .First(x => keys.Contains(x.Groups["name"].Value));
            result = match.Groups["value"].Value;
            status = match.Groups["name"].Value == "access_token"
                ? Status.Sucess
                : Status.Error;
        }
        private enum Status
        {
            Work,
            Sucess,
            Error
        }
    }
}
