using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridLines : MonoBehaviour {

    public Camera cam;
    public Vector2Int campos = new Vector2Int();
    
    public Transform gridlinePrefab;
    public List<GameObject> gridx = new List<GameObject>();
    public List<GameObject> gridy = new List<GameObject>();

    // Update is called once per frame
    void Update () {
        campos.x = Mathf.RoundToInt(cam.transform.position.x);
        campos.y = Mathf.RoundToInt(cam.transform.position.y);

        int ySize = Mathf.CeilToInt(cam.orthographicSize + 1);
        int xSize = Mathf.CeilToInt(ySize * cam.aspect);

        int bot = campos.y - ySize;
        int left = campos.x - xSize;

        //horizontal grid lines
        for (int i = 0; i < ySize*2; i++) 
        {
            GameObject go;
            if (i >= gridx.Count)
            {
                go = Instantiate(gridlinePrefab.gameObject);
                go.transform.parent = this.transform;
            }
            else
            {
                go = gridx[i];
            }

            gridx.Add(go);

            Vector3 pos = Vector3.zero;
            pos.y = bot + i + 0.5f;
            pos.x = campos.x;
            pos.z = 10;

            go.transform.position = pos;
        }

        //vertical
        for (int i = 0; i < xSize * 2; i++)
        {
            GameObject go;
            if (i >= gridy.Count)
            {
                go = Instantiate(gridlinePrefab.gameObject);

                go.transform.Rotate(0,0,90);
                go.transform.parent = this.transform;
            }
            else
            {
                go = gridy[i];
            }

            gridy.Add(go);

            Vector3 pos = Vector3.zero;
            pos.x = left + i;
            pos.y = campos.y;
            pos.z = 10;
            go.transform.position = pos;

            Vector3 scale = go.transform.localScale;
            scale.y = 0.05f;

            for (int power = 5; power > 1; power--)
            {
                if (pos.x % (int)Mathf.Pow(2, power) == 0)
                {
                    scale.y = 0.05f * power;
                    break;
                }
            }

            go.transform.localScale = scale;
        }
    }

}
