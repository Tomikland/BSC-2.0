using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class BendCurve
//iterate over a sorted list until we find the first position larger than our input
//get the two known values surrounding the target position, interpolate if needed
{
    List<KeyValuePair<float, float>> values = new List<KeyValuePair<float, float>>();

    public BendCurve()
    {


    }

    public void AddValue(float x, float y)
    {
        KeyValuePair<float, float> kv = new KeyValuePair<float, float>(x, y);
        values.Add(kv);
        SortList();

        Debug.Log("Added value " + y + " at position " + x);
    }

    void SortList()
    {
        values = values.OrderBy(o => o.Key).ToList();
    }

    public float GetValueAt(float x)
    {
        KeyValuePair<float, float> low = new KeyValuePair<float, float>(0, 0);
        KeyValuePair<float, float> high = new KeyValuePair<float, float>(1, values[values.Count - 1].Value);

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

    public void WriteToTexture(Texture2D texture, Color fgColor, Texture2D bgTexture)
    {

        //reset the pixels we changed (between high and low)

        int xSize = texture.width;
        int ySize = texture.height;

        

        for (int i = 0; i <= xSize; i++)
        {
            float xPos = (float)i / xSize;
            float yPos = GetValueAt(xPos);

            texture.SetPixel(i, ySize - (int)(yPos * ySize),fgColor);
            Debug.Log("Set pixel at"+ i +" " + (ySize - (int)(yPos * ySize)));
        }

        texture.Apply();
    
    }

}
