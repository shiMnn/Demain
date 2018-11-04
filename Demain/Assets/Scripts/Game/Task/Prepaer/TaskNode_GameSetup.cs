using UnityEngine;

namespace game {
    public class TaskNode_GameSetup : TaskNodeBase{
        public override bool Setup() {
            // プレイヤーの作成
            var playerPrefab = (GameObject)Resources.Load("Game/Player/Player");
            if (playerPrefab != null) {
                var player = GameObject.Instantiate(playerPrefab, CharacterManager.Instance.gameObject.transform);
                var module = player.GetComponent<CharacterBase>();
                if (module != null) {
                    module.Initialize(0, 0);
                }
                CharacterManager.Instance.AddCharacter(player, Power.Friendly);
            }
            
            return true;
        }
        public override bool Exec() {
            return true;
        }
        public override bool Finish() {
            TaskManager.Instance.Next(this, new TaskNode_CreateMap());
            return true;
        }
    }
}