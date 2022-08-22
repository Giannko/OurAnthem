using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;
using System;
using TMPro;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour, IAction
{

    
    private Story story;

    [SerializeField] GameObject dialogueArea;
    [SerializeField] ScrollRect scrollRect;
    [SerializeField] TextMeshProUGUI dialogueTextPrefab;
    [SerializeField] Button dialogueButtonPrefab;
    [SerializeField] Button nextButton;
    [SerializeField] InstrumentInventory instrumentInventory;
    [SerializeField] GameObject viewportContent;
    [SerializeField] Mover mover;
    [SerializeField] float distanceToSpeaker = 4f;
    [SerializeField] ActionScheduler actionScheduler;
    [SerializeField] PlayerController player;

    private InstrumentSO instrumentToBeGiven = null;
    private bool hasInstrumentBeenAcquired = false;
    private const string gotInstrumentTag = "acquired_instr";
    private string speaker = "";
    private string speakerTag = "speaker: ";
    private AIConversant aIConversant;
    private bool inDialogue = false;
    
    


    // Start is called before the first frame update
    void Start()
    {
        
    }

    

    // Update is called once per frame
    void Update()
    {
        if (aIConversant == null) return;
        if (inDialogue) return;
        if (!GetIsInRange(aIConversant.transform))
            {
                mover.MoveTo(aIConversant.transform.position, 1f);
            }
            else
            {
                mover.Cancel();
                StartDialogue();
            }
    }

    public void RefreshUI()
    {
        ClearUI();

        TextMeshProUGUI storyText = Instantiate(dialogueTextPrefab) as TextMeshProUGUI;

        storyText.text = LoadStoryChunk();
        //storyText.transform.SetParent(dialogueArea.transform, false);
        storyText.transform.SetParent(viewportContent.transform, false);
        

        if (story.currentChoices.Count == 0 && story.canContinue)
        {
            nextButton.gameObject.SetActive(true);
        }

        else

        {
            nextButton.gameObject.SetActive(false);
        }

        foreach (Choice choice in story.currentChoices)
        {
            Button choiceButton = Instantiate(dialogueButtonPrefab) as Button;
            //choiceButton.transform.SetParent(dialogueArea.transform, false);
            choiceButton.transform.SetParent(viewportContent.transform, false);
            TextMeshProUGUI choiceText = choiceButton.GetComponentInChildren<TextMeshProUGUI>();
            choiceText.text = choice.text;

            choiceButton.onClick.AddListener(delegate
            {
                ChooseStoryChoice(choice);
            });
        }
        Debug.Log("whaaaat");
        StartCoroutine(ScrollToBottom());
    }

    string LoadStoryChunk() //returning an empty string or the next chunk of the story
    {

        string text = "";
        if (story.canContinue)
        {
            text = story.Continue();
            text = "<b>" + GetSpeaker(story.currentTags) + "</b>" + text;
            HandleTags(story.currentTags);
        }
        
        return text;
    }

    private string GetSpeaker(List<string> currentTags)
    {
        foreach (string currentTag in currentTags)
        {
            if (currentTag.Contains(speakerTag))
            {
                return currentTag.Replace(speakerTag, "")  + ": ";
            }
        }

        return "You: ";
    }

    private void HandleTags(List<string> currentTags)
    {
        foreach (string currentTag in currentTags)
        {
            if (currentTag.Contains(gotInstrumentTag))
            {
                hasInstrumentBeenAcquired = true;
            }
        }
    } 


    void ChooseStoryChoice(Choice choice)
    {
        story.ChooseChoiceIndex(choice.index);
        RefreshUI();
    }

    void ClearUI()
    {
        
        for (int i=0; i < viewportContent.transform.childCount; i++)
        {

            GameObject dialogueAreaElement = viewportContent.transform.GetChild(i).gameObject;
            if (!dialogueAreaElement.CompareTag("PreserveUI") && dialogueAreaElement.GetComponent<Button>())
            {
                Destroy(dialogueAreaElement);
            }
                
        }
    }

    public void CallToStartDialogue(TextAsset inkJSON, InstrumentSO instrument, AIConversant conversationWith)
    {
        actionScheduler.StartAction(this);
        story = new Story(inkJSON.text);
        instrumentToBeGiven = instrument;
        aIConversant = conversationWith;
    }

    public void StartDialogue()
    {
        
        inDialogue = true;
        
        dialogueArea.SetActive(true);
        
        RefreshUI();
    }

    public void StopDialogue()
    {
        inDialogue = false;
        PushDialogueConcequences();
        dialogueArea.SetActive(false);
        ResetDialogueManager();
    }

    private void PushDialogueConcequences()
    {
        if (hasInstrumentBeenAcquired && instrumentToBeGiven != null)
        {
            instrumentInventory.AddToInstrumentList(instrumentToBeGiven);
        }
    }

    private void ResetDialogueManager()
    {
        story = null;
        instrumentToBeGiven = null;
        aIConversant = null;
        hasInstrumentBeenAcquired = false;
        ClearAllConversation();
    }

    private void ClearAllConversation()
    {
        for (int i=0; i < viewportContent.transform.childCount; i++)
        {

            GameObject dialogueAreaElement = viewportContent.transform.GetChild(i).gameObject;
            Destroy(dialogueAreaElement);
                
        }    
    }

    private bool GetIsInRange(Transform targetTransform)
    {
        return Vector3.Distance(player.transform.position, targetTransform.position) < distanceToSpeaker;
    }

    public void Cancel()
    {
        Debug.Log("cancel dialogue");
        StopDialogue();
    }

    public bool IsInDialogue()
    {
        return  inDialogue;
    }

        IEnumerator ScrollToBottom()
    {
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        scrollRect.verticalNormalizedPosition = 0f;
    }

}
