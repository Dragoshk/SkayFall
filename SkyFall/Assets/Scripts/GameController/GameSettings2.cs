using UnityEngine;
using System;
using System.Collections;

public static class GameSettings2
{
    public const float VERSION = .1f;

    #region PlayerPreff Constants
    private const string PLAYER_POSITION = "Player Position";
    private const string CHARACTER_MODEL_INDEX = "Model Index";
    private const string HAIR_COLOR = "Hair Color";
    private const string HAIR_MESH = "Hair Mesh";
    private const string EYES_COLOR = "Eyes Color";
    private const string EYES_MESH = "Eyes Mesh";
    private const string PLAYER_NAME = "PlayerName";
    private const string BASE_VALUE = " - BASE VALUE";
    private const string EXP_TO_LEVEL = " - EXP TO LEVEL";
    private const string CUR_vALUE = " - CUR VALUE";
    private const string GENDER = "Gender";
    private const string ID = "ID";
    //Itens
    private const string WEAPON = "WEAPON";
    private const string Shield = "Shield";
    private const string Chest = "Chest";
    private const string Legs = "Legs";
    private const string Glove = "Glove";
    private const string boots = "boots";

    private const string user_id = "User";
    private const string password = "Password";
    #endregion

    #region Resources Paths
    //public const string MELEE_WEAPON_iCON_PATH = "WEapons/Melee";

    public const string MALE_MODEL_PATH = "Character/Male/";
    public const string FEMALE_MODEL_PATH = "Character/Female/";
    public static string[] maleModels = { "Male_Player" };
    public static string[] femaleModels = { "Female_Player" };
    public static string MALE_HAIR_PATH = "Character/Male/Hair/";
    public static string FEMALE_HAIR_PATH = "Character/Female/Hair/";
    public static string HAIR_TEXURE_PATH = "Character/Male/Hair/Textures/";
    public static string EYES_TEXURE_PATH = "Character/Eyes_Texture/";
    public static string MALE_EYES_PATH = "Character/Male/Eyes/";
    public static string FEMALE_EYES_PATH = "Character/Female/Eyes/";
    #endregion

    public static Vector3 StartingPosition = new Vector3(0, .3f, 0);
    public static string[] levelNames = new string[3] { "Main_Menu", "Character Generator", "lol" };

    static GameSettings2()
    {
    }

    public static void TempUser(string user)
    {
        PlayerPrefs.SetString(user_id, user);
    }

    public static string LoadTempUser()
    {
        return PlayerPrefs.GetString(user_id, "Name me");
    }

    public static void TempPassword(string pass)
    {
        PlayerPrefs.SetString(password, pass);
    }

    public static string LoadTempPassword()
    {
        return PlayerPrefs.GetString(password, "Name me");
    }
    public static void SaveID(int index)
    {
        PlayerPrefs.SetInt(ID, index);
    }
    public static int LoadID()
    {
        return PlayerPrefs.GetInt(ID, 0);
    }

    #region Customizations Male
    //Hair Colors
    public static void SaveHairColorMale(int index)
    {
        PlayerPrefs.SetInt(HAIR_COLOR, index);
    }

    public static int LoadHairColorMale()
    {
        return PlayerPrefs.GetInt(HAIR_COLOR, 0);
    }

    //Hair mesh
    public static void SaveHairMeshMale(int index)
    {
        PlayerPrefs.SetInt(HAIR_MESH, index);
    }

    public static int LoadHairMeshMale()
    {
        return PlayerPrefs.GetInt(HAIR_MESH, 0);
    }

    public static void SaveHairMale(int mesh, int color)
    {
        SaveHairColorMale(color);
        SaveHairMeshMale(mesh);
    }

    //Eyes Color
    public static void SaveEyesColorMale(int index)
    {
        PlayerPrefs.SetInt(EYES_COLOR, index);
    }

    public static int LoadEyesColorMale()
    {
        return PlayerPrefs.GetInt(EYES_COLOR, 0);
    }

    //Eyes mesh
    public static void SaveEyesMeshMale(int index)
    {
        PlayerPrefs.SetInt(EYES_MESH, index);
    }

    public static int LoadEyesMeshMale()
    {
        return PlayerPrefs.GetInt(EYES_MESH, 0);
    }

