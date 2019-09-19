using MyBuyList.Shared;
using MyBuyList.Shared.Entities;
using System.Linq;

namespace MyBuyList.DataLayer.DataAdapters
{
    class SettingsDA : BaseContextDataAdapter<mybuylistEntities>
    {
        internal MBLSettingsWrapper GetMBLSettingsWrapper()
        {
            using (DataContext)
            {
                try
                {
                    var list = from settingItem in DataContext.mblsettings
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
                    mblsettings recentItemsSetting;

                    if (itemType == "recipe")
                    {
                        recentItemsSetting = DataContext.mblsettings.SingleOrDefault(s => s.SettingKey == "RecentRecipes");
                        recentItemsSetting.SettingValue = recentItems;
                    }
                    else if (itemType == "menu")
                    {
                        recentItemsSetting = DataContext.mblsettings.SingleOrDefault(s => s.SettingKey == "RecentMenus");
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
