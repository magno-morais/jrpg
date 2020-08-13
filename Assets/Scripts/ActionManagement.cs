using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionManagement : MonoBehaviour
{

    public GameObject dialogText;
    public PlayerStats player;
    public PlayerItems playerItems;
    public List<Text> options;
    public Color markedColor;
    public GameObject moveSelector;
    public GameObject itemSelector;
    public GameObject optionsSelector;

    public List<Text> MovesNameLayout;
    public List<Text> MovesPowerLayout;

    public List<Text> ItemsNameLayout;
    public List<Text> ItemsQuantLayout;

    Text dialogTextTrue;

    public void EnableMoveSelector(bool enable){
        moveSelector.SetActive(enable);
    }

    public void EnableItemSelector(bool enable){
        itemSelector.SetActive(enable);
    }

    public void EnableDialogText(bool enable){
        dialogText.SetActive(enable);
    }

    public void ResetDialogBox(){
        dialogTextTrue.text = "";
    }

    public IEnumerator saySomething(string dialog){
        dialogTextTrue = dialogText.GetComponent<Text>();
        dialogTextTrue.text = "";
        foreach(char letter in dialog.ToCharArray()){
            dialogTextTrue.text += letter;
            yield return null;
        }
        yield return new WaitForSeconds(2f);

    }

    public void EnableOptionsSelector(bool enable){
        optionsSelector.SetActive(enable);
    }

    public void DisplayMoves(){
        for(int i = 0 ; i < MovesNameLayout.Count; i++){
            if(i < player.movesNames.Count){
                MovesNameLayout[i].text = player.movesNames[i];
                MovesPowerLayout[i].text =  player.movesPowers[i] + " / " + player.movesManas[i];
            }
            else{
                MovesNameLayout[i].text = "--------------------";
                MovesPowerLayout[i].text = "-------------------";

            }
        }
    }

    public void DisplayItems(){
        // Debug.Log(playerItems.itemsNames[0]);
        for(int i = 0 ; i < ItemsNameLayout.Count; i++){
            if(i < playerItems.itemsNames.Count){
                ItemsNameLayout[i].text = playerItems.itemsNames[i];
                ItemsQuantLayout[i].text = "" + playerItems.itemsQuant[i];
            }
            else{
                ItemsNameLayout[i].text = "--------------------";
                ItemsQuantLayout[i].text = "-------------------";

            }
        }
    }

    public void updateSelection(int index_marked){
        for(int i = 0; i < options.Count; i++){
            if(i == index_marked){
                options[i].color = markedColor;
            }
            else{
                options[i].color = Color.white;
            }
        }
    }

    public void updateMovesSelection(int index){
        for(int i = 0; i < MovesNameLayout.Count; i++){
            if(i == index){
                MovesNameLayout[i].color = markedColor;
                MovesPowerLayout[i].color = markedColor;
            }
            else{
                MovesNameLayout[i].color = Color.white;
                MovesPowerLayout[i].color = Color.white;
            }
        }
    }

    public void updateItemsSelection(int index){
        for(int i = 0; i < ItemsNameLayout.Count; i++){
            if(i == index){
                ItemsNameLayout[i].color = markedColor;
                ItemsQuantLayout[i].color = markedColor;
            }
            else{
                ItemsNameLayout[i].color = Color.white;
                ItemsQuantLayout[i].color = Color.white;
            }
        }
    }
}
