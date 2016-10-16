using System.Collections.Generic;

namespace PriceCompareLogic
{
    //It is a good idea to encapsulate Data from UI, however I would consider the name of this class
    //Since the logic dll should not even assume there is a UI, a better name for this would be "DAL"
    //Further Information: https://en.wikipedia.org/wiki/Data_access_layer 
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