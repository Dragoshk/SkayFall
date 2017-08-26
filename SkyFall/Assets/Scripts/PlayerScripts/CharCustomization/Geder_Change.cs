using UnityEngine;
public class Geder_Change : MonoBehaviour
{
    public Renderer rend;

    public void OnMouseEnter()
    {
        HighLigth(true);
        //Debug.Log("true");
    }

    public void OnMouseExit()
    {
        Color _color = Color.blue;
        HighLigth(false);
        rend.material.color = _color;
    }

    public void OnMouseDown()
    {
        //Debug.Log("click");
        Messenger.Broadcast("ToggleGender");
    }

    private void HighLigth(bool glow)
    {
        Color _color = Color.blue;

        if (glow)
            _color = Color.red;

        rend.material.color = _color;
    }
}
