using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Collections;
using System.Collections.Generic;

public class PlayerModelCustomization : MonoBehaviour
{
    #region General Variables
    private const int STARTING_POINTS = 350;
    private const int MIN_STARTING_ATTRIBUTES_VALUE = 10;
    private const int STARTING_VALUE = 35;

    [Header("Proprietis")]
    public float rotSpeed = 100;
    public static GameObject CharacterMesh;
    public List<CharacterSlot> cs = new List<CharacterSlot>();
    public List<GameObject> slots = new List<GameObject>();
    public List<Text> StatsToAllowcate = new List<Text>();
    public List<Text> StatsGeneral = new List<Text>();

    [Header("UI Proprietis")]
    public List<GameObject> CharSelect = new List<GameObject>();
    public Text newName;
    public Text _name;
    public Text lvl;
    public Text pointsLeft;
    public GameObject mount;
    public GameObject CreatButton;
    public GameObject SelectPanel;
    public GameObject Creatpanel;
    public GameObject PlayerMenus;
    public GameObject OverallData;
    public GameObject StastGeneral;
    public GameObject StastToAllowcateInCreat;
    public GameObject StastToAllowcate;
    public GameObject AreaToGo;
    public GameObject ArenaPanell;
    public GameObject BgPanell;
    public DataWorker DW;
    public GameObject ChatPanel;
    public GameObject InventoryGrp;
    public GameObject ConfigurationsPanel;

    [System.NonSerialized]
    public int statStartingPos = 40;
    [System.NonSerialized]
    public int _butIndex;
    public bool changeItens = false;
    private int _pointsLeft;
    private int _allStuffID;
    #endregion

    #region Private Variables
    private Hair _hair = new Hair();
    private Eyes _eyes = new Eyes();
    private string _tempUser;
    private string _tempPassword;
    private int _index = 0;
    private int _gender = 0;
    private int _charCount;
    private int _charOwner;
    private int _charID;
    private int _charSlot;
    private int _slotSelect;
    private int _indexChar;
    private int _startLvl = 1;
    private int _currlvl;
    private int _charlvlup = 0;
    private int[] _status = new int[13];
    private bool _unsingMaleModel = true;
    private bool _rotateme = false;
    private bool _rotClockwise = true;
    private bool _characterCreated = false;
    private bool _needToAddPoints = false;
    #endregion

    public static Vector2 _scale = Vector2.one;
    public static int skinColor = 1;
    public string _charName;

    private int _myGender;
    private int _myHair;
    private int _myHairColor;
    private int _myEyesColor;

    #region Inventory
    [Header("Inventory")]
    public ItemDatabase database;
    public int Generalindex;
    public int GeneralGender;
    [System.NonSerialized]
    public GameObject item0;
    [System.NonSerialized]
    public GameObject item1;
    [System.NonSerialized]
    public GameObject item2;
    [System.NonSerialized]
    public GameObject item3;
    [System.NonSerialized]
    public GameObject item4;
    [System.NonSerialized]
    public GameObject item5;
    public int chest;
    public int hands;
    public int WeaponMain;
    public int Legs;
    public int Boots;
    public int WeaponOff;
    #endregion

    void Awake()
    {
        PlayerCononectMySql.ConectBd(PlayerCononectMySql._source);
    }

    void Start()
    {
        database = GameObject.FindGameObjectWithTag("ItemDatabase").GetComponent<ItemDatabase>();
        DW = GameObject.FindGameObjectWithTag("DataWorker").GetComponent<DataWorker>();

        if (GameSettings2.maleModels.Length < 1)
        {
            Debug.LogWarning("We have no male Models");
        }

        if (GameSettings2.femaleModels.Length < 1)
        {
            Debug.LogWarning("We have no female Models");
        }


        PlayerCononectMySql.InitateConection();

        _charCount = PlayerCononectMySql._charCount;
        _charOwner = PlayerCononectMySql._ownerChar;
        _charSlot = PlayerCononectMySql._charSlot;

        if (PlayerCononectMySql._charName != null)
        {
            _charName = PlayerCononectMySql._charName;
        }

        if (_charCount == 4)
            CreatButton.SetActive(false);

        if (_charCount > 0 && _charCount <= 4)
        {
            for (int i = 0; i <= _charCount - 1; i++)
            {
                PlayerCononectMySql.LoadCharacterData(i);
                _charID = PlayerCononectMySql._charID;
                _charName = PlayerCononectMySql._charName;
                _currlvl = PlayerCononectMySql._charLvl;
                _myGender = PlayerCononectMySql._gender;
                _myHair = PlayerCononectMySql._hairMesh;
                _myHairColor = PlayerCononectMySql._hairColor;
                _myEyesColor = PlayerCononectMySql._eyesColor;

                InstantiateAllCharacter(i, _charID, slots[i]);

                chest = PlayerCononectMySql._itensEquiped[1];
                hands = PlayerCononectMySql._itensEquiped[2];
                WeaponMain = PlayerCononectMySql._itensEquiped[4];
                Legs = PlayerCononectMySql._itensEquiped[3];
                Boots = PlayerCononectMySql._itensEquiped[0];
                WeaponOff = PlayerCononectMySql._itensEquiped[5];

                AddCharacterItens(_myGender, i, chest, hands, WeaponMain, Legs, Boots, WeaponOff);

                if (slots[i].transform.childCount != 0)
                {
                    //Char Base
                    CharSelect[i].gameObject.GetComponent<CharSelectBut>()._Name.text = "Name :" + _charName;
                    CharSelect[i].gameObject.GetComponent<CharSelectBut>().Level.text = "level :" + _currlvl;
                    CharSelect[i].gameObject.GetComponent<CharSelectBut>().charID = _charID;
                    CharSelect[i].gameObject.GetComponent<CharSelectBut>().Index = i;
                    CharSelect[i].gameObject.GetComponent<CharSelectBut>()._gender = _myGender;
                    CharSelect[i].gameObject.GetComponent<CharSelectBut>()._hair = _myHair;
                    CharSelect[i].gameObject.GetComponent<CharSelectBut>()._hairColor = _myHairColor;
                    CharSelect[i].gameObject.GetComponent<CharSelectBut>()._eyesColor = _myEyesColor;
                    CharSelect[i].gameObject.GetComponent<CharSelectBut>().needpoints = _needToAddPoints;

                    //Char Itens
                    CharSelect[i].gameObject.GetComponent<CharSelectBut>().chest = chest;
                    CharSelect[i].gameObject.GetComponent<CharSelectBut>().hands = hands;
                    CharSelect[i].gameObject.GetComponent<CharSelectBut>().WeaponMain = WeaponMain;
                    CharSelect[i].gameObject.GetComponent<CharSelectBut>().Legs = Legs;
                    CharSelect[i].gameObject.GetComponent<CharSelectBut>().Boots = Boots;
                    CharSelect[i].gameObject.GetComponent<CharSelectBut>().WeaponOff = WeaponOff;
                }
            }

            for (int i = 0; i <= 3; i++)
            {
                if (CharSelect[i].gameObject.GetComponent<CharSelectBut>().charID == 0)
                    CharSelect[i].gameObject.SetActive(false);
            }
        }

        PlayerCononectMySql._connection.Close();
        PlayerCononectMySql._connection.Dispose();
    }

