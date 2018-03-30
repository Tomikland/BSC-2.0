using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class NoteHelper {


    static int sampling_frequency = 48000; //FIXME SOMEHOW 
    public static  float semitoneFreqRat = Mathf.Pow(2f, 1f / 12f);

    public static string[] noteNames = { "C", "C#", "D", "D#", "E", "F", "F#", "G", "G#", "A", "A#", "B", };

    public static double PitchToFreq(int pitch)  //pitch 0 is C1
    {
        double C2 = 32.70f;
        if (pitch == 0) { return C2; }

        double  freq = C2 * Mathf.Pow(semitoneFreqRat, pitch);
        //Debug.Log("the frequency of pitch " + pitch + " is " + freq);
        return freq;
    }
    public static double SixteenthToSeconds(int d, int bpm)
    {
        return (d / 16f) / (bpm / 60f)*4;
    }
    public static int SixteenthToSamples(int d, int bpm)
    {
        return Mathf.RoundToInt((float)(SixteenthToSeconds(d, bpm) * sampling_frequency));
    }

    public static Vector2 SnapPosToGrid(float x, float y)
    {
        return new Vector2(Mathf.Round(x), Mathf.Round(y));
    }

    public static string PitchToNote(int pitch)
    {
        return noteNames[(pitch) % 12];
    }



    /*public static int PositionToPitch(float y) // middle C (36) is 0
    {
        int middle = 36;

        return middle + Mathf.RoundToInt(y);
    }*/
}
