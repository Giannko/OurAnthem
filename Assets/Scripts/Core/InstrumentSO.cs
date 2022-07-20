using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Instruments", fileName = "new Instrument")]

public class InstrumentSO : ScriptableObject
{
    [SerializeField] string instrumentName;
    [SerializeField] bool aquired;

    public void ChangeAquired(bool hasBeenAquired)
    {
        aquired = hasBeenAquired;
    }

    public string GetInstrumentName()
    {
        return instrumentName;
    }
}

