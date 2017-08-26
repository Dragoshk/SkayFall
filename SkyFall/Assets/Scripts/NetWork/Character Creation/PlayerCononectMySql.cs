using UnityEngine;
using System;
using System.Collections;
using MySql.Data.MySqlClient;

public static class PlayerCononectMySql
{
    //Sql variables
    public static MySqlConnection _connection;

    public static string _source = "Server = mysql.guaragames.com;" +
                                   "Database = guaragamescomdrag;" +
                                   "User ID = guaragamesdrag;" +
                                   "Pooling = false;" +
                                   "Password=Dragos*";


    #region Overall Variables
    public static string tempUser;
    public static string tempPassword;
    public static string tempEmail;
    public static string _charName;

    private static int _requestID;
    public static int _ownerChar;
    public static int _charLvl;
    public static float _charEXp;
    public static int _charCount;
    public static int _charSlot;
    public static int _charID;
    #endregion

    #region Personalization
    public static int _gender;
    public static int _hairMesh;
    public static int _hairColor;
    public static int _eyesMesh;
    public static int _eyesColor;
    #endregion

    #region Status Variables
    public static int charlvlUp;
    public static int[] _status = new int[13];
    public static int[] _itensEquiped = new int[6];
    #endregion

    public static void ConectBd(string source)
    {
        try
        {
            _connection = new MySqlConnection(source);
            _connection.Open();
        }
        catch (Exception e)
        {
            Debug.Log(e.Message.ToString());
        }
    }

    public static void InitateConection()
    {
        tempUser = GameSettings2.LoadTempUser();
        tempPassword = GameSettings2.LoadTempPassword();
        //pega o dono
        GetIDFromBd(_connection, tempUser, tempPassword);
        //pega o slot
        GetSlotData(_connection, _ownerChar);
    }

    static void GetIDFromBd(MySqlConnection connection, string user, string password)
    {
        MySqlCommand command = connection.CreateCommand();
        command.CommandText = "SELECT id,char_count FROM login WHERE user ='" + user + "'" + "AND password ='" + password + "'";
        MySqlDataReader data = command.ExecuteReader();

        while (data.Read())
        {
            _requestID = (int)data["id"];
            _charCount = (int)data["char_count"];
        }

        _ownerChar = _requestID;

        data.Close();
        data = null;
    }

    public static void LoadOwnerData()
    {
        string tempUser = GameSettings2.LoadTempUser();
        string tempPassword = GameSettings2.LoadTempPassword();

        //ConectBd(_source);

        GetIDFromBd(_connection, tempUser, tempPassword);

        GetSlotData(_connection, _ownerChar);

        GetCharacterData(_connection, _ownerChar, _charSlot);
    }

    public static void LoadCharacterData(int charSlot)
    {
        string tempUser = GameSettings2.LoadTempUser();
        string tempPassword = GameSettings2.LoadTempPassword();

        ConectBd(_source);

        GetIDFromBd(_connection, tempUser, tempPassword);

        GetSlotData(_connection, _ownerChar);

        GetCharacterData(_connection, _ownerChar, charSlot);

        GetItensForCharacter(_connection, _charID);
    }

    #region Character Base Info
    static void GetSlotData(MySqlConnection connection, int owner)
    {
        MySqlCommand command = connection.CreateCommand();
        command.CommandText = "SELECT char_slot FROM char_personalization WHERE owner_char ='" + owner + "'";
        MySqlDataReader data = command.ExecuteReader();

        while (data.Read())
        {
            _charSlot = (int)data["char_slot"];
        }

        data.Close();
        data = null;
    }

    static void GetCharacterData(MySqlConnection connection, int owner, int slot)
    {
        MySqlCommand command = connection.CreateCommand();
        command.CommandText = "SELECT char_id,char_name,char_lvl,char_exp,char_gender,char_hair_mesh," +
                              "char_hair_color,char_eyes_mesh,char_eyes_color FROM " +
                              "char_personalization WHERE owner_char ='" + owner + "'" +
                              "AND char_slot ='" + slot + "'";
        MySqlDataReader data = command.ExecuteReader();

        while (data.Read())
        {
            _charID = (int)data["char_id"];
            _charName = (string)data["char_name"];
            _charLvl = (int)data["char_lvl"];
            _charEXp = (float)data["char_exp"];
            _gender = (int)data["char_gender"];
            _hairMesh = (int)data["char_hair_mesh"];
            _hairColor = (int)data["char_hair_color"];
            _eyesMesh = (int)data["char_eyes_mesh"];
            _eyesColor = (int)data["char_eyes_color"];
        }

        data.Close();
        data = null;
    }

