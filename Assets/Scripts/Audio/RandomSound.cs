using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSound : MonoBehaviour
{
    public bool autoplay = false;

    public AudioClip[] sounds;

    public bool loop = false;

    public float min_range;
    public float max_range;

    void Start()
    {
        if (autoplay){
            PlaySound(sounds[Random.Range(0, sounds.Length)]);
        }
    }

    void PlaySound(AudioClip sound)
    {
        if (loop)
        {
            StartCoroutine(StartTimer());
        }
         if (World.i.stopTime) {
            return;
        }
        else{
            GetComponent<AudioSource>().PlayOneShot(sound);
        }
    }

    IEnumerator StartTimer()
    {
        yield return new WaitForSeconds(Random.Range(min_range, max_range));
        PlaySound(sounds[Random.Range(0, sounds.Length)]);
    }
}
