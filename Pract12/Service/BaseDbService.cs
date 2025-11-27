using Pract12.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pract12.Service
{
    internal class BaseDbService
    {
        private BaseDbService()
        {
            context = new AppDbContext();
        }

        private static BaseDbService? instance;
        public static BaseDbService Instance
        {
            get
            {
                if (instance == null)
                    instance = new BaseDbService();
                return instance;
            }
        }
        private AppDbContext context;
        public AppDbContext Context => context;

    }
}
