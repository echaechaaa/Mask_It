using UnityEngine;

public class AudioEvent : MonoBehaviour
{
    [EasyButtons.Button]
    public void ToggleMusic()
    {
        AudioManager.Instance.ToggleMusic();
    }

    [EasyButtons.Button]
    public void ToggleSFX()
    {
        AudioManager.Instance.ToggleSFX();
    }
    public void PlaySoundEffect(AudioClip soundEffectToPlay)
    {
        AudioManager.Instance.PlaySFX(soundEffectToPlay); //SFX are oneshot
    }

    public void SetVolumeTo(float value)
    {
        AudioManager.Instance.SetVolumeTo(value);
    }
}
