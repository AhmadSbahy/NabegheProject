using Hanssens.Net;
using Nabeghe.Web.Contracts;

namespace MyEshop_Clean.MVC.Base
{
    public class LocalStorageService : ILocalStorageService
    {
        private readonly LocalStorage _storage;

        public LocalStorageService()
        {
            var config = new LocalStorageConfiguration()
            {
                AutoLoad = true,
                AutoSave = true,
                Filename = "MyEshop_Clean",

            };
            _storage = new LocalStorage(config);
        }
        public void ClearStorage(List<string> keys)
        {
            foreach (var key in keys)
            {
                _storage.Remove(key);
            }
        }

        public bool IsExists(string key)
        {
            return _storage.Exists(key);
        }

        public T GetStorageValue<T>(string key)
        {
            return _storage.Get<T>(key);
        }

        public void SetStorageValue<T>(string key, T type)
        {
            _storage.Store(key, type);
            _storage.Persist();
        }
    }
}
