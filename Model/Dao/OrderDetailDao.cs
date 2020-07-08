using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.EF;

namespace Model.Dao
{
    public class OrderDetailDao
    {
        OnlineShopDbContext db = null;
        public OrderDetailDao()
        {
            db = new OnlineShopDbContext();
        }
        public bool Insert(OrderDetail detail)
        {
            try
            {
                db.OrderDetails.Add(detail);
                db.SaveChanges();
                return true;
            }catch
            {
                return false;
            }  
        }
        public bool UpdatePDQuantity(Product pd, int qty)
        {
            try
            {
                var pdao = new ProductDao();
                var product = db.Products.Find(pd.ID);
                pd.Quantity -= qty;
                pd.Viewcount += 1;
                pdao.Update(pd);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
