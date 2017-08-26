using UnityEngine;

public class PlayerCharacter : BaseCharacter
{
    #region Player Instance
    [System.NonSerialized]
    public GameObject wm_R;
    [System.NonSerialized]
    public GameObject wm_L;
    [System.NonSerialized]
    public GameObject head;
    [System.NonSerialized]
    public GameObject CH_H;
    [System.NonSerialized]
    public GameObject LG_H;
    [System.NonSerialized]
    public GameObject BT_H;
    [System.NonSerialized]
    public GameObject HH_H;
    public GameObject DrwHolder_L;
    public GameObject DrwHolder_R;
    public GameObject DrwHolder_BackL;
    public GameObject DrwHolder_BackR;

    public GameObject meshToHide1;
    public GameObject meshToHide2;
    public GameObject meshToHide3;
    public Player_DriverSystem DriverSysten;
    public static GameObject StartPos;
    public static int _gender;
    public static string _name = "";
    public static int lvl;
    private static GameObject go;
    private static PlayerCharacter _instance = null;
    private float _minDmg;
    private float _maxDmg;
    public int curDmg;
    public int _charID;
    #endregion

    public static PlayerCharacter Instance
    {
        get
        {
            if (_instance == null)
            {
                _gender = PlayerCononectMySql._gender;
                _name = PlayerCononectMySql._charName;
                lvl = PlayerCononectMySql._charLvl;

                if (_gender == 0)
                {
                    go = Instantiate(Resources.Load(GameSettings2.MALE_MODEL_PATH + GameSettings2.maleModels[0]),
                                                               StartPos.transform.position,
                                                               Quaternion.identity) as GameObject;

                    _instance = go.GetComponent<PlayerCharacter>();

                    PlayerCharacter temp = go.GetComponent<PlayerCharacter>();
                    if (temp == null)
                        Debug.LogError("Player Prefab does not containg a PC script");

                    LoadMaleCharacter();
                }
                else
                {
                    go = Instantiate(Resources.Load(GameSettings2.FEMALE_MODEL_PATH + GameSettings2.femaleModels[0]),
                                                               StartPos.transform.position,
                                                               Quaternion.identity) as GameObject;

                    _instance = go.GetComponent<PlayerCharacter>();

                    PlayerCharacter temp = go.GetComponent<PlayerCharacter>();
                    if (temp == null)
                        Debug.LogError("Player Prefab does not containg a PC script");

                    LoadFemaleCharacter();
                }

                go.name = "PC";
                go.tag = "Player";
            }
            return _instance;
        }
    }

    #region Unity Functions
    public new void Awake()
    {
        base.Awake();
    }

    public void Start()
    {
        wm_R = Weapon_Holder_R;
        wm_L = Weapon_Holder_L;
        CH_H = Chest_Holder;
        LG_H = Leggs_Holder;
        BT_H = Boots_Holder;
        HH_H = Hands_Holder;
        head = HairAnchor;
    }
    void Update()
    {
        CeckItensFuncions();
    }
    #endregion

    #region Loading Setting
    public static void LoadMaleCharacter()
    {
        LoadMaleHairMesh();
        LoadHair();
        LoadHairColor();
        LoadMaleEyesMesh();
        LoadEyesColor();
    }

    public static void LoadFemaleCharacter()
    {
        LoadFemaleHairMesh();
        LoadHair();
        LoadHairColor();
        LoadFemaleEyesMesh();
        LoadEyesColor();
    }

    public static void LoadHair()
    {
        //LoadSkinColor();
    }

    public static void LoadHairColor()
    {
        Texture temp = Resources.Load(GameSettings2.HAIR_TEXURE_PATH + ((HairColorNames)PlayerCononectMySql._hairColor).ToString()) as Texture;
        _instance.HairAnchor.GetComponentInChildren<Renderer>().material.mainTexture = temp;
    }

    public static void LoadEyesColor()
    {
        Texture temp = Resources.Load(GameSettings2.EYES_TEXURE_PATH + ((EyesColorNames)PlayerCononectMySql._eyesColor).ToString()) as Texture;
        _instance.EyesAnchor.GetComponentInChildren<Renderer>().material.mainTexture = temp;
    }

