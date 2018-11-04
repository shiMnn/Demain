using UnityEngine;

namespace game {
    public class TaskNode_SpawnPlayer : TaskNodeBase {
        public override bool Setup() {
            return true;
        }

        public override bool Exec() {
            // プレイヤーの座標をランダムな部屋の位置にする
            int posX = 0; UnityEngine.Random.Range(0, GameDefine.FLOOR_MAX_WIDTH);
            int posz = 0; UnityEngine.Random.Range(0, GameDefine.FLOOR_MAX_HEIGHT);

            GameObject block = null;
            while (block == null) {
                posX = UnityEngine.Random.Range(0, GameDefine.FLOOR_MAX_WIDTH);
                posz = UnityEngine.Random.Range(0, GameDefine.FLOOR_MAX_HEIGHT);

                block = BlockManager.Instance.GetBlock(posX, 0, posz);
                var module = block.GetComponent<BlockBase>();
                if (module != null) {
                    if (!module.IsRoom || module.IsOnAbove()) 
                    {// 部屋の床じゃない or 上に何か乗っている
                        block = null;
                    }
                }
            }

            // プレイヤーの作成
            var playerPrefab = (GameObject)Resources.Load("Game/Player/Player");
            if (playerPrefab != null) {
                var player = GameObject.Instantiate(playerPrefab, CharacterManager.Instance.gameObject.transform);
                var module = player.GetComponent<CharacterBase>();
                if (module != null) {
                    module.Initialize(posX, 1, posz);
                }
                CharacterManager.Instance.AddCharacter(player, Power.Friendly);
            }
            return true;
        }

        public override bool Finish() {
            TaskManager.Instance.Next(this, new TaskNode_PlayPhase());
            return true;
        }
    }
}