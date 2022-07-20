using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIConversant : MonoBehaviour, IRaycastable
{
    [SerializeField] DialogueManager dialogueManager;
    [SerializeField] TextAsset inkJSON;
    
    [SerializeField] InstrumentSO instrumentToBeGiven = null;

    public bool HandleRaycast(PlayerController playerController)
    {
        
        if (Input.GetMouseButtonDown(0))
        {
            dialogueManager.gameObject.SetActive(true);
            dialogueManager.StartDialogue(inkJSON, instrumentToBeGiven);
        }
        return true;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
