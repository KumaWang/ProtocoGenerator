using System;
#if MOD
namespace iuiu.mod
#else
namespace System.Collections.Generic
#endif
{
    [Serializable]
    public class IndexList<T> : System.Collections.Generic.List<T>
    {
        public new T this[int index] 
        {
            get 
            {
                for (int i = base.Count; i <= index; i++)
                    base.Add((T)System.Activator.CreateInstance(typeof(T)));

                return base[index];
            }
            set 
            {
                if (index >= base.Count)
                {
                    for (int i = base.Count; i < index; i++) 
                        base.Add(default(T));

                    base.Add(value);
                }
                else
                {
                    base[index] = value;
                }
            }
        }

        public new void Insert(int index, T item) 
        {
            for (var i = this.Count; i >= index; i--)
                this[i + 1] = this[i];

            this[index] = item;
        }
    }
}
