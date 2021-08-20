using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField] 
    private Renderer renderer;
    
    private Color _color;
    private MaterialPropertyBlock _propertyBlock;

    private void Awake() => _propertyBlock = new MaterialPropertyBlock();

    public void OnColorChanged(Color color)
    {
        _propertyBlock.SetColor("_Color", color);
        renderer.SetPropertyBlock(_propertyBlock);
    }
}
