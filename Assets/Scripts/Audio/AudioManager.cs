using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] InstrumentInventory instrumentInventory;
    [SerializeField] AudioInventorySO audioInventory;
    private AudioClip currentAudioClip = null;
    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        instrumentInventory.onInstrumentAdded += DetermineBackingTrack;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DetermineBackingTrack()
    {
        bool foundTrack = false;
        
        foreach(AudioTrackSO audioTrack in audioInventory.GetAllTracks())
        {
            if (audioTrack.GetRequiredInstruments().Count == instrumentInventory.GetAllInstruments().Count)
            {
                Debug.Log(audioTrack.GetRequiredInstruments().Count);
                foreach(InstrumentSO instrument in audioTrack.GetRequiredInstruments())
                {
                    Debug.Log("Checking instrument");
                    if (!instrumentInventory.GetAllInstruments().Contains(instrument))
                    {
                        Debug.Log("Different instrument");

                        foundTrack = false;
                        break;
                    }
                    else
                    {
                        Debug.Log("Same number");
                        foundTrack = true;
                    }
                }
            }
            if (foundTrack)
            {
                Debug.Log("Change Audio Track");
                
                ChangeBackingAudio(audioTrack.GetTrack());
                break;
            } 
        }
    }

    private void ChangeBackingAudio(AudioClip audioClip)
    {
        currentAudioClip = audioClip;
        audioSource.clip = currentAudioClip;
        audioSource.Play();
    }
}
