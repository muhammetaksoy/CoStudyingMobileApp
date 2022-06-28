using CoStudying.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoStudying
{
    public class DBContextMethods
    {
        public static DatabaseContext GetDatabaseContext()
        {
            DatabaseContext context = new DatabaseContext();
            context.Configuration.LazyLoadingEnabled = false;
            return context;
        }


    }
}