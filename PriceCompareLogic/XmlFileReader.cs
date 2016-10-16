using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace PriceCompareLogic
{
    /*As it name dictates, this class reads XML files
     * However, it has state which is not implied by its name- the item dictionary
     * Which has absolutely nothing to do with this class
     * The same issue arises with the GetData method which does not return a value
     * Don't you think a better approach would be to return the dictionary from the read method instead?
     */
    public class XmlFileReader
    {
        private XElement _xmlFile;
        private Dictionary<string, Item> _items;

        public XmlFileReader(string path)
        {
            _xmlFile = XElement.Load(path);
            _items = new Dictionary<string, Item>();
            GetData();
        }

        private void GetData()
        {
            string chainId = _xmlFile.Descendants("ChainId").First().Value;
            foreach (var item in _xmlFile.Descendants("Item"))
            {
                var itemCode = item.Descendants("ItemCode").First().Value;
                float price;
                if (!_items.ContainsKey(itemCode) && 
                    float.TryParse(item.Descendants("ItemPrice").First().Value, out price))
                {
                    _items.Add(itemCode, new Item()
                    {
                        ChainId = chainId,
                        ItemId = item.Descendants("ItemCode").First().Value,
                        ItemName = item.Descendants("ItemName").First().Value,
                        ItemPrice = price,
                        ManufactureCountry = item.Descendants("ManufactureCountry").First().Value,
                        UnitOfMeasure = item.Descendants("UnitOfMeasure").First().Value
                    });
                }
            }
        }

        public Dictionary<string, Item> Items => _items;
    }
}