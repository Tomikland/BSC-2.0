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

            while (true)
            {
                BendCurve bc = new BendCurve();
                while (true)
                {
                    string input = Console.ReadLine();
                    if (input == "")
                    {
                        break;
                    }

                    int spacePos = input.IndexOf(' ');


                    float inputX = float.Parse(input.Substring(0, spacePos));
                    float inputY = float.Parse(input.Substring(spacePos + 1, input.Length - spacePos - 1));

                    bc.AddValue(inputX, inputY);
                }



                bc.WriteCurve(100, 25);
                
            }
        }
    }
    class BendCurve
    {
        List<KeyValuePair<float,float>> values = new List<KeyValuePair<float,float>>();

        public BendCurve()
        {
            
            
        }

        public void AddValue(float x, float y)
        {
            KeyValuePair<float, float> kv = new KeyValuePair<float, float>(x,y);
            values.Add(kv);
            SortList();

            Console.WriteLine("Added value " + y + " at position " + x);
        }

        void SortList()
        {
            values = values.OrderBy(o => o.Key).ToList();
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

        public void WriteCurve(int xSize, int ySize)
        {

            int offset = Console.CursorTop;

            for (int i = 0; i <= xSize; i++)
            {
                float xPos = (float)i / xSize;
                float yPos = GetValueAt(xPos);

                Console.SetCursorPosition( i, ySize - (int)(yPos*ySize)  + offset);
                Console.Write("@");
            }

            Console.SetCursorPosition(0, offset + ySize + 3);
        }

    }
}
