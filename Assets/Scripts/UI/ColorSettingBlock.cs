using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class ColorSettingBlock : MonoBehaviour
{
    [SerializeField] private FileNamesConfig _fileNamesConfig; 
    [SerializeField] private ColorChannelPicker redChannelPicker;
    [SerializeField] private ColorChannelPicker greenChannelPicker;
    [SerializeField] private ColorChannelPicker blueChannelPicker;
    [SerializeField] private SaveData saveData;
    [SerializeField] private Image resultImage;
    
    private Color _color;

    private void Awake() => LoadData();

    private void OnEnable()
    {
        redChannelPicker.PickerValueUpdate += OnRedChannelValueUpdate;
        greenChannelPicker.PickerValueUpdate += OnGreenChannelValueUpdate;
        blueChannelPicker.PickerValueUpdate += OnBlueChannelValueUpdate;
    }

    private void OnDisable()
    {
        redChannelPicker.PickerValueUpdate -= OnRedChannelValueUpdate;
        greenChannelPicker.PickerValueUpdate -= OnGreenChannelValueUpdate;
        blueChannelPicker.PickerValueUpdate -= OnBlueChannelValueUpdate;
    }
    
    private void OnRedChannelValueUpdate(float r)
    {
        _color.r = r;
        resultImage.color = _color;
    }

    private void OnGreenChannelValueUpdate(float g)
    {
        _color.g = g;
        resultImage.color = _color;
    }

    private void OnBlueChannelValueUpdate(float b)
    {
        _color.b = b;
        resultImage.color = _color;
    }

    public void LoadData()
    {
        var path = Path.Combine(Application.persistentDataPath, _fileNamesConfig.saveFileName);
        
        if (File.Exists(path))
        {
            var jsonString = File.ReadAllText (path);
            var saveDataWrapper = JsonUtility.FromJson<SaveDataWrapper>(jsonString);

            redChannelPicker.Value = saveDataWrapper.Color.r;
            greenChannelPicker.Value = saveDataWrapper.Color.g;
            blueChannelPicker.Value = saveDataWrapper.Color.b;
            _color = saveDataWrapper.Color;
            _color.a = 1.0f;
            resultImage.color = _color;
        }
    }
    
    public void SaveData()
    {
        var path = Path.Combine(Application.persistentDataPath, _fileNamesConfig.saveFileName);

        if (File.Exists(path))
        {
            Debug.Log($"<color=green>File exist</color>");
            saveData.saveDataWrapper.Color = _color;
            
            var jsonString = File.ReadAllText (path);
            var saveDataWrapper = JsonUtility.FromJson<SaveDataWrapper>(jsonString);
            saveDataWrapper.Color = _color;
            File.WriteAllText(path, JsonUtility.ToJson(saveDataWrapper, true));
        }
        else
        {
            Debug.Log($"<color=red>File NOT exist</color>");
            saveData.saveDataWrapper.Color = _color;
            File.WriteAllText(path, JsonUtility.ToJson(saveData.saveDataWrapper, true));
        }
    }
}
