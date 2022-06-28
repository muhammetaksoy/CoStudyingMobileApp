using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace YeatyAppMobile.Views.MainTabbedPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ClubsPage : ContentPage
    {
        public ClubsPage()
        {
            InitializeComponent();
            this.Title = "Kulüpler";
        }
    }
}