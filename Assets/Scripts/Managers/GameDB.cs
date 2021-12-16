using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Giants {
    public class GameDB : MonoBehaviour {
        public static GameDB instance;

        private Dictionary<string, AudioData> m_audioMap;

        #region Editor

        [SerializeField]
        private AudioData[] m_audioData;

        #endregion  

        public AudioData GetAudioData(string id) {
            // initialize the map if it does not exist
            if (instance.m_audioMap == null) {
                instance.m_audioMap = new Dictionary<string, AudioData>();
                foreach (AudioData data in instance.m_audioData) {
                    instance.m_audioMap.Add(data.ID, data);
                }
            }
            if (instance.m_audioMap.ContainsKey(id)) {
                return instance.m_audioMap[id];
            }
            else {
                throw new KeyNotFoundException(string.Format("No Audio " +
                    "with id `{0}' is in the database", id
                ));
            }
        }

        #region Unity Callbacks

        private void OnEnable() {
            if (instance == null) {
                instance = this;
            }
            else if (instance != this) {
                Destroy(this.gameObject);
            }
        }

        #endregion
    }
}