    void Update()
    {
        if (_rotateme)
        {
            if (_rotClockwise)
                transform.Rotate(Vector3.up * Time.deltaTime * rotSpeed);
            else
                transform.Rotate(Vector3.down * Time.deltaTime * rotSpeed);

            _rotateme = false;
        }

        if (CharacterMesh == null)
            return;

        CharacterMesh.transform.localScale = new Vector3(_scale.x, _scale.y, _scale.x);
    }

    #region UI Functions
    public void CreatChar()
    {
        PlayerCononectMySql.ConectBd(PlayerCononectMySql._source);

        _pointsLeft = (STARTING_VALUE - MIN_STARTING_ATTRIBUTES_VALUE);
        InstantiateCharacterModel();
        SelectPanel.SetActive(false);
        Creatpanel.SetActive(true);
    }

    public void BackToSelectChar()
    {
        if (transform.childCount != 0)
            for (int cnt = 0; cnt < transform.childCount; cnt++)
                Destroy(transform.GetChild(cnt).gameObject);

        SelectPanel.SetActive(true);
        Creatpanel.SetActive(false);
        ConfigurationsPanel.SetActive(false);
    }

    public void CreatCharacter()
    {
        PlayerCononectMySql.ConectBd(PlayerCononectMySql._source);

        _charID = UnityEngine.Random.Range(1, 100);
        _charName = newName.text;
        pointsLeft.text = "Points Left :" + _pointsLeft.ToString();

        bool validi = PlayerCononectMySql.VerifyData(PlayerCononectMySql._connection, _charID, _charName);

        if (!validi)
        {
            _charSlot++;
            _charCount++;
            SaveCreatedNewCharacter();

            //destroi pre personagem criado
            if (transform.childCount != 0)
                for (int cnt = 0; cnt < transform.childCount; cnt++)
                    Destroy(transform.GetChild(cnt).gameObject);

            _characterCreated = true;

            SelectPanel.SetActive(true);
            Creatpanel.SetActive(false);
            StastToAllowcateInCreat.SetActive(false);

            //instancia o personagem na lista
            CharCreatInstance(_charSlot);
        }
        else
        {
            _charID = UnityEngine.Random.Range(1, 100);
            _charName = string.Empty;
        }
    }

    public void AddStatus()
    {
        StastToAllowcateInCreat.SetActive(true);
        Creatpanel.SetActive(false);

        //Vincula os status do personagem a seu respectivo dono
        for (int cnt = 0; cnt < Enum.GetValues(typeof(AtributeName)).Length; cnt++)
        {
            mount.GetComponentInChildren<PlayerCharacter>().GetPrimaryAttribute(cnt).BaseValue = STARTING_VALUE;
            StatsToAllowcate[cnt].text = mount.GetComponentInChildren<PlayerCharacter>().GetPrimaryAttribute(cnt).BaseValue.ToString();
            _status[cnt] = mount.GetComponentInChildren<PlayerCharacter>().GetPrimaryAttribute(cnt).BaseValue;
            _pointsLeft = (STARTING_VALUE - MIN_STARTING_ATTRIBUTES_VALUE);
        }
    }

    public void nextHairButton()
    {
        //seleciona o proxima malha do cabelo
        if (_unsingMaleModel)
        {
            _hair.NextMaleHairStyle();
        }
        else
            _hair.NextFemaleHairStyle();

    }

