using UnityEngine;
namespace game {
    public class ActionBase {
        protected GameObject m_gameObject = null;

        public ActionBase(GameObject gameObject) {
            m_gameObject = gameObject;
        }

        public virtual void Update() {
        }
    }
}