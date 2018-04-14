//using System;
//using System.Collections.Generic;
//using System.Text;
//using SQLite;
//using Xamarin.Forms;

//namespace MetinGo.Infrastructure.Database
//{
//    public class DatabaseProvider : IDatabaseProvider
//    {
//        private SQLiteAsyncConnection _connectionCache;

//        public SQLiteAsyncConnection Database
//        {
//            get
//            {
//                if (_connectionCache != null)
//                    return _connectionCache;
//                var databasePath = DependencyService.Get<IDatabasePathProvider>().GetPath();
//                _connectionCache = new SQLiteAsyncConnection(databasePath);
//                return _connectionCache;
//            }
//        }
//    }
//}