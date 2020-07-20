using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Task2.DataAccess.Models;

namespace Task2.DataAccess.Repository
{
    public class TABITEMRepository
    {
        private readonly Task2DbContext task2DbContext;

        public TABITEMRepository(Task2DbContext task2DbContext)
        {
            this.task2DbContext = task2DbContext;
        }

        public IEnumerable<TABITEM> GetAll()
        {
            return task2DbContext.TABITEM.ToList();
        }
    }
}
