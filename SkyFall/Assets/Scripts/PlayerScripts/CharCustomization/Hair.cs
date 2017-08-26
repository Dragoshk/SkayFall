using UnityEngine;

public class Hair
{
    public Rect position;
    public int offset;
    public int hairMechIndex;
    public int hairColorIndex;
    public GameObject hairStyle;

    //private Vector2 _hairStyleButtonSize;
    //private Vector2 _hairColorButtonSize;
    public Texture[] _hairColorTextures;
    private int _numberOfHairColors;
    private int _numberOfHairMesh;

    public Hair()
    {
        offset = 0;
        hairColorIndex = 0;
        hairMechIndex = 1;
        _numberOfHairColors = 5;
        _numberOfHairMesh = 3;

        _hairColorTextures = new Texture[_numberOfHairColors];

        position = new Rect(100, 30, 100, 50);

        //_hairColorButtonSize = new Vector2((position.width - (offset * 2)) / _numberOfHairColors, (position.height - (offset * 2)) / 2);
        //_hairStyleButtonSize = new Vector2((position.width - (offset * 2)) / 2, (position.height - (offset * 2)) / 2);        
    }

    public void DisplayMale()
    {
        GUI.BeginGroup(position);
        NextHairColorButtons();
        NextMaleHairStyle();
        MalePreviousHairStyle();
        GUI.EndGroup();
    }

    public void DisplayFemale()
    {
        GUI.BeginGroup(position);
        NextHairColorButtons();
        NextFemaleHairStyle();
        MalePreviousHairStyle();
        GUI.EndGroup();
    }

    public void LoadInicialHairMale()
    {
        if (_hairColorTextures[0] == null)
            LoadHairColorTextures();

        LoadHairMeshMale();
    }

    public void LoadInicialHairFemale()
    {
        if (_hairColorTextures[0] == null)
            LoadHairColorTextures();

        LoadHairMeshFemale();
    }

    public void NextHairColorButtons()
    {
        if (_hairColorTextures[0] == null)
            LoadHairColorTextures();

        hairColorIndex++;

        if (hairColorIndex > _hairColorTextures.Length)
            hairColorIndex = _hairColorTextures.Length;

        hairStyle.GetComponent<Renderer>().material.mainTexture = _hairColorTextures[hairColorIndex];
    }

    public void previusHairColorButtons()
    {
        if (_hairColorTextures[0] == null)
            LoadHairColorTextures();

        hairColorIndex--;

        if (hairColorIndex < 0)
            hairColorIndex = 0;

        hairStyle.GetComponent<Renderer>().material.mainTexture = _hairColorTextures[hairColorIndex];
    }

    public void MalePreviousHairStyle()
    {
        hairMechIndex--;

        if (hairMechIndex < 1)
            hairMechIndex = _numberOfHairMesh - 1;

        LoadHairMeshMale();
    }

    public void FemalePreviousHairStyle()
    {
        hairMechIndex--;

        if (hairMechIndex < 1)
            hairMechIndex = _numberOfHairMesh - 1;

        LoadHairMeshFemale();
    }

    public void NextMaleHairStyle()
    {
        hairMechIndex++;

        if (hairMechIndex > _numberOfHairMesh - 1)
            hairMechIndex = 1;

            
        LoadHairMeshMale();        
    }

    public void NextFemaleHairStyle()
    {
        hairMechIndex++;

        if (hairMechIndex > _numberOfHairMesh - 1)
            hairMechIndex = 1;
        LoadHairMeshFemale();
    }

    public void LoadHairColorTextures()
    {
        string path = "Character/Male/Hair/Textures/";

        for (int cnt = 0; cnt < _hairColorTextures.Length; cnt++)
        {
            _hairColorTextures[cnt] = Resources.Load(path + ((HairColorNames)cnt).ToString() ) as Texture;
        }
    }

    public void LoadHairMeshMale()
    {
        string path = "Character/Male/Hair/";
        GameObject anchor = PlayerModelCustomization.CharacterMesh.GetComponent<PlayerCharacter>().HairAnchor;

        if (anchor.transform.childCount > 0)
            Object.Destroy(anchor.transform.GetChild(0).gameObject);

        hairStyle = Object.Instantiate(Resources.Load(path + "Hair" + " " + hairMechIndex)) as GameObject;
        hairStyle.GetComponent<Renderer>().material.mainTexture = _hairColorTextures[hairColorIndex];
        hairStyle.transform.parent = PlayerModelCustomization.CharacterMesh.GetComponent<PlayerCharacter>().HairAnchor.transform;
        hairStyle.transform.localPosition = Vector3.zero;
        hairStyle.transform.rotation = new Quaternion(0, 0, 0, 0);
    }

    public void LoadHairMeshFemale()
    {
        //Debug.Log("Female");
        string path = "Character/Female/Hair/";
        GameObject anchor = PlayerModelCustomization.CharacterMesh.GetComponent<PlayerCharacter>().HairAnchor;

        if (anchor.transform.childCount > 0)
            Object.Destroy(anchor.transform.GetChild(0).gameObject);

        hairStyle = Object.Instantiate(Resources.Load(path + "Hair" + " " + hairMechIndex)) as GameObject;
        hairStyle.GetComponent<Renderer>().material.mainTexture = _hairColorTextures[hairColorIndex];
        hairStyle.transform.parent = PlayerModelCustomization.CharacterMesh.GetComponent<PlayerCharacter>().HairAnchor.transform;
        hairStyle.transform.localPosition = Vector3.zero;
        hairStyle.transform.rotation = new Quaternion(0, 0, 0, 0);
    }
}

public enum HairColorNames
{
    Black = 0,
    Black_Blue = 1,
    Blue = 2,
    Ligth_Blue = 3,
    Red = 4
}
