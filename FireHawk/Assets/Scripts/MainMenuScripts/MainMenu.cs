using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using TMPro;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject mainMenuScreen;
    [SerializeField] private GameObject pilotNameScreen;
    [SerializeField] private TMP_InputField inputPilotName;
    [SerializeField] private GameObject optionsMenuScreen;
    [SerializeField] private TextMeshProUGUI textPilotName;
    [SerializeField] private TextMeshProUGUI textPilotScore;
    [SerializeField] private TextMeshProUGUI textPilotDistance;
    [SerializeField] private TextMeshProUGUI textPilotCredits;
    [SerializeField] private TextMeshProUGUI textDificultValue;
    
    [SerializeField] private Slider sliderMusicVolume;
    [SerializeField] private Slider sliderSoundVolume;
    [SerializeField] private Slider sliderDificult;

    // Start is called before the first frame update
    void Start()
    {
        if (!MainGameManager.Instance.appStarted)
        {
            ShowPilotNameInput();
            MainGameManager.Instance.appStarted = true;
        }
        else
        {
            // pilotNameScreen.SetActive(false);
            this.ShowMainMenu();
        }
        this.optionsMenuScreen.SetActive(false);
        
    }

    private void UpdateOptionSliders()
    {
        sliderMusicVolume.value = MainGameManager.Instance.MusicVolume;
        sliderSoundVolume.value = MainGameManager.Instance.SoundVolume;
        sliderDificult.value = this.Dificult;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // ABSTRACTION
    public void ShowMainMenu()
    {
        mainMenuScreen.SetActive(true);
        optionsMenuScreen.SetActive(false);
        pilotNameScreen.SetActive(false);
        this.UpdatePilotOnUI();
    }

    // ABSTRACTION
    public void ShowPilotNameInput()
    {
        pilotNameScreen.SetActive(true);
        mainMenuScreen.SetActive(false);
        inputPilotName.text = MainGameManager.Instance.PilotName;
        inputPilotName.ActivateInputField();
    }

    // ABSTRACTION
    public void Access(string pilotname, string pilotpass)
    {
        MainGameManager.Instance.Access(pilotname, pilotpass);
        this.UpdatePilotOnUI();
        this.pilotNameScreen.SetActive(false);
        this.mainMenuScreen.SetActive(true);
        // LoadPilot...
    }

    // ABSTRACTION
    private void UpdatePilotOnUI()
    {
        this.textPilotName.text = MainGameManager.Instance.PilotName;
        this.textPilotScore.text = MainGameManager.Instance.PilotScore.ToString();
        this.textPilotDistance.text = "-" + MainGameManager.Instance.PilotMaxDistance.ToString();
        this.textPilotCredits.text = MainGameManager.Instance.PilotCredits.ToString();
    }

    // ABSTRACTION
    public void ShowOptionsMenu()
    {
        optionsMenuScreen.SetActive(true);
        mainMenuScreen.SetActive(false);
        this.UpdateOptionSliders();
    }

    // ABSTRACTION
    public void LaunchGame()
    {
        MainGameManager.Instance.LaunchGame();
    }

    // ABSTRACTION
    public void Exit()
    {
        #if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
        #else
            Application.Quit();
        #endif
    }

    // ENCAPSULATION
    public float Dificult
    {
        set 
        { 
            MainGameManager.Instance.Dificult = value;
            switch(value)
            {
                case 2:
                    textDificultValue.text = "MEDIUM";
                    textDificultValue.color = new Color(1f, 0.5f, 0f); // Orange
                    break;
                case 3:
                    textDificultValue.text = "HARD";
                    textDificultValue.color = Color.red;
                    break;
                default:
                    textDificultValue.text = "EASY";
                    textDificultValue.color = Color.green;
                    break;
            }
        }
        get 
        { 
            switch(MainGameManager.Instance.Dificult) // For first update
            {
                case 2:
                    textDificultValue.text = "MEDIUM";
                    textDificultValue.color = new Color(1f, 0.5f, 0f); // Orange
                    break;
                case 3:
                    textDificultValue.text = "HARD";
                    textDificultValue.color = Color.red;
                    break;
                default:
                    textDificultValue.text = "EASY";
                    textDificultValue.color = Color.green;
                    break;
            }
            return MainGameManager.Instance.Dificult; 
        }
    }
}
