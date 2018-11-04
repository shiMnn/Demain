using UnityEngine;
namespace game {
    public class CameraControl : MonoBehaviour {
        Camera m_camera = null;

        [SerializeField, Range(-10.0f, 10.0f)]
        private float offSetX = 0.0f;
        [SerializeField, Range(-10.0f, 10.0f)]
        private float offSetY = 4.0f;
        [SerializeField, Range(-10.0f, 10.0f)]
        private float offsetZ = 0.0f;

        private void Awake() {
            m_camera = gameObject.GetComponent<Camera>();
        }

        private void Update() {
            var controlCharacter = CharacterManager.Instance.GetControllableCharacter();
            if (controlCharacter != null) 
            {// 操作中のキャラクターにカメラを追従させる
                gameObject.transform.position = new Vector3(controlCharacter.transform.position.x + offSetX,
                                                            controlCharacter.transform.position.y + offSetY,
                                                            controlCharacter.transform.position.z + offsetZ);

                // 目標の方向を向く
                var diff = controlCharacter.transform.position - this.gameObject.transform.position;
                var look = Quaternion.LookRotation(diff);
                this.transform.rotation = look;
            }
        }
    }
}