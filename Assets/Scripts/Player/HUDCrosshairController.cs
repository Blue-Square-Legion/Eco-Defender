using UnityEngine;
using UnityEngine.UI;

public class HUDCrosshairController : MonoBehaviour
{

    Image crosshair;

    Sprite defaultSprite;
    Color defaultColor;

    public static HUDCrosshairController Instance;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }

        crosshair = GetComponent<Image>();
        defaultSprite = crosshair.sprite;
        defaultColor = crosshair.color;
    }

    public void SetSprite(Sprite sprite)
    {
        crosshair.sprite = sprite;
    }

    public void SetSprite(Sprite sprite, Color color)
    {
        crosshair.sprite = sprite;
        crosshair.color = color;
    }

    public void ResetSprite()
    {
        crosshair.sprite = defaultSprite;
        crosshair.color = defaultColor;
    }

}
