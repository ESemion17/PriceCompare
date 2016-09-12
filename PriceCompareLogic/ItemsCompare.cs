using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace PriceCompareLogic
{
    public class ItemsCompare
    {
        public ItemsCompare()
        {
            var allItems = DbAccessor.LoadAllItems();
            if (allItems.Count == 0)
            {
                LoadInitXmlFiles();
            }
        }

        private void LoadInitXmlFiles()
        {
            var file1Dict = (new XmlFileReader("..\\..\\..\\PriceCompareLogic\\InitXml\\PriceFull7290058140886-044-201609100010.xml")).Items;
            var name1 = "רמי לוי";
            DbAccessor.InsertStore(file1Dict.First().Value.ChainId,name1);

            var file2Dict = (new XmlFileReader("..\\..\\..\\PriceCompareLogic\\InitXml\\PriceFull7290492000005-656-201609110010.xml")).Items;
            var name2 = "דור אלון";
            DbAccessor.InsertStore(file2Dict.First().Value.ChainId, name2);

            var file3Dict = (new XmlFileReader("..\\..\\..\\PriceCompareLogic\\InitXml\\PriceFull7290873255550-014-201609110010.xml")).Items;
            var name3 = "טיב טעם";
            DbAccessor.InsertStore(file3Dict.First().Value.ChainId, name3);

            var dictionaryById = new Dictionary<string,int>();
            var dictionaryByName = new Dictionary<string, int>();

            foreach (var item in file1Dict)
            {
                if (!dictionaryById.ContainsKey(item.Value.ItemId))
                    dictionaryById.Add(item.Value.ItemId,1);
                if (!dictionaryByName.ContainsKey(item.Value.ItemName))
                    dictionaryByName.Add(item.Value.ItemName, 1);
            }

            foreach (var item in file2Dict)
            {
                if (dictionaryById.ContainsKey(item.Value.ItemId))
                    dictionaryById[item.Value.ItemId]=2;
                if (dictionaryByName.ContainsKey(item.Value.ItemName))
                    dictionaryByName[item.Value.ItemName] = 2;
            }

            foreach (var item in file3Dict)
            {
                if (dictionaryById.ContainsKey(item.Value.ItemId) && dictionaryById[item.Value.ItemId]==2)
                    dictionaryById[item.Value.ItemId] = 3;
                if (dictionaryByName.ContainsKey(item.Value.ItemName) && dictionaryByName[item.Value.ItemName]==2)
                    dictionaryByName[item.Value.ItemName] = 3;
            }

            foreach (var item in dictionaryById.Where(i => i.Value == 3))
            {
                file1Dict.First(i => i.Value.ItemId == item.Key).Value.ToShow = true;
                file2Dict.First(i => i.Value.ItemId == item.Key).Value.ToShow = true;
                file3Dict.First(i => i.Value.ItemId == item.Key).Value.ToShow = true;
            }
            foreach (var item in dictionaryByName.Where(i => i.Value == 3))
            {
                file1Dict.First(i => i.Value.ItemName == item.Key).Value.ToShow = true;
                file2Dict.First(i => i.Value.ItemName == item.Key).Value.ToShow = true;
                file3Dict.First(i => i.Value.ItemName == item.Key).Value.ToShow = true;
            }
            
            DbAccessor.InsertItem(file1Dict.Values.Where(i => i.ToShow).ToList());
            DbAccessor.InsertItem(file2Dict.Values.Where(i => i.ToShow).ToList());
            DbAccessor.InsertItem(file3Dict.Values.Where(i => i.ToShow).ToList());

        }

    }
}