using Players;
using Suima.Scene;
using UnityEngine;

namespace Result{
    public class ResultManager : MonoBehaviour{
        public bool IsClear{get;private set;}
        public Scene StageScene{get;private set;}
        public Character Character{get;private set;}
        
        public void Init(bool isClear,Scene scene,Character character){
            IsClear = isClear;
            StageScene = scene;
            Character = character;
        }
    }
}
