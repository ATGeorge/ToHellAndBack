using UnityEngine;

namespace ToHellAndBack
{
    [System.Serializable]
    public class Sound
    {
        // Public Fields
        public string SoundName;
        public AudioClip Clip;
        public SoundType Type;
        [Range(0f, 1f)]
        public float Volume;
        public bool Loop;

        [HideInInspector]
        public AudioSource Source;
    }

    public enum SoundType
    {
        Music,
        SFX,
        Background
    }
}
