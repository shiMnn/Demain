using UnityEngine;
namespace game {
    public class CameraControl : MonoBehaviour {
        Camera m_camera = null;
        private void Awake() {
            m_camera = gameObject.GetComponent<Camera>();
        }

        private void Update() {
            var controlCharacter = CharacterManager.Instance.GetControllableCharacter();
            if (controlCharacter != null) 
            {// 操作中のキャラクターにカメラを追従させる
                gameObject.transform.position = new Vector3(controlCharacter.transform.position.x + 1.5f,
                                                            controlCharacter.transform.position.y + 1.5f,
                                                            controlCharacter.transform.position.z + 4.0f);

                // 目標の方向を向く
                var diff = controlCharacter.transform.position - this.gameObject.transform.position;
                var look = Quaternion.LookRotation(diff);
                this.transform.rotation = look;
            }
        }
    }
}