using System;
using System.IO;
using UnityEngine;

public class SaveDataLoader : MonoBehaviour
{
    public static event Action<SaveData> SaveDataLoaded;
    
    [SerializeField] 
    private SaveData saveData;

    private string saveFileName => "saveData.json";
    
    private void Start()
    {
        var path = Path.Combine(Application.persistentDataPath, saveFileName);
       
        if (File.Exists(path))
        {
            var jsonString = File.ReadAllText (path);
            saveData.saveDataWrapper = JsonUtility.FromJson<SaveDataWrapper>(jsonString);
            SaveDataLoaded?.Invoke(saveData);
        }
        else
        {
            var data = JsonUtility.ToJson(saveData.saveDataWrapper, true);
            File.WriteAllText(path, data);
            SaveDataLoaded?.Invoke(saveData);
        }
    }
}
