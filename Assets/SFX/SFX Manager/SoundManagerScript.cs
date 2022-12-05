using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerScript : MonoBehaviour
{
    // Start is called before the first frame update
    public static AudioClip playerHitSound, playerCastSound, playerDeathSound;
    static AudioSource audioSrc;
    void Start()
    {
        playerHitSound = SFX.Load<AudioClip>("PlayerHit");
        playerCastSound = SFX.Load<AudioClip>("PlayerHit");
        playerDeathSound = SFX.Load<AudioClip>("PlayerDying");


    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public static void PlaySound (string clip)
    {
        switch (clip)
        {
            case "playerHit":
                audioSrc.PlayOneShot(playerHitSound);
                break;
            case "playerCast":
                audioSrc.PlayOneShot(playerCastSound); 
                break;
            case "playerDies":
                audioSrc.PlayOneShot(playerDeathSound); 
                break;
        }
    }
}
