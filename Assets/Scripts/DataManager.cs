using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DataManager : MonoBehaviour
{   
    public string playerName;
    public string highScoreName;
    public int highScore = 0;

    public static DataManager Instance;

    void Awake()
    {
        if (Instance == null) {
            Instance = this;
            Load();
            DontDestroyOnLoad(gameObject);
        }
    }

    [System.Serializable]
    class SaveData
    {
        public string highScoreName;
        public int highScore;
    }

    public void Save() {
        string path = Application.persistentDataPath + "/savefile.json";

        SaveData data = new SaveData();
        data.highScore = highScore;
        data.highScoreName = highScoreName;

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(path, json);
    }

    public void Load() {
        string path = Application.persistentDataPath + "/savefile.json";

        if (File.Exists(path)) {
            string json = File.ReadAllText(path);

            SaveData data = JsonUtility.FromJson<SaveData>(json); 
            highScore = data.highScore;
            highScoreName = data.highScoreName;
        }
    }
}
