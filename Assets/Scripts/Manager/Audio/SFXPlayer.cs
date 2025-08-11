using UnityEngine;

public class SFXPlayer : MonoBehaviour
{
    public AudioSource sfxSource;
    public AudioClip[] sfxClips;

    public void PlaySFX(int index)
    {
        sfxSource.PlayOneShot(sfxClips[index]);
    }
}
