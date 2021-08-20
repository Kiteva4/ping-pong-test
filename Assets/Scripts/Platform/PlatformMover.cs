using Photon.Pun;
using UnityEngine;

public class PlatformMover : MonoBehaviour
{
    [HideInInspector]
    public Vector3 ReflectNormal;
    public float VelocityX;
    
    private PhotonView _photonView;
    private Camera _mainCamera;
    private Transform _selfTransform;
    private float prevXPos;
    
    private void Awake()
    {
        _mainCamera = Camera.main;
        _selfTransform = GetComponent<Transform>();
        _photonView = GetComponent<PhotonView>();

        ReflectNormal = _mainCamera.WorldToViewportPoint(_selfTransform.position).y > 0.5f ? Vector3.down : Vector3.up;
    }

    private void Update()
    {

        if (_photonView.IsMine && Input.GetMouseButton(0))
        {
            _selfTransform.position = Vector3.up * _selfTransform.position.y +
                                      Vector3.right * _mainCamera.ScreenToWorldPoint(Input.mousePosition).x;

        }
        
        VelocityX = _selfTransform.position.x - prevXPos;
        VelocityX = Mathf.Clamp(VelocityX, -3.5f, 3.5f);
        prevXPos = _selfTransform.position.x;
    }
}
