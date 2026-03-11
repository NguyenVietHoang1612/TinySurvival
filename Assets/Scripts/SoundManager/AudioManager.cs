using System;
using UnityEngine;

namespace GameRPG
{
    public class AudioManager : Singleton<AudioManager>
    {
        public AudioSource sfxSource;
        public AudioSource musicSource;

        public void PlaySFX(AudioClip clip, float volume = 0.8f)
        {
            if (clip != null)
                sfxSource.PlayOneShot(clip, volume);
        }

        public void PlayMusic(AudioClip clip, bool loop = true)
        {
            if (clip != null)
            {
                musicSource.clip = clip;
                musicSource.loop = loop;
                musicSource.Play();
            }
        }

        public void PlayerSFXRandom(AudioClip[] clips, float volume = 0.8f)
        {
            if (clips.Length > 0)
            {
                int index = UnityEngine.Random.Range(0, clips.Length);
                PlaySFX(clips[index], volume);
            }
        }
    }


}
