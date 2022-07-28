using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/AudioInventory", fileName = "new Audio Inventory")]

public class AudioInventorySO : ScriptableObject
{

    [SerializeField] List<AudioTrackSO> audioTracks = new List<AudioTrackSO>();

    private void Awake() {
        
    }
    
    public List<AudioTrackSO> GetAllTracks()
    {
        return audioTracks;
    }

}
