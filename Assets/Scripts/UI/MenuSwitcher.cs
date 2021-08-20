using UnityEngine;

public class MenuSwitcher : MonoBehaviour
{
    [SerializeField]
    private Menu[] menuItems;
    
    public void OpenMenu(MenuType type)
    {
        foreach (var menuItem in menuItems)
        {
            if (menuItem.type == type)
            {
                OpenMenu(menuItem);
            }
        }
    }

    public void OpenMenu(Menu menu)
    {
        CloseAllMenu();
        menu.OpenMenu();
    }

    private void CloseAllMenu()
    {
        foreach (var menuItem in menuItems)
            menuItem.CloseMenu();
    }
}