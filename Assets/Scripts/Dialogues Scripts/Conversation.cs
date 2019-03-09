using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using System.Collections.Generic;



public class Conversation : MonoBehaviour
{
    private Button dialogueButton;
    public TextMeshProUGUI dialogueContainer, speakerContainer;
    public Button[] choiceCointeiner = new Button[4];
    public TextMeshProUGUI[] textInChoiceCointeiner = new TextMeshProUGUI[4];

    public TextAsset dialogueFolder;
    private ConvertXML dialogueText;
    private int sentenceId, dialogueId, dialoguesId, dialogueIdChoicePanel;
    private int currentVisibleCharCount, sentenceCharCount;
    private Coroutine coroutine;

    String dialoguePanel;

    Sentence sentence;
    public GameObject dialogueIu;
    public GameObject cloudStartDialogue;

    bool testZmienna;

    
    public AudioSource source;
    public AudioClip clip;


    private void Awake()
    {
        
        dialogueText = XmlSupport.LoadFromFile("Assets/Dialogues/" + dialogueFolder.name + ".xml");
        source.clip = clip;
    }

    // //////////////////// How to Start Conversation ///////////////////////
    private void Update()
    {

        
        if (Input.GetKeyDown("space") && testZmienna)
        {
            AssigningButtonsFromUi();
            SerialiseAllMethods();
        }
       
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            testZmienna = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        testZmienna = false;
    }

    // ////////////////////////////////////////////////////////////////////////

    private void AssigningButtonsFromUi()
    {
        dialogueIu.gameObject.SetActive(true);
        // Destroy(transform.FindChild("StartCloudDialogue(Clone)").gameObject);

        dialogueButton = GameObject.Find("ButtonNextSentence").GetComponent<Button>();
        dialogueButton.onClick.AddListener(WhenPressNextButton);

        choiceCointeiner[0] = GameObject.Find("Choice0").GetComponent<Button>();
        choiceCointeiner[1] = GameObject.Find("Choice1").GetComponent<Button>();
        choiceCointeiner[2] = GameObject.Find("Choice2").GetComponent<Button>();
        choiceCointeiner[3] = GameObject.Find("Choice3").GetComponent<Button>();

        textInChoiceCointeiner[0] = choiceCointeiner[0].gameObject.GetComponentInChildren<TextMeshProUGUI>();
        textInChoiceCointeiner[1] = choiceCointeiner[1].gameObject.GetComponentInChildren<TextMeshProUGUI>();
        textInChoiceCointeiner[2] = choiceCointeiner[2].gameObject.GetComponentInChildren<TextMeshProUGUI>();
        textInChoiceCointeiner[3] = choiceCointeiner[3].gameObject.GetComponentInChildren<TextMeshProUGUI>();

        choiceCointeiner[0].onClick.AddListener(delegate { WhenPressDialogueOption(0); });
        choiceCointeiner[1].onClick.AddListener(delegate { WhenPressDialogueOption(1); });
        choiceCointeiner[2].onClick.AddListener(delegate { WhenPressDialogueOption(2); });
        choiceCointeiner[3].onClick.AddListener(delegate { WhenPressDialogueOption(3); });
    }

    private void WritingOutButtonsFromUi()
    {
        dialogueIu.gameObject.SetActive(true);

        dialogueButton.onClick.RemoveAllListeners();
        dialogueButton = null;

        choiceCointeiner[0].onClick.RemoveAllListeners();
        choiceCointeiner[1].onClick.RemoveAllListeners();
        choiceCointeiner[2].onClick.RemoveAllListeners();
        choiceCointeiner[3].onClick.RemoveAllListeners();

        choiceCointeiner[0] = null;
        choiceCointeiner[1] = null;
        choiceCointeiner[2] = null;
        choiceCointeiner[3] = null;

        textInChoiceCointeiner[0] = null;
        textInChoiceCointeiner[1] = null;
        textInChoiceCointeiner[2] = null;
        textInChoiceCointeiner[3] = null;

        dialogueIu.gameObject.SetActive(false);
    }

    private void SerialiseAllMethods()
    {
       

        CalculateCurrentTextFromXml();
        DisplayWholeText();
        coroutine = StartCoroutine(DisplayDialogueAnimation(sentence.speaks.Length));
    }

    private void CalculateCurrentTextFromXml()
    {
        CalculateCurrentRawChoiceListFromXml();
        speakerContainer.text = dialogueText.dialogues.Find(g => g.dialoguesID == dialoguesId)
                                           .dialogue.Find(g => g.dialogueID == dialogueId)
                                           .sentence.Find(x => x.sentenceID == sentenceId)
                                           .person;
        dialogueContainer.text = dialogueText.dialogues.Find(g => g.dialoguesID == dialoguesId)
                                          .dialogue.Find(g => g.dialogueID == dialogueId)
                                          .sentence.Find(x => x.sentenceID == sentenceId)
                                          .speaks;
    }

    private void CalculateCurrentRawChoiceListFromXml()
    {
        sentence = dialogueText.dialogues.Find(g => g.dialoguesID == dialoguesId)
                                         .dialogue.Find(g => g.dialogueID == dialogueId)
                                         .sentence.Find(x => x.sentenceID == sentenceId);

        try
        {
            dialoguePanel = sentence.dialoguePanel.ToString();
            dialogueIdChoicePanel = sentence.dialoguePanel.choicePanel;
        }
        catch
        {
            dialoguePanel = null;
        }
    }

