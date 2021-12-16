using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Giants {
    [CreateAssetMenu(fileName = "NewAudioData", menuName = "Giants/Audio/AudioData")]
    public class AudioData : ScriptableObject {
        public string ID {
            get { return m_id; }
        }
        public AudioClip Clip {
            get { return m_clip; }
        }
        public float Volume {
            get { return m_volume; }
        }
        public float Pan {
            get { return m_pan; }
        }

        [SerializeField]
        private string m_id;
        [SerializeField]
        private AudioClip m_clip;
        [SerializeField]
        private float m_volume = 1;
        [SerializeField]
        private float m_pan = 0f;

    }
}