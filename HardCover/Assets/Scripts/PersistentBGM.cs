using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PersistentBGM : MonoBehaviour
{
    public static PersistentBGM instance;
    public AudioClip sceneMusicClip;
    public float volumeMultiplier;

    [Range(0,1)]
    int activeAudioSource;
    [SerializeField] AudioSource aSource1;
    [SerializeField] AudioSource aSource2;

    private void Awake()
    {
        if (instance != null)
        {
            instance.PlayNextBGM(sceneMusicClip);
            Destroy(gameObject);
            return;
        }
        else
        {
            instance = this;
            activeAudioSource = 0;

            aSource1.volume = volumeMultiplier;
            aSource2.volume = 0;

            aSource1.clip = sceneMusicClip;
            aSource1.Play();
        }
        DontDestroyOnLoad(gameObject);
    }

    public void PlayNextBGM(AudioClip nextClip)
    {
        ChangeVolume(GetActiveAudioSource(), 0, 0.2f, 0);

        if (nextClip != null)
        {
            SwitchActiveAudioSource(nextClip);
            GetActiveAudioSource().Play();
            ChangeVolume(GetActiveAudioSource(), 1, 0.2f, 1);
        }
    }

    AudioSource GetActiveAudioSource()
    {
        return activeAudioSource == 0 ? aSource1 : aSource2;
    }

    void SwitchActiveAudioSource(AudioClip newClip)
    {
        // switch when game changes between main menu and game loop
        if (activeAudioSource == 0)
        {
            activeAudioSource = 1;
        }
        else
        {
            activeAudioSource = 0;
        }
        GetActiveAudioSource().clip = newClip;
    }

    public void ChangeVolume(AudioSource aSource, float volume, float changeSpeed, float delay)
    {
        bool stopAtEnd = volume <= 0;
        StartCoroutine(ChangeVolumeOverTime(aSource, volume, changeSpeed, delay, stopAtEnd));
    }

    IEnumerator ChangeVolumeOverTime(AudioSource aSource, float _volume, float changeSpeed, float delay = 0, bool stopAtEnd = false)
    {
        yield return new WaitForSeconds(delay);
        float volume = _volume * volumeMultiplier;
        float volumeDiff = Mathf.Abs(aSource.volume - volume);
        while (volumeDiff > 0.01f)
        {
            aSource.volume += Mathf.Sign(volume - aSource.volume) * changeSpeed * Time.deltaTime;
            volumeDiff = Mathf.Abs(aSource.volume - volume);
            yield return null;
        }
        aSource.volume = volume;
        if (stopAtEnd) aSource.Stop();
    }
}
