using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int MaxHp;
    public int Hp;
    public int MaxMana;
    public int Mana;
    public int Xp;
    public int Level;
    public int XpToLevelUp;

    public List<string> movesNames;
    public List<int> movesPowers;
    public List<int> movesManas;

    public Dictionary<string,int> moves = new Dictionary<string,int>();
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < movesNames.Count; i++){
            moves.Add(movesNames[i], movesPowers[i]);
        }
    }

    public int GetMovePower(int index){
        return movesPowers[index];
    }

    public int GetMoveMana(int index){
        return movesManas[index];
    }

    public void ReceiveDamage(int damage){
        Hp -= damage;
        if(MaxHp < Hp){
            Hp = MaxHp;
        }
    }

    public void CastMana(int manaCost){
        Mana -= manaCost;
        if(MaxMana < Mana){
            Mana = MaxMana;
        }
    }

    public void AddExp(int xp){
        Xp += xp;
    }

    public bool CheckLevelUp(){
        if(Xp >= XpToLevelUp){
            int levels = Mathf.FloorToInt(Xp / XpToLevelUp);
            Level += levels;
            for(int i = 0; i < movesPowers.Count; i++){
                movesPowers[i] += levels;
            }
            MaxHp += levels;
            Hp += levels;
            
            Xp -= levels*XpToLevelUp;
            return true;
        }
        else{
            return false;
        }
    }

    public int GetHp(){
        return Hp;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
