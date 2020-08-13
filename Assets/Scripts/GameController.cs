using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState {FreeRoam, Battle}

public class GameController : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public GameObject battleSystem;
    public BattleManager battleManager;
    public Camera worldCamera;

    public GameState state;
    // Start is called before the first frame update
    void Start()
    {
        playerMovement.OnEncounter += StartBattle;
        battleManager.OnBattleOver += OverBattle;
    }

    void OverBattle(bool won){
        state = GameState.FreeRoam;
        worldCamera.gameObject.SetActive(true);
        battleSystem.SetActive(false);

    }

    void StartBattle(){
        state = GameState.Battle;
        battleSystem.SetActive(true);
        battleManager.StartBattleTurn();
        worldCamera.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(state == GameState.FreeRoam){
            playerMovement.HandleUpdate();
        }
        else if(state == GameState.Battle){
            battleManager.HandleUpdate();
        }

    }
}
