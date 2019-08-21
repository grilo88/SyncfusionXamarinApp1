using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace SyncfusionXamarinApp1
{
    public partial class App : Application
    {
        public App()
        {
            string licenseKey = "MTM0MjQyQDMxMzcyZTMyMmUzMEdRQ01xc2ZVVTI2QVAwYzB0b1ZIV3QrTytBekkxd0YxY2d0bjd2MVd0Yzg9;MTM0MjQzQDMxMzcyZTMyMmUzMGZkNXFWaE9qb2pXdXpGMHhBZyt6dW9BK1drK2oxWEtDKzVLcE4xZmlrQ289;MTM0MjQ0QDMxMzcyZTMyMmUzMEUxTjFtZ3VuV1Bvd095d1ZQdFFma0xPaHVyZU1FVE5sWDNUdHZTdm5EbTg9";
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(licenseKey);

            InitializeComponent();

            MainPage = new NavigationPage(new MainPage())
            {
                BarBackgroundColor = Color.Blue,
                BarTextColor = Color.White
            };
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
