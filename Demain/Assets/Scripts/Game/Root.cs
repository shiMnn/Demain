using UnityEngine;
namespace game {
    public class Root : MonoBehaviour {
        private void Awake() {
            TaskManager.Instance.AddLast(new TaskNode_GameSetup());
        }

        private void Update() {
            TaskManager.Instance.Update();
        }
    }
}
