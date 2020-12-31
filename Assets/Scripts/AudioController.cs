using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public AudioSource audioSource;

    public AudioClip backgroundClip;
    public AudioClip bossClip;

    public AudioClip itemClip;
    public AudioClip battleClip;
    public AudioClip doorClip;
    public AudioClip flyClip;
    
    public void MeetBoss()
    {
        audioSource.clip = bossClip;
        if (!audioSource.isPlaying) audioSource.Play();
    }

    public void NormalLevel()
    {
        audioSource.clip = backgroundClip;
        if (!audioSource.isPlaying) audioSource.Play();
    }

    public void PlayClip(ClipType type)
    {
        switch(type)
        {
            case ClipType.Item:
                AudioSource.PlayClipAtPoint(itemClip, audioSource.transform.position);
                break;
            case ClipType.Battle:
                AudioSource.PlayClipAtPoint(battleClip, audioSource.transform.position);
                break;
            case ClipType.Door:
                AudioSource.PlayClipAtPoint(doorClip, audioSource.transform.position);
                break;
            case ClipType.Fly:
                AudioSource.PlayClipAtPoint(flyClip, audioSource.transform.position);
                break;
            
        }
    }
}

public enum ClipType
{
    Item,
    Battle,
    Door,
    Fly
}
