using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Note))]
public class CustomEditor_Note : Editor {

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        Note myNote = (Note)target;
        if (GUILayout.Button("Play"))
        {
            myNote.startTick = AudioSettings.dspTime * 48000;
            myNote.endTick = myNote.startTick + NoteHelper.SixteenthToSamples(myNote.duration, 120);
        }
    }
}
