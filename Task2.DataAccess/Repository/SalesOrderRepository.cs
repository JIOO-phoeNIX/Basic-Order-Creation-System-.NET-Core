using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task2.DataAccess.Models;

namespace Task2.DataAccess.Repository
{
    public class SalesOrderRepository
    {
        private readonly Task2DbContext task2DbContext;

        public SalesOrderRepository(Task2DbContext task2DbContext)
        {
            this.task2DbContext = task2DbContext;
        }

        #region SalesOrder
        public IEnumerable<TABSORDER> GetAll()
        {
            return task2DbContext.TABSORDER.ToList();
        }

        public IEnumerable<object> GetAllEXT()
        {
            var orders = task2DbContext.TABSORDER
                .Join(task2DbContext.TABCUST,
                    outer => outer.CUSTID,
                    inner => inner.CUSTID,
                    (order, cust) => new
                    {
                        SalesOrderId = order.SORDID,
                        CustomerName = cust.CUSTNAME,
                        SalesOrderDate = order.SORDDATE,
                        SalesOrderAmount = order.SORDAMNT,
                        SalesOrderStatus = order.DATAEXST
                    })
                .Where(ord => ord.SalesOrderStatus == "EXT")
                .ToList();
            return orders;
        }

        public IEnumerable<object> GetAllItems(decimal sOrdId)
        {
            var orderDetails = (from ord in task2DbContext.TABSORDER
                                join data in task2DbContext.TABSODATA on ord.SORDID equals data.SORDID
                                join item in task2DbContext.TABITEM on data.ITEMID equals item.ITEMID
                                join cust in task2DbContext.TABCUST on ord.CUSTID equals cust.CUSTID
                                where ord.SORDID == sOrdId && data.DATAEXST == "EXT"
                                select new
                                {
                                    ItemName = item.ITEMNAME,
                                    TotalOrderAmount = ord.SORDAMNT,
                                    Rate = data.ITEMRATE,
                                    CustName = cust.CUSTNAME,
                                    OrdDate = ord.SORDDATE,
                                    ItemId = item.ITEMID
                                });

            return orderDetails;
        }

        public TABSORDER Get(decimal ordId)
        {
            return task2DbContext.TABSORDER.Find(ordId);
        }

        public Object GetOrderItem(decimal orderId, decimal itemId)
        {
            var orderItem = (from ord in task2DbContext.TABSORDER
                             join data in task2DbContext.TABSODATA on ord.SORDID equals data.SORDID
                             join item in task2DbContext.TABITEM on data.ITEMID equals item.ITEMID
                             where ord.SORDID == orderId && data.ITEMID == itemId
                             select new
                             {
                                 ItemName = item.ITEMNAME,
                                 Rate = data.ITEMRATE,
                                 OrdDate = ord.SORDDATE,
                                 TotalRate = ord.SORDAMNT
                             });

            return orderItem;
        }

        public async Task<(decimal? itemRate, decimal? totalRate)> UpdateOrderItem(NewOrderItemPrice newPrices)
        {
            var salesOrder = task2DbContext.TABSORDER
                .Where(t => t.SORDID == newPrices.SOrdId);

            salesOrder.FirstOrDefault().SORDAMNT = newPrices.NewTotalPrice;

            var item = task2DbContext.TABSODATA
                .Where(t => t.ITEMID == newPrices.ItemId && t.SORDID == newPrices.SOrdId);

            item.FirstOrDefault().ITEMRATE = newPrices.NewItemPrice;

            await task2DbContext.SaveChangesAsync();

            var newSalesOrder = task2DbContext.TABSORDER
                .Where(t => t.SORDID == newPrices.SOrdId);

            var newItem = task2DbContext.TABSODATA
                .Where(t => t.ITEMID == newPrices.ItemId && t.SORDID == newPrices.SOrdId);

            return (newItem.FirstOrDefault().ITEMRATE, newSalesOrder.FirstOrDefault().SORDAMNT);
        }

        public async Task<string> Delete(decimal orderId)
        {
            var orderToDelete = task2DbContext.TABSORDER.Where(t => t.SORDID == orderId);

            orderToDelete.FirstOrDefault().DATAEXST = "DEL";

            await task2DbContext.SaveChangesAsync();

            var newDeletedOrder = task2DbContext.TABSORDER.Where(t => t.SORDID == orderId);


            return newDeletedOrder.FirstOrDefault().DATAEXST;
        }

        public async Task<string> DeleteItem(decimal orderId, decimal itemId)
        {
            var itemTODelete = task2DbContext.TABSODATA
                .Where(t => t.SORDID == orderId && t.ITEMID == itemId);

            itemTODelete.FirstOrDefault().DATAEXST = "DEL";

            var orderToUpdate = task2DbContext.TABSORDER.Where(t => t.SORDID == orderId);
            orderToUpdate.FirstOrDefault().SORDAMNT -= itemTODelete.FirstOrDefault().ITEMRATE;

            await task2DbContext.SaveChangesAsync();

            var newDeletedItem = task2DbContext.TABSODATA
                .Where(t => t.SORDID == orderId && t.ITEMID == itemId);

            return newDeletedItem.FirstOrDefault().DATAEXST;
        }

        public async Task<TABSORDER> CreateOrder(TABSORDER newOrder)
        {
            var newOrd = task2DbContext.TABSORDER.Add(newOrder);
            await task2DbContext.SaveChangesAsync();           

            return newOrder;   
        }

        public async Task<TABSODATA> AdditemToOrder(TABSODATA newItem)
        {
            var checkItem = task2DbContext.TABSODATA
                .Where(t => t.SORDID == newItem.SORDID && t.ITEMID == newItem.ITEMID);

            if (checkItem.FirstOrDefault() == null)
            {
                var addeditem = task2DbContext.TABSODATA.Add(newItem);

                var order = task2DbContext.TABSORDER
                    .Where(t => t.SORDID == newItem.SORDID);
                order.FirstOrDefault().SORDAMNT += newItem.ITEMRATE;

                await task2DbContext.SaveChangesAsync();

                return newItem;
            }
            else
            {
                checkItem.FirstOrDefault().DATAEXST = "EXT";
                await task2DbContext.SaveChangesAsync();
                return checkItem.FirstOrDefault();
            }
        }
        #endregion       
    }

    public class NewOrderItemPrice
    {
        public decimal NewTotalPrice { get; set; }
        public decimal NewItemPrice { get; set; }
        public decimal SOrdId { get; set; }
        public decimal ItemId { get; set; }
    }
}
