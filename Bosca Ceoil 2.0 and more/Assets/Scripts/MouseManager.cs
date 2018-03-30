using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseManager : MonoBehaviour {

    
    public Transform noteGhost;
    SpriteRenderer ngspr;
    int duration;

    public Master master;

    public float scrollSpeed;

    // Use this for initialization
    void Start () {
        ngspr = noteGhost.GetComponentInChildren<SpriteRenderer>();

    }
	
	// Update is called once per frame
	void Update () {
		
    Vector2 mousePos = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
    Vector2 notePos = NoteHelper.SnapPosToGrid(mousePos.x - 0.5f, mousePos.y);
        if (Input.GetKey(KeyCode.LeftAlt) == false)
        {
            duration += Mathf.RoundToInt(Input.GetAxisRaw("Mouse ScrollWheel") * 10);
            if (duration <= 0) duration = 1;

            ngspr.size = new Vector2(duration, 1);

            if (notePos.x <= 0) notePos.x = 0;

            noteGhost.position = new Vector3(notePos.x, notePos.y);

            if (Input.GetKeyDown(KeyCode.Mouse0) == true)
            {
                master.AddNoteAtPos(notePos,duration);
               
            }
        }
        else //move camera
        {
            Vector3 campos = Camera.main.transform.position;

            if (Input.GetMouseButton(0))
            {
                campos -= new Vector3(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"), 0);
            }
            Camera.main.orthographicSize -= Input.GetAxis("Mouse ScrollWheel") * scrollSpeed;

            Camera.main.transform.position = campos;
        }
	}

    Note FindNoteUnderMouse(Vector2 mousePos) //TODO
    {
        return null;
    }
}
