using System.Collections.Generic;
using UnityEngine;
namespace game {
    public class CharacterManager : SingletonMonoBehaviour<CharacterManager> {
        Dictionary<Power, List<GameObject>> m_characters;

        public CharacterManager() {
            m_characters = new Dictionary<Power, List<GameObject>>();
        }

        /// <summary>
        /// 勢力にキャラクターを追加する
        /// </summary>
        /// <param name="character"></param>
        /// <param name="power"></param>
        public void AddCharacter(GameObject character, Power power) {
            if (!m_characters.ContainsKey(power)) {
                m_characters.Add(power, new List<GameObject>());
            }
            List<GameObject> collection = null;
            if(m_characters.TryGetValue(Power.Friendly, out collection)) {
                if (collection != null) {
                    collection.Add(character);
                }
            }
        }

        /// <summary>
        /// 操作可能なキャラクターを取得する
        /// </summary>
        /// <returns>null返却の可能性あり</returns>
        public GameObject GetControllableCharacter() {
            List<GameObject> collection = null;
            if (m_characters.TryGetValue(Power.Friendly, out collection)) {
                if (collection != null) {
                    foreach(var character in collection) {
                        var module = character.GetComponent<CharacterBase>();
                        if (module != null) {
                            if (module.Controlling) {
                                return character;
                            }
                        }
                    }
                }
            }

            return null;
        }
    }
}