using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Task2.DataAccess.Models;

namespace Task2.DataAccess.Repository
{
    public class TABCUSTRepository
    {
        private readonly Task2DbContext task2DbContext;

        public TABCUSTRepository(Task2DbContext task2DbContext)
        {
            this.task2DbContext = task2DbContext;
        }

        public IEnumerable<TABCUST> GetAll()
        {
            return task2DbContext.TABCUST.ToList();
        }

        public TABCUST GetById(decimal id)
        {
            var cust = task2DbContext.TABCUST.Find(id);
            if(cust == null)
            {
                cust = task2DbContext.TABCUST.Find(1);
            }
            return cust;
        }
    }
}
