using UnityEngine;

namespace Systems{
    [CreateAssetMenu(fileName = "SoundDatabase", menuName = "SoundDatabase", order = 0)]
    public class SoundDatabase : ScriptableObject{
        [SerializeField] private AudioClip title;
        [SerializeField] private AudioClip stage;
        [SerializeField] private AudioClip gameOver;
        [SerializeField] private AudioClip gameClear;
        [SerializeField] private AudioClip settingOpen;
        [SerializeField] private AudioClip settingClose;
        [SerializeField] private AudioClip charaChange;
        [SerializeField] private AudioClip otherButtonClick;
        [SerializeField] private AudioClip otherButtonClick2;
        [SerializeField] private AudioClip result;

        public AudioClip Title => title;
        public AudioClip Stage => stage;
        public AudioClip GameOver => gameOver;
        public AudioClip GameClear => gameClear;
        public AudioClip SettingOpen => settingOpen;
        public AudioClip SettingClose => settingClose;
        public AudioClip CharaChange => charaChange;
        public AudioClip OtherButtonClick => otherButtonClick;
        public AudioClip OtherButtonClick2 => otherButtonClick2;
        public AudioClip Result => result;
    }
}
