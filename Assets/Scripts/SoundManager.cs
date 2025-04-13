using NUnit.Framework.Constraints;
using Unity.VisualScripting;
using UnityEngine;


public enum SoundType
{
    THRUSTER,
    PICKUP,
    CRASH,
    LOST
}

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioClip[] soundList;
    private static SoundManager instance;
    private AudioSource audioSource;
    private static bool isPlayingThruster = false;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public static void PlaySound(SoundType sound, float volume = 1f)
    {
        instance.audioSource.PlayOneShot(instance.soundList[(int)sound], volume);
    }

    public static void PlayThruster()
    {
        if (!isPlayingThruster && !instance.audioSource.isPlaying)
        {
            instance.audioSource.PlayOneShot(instance.soundList[0], 0.2f);
        }
        else
        {
            isPlayingThruster = false;
        }
    }
}