    public void previusHairButton()
    {
        //seleciona o anterior malha do cabelo
        if (_unsingMaleModel)
            _hair.MalePreviousHairStyle();
        else
            _hair.FemalePreviousHairStyle();
    }

    public void NextHairColor()
    {
        //seleciona o proxima cor do cabelo
        _hair.NextHairColorButtons();
    }

    public void PreviusHairColor()
    {
        //seleciona o anterior cor do cabelo
        _hair.previusHairColorButtons();
    }

    public void NextEyeColor()
    {
        //seleciona o proxima cor do olho
        _eyes.NextEyesColorButtons();
    }

    public void PreviusEyeColor()
    {
        //seleciona o anterior cor do olho
        _eyes.PreviusEyesColorButtons();
    }

    //Seleciona o Personagem
    public void SelectCharButton(CharSelectBut selct)
    {
        PlayerCononectMySql.ConectBd(PlayerCononectMySql._source);

        //Desativa menus especifico
        PlayerMenus.SetActive(true);
        OverallData.SetActive(true);
        StastGeneral.SetActive(false);
        GeneralStastValues(selct.Index);
        SelectPanel.SetActive(false);
        ChatPanel.SetActive(true);
        
        //localiza botão status e aferi um index de seleção
        GameObject.Find("Stast").GetComponent<StastButton>().Index = selct.Index;
        //localiza botão da arena e aferi um index de seleção
        GameObject.Find("Select Area to go").GetComponent<StastButton>().Index = selct.Index;
        //localiza botão da arena e aferi um ID ao personagem
        GameObject.Find("Select Area to go").GetComponent<StastButton>().ID = selct.charID;
        //Coloca index referentes(ta bem descritivo)
        _allStuffID = selct.Index;
        _name.text = selct._Name.text;
        lvl.text = selct.Level.text;
        _butIndex = selct.Index;
        Generalindex = selct.Index;
        GeneralGender = selct._gender;

        //passa valores ao Data Worker e nele q os dados do personagem são levados para outra cena
        DW._name = selct._Name.text;
        DW._gender = selct._gender;
        DW._id = selct.charID;
        DW._hair = selct._hair;
        DW._hairColor = selct._hairColor;
        DW._eyesColor = selct._eyesColor;

        //character itens on DW
        DW.chest = selct.chest;
        DW.hands = selct.hands;
        DW.WeaponMain = selct.WeaponMain;
        DW.Legs = selct.Legs;
        DW.Boots = selct.Boots;
        DW.WeaponOff = selct.WeaponOff;

        //Pegas os dados de status do personagem
        GetCharacterStast(selct.charID);

        //Potons do stats do personagem
        if (selct.needpoints)
        {
            FirstPointsToAdd(selct.Index);
        }
    }

    //Informações gerais do personagem
    public void OverallInfoButton()
    {
        InventoryGrp.SetActive(false);
        OverallData.SetActive(true);
        StastGeneral.SetActive(false);
        StastToAllowcateInCreat.SetActive(false);
        AreaToGo.SetActive(false);
        ConfigurationsPanel.SetActive(false);
        changeItens = false;
    }

    //Mostra os status do personagem
    public void StastButton(StastButton stats)
    {
        InventoryGrp.SetActive(false);
        OverallData.SetActive(true);
        StastGeneral.SetActive(true);
        StastToAllowcateInCreat.SetActive(false);
        AreaToGo.SetActive(false);
        GeneralStastValues(stats.Index);
        ConfigurationsPanel.SetActive(false);
        changeItens = false;
    }

    //Livro de spell(TO DO)
    public void SpellBookButton()
    {
        InventoryGrp.SetActive(false);
        OverallData.SetActive(false);
        StastGeneral.SetActive(false);
        StastToAllowcateInCreat.SetActive(false);
        AreaToGo.SetActive(false);
        ConfigurationsPanel.SetActive(false);
        changeItens = false;
    }

    //Livro de Quests(TO DO)
    public void QuestLogButton()
    {
        OverallData.SetActive(false);
        StastGeneral.SetActive(false);
        StastToAllowcateInCreat.SetActive(false);
        AreaToGo.SetActive(false);
        InventoryGrp.SetActive(false);
        ConfigurationsPanel.SetActive(false);
        changeItens = false;
    }

    //Armory do personagem
    public void InvetoryButton()
    {
        InventoryGrp.SetActive(true);
        OverallData.SetActive(false);
        StastGeneral.SetActive(false);
        StastToAllowcateInCreat.SetActive(false);
        AreaToGo.SetActive(false);
        ConfigurationsPanel.SetActive(false);

        changeItens = true;
        cs[0].chek = 1;
        cs[1].chek = 1;
        cs[2].chek = 1;
        cs[3].chek = 1;
        cs[4].chek = 1;
        cs[5].chek = 1;
    }

    //Seleção de Areas
    public void GoToArea(StastButton index)
    {
        InventoryGrp.SetActive(false);
        OverallData.SetActive(false);
        StastGeneral.SetActive(false);
        StastToAllowcateInCreat.SetActive(false);
        SelectPanel.SetActive(false);
        AreaToGo.SetActive(true);
        ConfigurationsPanel.SetActive(false);
        InventoryGrp.SetActive(false);
        changeItens = false;
    }

