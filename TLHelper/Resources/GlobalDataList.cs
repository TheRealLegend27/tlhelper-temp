using System;
using System.Collections.Generic;
using System.Linq;

namespace TLHelper.Resources
{
    public class GlobalDataList<KE, VE>
    {
        private readonly Dictionary<KE, VE> Data = new Dictionary<KE, VE>();

        public void AddData(KE key, VE value)
        {
            Data.Add(key, value);
        }

        public VE GetValue(KE key)
        {
            Console.WriteLine("Requrested Value-Key: " + key);
            return Data[key];
        }

        public KE GetKey(VE value)
        {
            return Data.Keys.Where(key => Data[key].Equals(value)).FirstOrDefault();
        }

        public VE[] GetValues()
        {
            return Data.Values.ToArray<VE>();
        }

        public KE[] GetKeys()
        {
            return Data.Keys.ToArray<KE>();
        }

        public bool ContainsKey(KE key) => Data.ContainsKey(key);
        public bool ContainsValue(VE val) => Data.Keys.Where(key => Data[key].Equals(val)).Count() != 0;

    }
}
