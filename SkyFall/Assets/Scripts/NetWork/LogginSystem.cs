using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Collections;
using MySql.Data.MySqlClient;
using System;
using UnityEngine.SceneManagement;

public class LogginSystem : MonoBehaviour
{
    #region Public Variables
    [Header("SetUp")]
    public GameObject Client;
    public GameObject Login;
    public GameObject CreatAccount;
    public GameObject OptionsMenu;

    [Header("Login")]
    public Text User;
    public InputField Password;
    public int requesterID;

    [Header("Creat Account")]
    public Text UserAC;
    public Text PasswordAC;
    public Text PasswordConfirmeAC;
    public Text Email;
    #endregion

    #region Private Variables
    private MySqlConnection _connection;
    private string _source;
    private string _user = "";
    private string _email = "";
    private string _password = "";
    private string _passwordConfirm = "";
    private bool _startLogging;
    private int _playerCount;
    #endregion

    void Start()
    {
        _source = "Server = mysql.guaragames.com;" +
                  "Database = guaragamescomdrag;" +
                  "User ID = guaragamesdrag;" +
                  "Pooling = false;" +
                  "Password=Dragos*";


        //Invoke("ConectBd",10);
        ConectBd(_source);
    }

    #region UI
    //Client UI
    public void LoginButton()
    {
        _startLogging = GetUserFromBD(_connection, User.text, Password.text);
        if (_startLogging)
        {
            _connection.Close();
            SceneManager.LoadScene("Character Generator");
            //_connection.Dispose();
        }
    }

    public void GoToCreat()
    {
        Login.gameObject.SetActive(false);
        CreatAccount.gameObject.SetActive(true);
    }

    public void BackToLogin()
    {
        CreatAccount.gameObject.SetActive(false);
        Login.gameObject.SetActive(true);
    }

    public void CreatAccounButton()
    {
        _user = UserAC.text;
        _email = Email.text;
        _password = PasswordAC.text;
        _passwordConfirm = PasswordConfirmeAC.text;

        if (_passwordConfirm == _password)
        {
            SendToBd(_connection, _user, _email, _password);
            print("Cadastrado como: " + _user + " " + "Senha = " + _password + "," + "Contra senha" + _passwordConfirm);
            Debug.Log("Account successfully created");
        }
        else
        {
            Debug.Log("One or more entries are wrong !");
            _user = "";
            _email = "";
            _password = "";
            _passwordConfirm = "";
            UserAC.text = _user;
            Email.text = _email;
            PasswordAC.text = _password;
            PasswordConfirmeAC.text = _passwordConfirm;
        }
    }

    public void OptionsPanell()
    {
        OptionsMenu.SetActive(true);
    }
    #endregion

    #region SQl
    //Bd conection Code
    void ConectBd(string source)
    {
        try
        {
            _connection = new MySqlConnection(source);
            _connection.Open();
            Debug.Log("Connected!!");
        }
        catch (Exception e)
        {
            Debug.Log(e.Message.ToString());
        }
    }

    void SendToBd(MySqlConnection connection, string user, string email, string password)
    {
        MySqlCommand command = connection.CreateCommand();
        command.CommandText = "INSERT INTO login(user,Email,password) VALUES('" + user + "','" + email + "','" + password + "')";
        MySqlDataReader data = command.ExecuteReader();
        data.Read();
        data.Close();
        data = null;
    }

    bool GetUserFromBD(MySqlConnection connection, string user, string password)
    {
        MySqlCommand command = connection.CreateCommand();
        command.CommandText = "SELECT user,password FROM login WHERE user ='" + user + "'" + "AND password ='" + password + "'";
        MySqlDataReader data = command.ExecuteReader();

        while (data.Read())
        {
            if ((string)data["user"] == user)
            {
                if ((string)data["password"] == password)
                {
                    //Debug.Log("user: "+user+" - "+ "Password: " + password);
                    GameSettings2.TempUser(user);
                    GameSettings2.TempPassword(password);
                    //GameSettings2.TempEmail(_email);
                    return true;
                }
                return false;
            }
        }
        data.Close();
        data = null;
        return false;
    }
    #endregion

    void OnApplicationQuit()
    {
        _connection.Close();
    }
}
