using UnityEngine;

public class Eyes
{
    public Rect position;
    public int offset;
    public int eyesMechIndex;
    public int eyesColorIndex;
    public GameObject eyesStyle;
    
    //private Vector2 _eyesColorButtonSize;
    private Texture[] _eyesColorTextures;
    private int _numberOfEyeColors;

    public Eyes()
    {
        offset = 0;
        eyesColorIndex = 0;
        eyesMechIndex = 1;
        _numberOfEyeColors = 5;

        _eyesColorTextures = new Texture[_numberOfEyeColors];

        position = new Rect(100, 80, 100, 50);

        //_eyesColorButtonSize = new Vector2((position.width - (offset * 2)) / _numberOfEyeColors, (position.height - (offset * 2)) / 2);
    }

    public void Display()
    {
        GUI.BeginGroup(position);
        //EyesColorButtons();
        GUI.EndGroup();
    }

    public void LoadInicialEyesMale()
    {
        if (_eyesColorTextures[0] == null)
            LoadEyesColorTextures();

        LoadEyesMeshMale();
    }

    public void LoadInicialEyesFemale()
    {
        if (_eyesColorTextures[0] == null)
            LoadEyesColorTextures();

        LoadEyesMeshFemale();
    }

    public void NextEyesColorButtons()
    {
        if (_eyesColorTextures[0] == null)
            LoadEyesColorTextures();

        if (eyesColorIndex > _eyesColorTextures.Length)
            eyesColorIndex = _eyesColorTextures.Length;

        eyesColorIndex ++;
        eyesStyle.GetComponent<Renderer>().material.mainTexture = _eyesColorTextures[eyesColorIndex];
    }

    public void PreviusEyesColorButtons()
    {
        if (_eyesColorTextures[0] == null)
            LoadEyesColorTextures();

        eyesColorIndex--;

        if(eyesColorIndex < 0)
            eyesColorIndex = 0;

        eyesStyle.GetComponent<Renderer>().material.mainTexture = _eyesColorTextures[eyesColorIndex];
    }

    private void LoadEyesColorTextures()
    {
        string path = "Character/Eyes_Texture/";

        for (int cnt = 0; cnt < _eyesColorTextures.Length; cnt++)
        {
            _eyesColorTextures[cnt] = Resources.Load(path + ((EyesColorNames)cnt).ToString()) as Texture;
        }
    }

    private void LoadEyesMeshMale()
    {
        string path = "Character/Male/Eyes/";
        GameObject anchor = PlayerModelCustomization.CharacterMesh.GetComponent<PlayerCharacter>().EyesAnchor;

        if (anchor.transform.childCount > 0)
            Object.Destroy(anchor.transform.GetChild(0).gameObject);

        eyesStyle = Object.Instantiate(Resources.Load(path + "Eyes" + " " + eyesMechIndex)) as GameObject;
        eyesStyle.GetComponent<Renderer>().material.mainTexture = _eyesColorTextures[eyesColorIndex];
        eyesStyle.transform.parent = PlayerModelCustomization.CharacterMesh.GetComponent<PlayerCharacter>().EyesAnchor.transform;
        eyesStyle.transform.localPosition = Vector3.zero;
        eyesStyle.transform.rotation = new Quaternion(0, 0, 0, 0);
    }

    private void LoadEyesMeshFemale()
    {
        string path = "Character/Female/Eyes/";
        GameObject anchor = PlayerModelCustomization.CharacterMesh.GetComponent<PlayerCharacter>().EyesAnchor;

        if (anchor.transform.childCount > 0)
            Object.Destroy(anchor.transform.GetChild(0).gameObject);

        eyesStyle = Object.Instantiate(Resources.Load(path + "Eyes" + " " + eyesMechIndex)) as GameObject;
        eyesStyle.GetComponent<Renderer>().material.mainTexture = _eyesColorTextures[eyesColorIndex];
        eyesStyle.transform.parent = PlayerModelCustomization.CharacterMesh.GetComponent<PlayerCharacter>().EyesAnchor.transform;
        eyesStyle.transform.localPosition = Vector3.zero;
        eyesStyle.transform.rotation = new Quaternion(0, 0, 0, 0);
    }
}

public enum EyesColorNames
{
    blue = 0,
    gold = 1,
    green = 2,
    purple = 3,
    Red = 4
}