    public void BackButton()
    {
        OverallData.SetActive(false);
        StastGeneral.SetActive(false);
        StastToAllowcateInCreat.SetActive(false);
        PlayerMenus.SetActive(false);
        SelectPanel.SetActive(true);
        AreaToGo.SetActive(false);
        ArenaPanell.SetActive(false);
        BgPanell.SetActive(false);
        ChatPanel.SetActive(false);
        ConfigurationsPanel.SetActive(false);
        InventoryGrp.SetActive(false);
        changeItens = false;

        PlayerCononectMySql.UpdateItensForCharacter(PlayerCononectMySql._connection,DW._id,DW.chest,DW.hands,DW.WeaponMain,
                                                    DW.Legs,DW.Boots,DW.WeaponOff);

        PlayerCononectMySql._connection.Close();
        PlayerCononectMySql._connection.Dispose();
    }

    //Menu de configurações
    public void ConfigurationsMenu()
    {
        InventoryGrp.SetActive(false);
        OverallData.SetActive(false);
        StastGeneral.SetActive(false);
        StastToAllowcateInCreat.SetActive(false);
        SelectPanel.SetActive(false);
        AreaToGo.SetActive(false);
        //ChatPanel.SetActive(false);
        ConfigurationsPanel.SetActive(true);
        changeItens = false;
    }

    public void PlusButtonInCreate(int value)
    {
        //Quando cria o personagem aqui são adc os pontos para determinados stats de base
        if (_pointsLeft >= 0)
        {
            mount.GetComponentInChildren<PlayerCharacter>().GetPrimaryAttribute(value).BaseValue++;
            StatsToAllowcate[value].text = mount.GetComponentInChildren<PlayerCharacter>().GetPrimaryAttribute(value).BaseValue.ToString();
            _status[value] = mount.GetComponentInChildren<PlayerCharacter>().GetPrimaryAttribute(value).BaseValue;
            pointsLeft.text = "Points Left :" + _pointsLeft.ToString();
            _pointsLeft--;
        }
    }

    public void MinusButtonInCreate(int value)
    {
        //Quando cria o personagem aqui são diminuir os pontos que foram adc para determinados stats de base
        if (_pointsLeft <= 26)
        {
            mount.GetComponentInChildren<PlayerCharacter>().GetPrimaryAttribute(value).BaseValue--;
            StatsToAllowcate[value].text = mount.GetComponentInChildren<PlayerCharacter>().GetPrimaryAttribute(value).BaseValue.ToString();
            _status[value] = mount.GetComponentInChildren<PlayerCharacter>().GetPrimaryAttribute(value).BaseValue;
            pointsLeft.text = "Points Left :" + _pointsLeft.ToString();
            _pointsLeft++;
        }
    }

    public void PlusButton(int value)
    {
        slots[value].GetComponentInChildren<PlayerCharacter>().GetPrimaryAttribute(value).BaseValue++;
        StatsToAllowcate[value].text = slots[value].GetComponentInChildren<PlayerCharacter>().GetPrimaryAttribute(value).BaseValue.ToString();
        pointsLeft.text = "Points Left :" + _pointsLeft.ToString();
        _pointsLeft--;
    }

    public void MinusButton(int value)
    {
        slots[value].GetComponentInChildren<PlayerCharacter>().GetPrimaryAttribute(value).BaseValue--;
        StatsToAllowcate[value].text = slots[value].GetComponentInChildren<PlayerCharacter>().GetPrimaryAttribute(value).BaseValue.ToString();
        pointsLeft.text = "Points Left :" + _pointsLeft.ToString();
        _pointsLeft++;
    }

    public void ArenaButton()
    {
        ArenaPanell.SetActive(true);
        BgPanell.SetActive(false);
    }

    public void BgButton()
    {
        ArenaPanell.SetActive(false);
        BgPanell.SetActive(true);
    }

    public void GoToLevel(string lvl)
    {
        PlayerCononectMySql.LoadCharacterData(_allStuffID);
        PlayerCononectMySql.GetStatusForCharacter(PlayerCononectMySql._connection, _allStuffID);
        PlayerCononectMySql._connection.Close();
        SceneManager.LoadScene(lvl);//"SkyFall Arena(2)"
    }

    //Qndo se adc pontos pela primeira vez
    private void FirstPointsToAdd(int index)
    {
        pointsLeft.text = _pointsLeft.ToString();

        for (int cnt = 0; cnt < Enum.GetValues(typeof(AtributeName)).Length; cnt++)
        {
            slots[index].GetComponentInChildren<PlayerCharacter>().GetPrimaryAttribute(cnt).BaseValue = STARTING_VALUE;
            StatsToAllowcate[cnt].text = slots[index].GetComponentInChildren<PlayerCharacter>().GetPrimaryAttribute(cnt).BaseValue.ToString();
            _pointsLeft = (STARTING_VALUE - MIN_STARTING_ATTRIBUTES_VALUE);
        }
    }

    private void GeneralStastValues(int index)
    {
        for (int cnt = 0; cnt < Enum.GetValues(typeof(AtributeName)).Length; cnt++)
        {
            slots[index].GetComponentInChildren<PlayerCharacter>().GetPrimaryAttribute(cnt).BaseValue = _status[cnt];
            StatsGeneral[cnt].text = slots[index].GetComponentInChildren<PlayerCharacter>().GetPrimaryAttribute(cnt).BaseValue.ToString();
        }
    }
    #endregion

    #region Creat Character
    public void OnEnable()
    {
        Messenger.AddListener("ToggleGender", OnToggleGender);
        Messenger<bool>.AddListener("RotatePlayerClockwise", RotatePlayerClockwise);
    }

