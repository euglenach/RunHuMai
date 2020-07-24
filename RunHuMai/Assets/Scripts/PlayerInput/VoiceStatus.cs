namespace PlayerInput{
    public struct VoiceStatus{
        private int separateNum;
        private int value;
        private float volume;
        public VoiceStatus(int separateNum, int value, float volume){
            this.separateNum = separateNum;
            this.value = value;
            this.volume = volume;
        }
        public int SeparateNum => separateNum;
        public int Value => value;
        public float Volume => volume;
    }
}
