using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum SFXID
{
    noSelection,
    jump,
    hit,
    platformSpawned,
    death,
    vistory,
    background
}

public class SFXManager : MonoBehaviour
{
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private Dictionary<SFXID, AudioClip> sounds = new();

    private bool PlayVictory = false;

    private void Start()
    {
        PlayBackgroundMusic();
    }

    private void OnEnable()
    {
        SpawnManager.PlatformSpawned += PlayPlatformSpawned;
        PlayerMovement.RespawnPlayer += PlayDeath;
        CollisionManager.CollidedWIthEnd += PlayVictoryMusic;
        CollisionManager.HitGround += PlayHit;
        PlayerMovement.PlayerJumped += PlayJump;

    }
    private void OnDisable()
    {
        SpawnManager.PlatformSpawned -= PlayPlatformSpawned;
        PlayerMovement.RespawnPlayer -= PlayDeath;
        CollisionManager.CollidedWIthEnd -= PlayVictoryMusic;
        CollisionManager.HitGround -= PlayHit;
        PlayerMovement.PlayerJumped -= PlayJump;
    }

    private void PlayBackgroundMusic()
    {
        musicSource.clip = sounds[SFXID.background];
        musicSource.loop = true;
        musicSource.Play();
    }

    private void PlayVictoryMusic()
    {
        if (PlayVictory) return;
        else PlayVictory = true;

        musicSource.clip = sounds[SFXID.vistory];
        musicSource.loop = false;
        musicSource.Play();
    }

    private void PlayJump()
    {
        sfxSource.PlayOneShot(sounds[SFXID.jump]);
    }

    private void PlayHit()
    {
        sfxSource.PlayOneShot(sounds[SFXID.hit]);
    }

    private void PlayPlatformSpawned()
    {
        sfxSource.PlayOneShot(sounds[SFXID.platformSpawned]);
    }
    private void PlayDeath()
    {
        sfxSource.PlayOneShot(sounds[SFXID.death]);
    }
}
