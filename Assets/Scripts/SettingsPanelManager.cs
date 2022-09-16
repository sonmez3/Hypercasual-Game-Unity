using UnityEngine;
using UnityEngine.UI;

public class SettingsPanelManager : MonoBehaviour
{
    [SerializeField] private Transform SoundGroup;
    [SerializeField] private GameObject VibrationOnButton;
    [SerializeField] private GameObject VibrationOffButton;
    [SerializeField] private GameObject CreditsPanel;
    private void Start()
    {
        SoundGroup.GetChild(2).GetComponent<Slider>().value = PlayerPrefs.GetFloat("SoundEffectVolumeLevel");
        AudioManager.instance.ChangeEffectVolume(PlayerPrefs.GetFloat("SoundEffectVolumeLevel"));
        if (PlayerPrefs.GetInt("IsVibration") == 1)
        {
            VibrationOnButton.SetActive(true);
        }
        else
        {
            VibrationOffButton.SetActive(true);
        }
    }

    
    public void RedirectToInstagram()
    {
        Application.OpenURL("https://instagram.com/anemogames");
    }

    public void RedirectToDiscord()
    {
        Application.OpenURL("https://discord.gg/bjrtKTzKY6");
    }

    public void RedirectToZapsplat()
    {
        Application.OpenURL("https://www.zapsplat.com");
    }

    public void SendEmail()
    {
        string email = "info@anemogames.com";
        string subject = MyEscapeURL("support");
        string body = MyEscapeURL("body");
        Application.OpenURL("mailto:" + email + "?subject=" + subject + "&body=" + body);
    }

    public static string MyEscapeURL(string url)
    {
        return WWW.EscapeURL(url).Replace("+", "%20");
    }


    public void SoundSliderValueChangeMethod()
    {
        PlayerPrefs.SetFloat("SoundEffectVolumeLevel", SoundGroup.GetChild(2).GetComponent<Slider>().value);
        AudioManager.instance.ChangeEffectVolume(PlayerPrefs.GetFloat("SoundEffectVolumeLevel"));
    }

    public void VibrationOnButtonMethod()
    {
        VibrationOnButton.SetActive(false);
        VibrationOffButton.SetActive(true);
        PlayerPrefs.SetInt("IsVibration", 0);
    }

    public void VibrationOffButtonMethod()
    {
        VibrationOffButton.SetActive(false);
        VibrationOnButton.SetActive(true);
        PlayerPrefs.SetInt("IsVibration", 1);
    }

    public void OpenCreditsPanel()
    {
        CreditsPanel.SetActive(true);
    }

    public void CloseCreditsPanel()
    {
        CreditsPanel.SetActive(false);
    }
}
