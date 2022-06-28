using System;
using System.Collections.Generic;
using System.Text;

namespace YeatyAppMobile.Models.StorageModels
{
    public class UpdateStorage
    {
        private static string KEY_UPDATE = "currentUpdateModel";

        public static UpdateModel GetUpdateModel()
        {
            if (App.Current.Properties.ContainsKey(KEY_UPDATE))
            {
                UpdateModel model = new UpdateModel();
                model = App.Current.Properties[KEY_UPDATE] as UpdateModel;
                return model;
            }
            else
            {
                UpdateModel model = new UpdateModel();
                App.Current.Properties[KEY_UPDATE] = model;
                return model;
            }
        }

        public static bool isUpdateRequiredOwnedQR()
        {
            UpdateModel model = GetUpdateModel();
            return model.needUpdateQRCodes;
        }

        public static void ChangeUpdateSituationOwnedQR(bool isRequired)
        {
            UpdateModel model = GetUpdateModel();
            model.needUpdateQRCodes = isRequired;
            SaveModel(model);
        }

        public static void SaveModel(UpdateModel updateModel)
        {
            App.Current.Properties[KEY_UPDATE] = updateModel;
        }


    }
}
