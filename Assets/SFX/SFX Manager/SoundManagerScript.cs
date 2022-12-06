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
        audioSrc = GetComponent<AudioSource>();
        playerHitSound = Resources.Load<AudioClip>("PlayerHit");
        playerCastSound = Resources.Load<AudioClip>("PlayerCast");
        playerDeathSound = Resources.Load<AudioClip>("PlayerDying");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public static void PlaySound (AudioClip clip)
    {
        audioSrc.PlayOneShot(clip);
    }
}
