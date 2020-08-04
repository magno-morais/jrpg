using System;
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Ink.Runtime;

public class DialogManager : MonoBehaviour
{
    public GameObject optionsPanel;
    public GameObject dialogBox;
    public GameObject optionButton;
    public Text dialogText;
    public PlayerMovement player;
    private TextAsset inkFile;
    static Story story;
    static Choice choiceSelected;

    void Start()
    {
        GameObject optionsPanel = GetComponent<GameObject>();
        GameObject dialogBox = GetComponent<GameObject>();
        GameObject optionButton = GetComponent<GameObject>();
        Text dialogText = GetComponent<Text>();
    }

    public void SetStory(TextAsset ink){
        inkFile = ink;
        story = new Story(inkFile.text);
        choiceSelected = null;
    }

    // Update is called once per frame
    void Update(){}

    public void Conversation(){
        player.talking = true;
        if(dialogBox.activeInHierarchy){

            if(story.canContinue){
                AdvanceDialog();
            }
            else if(story.currentChoices.Count != 0){
                dialogBox.SetActive(false);
                StartCoroutine(ShowChoices());
            }
            else{
                dialogBox.SetActive(false);
                player.talking = false;
            }
        }
        else{
            dialogBox.SetActive(true);
            if(story.canContinue){
                AdvanceDialog();
            }
        }
    }

    void AdvanceDialog(){
        string currentDialog = story.Continue();
        // StopAllCoroutines();
        StartCoroutine(TypeSentence(currentDialog));
    }

    IEnumerator TypeSentence(string sentence){
        dialogText.text = "";
        foreach(char letter in sentence.ToCharArray()){
            dialogText.text += letter;
            yield return null;
        }
    }

    IEnumerator ShowChoices(){
        List<Choice> _choices = story.currentChoices;
        int changeHigh = 0;
        for(int i = 0; i< _choices.Count; i++){

            GameObject temp = Instantiate(optionButton, optionsPanel.transform);
            temp.transform.GetChild(0).GetComponent<Text>().text = _choices[i].text;

            if(i == 0){
                temp.transform.GetChild(0).GetComponent<Text>().text = "* " + _choices[i].text;
            }

            temp.AddComponent<Selectable>();
            temp.GetComponent<Selectable>().element = _choices[i];
            temp.GetComponent<Selectable>().index = i;
            temp.GetComponent<Selectable>().true_index = i;
            temp.GetComponent<Selectable>().index_marked = 0;
            temp.GetComponent<Selectable>().total = _choices.Count;
            temp.GetComponent<Button>().onClick.AddListener(() => { temp.GetComponent<Selectable>().Decide();});
            temp.transform.Translate(0,changeHigh,0);
            changeHigh -= 30;
        }
        optionsPanel.SetActive(true);
        yield return new WaitUntil(() => { return choiceSelected != null;});

        AdvanceFromDecision();
    }


    public static void SetDecision(object element){
        choiceSelected = (Choice)element;
        story.ChooseChoiceIndex(choiceSelected.index);
    }

    public void AdvanceFromDecision(){
        optionsPanel.SetActive(false);
        for(int i = 0; i < optionsPanel.transform.childCount;i++){
            Destroy(optionsPanel.transform.GetChild(i).gameObject);
        }
        choiceSelected = null;
        dialogBox.SetActive(true);
        AdvanceDialog();
    }
}



public class Selectable : MonoBehaviour{
    public object element;
    public int index;
    public int true_index;
    public int index_marked;
    private bool flow = false;
    public int total;

    public void Decide()
    {
        DialogManager.SetDecision(element);
    }

    void Update(){

        SwitchOption();


        if(Input.GetKeyDown(KeyCode.Z)){
            if(transform.GetChild(0).GetComponent<Text>().text.Contains("*")){
                Decide();
            }
        }
    }

    void SwitchOption(){
        bool decisionUp = Input.GetKeyDown(KeyCode.UpArrow);
        bool decisionDn = Input.GetKeyDown(KeyCode.DownArrow);

        // Debug.Log(index_marked + "----" + true_index);
        if((decisionDn && index_marked < total-1) || (decisionUp && index_marked != 0)){
            // Index antigo
            if(index == 0){
                string new_text = transform.GetChild(0).GetComponent<Text>().text;
                transform.GetChild(0).GetComponent<Text>().text = new_text.Remove(0,1);
                flow = true;
            }
            // Mudança pro proximo index
            index += Convert.ToInt32(decisionUp) - Convert.ToInt32(decisionDn);

            // Mudança para o index do próximo marcado
            index_marked += Convert.ToInt32(decisionDn) - Convert.ToInt32(decisionUp) ;

            // Index novo
            if(index == 0 && !flow){
                string new_text = transform.GetChild(0).GetComponent<Text>().text;
                transform.GetChild(0).GetComponent<Text>().text = "*" + new_text;
            }
            flow = false;
        }
    }

}
