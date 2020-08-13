using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Ink.Runtime;

public class Npc : MonoBehaviour
{
    public TextAsset inkFile;
    public DialogManager dialogManager;
    public List<string> dialog;
    private bool playerInRange;

    static Story story;
    static Choice choiceSelected;
    // Start is called before the first frame update
    void Start()
    {
        dialogManager.SetStory(inkFile);
    }

    // Update is called once per frame
    void Update()
    {

        StartConversation();
    }

    void StartConversation(){
        if(Input.GetKeyDown(KeyCode.Z) && playerInRange){
            dialogManager.Conversation();
        }
    }

    private void OnTriggerEnter2D(Collider2D colission){
        if(colission.CompareTag("Player")){
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D colission){
        if(colission.CompareTag("Player")){
            playerInRange = false;
        }
    }
}
