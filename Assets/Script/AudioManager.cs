using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip bossRoomClip;
    public AudioClip[] playlist;
    public AudioSource audioSource;
    private bool bossSongOn = false;
    private int indexMusic = 0;

    private void Start()
    {
        audioSource.clip = playlist[0];
        audioSource.Play();
    }

    private void Update()
    {
        if (!audioSource.isPlaying)
        {
            if (!bossSongOn)
            {
                playNextSong();
            }
            else
            {
                audioSource.Play();
            }
            
        }
    }

    void playNextSong()
    {
        indexMusic = (indexMusic + 1) % playlist.Length;
        audioSource.clip = playlist[indexMusic];
        audioSource.Play();
    }

    public void playBossSong()
    {
        bossSongOn = true;
        audioSource.clip = bossRoomClip;
        audioSource.Play();
    }

}
