using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Note : MonoBehaviour{


    //TODO: clean up casts, use doubles everywhere

    public Master master;
    public int bpm;
    public bool playing = false;

    public int overtones = 5;


    public int pitch = 36;
    public int duration = 16; //duration in sixteenth notes
    public int durationInSamples = -1;
    public double durationInSeconds = -1;
    public float maxVolume = 1;
    public float percentage; //Needs a better name: how far along we are in the note's duration, between 0 and 1
    public int currTick;

    public int startPoint = 0; 
    Instrument instrument;

    AudioSource src;
    AudioClip clip;

    public double startTick;
    public double endTick;
    public void Play()
    {
       
        if (instrument.sampler == false)
        {
            int currTick = (int)(AudioSettings.dspTime * sampling_frequency);
            startTick = currTick % master.arrangement_lengthInSamples + NoteHelper.SixteenthToSamples(startPoint, bpm);

            endTick = startTick + NoteHelper.SixteenthToSamples( duration,bpm);

        }
        else
        {
            src.PlayOneShot(instrument.FindAudioClip(pitch));
        }
    }
    public  float increment = 0f;
    public  float phase = 0f;
    public  double frequency;
    public  double sampling_frequency = 48000f;
    public  int sampleCounter = 0;
    public int samplesPassed;



    
    private void OnAudioFilterRead(float[] data, int channels)
    {
        //TODO: start and check duration counter
        currTick = (int)(AudioSettings.dspTime * sampling_frequency);

        increment = (float)(frequency * 2f * Mathf.PI / sampling_frequency);
        //Debug.Log("increment: "+ increment);
        for (int i = 0; i < data.Length; i += channels)
        {
            phase += increment;
            percentage = (float)(currTick - startTick) / (float)(NoteHelper.SixteenthToSamples( duration, bpm));

            if (currTick > startTick && currTick < endTick) //only play the note if we are inside its bounds
            {
                float gain = Mathf.Sin(percentage * Mathf.PI) - 0.1f;

                for (int ot = 1; ot < overtones + 1; ot++)
                {

                data[i] += instrument.PhaseValue((phase * ot) % (2 * Mathf.PI), 0.08f * gain);

                }

            }
            
            if (channels == 2)
            {
                data[i + 1] += data[i];
            }
            if (phase > 2f * Mathf.PI)
            {
                phase = 0f;
            }
            currTick++;
            
        }


    }

    public Note(int p, int d, Instrument i)
    {
        pitch = p;
        duration = d;
        instrument = i;
    }

    void Start()
    {
        sampling_frequency = AudioSettings.outputSampleRate;

        master = FindObjectOfType<Master>();
        this.bpm = master.bpm;
        instrument = new SineWave();
        
        
        
        Play();
    }
    private void Update()
    {
        frequency = NoteHelper.PitchToFreq(pitch);
    }
}

