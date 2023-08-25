using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public static class SettingData
{
    public static bool isFullScreen = true;
    public static AudioMixer mixer;
    public static int quality;
    public static float volume;
    public static int rslWidth;
    public static int rslHeight;
}
