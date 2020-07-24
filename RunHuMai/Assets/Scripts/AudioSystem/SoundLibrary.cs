using UnityEngine;
using System.Collections;
/// <summary>
/// http://ibako-study.hateblo.jp/entry/2014/02/06/225256
/// </summary>
public static class SoundLibrary{

    // オーディオの周波数を調べる
    // ac: 解析したいオーディオソース
    // qSamples: 解析結果のサイズ
    // threshold: ピッチの閾値
    public static SoundData AnalyzeSound(AudioSource ac, int qSamples, float threshold)
    {
        float[] spectrum = new float[qSamples];
        ac.GetSpectrumData(spectrum, 0, FFTWindow.BlackmanHarris);
        float maxV = 0;
        int maxN = 0;
        //最大値（ピッチ）を見つける。ただし、閾値は超えている必要がある
        for (int i = 0; i < qSamples; i++)
        {
            if (spectrum[i] > maxV && spectrum[i] > threshold)
            {
                maxV = spectrum[i];
                maxN = i;
            }
        }

        float freqN = maxN;
        if (maxN > 0 && maxN < qSamples - 1)
        {
            //隣のスペクトルも考慮する
            float dL = spectrum[maxN - 1] / spectrum[maxN];
            float dR = spectrum[maxN + 1] / spectrum[maxN];
            freqN += 0.5f * (dR * dR - dL * dL);
        }

        float pitchValue = freqN * (AudioSettings.outputSampleRate / 2) / qSamples;
        return new SoundData(pitchValue,freqN);
    }

    // ヘルツから音階への変換
    public static float ConvertHertzToScale(float hertz)
    {
        float value = 0.0f;
        if (hertz == 0.0f) return value;
        else
        {
            value = 12.0f * Mathf.Log(hertz / 110.0f) / Mathf.Log(2.0f);
            // while (value <= 12.0f) value += 12.0f;
            // while (value > 36.0f) value -= 12.0f;
            return value;
        }
    }

    // 数値音階から文字音階への変換
    public static string ConvertScaleToString(float scale)
    {
        // 12音階の何倍の精度で音階を見るか
        int precision = 2;

        // 今の場合だと、mod24が0ならA、1ならAとA#の間、2ならA#…
        int s = (int)scale;
        if (scale - s >= 0.5) s += 1; // 四捨五入
        s *= precision;

        int smod = s % (12 * precision); // 音階
        int soct = s / (12 * precision); // オクターブ

        string value; // 返す値

        if (smod == 0) value = "A";
        else if (smod == 1) value = "A+";
        else if (smod == 2) value = "A#";
        else if (smod == 3) value = "A#+";
        else if (smod == 4) value = "B";
        else if (smod == 5) value = "B+";
        else if (smod == 6) value = "C";
        else if (smod == 7) value = "C+";
        else if (smod == 8) value = "C#";
        else if (smod == 9) value = "C#+";
        else if (smod == 10) value = "D";
        else if (smod == 11) value = "D+";
        else if (smod == 12) value = "D#";
        else if (smod == 13) value = "D#+";
        else if (smod == 14) value = "E";
        else if (smod == 15) value = "E+";
        else if (smod == 16) value = "F";
        else if (smod == 17) value = "F+";
        else if (smod == 18) value = "F#";
        else if (smod == 19) value = "F#+";
        else if (smod == 20) value = "G";
        else if (smod == 21) value = "G+";
        else if (smod == 22) value = "G#";
        else value = "G#+";
        value += soct + 1;

        return value;
    }

    // 数値音階から生波形を出す
    public static void ScaleWave(float[] scale, int size, LineRenderer line)
    {
        line.SetVertexCount(size);
        float x = -150.0f;
        for (int i = 0; i < size; i++)
        {
            line.SetPosition(i, new Vector3(x, -80.0f + scale[i] * 2.5f, 0));
            x += 1.0f;
        }
    }
    
    public struct SoundData{
        private float pitch;
        private float volume;
        public float Pitch => pitch;
        public float Volume => volume;
        public SoundData(float pitch, float volume){
            this.pitch = pitch;
            this.volume = volume;
        }

    }
}