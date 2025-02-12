using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class ScOptions : MonoBehaviour
{
    [SerializeField] private AudioMixer masterMixer;

    public void AdjustMasterVolume(float value)
    {
        SetMixerFloat(masterMixer, "Master", value);
    }

    public void AdjustMusicVolume(float value)
    {
        SetMixerFloat(masterMixer, "Music", value);
    }

    public void AdjustSoundVolume(float value)
    {
        SetMixerFloat(masterMixer, "Sound", value);
    }

    private void SetMixerFloat(AudioMixer mixer, string name, float value)
    {
        mixer.SetFloat(name, Mathf.Log10(value) * 20);
    }

    private void SetMixerFloat(AudioMixerGroup group, string name, float value)
    {
        SetMixerFloat(group.audioMixer, name, value);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
