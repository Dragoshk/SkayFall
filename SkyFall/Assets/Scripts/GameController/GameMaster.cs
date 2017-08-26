using UnityEngine;
using System.Collections;

public class GameMaster : MonoBehaviour
{
    public GameObject GameSettings;
    //public Camera mainCamera;

    public float zOffset;
    public float yOffset;
    public float xRotOffset;
    public PlayerCharacter _pcScript;
    public Vector3 _playerSpawnPointpos;

    private GameObject _pc;
    private int _gender;


    void Start()
    {
        PlayerCharacter.Instance.Awake();
        PlayerCharacter.Instance.GetComponent<Player_Controler>().enabled = true;
        PlayerCharacter.Instance.GetComponent<PlayerHUD>().enabled = true;
        PlayerCharacter.Instance.Name = GameSettings2.LoadName();
        GameSettings2.LoadName();
        GameSettings2.LoadAttributes();
        GameSettings2.LoadVitals();
        GameSettings2.LoadSkills();

    }
}
