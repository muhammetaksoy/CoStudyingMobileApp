using Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace YeatyAppMobile.Models.StorageModels
{
    public class QRStorage
    {
        private static string KEY_QR = "currentQRModel";

        public static QRModel GetQRModel()
        {
            if (Xamarin.Essentials.Preferences.ContainsKey(KEY_QR))
            {
                string str = Xamarin.Essentials.Preferences.Get(KEY_QR, null);
                QRModel model = JsonConvert.DeserializeObject<QRModel>(str);
                return model;
            }
            else
            {
                return null;
            }
        }

        // CHECK QR, if it's timed out, Clear Storage ( Less than 2 minutes )
        public static void QRTimeCheck()
        {
            QRModel qrModel = GetQRModel();
            if ( qrModel != null )
            {
                
                if(qrModel.FinishOn < DateTime.Now)
                {
                    ClearModel();
                }
                else if (qrModel.FinishOn.Subtract(DateTime.Now) < new TimeSpan(0,2,0))
                {
                    ClearModel();
                }
            }
        }


        public static void QRCreated(string hashedPassCode)
        {
            QRModel qrModel = new QRModel() { CreatedOn = DateTime.Now, FinishOn = DateTime.Now.AddMinutes(10), HashedPassCode = hashedPassCode };
            SaveModel(qrModel);
        }



        public static void SaveModel(QRModel qrModel)
        {
            var jsonResult = JsonConvert.SerializeObject(qrModel);
            Xamarin.Essentials.Preferences.Set(KEY_QR, jsonResult);
        }

        public static void ClearModel()
        {
            if (Xamarin.Essentials.Preferences.ContainsKey(KEY_QR))
                Xamarin.Essentials.Preferences.Remove(KEY_QR);
        }







    }
}
