using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItems : MonoBehaviour
{

    public List<string> itemsNames;
    public List<int> itemsLife;
    public List<int> itemsManas;
    public List<int> itemsQuant;

    public void SpendItem(int index){
        itemsQuant[index] -= 1;
    }


}
