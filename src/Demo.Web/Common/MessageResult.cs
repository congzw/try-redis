using System.Collections.Generic;
using System.Linq;

namespace Demo.Web.Common
{
    public class MessageResult
    {
        public string Message { get; set; }
        public object Data { get; set; }
        public bool Success { get; set; }

        public static MessageResult Create(string msg, bool success, object data = null)
        {
            var messageResult = new MessageResult();
            messageResult.Message = msg;
            messageResult.Success = success;
            messageResult.Data = data;
            return messageResult;
        }
    }

    public class ObjectDesc
    {
        public string Info { get; set; }
        public static ObjectDesc Create(object item)
        {
            var objectDesc = new ObjectDesc();
            objectDesc.Info = GetObjectDesc(item);
            return objectDesc;
        }
        public static List<ObjectDesc> Create(params object[] items)
        {
            return items.Select(Create).ToList();
        }
        public static List<string> GetObjectDesc(params object[] items)
        {
            return items.Select(Create).Select(x => x.Info).ToList();
        }
        private static string GetObjectDesc(object item)
        {
            if (item == null)
            {
                return "[NULL]";
            }
            return $"[{item.GetType().FullName}:{item.GetHashCode()}]";
        }
    }
}
