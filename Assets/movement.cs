using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class movement : MonoBehaviour
{
    [SerializeField] private Transform[] movePositionTransform;
    public Transform characterTransform;
    private Transform chobiTransform;
    public int pathCounter = 0;
    private NavMeshAgent navMeshAgent;
    public bool followingCharacter = false;
    public bool followingCharacterForRoot = false;
    public AudioClip mainTheme;
    public AudioClip warningTheme;
    public AudioClip warningThemeLoop;
    public AudioSource audioSource;
    void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        chobiTransform = GetComponent<Transform>();
    }

    private void Update()
    {
        if (Vector3.Distance(characterTransform.position, chobiTransform.position) < 50)
        {
            followingCharacter = true;
            GetComponent<Animator>().SetBool("FollowingPlayer", true);
        } else
        {
            followingCharacter = false;
            GetComponent<Animator>().SetBool("FollowingPlayer", false);
        }
        if (followingCharacter || followingCharacterForRoot)
        {
            if (mainTheme.name == audioSource.clip.name)
            {
                audioSource.volume = 0.2f;
                playSound(warningTheme, false);
                StartCoroutine(teleportChobi());
            }
            if (audioSource.clip.name == warningTheme.name && audioSource.isPlaying)
            {
                playSound(warningThemeLoop, true);
            }
            navMeshAgent.destination = characterTransform.position;
        } else
        {
            if (audioSource.clip.name == warningTheme.name || audioSource.clip.name == warningThemeLoop.name)
            {
                playSound(mainTheme, true);
                audioSource.volume = 1;
            }
            navMeshAgent.destination = movePositionTransform[pathCounter].position;
        }
    }

    IEnumerator teleportChobi()
    {
        yield return new WaitForSeconds(25);
        if (followingCharacter || followingCharacterForRoot)
        {
            followingCharacter = false;
            GetComponent<Animator>().SetBool("FollowingPlayer", false);
            followingCharacterForRoot = false;
            foreach (Transform pathPosition in movePositionTransform)
            {
                if (Vector3.Distance(pathPosition.position, characterTransform.position) > 50)
                {
                    chobiTransform.position = pathPosition.position;
                }
            }
        }
    }
    private void playSound(AudioClip clip, bool loop)
    {
        audioSource.Stop();
        audioSource.clip = clip;
        audioSource.loop = loop;
        audioSource.Play();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "spheres")
        {
            pathCounter++;
        } else if (other.tag == "Player")
        {
            characterTransform.gameObject.GetComponent<ThirdPersonController>().GameOver();
            GetComponent<Animator>().SetBool("GameOver", true);
        }
        else if (other.tag == "cabin")
        {
            followingCharacter = false;
            followingCharacterForRoot = false;
            foreach (Transform pathPosition in movePositionTransform)
            {
                if (Vector3.Distance(pathPosition.position, characterTransform.position) > 70)
                {
                    chobiTransform.position = pathPosition.position;
                }
            }
        }
        if (pathCounter == movePositionTransform.Length)
        {
            pathCounter = 0;
        }
    }

    public void followForRoot()
    {
        followingCharacterForRoot = true;
        GetComponent<Animator>().SetBool("FollowingPlayer", true);
    }
}