    public static void LoadMaleHairMesh()
    {
        GameObject hairStyle;
        int hairMechIndex = PlayerCononectMySql._hairMesh;

        hairStyle = Instantiate(Resources.Load(GameSettings2.MALE_HAIR_PATH + "Hair" + " " + hairMechIndex),
                    _instance.HairAnchor.transform.position,
                    _instance.HairAnchor.transform.rotation) as GameObject;

        hairStyle.transform.parent = _instance.HairAnchor.transform;
        hairStyle.transform.localPosition = Vector3.zero;
        hairStyle.transform.rotation = new Quaternion(0, 0, 0, 0);
    }

    public static void LoadMaleEyesMesh()
    {
        GameObject eyesStyle;
        int eyesMechIndex = PlayerCononectMySql._eyesMesh;

        eyesStyle = Instantiate(Resources.Load(GameSettings2.MALE_EYES_PATH + "Eyes" + " " + eyesMechIndex),
                    _instance.EyesAnchor.transform.position,
                    _instance.EyesAnchor.transform.rotation) as GameObject;

        eyesStyle.transform.parent = _instance.EyesAnchor.transform;
        eyesStyle.transform.localPosition = Vector3.zero;
        eyesStyle.transform.rotation = new Quaternion(0, 0, 0, 0);
    }

    public static void LoadFemaleHairMesh()
    {
        GameObject hairStyle;
        int hairMechIndex = PlayerCononectMySql._hairMesh;

        hairStyle = Instantiate(Resources.Load(GameSettings2.FEMALE_HAIR_PATH + "Hair" + " " + hairMechIndex),
                    _instance.HairAnchor.transform.position,
                    _instance.HairAnchor.transform.rotation) as GameObject;

        hairStyle.transform.parent = _instance.HairAnchor.transform;
        hairStyle.transform.localPosition = Vector3.zero;
        hairStyle.transform.rotation = new Quaternion(0, 0, 0, 0);
    }

    public static void LoadFemaleEyesMesh()
    {
        GameObject eyesStyle;
        int eyesMechIndex = PlayerCononectMySql._eyesMesh;

        eyesStyle = Instantiate(Resources.Load(GameSettings2.FEMALE_EYES_PATH + "Eyes" + " " + eyesMechIndex),
                    _instance.EyesAnchor.transform.position,
                    _instance.EyesAnchor.transform.rotation) as GameObject;

        eyesStyle.transform.parent = _instance.EyesAnchor.transform;
        eyesStyle.transform.localPosition = Vector3.zero;
        eyesStyle.transform.rotation = new Quaternion(0, 0, 0, 0);
    }


    public static void LoadSkinColor() { }
    #endregion

    #region Itens
    CostumeDriver_Chest controlerChest;
    CostumeDriver_legs controlerLegs;
    CostumeDriver_Shoes controlerBots;

