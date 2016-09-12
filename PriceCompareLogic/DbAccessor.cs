using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;

namespace PriceCompareLogic
{
    public class DbAccessor
    {
        public static void CreateOrUpdateAdmin(User user)
        {
            using (var db = new PriceCompareContext())
            {
                var query = from u in db.Users
                    where u.Master == true
                    select u;
                var admin = query.FirstOrDefault();
                if (admin == null)
                {
                    db.Users.Add(user);
                }
                else
                {
                    user.Id = admin.Id;
                    db.Entry(admin).CurrentValues.SetValues(user);
                }
                db.SaveChanges();
            }
        }

        public static List<Store> Loadstores()
        {
            List<Store> listToReturn;
            using (var db = new PriceCompareContext())
            {
                var query = from i in db.Stores
                            select i;
                listToReturn = query.ToList();
            }
            return listToReturn;
        }

        public static List<Item> LoadAllItems()
        {
            List<Item> listToReturn;
            using (var db = new PriceCompareContext())
            {
                var query = from i in db.Items
                    select i;
                listToReturn = query.ToList();
            }
            return listToReturn;
        }

        public static List<List<Item>> LoadItemsToShow()
        {
            List<Item> unsortList;
            using (var db = new PriceCompareContext())
            {
                var query = from i in db.Items
                    where i.ToShow == true
                    select i;
                unsortList = query.OrderBy(i => i.ChainId).ToList();
                
            }
            var lastChain = unsortList.First().ChainId;

            var listToRetern = new List<List<Item>>() {new List<Item>()};
            int index = 0;
            foreach (var item in unsortList)
            {
                if (item.ChainId == lastChain)
                    listToRetern[index].Add(item);
                else
                {
                    lastChain = item.ChainId;
                       index++;
                    listToRetern.Add(new List<Item>() {item});
                }
            }
            return listToRetern;
        }

        public static List<Item> LoadItemsByChainId(string chainId)
        {
            IQueryable<Item> query;
            using (var db = new PriceCompareContext())
            {
                query = from i in db.Items
                    where i.ChainId == chainId
                    select i;
            }
            return query.ToList();
        }

        public static List<Item> LoadItemsByItemName(string name)
        {
            IQueryable<Item> query;
            using (var db = new PriceCompareContext())
            {
                query = from i in db.Items
                    where i.ItemName == name
                    select i;
            }
            return query.ToList();
        }

        public static List<Item> LoadItemsByItemId(string itemId)
        {
            IQueryable<Item> query;
            using (var db = new PriceCompareContext())
            {
                query = from i in db.Items
                    where i.ItemId == itemId
                    select i;
            }
            return query.ToList();
        }

        public static void InsertStore(string chainId, string name)
        {
            using (var db = new PriceCompareContext())
            {
                var query = from s in db.Stores
                    where s.Id == chainId
                    select s;
                if (query.FirstOrDefault() == null)
                {
                    db.Stores.Add(new Store()
                    {
                        ChainName = name,
                        Id = chainId
                    });
                }
                db.SaveChanges();
            }
        }

        public static void InsertItem(List<Item> items)
        {
            using (var db = new PriceCompareContext())
            {
                foreach (var item in items)
                {
                    //try
                    //{
                    db.Items.Add(item);
                    //}
                    //catch (DbEntityValidationException e)
                    //{
                    //    foreach (var eve in e.EntityValidationErrors)
                    //    {
                    //        Debug.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                    //            eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    //        foreach (var ve in eve.ValidationErrors)
                    //        {
                    //            Debug.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                    //                ve.PropertyName, ve.ErrorMessage);
                    //        }
                    //    }
                    //    throw;
                    //}
                }
                db.SaveChanges();
            }
        }

        public static void InsertCart(int[] itemId, float[] quantity, int userId)
        {
            using (var db = new PriceCompareContext())
            {
                var query = from c in db.Carts
                            where c.UserId == userId
                            select c;
                if (query.FirstOrDefault() != null)
                    db.Carts.RemoveRange(db.Carts.Where(c => c.UserId == userId));
                db.SaveChanges();

                for (int i = 0; i < itemId.Length; i++)
                    db.Carts.Add(new Cart()
                    {
                        ItemId = itemId[0],
                        Quantity = quantity[0],
                        UserId = userId
                    });
                db.SaveChanges();
            }
        }

        public static bool InsertUser(string name)
        {
            var flag = true;
            using (var db = new PriceCompareContext())
            {
                var query = from u in db.Users
                    where u.UserName == name
                    select u;
                if (query.FirstOrDefault() == null)
                    db.Users.Add(new User() {UserName = name});
                else
                    flag = false;
            }
            return flag;
        }
    }
}