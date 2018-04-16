using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BendCurve
{
    class Program
    {
        static void Main(string[] args)
        {
            BendCurve bc = new BendCurve();

            while (true)
            {
                Console.WriteLine( bc.GetValueAt(float.Parse(Console.ReadLine())));
            }
        }
    }
    class BendCurve
    {
        List<KeyValuePair<float,float>> values = new List<KeyValuePair<float,float>>();

        public BendCurve()
        {
            AddValue(0, 0);
            AddValue(0.5f, 1);
            AddValue(1, 0.5f);
        }

        public void AddValue(float x, float y)
        {
            KeyValuePair<float, float> kv = new KeyValuePair<float, float>(x,y);
            values.Add(kv);
            SortList();
        }

        void SortList()
        {

        }

        public float GetValueAt(float x)
        {
            KeyValuePair<float, float> low = new KeyValuePair<float, float>(0,0);
            KeyValuePair<float, float> high= new KeyValuePair<float, float>(1,values[values.Count - 1].Value);
            
                for (int i = 0; i < values.Count; i++)
                {
                    if (values[i].Key > x)
                    {
                        low = values[i - 1];
                        high = values[i];
                        break;
                    }

                }
            
            float length = high.Key - low.Key;


            //interpolation
            float val = low.Value * (high.Key - x) + high.Value * (x - low.Key);
            val /= length;

            return val;
        }

    }
}