    public static void SaveEyesMale(int mesh, int color)
    {
        SaveEyesColorMale(color);
        SaveEyesMeshMale(mesh);
    }
    #endregion

    #region Customizations Famele
    //Hair Colors
    public static void SaveHairColorFemale(int index)
    {
        PlayerPrefs.SetInt(HAIR_COLOR, index);
    }

    public static int LoadHairColorFemale()
    {
        return PlayerPrefs.GetInt(HAIR_COLOR, 0);
    }

    //Hair mesh
    public static void SaveHairMeshFemale(int index)
    {
        PlayerPrefs.SetInt(HAIR_MESH, index);
    }

    public static int LoadHairMeshFemale()
    {
        return PlayerPrefs.GetInt(HAIR_MESH, 0);
    }

    public static void SaveHairFemale(int mesh, int color)
    {
        SaveHairColorMale(color);
        SaveHairMeshMale(mesh);
    }

    //Eyes Color
    public static void SaveEyesColorFemale(int index)
    {
        PlayerPrefs.SetInt(EYES_COLOR, index);
    }

    public static int LoadEyesColorFemale()
    {
        return PlayerPrefs.GetInt(EYES_COLOR, 0);
    }

    //Eyes mesh
    public static void SaveEyesMeshFemale(int index)
    {
        PlayerPrefs.SetInt(EYES_MESH, index);
    }

    public static int LoadEyesMeshFemale()
    {
        return PlayerPrefs.GetInt(EYES_MESH, 0);
    }

    public static void SaveEyesFemale(int mesh, int color)
    {
        SaveEyesColorMale(color);
        SaveEyesMeshMale(mesh);
    }
    #endregion

    #region Itens Custumazation
    public const string MELLE_WEAPON_MESH_PATH = "Itens/Weapons/Melee/";
    public const string MELLE_SHILD_MESH_PATH = "Itens/Weapons/Shilds/";
    public const string FEMALE_ARMORS_MESH_PATH = "Itens/Armors/Female/";
    public const string MALE_ARMORS_MESH_PATH = "Itens/Armors/Male/";
    #endregion

    public static void SaveItens(int weapon/*,int Shield, int Chest, int Legs,int Glove, int boots*/)
    {
        PlayerPrefs.SetInt(WEAPON, weapon);
        /*
        PlayerPrefs.SetInt(WEAPON,weapon);
        PlayerPrefs.SetInt(WEAPON,weapon);
        PlayerPrefs.SetInt(WEAPON,weapon);
        PlayerPrefs.SetInt(WEAPON,weapon);
        PlayerPrefs.SetInt(WEAPON,weapon);
        */
    }

    public static int LoadItens(int WeaponID)
    {
        return PlayerPrefs.GetInt(WEAPON, WeaponID);
    }


    public static void SavePlayerPosition(Vector3 pos)
    {
        PlayerPrefs.SetFloat(PLAYER_POSITION + "x", pos.x);
        PlayerPrefs.SetFloat(PLAYER_POSITION + "y", pos.y);
        PlayerPrefs.SetFloat(PLAYER_POSITION + "z", pos.z);

    }

    public static Vector3 LoadPlayerPosition()
    {
        Vector3 temp = new Vector3(PlayerPrefs.GetFloat(PLAYER_POSITION + "x", StartingPosition.x), PlayerPrefs.GetFloat(PLAYER_POSITION + "y", StartingPosition.y), PlayerPrefs.GetFloat(PLAYER_POSITION + "z", StartingPosition.z));
        return temp;
    }
    public static void SaveName(string name)
    {
        PlayerPrefs.SetString(PLAYER_NAME, name);
    }
    public static string LoadName()
    {
        return PlayerPrefs.GetString(PLAYER_NAME, "Name me");
    }
    public static void SaveGender(int gender)
    {
        PlayerPrefs.SetInt(GENDER, gender);
    }
    public static int LoadGender()
    {
        return PlayerPrefs.GetInt(GENDER, 0);
    }
    #region Atributes
    public static void SaveAttributes(AtributeName name, Atributes att)
    {
        PlayerPrefs.SetInt(((AtributeName)name).ToString() + BASE_VALUE, att.AdjustedBaseValue);
        PlayerPrefs.SetInt(((AtributeName)name).ToString() + EXP_TO_LEVEL, att.XptoLvL);
    }

