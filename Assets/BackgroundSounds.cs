using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundSounds : MonoBehaviour
{
    private AudioSource[] sources;
    public AudioClip[] backgroundClips;
    public AudioClip rootClip;
    bool isQueued = false;
    void Start()
    {
        sources = GetComponents<AudioSource>();
    }
    // Update is called once per frame
    void Update()
    {
        if (!sources[1].isPlaying && !isQueued)
        {
            isQueued = true;
            StartCoroutine(waitForSeconds(Random.Range(3, 6)));
        }
    }
    IEnumerator waitForSeconds(float time) 
    {
        
        yield return new WaitForSeconds(time);
        int clipId = Mathf.FloorToInt(Random.Range(0, backgroundClips.Length));
        isQueued = false;
        sources[1].PlayOneShot(backgroundClips[clipId]);
    }

    public void playRootClip()
    {
        sources[1].PlayOneShot(rootClip);
    }
}