    public void OnDisable()
    {
        Messenger.RemoveListener("ToggleGender", OnToggleGender);
        Messenger<bool>.RemoveListener("RotatePlayerClockwise", RotatePlayerClockwise);
    }

    public void OnToggleGender()
    {
        _unsingMaleModel = !_unsingMaleModel;
        _index = 0;
        InstantiateCharacterModel();
    }

    public static void ChangePlayerSkinColor(int color)
    {
        skinColor = color;
    }

    private void InstantiateCharacterModel()
    {
        if (transform.childCount != 0)
            for (int cnt = 0; cnt < transform.childCount; cnt++)
                Destroy(transform.GetChild(cnt).gameObject);

        if (_unsingMaleModel)
        {
            CharacterMesh = Instantiate(Resources.Load(GameSettings2.MALE_MODEL_PATH + GameSettings2.maleModels[_index]),
                                        transform.position = new Vector3(3.94f, 0.3282194f, 0),
                                        transform.rotation) as GameObject;

            CharacterMesh.GetComponent<Player_Controler>().enabled = false;
            CharacterMesh.GetComponentInChildren<Camera>().enabled = false;
            CharacterMesh.GetComponentInChildren<NetworkLocalController>().enabled = false;

            _gender = 0;

            _hair.LoadInicialHairMale();
            _eyes.LoadInicialEyesMale();
        }
        else {
            CharacterMesh = Instantiate(Resources.Load(GameSettings2.FEMALE_MODEL_PATH + GameSettings2.femaleModels[_index]),
                                        transform.position = new Vector3(3.94f, 0.3282194f, 0),
                                        transform.rotation) as GameObject;

            CharacterMesh.GetComponent<Player_Controler>().enabled = false;
            CharacterMesh.GetComponentInChildren<Camera>().enabled = false;
            CharacterMesh.GetComponentInChildren<NetworkLocalController>().enabled = false;

            _gender = 1;
            _hair.LoadInicialHairFemale();
            _eyes.LoadInicialEyesFemale();
        }
        CharacterMesh.transform.parent = transform;
    }

    private void RotatePlayerClockwise(bool clockwise)
    {
        _rotateme = true;
        _rotClockwise = clockwise;
    }

    private void SaveCreatedNewCharacter()
    {
        _charSlot = _charCount - 1;
        PlayerCononectMySql.SendNewCharacterToBd(PlayerCononectMySql._connection,
                                                    _charOwner, _charID, _charName,
                                                    _startLvl, 0, _charSlot, _gender,
                                                    _hair.hairMechIndex,
                                                    _hair.hairColorIndex,
                                                    _eyes.eyesMechIndex,
                                                    _eyes.eyesColorIndex);

        PlayerCononectMySql.UpdateOwnerInfo(PlayerCononectMySql._connection, _charCount, _charOwner);

        PlayerCononectMySql.SendStatusForCharacter(PlayerCononectMySql._connection, _charID, _charlvlup, _status[0],
                                                   _status[1], _status[2], _status[3], _status[4], _status[5], _status[6],
                                                   _status[7], _status[8], _status[9], _status[10], _status[11],
                                                   _status[12]);


        PlayerCononectMySql._connection.Close();
        PlayerCononectMySql._connection.Dispose();
    }

    private void CharCreatInstance(int charslot)
    {
        if (_charCount == 4)
            CreatButton.SetActive(false);

        if (_characterCreated)
        {
            //Debug.Log(charslot);
            PlayerCononectMySql.LoadCharacterData(charslot);
            InstantiateAllCharacter(charslot, _charID, slots[charslot]);
            _charName = PlayerCononectMySql._charName;
            _charID = PlayerCononectMySql._charID;
            CharSelect[charslot].gameObject.SetActive(true);
            CharSelect[charslot].gameObject.GetComponent<CharSelectBut>()._Name.text = "Name :" + _charName;
            CharSelect[charslot].gameObject.GetComponent<CharSelectBut>().Level.text = "Level :" + _startLvl;
            CharSelect[charslot].gameObject.GetComponent<CharSelectBut>().charID = _charID;
            CharSelect[charslot].gameObject.GetComponent<CharSelectBut>().Index = charslot;
            _needToAddPoints = true;
            CharSelect[charslot].gameObject.GetComponent<CharSelectBut>().needpoints = _needToAddPoints;
        }
    }

