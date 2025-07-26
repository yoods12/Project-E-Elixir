using UnityEngine;
using UnityEngine.UI;

public class VolumeSliderInitializer : MonoBehaviour
{
    public enum VolumeType { Master, Music, SFX }

    [SerializeField] private VolumeType type;
    [SerializeField] private Slider slider;

    private const string MasterKey = "MasterVolumePref";
    private const string MusicKey = "MusicVolumePref";
    private const string SFXKey = "SFXVolumePref";

    private void Start()
    {
        // 1) ����� ������ �����̴� �ʱ�ȭ
        float saved = type switch
        {
            VolumeType.Master => PlayerPrefs.GetFloat(MasterKey, 0.75f),
            VolumeType.Music => PlayerPrefs.GetFloat(MusicKey, 0.75f),
            VolumeType.SFX => PlayerPrefs.GetFloat(SFXKey, 0.75f),
            _ => 0.75f
        };
        slider.value = saved;

        // 2) ��ȭ�� ���� �� �Ŵ����� ����
        slider.onValueChanged.AddListener(value => {
            switch (type)
            {
                case VolumeType.Master:
                    AudioSettingsManager.Instance.SetMasterVolume(value);
                    break;
                case VolumeType.Music:
                    AudioSettingsManager.Instance.SetMusicVolume(value);
                    break;
                case VolumeType.SFX:
                    AudioSettingsManager.Instance.SetSFXVolume(value);
                    break;
            }
        });
    }
}
