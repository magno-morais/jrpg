using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    public static AudioClip damageHitSound;
    static AudioSource audioSrc;
    // Start is called before the first frame update
    void Start()
    {
        damageHitSound = Resources.Load<AudioClip>("grito");
        audioSrc = GetComponent<AudioSource>();
    }

    public static void PlaySound(string clip){
        switch(clip){
            case "Damage":
                audioSrc.PlayOneShot(damageHitSound);
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
