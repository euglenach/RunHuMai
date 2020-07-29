using System;
using UnityEngine;

/// <summary>
/// Sceneをまたいで存在したくないシングルトン
/// Sceneをまたぎたいときは派生クラスで各自
/// </summary>
public abstract class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour{
    private static T instance;

    public static T Instance{
        get{
            if(instance == null){
                var t = typeof(T);

                instance = (T)FindObjectOfType(t);
                if(instance == null){
                    //Debug.LogError (t + " をアタッチしているGameObjectはありません");
                }
            }

            return instance;
        }
    }

    protected virtual void Awake(){
        // 他のゲームオブジェクトにアタッチされているか調べる
        // アタッチされている場合は破棄する。
        CheckInstance();
    }

    protected bool CheckInstance(){
        if(instance == null){
            instance = this as T;
            return true;
        } else if(Instance == this){ return true; }

        Destroy(gameObject);
        return false;
    }
}
