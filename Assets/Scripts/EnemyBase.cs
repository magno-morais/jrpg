using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyBase : MonoBehaviour
{
    public int MaxHp;
    public int Hp;
    public int Xp;
    public string Name;
    public List<string> movesNames;
    public List<int> movesPowers;

    public Dictionary<string,int> moves = new Dictionary<string,int>();

    void Start()
    {
        for(int i = 0; i < movesNames.Count; i++){
            moves.Add(movesNames[i], movesPowers[i]);
        }
    }

    public int GetMovePower(int index){
        return movesPowers[index];
    }

    public void ReceiveDamage(int damage){
        Hp -= damage;
    }

    public int GetXp(){
        return Xp;
    }

    public int GetHp(){
        return Hp;
    }
}
