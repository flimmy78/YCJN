using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SAC.Helper
{
    /// <summary>
    /// 对象相等比较的委托。
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    public delegate bool EqualsComparer<T>(T x, T y);

    /// <summary>
    /// 对象的相等比较。
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class EqualCompare<T> : IEqualityComparer<T>
    {
        private EqualsComparer<T> _equalsComparer;

        public EqualCompare(EqualsComparer<T> equalsComparer)
        {
            this._equalsComparer = equalsComparer;
        }

        public bool Equals(T x, T y)
        {
            if (this._equalsComparer != null)
            {
                return this._equalsComparer(x, y);
            }
            else
            {
                return false;
            }
        }

        public int GetHashCode(T obj)
        {
            return obj.ToString().GetHashCode();
        }
    }
}
