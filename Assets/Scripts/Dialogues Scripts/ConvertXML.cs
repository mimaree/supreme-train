using UnityEngine;
using System.Xml.Serialization;
using System.IO;
using System.Collections.Generic;
using System;

[XmlRoot("DialoguesCollection")]
public class ConvertXML
{

    [XmlElement("Dialogues")]
    public List<Dialogues> dialogues;

   

}

public class Dialogues

{
    [XmlAttribute("DialoguesID")]
    public int dialoguesID;
    // [XmlText]
    // public string name;
    [XmlElement("Dialogue")]
    public List<Dialogue> dialogue;

}

public class Dialogue

{
    [XmlAttribute("DialogueID")]
    public int dialogueID;

    [XmlElement("Choices")]
    public Choices choices;

    [XmlElement("Sentence")]
    public List<Sentence> sentence;

}

public class Sentence
{
    [XmlAttribute("SentenceID")]
    public int sentenceID;

    [XmlElement("Speaks")]
    public string speaks;


    [XmlElement("Person")]
    public string person;


    [XmlElement("DialoguePanel")]
    public DialoguePanel dialoguePanel;


  
}

public class DialoguePanel
{
    [XmlAttribute("ChoicePanel")]
    public int choicePanel;


}

#region Choices

public class Choices
{
    [XmlElement("Choice")]
    public List<Choice> choice;

    [XmlElement("Shop")]
    public Shop shop;


}

public class Shop
{

}
public class Choice

{
    [XmlAttribute("Destination")]
    public int destination;

    [XmlAttribute("Disposable")]
    public int disposable;

    [XmlAttribute("Quest")]
    public string quest;

    [XmlAttribute("MakedQuest")]
    public string makedQuest;

    [XmlAttribute("FalseDestination")]
    public int dalseDestination;

    [XmlAttribute("Exit")]
    public int exit;



    [XmlText]
    public string text;

}
#endregion