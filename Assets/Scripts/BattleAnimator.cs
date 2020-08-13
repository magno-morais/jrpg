using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleAnimator : MonoBehaviour
{
    GameObject battleAnim;
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        battleAnim = GetComponent<GameObject>();
        anim = GetComponent<Animator>();
    }

    public IEnumerator DoAnimation(string animationName){
        gameObject.SetActive(true);
        anim = GetComponent<Animator>();

        switch(animationName){
            case "Bite":
                anim.SetInteger("AnimParam", 1);
                break;
            case "SpiderWeb":
                anim.SetInteger("AnimParam", 2);
                yield return new WaitForSeconds(1.917f);
                break;
            case "Fireball":
                anim.SetInteger("AnimParam", 3);
                yield return new WaitForSeconds(1.767f);
                break;
            case "Cut":
                anim.SetInteger("AnimParam", 4);
                yield return new WaitForSeconds(1.033f);
                break;
            case "BoneThrow":
                anim.SetInteger("AnimParam", 5);
                yield return new WaitForSeconds(1.167f);
                break;
            case "RayEye":
                anim.SetInteger("AnimParam", 6);
                yield return new WaitForSeconds(1.117f);
                break;
            case "Swing":
                anim.SetInteger("AnimParam", 7);
                yield return new WaitForSeconds(0.567f);
                break;
        }

        gameObject.SetActive(false);
    }

}
