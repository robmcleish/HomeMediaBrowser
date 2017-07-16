using System.Collections.Generic;

using Xamarin.Forms;

namespace HomeMediaBrowser
{
    public partial class App : Application
    {
        public static bool UseMockDataStore = false;
        public static string BackendUrl = "https://localhost:49841";

        public static IDictionary<string, string> LoginParameters => null;

        public App()
        {
            InitializeComponent();

            if (UseMockDataStore)
                DependencyService.Register<MockDataStore>();
            else
                DependencyService.Register<CloudDataStore>();

            SetMainPage();
        }

        public static void SetMainPage()
        {
            if (!UseMockDataStore && !Settings.IsLoggedIn)
            {
                Current.MainPage = new NavigationPage(new LoginPage())
                {
                    BarBackgroundColor = (Color)Current.Resources["Primary"],
                    BarTextColor = Color.White
                };
            }
            else
            {
                GoToMainPage();
            }
        }

        public static void GoToMainPage()
        {
            Current.MainPage = new TabbedPage
            {
                Children = {
                    new NavigationPage(new ItemsPage())
                    {
                        Title = "Browse",
#pragma warning disable CS0618 // Type or member is obsolete
                        Icon = Device.OnPlatform("tab_feed.png", null, null)
#pragma warning restore CS0618 // Type or member is obsolete
                    },
                    new NavigationPage(new AboutPage())
                    {
                        Title = "About",
#pragma warning disable CS0618 // Type or member is obsolete
                        Icon = Device.OnPlatform("tab_about.png", null, null)
#pragma warning restore CS0618 // Type or member is obsolete
                    },
                }
            };
        }
    }
}
