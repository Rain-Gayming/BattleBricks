using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using System.IO;
using Sirenix.OdinInspector;
using ES3Internal;
using TMPro;

[System.Serializable]
public class SettingsData
{
    
    public int fullscreen;
    [Space(10)]
    public int resolutionIndex;
    
    [Header("Audio")]
    [Space(10)]
    public float masterVolume;
    [Space(10)]
    public float musicVolume;
    [Space(10)]
    public float soundEffectsVolume;

    [Header("Gameplay")]
    [Space(10)]
    public float xSensitivity;
    [Space(10)]
    public float ySensitivity;
}
public class Settings : MonoBehaviour
{
    public string saveFile;
    public SettingsData settingsData = new SettingsData();
    public PlayerController playerController;
    public PlayerCamera playerCamera;

    [BoxGroup("Fullscreen")]
    public UIValue fullscreenValue;
    
    [BoxGroup("Resolution")]
    public UIValue resolutionValue;
    [BoxGroup("Resolution")]
    public Resolution[] resolutions;
    [BoxGroup("Resolution")]
    public List<string> reso;

    [BoxGroup("Volume")]
    public AudioMixer audioMixer;
    [BoxGroup("Volume")]
    public Slider masterVolSlider;
    [BoxGroup("Volume")]
    public Slider musicVolSlider;
    [BoxGroup("Volume")]
    public Slider soundEffectsSlider;
    [BoxGroup("Volume")]
    public TMP_Text masterVolText;
    [BoxGroup("Volume")]
    public TMP_Text musicVolText;
    [BoxGroup("Volume")]
    public TMP_Text soundEffectsVolText;

    [BoxGroup("Sensitivity")]
    public Slider xSensitivitySlider;
    [BoxGroup("Sensitivity")]
    public Slider ySensitivitySlider;
    [BoxGroup("Sensitivity")]
    public TMP_Text xSensitivityText;
    [BoxGroup("Sensitivity")]
    public TMP_Text ySensitivityText;

        
    void Awake()
    {
        saveFile = PlayerPrefs.GetString("Most Recent Save");
    }

    // Start is called before the first frame update
    void Start()
    {
        
        List<string> options = new List<string>();
        resolutions = Screen.resolutions;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height + " @ " + resolutions[i].refreshRate + "hz";
            reso.Add( resolutions[i].width + " x " + resolutions[i].height + " @ " + resolutions[i].refreshRate + "hz");
            options.Add(option);
        }
        resolutionValue.maxValue = resolutions.Length;
        
        ReadFile();
    }

    // Update is called once per frame
    void Update()
    {
        saveFile = PlayerPrefs.GetString("Most Recent Save");
        switch (fullscreenValue.currentValue)
        {
            case 0: 
                fullscreenValue.valueText.text = "Exclusive Fullscreen";
                Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
                Screen.fullScreen = true;
            break;  

            case 1:             
                fullscreenValue.valueText.text = "Fullscreen Windowed";
                Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
                Screen.fullScreen = true;
            break; 

            case 2: 
                fullscreenValue.valueText.text = "Windowed";
                Screen.fullScreenMode = FullScreenMode.Windowed;
                Screen.fullScreen = false;
            break;
        }    
        #region audio
        audioMixer.SetFloat("MasterVolume", masterVolSlider.value);
        audioMixer.SetFloat("MusicVolume", musicVolSlider.value);
        audioMixer.SetFloat("EffectsVolume", soundEffectsSlider.value);

        masterVolText.text = "Master Volume (" + (masterVolSlider.value + 80) + "): ";
        musicVolText.text = "Music Volume (" + (musicVolSlider.value + 80) + "): ";
        soundEffectsVolText.text = "Voices Volume (" + (soundEffectsSlider.value + 80) + "): ";
        #endregion
        
        /*if(PlayerManager.instance){
            PlayerManager.instance.xSensitivity = xSensitivitySlider.value * 1.5f;
            PlayerManager.instance.ySensitivity = ySensitivitySlider.value * 1.5f;
        }*/

        xSensitivityText.text = "X Sensitivity (" + xSensitivitySlider.value + "):";
        ySensitivityText.text = "Y Sensitivity (" + ySensitivitySlider.value + "):";
        if(playerCamera){
            playerCamera.mouseSensitvityX = xSensitivitySlider.value / 10;
            playerCamera.mouseSensitvityY = ySensitivitySlider.value / 10;
        }
        SetResolution(resolutionValue.currentValue);
    }

    public void Back()
    {
        WriteFile();
    }
     
    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        resolutionValue.valueText.text = resolution.ToString();
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void MatchSensitivity()
    {
        ySensitivitySlider.value = xSensitivitySlider.value;
    }

    [ContextMenu("Load")]
    public void ReadFile()
    {
        if (File.Exists(saveFile))
        {
            string fileContents = File.ReadAllText(saveFile);
            
            settingsData = JsonUtility.FromJson<SettingsData>(fileContents);
            
            

            masterVolSlider.value = settingsData.masterVolume;
            musicVolSlider.value = settingsData.musicVolume;
            soundEffectsSlider.value = settingsData.soundEffectsVolume;

            ySensitivitySlider.value = settingsData.ySensitivity;
            xSensitivitySlider.value = settingsData.xSensitivity;
        
        }
    }


    [ContextMenu("Save")]
    public void WriteFile()
    {
        settingsData.masterVolume = masterVolSlider.value;
        settingsData.musicVolume = musicVolSlider.value;
        settingsData.soundEffectsVolume = soundEffectsSlider.value;

        settingsData.xSensitivity = xSensitivitySlider.value;
        settingsData.ySensitivity = ySensitivitySlider.value;

        settingsData.fullscreen = fullscreenValue.currentValue;
        settingsData.resolutionIndex = resolutionValue.currentValue;

        saveFile = Application.persistentDataPath + "/" + "Settings" + ".json";
        Debug.Log(saveFile);
        PlayerPrefs.SetString("Most Recent Save", saveFile);
        string jsonString = JsonUtility.ToJson(settingsData, true);

        File.WriteAllText(saveFile, jsonString);
        
    }
}
