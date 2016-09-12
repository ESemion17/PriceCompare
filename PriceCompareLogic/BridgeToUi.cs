using System.Collections.Generic;

namespace PriceCompareLogic
{
    public class BridgeToUi
    {
        public static List<List<Item>> GetItems()
        {
            return DbAccessor.LoadItemsToShow();
        }
        public static List<Store> GetStoreList()
        {
            return DbAccessor.Loadstores();
        }
    }
}