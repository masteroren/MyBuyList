using MyBuyList.Shared;
using MyBuyList.Shared.Entities;
using System.Linq;

namespace MyBuyList.DataLayer.DataAdapters
{
    class SettingsDA : BaseContextDataAdapter<MyBuyListEntities1>
    {
        internal MBLSettingsWrapper GetMBLSettingsWrapper()
        {
            using (DataContext)
            {
                try
                {
                    var list = from settingItem in DataContext.MBLSettings
                               select settingItem;
                    MBLSettingsWrapper settingsWrapper = new MBLSettingsWrapper(list.ToArray());

                    return settingsWrapper;
                }
                catch
                {
                    return null;
                }
            }
        }

        internal bool SaveMBLSettingsRecentItem(string recentItems, string itemType)
        {
            using (DataContext)
            {
                try
                {
                    bool succeeded = false;
                    MBLSetting recentItemsSetting;

                    if (itemType == "recipe")
                    {
                        recentItemsSetting = DataContext.MBLSettings.SingleOrDefault(s => s.SettingKey == "RecentRecipes");
                        recentItemsSetting.SettingValue = recentItems;
                    }
                    else if (itemType == "menu")
                    {
                        recentItemsSetting = DataContext.MBLSettings.SingleOrDefault(s => s.SettingKey == "RecentMenus");
                        recentItemsSetting.SettingValue = recentItems;
                    }

                    DataContext.SaveChanges();
                    succeeded = true;

                    return succeeded;
                }
                catch
                {
                    return false;
                }
            }
        }
    }
}
