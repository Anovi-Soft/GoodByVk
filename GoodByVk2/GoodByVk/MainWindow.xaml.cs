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
using GoodByVk.Api;

namespace GoodByVk
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private VkApi api;
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void Window_Initialized(object sender, EventArgs e)
        {
            Hide();
            var key = "";
            try
            {
                key = await AuthWindow.Auth(5531929, 4098);
            }
            catch(Exception ex){
                MessageBox.Show($"Auth error:{ex.Message}");
                Close();
            }
            api = new VkApi(key);
            BotGoodBy.GoodBy(api);
            //Show();
        }
    }
}
