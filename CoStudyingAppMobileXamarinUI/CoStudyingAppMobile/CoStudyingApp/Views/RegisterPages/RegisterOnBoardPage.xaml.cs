using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace YeatyAppMobile.Views.RegisterPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterOnBoardPage : ContentPage
    {



        public RegisterOnBoardPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);

        }

        private async void tapBack_Tapped(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}