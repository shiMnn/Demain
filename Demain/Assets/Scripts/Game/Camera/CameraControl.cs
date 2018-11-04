using UnityEngine;
namespace game {
    public class CameraControl : MonoBehaviour {
        Camera m_camera = null;

        [SerializeField, Range(-10.0f, 10.0f)]
        private float offsetX = 0.0f;
        [SerializeField, Range(-10.0f, 10.0f)]
        private float offsetY = 4.0f;
        [SerializeField, Range(-10.0f, 10.0f)]
        private float offsetZ = 1.0f;

        private void Awake() {
            m_camera = gameObject.GetComponent<Camera>();
        }

        private void Update() {
            var controlCharacter = CharacterManager.Instance.GetControllableCharacter();
            if (controlCharacter != null) 
            {// 操作中のキャラクターにカメラを追従させる
                gameObject.transform.position = new Vector3(controlCharacter.transform.position.x + offsetX,
                                                            controlCharacter.transform.position.y + offsetY,
                                                            controlCharacter.transform.position.z + offsetZ);

                // 目標の方向を向く
                var diff = controlCharacter.transform.position - this.gameObject.transform.position;
                var look = Quaternion.LookRotation(diff);
                this.transform.rotation = look;
            }
        }
    }
}