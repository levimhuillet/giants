using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Giants {
    [RequireComponent(typeof(Animator))]
    public class Player : MonoBehaviour {
        public static Player instance;

        private Animator m_animator;

        private void OnEnable() {
            if (instance == null) {
                instance = this;
                DontDestroyOnLoad(this.gameObject);
            }
            else if (this != instance) {
                Destroy(this.gameObject);
            }

            m_animator = this.GetComponent<Animator>();

            EventManager.OnPause.AddListener(HandleOnPause);
            EventManager.OnResume.AddListener(HandleEndPause);
            EventManager.OnRestart.AddListener(HandleEndPause);
        }

        private void OnDestroy() {
            EventManager.OnPause.RemoveListener(HandleOnPause);
            EventManager.OnResume.RemoveListener(HandleEndPause);
            EventManager.OnRestart.RemoveListener(HandleEndPause);
        }

        private void HandleOnPause() {
            // pause animation
            m_animator.speed = 0;
        }

        private void HandleEndPause() {
            // resume animation
            m_animator.speed = 1;
        }
    }
}
