using UnityEngine;

namespace game {
    public class TaskNode_Move : TaskNodeBase {
        enum Step {
            Init,
            Processing,
            Finish,

            ForceQuit,
        }

        Direction m_direction = Direction.None;
        Step m_step = Step.Init;
        CharacterBase m_characterModule = null;

        public TaskNode_Move(Direction direction) {
            m_direction = direction;

            bool error = true;
            var gameObject = CharacterManager.Instance.GetControllableCharacter();
            if(gameObject != null) {
                m_characterModule = gameObject.GetComponent<CharacterBase>();
                if (m_characterModule != null) {
                    error = false;
                }
            }

            if (error) {
                m_step = Step.ForceQuit;
            }
        }

        public override bool Exec() {
            switch (m_step) {
                case Step.Init: {
                        int targetPosX = m_characterModule.MapIndex_X;
                        int targetPosY = m_characterModule.MapIndex_Y;
                        switch (m_direction) {
                            case Direction.Top: {
                                    --targetPosY;
                                }
                                break;
                            case Direction.Bottom: {
                                    ++targetPosY;
                                }
                                break;
                            case Direction.Left: {
                                    ++targetPosX;
                                }
                                break;
                            case Direction.Right: {
                                    --targetPosX;
                                }
                                break;
                        }
                        m_characterModule.SetTargetPos(targetPosX, targetPosY);
                    }
                    return true;
                case Step.Processing: {
                    }
                    break;
                case Step.Finish: {
                    }
                    break;
                case Step.ForceQuit: {
                    }
                    return true;
            }
            return false;
        }

        public override bool Finish() {
            return true;
        }

        public override bool Setup() {
            TaskManager.Instance.Next(this, new TaskNode_PlayPhase());
            return true;
        }
    }
}