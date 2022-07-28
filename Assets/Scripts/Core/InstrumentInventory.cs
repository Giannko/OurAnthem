using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Instrument Inventory", fileName = "new Instrument Inventory")]

public class InstrumentInventory : ScriptableObject
{

public event Action onInstrumentAdded;

    List<InstrumentSO> instruments = new List<InstrumentSO>();

    private void OnEnable() {
        instruments.Clear();
    }

    public void AddToInstrumentList(InstrumentSO instrument)
    {
        instruments.Add(instrument);
        
        onInstrumentAdded();
    }

    public List<InstrumentSO> GetAllInstruments()
    {
        return instruments;
    }

}
