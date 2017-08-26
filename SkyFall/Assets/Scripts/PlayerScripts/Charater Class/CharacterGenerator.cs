using UnityEngine;
using System.Collections;
using System;

public class CharacterGenerator : MonoBehaviour
{
    #region Stats Variables
    public GameObject mount;
    
    private const int STARTING_POINTS = 350;
    private const int MIN_STARTING_ATTRIBUTES_VALUE = 10;
    private const int STARTING_VALUE = 35;

    private const int OFFSET = 5;
    private const int LINE_HIGH = 20;
    private const int STAT_LABEL_WIDTH = 100;
    private const int BASEVALUE_LABEL_WIDTH = 30;
    private const int BUTTON_WIDTH = 20;
    private const int BUTTON_HEIGTH = 20;
    private int statStartingPos = 40;
    private int _pointsLeft;
    public float delayTimer = .1f;
    private float _lastClick = 0;
    #endregion    

    GameObject player;
    // Use this for initialization
    void Awake()
    {
       
    }

    void Start()
    {
        PlayerCharacter.Instance.GetComponentInChildren<Camera>().enabled = false;

        mount.SetActive(false);      
        for (int cnt = 0; cnt < Enum.GetValues(typeof(AtributeName)).Length; cnt++)
        {
            PlayerCharacter.Instance.GetPrimaryAttribute(cnt).BaseValue = STARTING_VALUE;
            _pointsLeft = (STARTING_VALUE - MIN_STARTING_ATTRIBUTES_VALUE);            
        }
        PlayerCharacter.Instance.StatsUpDate();
        player = GameObject.FindGameObjectWithTag("Player");

        //player.GetComponent<Inventory>().enabled = false;
    }

    void OnGUI()
    {
        //DisplayName();
        DisplayPointsLeft();
        DisplayAttributes();
        DisplayVitals();
        DisplaySkill();

        if (PlayerCharacter.Instance.Name != "" && _pointsLeft < 1)
            DisplayCreatButton();
    }
    //mostra o nome do player
    private void DisplayName()
    {
        GUI.Label(new Rect(10, 10, 50, 25), "Name");
        PlayerCharacter.Instance.Name = GUI.TextField(new Rect(65, 10, 100, 25), PlayerCharacter.Instance.Name);
    }

    //mostra os atributos, adiciona pontos
    private void DisplayAttributes()
    {
  
        for (int cnt = 0; cnt < Enum.GetValues(typeof(AtributeName)).Length; cnt++)
        {
            GUI.Label(new Rect(OFFSET,                                  // pos x
                               statStartingPos + (cnt * LINE_HIGH),     // pos y
                               STAT_LABEL_WIDTH,                        // width
                               LINE_HIGH),                              // heigth
                               ((AtributeName)cnt).ToString());

            GUI.Label(new Rect(STAT_LABEL_WIDTH + OFFSET,               // pos x
                                statStartingPos + (cnt * LINE_HIGH),    // pos y
                                BASEVALUE_LABEL_WIDTH,                  // width
                                LINE_HIGH),                             // heigth
                                PlayerCharacter.Instance.GetPrimaryAttribute(cnt).AdjustedBaseValue.ToString());

            if (GUI.RepeatButton(new Rect(OFFSET + STAT_LABEL_WIDTH + BASEVALUE_LABEL_WIDTH,    // pos x
                                    statStartingPos + (cnt * BUTTON_HEIGTH),                    // pos y
                                    BUTTON_WIDTH,                                               // width
                                    BUTTON_HEIGTH),                                             // heitgh
                                    "-"))
            {
                if (Time.time - _lastClick > delayTimer)
                {
                    if (PlayerCharacter.Instance.GetPrimaryAttribute(cnt).BaseValue > MIN_STARTING_ATTRIBUTES_VALUE)
                    {
                        PlayerCharacter.Instance.GetPrimaryAttribute(cnt).BaseValue--;
                        _pointsLeft++;
                        PlayerCharacter.Instance.StatsUpDate();
                    }
                    _lastClick = Time.time;                    
                }
            }

            if (GUI.RepeatButton(new Rect(OFFSET + STAT_LABEL_WIDTH + BASEVALUE_LABEL_WIDTH + BUTTON_WIDTH,       // pos x 
                                    statStartingPos + (cnt * BUTTON_HEIGTH),                                      // pos y
                                    BUTTON_WIDTH,                                                                 // width
                                    BUTTON_HEIGTH),                                                               // heitgh
                                    "+"))
            {
                if (Time.time - _lastClick > delayTimer)
                {
                    if (_pointsLeft > 0)
                    {
                        PlayerCharacter.Instance.GetPrimaryAttribute(cnt).BaseValue++;
                        _pointsLeft--;
                        PlayerCharacter.Instance.StatsUpDate();
                    }
                    _lastClick = Time.time;
                }
            }
        }
    }

