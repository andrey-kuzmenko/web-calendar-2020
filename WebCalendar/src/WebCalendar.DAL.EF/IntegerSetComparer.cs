using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace WebCalendar.DAL.Models
{
    public class IntegerSetComparer : ValueComparer<ICollection<int>>
    {
        public IntegerSetComparer() : base(true)//: base(GetEqualsExpression(), GetHashCodeExpression(), GetSnapshotExpression())
        {

        }

      /*  private static Expression<Func<ICollection<int>, ICollection<int>, bool>> 
            GetEqualsExpression() => (set1, set2) => set1.SequenceEqual(set2);
        private static Expression<Func<ICollection<int>, int>> GetHashCodeExpression() => set => GetICollectionHashCode(set);
        private static Expression<Func<ICollection<int>, ICollection<int>>> GetSnapshotExpression() => set => new HashSet<int>(set);
       
        private static int GetICollectionHashCode(ICollection<int> set)
        {
            int hashCode = 0;

            foreach (int item in set)
            {
                hashCode ^= item.GetHashCode();
            }

            return hashCode;
        }*/
    }
}
