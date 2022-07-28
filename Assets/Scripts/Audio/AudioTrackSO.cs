using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Scriptable Objects/AudioTrack", fileName = "new Audio Track")]

public class AudioTrackSO : ScriptableObject
{

    [SerializeField] AudioClip track;
    [SerializeField] List<InstrumentSO> requiredInstruments = new List<InstrumentSO>();

    private void Awake() {
        
    }

    public List<InstrumentSO> GetRequiredInstruments()
    {
        return requiredInstruments;
    }

    public AudioClip GetTrack()
    {
        return track;
    }

}

