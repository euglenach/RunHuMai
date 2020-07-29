using Suima.Scene;
using UnityEngine;

namespace Result{
    public class ResultManager : MonoBehaviour{
        public bool IsClear{get;private set;}
        public Scene StageScene{get;private set;}
        
        public void Init(bool isClear,Scene scene){
            IsClear = isClear;
            StageScene = scene;
        }
    }
}
