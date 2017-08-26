using UnityEngine;

public class test : MonoBehaviour
{
    [SerializeField]
    private Stats _health;
    [SerializeField]
    private Stats _mana;
    [SerializeField]
    private Stats _stamina;

    private void Awake()
    {
        _health.Initialize();
        _mana.Initialize();
        _stamina.Initialize();
    }
	
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            _health.CurrentVal -= 10;
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            _health.CurrentVal += 10;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            _mana.CurrentVal -= 10;
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            _mana.CurrentVal += 10;
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            _stamina.CurrentVal -= 10;
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            _stamina.CurrentVal += 10;
        }
    }
}
