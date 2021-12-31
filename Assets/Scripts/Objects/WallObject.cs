using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Giants {
    public class WallObject : MonoBehaviour {
        [SerializeField]
        private float m_speed;
        [SerializeField]
        private float m_vanishSpeed;

        private Vector3 m_dir;

        private void Update() {
            if (GameManager.instance.IsPaused) {
                return;
            }

            this.transform.position = this.transform.position + m_dir * m_speed * Time.deltaTime;
            this.transform.localScale -= new Vector3(m_vanishSpeed, m_vanishSpeed, 0) * Time.deltaTime;
        }

        public void SetDir(Transform focalPoint) {
            m_dir = (focalPoint.position - this.transform.position).normalized;
        }
    }
}