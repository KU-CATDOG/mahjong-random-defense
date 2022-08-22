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

        [SerializeField]
        private AudioSource audioSourceBullet;

        [SerializeField]
        private AudioSource audioSourceSpecialBullet;

        private Dictionary<string, AudioClip> audioClipDic = new();

        private List<AudioClip> speciaBulletList = new();

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


        public void PlaySFX(string sfxName, float volume = 1f)
        {
            audioSourceSFX.PlayOneShot(audioClipDic[sfxName], volume);
        }

        public void PlayBullet(string bulletName, float volume = 1f)
        {
            audioSourceBullet.PlayOneShot(audioClipDic[bulletName], volume);
        }

        public void PlaySpecialBullet(string specialBulletName, float volume = 1f)
        {
            if (speciaBulletList.Count < 15)
            {
                audioSourceBullet.PlayOneShot(audioClipDic[specialBulletName], volume);
                speciaBulletList.Add(audioClipDic[specialBulletName]);
                StartCoroutine(RemoveVolumeFromClip(audioClipDic[specialBulletName], speciaBulletList));
            }
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
        private IEnumerator RemoveVolumeFromClip(AudioClip clip, List<AudioClip> audioClips)
        {
            yield return new WaitForSeconds(clip.length);

            audioClips.RemoveAt(audioClips.Count - 1);
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
