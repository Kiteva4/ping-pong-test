using System.IO;
using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    [SerializeField] private FileNamesConfig _fileNamesConfig;
    [SerializeField] private TMP_Text bestScore;
    [SerializeField] private TMP_Text currentScore;
    
    private int _bestScore;
    private int BestScore
    {
        get => _bestScore;
        set
        {
            _bestScore = value;
            bestScore.text = _bestScore.ToString();
        }
    }
    private int _currentScore;
    private int CurrentScore
    {
        get => _currentScore;
        set
        {
            _currentScore = value;
            currentScore.text = _currentScore.ToString();
        }
    }

    private void Awake() => LoadBestScore();
    private void OnEnable() => Ball.BallBounced += IncrementScore;
    private void OnDisable() => Ball.BallBounced -= IncrementScore;
    private void IncrementScore() => ++CurrentScore;

    public void OnReset()
    {
        if (BestScore < CurrentScore)
        {
            BestScore = CurrentScore;
            SaveBestScore();
        }
            
        CurrentScore = 0;
    }

    private void SaveBestScore()
    {
        var path = Path.Combine(Application.persistentDataPath, _fileNamesConfig.saveFileName);
        
        if (File.Exists(path))
        {
            var jsonString = File.ReadAllText (path);
            var saveDataWrapper = JsonUtility.FromJson<SaveDataWrapper>(jsonString);
            saveDataWrapper.BestScore = BestScore;
            File.WriteAllText(path, JsonUtility.ToJson(saveDataWrapper, true));
        }
    }
    
    private void LoadBestScore()
    {
        var path = Path.Combine(Application.persistentDataPath, _fileNamesConfig.saveFileName);
        
        if (File.Exists(path))
        {
            var jsonString = File.ReadAllText (path);
            var saveDataWrapper = JsonUtility.FromJson<SaveDataWrapper>(jsonString);
            BestScore = saveDataWrapper.BestScore;
        }
    }
}
