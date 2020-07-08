using Model.EF;
using Model.ViewModel;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class ProductDao
    {
        OnlineShopDbContext db = null;

        public ProductDao()
        {
            db = new OnlineShopDbContext();
        }

        public List<Product> ListProduct()
        {
            return db.Products.OrderByDescending(x => x.CreateDate).ToList();
        }
        public List<Product> ListNewProduct(int top)
        {
            return db.Products.OrderByDescending(x => x.CreateDate).Take(top).ToList();
        }
      
        public List<Product> ListPopularProduct(int top)
        {
            return db.Products.Where(x => x.Viewcount != null && x.Viewcount > 10).OrderByDescending(x => x.CreateDate).Take(top).ToList();
        }
        public Product ViewDetail(long id)
        {
            return db.Products.Find(id);
        }
        //public List<Product> ListName(string keyword)
        //{
        //    //return db.Products.Where(x => x.Name.Contains(keyword) || x.MetaKeywords.Contains(keyword)).Select(x => x.Name).ToList();
        //    return db.Products.Where(x => x.Name == keyword || x.MetaKeywords == keyword).ToList();
            
        //}
        public List<Product> ListName(string keyword)
        {
            //return db.Products.Where(x => x.Name.Contains(keyword) || x.MetaKeywords.Contains(keyword)).Select(x => x.Name).ToList();
            return db.Products.Where(x => x.Name == keyword || x.MetaKeywords == keyword).ToList();
        }
        public List<Product> ListCategoryProduct(long CateId)
        {
            return db.Products.Where(x => x.CategoryID == CateId).OrderByDescending(x => x.CreateDate).ToList();
        }

        public IEnumerable<Product> ListAllPaging(string searchString, int page, int pageSize)
        {
            IQueryable<Product> model = db.Products;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Name.Contains(searchString) || x.Name.Contains(searchString));
            }
            return model.OrderByDescending(x => x.ID).ToPagedList(page, pageSize);
        }
        public Product ViewDetails(int id)
        {
            return db.Products.Find(id);
        }
        public bool Delete(int id)
        {
            try
            {
                var pd = db.Products.Find(id);
                db.Products.Remove(pd);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public long Insert(Product entity)
        {
            db.Products.Add(entity);
            db.SaveChanges();
            return entity.ID;
        }
        public bool Update(Product entity)
        {
            try
            {
                var product = db.Products.Find(entity.ID);

                product.Name = entity.Name;
                product.Code = entity.Code;
                product.Metatitle = entity.Metatitle;
                product.Image = entity.Image;
                product.Price = entity.Price;
                product.Quantity = entity.Quantity;
                product.Description = entity.Description;
                product.ModifiedBy = entity.ModifiedBy;
                product.ModifiedDate = DateTime.Now;
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        public List<ProductViewModel> ListByCategoryId(long categoryID, ref int totalRecord, int pageIndex = 1, int pageSize = 2)
        {
            totalRecord = db.Products.Where(x => x.CategoryID == categoryID).Count();
            var model = (from a in db.Products
                         join b in db.ProductCategories
                         on a.CategoryID equals b.ID
                         where a.CategoryID == categoryID
                         select new
                         {
                             CateMetaTitle = b.MetaTitle,
                             CateName = b.Name,
                             CreatedDate = a.CreateDate,
                             ID = a.ID,
                             Images = a.Image,
                             Name = a.Name,
                             MetaTitle = a.Metatitle,
                             Price = a.Price
                         }).AsEnumerable().Select(x => new ProductViewModel()
                         {
                             CateMetaTitle = x.MetaTitle,
                             CateName = x.Name,
                             CreatedDate = x.CreatedDate,
                             ID = x.ID,
                             Images = x.Images,
                             Name = x.Name,
                             MetaTitle = x.MetaTitle,
                             Price = x.Price
                         });
            model.OrderByDescending(x => x.CreatedDate).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            return model.ToList();
        }
        public List<ProductViewModel> Search(string keyword, ref int totalRecord, int pageIndex = 1, int pageSize = 2)
        {
            totalRecord = db.Products.Where(x => x.Name == keyword).Count();
            var model = (from a in db.Products
                         join b in db.ProductCategories
                         on a.CategoryID equals b.ID
                         where a.Name.Contains(keyword)
                         select new
                         {
                             CateMetaTitle = b.MetaTitle,
                             CateName = b.Name,
                             CreatedDate = a.CreateDate,
                             ID = a.ID,
                             Images = a.Image,
                             Name = a.Name,
                             MetaTitle = a.Metatitle,
                             Price = a.Price
                         }).AsEnumerable().Select(x => new ProductViewModel()
                         {
                             CateMetaTitle = x.MetaTitle,
                             CateName = x.Name,
                             CreatedDate = x.CreatedDate,
                             ID = x.ID,
                             Images = x.Images,
                             Name = x.Name,
                             MetaTitle = x.MetaTitle,
                             Price = x.Price
                         });
            model.OrderByDescending(x => x.CreatedDate).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            return model.ToList();
        }
    }
}