    //mostra os pontos de vitalidade
    private void DisplayVitals()
    {

        for (int cnt = 0; cnt < Enum.GetValues(typeof(VitalName)).Length; cnt++)
        {
            GUI.Label(new Rect(OFFSET,                                          // pos x 
                               statStartingPos + ((cnt + 13) * LINE_HIGH),      // pos y
                               STAT_LABEL_WIDTH,                                // width
                               LINE_HIGH),                                      // heitgh
                               ((VitalName)cnt).ToString());
            
            GUI.Label(new Rect(OFFSET + STAT_LABEL_WIDTH,                               // pos x 
                               statStartingPos + ((cnt + 13) * LINE_HIGH),              // pos y
                               BASEVALUE_LABEL_WIDTH,                                   // width
                               LINE_HIGH),                                              // heitgh 
                               PlayerCharacter.Instance.GetVitals(cnt).AdjustedBaseValue.ToString());
        }
    }

    //mostra os pontos de abilidade
    private void DisplaySkill()
    {

        for (int cnt = 0; cnt < Enum.GetValues(typeof(SkillName)).Length; cnt++)
        {
            GUI.Label(new Rect(OFFSET + STAT_LABEL_WIDTH + BASEVALUE_LABEL_WIDTH + BUTTON_WIDTH * 2 + OFFSET * 2,statStartingPos + (cnt * LINE_HIGH),STAT_LABEL_WIDTH,LINE_HIGH),((SkillName)cnt).ToString());

            GUI.Label(new Rect(OFFSET + STAT_LABEL_WIDTH + BASEVALUE_LABEL_WIDTH + BUTTON_WIDTH * 2 + OFFSET * 2 + STAT_LABEL_WIDTH,statStartingPos + (cnt * LINE_HIGH),BASEVALUE_LABEL_WIDTH,LINE_HIGH),PlayerCharacter.Instance.GetSkills(cnt).AdjustedBaseValue.ToString());
        }
    }

    //pontos restantes
    private void DisplayPointsLeft()
    {
        GUI.Label(new Rect(250, 10, 100, 25), "Points Left:" + _pointsLeft.ToString());
    }

    private void DisplayCreatLable()
    {
        GUI.Button(new Rect(Screen.width / 2 - 50,statStartingPos + (16 * LINE_HIGH),100,LINE_HIGH),"Create", "Button");
    }

    //Cria o personagem, salva seus dados, e preferencias
    private void DisplayCreatButton()
    {
        if (GUI.Button(new Rect(Screen.width / 2 - 50,statStartingPos + (16 * LINE_HIGH), 100,LINE_HIGH),"Next"))
        {
            UpDateCurVitalValues();
            GameSettings2.SaveName(PlayerCharacter.Instance.Name);
            GameSettings2.SaveAttributes(PlayerCharacter.Instance.primaryAttribute);
            GameSettings2.SaveVitals(PlayerCharacter.Instance.vital);
            GameSettings2.SaveSkills(PlayerCharacter.Instance.skills);
            Debug.Log(player);
            //player.GetComponent<Inventory>().enabled = true;
        }
    }

    //atualiza os pontos do personagem
    private void UpDateCurVitalValues()
    {
        for (int cnt = 0; cnt < Enum.GetValues(typeof(VitalName)).Length; cnt++)
        {
            PlayerCharacter.Instance.GetVitals(cnt).CurValue = PlayerCharacter.Instance.GetVitals(cnt).AdjustedBaseValue;
        }
    }



}
