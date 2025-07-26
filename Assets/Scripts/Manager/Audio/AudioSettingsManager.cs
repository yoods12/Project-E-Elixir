using UnityEngine;
using UnityEngine.Audio;

public class AudioSettingsManager : MonoBehaviour
{
    public static AudioSettingsManager Instance { get; private set; }

    [SerializeField] private AudioMixer audioMixer;
    private const string MasterVolume = "MasterVolume";
    private const string SFXVolume = "SFXVolume";
    private const string MusicVolume = "MusicVolume";

    private const string MasterKey = "MasterVolumePref";
    private const string SFXKey = "SFXVolumePref";
    private const string MusicKey = "MusicVolumePref";

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadSettings();
    }

    public void SetMasterVolume(float linear)
    {
        float dB = Mathf.Log10(Mathf.Clamp(linear, 0.0001f, 1f)) * 20f;
        audioMixer.SetFloat(MasterVolume, dB);
        PlayerPrefs.SetFloat(MasterKey, linear);
        PlayerPrefs.Save();
    }
    public void SetMusicVolume(float linear)
    {
        float dB = Mathf.Log10(Mathf.Clamp(linear, 0.0001f, 1f)) * 20f;
        audioMixer.SetFloat(MusicVolume, dB);
        PlayerPrefs.SetFloat(MusicKey, linear);
        PlayerPrefs.Save();
    }
    public void SetSFXVolume(float linear)
    {
        float dB = Mathf.Log10(Mathf.Clamp(linear, 0.0001f, 1f)) * 20f;
        audioMixer.SetFloat(SFXVolume, dB);
        PlayerPrefs.SetFloat(SFXKey, linear);
        PlayerPrefs.Save();
    }

    private void LoadSettings()
    {
        float master = PlayerPrefs.GetFloat(MasterKey, 0.75f);
        float music = PlayerPrefs.GetFloat(MusicKey, 0.75f);
        float sfx = PlayerPrefs.GetFloat(SFXKey, 0.75f);

        SetMasterVolume(master);
        SetMusicVolume(music);
        SetSFXVolume(sfx);
    }
}
