using System;
using System.Collections.Generic;
using System.Linq;
using Demo.Web.Domain;

namespace Demo.Web.AppServices
{
    public interface IDemoAppService
    {
        List<DemoItem> GetAll();
        DemoItem GetById(string id);
        void Save(DemoItem item);
    }

    public class DemoAppService : IDemoAppService
    {
        private readonly DemoDb _demoDb;
        private readonly IMyCacheService _cache;

        public DemoAppService(DemoDb demoDb, IMyCacheService cache)
        {
            _demoDb = demoDb;
            _cache = cache;
        }

        public List<DemoItem> GetAll()
        {
            return _demoDb.DemoItems;
        }

        public DemoItem GetById(string id)
        {
            var demoItem = _cache.Get<DemoItem>(id);
            if (demoItem != null)
            {
                demoItem.CacheLastHit = DateTime.Now.ToString("HH:mm:ss");
                return demoItem;
            }

            var theOne = _demoDb.DemoItems.SingleOrDefault(x => x.Id == id);
            if (theOne != null)
            {
                theOne.DbLastHit = DateTime.Now.ToString("HH:mm:ss");
            }

            _cache.Set(id, theOne);
            return theOne;
        }

        public void Save(DemoItem item)
        {
            if (item == null)
            {
                return;
            }
            
            var theOne = _demoDb.DemoItems.SingleOrDefault(x => x.Id == item.Id);
            if (theOne != null)
            {
                _demoDb.DemoItems.Remove(theOne);
            }
            
            _demoDb.DemoItems.Add(item);
            _cache.Set(item.Id, item);
        }
    }
}
