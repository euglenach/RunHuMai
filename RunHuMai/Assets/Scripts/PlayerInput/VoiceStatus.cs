namespace PlayerInput{
    public struct VoiceStatus{
        private int pitch;
        private float volume;
        public VoiceStatus(int pitch,float volume){
            this.pitch = pitch;
            this.volume = volume;
        }
        public int Pitch => pitch;
        public float Volume => volume;
    }
}