    IEnumerator DisplayDialogueAnimation(int sentenceLength)
    {
        dialogueButton.gameObject.SetActive(false);
        TurnOffAllDialogueOptions();

        sentenceCharCount = sentenceLength;
        int counter = 0;
        int halfSentenceCharCount = sentenceCharCount / 8;

        while (true)
        {
            source.Play();
            //   Debug.Log("counter trwa");
            currentVisibleCharCount = counter % (sentenceLength + 1);

            dialogueContainer.maxVisibleCharacters = currentVisibleCharCount;

            if (counter == halfSentenceCharCount)  // wlacza przycisk next w poloie dialogu
            {
                dialogueButton.gameObject.SetActive(true);
            }

            if (currentVisibleCharCount >= sentenceLength)   // zrob kiedy caly texk bedzie wyswietlony
            {
                if (dialoguePanel != null)
                {
                    DisplayCorrectDialoguePanel(ReturnChoiceList(ReturnRawChoiceList())); //pokaz poprawne sciezki dialogowe   
                }
                yield break;
            }
            else
            {  
                counter += 1;
           
                yield return new WaitForSeconds(0.01f);
            }
        }
    }

    private void WhenPressDialogueOption(int which)
    {
        ///////////WyswietlCalyTekst();
        List<Choice> displayChoiceList = ReturnChoiceList(ReturnRawChoiceList());

        if (displayChoiceList[which].disposable == 1)  //jesli jest jednorazowy 
        {
            dialogueText.dialogues.Find(g => g.dialoguesID == dialoguesId)
                                            .dialogue.Find(g => g.dialogueID == dialogueIdChoicePanel)
                                            .choices.choice.Find(x => x.text == displayChoiceList[which].text).disposable = 2;
        }

        sentenceId = displayChoiceList[which].destination; //zmien sentence na dana z dialogu

        if (dialoguePanel != null)
        {
            dialogueId = sentence.dialoguePanel.choicePanel;
        }

        if (displayChoiceList[which].exit == 1) //jesli to exit
        {
            dialogueButton.gameObject.SetActive(true);
            for (int i = 0; i < 4; i++)
            {
                choiceCointeiner[i].gameObject.SetActive(true);
            }
            WritingOutButtonsFromUi();
        }
        else // przejdz do tego dialogu id
        {
            SerialiseAllMethods();
        }
    }

    private void WhenPressNextButton()
    {
        if (currentVisibleCharCount < sentenceCharCount) // DisplayWholeText
        {
            StopCoroutineAndDisplayCorrectDialogueOptions();

            if (dialoguePanel != null)
            {
                DisplayCorrectDialoguePanel(ReturnChoiceList(ReturnRawChoiceList()));  /////// pokaz poprawne dialogi
            }
            DisplayWholeText();
        }
        else if (dialoguePanel == null)  //Next Sentence
        {
            StopCoroutineAndDisplayCorrectDialogueOptions();
            TurnOffAllDialogueOptions();
            sentenceId++;
            SerialiseAllMethods();
        }
        else  //Dialogue Panel
        {
            //StopCoroutine(coroutine);
        }
    }

    /// //////////////////////////////////// Metody Pomicnicze ////////////////////////////////////

    private void TurnOffAllDialogueOptions()
    {
        for (int i = 0; i < 4; i++)
        {
            choiceCointeiner[i].gameObject.SetActive(false);
        }
    }

    private void DisplayWholeText()
    {
        sentence = dialogueText.dialogues.Find(g => g.dialoguesID == dialoguesId)
                                             .dialogue.Find(g => g.dialogueID == dialogueId)
                                             .sentence.Find(x => x.sentenceID == sentenceId);

        speakerContainer.text = sentence.person;
        dialogueContainer.text = sentence.speaks;
        dialogueContainer.maxVisibleCharacters = dialogueContainer.text.Length;
        currentVisibleCharCount = dialogueContainer.text.Length;
    }

    private void StopCoroutineAndDisplayCorrectDialogueOptions()
    {
        StopCoroutine(coroutine);

        if (dialoguePanel != null)
        {
            DisplayCorrectDialoguePanel(ReturnChoiceList(ReturnRawChoiceList()));
        }
    }

    // //////////////////////////////////// Listy //////////////////////////////

    private List<Choice> ReturnRawChoiceList()
    {
        List<Choice> choiceList = new List<Choice>();
        choiceList = dialogueText.dialogues.Find(g => g.dialoguesID == dialoguesId)
                                          .dialogue.Find(g => g.dialogueID == dialogueIdChoicePanel)
                                          .choices.choice;
        return choiceList;
    }

    private List<Choice> ReturnChoiceList(List<Choice> choiceList)
    {
        List<Choice> displayChoiceList = new List<Choice>();
        foreach (var item in choiceList)
        {
            if (item.makedQuest == "nie wykonane zadanie") // opcja dialogowa ktora ma sie wlaczyc oi wykonaniu zadania
            {
                continue;
            }
            if (item.disposable == 2) // skonczone zdanie
            {
                continue;
            }
            displayChoiceList.Add(item);
        }
        return displayChoiceList;
    }

    private void DisplayCorrectDialoguePanel(List<Choice> displayChoiceList)
    {
        dialogueButton.gameObject.SetActive(false);
        for (int i = 0; i < 4; i++)  // usuwa puse sciezki dialogowe
        {
            if (i < displayChoiceList.Count)
            {
                choiceCointeiner[i].gameObject.SetActive(true);
                textInChoiceCointeiner[i].text = displayChoiceList[i].text;
            }
            else
            {
                choiceCointeiner[i].gameObject.SetActive(false);
            }
        }
    }
}