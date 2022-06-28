using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using YeatyAppMobile.Models.AppConstants;
using YeatyAppMobile.Models.ResponseModels;

namespace YeatyAppMobile.ViewCells.CodeBased
{
    public class DistrictCardViewCell
    {
        public DistrictCardViewCell(SearchIndexDistrictsResponseModel _model)
        {
            responseModel = _model;
            mainFrame = CreateFrame();
        }

        public SearchIndexDistrictsResponseModel responseModel { get; set; }

        private Frame CreateFrame()
        {
            Frame mainFrame = new Frame()
            {
                CornerRadius = 8,
                HeightRequest = 120,
                WidthRequest = 160,
                HasShadow = false,
                Padding = 0,
                Margin = new Thickness(6, 0, 0, 0),
                BackgroundColor = Color.Transparent,
                BorderColor = CustomColors.defaultShadedWhiteColor
            };
            Grid mainGrid = new Grid();
            mainGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1.5, GridUnitType.Star) });
            mainGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) });
            Image bgImage = new Image()
            {
                Source = responseModel.PhotoPath,
                Aspect = Aspect.Fill,

            };
            Frame frameShadow = new Frame()
            {
                BackgroundColor = CustomColors.defaultBlackColor,
                Opacity = 0.25
            };
            StackLayout stackDetails = new StackLayout()
            {
                HorizontalOptions = LayoutOptions.StartAndExpand,
                VerticalOptions = LayoutOptions.EndAndExpand,
                Spacing = 0,
                Margin = new Thickness(8, 0, 6, 3)
            };
            Label lblRestaurantCount = new Label()
            {
                TextColor = CustomColors.defaultWhiteColor,
                FontSize = 10,
                FontFamily = "Custommedium",
                Margin = 0,
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Start,
                Text = responseModel.RestaurantCount.ToString() + " mekanlar"
            };
            Label lblRestaurantName = new Label()
            {
                TextColor = CustomColors.defaultWhiteColor,
                FontFamily = "CustomBold",
                Margin = 0,
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Start,
                Text = responseModel.DistrictName
            };
            if (responseModel.DistrictName.Length > 12)
                lblRestaurantName.FontSize = 15;
            else
                lblRestaurantName.FontSize = 18;
            stackDetails.Children.Add(lblRestaurantCount);
            stackDetails.Children.Add(lblRestaurantName);
            mainGrid.Children.Add(bgImage, 0, 0);
            mainGrid.Children.Add(frameShadow, 0, 0);         
            Grid.SetRowSpan(bgImage, 2);
            Grid.SetRowSpan(frameShadow, 2);
            mainGrid.Children.Add(stackDetails, 0, 1);
            mainFrame.Content = mainGrid;
            mainFrame.BindingContext = responseModel;
            return mainFrame;
        }

        public Frame mainFrame { get; set; }


    }
}
