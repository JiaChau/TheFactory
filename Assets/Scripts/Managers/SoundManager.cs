using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour
{
    [Header("Audio Source Assigment")]
    [SerializeField]
    AudioSource backgroundAudioSource;

    [SerializeField]
    AudioSource toolAudioSource;
    [SerializeField]
    AudioSource generatorAudioSource;
    [SerializeField]
    AudioSource craftingAudioSource;

    [Header("Audio Clip Assigment")]
    [SerializeField]
    AudioClip[] woodChopClips;
    [SerializeField]
    AudioClip pickAxeClip;

    [SerializeField]
    AudioClip[] backgroundClips;
    int backgroundIndex = 0;

    public float fadeDuration = 10f;
    float elapsedTime = 0;

    public static SoundManager Instance;

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void Update()
    {
        elapsedTime += Time.deltaTime;
    }

    private void Start()
    {
        StartCoroutine(PlayBackgroundPlaylist());
    }
    public void PlayGeneratorSound()
    {
        generatorAudioSource.Play();
    }

    public void StopGeneratorSound()
    {
        generatorAudioSource.Stop();
    }

    public void PlayCraftingSound()
    {
        craftingAudioSource.Play();
    }

    public void StopCraftingSound()
    {
        craftingAudioSource.Stop();
    }

    public void PlayWoodChopSound()
    {
        toolAudioSource.PlayOneShot(GetRandomWoodChopSound());
    }
    public void PlayPickAxeSound()
    {
        toolAudioSource.PlayOneShot(pickAxeClip);
    }

    AudioClip GetRandomWoodChopSound()
    {
        int index = Random.Range(0, woodChopClips.Length);
        return woodChopClips[index];
    }


    private IEnumerator PlayBackgroundPlaylist()
    {
        while (true)
        {
            AudioClip currentClip = backgroundClips[backgroundIndex];
            backgroundAudioSource.clip = currentClip;
            backgroundAudioSource.volume = 0f;
            backgroundAudioSource.Play();

            // Fade in
            yield return StartCoroutine(FadeAudioSource(backgroundAudioSource, 0f, .25f, fadeDuration));

            // Play for the duration of the clip minus fade-out time
            elapsedTime = 0f;
            while (elapsedTime < currentClip.length - fadeDuration)
            {
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            // Fade out
            yield return StartCoroutine(FadeAudioSource(backgroundAudioSource, .25f, 0f, fadeDuration));
            backgroundAudioSource.Stop();

            IterateBackgroundIndex();
        }
    }

    private IEnumerator FadeAudioSource(AudioSource source, float fromVolume, float toVolume, float duration)
    {
        float timer = 0f;
        while (timer < duration)
        {
            print(timer);
            timer += Time.deltaTime;
            source.volume = Mathf.Lerp(fromVolume, toVolume, timer / duration);
            yield return null;
        }
        source.volume = toVolume;
    }


    void IterateBackgroundIndex()
    {
        backgroundIndex++;
        if(backgroundIndex == backgroundClips.Length)
        {
            backgroundIndex = 0;
        }
    }

    public void PauseMusic()
    {
        backgroundAudioSource.Pause();
    }
    public void UnPauseMusic()
    {
        backgroundAudioSource.UnPause();
    }

}
