using Lottie.Forms;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace YeatyAppMobile.BusinessModels
{
    public class LoadingState
    {

        public static async Task StartLoading(View mainStack, View stackLoading, AnimationView lottieLoading)
        {
            mainStack.IsVisible = false;
            lottieLoading.PlayAnimation();
            stackLoading.IsVisible = true;          
        }

        public static async Task StopLoading(View mainStack, View stackLoading, AnimationView lottieLoading)
        {
            await stackLoading.FadeTo(0, 100);
            stackLoading.IsVisible = false;
            stackLoading.Opacity = 1;
            mainStack.IsVisible = true;
        }





    }
}
