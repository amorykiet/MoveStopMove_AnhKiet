using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    [SerializeField] private AudioClip clickButtonClip;
    [SerializeField] private AudioClip deadClip;
    [SerializeField] private AudioClip victoryClip;
    [SerializeField] private AudioClip loseClip;
    [SerializeField] private AudioClip throwWeaponClip;
    [SerializeField] private AudioClip levelUpClip;

    [SerializeField] private AudioSource audioSource;

    private bool isMuted = false;

    public void OnButtonClick()
    {
        if (isMuted) return;
        if (clickButtonClip != null)
        {
            audioSource.clip = clickButtonClip;
            audioSource.Play();
        }
    }

    public void OnDead()
    {
        if (isMuted) return;
        audioSource.clip = deadClip;
        audioSource.Play();
    }

    public void OnVictory()
    {
        if (isMuted) return;
        audioSource.clip = victoryClip;
        audioSource.Play();
    }

    public void OnLose()
    {
        if (isMuted) return;
        audioSource.clip = loseClip;
        audioSource.Play();
    }

    public void OnThrowWeapon()
    {
        if (isMuted) return;
        audioSource.clip = throwWeaponClip;
        audioSource.Play();
    }

    public void OnLevelUp()
    {
        if (isMuted) return;
        audioSource.clip = levelUpClip;
        audioSource.Play();
    }

    public void Mute()
    {
        isMuted = true;
    }

    public void UnMute()
    {
        isMuted = false;
    }
}
