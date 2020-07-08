using Model.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Model.Dao
{
    public class BillDao
    {
        OnlineShopDbContext db;

        public BillDao()
        {
            db = new OnlineShopDbContext();
        }

        public Order ViewDetail(long id)
        {
            return db.Orders.Find(id);
        }
        public IEnumerable<Order> ListAllPaging(string searchString, int page, int pageSize)
        {
            IQueryable<Order> model = db.Orders;
            return model.OrderByDescending(x => x.ID).ToPagedList(page, pageSize);
        }
        public bool Delete(int id)
        {
            try
            {
                var b = db.Orders.Find(id);
                db.Orders.Remove(b);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public long Insert(Order entity)
        {
            db.Orders.Add(entity);
            db.SaveChanges();
            return entity.ID;
        }
        public bool Update(Order entity)
        {
            try
            {
                var bil = db.Orders.Find(entity.ID);
                bil.CustomerID = entity.CustomerID;
                bil.CreateDate = DateTime.Now;
                bil.ShipAddress = entity.ShipAddress;
                bil.ShipName = entity.ShipName;
                bil.ShipMobile = entity.ShipMobile;
                bil.ShipEmail = entity.ShipEmail;
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
