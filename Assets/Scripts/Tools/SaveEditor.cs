using System.Collections;
#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using Sirenix.OdinInspector.Editor;
using Sirenix.OdinInspector;
using Sirenix.Utilities.Editor;

public class SaveEditor : OdinMenuEditorWindow
{
    [MenuItem("Tools/Save Editor")]
    public static void OpenWindow()
    {
        GetWindow<SaveEditor>().Show();
    }

    SaveFile saveFile;

    protected override OdinMenuTree BuildMenuTree()
    {
        var tree = new OdinMenuTree();
        
        saveFile = new SaveFile();
        tree.Add("Open Save File", saveFile);

        //tree.AddAllAssetsAtPath("Items", "Assets/ScriptableObjects/Items/Items", typeof(ItemObject));

        return tree;
    }

    public class SaveFile
    {
        public ItemDatabase itemDatabase;
        public string saveFileLocation;
        SettingsData settingsData;
        bool settings;
        string preSave;
        string saveFile;
        
        [BoxGroup("Settings")]
        [BoxGroup("Settings/Graphics")]
        [ShowIf("settings", true)] public int fullscreen;
        [BoxGroup("Settings/Graphics")]
        [ShowIf("settings", true)] public int resolutionIndex;

        [BoxGroup("Settings/Audio")]
        [Range(-80, 20)]
        [ShowIf("settings", true)] public float masterVolume;
        [BoxGroup("Settings/Audio")]
        [Range(-80, 20)]
        [ShowIf("settings", true)] public float musicVolume;
        [BoxGroup("Settings/Audio")]
        [Range(-80, 20)]
        [ShowIf("settings", true)] public float soundEffectVolume;

        [BoxGroup("Settings/Sensitivity")]
        [Range(0, 100)]
        [ShowIf("settings", true)] public float xSensitivity;
        [BoxGroup("Settings/Sensitivity")]
        [Range(0, 100)]
        [ShowIf("settings", true)] public float ySensitivity;
        

        public SaveFile()
        {
            //EOA C:\Users\Kenor\AppData\LocalLow\Rain Gaymes\BattleBricks\Settings.json
            saveFileLocation = PlayerPrefs.GetString("Most Recent Save");
            itemDatabase = AssetDatabase.LoadAssetAtPath("Assets/ScriptableObjects/Items/Item Database.asset", typeof(ItemDatabase)) as ItemDatabase;
        }

        [Button("Load File")]
        public void LoadFile()
        {            
            if(saveFileLocation.Contains("Settings.json")){
                settings = true;
                string fileContents = File.ReadAllText(saveFileLocation);
                settingsData = JsonUtility.FromJson<SettingsData>(fileContents);

                fullscreen = settingsData.fullscreen;
                resolutionIndex = settingsData.resolutionIndex;

                masterVolume = settingsData.masterVolume;
                musicVolume = settingsData.musicVolume;
                soundEffectVolume = settingsData.soundEffectsVolume;

                xSensitivity = settingsData.xSensitivity;
                ySensitivity = settingsData.ySensitivity;

                preSave = saveFileLocation;   
            }
        }
        [Button("Save File")]
        public void SaveFiles()
        {            
            settingsData.masterVolume = masterVolume;
            settingsData.musicVolume = musicVolume;
            settingsData.soundEffectsVolume = soundEffectVolume;

            settingsData.xSensitivity = xSensitivity;
            settingsData.ySensitivity = ySensitivity;

            settingsData.fullscreen = fullscreen;
            settingsData.resolutionIndex = resolutionIndex;

            saveFile = saveFileLocation;
            Debug.Log(saveFile);
            PlayerPrefs.SetString("Most Recent Save", saveFile);
            string jsonString = JsonUtility.ToJson(settingsData, true);

            File.WriteAllText(saveFile, jsonString);
        }
    }

}


#endif 