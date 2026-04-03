using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    [SerializeField]
    private Toggle soundToggle;
    [SerializeField]
    private Slider musicSlider;
    [SerializeField]
    private Slider EffectSlider;
    [SerializeField]
    private PlayerHealth playerAudio;
    [SerializeField]
    private Gun gunAudio;
    [SerializeField] 
    private Enemy bearAudio;
    [SerializeField]
    private Enemy bunnyAudio;
    [SerializeField]
    private Enemy elephantAudio;
    [SerializeField]
    private GameManager gameManager;

    private void Awake()
    {
        soundToggle.isOn = true;
        musicSlider.value = 1f;
        EffectSlider.value = 1f;

        playerAudio.GetComponent<AudioSource>().volume = EffectSlider.value;
        gunAudio.GetComponent<AudioSource>().volume = EffectSlider.value;
        bearAudio.GetComponent<AudioSource>().volume = EffectSlider.value;
        bunnyAudio.GetComponent<AudioSource>().volume = EffectSlider.value;
        elephantAudio.GetComponent<AudioSource>().volume = EffectSlider.value;

        Camera.main.GetComponent<AudioSource>().volume = musicSlider.value;


    }

    void Update()
    {
        if(gameManager.isGameEnd == true)
        {
            Camera.main.GetComponent<AudioSource>().mute = true;
            return;
        }

        playerAudio.GetComponent<AudioSource>().mute = !soundToggle.isOn;
        gunAudio.GetComponent<AudioSource>().mute = !soundToggle.isOn;
        bearAudio.GetComponent<AudioSource>().mute = !soundToggle.isOn;
        bunnyAudio.GetComponent<AudioSource>().mute = !soundToggle.isOn;
        elephantAudio.GetComponent<AudioSource>().mute = !soundToggle.isOn;

        Camera.main.GetComponent<AudioSource>().mute = !soundToggle.isOn;


        playerAudio.GetComponent<AudioSource>().volume = EffectSlider.value;
        gunAudio.GetComponent<AudioSource>().volume = EffectSlider.value;
        bearAudio.GetComponent<AudioSource>().volume = EffectSlider.value;
        bunnyAudio.GetComponent<AudioSource>().volume = EffectSlider.value;
        elephantAudio.GetComponent<AudioSource>().volume = EffectSlider.value;

        Camera.main.GetComponent<AudioSource>().volume = musicSlider.value;
    }
}
