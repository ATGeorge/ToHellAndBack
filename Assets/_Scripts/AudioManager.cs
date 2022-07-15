using System;
using UnityEngine;
using UnityEngine.Audio;

namespace ToHellAndBack
{
    public class AudioManager : Singleton<AudioManager>
    {
        // Private Fields
        public float MaxVolume;
        public AudioMixer MasterMixer;
        public AudioMixerGroup[] MixerGroups;
        [Range(0f, 1f)]
        public float[] Volumes;
        public Sound[] Sounds;

        private void Start()
        {
            Volumes[0] = PlayerPrefs.GetFloat("Master Volume", Volumes[0]);
            Volumes[1] = PlayerPrefs.GetFloat("Music Volume", Volumes[1]);
            Volumes[2] = PlayerPrefs.GetFloat("SFX Volume", Volumes[2]);
            Volumes[2] = PlayerPrefs.GetFloat("Background Volume", Volumes[3]);

            SetupAudio();
            Play("Music");
        }

        private void OnEnable()
        {
            EventManager.Instance.OnVolumeChanged += ChangeVolume;
        }

        private void OnDisable()
        {
            PlayerPrefs.SetFloat("Master Volume", Volumes[0]);
            PlayerPrefs.SetFloat("Music Volume", Volumes[1]);
            PlayerPrefs.SetFloat("SFX Volume", Volumes[2]);
            PlayerPrefs.SetFloat("Background Volume", Volumes[3]);

            if (EventManager.Instance != null)
            {
                EventManager.Instance.OnVolumeChanged -= ChangeVolume;
            }
        }

        private void ChangeVolume(int groupIndex, float value)
        {
            Volumes[groupIndex] = (value / MaxVolume);
            string parameterName = MixerGroups[groupIndex].name + " Volume";
            SetVolume(parameterName, Volumes[groupIndex]);
        }

        private void SetVolume(string parameterName, float volumeProportion)
        {
            float dbvolume = Mathf.Log10(Mathf.Clamp(volumeProportion, 0.01f, 1f)) * 40f;
            MasterMixer.SetFloat(parameterName, dbvolume);
        }

        private void SetupAudio()
        {
            foreach (Sound sound in Sounds)
            {
                AudioSource source = gameObject.AddComponent<AudioSource>();
                sound.Source = source;

                source.clip = sound.Clip;
                source.volume = sound.Volume;
                source.loop = sound.Loop;
                source.outputAudioMixerGroup = Array.Find(MixerGroups, group => group.name == sound.Type.ToString());
            }
            SetupVolumes(Volumes);
        }

        private void SetupVolumes(float[] setVolumes)
        {
            for (int i = 0; i < MixerGroups.Length; i++)
            {
                string name = MixerGroups[i].name + " Volume";
                Volumes[i] = setVolumes[i];
                SetVolume(name, Volumes[i]);
            }
        }

        public void Play(string soundName)
        {
            Sound sound = Array.Find(Sounds, sound => sound.SoundName == soundName);
            if (sound != null)
            {
                sound.Source.Play();
            }
            else
            {
                Debug.LogError("Audio: Sound '" + soundName + "' does not exist!");
            }
        }

        public void Stop(string soundName)
        {
            Sound sound = Array.Find(Sounds, sound => sound.SoundName == soundName);
            if (sound != null)
            {
                sound.Source.Stop();
            }
            else
            {
                Debug.LogError("Audio: Sound '" + soundName + "' does not exist!");
            }
        }

        public void Pause()
        {
            AudioListener.pause = true;
        }

        public void Resume()
        {
            AudioListener.pause = false;
        }
    }
}