    public static void SendNewCharacterToBd(MySqlConnection connection, int owner, int charID, string charName, int char_lvl,
                                            int char_exp, int charSlor, int gender, int hairMesh, int hairColor, int eyesMesh, int eyesColor)
    {
        MySqlCommand command = connection.CreateCommand();
        command.CommandText = "INSERT INTO char_personalization(owner_char,char_id,char_name,char_lvl,char_exp,char_slot,char_gender,char_hair_mesh" +
                                                                ",char_hair_color,char_eyes_mesh,char_eyes_color) VALUES('"
                                                                + owner + "','" + charID + "','" + charName + "','" + char_lvl + "','" + char_exp +
                                                                "','" + charSlor + "','" + gender + "','" + hairMesh + "','" + hairColor + "','" +
                                                                eyesMesh + "','" + eyesColor + "')";
        MySqlDataReader data = command.ExecuteReader();
        data.Read();
        data.Close();
        data = null;
    }

    public static void UpdateOwnerInfo(MySqlConnection connection, int charCount, int owner)
    {
        MySqlCommand command = connection.CreateCommand();
        command.CommandText = "UPDATE login SET char_count='" + charCount + "' WHERE id='" + owner + "'";
        MySqlDataReader data = command.ExecuteReader();
        //Debug.Log(charCount);
        data.Read();
        data.Close();
        data = null;

        /*@"UPDATE tableName SET paramColumn='@paramName' WHERE conditionColumn='@conditionName'";*/
    }

    public static void UpdateCharExp(MySqlConnection connection, int charID, float exp)
    {
        MySqlCommand command = connection.CreateCommand();
        command.CommandText = "UPDATE char_personalization SET char_exp = '" + exp + "' WHERE char_id='" + charID + "'";
        MySqlDataReader data = command.ExecuteReader();
        data.Read();
        data.Close();
        data = null;
    }

    static void GetSingleCharacter(MySqlConnection connection, int charID)
    {
        MySqlCommand command = connection.CreateCommand();
        command.CommandText = "SELECT char_name,char_lvl,char_exp,char_gender,char_hair_mesh," +
                              "char_hair_color,char_eyes_mesh,char_eyes_color FROM " +
                              "char_personalization WHERE char_id ='" + charID + "'";
        MySqlDataReader data = command.ExecuteReader();

        while (data.Read())
        {
            _charName = (string)data["char_name"];
            _charLvl = (int)data["char_lvl"];
            _charEXp = (float)data["char_exp"];
            _gender = (int)data["char_gender"];
            _hairMesh = (int)data["char_hair_mesh"];
            _hairColor = (int)data["char_hair_color"];
            _eyesMesh = (int)data["char_eyes_mesh"];
            _eyesColor = (int)data["char_eyes_color"];
        }

        data.Close();
        data = null;
    }

    //"SELECT * from users where user_name like 'Adam' AND password like '123456'", sqlConnection)
    //DELETE FROM `char_personalization` WHERE `char_personalization`.`char_id` = 10;
    public static bool VerifyData(MySqlConnection connection, int charID, string charName)
    {
        MySqlCommand command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM char_personalization WHERE char_id like '" + charID + "' AND char_name like'" + charName + "'";
        MySqlDataReader data = command.ExecuteReader();

        while (data.Read())
        {
            if ((int)data["char_id"] == charID && (string)data["char_name"] == charName)
            {
                Debug.Log("Already existe an id" + charID + " or a name in db" + charName);
                return true;
            }
        }
        data.Close();
        data = null;
        return false;
    }

    public static void DeleteCharacter(MySqlConnection connection, int charCount, int owner)
    { }
    #endregion

