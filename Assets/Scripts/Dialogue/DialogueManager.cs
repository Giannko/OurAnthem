using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;
using System;
using TMPro;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{

    
    private Story story;

    [SerializeField] GameObject dialogueArea;
    [SerializeField] TextMeshProUGUI dialogueTextPrefab;
    [SerializeField] Button dialogueButtonPrefab;
    [SerializeField] Button nextButton;
    [SerializeField] InstrumentInventory instrumentInventory;
    [SerializeField] GameObject viewportContent;
    private InstrumentSO instrumentToBeGiven = null;
    private bool hasInstrumentBeenAcquired = false;
    private const string gotInstrumentTag = "acquired_instr";
    


    // Start is called before the first frame update
    void Start()
    {
        //story = new Story(inkJSON.text);
        //RefreshUI();

    }

    

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RefreshUI()
    {
        //ClearUI();

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
    }

    string LoadStoryChunk() //returning an empty string or the next chunk of the story
    {

        string text = "";
        if (story.canContinue)
        {
            text = story.Continue(); //makes tags not work?? need to use continue??
            HandleTags(story.currentTags);
        }
        
        return text;
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
        for (int i=0; i < dialogueArea.transform.childCount; i++)
        {
            if (!dialogueArea.transform.GetChild(i).gameObject.CompareTag("PreserveUI"))
            Destroy(dialogueArea.transform.GetChild(i).gameObject);
        }
    }

    public void StartDialogue(TextAsset inkJSON, InstrumentSO instrument)
    {
        instrumentToBeGiven = instrument;
        dialogueArea.SetActive(true);
        story = new Story(inkJSON.text);
        RefreshUI();
    }

    public void StopDialogue()
    {
        PushDialogueConcequences();
        this.enabled = false;
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
        hasInstrumentBeenAcquired = false;
    }
}
