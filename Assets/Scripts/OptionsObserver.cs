using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Ink.Runtime;

public class OptionsObserver : MonoBehaviour
{
    // private GameObject member;
    // Start is called before the first frame update
    void Start()
    {
        // member = GetComponent<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        // Observer();
    }
    //
    // void Observer() {
    //     // if(gameObject.activeInHierarchy){
    //         int diference = 0;
    //         float decision = Input.GetAxisRaw("Vertical");
    //         if(decision != 0){
    //             bool done = false;
    //             foreach(Transform child in transform){
    //                 if(!done && child.transform.GetChild(0).GetComponent<Text>().text.Contains("*")){
    //                     int index_chosen = child.transform.GetSiblingIndex();
    //                     Debug.Log(index_chosen);
    //
    //                     child.transform.GetChild(0).GetComponent<Selectable>().RemoveMark();
    //                     if(decision > 0 && diference < index_chosen){
    //                         diference = -1;
    //                     }
    //                     if(decision < 0 && index_chosen+1 < transform.childCount){
    //                         diference = 1;
    //                     }
    //                     transform.GetChild(index_chosen + diference).GetChild(0).GetComponent<Selectable>().AddMark();
    //                     done = true;
    //                     // Debug.Log(diference);
    //                 }
    //             }
    //         }
    //     // }
    // }
}
