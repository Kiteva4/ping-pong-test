using UnityEngine;
using Random = UnityEngine.Random;

public class GameRestarter : MonoBehaviour
{
    [SerializeField] 
    private Score score;
    
    [SerializeField] 
    private Transform ballTransform;
    
    [SerializeField] 
    private Ball ball;
    
    [SerializeField] 
    private float minRandomScale = 0.4f;
    [SerializeField] 
    private float maxRandomScale = 2.5f;
    
    [SerializeField] 
    private float minRandomSpeed = 5.0f;
    [SerializeField] 
    private float maxRandomSpeed = 15.0f;
    
    private Camera _mainCamera;

    private Vector3 screenPos;
    
    private void Awake() => _mainCamera = Camera.main;
    private void Start() => ball.Restart(RandomDirection(),
        Random.Range(minRandomScale, maxRandomScale), 
        Random.Range(minRandomSpeed, maxRandomSpeed));
    
    private void Update()
    {    
        screenPos = _mainCamera.WorldToViewportPoint(ballTransform.position);
        
        if (screenPos.y > 1 || screenPos.y < 0)
        {
            ball.Restart(RandomDirection(),
                Random.Range(minRandomScale, maxRandomScale),
                Random.Range(minRandomSpeed, maxRandomSpeed));
            
            score.OnReset();
        }
    }
    
    private Vector3 RandomDirection() =>
        Quaternion.Euler(Vector3.forward * Random.Range(-45.0f, 45.0f)) * 
        (Vector3.up * (Random.value < 0.5f? 1 : -1));
}
