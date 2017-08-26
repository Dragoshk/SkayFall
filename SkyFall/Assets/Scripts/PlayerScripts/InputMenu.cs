using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class InputMenu : MonoBehaviour
{
    [Header("Proprietis")]
    public Player_Controler pc;
    public GameObject menuPanel;
    public GameObject inputs;
    public GameObject settings;
    
    [Header("Inputs")]
    public Button forward;
    public Button backward;
    public Button strafeLeft;
    public Button strafeRight;
    public Button RotateLeft;
    public Button RotateRight;
    public Button jump;
    public Button Target;
    public Button Rum;
    public Button Shild;
    public Button DranWeaponKey;

    Event keyEvent;
    Text buttonText;
    KeyCode newKey;

    bool waitingForaKey;

	// Use this for initialization
	void Start ()
    {
        menuPanel.gameObject.SetActive(false);
        waitingForaKey = false;

        forward.GetComponentInChildren<Text>().text = pc.Forward.ToString();
        backward.GetComponentInChildren<Text>().text = pc.Backward.ToString();
        strafeLeft.GetComponentInChildren<Text>().text = pc.StrafeLeft.ToString();
        strafeRight.GetComponentInChildren<Text>().text = pc.StrafeRight.ToString();
        RotateLeft.GetComponentInChildren<Text>().text = pc.RotateLeft.ToString();
        RotateRight.GetComponentInChildren<Text>().text = pc.RotateRigth.ToString();
        jump.GetComponentInChildren<Text>().text = pc.Jump.ToString();
        DranWeaponKey.GetComponentInChildren<Text>().text = pc.DranWeaponKey.ToString();
        Target.GetComponentInChildren<Text>().text = pc.Target.ToString();
        Shild.GetComponentInChildren<Text>().text = pc.Shild.ToString();
        Rum.GetComponentInChildren<Text>().text = pc.Rum.ToString();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !menuPanel.activeSelf)
        {
            menuPanel.SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && menuPanel.activeSelf)
            menuPanel.SetActive(false);
    }

    #region Inputs
    public void ApplyButtonClick()
    {
        inputs.SetActive(false);
        menuPanel.SetActive(true);
    }
    void OnGUI()
    {
        keyEvent = Event.current;

        if (keyEvent.isKey && waitingForaKey)
        {
            newKey = keyEvent.keyCode;
            waitingForaKey = false;
        }
    }

    public void StarAssignment(string keyName)
    {
        if (!waitingForaKey)
        {
            StartCoroutine(AssignKey(keyName));
        }
    }

    public void SendText(Text text)
    {
        buttonText = text;
    }

    IEnumerator WaitForKey()
    {
        while (!keyEvent.isKey)
        {
            yield return null;
        }
    }

    public IEnumerator AssignKey(string keyName)
    {
        waitingForaKey = true;

        yield return WaitForKey();

        switch (keyName)
        {
            case "Forward":
                pc.Forward = newKey;
                buttonText.text = pc.Forward.ToString();
                PlayerPrefs.SetString("forwardKey", pc.Forward.ToString());
                break;

            case "Backward":
                pc.Backward = newKey;
                buttonText.text = pc.Backward.ToString();
                PlayerPrefs.SetString("backwardKey", pc.Backward.ToString());
                break;

            case "strafeLeftKey":
                pc.StrafeLeft = newKey;
                buttonText.text = pc.StrafeLeft.ToString();
                PlayerPrefs.SetString("strafeLeftKey", pc.StrafeLeft.ToString());
                break;

            case "starfeRightKey":
                pc.StrafeRight = newKey;
                buttonText.text = pc.StrafeRight.ToString();
                PlayerPrefs.SetString("starfeRightKey", pc.StrafeRight.ToString());
                break;
            case "rotateLeft":
                pc.RotateLeft = newKey;
                buttonText.text = pc.RotateLeft.ToString();
                PlayerPrefs.SetString("rotateLeft", pc.RotateLeft.ToString());
                break;

            case "rotateRigth":
                pc.RotateRigth = newKey;
                buttonText.text = pc.RotateRigth.ToString();
                PlayerPrefs.SetString("rotateRigth", pc.RotateRigth.ToString());
                break;

            case "Target":
                pc.Target = newKey;
                buttonText.text = pc.Target.ToString();
                PlayerPrefs.SetString("target", pc.Target.ToString());
                break;

            case "Jump":
                pc.Jump = newKey;
                buttonText.text = pc.Jump.ToString();
                PlayerPrefs.SetString("jumpKey", pc.Jump.ToString());
                break;

            case "Rum":
                pc.Rum = newKey;
                buttonText.text = pc.Rum.ToString();
                PlayerPrefs.SetString("Rum", pc.Rum.ToString());
                break;

            case "Shild":
                pc.Shild = newKey;
                buttonText.text = pc.Shild.ToString();
                PlayerPrefs.SetString("Shild", pc.Shild.ToString());
                break;

            case "dranWeapon":
                pc.DranWeaponKey = newKey;
                buttonText.text = pc.DranWeaponKey.ToString();
                PlayerPrefs.SetString("dranWeapon", pc.DranWeaponKey.ToString());
                break;
        }

        yield return null;
    }
    #endregion

    #region UI
    public void OptionsPanel()
    {
        settings.SetActive(true);
        menuPanel.SetActive(false);
    }
    public void InputPanel()
    {
        inputs.SetActive(true);
        menuPanel.SetActive(false);
    }
    #endregion
}
