using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public const float VERSION = .1f;
    public bool clearPreff = false;

    private string _levelToLoad = "";
    private string _characterGeneration = GameSettings2.levelNames[1];
    private string _firstLevel = GameSettings2.levelNames[3];
    private bool _hasCharacter = false;
    private float _percentLoaded = 0;

	// Use this for initialization
	void Start ()
    {
        if (clearPreff)
            PlayerPrefs.DeleteAll();

        if (PlayerPrefs.HasKey("ver"))
        {
            //Debug.Log("ther is a ver key");
            if (PlayerPrefs.GetFloat("ver") != VERSION)
            {
                //Debug.Log("Save version is not the same as current");
                //Upgrade player praff here
            }
            else
            {
                //Debug.Log("Save version is the same as current version");
                if (PlayerPrefs.HasKey("PlayerName"))
                {
                    //Debug.Log("There is a Player Name tag");
                    if (PlayerPrefs.GetString("PlayerName") == "")
                    {
                        //Debug.Log("The Player name key does not have anything in it");
                        PlayerPrefs.DeleteAll();
                        PlayerPrefs.SetFloat("ver", VERSION);
                        _levelToLoad = _characterGeneration;
                    }
                    else
                    {
                        //Debug.Log("The Player name key has a value");
                        _hasCharacter = true;
                        //_levelToLoad = _firstLevel;
                    }
                }
                else
                {
                    //Debug.Log("There is no Player Name key");
                }
            }
        }
        else
        {
            //Debug.Log("ther is no ver key");
            PlayerPrefs.DeleteAll();
            PlayerPrefs.SetFloat("ver", VERSION);
            _levelToLoad = _characterGeneration;
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (_levelToLoad == "")
        {
            return;
        }

        if (Application.GetStreamProgressForLevel(_levelToLoad) == 1)
        {
            //Debug.Log("Level Ready");
            _percentLoaded = 1;
        }

        if (Application.CanStreamedLevelBeLoaded(_levelToLoad))
        {
            SceneManager.LoadScene(_levelToLoad);
        }
        else
        {
            _percentLoaded = Application.GetStreamProgressForLevel(_levelToLoad);
        }
    }

    void OnGUI()
    {
        if (_hasCharacter)
        {
            if (GUI.Button(new Rect(10, 10, 110, 25), "Load Character"))
            {
                _levelToLoad = _firstLevel;
            }

            if (GUI.Button(new Rect(10, 40, 110, 25), "Delete Character"))
            {
                PlayerPrefs.DeleteAll();
                PlayerPrefs.SetFloat("ver", VERSION);
                _levelToLoad = _characterGeneration;
            }
        }

        if (_levelToLoad == "")
        {
            return;
        }
        else
        {
            GUI.Label(new Rect(Screen.width/2 - 50, Screen.height - 45, 100, 25), (_percentLoaded * 100) + "%");
            GUI.Box(new Rect(0,Screen.height - 20, Screen.width * _percentLoaded,15), "");
        }
    }
}
