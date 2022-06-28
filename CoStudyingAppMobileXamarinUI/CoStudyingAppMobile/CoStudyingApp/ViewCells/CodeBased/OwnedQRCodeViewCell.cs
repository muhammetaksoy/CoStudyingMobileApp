using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using YeatyAppMobile.BusinessModels;
using YeatyAppMobile.Models.AppConstants;
using YeatyAppMobile.Models.ViewModels;

namespace YeatyAppMobile.ViewCells.CodeBased
{
    public class OwnedQRCodeViewCell
    {

        public OwnedQRCodeViewCell(OwnedQRCodeViewModel _vm)
        {
            vm = _vm;
            mainGrid = CreateViewCell();

        }
        

        private Grid CreateViewCell()
        {

            Grid mainGrid = new Grid();
            Frame frameImage = new Frame()
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                HeightRequest = 180,
                CornerRadius = 3,
                Padding = 0,
                IsClippedToBounds = true,
                BackgroundColor = CustomColors.defaultShadedWhiteColor
            };
            Image img = new Image()
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Aspect = Aspect.AspectFill,
                Source = vm.PhotoUrl
            };
            frameImage.Content = img;
            Frame frameShadow = new Frame()
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                HeightRequest = 180,
                CornerRadius = 3,
                Padding = 0,
                Opacity = 0.7,
                IsClippedToBounds = true,
                BackgroundColor = CustomColors.defaultBlackColor
            };
            Frame frameQRSymbol = new Frame()
            {
                Padding = new Thickness(10, 2, 10, 2),
                HorizontalOptions = LayoutOptions.Center,
                CornerRadius = 3,
                Opacity = 0.12,
                VerticalOptions = LayoutOptions.Center,
                BackgroundColor = Color.Transparent,
                BorderColor = CustomColors.defaultShadedWhiteColor
            };
            Label qrLabel = new Label()
            {
                Text = "qrcode",
                FontSize = 120,
                FontFamily = GetFontFamilyIcons.FontAwesomeSolid,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                TextColor = CustomColors.defaultShadedWhiteColor
            };
            frameQRSymbol.Content = qrLabel;
            StackLayout stackLocation = new StackLayout()
            {
                Padding = 0,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                HeightRequest = 180,
                Spacing = 0,
                BackgroundColor = Color.Transparent
            };
            StackLayout insideStack = new StackLayout()
            {
                HorizontalOptions = LayoutOptions.StartAndExpand,
                VerticalOptions = LayoutOptions.Start,
                Orientation = StackOrientation.Horizontal,
                Margin = new Thickness(6, 6, 6, 0)
            };
            Label locationIcon = new Label()
            {
                Text = "map-marker-alt",
                VerticalOptions = LayoutOptions.Center,
                FontSize = 18,
                FontFamily = GetFontFamilyIcons.FontAwesomeLight,
                TextColor = CustomColors.defaultWhiteColor
            };
            Label locationRestName = new Label()
            {
                Text = vm.RestaurantName,
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                FontSize = 10,
                Margin = 0,
                FontFamily = "CustomMedium",
                TextColor = CustomColors.defaultWhiteColor
            };
            insideStack.Children.Add(locationIcon);
            insideStack.Children.Add(locationRestName);
            stackLocation.Children.Add(insideStack);
            StackLayout stackDetails = new StackLayout()
            {
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                Margin = new Thickness(12, 0, 12, 0),
                Spacing = 0,
            };
            Label lblTitle = new Label()
            {
                Text = vm.Title,
                VerticalOptions = LayoutOptions.Center,
                FontSize = vm.TitleFontSize,
                Margin = 0,
                FontFamily = "CustomBold",
                HorizontalTextAlignment = TextAlignment.Center,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                TextColor = CustomColors.defaultWhiteColor,
            };
            Label lblSubText = new Label()
            {
                Text = vm.SubText,
                VerticalOptions = LayoutOptions.Center,
                FontSize = 8,
                Margin = 0,
                FontFamily = "CustomMedium",
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                TextColor = CustomColors.defaultWhiteColor,
            };
            stackDetails.Children.Add(lblTitle);
            stackDetails.Children.Add(lblSubText);
            mainGrid.Children.Add(frameImage);
            mainGrid.Children.Add(frameShadow);
            mainGrid.Children.Add(frameQRSymbol);
            mainGrid.Children.Add(stackLocation);
            mainGrid.Children.Add(stackDetails);
            mainGrid.BindingContext = vm;
            return mainGrid;

        }


        public OwnedQRCodeViewModel vm { get; set; }
        public Grid mainGrid { get; set; }

       
    }
}
