using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPHMMaker.Items
{
    internal class ItemPairReport 
    {
        public string Value
        {
            get
            {
                string s = "";
                if (pairs.Count == 0) return s;
                s += pairs[0].Item1 + ": " + pairs[0].Item2;
                for (int i = 1; i < pairs.Count; i++)
                {
                    s += "\n" + pairs[i].Item1 + ": " + pairs[i].Item2;
                }
                return s;
            }
        }

        public string StringsOnly
        {
            get
            {
                string s = "";
                if (pairs.Count == 0) return s;

                s += pairs[0].Item1;
                for (int i = 1; i < pairs.Count; i++)
                {
                    s += "\n" + pairs[i].Item1;
                }
                return s;
            }
        }

        public string NumbersOnly
        {
            get
            {
                string s = "";
                if (pairs.Count == 0) return s;

                s += pairs[0].Item2;
                for (int i = 1; i < pairs.Count; i++)
                {
                    s += "\n" + pairs[i].Item2;
                }
                return s;
            }
        }

        List<(string, double)> pairs;


        public ItemPairReport()
        {
            pairs = new List<(string, double)>();
        }

        public void AddLine(string aReport, double aValue)
        {
             
            pairs.Add((aReport, aValue));
        }

        public override string ToString()
        {
            return Value;
        }
    }
}
