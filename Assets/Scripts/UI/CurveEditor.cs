using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurveEditor : MonoBehaviour {

    public Image img;
    public SpriteRenderer renderer;
    Texture2D texture;
    public Texture2D bgTexture;

    // Use this for initialization
    void Start () {

        BendCurve bc = new BendCurve();
        bc.AddValue(0, 0);
        bc.AddValue(1, 1);


        texture = new Texture2D(100, 100);
        bc.WriteToTexture(texture, Color.black,bgTexture);

        renderer.sprite = Sprite.Create(texture, new Rect(0, 0, 100, 100), new Vector2(0.5f, 0.5f),100);
        
            
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
