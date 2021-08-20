using UnityEngine;

[CreateAssetMenu(fileName = "SaveData", menuName = "Settings/Create save data asset", order = 0)]
public class SaveData : ScriptableObject
{
    public SaveDataWrapper saveDataWrapper;
}