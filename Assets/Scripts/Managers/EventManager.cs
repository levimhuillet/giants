using UnityEngine;
using UnityEngine.Events;

namespace Giants {
    public class EventManager : MonoBehaviour {
        public static EventManager instance;

        public static UnityEvent OnStart;
        public static UnityEvent OnPause;
        public static UnityEvent OnResume;
        public static UnityEvent OnRestart;
        public static UnityEvent OnGameOver;
        public static UnityEvent OnReturnMain;

        // public static UnityEvent OnNewLife;

        // public static UnityEvent OnScoreChanged;

        // public static UnityEvent OnNoAds;
        // public static UnityEvent OnAdReward;

        private void OnEnable() {
            if (instance == null) {
                instance = this;
                DontDestroyOnLoad(this.gameObject);
            }
            else if (this != instance) {
                Destroy(this.gameObject);
            }

            OnStart = new UnityEvent();
            OnPause = new UnityEvent();
            OnResume = new UnityEvent();
            OnRestart = new UnityEvent();
            OnGameOver = new UnityEvent();
            // OnNewLife = new UnityEvent();
            OnReturnMain = new UnityEvent();

            // OnTurnCorner = new UnityEvent();

            // OnScoreChanged = new UnityEvent();

            // OnNoAds = new UnityEvent();
            // OnAdReward = new UnityEvent();
        }
    }
}