    public void CeckItensFuncions()
    {
        //Chest Item Driven System
        #region Chest
        if (CH_H.transform.childCount != 0)
        {
            controlerChest = CH_H.GetComponentInChildren<CostumeDriver_Chest>();

            controlerChest.JNT_Driver_OBJ = this.DriverSysten.JNT_Driver_OBJ_Chest;
            controlerChest.JNT_Driver_OBJ_SH_R = this.DriverSysten.JNT_Driver_OBJ_SH_R;
            controlerChest.JNT_Driver_OBJ_SH_L = this.DriverSysten.JNT_Driver_OBJ_SH_L;
            controlerChest.JNT_Driver_OBJ_AR_R = this.DriverSysten.JNT_Driver_OBJ_AR_R;
            controlerChest.JNT_Driver_OBJ_AR_L = this.DriverSysten.JNT_Driver_OBJ_AR_L;
            
            if (controlerChest.JNT_Driven_OBJ_ER_Ra != null)
                controlerChest.JNT_Driver_OBJ_ER_Ra = this.DriverSysten.JNT_Driver_OBJ_ER_Ra;

            if (controlerChest.JNT_Driven_OBJ_ER_La != null)
                controlerChest.JNT_Driver_OBJ_ER_La = this.DriverSysten.JNT_Driver_OBJ_ER_La;

            if (controlerChest.JNT_Driven_OBJ_ER_Rb != null)
                controlerChest.JNT_Driver_OBJ_ER_Rb = this.DriverSysten.JNT_Driver_OBJ_ER_Rb;

            if (controlerChest.JNT_Driven_OBJ_ER_Lb != null)
                controlerChest.JNT_Driver_OBJ_ER_Lb = this.DriverSysten.JNT_Driver_OBJ_ER_Lb;

            controlerChest.JNT_Driven_OBJ.transform.parent = this.DriverSysten.JNT_Driver_OBJ_Chest.transform.parent;
            controlerChest.JNT_Driven_OBJ_SH_R.transform.parent = this.DriverSysten.JNT_Driver_OBJ_SH_R.transform.parent;
            controlerChest.JNT_Driven_OBJ_SH_L.transform.parent = this.DriverSysten.JNT_Driver_OBJ_SH_L.transform.parent;
            controlerChest.JNT_Driven_OBJ_AR_R.transform.parent = this.DriverSysten.JNT_Driver_OBJ_AR_R.transform.parent;
            controlerChest.JNT_Driven_OBJ_AR_L.transform.parent = this.DriverSysten.JNT_Driver_OBJ_AR_L.transform.parent;

            if (controlerChest.JNT_Driven_OBJ_ER_Ra != null)
                controlerChest.JNT_Driver_OBJ_ER_Ra.transform.parent = this.DriverSysten.JNT_Driver_OBJ_ER_Ra.transform.parent;

            if (controlerChest.JNT_Driven_OBJ_ER_La != null)
                controlerChest.JNT_Driver_OBJ_ER_La.transform.parent = this.DriverSysten.JNT_Driver_OBJ_ER_La.transform.parent;

            if (controlerChest.JNT_Driven_OBJ_ER_Rb != null)
                controlerChest.JNT_Driver_OBJ_ER_Rb.transform.parent = this.DriverSysten.JNT_Driver_OBJ_ER_Rb.transform.parent;

            if (controlerChest.JNT_Driven_OBJ_ER_Lb != null)
                controlerChest.JNT_Driver_OBJ_ER_Lb.transform.parent = this.DriverSysten.JNT_Driver_OBJ_ER_Lb.transform.parent;

        }
        #endregion
        //Hands Item Driven System(TO DO)

        //Legs Item Driven System
        #region Legs
        if (this.LG_H.transform.childCount != 0)
        {
            controlerLegs = LG_H.GetComponentInChildren<CostumeDriver_legs>();

            meshToHide1.SetActive(false);
            controlerLegs.JNT_Driver_OBJ = this.DriverSysten.JNT_Driver_OBJ_Legs;
            controlerLegs.JNT_Driver_OBJ_LG_R = this.DriverSysten.JNT_Driver_OBJ_LG_R;
            controlerLegs.JNT_Driver_OBJ_LG_L = this.DriverSysten.JNT_Driver_OBJ_LG_L;

            controlerLegs.JNT_Driven_OBJ.transform.parent = DriverSysten.JNT_Driver_OBJ_Legs.transform.parent;
            controlerLegs.JNT_Driven_OBJ_LG_R.transform.parent = DriverSysten.JNT_Driver_OBJ_LG_R.transform.parent;
            controlerLegs.JNT_Driven_OBJ_LG_L.transform.parent = DriverSysten.JNT_Driver_OBJ_LG_L.transform.parent;
        }
        else
        {
            meshToHide1.SetActive(true);
        }
        #endregion

        //Boots Item Driven System
        #region Boots
        if (BT_H.transform.childCount != 0)
        {
            controlerBots = BT_H.GetComponentInChildren<CostumeDriver_Shoes>();

            meshToHide2.SetActive(controlerBots.hideLeg);
            meshToHide3.SetActive(false);

            if (controlerBots.JNT_Driven_OBJ_LG_R != null)
                controlerBots.JNT_Driver_OBJ_LG_R = DriverSysten.JNT_Driver_OBJ_LG_R;

            if (controlerBots.JNT_Driven_OBJ_LG_L != null)
                controlerBots.JNT_Driver_OBJ_LG_L = DriverSysten.JNT_Driver_OBJ_LG_L;

            if (controlerBots.JNT_Driven_OBJ_Kne_aR != null)
                controlerBots.JNT_Driver_OBJ_Kne_aR = DriverSysten.JNT_Driver_OBJ_Kne_aR;

            if (controlerBots.JNT_Driven_OBJ_Kne_aL != null)
                controlerBots.JNT_Driver_OBJ_Kne_aL = DriverSysten.JNT_Driver_OBJ_Kne_aL;

            controlerBots.JNT_Driver_OBJ = this.DriverSysten.JNT_Driver_OBJ_Boots;
            controlerBots.JNT_Driver_OBJ_Kne_bR = this.DriverSysten.JNT_Driver_OBJ_Kne_bR;
            controlerBots.JNT_Driver_OBJ_Kne_bL = this.DriverSysten.JNT_Driver_OBJ_Kne_bL;
            controlerBots.JNT_Driver_OBJ_Foot_R = this.DriverSysten.JNT_Driver_OBJ_Foot_R;
            controlerBots.JNT_Driver_OBJ_Foot_L = this.DriverSysten.JNT_Driver_OBJ_Foot_L;
            controlerBots.JNT_Driver_OBJ_Ball_R = this.DriverSysten.JNT_Driver_OBJ_Ball_R;
            controlerBots.JNT_Driver_OBJ_Ball_L = this.DriverSysten.JNT_Driver_OBJ_Ball_L;

            if (controlerBots.JNT_Driver_OBJ_LG_R != null)
                controlerBots.JNT_Driven_OBJ_LG_R.transform.parent = DriverSysten.JNT_Driver_OBJ_LG_R.transform.parent;

            if (controlerBots.JNT_Driver_OBJ_LG_L != null)
                controlerBots.JNT_Driven_OBJ_LG_L.transform.parent = DriverSysten.JNT_Driver_OBJ_LG_L.transform.parent;

            if (controlerBots.JNT_Driver_OBJ_Kne_aR != null)
                controlerBots.JNT_Driven_OBJ_Kne_aR.transform.parent = DriverSysten.JNT_Driver_OBJ_Kne_aR.transform.parent;

            if (controlerBots.JNT_Driver_OBJ_Kne_aL != null)
                controlerBots.JNT_Driven_OBJ_Kne_aL.transform.parent = DriverSysten.JNT_Driver_OBJ_Kne_aL.transform.parent;

            controlerBots.JNT_Driven_OBJ.transform.parent = this.DriverSysten.JNT_Driver_OBJ_Boots.transform.parent;
            controlerBots.JNT_Driven_OBJ_Kne_bR.transform.parent = this.DriverSysten.JNT_Driver_OBJ_Kne_bR.transform.parent;
            controlerBots.JNT_Driven_OBJ_Kne_bL.transform.parent = this.DriverSysten.JNT_Driver_OBJ_Kne_bL.transform.parent;
            controlerBots.JNT_Driven_OBJ_Foot_R.transform.parent = this.DriverSysten.JNT_Driver_OBJ_Foot_R.transform.parent;
            controlerBots.JNT_Driven_OBJ_Foot_L.transform.parent = this.DriverSysten.JNT_Driver_OBJ_Foot_L.transform.parent;
            controlerBots.JNT_Driven_OBJ_Ball_R.transform.parent = this.DriverSysten.JNT_Driver_OBJ_Ball_R.transform.parent;
            controlerBots.JNT_Driven_OBJ_Ball_L.transform.parent = this.DriverSysten.JNT_Driver_OBJ_Ball_L.transform.parent;
        }
        else
        {
            meshToHide2.SetActive(true);
            meshToHide3.SetActive(true);
        }
        #endregion
    }
    #endregion

    public void CalculeteDamage()
    {
        _minDmg = 5f;
        _maxDmg = 20f;
        curDmg = ((int)Random.Range(_minDmg, _maxDmg));
    }
}