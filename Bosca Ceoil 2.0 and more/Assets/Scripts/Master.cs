using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Master : MonoBehaviour {

    public List<Note> notes = new List<Note>();
    public int bpm = 120;

    public int thisSample;
    public int thisTick;
    public int arrangement_length = 64;
    public int arrangement_lengthInSamples = 1;
    public int sampling_frequency;
    public int degreeOfError = 256;
    public int NextRevStartTick;

    public Transform notePrefab;

    private void Start()
    {
        sampling_frequency = AudioSettings.outputSampleRate;
        arrangement_lengthInSamples = NoteHelper.SixteenthToSamples(arrangement_length,bpm);
        

        UpdateNoteList();
        
    }

    private void Update() //TODO: should probably offload this to some manager
    {
        if (Input.GetKey(KeyCode.LeftAlt) == false) {

            Vector2 mousePos = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
            Vector2 notePos = NoteHelper.SnapPosToGrid(mousePos.x - 0.5f, mousePos.y);
            if (notePos.x <= 0) notePos.x = 0;

            

            if (Input.GetKeyDown(KeyCode.Mouse0) == true)
            {

               
            }
        }
        else //move camera
        {
            Vector3 campos = Camera.main.transform.position;

            if (Input.GetMouseButton(0))
            {
                campos -= new Vector3(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"),0);
            }

            campos.z += Input.GetAxis("Mouse ScrollWheel");
        }
    }

    public void AddNoteAtPos(Vector2 notePos, int duration)
    {
        GameObject go = Instantiate(notePrefab.gameObject, new Vector3(notePos.x, notePos.y), Quaternion.identity);

        Note n = go.GetComponentInChildren<Note>();
        n.pitch = 36 + Mathf.RoundToInt(notePos.y);


        Debug.Log("Pitch " + n.pitch + " is " + NoteHelper.PitchToNote(n.pitch));
        n.startPoint = Mathf.RoundToInt(notePos.x);//FIXME
        n.duration = duration;

        SpriteRenderer spr = go.GetComponentInChildren<SpriteRenderer>();
        spr.size = new Vector2(duration, 1);

        notes.Add(n);
    }


    void UpdateNoteList()
    {
        foreach( Note n in FindObjectsOfType<Note>())
        {
            if(notes.Contains(n) == false)
            {
                notes.Add(n);
            }
        }
            
        
    }
    void UpdateNoteStartEnd(Note n, int currDspTick)
    {

        //TODO: notes overlapping two beats
        NextRevStartTick = (int)(((currDspTick / arrangement_lengthInSamples)) * arrangement_lengthInSamples);

        n.startTick = NextRevStartTick + NoteHelper.SixteenthToSamples(n.startPoint, bpm);
        n.endTick = n.startTick + NoteHelper.SixteenthToSamples(n.duration, bpm);
    }


    private void OnAudioFilterRead(float[] data, int channels)
    {

        // thisSample = Mathf.RoundToInt((float)(AudioSettings.dspTime * (double)sampling_frequency));
        thisTick = Mathf.RoundToInt((float)(AudioSettings.dspTime * (double)sampling_frequency));
        foreach (Note n in notes)
        {
            if(thisTick >= n.endTick)
            {
                
                UpdateNoteStartEnd(n,thisTick);
            }
        }
    }
}
