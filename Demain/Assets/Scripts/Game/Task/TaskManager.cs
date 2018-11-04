using System.Collections.Generic;
namespace game {
    public class TaskManager : SingletonClass<TaskManager> {
        enum Step {
            None,
            Setup,
            Exec,
            Finish,
        }

        LinkedList<TaskNodeBase> m_taskList = new LinkedList<TaskNodeBase>();
        LinkedListNode<TaskNodeBase> m_currentNode = null;
        Step m_step = Step.None;

        public void Update() {
            switch (m_step) {
                case Step.None:
                    if (m_taskList.Count > 0) {
                        do {
                            if (m_currentNode == null) {
                                m_currentNode = m_taskList.First;
                            } else {
                                var nextNode = m_currentNode.Next;
                                m_taskList.Remove(m_currentNode);
                                m_currentNode = nextNode;
                            }
                        } while (m_currentNode == null);
                        m_step = Step.Setup;
                    }
                    break;
                case Step.Setup:
                    if (m_currentNode.Value.Setup()) {
                        m_step = Step.Exec;
                    }
                    break;

                case Step.Exec:
                    if (m_currentNode.Value.Exec()) {
                        m_step = Step.Finish;
                    }
                    break;

                case Step.Finish:
                    if (m_currentNode.Value.Finish()) {
                        m_step = Step.None;
                    }
                    break;
            }
        }

        public void AddFront(TaskNodeBase task) {
            m_taskList.AddFirst(task);
        }

        public void AddLast(TaskNodeBase task) {
            m_taskList.AddLast(task);
        }

        public void Next(TaskNodeBase baseTask, TaskNodeBase addTask) {
            var node = m_taskList.Find(baseTask);
            if (node != null) {
                m_taskList.AddAfter(node, addTask);
            } else {
                m_taskList.AddLast(addTask);
            }
        }
    }
}