using CoStudyApp.Models;
using CoStudyApp.Models.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using YeatyAppMobile.ServiceController;

namespace YeatyAppMobile.Views.RegisterPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterStep2_B : ContentPage
    {
        User user = new User();
        ManageServices service = new ManageServices();
        List<University> universities = new List<University>();
        List<Department> allDepartments = new List<Department>();
        List<Faculty> allFaculties = new List<Faculty>();
        bool isProceedEnabled = false;
        string status = "";
        bool isCheckedPrivacy = false;
        bool isProceeded = false;

        int selectedUniId = 0;

        List<string> departmentNames = new List<string>();
        List<string> facultyNames = new List<string>();
        List<string> universityNames = new List<string>();
        public RegisterStep2_B(User _user, List<University> _universities, bool _isGoogleLogin = false, bool _isFacebookLogin = false)
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            user = _user;
            universities = _universities;
            foreach (University uni in universities)
            {
                foreach (Faculty fac in uni.Faculties)
                {
                    allFaculties.Add(fac);
                    foreach (Department dep in fac.Departments)
                    {
                        allDepartments.Add(dep);
                    }
                }
            }
            universityNames = universities.OrderBy(x => x.UniversityName).Select(x => x.UniversityName).ToList();
            pickerUni.BindingContext = universityNames;
            pickerUni.ItemsSource = universityNames;
            //pickerFaculty.BindingContext = facultyNames;
            //pickerFaculty.ItemsSource = facultyNames;           
            //user.isGoogleLogin = _isGoogleLogin;
            //user.isFacebookLogin = _isFacebookLogin;
        }

        private async void tapBack_Tapped(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }


        private async void tapProceed_Tapped(object sender, EventArgs e)
        {
            if (pickerUni.SelectedItem == null)
            {
                await DisplayAlert("Hata", "Lütfen okulunuzu seçiniz", "Tamam");
            }
            if (pickerFaculty.SelectedItem == null)
            {
                await DisplayAlert("Hata", "Lütfen fakültenizi seçiniz", "Tamam");
            }
            if (pickerFaculty.SelectedItem == null)
            {
                await DisplayAlert("Hata", "Lütfen bölümünüzü seçiniz", "Tamam");
            }
            string selectedFacultyStr = pickerFaculty.SelectedItem as string;
            string selectedDepartmentStr = pickerDepartment.SelectedItem as string;

            Faculty selectedFaculty = allFaculties.Where(x => x.FacultyName == selectedFacultyStr).FirstOrDefault();
            Department selectedDepartment = allDepartments.Where(x => x.DepartmentName.ToLower() == selectedDepartmentStr.ToLower()).FirstOrDefault();
            user.DepartmentId = selectedDepartment.Id;
            isProceeded = true;
            await Navigation.PushAsync(new RegisterStep3Page(user), true);
        }

        private void pickerFaculty_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedUniStr = pickerUni.SelectedItem as string;
            University selectedUni = universities.Where(x => x.UniversityName.ToLower() == selectedUniStr.ToLower()).FirstOrDefault();
            string selected = pickerFaculty.SelectedItem as string;
            Faculty selectedFaculty = allFaculties.Where(x => x.FacultyName.ToLower() == selected.ToLower() && x.UniversityId == selectedUni.Id).FirstOrDefault();
            List<string> departmentNames = allDepartments.Where(x => x.FacultyId == selectedFaculty.Id).
                OrderBy(x => x.DepartmentName).Select(x => x.DepartmentName).ToList();
            pickerDepartment.SelectedItem = null;
            pickerDepartment.BindingContext = null;
            pickerDepartment.ItemsSource = null;
            pickerDepartment.BindingContext = departmentNames;
            pickerDepartment.ItemsSource = departmentNames;
            isProceedEnabled = false;
            ChangeButtonAvailablity();



        }

        private void pickerDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (pickerDepartment.SelectedItem != null)
                isProceedEnabled = true; ChangeButtonAvailablity();
        }

        private void pickerUni_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selected = pickerUni.SelectedItem as string;
            University selectedUni = universities.Where(x => x.UniversityName.ToLower() == selected.ToLower()).FirstOrDefault();
            List<string> facultyNames = allFaculties.Where(x => x.UniversityId == selectedUni.Id).
                OrderBy(x => x.FacultyName).Select(x => x.FacultyName).ToList();
            pickerFaculty.SelectedItem = null;
            pickerFaculty.BindingContext = null;
            pickerFaculty.ItemsSource = null;
            pickerDepartment.SelectedItem = null;
            pickerDepartment.BindingContext = null;
            pickerDepartment.ItemsSource = null;
            pickerFaculty.BindingContext = facultyNames;
            pickerFaculty.ItemsSource = facultyNames;
            isProceedEnabled = false;
            ChangeButtonAvailablity();
        }

        public void ChangeButtonAvailablity()
        {
            if (isProceedEnabled)
                frameProceed.Opacity = 1;
            else
                frameProceed.Opacity = 0.3;
        }

   
    }
}
