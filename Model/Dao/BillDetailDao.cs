using Model.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Model.Dao
{
    public class BillDetailDao
    {
        OnlineShopDbContext db;

        public BillDetailDao()
        {
            db = new OnlineShopDbContext();
        }

        public List<OrderDetail> ViewDetail(long id)
        {
            var query = db.OrderDetails;
            //IQueryable<OrderDetail>  = this.db.OrderDetails;
            //string qr = "SELECT *FROM OrderDetail WHeRE OrderID ="+id;
            return query.Where(x => x.OrderID == id).OrderBy(x => x.Quantity).ToList();
        }
        public IEnumerable<OrderDetail> ListAllPaging(string searchString, int page, int pageSize)
        {
            IQueryable<OrderDetail> model = db.OrderDetails;
            return model.OrderByDescending(x => x.Quantity).ToPagedList(page, pageSize);
        }
        public OrderDetail ViewDetails(int id)
        {
            return db.OrderDetails.Find(id);
        }
        public bool Delete(int id)
        {
            try
            {
                var b = db.OrderDetails.Find(id);
                db.OrderDetails.Remove(b);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public long Insert(OrderDetail entity)
        {
            db.OrderDetails.Add(entity);
            db.SaveChanges();
            return entity.OrderID;
        }
        public bool Update(OrderDetail entity)
        {
            try
            {
                var bil = db.OrderDetails.Find(entity.OrderID);
                bil.ProductID = entity.ProductID;
                bil.Quantity = entity.Quantity;
                bil.Price = entity.Price;
                bil.Quantity = entity.Quantity;

                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
    }
}
