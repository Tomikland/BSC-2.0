using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteNames : MonoBehaviour {

    Transform[] octaves = new Transform[5];

	// Use this for initialization
	void Start () {
        cam = Camera.main;

        for (int i = 0; i < octaves.Length; i++)
        {
            GameObject go = new GameObject();
            go.name = "octave" + i;

            Transform tr = go.transform;

            tr.parent = this.transform;
            tr.position = new Vector3(0,i*12,0);

            octaves[i] = tr;

            CreateOctave(tr);
        }


        
	}

    float campos;
    public Transform namePrefab;

    List<GameObject> names = new List<GameObject>();
        Camera cam;

    // Update is called once per frame
    void Update () {

        campos = Camera.main.transform.position.y;
        //int top = Mathf.CeilToInt(cam.orthographicSize);
        int bot = Mathf.FloorToInt(campos - cam.orthographicSize);

        for (int i = 0; i < octaves.Length; i++)
        {
            octaves[i].position = new Vector3(0,((bot / 12) * 12) - 12 + i * 12);
        }

    }

    void CreateOctave(Transform parent)
    {

        for (int i = 0; parent.childCount < 12; i++)
        {

            GameObject go = Instantiate(namePrefab.gameObject,parent);

           
        }

        for (int i = 0; i < 12; i++)
        {
            GameObject go = parent.GetChild(i).gameObject;

            go.transform.position += new Vector3(0, i, 0);

            int pitch = 36 + i;

            TextMesh txtm = go.GetComponentInChildren<TextMesh>();

            txtm.text = NoteHelper.PitchToNote(pitch);
        }
    }
}
