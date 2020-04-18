using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace WebCalendar.DAL.Models
{
    public class IntegerSetComparer : ValueComparer<ISet<int>>
    {
        public IntegerSetComparer() : base(GetEqualsExpression(), GetHashCodeExpression(), GetSnapshotExpression())
        {

        }

        private static Expression<Func<ISet<int>, ISet<int>, bool>> GetEqualsExpression() => (set1, set2) => set1.SetEquals(set2);
        private static Expression<Func<ISet<int>, int>> GetHashCodeExpression() => set => GetISetHashCode(set);
        private static Expression<Func<ISet<int>, ISet<int>>> GetSnapshotExpression() => set => new HashSet<int>(set);
       
        private static int GetISetHashCode(ISet<int> set)
        {
            int hashCode = 0;

            foreach (int item in set)
            {
                hashCode ^= item.GetHashCode();
            }

            return hashCode;
        }
    }
}
