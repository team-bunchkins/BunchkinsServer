using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace System.Linq
{
    public static class DbSetExtensions
    {
        public static T GetRandomElement<T>(this IQueryable<T> dbSet, Expression<Func<T, int>> lambda) where T : class
        {
            var rand = new Random();
            var skip = (int)(rand.NextDouble() * dbSet.Count());
            return dbSet.OrderBy(lambda).Skip(skip).Take(1).First();
        }
    }
}
