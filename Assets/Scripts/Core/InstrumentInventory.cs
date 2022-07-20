using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Instrument Inventory", fileName = "new Instrument Inventory")]

public class InstrumentInventory : ScriptableObject
{

    List<InstrumentSO> instruments = new List<InstrumentSO>();

    public void AddToInstrumentList(InstrumentSO instrument)
    {
        instruments.Add(instrument);
        Debug.Log("Added instrument " + instruments[0].GetInstrumentName());
    }

}
