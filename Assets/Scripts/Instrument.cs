using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instrument  {

    public bool sampler = false;
    public virtual float PhaseValue( float phase, float gain = 0.03f)
    {
        return 0;
    }

    public AudioClip FindAudioClip(int pitch) {
        return null;
    }
}
