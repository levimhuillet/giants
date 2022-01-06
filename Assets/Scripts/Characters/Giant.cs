using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Giants {
    [RequireComponent(typeof(Animator))]
    public class Giant : MonoBehaviour {
        private Animator m_animator;

        [SerializeField]
        private bool m_isStaggered;
        [SerializeField]
        private float m_speed;
        [SerializeField]
        private float m_magRate;

        public struct Track {
            public Vector3 StartPos;
            public Vector3 FinalPos;
            public Vector3 MoveVector;

            public Track(Vector3 start, Vector3 final) {
                StartPos = start;
                FinalPos = final;
                MoveVector = FinalPos - StartPos;
            }
        }

        private Track m_track;

        private float m_startScale;
        private int m_playerCatchDistance;
        private float m_moveInwardRatio;
        private float m_gapOffset;

        private void OnEnable() {
            m_animator = this.GetComponent<Animator>();

            m_animator.SetBool("isStaggered", m_isStaggered);

            m_startScale = this.transform.localScale.x;

            m_playerCatchDistance = RunManager.instance.GetPlayerCatchDistance();

            m_gapOffset = RunManager.instance.GetGapBetweenGiants();

            if (this.transform.position.x < 0) {
                m_gapOffset *= -1;
            }

            Vector3 startPos = this.transform.position;
            Vector3 finalPos = new Vector3(m_gapOffset, this.transform.position.y, m_playerCatchDistance);

            m_track = new Track(startPos, finalPos);

            m_moveInwardRatio = (this.transform.position.x - m_gapOffset) / m_playerCatchDistance;

            EventManager.OnPause.AddListener(HandleOnPause);
            EventManager.OnResume.AddListener(HandleEndPause);
            EventManager.OnRestart.AddListener(HandleOnRestart);
            EventManager.OnReturnMain.AddListener(HandleOnReturnMain);
        }

        private void OnDestroy() {
            EventManager.OnPause.RemoveListener(HandleOnPause);
            EventManager.OnResume.RemoveListener(HandleEndPause);
            EventManager.OnRestart.RemoveListener(HandleOnRestart);
            EventManager.OnReturnMain.RemoveListener(HandleOnReturnMain);
        }

        private void Update() {
            if (GameManager.instance.IsPaused) {
                return;
            }

            // move closer to camera
            Vector3 currPos = this.transform.position;
            Vector3 newPos = currPos + new Vector3(m_moveInwardRatio, 0, -m_speed) * Time.deltaTime;

            this.transform.position = newPos;

            // increase magnification
            //float currMag = m_startScale + Mathf.Pow(this.transform.position.z * m_magRate, 2);
            //this.transform.localScale = new Vector3(currMag, currMag, 1);

            if (this.transform.position.z <= m_playerCatchDistance) {
                EventManager.OnGameOver.Invoke();
            }
        }

        private void HandleOnPause() {
            // pause animation
            m_animator.speed = 0;
        }

        private void HandleEndPause() {
            // resume animation
            m_animator.speed = 1;
        }

        private void HandleOnRestart() {
            HandleEndPause();
            ResetPosition();
        }

        private void HandleOnReturnMain() {
            ResetPosition();
        }

        private void ResetPosition() {
            // reset Giant to prevent triggering GameOver
            this.transform.position = m_track.StartPos;
        }

        public Track GetTrack() {
            return m_track;
        }

        public void KnockBack(float knockBackDistance) {
            Vector3 currPos = this.transform.position;
            Vector3 newPos = currPos - m_track.MoveVector.normalized * knockBackDistance;

            this.transform.position = newPos;
        }
    }
}
