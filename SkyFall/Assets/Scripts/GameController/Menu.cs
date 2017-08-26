using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class Menu : MonoBehaviour
{
    public GameObject mainMenuHolder;
    public GameObject opitionsMenuHolder;
    public Toggle isFullScreen;

    public Slider[] volumeSliders;
    public Toggle[] resolutionToggles;
    public int[] screenWidths;
    private int _activeScreenIndex;

    void Start()
    {
        mainMenuHolder.SetActive(true);
        opitionsMenuHolder.SetActive(false);
    }

    public void Play()
    {
        SceneManager.LoadScene("Game");
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void OptionsMenu()
    {
        mainMenuHolder.SetActive(false);
        opitionsMenuHolder.SetActive(true);
    }
    public void MainMenu()
    {
        mainMenuHolder.SetActive(true);
        opitionsMenuHolder.SetActive(false);
    }
    public void SetScreenResolution(int i)
    {
        if (resolutionToggles[i].isOn)
        {
            _activeScreenIndex = i;
            float aspectRatio = 16 / 9f;
            Screen.SetResolution(screenWidths[i], (int)(screenWidths[i]/aspectRatio),false);
            PlayerPrefs.SetInt("screen res index",_activeScreenIndex);
            PlayerPrefs.Save();
        }
    }

    public void SetFullScreen()
    {
        for (int i = 0; i < resolutionToggles.Length; i++)
        {
            resolutionToggles[i].interactable = !isFullScreen.isOn;
        }

        if (isFullScreen.isOn)
        {
            Resolution[] allResolution = Screen.resolutions;
            Resolution maxResolution = allResolution[allResolution.Length - 1];
            Screen.SetResolution(maxResolution.width, maxResolution.height, true);
        }
        else
        {
            SetScreenResolution(_activeScreenIndex);
        }

        PlayerPrefs.SetInt("fullscreen", ((isFullScreen.isOn) ? 1:0));
        PlayerPrefs.Save();
    }

    public void SetMasterVolume(float i)
    {

    }

    public void SetMusicVolume(float i)
    {

    }

    public void SetSFxVolume(float i)
    {

    }
}