    public void InstantiateAllCharacter(int slot, int charID, GameObject pos)
    {
        if (PlayerCononectMySql._gender == 0)
        {

            CharacterMesh = Instantiate(Resources.Load(GameSettings2.MALE_MODEL_PATH + GameSettings2.maleModels[_index]),
                                        transform.position = pos.transform.position,
                                        transform.rotation) as GameObject;

            CharacterMesh.transform.parent = pos.transform;
            CharacterMesh.GetComponent<PlayerCharacter>()._charID = charID;
            CharacterMesh.GetComponent<Player_Controler>().name = _charName;
            CharacterMesh.GetComponent<Player_Controler>().enabled = false;
            CharacterMesh.GetComponentInChildren<Camera>().enabled = false;
            CharacterMesh.GetComponentInChildren<NetworkLocalController>().enabled = false;

            GameObject hairStyle;

            int hairMechIndex = PlayerCononectMySql._hairMesh;

            hairStyle = Instantiate(Resources.Load(GameSettings2.MALE_HAIR_PATH + "Hair" + " " + hairMechIndex),
                CharacterMesh.GetComponent<PlayerCharacter>().HairAnchor.transform.position,
                CharacterMesh.GetComponent<PlayerCharacter>().HairAnchor.transform.rotation) as GameObject;

            hairStyle.transform.parent = CharacterMesh.GetComponent<PlayerCharacter>().HairAnchor.transform;

            Texture temp = Resources.Load(GameSettings2.HAIR_TEXURE_PATH + ((HairColorNames)PlayerCononectMySql._hairColor).ToString()) as Texture;
            CharacterMesh.GetComponent<PlayerCharacter>().HairAnchor.GetComponentInChildren<Renderer>().material.mainTexture = temp;

            GameObject eyesStyle;
            int eyesMechIndex = PlayerCononectMySql._eyesMesh;

            eyesStyle = Instantiate(Resources.Load(GameSettings2.MALE_EYES_PATH + "Eyes" + " " + eyesMechIndex),
                        CharacterMesh.GetComponent<PlayerCharacter>().EyesAnchor.transform.position,
                        CharacterMesh.GetComponent<PlayerCharacter>().EyesAnchor.transform.rotation) as GameObject;

            eyesStyle.transform.parent = CharacterMesh.GetComponent<PlayerCharacter>().EyesAnchor.transform;
            eyesStyle.transform.localPosition = Vector3.zero;
            eyesStyle.transform.rotation = new Quaternion(0, 0, 0, 0);

            Texture tempeye = Resources.Load(GameSettings2.EYES_TEXURE_PATH + ((EyesColorNames)PlayerCononectMySql._eyesColor).ToString()) as Texture;
            CharacterMesh.GetComponent<PlayerCharacter>().EyesAnchor.GetComponentInChildren<Renderer>().material.mainTexture = tempeye;
        }
        else
        {
            CharacterMesh = Instantiate(Resources.Load(GameSettings2.FEMALE_MODEL_PATH + GameSettings2.femaleModels[_index]),
                                        transform.position = pos.transform.position,
                                        transform.rotation) as GameObject;

            CharacterMesh.transform.parent = pos.transform;
            CharacterMesh.GetComponent<PlayerCharacter>()._charID = charID;
            CharacterMesh.GetComponent<Player_Controler>().name = _charName;
            CharacterMesh.GetComponent<Player_Controler>().enabled = false;
            CharacterMesh.GetComponentInChildren<Camera>().enabled = false;
            CharacterMesh.GetComponentInChildren<NetworkLocalController>().enabled = false;

            GameObject hairStyle;
            int hairMechIndex = PlayerCononectMySql._hairMesh;

            hairStyle = Instantiate(Resources.Load(GameSettings2.FEMALE_HAIR_PATH + "Hair" + " " + hairMechIndex),
                    CharacterMesh.GetComponent<PlayerCharacter>().HairAnchor.transform.position,
                    CharacterMesh.GetComponent<PlayerCharacter>().HairAnchor.transform.rotation) as GameObject;

            hairStyle.transform.parent = CharacterMesh.GetComponent<PlayerCharacter>().HairAnchor.transform;

            Texture temp = Resources.Load(GameSettings2.HAIR_TEXURE_PATH + ((HairColorNames)PlayerCononectMySql._hairColor).ToString()) as Texture;
            CharacterMesh.GetComponent<PlayerCharacter>().HairAnchor.GetComponentInChildren<Renderer>().material.mainTexture = temp;

            GameObject eyesStyle;
            int eyesMechIndex = PlayerCononectMySql._eyesMesh;

            eyesStyle = Instantiate(Resources.Load(GameSettings2.FEMALE_EYES_PATH + "Eyes" + " " + eyesMechIndex),
                        CharacterMesh.GetComponent<PlayerCharacter>().EyesAnchor.transform.position,
                        CharacterMesh.GetComponent<PlayerCharacter>().EyesAnchor.transform.rotation) as GameObject;

            eyesStyle.transform.parent = CharacterMesh.GetComponent<PlayerCharacter>().EyesAnchor.transform;
            eyesStyle.transform.localPosition = Vector3.zero;
            eyesStyle.transform.rotation = new Quaternion(0, 0, 0, 0);

            Texture tempeye = Resources.Load(GameSettings2.EYES_TEXURE_PATH + ((EyesColorNames)PlayerCononectMySql._eyesColor).ToString()) as Texture;
            CharacterMesh.GetComponent<PlayerCharacter>().EyesAnchor.GetComponentInChildren<Renderer>().material.mainTexture = tempeye;
        }
    }
    #endregion

    #region Stat
    private void GetCharacterStast(int ID)
    {
        PlayerCononectMySql.GetStatusForCharacter(PlayerCononectMySql._connection, ID);

        for (int cnt = 0; cnt < _status.Length; cnt++)
        {
            _status[cnt] = PlayerCononectMySql._status[cnt];
        }
    }

    #endregion

