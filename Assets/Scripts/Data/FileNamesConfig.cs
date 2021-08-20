using UnityEngine;

[CreateAssetMenu(fileName = "FileNamesConfig", menuName = "Settings/Create file names config", order = 0)]
public class FileNamesConfig : ScriptableObject
{
    public string saveFileName => "saveData.json";
}
