using UnityEngine;
using System.Collections;
public class BGMPlayer : MonoBehaviour
{
    public AudioSource musicSource;
    public AudioClip[] bgmClips;
    public float fadeDuration = 1.0f;
    public float targetVolume = 1.0f; // 항상 복원할 목표 볼륨

    private Coroutine fadeCoroutine;

    public void PlayBGM(int index)
    {
        if (index < 0 || index >= bgmClips.Length) return;

        if (fadeCoroutine != null)
            StopCoroutine(fadeCoroutine);

        fadeCoroutine = StartCoroutine(FadeToNewTrack(bgmClips[index]));
    }

    private IEnumerator FadeToNewTrack(AudioClip newClip)
    {
        float t = 0f;

        // 1) 페이드 아웃 (targetVolume → 0)
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            musicSource.volume = Mathf.Lerp(targetVolume, 0f, t / fadeDuration);
            yield return null;
        }
        musicSource.volume = 0f;

        // 2) 새 트랙 재생
        musicSource.clip = newClip;
        musicSource.Play();

        // 3) 페이드 인 (0 → targetVolume)
        t = 0f;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            musicSource.volume = Mathf.Lerp(0f, targetVolume, t / fadeDuration);
            yield return null;
        }
        musicSource.volume = targetVolume;
    }
}
