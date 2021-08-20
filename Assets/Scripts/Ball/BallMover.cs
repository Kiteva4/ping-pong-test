using UnityEngine;

public class BallMover : MonoBehaviour
{
    [HideInInspector]
    public Vector3 Direction;
    
    [HideInInspector]
    public Rigidbody Rigidbody;
    
    [HideInInspector]
    public Transform _selfTransform;

    private float _ballPixelsSize;

    [Range(0.0f, 20.0f)]
    public float Speed = 0.5f;
    
    private Camera _mainCamera;

    private float invertDirectionDelay = 0.2f;
    private float invertDirectionTimer = -1;

    
    private void Awake()
    {
        _selfTransform = transform;
        _mainCamera = Camera.main;
        
        Rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        var screenPos = _mainCamera.WorldToViewportPoint(_selfTransform.position);
        Reflect(screenPos);
    }
    
    private void Reflect(Vector3 screenPos)
    {
        invertDirectionTimer -= Time.deltaTime;
        
        if (invertDirectionTimer >= 0) 
            return;
        
        if (screenPos.x - _ballPixelsSize <= 0)
        {
            Direction = Vector3.Reflect(Direction, Vector3.right);
            invertDirectionTimer = invertDirectionDelay;
        }
        else if (screenPos.x + _ballPixelsSize >= 1)
        {
            Direction = Vector3.Reflect(Direction, Vector3.left);
            invertDirectionTimer = invertDirectionDelay;
        }
    }

    private void FixedUpdate()
    {
        Rigidbody.MovePosition(_selfTransform.transform.position + 
                               Direction.normalized * Speed * Time.fixedDeltaTime);
    }

    public void UpdatePixelSize() => _ballPixelsSize = _mainCamera.WorldToViewportPoint(Vector3.right * _selfTransform.localScale.x * 0.5f).x - 
                                                       _mainCamera.WorldToViewportPoint(Vector3.zero).x;
}
