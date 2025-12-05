using UnityEngine;

[CreateAssetMenu(fileName = "AudioSettingsData", menuName = "Scriptable Objects/AudioSettingsData")]
public class AudioSettingsData : ScriptableObject
{
    public SceneMusicEntry[] entries;
    public float StartVolume;
}

[System.Serializable]
public class SceneMusicEntry
{
    public string sceneName;
    public AudioClip musicClip;
}
