using System;
using UnityEngine;

namespace Controllers.Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioController : MonoBehaviour
    {
        [SerializeField] private AudioClip music;
        [SerializeField] private AudioSource effectAudioSource;
        
        [SerializeField] private AudioClipManager buttonClick;
        [SerializeField] private AudioClipManager findOpponent;
        [SerializeField] private AudioClipManager collectObject;
        [SerializeField] private AudioClipManager wrongClickObject;
        [SerializeField] private AudioClipManager returnObject;
        [SerializeField] private AudioClipManager completeCollect;
        [SerializeField] private AudioClipManager winSound;
        [SerializeField] private AudioClipManager loseSound;


        private AudioSource _musicAudioSource;

        
        
        private void Awake()
        {
            _musicAudioSource = GetComponent<AudioSource>();
            _musicAudioSource.clip = music;
            DontDestroyOnLoad(this);
        }
            

        public void PlayMusic() => _musicAudioSource.Play();
        public void StopMusic() => _musicAudioSource.Stop();
        
        public void Click() => effectAudioSource.PlayOneShot(buttonClick.clip, buttonClick.volume);
        public void FindOpponent() => effectAudioSource.PlayOneShot(findOpponent.clip, findOpponent.volume);
        public void CollectObject() => effectAudioSource.PlayOneShot(collectObject.clip, collectObject.volume);
        public void WrongClickObject() => effectAudioSource.PlayOneShot(wrongClickObject.clip, wrongClickObject.volume);
        public void ReturnObject() => effectAudioSource.PlayOneShot(returnObject.clip, returnObject.volume);
        public void CompleteCollect() => effectAudioSource.PlayOneShot(completeCollect.clip, completeCollect.volume);
        public void WinSound() => effectAudioSource.PlayOneShot(winSound.clip, winSound.volume);
        public void LoseSound() => effectAudioSource.PlayOneShot(loseSound.clip, loseSound.volume);
        
    }

    [Serializable]
    public struct AudioClipManager
    {
        public AudioClip clip;
        
        [Range(0,2)] public float volume;
    }
}
