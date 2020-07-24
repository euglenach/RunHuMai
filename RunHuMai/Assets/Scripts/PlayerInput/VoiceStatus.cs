namespace PlayerInput{
    public struct VoiceStatus{
        private int separateNum;
        private int pitch;
        private float volume;
        public VoiceStatus(int pitch, int separateNum, float volume){
            this.separateNum = separateNum;
            this.pitch = pitch;
            this.volume = volume;
        }
        public int SeparateNum => separateNum;
        public int Pitch => pitch;
        public float Volume => volume;
    }
}
