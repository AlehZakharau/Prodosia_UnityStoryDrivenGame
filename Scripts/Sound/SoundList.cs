using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class SoundList : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private bool playAlways = false;

    [SerializeField] private List<AudioClip> _audioClips;

    public List<AudioClip> AudioClips => _audioClips;

    private bool hasPause;
    [ContextMenu("PlayRandom")]
    public void PlayRandom()
    {
        playAlways = true;
        var randomIndex = Random.Range(0, _audioClips.Count);
        var clip = _audioClips[randomIndex];
        _audioSource.clip = clip;

        _audioSource.volume = 0;
        var func = ChangeVolume(1, 1);
        StopCoroutine(func);
        StartCoroutine(func);
        _audioSource.Play();
    }

    public IEnumerator ChangeVolume(float duration, float targetVolume)
    {
        float currentTime = 0;
        float start = _audioSource.volume;

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            _audioSource.volume = Mathf.Lerp(start, targetVolume, currentTime / duration);
            yield return null;
        }
        yield break;
    }

    public void PlayByIndex(int Index)
    {
        _audioSource.clip = _audioClips[Index];
        _audioSource.Play();
    }

    public void Update()
    {
        if (!playAlways)
            return;

        if(!_audioSource.isPlaying)
        {
            PlayRandom();
        }
    }

    public void StopMusic()
    {
        playAlways = false;
        _audioSource.Stop();
    }

    public void PauseSound()
    {
        hasPause = !hasPause;
        if (hasPause)
        { 
            _audioSource.Pause();
        }
        else
        {
            _audioSource.UnPause();
        }
    }
}
