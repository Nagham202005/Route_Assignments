using System;
using System.Collections.Generic;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Assignment solved in regions below. Use Solution Explorer to navigate.");
            Console.ReadKey();
        }
    }

    #region Q1: What is a generic class? Why use generics?
    /*
     * A generic class is a class that can work with any data type. 
     * We use generics to achieve:
     * 1. Type Safety (errors caught at compile time).
     * 2. Performance (prevents boxing/unboxing).
     * 3. Code Reusability (write once, use for many types).
     */
    #endregion

    #region Q2: Generic class Container<T>
    public class Container<T>
    {
        private T _item;
        public void Add(T item) => _item = item;
        public T Get() => _item;
    }
    #endregion

    #region Q3: Multiple type parameters Pair<TKey, TValue>
    public class Pair<TKey, TValue>
    {
        public TKey Key { get; set; }
        public TValue Value { get; set; }
    }
    #endregion

    #region Q4: Generic method Swap<T>
    public static class Helper
    {
        public static void Swap<T>(ref T a, ref T b)
        {
            T temp = a;
            a = b;
            b = temp;
        }
    }
    #endregion

    #region Q5: Generic method FindMax<T>
    public static class MathHelper
    {
        public static T FindMax<T>(T a, T b) where T : IComparable<T>
        {
            return a.CompareTo(b) > 0 ? a : b;
        }
    }
    #endregion

    #region Q6: Generic interface IRepository<T>
    public interface IRepository<T>
    {
        void Add(T entity);
        T GetById(int id);
        IEnumerable<T> GetAll();
    }
    #endregion

    #region Q7: 'struct' constraint (Value Type)
    public class ValueHandler<T> where T : struct
    {
        public T Data { get; set; }
    }
    #endregion

    #region Q8: 'class' constraint (Reference Type)
    public class ReferenceHandler<T> where T : class
    {
        public T Data { get; set; }
    }
    #endregion

    #region Q9: 'new()' constraint

    public class Factory<T> where T : new()
    {
        public T Create() => new T();
    }
    #endregion

    #region Q10: Interface constraint
    public class Processor<T> where T : IDisposable
    {
        public void CleanUp(T obj) => obj.Dispose();
    }
    #endregion

    #region Q11: Base class constraint
    public class BaseClass { }
    public class DerivedClass : BaseClass { }

    public class BaseHandler<T> where T : BaseClass
    {
        
    }
    #endregion

    #region Q12: Multiple constraints
    public class MultiHandler<T> where T : class, IComparable<T>, new()
    {
        
    }
    #endregion

    #region Q13: 'default' keyword
    /*
     * The 'default' keyword returns the default value for a type:
     * - Null for reference types.
     * - Zero for numeric value types.
     */
    #endregion

    #region Q14: SafeList<T> with default return
    public class SafeList<T>
    {
        private List<T> _items = new List<T>();
        public void Add(T item) => _items.Add(item);

        public T GetAt(int index)
        {
            if (index < 0 || index >= _items.Count)
                return default(T);
            return _items[index];
        }
    }
    #endregion

    #region Q15: Covariance (out keyword)
    /*
     * Covariance allows using a more derived type than specified.
     * It uses the 'out' keyword and is only for return types.
     */
    public interface ICovariant<out T>
    {
        T GetItem();
    }
    #endregion

    #region Q16: Contravariance (in keyword)
    /*
     * Contravariance allows using a less derived type than specified.
     * It uses the 'in' keyword and is only for input parameters.
     */
    public interface IContravariant<in T>
    {
        void SetItem(T item);
    }
    #endregion

    #region Q17: Difference between Covariance and Contravariance
    /*
     * Covariance (out): Used for Output (Return values).
     * Contravariance (in): Used for Input (Parameters).
     */
    #endregion

    #region Q18: Static members in generic types
    public class GenericStatic<T>
    {
        public static int Count;
    }
    #endregion

    #region Q19: Inheriting from a generic class
    // 1. Closed inheritance
    public class IntContainer : Container<int> { }

    // 2. Open inheritance
    public class SpecializedContainer<T> : Container<T> { }
    #endregion

    #region Q20: Exercise - Cache<TKey, TValue>
    public class CacheItem<TValue>
    {
        public TValue Value { get; set; }
        public DateTime Expiration { get; set; }
    }

    public class Cache<TKey, TValue>
    {
        private readonly Dictionary<TKey, CacheItem<TValue>> _store = new Dictionary<TKey, CacheItem<TValue>>();

        public void Add(TKey key, TValue value, TimeSpan duration)
        {
            _store[key] = new CacheItem<TValue>
            {
                Value = value,
                Expiration = DateTime.Now.Add(duration)
            };
        }

        public TValue Get(TKey key)
        {
            if (Contains(key))
                return _store[key].Value;
            return default;
        }

        public bool Contains(TKey key)
        {
            if (_store.ContainsKey(key))
            {
                if (DateTime.Now < _store[key].Expiration)
                    return true;

                _store.Remove(key); // Auto-remove if expired
            }
            return false;
        }

        public void Remove(TKey key) => _store.Remove(key);
    }
    #endregion
}