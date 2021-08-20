using UnityEngine;

public class Menu : MonoBehaviour
{
    public MenuType type;
    public void OpenMenu() => gameObject.SetActive(true);
    public void CloseMenu() => gameObject.SetActive(false);
}