using UnityEngine;
public class Rotate_Player : MonoBehaviour
{
    public bool rotateClockwise = true;
    public Renderer rend;
    private bool _arrowPress = false;

    void Update()
    {
        if (_arrowPress)
            Messenger<bool>.Broadcast("RotatePlayerClockwise", rotateClockwise);
    }

    public void OnMouseEnter()
    {
        HighLigth(true);
        //Debug.Log("true");
    }

    public void OnMouseExit()
    {
        HighLigth(false);
        //Debug.Log("false");
        Color _color = Color.blue;
        rend.material.color = _color;
    }

    public void OnMouseDown()
    {
        _arrowPress = true;
        //Debug.Log("Down");        
    }

    public void OnMouseUp()
    {
        //Debug.Log("up");
        _arrowPress = false;
    }

    private void HighLigth(bool glow)
    {
        Color _color = Color.blue;

        if (glow)
            _color = Color.red;

        rend.material.color = _color;
    }
}