    public static Atributes LoadAttribute(AtributeName name)
    {


        Atributes att = new Atributes();
        att.BaseValue = PlayerPrefs.GetInt(((AtributeName)name).ToString() + BASE_VALUE, 0);
        att.XptoLvL = PlayerPrefs.GetInt(((AtributeName)name).ToString() + EXP_TO_LEVEL, Atributes.STARTING_EXP_COST);
        return att;
    }

    public static void SaveAttributes(Atributes[] atributes)
    {
        for (int cnt = 0; cnt < atributes.Length; cnt++)
            SaveAttributes((AtributeName)cnt, atributes[cnt]);
    }

    public static void LoadAttributes()
    {
        for (int cnt = 0; cnt < Enum.GetValues(typeof(AtributeName)).Length; cnt++)
            PlayerCharacter.Instance.primaryAttribute[cnt] = LoadAttribute((AtributeName)cnt);
    }
    #endregion
    #region Vitals
    public static void SaveVital(VitalName name, Vital vital)
    {
        PlayerPrefs.SetInt(((VitalName)name).ToString() + BASE_VALUE, vital.AdjustedBaseValue);
        PlayerPrefs.SetInt(((VitalName)name).ToString() + EXP_TO_LEVEL, vital.XptoLvL);
        PlayerPrefs.SetInt(((VitalName)name).ToString() + CUR_vALUE, vital.CurValue);
    }

    public static Vital LoadVital(VitalName name)
    {
        PlayerCharacter.Instance.GetVitals((int)name).BaseValue = PlayerPrefs.GetInt(((VitalName)name).ToString() + BASE_VALUE, 0);
        PlayerCharacter.Instance.GetVitals((int)name).XptoLvL = PlayerPrefs.GetInt(((VitalName)name).ToString() + EXP_TO_LEVEL, 0);
        //tenha certeza de q esta chamando isso pois se n n vai ajustar os valores
        PlayerCharacter.Instance.GetVitals((int)name).UpDate();
        //aki acontece a magica - guarda os valores para cada ponto vital :v
        PlayerCharacter.Instance.GetVitals((int)name).CurValue = PlayerPrefs.GetInt(((VitalName)name).ToString() + CUR_vALUE, 1);

        return PlayerCharacter.Instance.GetVitals((int)name);
    }

    public static void SaveVitals(Vital[] vital)
    {
        for (int cnt = 0; cnt < vital.Length; cnt++)
            SaveVital((VitalName)cnt, vital[cnt]);
    }

    public static void LoadVitals()
    {
        for (int cnt = 0; cnt < Enum.GetValues(typeof(VitalName)).Length; cnt++)
        {
            PlayerCharacter.Instance.vital[cnt] = LoadVital((VitalName)cnt);
            //Debug.Log(PlayerCharacter.Instance.GetVitals(cnt).AdjustedBaseValue);
        }
    }
    #endregion
    #region Skils
    public static void SaveSkills(SkillName name, Skills skill)
    {
        PlayerPrefs.SetInt(((SkillName)name).ToString() + BASE_VALUE, skill.AdjustedBaseValue);
        PlayerPrefs.SetInt(((SkillName)name).ToString() + EXP_TO_LEVEL, skill.XptoLvL);
    }

    public static Skills LoadSkills(SkillName name)
    {
        Skills skill = new Skills();
        skill.BaseValue = PlayerPrefs.GetInt(((SkillName)name).ToString() + BASE_VALUE, 0);
        skill.XptoLvL = PlayerPrefs.GetInt(((SkillName)name).ToString() + EXP_TO_LEVEL, 0);
        return skill;
    }

    public static void SaveSkills(Skills[] skill)
    {
        for (int cnt = 0; cnt < skill.Length; cnt++)
            SaveSkills((SkillName)cnt, skill[cnt]);
    }

    public static void LoadSkills()
    {
        for (int cnt = 0; cnt < Enum.GetValues(typeof(SkillName)).Length; cnt++)
            PlayerCharacter.Instance.skills[cnt] = LoadSkills((SkillName)cnt);
    }
    #endregion
}
