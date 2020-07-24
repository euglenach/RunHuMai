using UniRx;
using UnityEngine;

namespace UniRx.Toolkit {
    public static class ObjectPoolExtension {
        public static T Rent<T>(this ObjectPool<T> pool,Transform parent)where T : UnityEngine.Component{
            var obj = pool.Rent();
            obj.transform.parent = parent;
            return obj;
        }
        
        public static T Rent<T>(this ObjectPool<T> pool,Vector3 position)where T : UnityEngine.Component{
            var obj = pool.Rent();
            obj.transform.position = position;
            return obj;
        }
        
        public static T Rent<T>(this ObjectPool<T> pool,Vector3 position,Quaternion rotation)where T : UnityEngine.Component{
            var obj = pool.Rent();
            obj.transform.position = position;
            obj.transform.rotation = rotation;
            return obj;
        }

    }
}