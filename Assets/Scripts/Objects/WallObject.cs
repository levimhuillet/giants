using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Giants {
    [RequireComponent(typeof(SpriteRenderer))]
    public class WallObject : MonoBehaviour {
        [SerializeField]
        private float m_speed;
        [SerializeField]
        private float m_vanishSpeed;
        [SerializeField]
        private float m_knockBackDistance;

        private Vector3 m_dir;

        public bool Thrown {
            get;
            set;
        }

        private void Update() {
            if (GameManager.instance.IsPaused) {
                return;
            }

            if (Thrown) {
                this.transform.position = this.transform.position + m_dir * m_speed * Time.deltaTime;
                this.transform.localScale -= new Vector3(m_vanishSpeed, m_vanishSpeed, m_vanishSpeed) * Time.deltaTime;

                if (this.gameObject.transform.position.z >= 0 || this.gameObject.transform.localScale.x <= 0) {
                    Destroy(this.gameObject);
                }
            }
            else {
                this.transform.position = this.transform.position + m_dir * m_speed * Time.deltaTime;
                this.transform.localScale -= new Vector3(m_vanishSpeed, m_vanishSpeed, m_vanishSpeed) * Time.deltaTime;
            }
        }

        private void OnTriggerEnter(Collider other) {
            // check for collision with giant
            Giant giant = other.gameObject.GetComponent<Giant>();
            if (giant != null) {
                // knock giant backwards
                giant.KnockBack(m_knockBackDistance);

                // remove this object
                Destroy(this.gameObject);
            }
        }

        public void SetDir(Transform focalPoint) {
            m_dir = (focalPoint.position - this.transform.position).normalized;
        }
        public void SetDir(Vector3 dir) {
            m_dir = dir;
        }

        public void MarkThrown() {
            Thrown = true;
            SpriteRenderer sr = GetComponent<SpriteRenderer>();
            sr.sortingLayerName = "Running";
            sr.sortingOrder = 0;
        }
    }
}