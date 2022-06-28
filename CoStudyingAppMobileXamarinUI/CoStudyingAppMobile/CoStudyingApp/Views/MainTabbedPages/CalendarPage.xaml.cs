using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using YeatyAppMobile.Models.ResponseModels;
using YeatyAppMobile.ServiceController;

namespace YeatyAppMobile.Views.MainTabbedPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CalendarPage : ContentPage
    {
        ExploreServices exploreServices = new ExploreServices();
        class CalendarViewModel
        {

           


        }
        public List<calendarItem> calendarItems = new List<calendarItem>();
        public List<calendarHeaderItem> calendarHeaderItems = new List<calendarHeaderItem>() {
            new calendarHeaderItem()
            {
                Month = 6,Year = 2022
            },
            new calendarHeaderItem()
            {
                Month = 7,Year = 2022
            },
            new calendarHeaderItem()
            {
                Month = 8,Year = 2022
            },
            new calendarHeaderItem()
            {
                Month = 9,Year = 2022
            },
            new calendarHeaderItem()
            {
                Month = 10,Year = 2022
            },
            new calendarHeaderItem()
            {
                Month = 11,Year = 2022
            },
            new calendarHeaderItem()
            {
                Month = 12,Year = 2022
            },
            new calendarHeaderItem()
            {
                Month = 1,Year = 2023
            },
            new calendarHeaderItem()
            {
                Month = 2,Year = 2023
            },
            new calendarHeaderItem()
            {
                Month = 3,Year = 2023
            },
            new calendarHeaderItem()
            {
                Month = 4,Year = 2023
            },
            new calendarHeaderItem()
            {
                Month = 5,Year = 2023
            },
            new calendarHeaderItem()
            {
                Month = 6,Year = 2023
            }
        };
        calendarHeaderItem selectedHeaderItem = new calendarHeaderItem();
        List<string> calendarHeaderItemsStr = new List<string>();

        public class calendarHeaderItem
        {
            public int Month { get; set; }

            public int Year { get; set; }

            public string CalendarTitle
            {           
                get
                {
                    string monthStr = "";
                    if (Month == 1)
                        monthStr = "Ocak";
                    if (Month == 2)
                        monthStr = "Şubat";
                    if (Month == 3)
                        monthStr = "Mart";
                    if (Month == 4)
                        monthStr = "Nisan";
                    if (Month == 5)
                        monthStr = "Mayıs";
                    if (Month == 6)
                        monthStr = "Haziran";
                    if (Month == 7)
                        monthStr = "Temmuz";
                    if (Month == 8)
                        monthStr = "Ağustos";
                    if (Month == 9)
                        monthStr = "Eylül";
                    if (Month == 10)
                        monthStr = "Ekim";
                    if (Month == 11)
                        monthStr = "Kasım";
                    if (Month == 12)
                        monthStr = "Aralık";

                    return monthStr + ", " + Year.ToString();
                }
            }

            
        }

        public class calendarItem
        {
            public DateTime date { get; set; }

            public string dayStr
            {
                get
                {
                    if (date.Day < 10)
                    {
                        return "0" + date.Day.ToString();
                    }
                    else
                    {
                        return date.Day.ToString();
                    }
                }
            }

            public bool hasDeadLine
            {
                get
                {
                    if (tasks == null || tasks.Count == 0)
                        return false;
                    else
                        return true;
                }
            }
            public string countDeadLine
            {
                get
                {
                    if (tasks == null || tasks.Count == 0)
                        return "";
                    else
                        return tasks.Count + " adet görevde son gün";
                }
            }

            public string WeekdayStr
            {
                get
                {

                    if ((int)date.DayOfWeek == 0)
                        return "pazar";
                    else if ((int)date.DayOfWeek == 1)
                        return "pazartesi";
                    else if ((int)date.DayOfWeek == 2)
                        return "salı";
                    else if ((int)date.DayOfWeek == 3)
                        return "çarşamba";
                    else if ((int)date.DayOfWeek == 4)
                        return "perşembe";
                    else if ((int)date.DayOfWeek == 5)
                        return "cuma";
                    else
                        return "cumartesi";


                }
            }

            public  List<TaskResponseModel> tasks { get; set; }
        }
        List<TaskResponseModel> responseModels = new List<TaskResponseModel>();
        public CalendarPage()
        {
            InitializeComponent();
            this.Title = "Takvim";

            StartPage();

            
        }

        private async void StartPage()
        {
            responseModels = await exploreServices.GetMyTasks();
            for (DateTime date = DateTime.Now; date < DateTime.Now.AddYears(1); date = date.AddDays(1.0))
            {
                List<TaskResponseModel> taskkks = new List<TaskResponseModel>();
                int headCounter = 0;
                if (responseModels != null)
                {
                    if (responseModels.Count > 0)
                    {
                        taskkks = responseModels.Where(x => x.deadline.Day == date.Day && x.deadline.Month == date.Month && x.deadline.Year == date.Year).ToList();
                    }
                }
                calendarItem newItem = new calendarItem()
                {
                    date = date
                };
                if (taskkks != null)
                    newItem.tasks = taskkks;

                calendarItems.Add(newItem);
            }


            calendarHeaderItemsStr = calendarHeaderItems.Select(x => x.CalendarTitle).ToList();
            pickerMonthHeader.ItemsSource = calendarHeaderItemsStr;
            pickerMonthHeader.SelectedIndex = 0;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
        }

        private async void pickerMonthHeader_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            listViewDays.BindingContext = null;
            listViewDays.ItemsSource = null;
            Picker picker = sender as Picker;
            string headerItemStr = picker.SelectedItem as string;
            calendarHeaderItem selectedHeaderItem = calendarHeaderItems.Where(x => x.CalendarTitle == headerItemStr).FirstOrDefault();
            List<calendarItem> calendarItemsSelected = calendarItems.Where(x => x.date.Month == selectedHeaderItem.Month && x.date.Year == selectedHeaderItem.Year).
                OrderBy(x => x.date.Day).ToList();
            listViewDays.BindingContext = calendarItemsSelected;
            listViewDays.ItemsSource = calendarItemsSelected;
            
        }

        private async void listViewDays_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CollectionView colView = sender as CollectionView;
            calendarItem calendarItem = colView.SelectedItem as calendarItem;
            string selection = "";
            if (calendarItem.hasDeadLine)
            {
                selection = await  DisplayActionSheet("İşlem seçiniz", "Vazgeç", "Tamam", new string[] { "Görevleri görüntüle", "Yeni görev ekle" });
            }
            else
            {
                selection = await DisplayActionSheet("İşlem seçiniz", "Vazgeç", "Tamam", new string[] { "Yeni görev ekle" });
            }
            

        }

        private  async void tapSelectDay_Tapped(object sender, EventArgs e)
        {
            Frame senderFrame = sender as Frame;
            calendarItem calendarItem = senderFrame.BindingContext as calendarItem;
            string selection = "";
            if (calendarItem.hasDeadLine)
            {
                selection = await DisplayActionSheet("İşlem seçiniz", "Vazgeç", "Tamam", new string[] { "Görevleri görüntüle", "Yeni görev ekle" });
            }
            else
            {
                selection = await DisplayActionSheet("İşlem seçiniz", "Vazgeç", "Tamam", new string[] { "Yeni görev ekle" });
            }
            if (selection == "Yeni görev ekle")
            {
                await Navigation.PushAsync(new CreatePersonalTaskPage(calendarItem.date));
            }
        }
    }
}