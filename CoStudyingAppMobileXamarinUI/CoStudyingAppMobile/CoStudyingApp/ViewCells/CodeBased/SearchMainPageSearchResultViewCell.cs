using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using YeatyAppMobile.BusinessModels;
using YeatyAppMobile.Models.AppConstants;
using YeatyAppMobile.Models.ViewModels;

namespace YeatyAppMobile.ViewCells.CodeBased
{
    public class SearchMainPageSearchResultViewCell
    {


        public SearchMainPageSearchResultViewCell(SearchPageIndexResultViewModel _vm)
        {
            vm = _vm;
            mainGrid = CreateViewCell();

        }


        private StackLayout CreateViewCell()
        {
            StackLayout mainStack = new StackLayout()
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Orientation = StackOrientation.Horizontal,
                Spacing = 10,
                Margin = new Thickness(0, 18, 0, 0)
            };
            Frame iconFrame = new Frame()
            {
                HeightRequest = 42,
                WidthRequest = 42,
                CornerRadius = 8,
                IsClippedToBounds = true,
                Padding = 0,
                BackgroundColor = CustomColors.defaultShadedWhiteColor
            };
            if (vm.isImage)
            {
                Image iconImage = new Image()
                {
                    Source = vm.iconImage,
                    HeightRequest = 42,
                    WidthRequest = 42,
                    Aspect = Aspect.AspectFill
                };
                iconFrame.Content = iconImage;
            }
            else
            {
                Label iconLabel = new Label()
                {
                    FontFamily = GetFontFamilyIcons.FontAwesomeLight,
                    TextColor = CustomColors.defaultFoggyColor,
                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                    FontSize = 20,
                    Text = vm.iconImage,
                    VerticalOptions = LayoutOptions.CenterAndExpand,
                    Margin = 0
                };
                iconFrame.Content = iconLabel;
            }
            Label labelText = new Label()
            {
                Text = vm.DefinitionString,
                FontFamily = "CustomMedium",
                FontSize = 14,
                VerticalOptions = LayoutOptions.Center,
                TextColor = CustomColors.defaultFoggyColor
            };
            mainStack.Children.Add(iconFrame);
            mainStack.Children.Add(labelText);
            mainStack.BindingContext = vm;
            return mainStack;
        }


        public SearchPageIndexResultViewModel vm { get; set; }
        public StackLayout mainGrid { get; set; }





    }
}
