using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    byte numToSpawn = 20;
    public static SoundController instance;

    public List<GameObject> audioPlayers;
    public AudioClip shoot;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
        }

        populatePool();
    }


    public void PlaySound(AudioClip clipToPlay)
    {
        GameObject audioPlayer = instance.GetPlayerFromPool();
        audioPlayer.SetActive(true);
        audioPlayer.GetComponent<AudioPlayer>().clipDuration = clipToPlay.length;

        AudioSource audioSource = audioPlayer.GetComponent<AudioSource>();
        audioSource.clip = clipToPlay;
        audioSource.pitch = 1 + (Random.Range(-0.2f, 0.2f));
    }

    public GameObject GetPlayerFromPool()
    {
        for (int i = 0; i < audioPlayers.Count; i++)
        {
            if (!audioPlayers[i].activeInHierarchy)
            {
                return audioPlayers[i];
            }
        }

        GameObject audioPlayer = Instantiate(Resources.Load("AudioPlayerPrefab")) as GameObject;
        audioPlayers.Add(audioPlayer);
        return audioPlayer;
    }

    private void populatePool()
    {
        for (int i = 0; i < numToSpawn; i++)
        {
            GameObject audioPlayer = Instantiate(Resources.Load("AudioPlayerPrefab"), transform) as GameObject;
            audioPlayers.Add(audioPlayer);
            if (audioPlayer.activeInHierarchy)
                audioPlayer.SetActive(false);
        }
    }
}