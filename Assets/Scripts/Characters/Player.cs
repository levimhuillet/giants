using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Giants {
    [RequireComponent(typeof(Animator))]
    public class Player : MonoBehaviour {
        public static Player instance;

        private Animator m_animator;

        [SerializeField]
        private int m_catchDistance;

        private void OnEnable() {
            if (instance == null) {
                instance = this;
                DontDestroyOnLoad(this.gameObject);
            }
            else if (this != instance) {
                Destroy(this.gameObject);
            }

            m_animator = this.GetComponent<Animator>();

            m_catchDistance *= -1; // camera is oriented backwards

            EventManager.OnPause.AddListener(HandleOnPause);
            EventManager.OnResume.AddListener(HandleEndPause);
        }

        public int GetCatchDistance() {
            return m_catchDistance;
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
