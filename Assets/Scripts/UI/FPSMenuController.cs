using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class FPSMenuController : MonoBehaviour
{
    public GameObject Menu;
    public UnityEvent OnMenuOpen, OnMenuClose;


    public void OnMenu()
    {
        print("MenuEvent");
        Menu.SetActive(!Menu.activeSelf);

        if (Menu.activeSelf)
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
            OnMenuOpen.Invoke();
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            OnMenuClose.Invoke();
        }
    }
}
