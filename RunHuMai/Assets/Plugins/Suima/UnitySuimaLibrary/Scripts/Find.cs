using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Suima{
    static public class Find{
        /// <summary>
        /// 指定したInterfaceを実装しているコンポーネントをもつゲームオブジェクトを検索する
        /// </summary>
        public static GameObject[] FindObjectsOfComponent<T>() where T : class{
            var list = new List<GameObject>();            
            foreach (var item in GameObject.FindObjectsOfType<MonoBehaviour>()){
                if(item is T){
                    list.Add(item.gameObject);
                }
            }

            return list.ToArray();
        }

        /// <summary>
        /// 指定したInterfaceを実装しているコンポーネントをもつゲームオブジェクト一つを検索する
        /// </summary>
        public static GameObject FindObjectOfComponent<T>() where T : class{
            foreach (var item in GameObject.FindObjectsOfType<MonoBehaviour>()){
                if(item is T){ return item.gameObject; }
            }
            return null;
        }
        
        /// <summary>
        /// 指定したInterfaceを実装しているコンポーネントを検索する
        /// </summary>
        public static T[] FindComponents<T>() where T : class{
            return GameObject.FindObjectsOfType<MonoBehaviour>()
                             .OfType<T>()
                             .ToArray();
        }
        
        /// <summary>
        /// 指定したInterfaceを実装しているコンポーネントを一つを検索する
        /// </summary>
        public static T FindComponent<T>() where T : class{
            return GameObject.FindObjectsOfType<MonoBehaviour>()
                             .OfType<T>()
                             .FirstOrDefault();
        }
    }
}
