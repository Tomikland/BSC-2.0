using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class SineWave : Instrument{

	public override float PhaseValue( float phase, float gain = 0.03f)
    {
       // Debug.Log("Phase: "+ phase+" Sine value:"+ gain * Mathf.Sin(phase));
       
        return Mathf.Clamp01(gain) * Mathf.Sin(phase);
    }
    
}