    #region Character Stats Info
    public static void GetStatusForCharacter(MySqlConnection connection, int charID)
    {
        MySqlCommand command = connection.CreateCommand();
        command.CommandText = "SELECT char_lvlup,stamina,constitution,atackpower," +
                              "defencerate,wisdow,endurence,agility,strength,intelect,spellpower,spiritpower," +
                              "hast,dexterity FROM " +
                              "char_stats WHERE char_id ='" + charID + "'";

        MySqlDataReader data = command.ExecuteReader();

        while (data.Read())
        {
            charlvlUp = (int)data["char_lvlup"];
            _status[0] = (int)data["stamina"];
            _status[1] = (int)data["constitution"];
            _status[2] = (int)data["atackpower"];
            _status[3] = (int)data["defencerate"];
            _status[4] = (int)data["wisdow"];
            _status[5] = (int)data["endurence"];
            _status[6] = (int)data["agility"];
            _status[7] = (int)data["strength"];
            _status[8] = (int)data["intelect"];
            _status[9] = (int)data["spellpower"];
            _status[10] = (int)data["spiritpower"];
            _status[11] = (int)data["hast"];
            _status[12] = (int)data["dexterity"];
        }

        data.Close();
        data = null;
    }

    public static void UpdateStatusForCharacter(MySqlConnection connection, int charId, int chlvlup, int sta, int cons,
                                                int atk, int def, int wis, int end,
                                                int agi, int str, int intel, int spow, int shell, int hast, int dex)
    {
        MySqlCommand command = connection.CreateCommand();
        command.CommandText = "UPDATE char_stats SET char_lvlup='" + chlvlup + "',stamina='" + sta + "',constitution='" + cons +
                              "',atackpower='" + atk + "',defencerate='" + def + "',wisdow='" + wis + "',endurence='" + end +
                              "',agility='" + agi + "',strength='" + str + "',intelect='" + intel + "',spellpower='" + spow +
                              "',spiritpower='" + shell + "',hast='" + hast + "',dexterity='" + dex + "' WHERE char_id='" + charId + "'";

        MySqlDataReader data = command.ExecuteReader();
        data.Read();
        data.Close();
        data = null;
    }

    public static void SendStatusForCharacter(MySqlConnection connection, int charId, int chlvlup, int sta, int cons, int atk,
                                             int def, int wis, int end, int agi, int str, int intel, int spow, int shell,
                                             int hast, int dex)
    {
        MySqlCommand command = connection.CreateCommand();
        command.CommandText = "INSERT INTO char_stats(char_id,char_lvlup,stamina,constitution,atackpower," +
                              "defencerate,wisdow,endurence,agility,strength,intelect,spellpower,spiritpower," +
                              "hast,dexterity) VALUES('" + charId + "','" + chlvlup + "','" + sta + "','" + cons +
                              "','" + atk + "','" + def + "','" + wis + "','" + end + "','" + agi + "','" + str +
                              "','" + intel + "','" + spow + "','" + shell + "','" + hast + "','" + dex + "')";

        MySqlDataReader data = command.ExecuteReader();
        data.Read();
        data.Close();
        data = null;
    }
    #endregion

    #region Character Itens
    public static void GetItensForCharacter(MySqlConnection connection, int charID)
    {
        MySqlCommand command = connection.CreateCommand();
        command.CommandText = "SELECT char_id, item_chest, item_hands, item_WeaponMain, item_Legs, item_Boots,"+
                              "item_WeaponOff FROM Equipped_Items WHERE char_id ='"+ charID + "'";

        MySqlDataReader data = command.ExecuteReader();

        while (data.Read())
        {
            _itensEquiped[0] = (int)data["item_Boots"];
            _itensEquiped[1] = (int)data["item_chest"];
            _itensEquiped[2] = (int)data["item_hands"];
            _itensEquiped[3] = (int)data["item_Legs"];
            _itensEquiped[4] = (int)data["item_WeaponMain"];
            _itensEquiped[5] = (int)data["item_WeaponOff"];
        }

        data.Close();
        data = null;
    }

    public static void UpdateItensForCharacter(MySqlConnection connection, int charID,int chest,int hands,
                                             int mainWeapon, int legs,int boots,int offHand)
    {
        MySqlCommand command = connection.CreateCommand();
        command.CommandText = "UPDATE Equipped_Items SET item_chest='"+ chest + "', item_hands='" + hands + "', item_WeaponMain='" + mainWeapon +
                              "', item_Legs='" + legs + "', item_Boots='" + boots + "'," +
                              "item_WeaponOff='" + offHand + "' WHERE char_id ='" + charID + "'";

        MySqlDataReader data = command.ExecuteReader();
        data.Read();
        data.Close();
        data = null;
    }
    #endregion
}
