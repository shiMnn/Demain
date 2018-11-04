using UnityEngine;
namespace game {
    public class Action_Move : ActionBase {
        enum Step {
            None,
            Init,
            Movement,
        }

        int m_needFrame;
        int m_currentFrame;
        Vector3 m_startPos;
        Vector3 m_goalPos;

        int m_targetPosX;
        int m_targetPosY;
        int m_targetPosZ;


        Step m_step = Step.None;

        public Action_Move(GameObject gameobject) : base(gameobject) {
        }

        public override void Update() {
            base.Update();

            switch (m_step) {
                case Step.Movement: {
                        if (m_currentFrame <= m_needFrame) {
                            ++m_currentFrame;

                            var module = m_gameObject.GetComponent<CharacterBase>();
                            if (module) {
                                m_gameObject.transform.position = 
                                    Vector3.Lerp(m_startPos, m_goalPos, ((float)m_currentFrame / (float)m_needFrame));
                            }

                        } else {
                            var module = m_gameObject.GetComponent<CharacterBase>();
                            if (module) {
                                module.SetMapIndex(m_targetPosX, m_targetPosY, m_targetPosZ);
                            }
                            m_gameObject.transform.position = m_goalPos;
                            m_step = Step.None;
                        }
                    }
                    break;
            }
        }

        public void SetTarget(int x, int y, int z) {
            m_needFrame = 5;
            m_currentFrame = 0;

            m_targetPosX = x;
            m_targetPosY = y;
            m_targetPosZ = z;

            m_startPos = m_gameObject.transform.position;
            m_goalPos = new Vector3(GameDefine.BLOCK_SIZE_WIDTH * m_targetPosX,
                                    GameDefine.BLOCK_SIZE_HEIGHT * m_targetPosY,
                                    GameDefine.BLOCK_SIZE_DEPTH * m_targetPosZ);

            m_step = Step.Movement;
        }

        public bool IsBusy() {
            return (m_step != Step.None);
        }
    }
}