using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioMixer myMixer;
    [SerializeField] private Slider BGMSlider;
    [SerializeField] private Slider EffectSlider;

    public AudioClip MainSceneClip;
    public AudioClip IntroClip;
    public AudioClip StoryClip;
    public AudioClip InGameClip;
    public AudioClip EndClip;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        if (PlayerPrefs.HasKey("BGMVolume"))
        {
            LoadVolume();
        }
        else
        {
            SetBGMVolume();
        }
        SetBGMVolume();
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        string SceneName = scene.name;

        AudioClip clipToPlay = SceneName switch
        {
            "MainScene" => MainSceneClip,
            "StartStoryScene" => IntroClip,
            "StartCinema" or "Stage1-1Story" or "Stage1-2Story" or "Stage1-3Story" => StoryClip,
            _ when SceneName.StartsWith("Stage") => InGameClip,
            "End" => EndClip,
            _ => null
        };

        if (clipToPlay != null)
        {
            audioSource.clip = clipToPlay;
            audioSource.Play();
        }
    }

    private void OnEnable() => SceneManager.sceneLoaded += OnSceneLoaded;
    private void OnDisable() => SceneManager.sceneLoaded -= OnSceneLoaded;

    public void SetBGMVolume()
    {
        float volume = BGMSlider.value;
        myMixer.SetFloat("BGM", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("BGMVolume", volume);
    }

    public void SetEffectVolume()
    {
        float volume = EffectSlider.value;
        myMixer.SetFloat("Eff", Mathf.Log10(volume) * 20);
    }

    private void LoadVolume()
    {
        BGMSlider.value = PlayerPrefs.GetFloat("BGMVolume");
        SetBGMVolume();
    }
}
