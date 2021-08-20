using System;
using Photon.Pun;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public static event Action BallBounced;
    
    [SerializeField]
    private Renderer renderer;
    private MaterialPropertyBlock _propertyBlock;
    
    private BallMover _ballMover;
    private PhotonView _photonView;
    
    [Range(0.05f, 2.0f)]
    public float affectStrength = 0.2f;
    
    private void Awake()
    {
        _photonView = GetComponent<PhotonView>();
        _ballMover = GetComponent<BallMover>();
        _propertyBlock = new MaterialPropertyBlock();
    }

    private void OnEnable() => SaveDataLoader.SaveDataLoaded += OnSaveDataLoaded;
    private void OnDisable() => SaveDataLoader.SaveDataLoaded -= OnSaveDataLoaded;

    private void Start()
    {
        if (!_photonView.IsMine)
        {
            Destroy(_ballMover.Rigidbody);
            Destroy(_ballMover);
            Destroy(this);
        }
    }
    
    public void Restart(Vector3 direction, float scale, float speed)
    {
        _ballMover.Direction = direction;
        _ballMover._selfTransform.position = Vector3.zero;
        _ballMover._selfTransform.localScale = scale * Vector3.one;
        _ballMover.Speed = speed;
        _ballMover.UpdatePixelSize();
    }
    
    private void OnSaveDataLoaded(SaveData saveData) => ChangeColor(saveData.saveDataWrapper.Color);

    private void ReflectDirectionAndAppendPlatformMove(float platformVelocityX, Vector3 reflectNormal)
    {
        affectStrength = 0.2f;
        var dirAfterPlatformAffect = (_ballMover.Direction + affectStrength * Vector3.right * platformVelocityX).normalized;
        
        if (Mathf.Abs(dirAfterPlatformAffect.y / dirAfterPlatformAffect.x) < 0.7f)
        {
            dirAfterPlatformAffect = new Vector3(Mathf.Sign(dirAfterPlatformAffect.y) * dirAfterPlatformAffect.x, dirAfterPlatformAffect.x, 0.0f).normalized;
        }
        
        _ballMover.Direction = Vector3.Reflect(dirAfterPlatformAffect, reflectNormal);
    }

    private void OnCollisionEnter(Collision other)
    {
        var platformMover = other.transform.GetComponentInParent<PlatformMover>();
        
        if (platformMover == null) 
            return;
        
        BallBounced?.Invoke();
        ReflectDirectionAndAppendPlatformMove(platformMover.VelocityX, platformMover.ReflectNormal);
    }
    
    private void ChangeColor(Color color)
    {
        _propertyBlock.SetColor("_Color", color);
        renderer.SetPropertyBlock(_propertyBlock);
    }
}
