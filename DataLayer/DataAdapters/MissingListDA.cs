using MyBuyList.Shared;
using System;
using System.Linq;

namespace MyBuyList.DataLayer.DataAdapters
{
    class MissingItemsListDA : BaseContextDataAdapter<mybuylistEntities>
    {
        internal int AddList(int userId)
        {
            using (DataContext)
            {
                missinglists missingList = DataContext.missinglists.SingleOrDefault(p => p.CREATED_BY == userId && p.ACTIVE.HasValue ? p.ACTIVE.Value : false);

                if (missingList == null)
                {
                    missingList = new missinglists { CREATED_BY = userId, CREATE_DATE = DateTime.Today, ACTIVE = true };
                    DataContext.missinglists.Add(missingList);
                    DataContext.SaveChanges();
                    //DataContext.MissingLists.InsertOnSubmit(missingList);
                    //DataContext.SubmitChanges();
                }

                return missingList.ID;
            }
        }

        internal void AddListItem(string name, int quantity, int measureUnit, int userId)
        {
            int missingListId;
            using (DataContext)
            {
                missinglists missingList = DataContext.missinglists.SingleOrDefault(p => p.CREATED_BY == userId && p.ACTIVE.HasValue ? p.ACTIVE.Value : false);

                if (missingList != null)
                {
                    missingListId = missingList.ID;

                    missinglistdetails missingListDetail = null;

                    food food = DataContext.food.SingleOrDefault(p => p.FoodName == name);

                    if (food == null)
                    {
                        food = new food
                        {
                            FoodName = name,
                            CreatedBy = userId,
                        };
                        new AdminDA().SaveFood(food);
                        food = DataContext.food.SingleOrDefault(p => p.FoodName == name);
                    }

                    missingListDetail = DataContext.missinglistdetails.SingleOrDefault(p => p.LIST_ID == missingListId && p.FOOD_ID == food.FoodId);

                    if (missingListDetail != null)
                    {
                        missingListDetail.QUANTITY = quantity;
                    }
                    else
                    {
                        missingListDetail = new missinglistdetails
                        {
                            FOOD_ID = food.FoodId,
                            LIST_ID = missingListId,
                            QUANTITY = quantity,
                            MEASUREMENT_UNIT_ID = measureUnit
                        };
                        DataContext.missinglistdetails.Add(missingListDetail);
                        //DataContext.MissingListDetails.InsertOnSubmit(missingListDetail);
                    }
                }

                DataContext.SaveChanges();
                //DataContext.SubmitChanges();
            }
        }

        internal IQueryable<missinglistdetails> GetList(int userId)
        {
            missinglists missingList = DataContext.missinglists.SingleOrDefault(p => p.CREATED_BY == userId);

            if (missingList != null)
            {
                IQueryable<missinglistdetails> missingListDetail = DataContext.missinglistdetails.Where(p => p.LIST_ID == missingList.ID);
                return missingListDetail;
            }

            return null;
        }

        internal void DeleteList(int listId)
        {
            using (DataContext)
            {
                missinglists missingList = DataContext.missinglists.SingleOrDefault(p => p.ID == listId);
                if (missingList != null)
                {
                    DataContext.missinglists.Remove(missingList);
                    DataContext.SaveChanges();
                    //DataContext.MissingLists.DeleteOnSubmit(missingList);
                    //DataContext.SubmitChanges();
                }
            }
        }

        internal void DeleteListItem(int id)
        {
            using (DataContext)
            {
                int listId = 0;
                missinglistdetails missingListDetail = DataContext.missinglistdetails.SingleOrDefault(p => p.ID == id);
                if (missingListDetail != null)
                {
                    listId = missingListDetail.LIST_ID;
                    DataContext.missinglistdetails.Remove(missingListDetail);
                    DataContext.SaveChanges();
                    //DataContext.MissingListDetails.DeleteOnSubmit(missingListDetail);
                    //DataContext.SubmitChanges();
                }

                if (!DataContext.missinglistdetails.Any())
                {
                    DeleteList(listId);
                }
            }
        }

        internal void UpdateMissingListItem(int id, int quantity)
        {
            using (DataContext)
            {
                missinglistdetails missingListDetail = DataContext.missinglistdetails.SingleOrDefault(p => p.ID == id);
                if (missingListDetail != null)
                {
                    missingListDetail.QUANTITY = quantity;
                    DataContext.SaveChanges();
                    //DataContext.SubmitChanges();
                }
            }
        }
    }
}
