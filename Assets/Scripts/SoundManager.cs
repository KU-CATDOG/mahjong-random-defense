using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MRD
{
    public class SoundManager : Singleton<SoundManager>
    {
        [SerializeField]
        private AudioSource audioSourceBGM;

        [SerializeField]
        private AudioSource audioSourceSFX;

        private Dictionary<string, AudioClip> audioClipDic = new();

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            LoadSounds();
        }

        public void SetBGM(string bgmName)
        {
            audioSourceBGM.clip = audioClipDic[bgmName];
            audioSourceBGM.Play();
        }


        public void PlaySFX(string sfxName)
        {
            audioSourceSFX.PlayOneShot(audioClipDic[sfxName]);
        }

        public void SetBGMVolume(float volume, bool mute = false)
        {
            audioSourceBGM.volume = volume;
            audioSourceBGM.mute = mute;
        }

        public void SetSFXVolume(float volume, bool mute = false)
        {
            audioSourceSFX.volume = volume;
            audioSourceSFX.mute = mute;
        }

        private void LoadSounds()
        {
            var sounds = ResourceDictionary.GetAll<AudioClip>("Sound");

            foreach(var sound in sounds)
            {
                audioClipDic.Add(sound.name, sound);
            }
        }
    }
}
