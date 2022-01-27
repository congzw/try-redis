using System.Collections.Generic;

namespace Demo.Web.Domain
{
    public class DemoDb
    {
        public List<DemoItem> DemoItems { get; set; } = new List<DemoItem>();

        public static DemoDb Create()
        {
            var mockDb = new DemoDb();
            
            mockDb.DemoItems.Add(new DemoItem(){Id = "001", Name = "A"});
            mockDb.DemoItems.Add(new DemoItem(){Id = "002", Name = "B"});
            mockDb.DemoItems.Add(new DemoItem(){Id = "003", Name = "C"});

            return mockDb;
        }
    }
}