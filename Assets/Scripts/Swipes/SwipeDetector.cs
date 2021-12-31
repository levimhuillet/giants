using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Giants {
    public class SwipeDetector : MonoBehaviour {
        private Vector2 m_fingerDownPosition;
        private Vector2 m_fingerUpPosition;

        [SerializeField]
        private ObjectGenerator m_leftGenerator, m_rightGenerator;

        [SerializeField]
        private bool m_detectSwipeOnlyAfterRelease = false;

        [SerializeField]
        private float m_minDistanceForSwipe = 20f;

        public static event Action<SwipeData> OnSwipe = delegate { };

        private void Update() {
            if (GameManager.instance.IsPaused) {
                return;
            }

            foreach (Touch touch in Input.touches) {
                if (touch.phase == TouchPhase.Began) {
                    m_fingerUpPosition = touch.position;
                    m_fingerDownPosition = touch.position;
                }

                if (!m_detectSwipeOnlyAfterRelease && touch.phase == TouchPhase.Moved) {
                    m_fingerDownPosition = touch.position;
                    DetectSwipe();
                }

                if (touch.phase == TouchPhase.Ended) {
                    m_fingerDownPosition = touch.position;
                    DetectSwipe();
                }
            }
        }

        private void DetectSwipe() {
            if (SwipeDistanceCheckMet()) {
                if (IsVerticalSwipe()) {
                    SwipeDir dir = (m_fingerDownPosition.y - m_fingerUpPosition.y > 0) ? SwipeDir.Up : SwipeDir.Down;
                    SendSwipe(dir);
                }
                else {
                    SwipeDir dir = (m_fingerDownPosition.x - m_fingerUpPosition.x > 0) ? SwipeDir.Right : SwipeDir.Left;
                    SendSwipe(dir);
                }

                m_fingerUpPosition = m_fingerDownPosition;
            }
        }

        private bool SwipeDistanceCheckMet() {
            return VerticalDistance() > m_minDistanceForSwipe
                || HorizontalDistance() > m_minDistanceForSwipe;
        }

        private float VerticalDistance() {
            return Mathf.Abs(m_fingerDownPosition.y - m_fingerUpPosition.y);
        }

        private float HorizontalDistance() {
            return Mathf.Abs(m_fingerDownPosition.x - m_fingerUpPosition.x);
        }

        private bool IsVerticalSwipe() {
            return VerticalDistance() > HorizontalDistance();
        }

        private void SendSwipe(SwipeDir dir) {
            SwipeData swipeData = new SwipeData() {
                Dir = dir,
                StartPosition = m_fingerDownPosition,
                EndPosition = m_fingerUpPosition
            };
            OnSwipe(swipeData);
        }

        public struct SwipeData {
            public Vector2 StartPosition;
            public Vector2 EndPosition;
            public SwipeDir Dir;
        }

        public enum SwipeDir {
            Up,
            Down,
            Left,
            Right
        }

        #region Debug

        private void Awake() {
            OnSwipe += HandleOnSwipe;
        }

        private void HandleOnSwipe(SwipeData data) {
            if (data.StartPosition.x > Screen.width / 2 && data.Dir == SwipeDir.Left) {
                // right half
                m_rightGenerator.ThrowWallObject();
            }
            else if ((data.StartPosition.x <= Screen.width / 2 && data.Dir == SwipeDir.Right)) {
                // left half
                m_leftGenerator.ThrowWallObject();
            }
        }

        #endregion
    }
}