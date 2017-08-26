using UnityEngine;
using System.Collections;

public class FloatingTextControler : MonoBehaviour
{
    private static FloatingText popupText;
    private static GameObject canvas;

    public static void Initialize()
    {
        canvas = GameObject.Find("Player UI");
        if (!popupText)
            popupText = Resources.Load<FloatingText>("Others/PoPUpTextParent");
    }

    public static void CreateFloatingText(string text, Transform location)
    {
        FloatingText instance = Instantiate(popupText);
        //Vector2 screenPosition = Camera.main.WorldToScreenPoint(new Vector2(location.position.x + Random.Range(-.2f, .2f), location.position.y + Random.Range(-.2f, .2f)));

        instance.transform.SetParent(canvas.transform, false);
        //instance.transform.position = screenPosition;
        instance.SetText(text);
    }
}