    #region Itens
    void AddCharacterItens(int gender, int slotMesh, int chest, int hands, 
                           int WeaponMain, int Legs, int Boots, int WeaponOff)
    {
        if (database.items[chest].itemType == Item.ItemType.Chest)
        {
            if (gender == 0)
            {
                //item0 = Instantiate(Resources.Load(GameSettings2.MALE_ARMORS_MESH_PATH + database.items[chest].itemName),
                //                    slots[slotMesh].GetComponentInChildren<PlayerCharacter>().Chest_Holder.transform.position,
                //                    slots[slotMesh].GetComponentInChildren<PlayerCharacter>().Chest_Holder.transform.rotation)
                //                    as GameObject;

                //item0.transform.parent = slots[slotMesh].GetComponentInChildren<PlayerCharacter>().Chest_Holder.transform;
            }
            else
            {
                item0 = Instantiate(Resources.Load(GameSettings2.FEMALE_ARMORS_MESH_PATH + database.items[chest].itemName),
                                    slots[slotMesh].GetComponentInChildren<PlayerCharacter>().Chest_Holder.transform.position,
                                    slots[slotMesh].GetComponentInChildren<PlayerCharacter>().Chest_Holder.transform.rotation)
                                    as GameObject;

                item0.transform.parent = slots[slotMesh].GetComponentInChildren<PlayerCharacter>().Chest_Holder.transform;
            }
        }

        if (database.items[hands].itemType == Item.ItemType.Hands)
        {
            //if (gender == 0)
            //{
            //    item1 = Instantiate(Resources.Load(GameSettings2.MALE_ARMORS_MESH_PATH + database.items[hands].itemName),
            //                        Player.slots[slotMesh].GetComponentInChildren<PlayerCharacter>().Weapon_Holder_R.transform.position,
            //                        Player.slots[slotMesh].GetComponentInChildren<PlayerCharacter>().Weapon_Holder_R.transform.rotation)
            //                        as GameObject;
            //    item1.transform.parent = slots[slotMesh].GetComponentInChildren<PlayerCharacter>().Chest_Holder.transform.transform;
            //}
            //else
            //{
            //    item1 = Instantiate(Resources.Load(GameSettings2.FEMALE_ARMORS_MESH_PATH + database.items[hands].itemName),
            //                        Player.slots[slotMesh].GetComponentInChildren<PlayerCharacter>().Weapon_Holder_R.transform.position,
            //                        Player.slots[slotMesh].GetComponentInChildren<PlayerCharacter>().Weapon_Holder_R.transform.rotation)
            //                        as GameObject;
            //    item1.transform.parent = slots[slotMesh].GetComponentInChildren<PlayerCharacter>().Chest_Holder.transform;
            //}
        }

        if (database.items[WeaponMain].itemType == Item.ItemType.Weapon)
        {
            if (typeof(Weapon) == database.items[WeaponMain].GetType())
            {
                Weapon wep = (Weapon)database.items[WeaponMain];

                if (wep.weaponType == Weapon.WeaponType.MainHand)
                {
                    item2 = Instantiate(Resources.Load(GameSettings2.MELLE_WEAPON_MESH_PATH + database.items[WeaponMain].itemName),
                                     slots[slotMesh].transform.GetComponentInChildren<PlayerCharacter>().DrwHolder_L.transform.position,
                                     slots[slotMesh].transform.GetComponentInChildren<PlayerCharacter>().DrwHolder_L.transform.rotation)
                                     as GameObject;

                    item2.transform.parent = slots[slotMesh].transform.GetComponentInChildren<PlayerCharacter>().DrwHolder_L.transform;
                    item2.transform.position = slots[slotMesh].transform.GetComponentInChildren<PlayerCharacter>().DrwHolder_L.transform.position;
                    item2.transform.rotation = slots[slotMesh].transform.GetComponentInChildren<PlayerCharacter>().DrwHolder_L.transform.rotation;

                    if (slots[slotMesh].transform.GetComponentInChildren<PlayerCharacter>().DrwHolder_L.transform.childCount != 1)
                    {
                        for (int cnt = 1; cnt < slots[slotMesh].transform.GetComponentInChildren<PlayerCharacter>().DrwHolder_L.transform.childCount; cnt++)
                            Destroy(slots[slotMesh].transform.GetComponentInChildren<PlayerCharacter>().DrwHolder_L.transform.GetChild(cnt).gameObject);
                    }
                }

                if (wep.weaponType == Weapon.WeaponType.TwoHand)
                {
                    item2 = Instantiate(Resources.Load(GameSettings2.MELLE_WEAPON_MESH_PATH + database.items[WeaponMain].itemName),
                                            slots[slotMesh].transform.GetComponentInChildren<PlayerCharacter>().DrwHolder_BackR.transform.position,
                                            slots[slotMesh].transform.GetComponentInChildren<PlayerCharacter>().DrwHolder_BackR.transform.rotation)
                                            as GameObject;

                    item2.transform.parent = slots[slotMesh].transform.GetComponentInChildren<PlayerCharacter>().DrwHolder_BackR.transform;
                    item2.transform.position = slots[slotMesh].transform.GetComponentInChildren<PlayerCharacter>().DrwHolder_BackR.transform.position;
                    item2.transform.rotation = slots[slotMesh].transform.GetComponentInChildren<PlayerCharacter>().DrwHolder_BackR.transform.rotation;


                    if (slots[slotMesh].transform.GetComponentInChildren<PlayerCharacter>().DrwHolder_BackR.transform.childCount != 1)
                    {
                        for (int cnt = 1; cnt < slots[slotMesh].transform.GetComponentInChildren<PlayerCharacter>().DrwHolder_BackR.transform.childCount; cnt++)
                            Destroy(slots[slotMesh].transform.GetComponentInChildren<PlayerCharacter>().DrwHolder_BackR.transform.GetChild(cnt).gameObject);
                    }
                }
            }

            //item2 = Instantiate(Resources.Load(GameSettings2.MELLE_WEAPON_MESH_PATH + database.items[WeaponMain].itemName),
            //                        slots[slotMesh].GetComponentInChildren<PlayerCharacter>().DrwHolder_L.transform.position,
            //                        slots[slotMesh].GetComponentInChildren<PlayerCharacter>().DrwHolder_L.transform.rotation)
            //                        as GameObject;

            //item2.transform.parent = slots[slotMesh].GetComponentInChildren<PlayerCharacter>().DrwHolder_L.transform;
            //item2.transform.position = slots[slotMesh].GetComponentInChildren<PlayerCharacter>().DrwHolder_L.transform.position;
            //item2.transform.rotation = slots[slotMesh].GetComponentInChildren<PlayerCharacter>().DrwHolder_L.transform.rotation;
        }

        if (database.items[Legs].itemType == Item.ItemType.Legs)
        {
            if (gender == 0)
            {
                //item4 = Instantiate(Resources.Load(GameSettings2.MALE_ARMORS_MESH_PATH + database.items[Legs].itemName),
                //                    slots[slotMesh].GetComponentInChildren<PlayerCharacter>().Leggs_Holder.transform.position,
                //                    slots[slotMesh].GetComponentInChildren<PlayerCharacter>().Leggs_Holder.transform.rotation)
                //                    as GameObject;

                //item4.transform.parent = slots[slotMesh].GetComponentInChildren<PlayerCharacter>().Leggs_Holder.transform;
            }
            else
            {
                item4 = Instantiate(Resources.Load(GameSettings2.FEMALE_ARMORS_MESH_PATH + database.items[Legs].itemName),
                                    slots[slotMesh].GetComponentInChildren<PlayerCharacter>().Leggs_Holder.transform.position,
                                    slots[slotMesh].GetComponentInChildren<PlayerCharacter>().Leggs_Holder.transform.rotation)
                                    as GameObject;

                item4.transform.parent = slots[slotMesh].GetComponentInChildren<PlayerCharacter>().Leggs_Holder.transform;
            }

            slots[slotMesh].GetComponentInChildren<PlayerCharacter>().meshToHide2.SetActive(false);
        }

        if (database.items[Boots].itemType == Item.ItemType.Boots)
        {
            if (gender == 0)
            {
                //item3 = Instantiate(Resources.Load(GameSettings2.MALE_ARMORS_MESH_PATH + database.items[Boots].itemName),
                //                    slots[slotMesh].GetComponentInChildren<PlayerCharacter>().Boots_Holder.transform.position,
                //                   slots[slotMesh].GetComponentInChildren<PlayerCharacter>().Boots_Holder.transform.rotation)
                //                    as GameObject;

                //item3.transform.parent = slots[slotMesh].GetComponentInChildren<PlayerCharacter>().Boots_Holder.transform;
                //item3.transform.position = slots[slotMesh].GetComponentInChildren<PlayerCharacter>().Boots_Holder.transform.position;
                //item3.transform.rotation = slots[slotMesh].GetComponentInChildren<PlayerCharacter>().Boots_Holder.transform.rotation;
            }
            else
            {
                item3 = Instantiate(Resources.Load(GameSettings2.FEMALE_ARMORS_MESH_PATH + database.items[Boots].itemName),
                                    slots[slotMesh].GetComponentInChildren<PlayerCharacter>().Boots_Holder.transform.position,
                                    slots[slotMesh].GetComponentInChildren<PlayerCharacter>().Boots_Holder.transform.rotation)
                                    as GameObject;

                item3.transform.parent = slots[slotMesh].GetComponentInChildren<PlayerCharacter>().Boots_Holder.transform;
                item3.transform.position = slots[slotMesh].GetComponentInChildren<PlayerCharacter>().Boots_Holder.transform.position;
                item3.transform.rotation = slots[slotMesh].GetComponentInChildren<PlayerCharacter>().Boots_Holder.transform.rotation;
            }

            slots[slotMesh].GetComponentInChildren<PlayerCharacter>().meshToHide3.SetActive(false);
        }

        if (database.items[WeaponOff].itemType == Item.ItemType.WeaponOff)
        {
            item5 = Instantiate(Resources.Load(GameSettings2.MELLE_SHILD_MESH_PATH + database.items[WeaponOff].itemName),
                                    slots[slotMesh].GetComponentInChildren<PlayerCharacter>().Weapon_Holder_L.transform.position,
                                    slots[slotMesh].GetComponentInChildren<PlayerCharacter>().Weapon_Holder_L.transform.rotation)
                                    as GameObject;

            item5.transform.parent = slots[slotMesh].GetComponentInChildren<PlayerCharacter>().Weapon_Holder_L.transform;
            item5.transform.position = slots[slotMesh].GetComponentInChildren<PlayerCharacter>().Weapon_Holder_L.transform.position;
            item5.transform.rotation = slots[slotMesh].GetComponentInChildren<PlayerCharacter>().Weapon_Holder_L.transform.rotation;
        }
    }

    #endregion

    void OnApplicationQuit()
    {
        PlayerCononectMySql._connection.Close();
        PlayerCononectMySql._connection.Dispose();
    }
}
