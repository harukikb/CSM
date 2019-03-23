using Model.EF;
using Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using PagedList;

namespace Model.DAO
{
    public class ProductDao
    {
        /**
         * Constants
         */
        private ShopDbContext db = null;

        /**
         * @description -- init
         */

        public ProductDao()
        {
            db = new ShopDbContext();
        }

        private static ProductDao instance;

        public static ProductDao Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ProductDao();
                }
                return instance;
            }
        }


        /**
         * @description -- get Product by ProdID
         * @param _key: int -- is field ProdID
         */

        public Product getByID(int _key)
        {
            return db.Product.SingleOrDefault(obj => obj.ProdID == _key);
        }

        /**
         * @description -- check exits product in table Product
         * @param _prod: Product -- is a transion object
         */

        public bool hasProcuct(Product _prod)
        {
            var product = db.Product.SingleOrDefault(obj => obj.ProdID == _prod.ProdID);
            return product != default(Product) ? true : false;
        }

        /**
         * @description -- insert a product
         * @param _request: Product -- entity object
         */

        public bool insert(Product _request)
        {
            if (!hasProcuct(_request))
            {
                _request.CreatedAt = DateTime.Now;
                db.Product.Add(_request);
                db.SaveChanges();
                return true;
            }
            return false;
        }

        /**
         * @description -- delete a product
         * @param _key: int -- is field ProdID
         */

        public bool delete(int _key)
        {
            if (hasReference(_key))
                return false;
            db.Product.Remove(getByID(_key));
            db.SaveChanges();
            return true;
        }

        /**
         * @description -- change status active
         * @param _key: int -- is field ProdID
         */

        public bool changeStatus(int _key)
        {
            var product = getByID(_key);
            product.isActive = !product.isActive;
            product.UpdatedAt = DateTime.Now;
            db.SaveChanges();
            return product.isActive;
        }

        /**
         * @description -- update info product has image
         * @param _request: ProductRequestDto -- is the data transmitted down from the display screen
         */

        public bool Update(Product _request)
        {
            var product = getByID(_request.ProdID);
            product.ProdName = _request.ProdName;
            product.Decription = _request.Decription;
            product.Cost = _request.Cost;
            product.ImageUrl = _request.ImageUrl;
            product.UpdatedAt = DateTime.Now;
            product.Wantity = _request.Wantity;
            product.isActive = _request.isActive;
            db.SaveChanges();
            return true;
        }
        public bool UpdateWantity(Product _request)
        {
            var product = getByID(_request.ProdID);
            product.UpdatedAt = DateTime.Now;
            product.isActive = _request.isActive;
            product.Wantity = _request.Wantity;
            db.SaveChanges();
            return true;
        }


        public IEnumerable<Product> ListAllPaging(string searchString, int page)
        {
            IQueryable<Product> model = db.Product;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.ProdName.Contains(searchString) || x.ProdName.Contains(searchString));
            }
            return model.OrderByDescending(x => x.CreatedAt).ToPagedList(page, Constants.PageSize);

        }

        /**
         * @description -- get products list by search key
         * @param _search: string -- is search key
         */

        public IEnumerable<Product> getObjectList(string _search, int page, out int totalRows, out int totalPages)
        {
            var model = db.Product.OrderBy(p => p.CreatedAt).ToList();

            if (_search != null)
            {
                model = model.Where(obj => obj.ProdName.Contains(_search)).ToList();
            }

            totalRows = model.Count();
            totalPages = (int)Math.Ceiling((double)(totalRows / Constants.PageSize));

            return model.Skip((page - 1) * Constants.PageSize)
                        .Take(Constants.PageSize);
        }

        /**
         * @private
         * @description -- check the existence of image
         * @param imagefilePath: string -- is the path of the image file
         */


        public List<Product> ListNewProduct(int top,string _keysearch)
        {
            return db.Product.OrderByDescending(x => x.CreatedAt).Where(x => x.ProdName.Contains(_keysearch)).Take(top).ToList();
            //return db.Product.OrderByDescending(x => x.CreatedAt).Take(top).ToList();
        }

        private bool hasReference(int _key)
        {
            var product = getByID(_key);
            if (product != default(Product))
            {
                var count_one = db.Order.Where(obj => obj.ProdID == _key).ToList().Count;
                return count_one > Constants.zeroNumber;
            }
            return Constants.falseValue;
        }
        public List<Product> ListRelateProduct(int productID)
        {
            var product = db.Product.Find(productID);
            return db.Product.Where(x => x.ProdID != productID && x.CateID == product.CateID).ToList();
        }

        public List<Product> ListByCategoryId(ref int totalRecord, int pageIndex = 1,string key_search="")
        {
            var model = db.Product.OrderBy(x => x.ProdID).Where(x=>x.ProdName.Contains(key_search)).ToList();
            totalRecord = model.Count();//nghi nó bằng 0 chỗ này
            model = model.Skip((pageIndex - 1) * Constants.PageSize).Take(Constants.PageSize).ToList();
            return model;
        }
        public List<string> ListName(string keyword)
        {
            return db.Product.Where(x => x.ProdName.Contains(keyword)).Select(x => x.ProdName).ToList();
        }
        public List<Product> Search(string search_kw, ref int totalRecord, int pageIndex = 1)
        {
            var model = db.Product.Where(x => x.ProdName.Contains(search_kw)).ToList();
            totalRecord = db.Product.Where(x=>x.ProdName.Contains(search_kw)).Count();//nghi nó bằng 0 chỗ này
            model = model.Skip((pageIndex - 1) * Constants.PageSize).Take(Constants.PageSize).ToList();
            return model;
        }
    }
}