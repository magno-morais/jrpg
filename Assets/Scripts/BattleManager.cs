using System;
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST}

public class BattleManager : MonoBehaviour
{
    public PlayerStats player;
    public PlayerItems playerItems;
    public GameObject playerHud;
    public List<GameObject> Enemies;
    GameObject Enemy;
    public Transform EnemyPosition;
    public Text dialogText;
    public BattleState state;
    public ActionManagement dialogBox;
    public event Action<bool> OnBattleOver;
    public BattleAnimator battleAnimator;

    public int index_options;
    public int index_moves;
    public int index_items;
    public bool selectingMoves = false;
    public bool selectingItems = false;

    GameObject EnemySpawn;
    EnemyBase EnemyUnit;


    void placePlayerStats(){
        playerHud.transform.GetChild(0).GetComponent<Text>().text = "HP: " + player.Hp;
        playerHud.transform.GetChild(1).GetComponent<Text>().text = "MANA: " + player.Mana;
        playerHud.transform.GetChild(2).GetComponent<Text>().text = "LEVEL: " + player.Level;
    }

    IEnumerator placeEnemy(){
        dialogBox.EnableMoveSelector(false);
        dialogBox.EnableOptionsSelector(false);
        dialogBox.EnableItemSelector(false);
        dialogBox.EnableDialogText(true);
        EnemySpawn = Instantiate(Enemy, EnemyPosition);
        EnemyUnit = EnemySpawn.GetComponent<EnemyBase>();
        yield return dialogBox.saySomething("Wild " + EnemyUnit.Name + " approaches");
        playerTurn();
    }

    void playerTurn(){
        state = BattleState.PLAYERTURN;
        selectingMoves = false;
        selectingItems = false;
        // StopAllCoroutines();
        StartCoroutine(dialogBox.saySomething("Choose an action:"));
        dialogBox.EnableOptionsSelector(true);
    }


    public void StartBattleTurn(){
        state = BattleState.START;
        int indexEnemy = UnityEngine.Random.Range(0,Enemies.Count);
        Enemy = Enemies[indexEnemy];
        placePlayerStats();
        StartCoroutine(placeEnemy());
    }

    // Update is called once per frame
    public void HandleUpdate()
    {

        if(state == BattleState.PLAYERTURN){
            if(selectingMoves){
                OptionsController(ref index_moves, player.movesNames.Count);
                dialogBox.updateMovesSelection(index_moves);
                if (Input.GetKeyDown(KeyCode.Z)) {
                    StartCoroutine(PlayerTurnAttack());
                }
                else if(Input.GetKeyDown(KeyCode.X)){
                    dialogBox.EnableMoveSelector(false);
                    dialogBox.EnableDialogText(true);
                    dialogBox.EnableOptionsSelector(true);
                    selectingMoves = false;
                }
            }
            else if(selectingItems){
                OptionsController(ref index_items, playerItems.itemsNames.Count);
                dialogBox.updateItemsSelection(index_items);
                if (Input.GetKeyDown(KeyCode.Z)) {
                    // StartCoroutine(PlayerTurnAttack());
                    StartCoroutine(PlayerTurnItem());
                }
                else if(Input.GetKeyDown(KeyCode.X)){
                    dialogBox.EnableItemSelector(false);
                    dialogBox.EnableDialogText(true);
                    dialogBox.EnableOptionsSelector(true);
                    selectingItems = false;
                }
            }
            else{
                OptionsController(ref index_options, dialogBox.options.Count);
                dialogBox.updateSelection(index_options);
                if(Input.GetKeyUp(KeyCode.Z) && player.GetMoveMana(index_moves) <= player.Mana){
                    dialogBox.EnableDialogText(false);
                    dialogBox.EnableOptionsSelector(false);
                    switch(index_options){
                        case 0: // FIGHT
                            dialogBox.EnableMoveSelector(true);
                            dialogBox.DisplayMoves();
                            selectingMoves = true;
                            break;
                        case 1: // ITEMS
                            dialogBox.EnableItemSelector(true);
                            dialogBox.DisplayItems();
                            selectingItems = true;
                            break;
                    }
                }
            }
        }
        else if (state == BattleState.ENEMYTURN && Input.GetKeyDown(KeyCode.Z)) {

        }
    }

    IEnumerator PlayerTurnItem(){
        player.ReceiveDamage(- playerItems.itemsLife[index_items]);
        player.CastMana(- playerItems.itemsManas[index_items]);
        playerItems.SpendItem(index_items);
        placePlayerStats();
        dialogBox.EnableItemSelector(false);
        dialogBox.EnableDialogText(true);
        yield return dialogBox.saySomething("The Player has recovery its stats");
        state = BattleState.ENEMYTURN;
        yield return EnemyTurn();
    }

    IEnumerator PlayerTurnAttack(){
        EnemyUnit.ReceiveDamage(player.GetMovePower(index_moves));
        player.CastMana(player.movesManas[index_moves]);
        placePlayerStats();
        dialogBox.EnableMoveSelector(false);
        dialogBox.EnableDialogText(true);
        string enemyName = EnemyUnit.Name;
        dialogBox.ResetDialogBox();
        yield return battleAnimator.DoAnimation(player.movesNames[index_moves]);
        SoundManager.PlaySound("Damage");
        yield return dialogBox.saySomething($"You deal {player.GetMovePower(index_moves)} in this {enemyName}");
        if(EnemyUnit.GetHp() <= 0){
            yield return dialogBox.saySomething($"You defated {enemyName} and won {EnemyUnit.GetXp()}XP, Congratulations!!!");
            player.AddExp(EnemyUnit.GetXp());
            if(player.CheckLevelUp()){
                placePlayerStats();
                yield return dialogBox.saySomething($"You Level Up to {player.Level}, Congratulations!");
            }
            Destroy(EnemySpawn);
            OnBattleOver(true);
        }
        state = BattleState.ENEMYTURN;
        yield return EnemyTurn();
    }

    IEnumerator EnemyTurn(){
        int attack_index = UnityEngine.Random.Range(0,EnemyUnit.movesPowers.Count);
        int attack_damage = EnemyUnit.movesPowers[attack_index];
        string attack_name = EnemyUnit.movesNames[attack_index];
        dialogBox.ResetDialogBox();
        yield return battleAnimator.DoAnimation(attack_name);
        player.ReceiveDamage(EnemyUnit.movesPowers[attack_index]);
        placePlayerStats();
        yield return dialogBox.saySomething($"{EnemyUnit.Name} deals {attack_damage} with his {attack_name}");
        playerTurn();
    }

    void OptionsController(ref int index, int max){
        bool decisionUp = Input.GetKeyDown(KeyCode.UpArrow);
        bool decisionDn = Input.GetKeyDown(KeyCode.DownArrow);
        if ( decisionUp && index > 0) {
            index += -1;
        }

        if ( decisionDn && index < max - 1) {
            index += 1;
        }
    }
}
