using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using TMPro;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject mainMenuScreen;
    [SerializeField] private GameObject pilotNameScreen;
    [SerializeField] private TMP_InputField inputPilotName;
    [SerializeField] private GameObject optionsMenuScreen;
    [SerializeField] private TextMeshProUGUI TextPilotName;
    [SerializeField] private TextMeshProUGUI TextPilotScore;
    [SerializeField] private TextMeshProUGUI TextPilotDistance;
    [SerializeField] private TextMeshProUGUI TextPilotCredits;
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
            pilotNameScreen.SetActive(false);
        }
        this.optionsMenuScreen.SetActive(false);
        this.UpdatePilotOnUI();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowMainMenu()
    {
        mainMenuScreen.SetActive(true);
        optionsMenuScreen.SetActive(false);
        pilotNameScreen.SetActive(false);
        this.UpdatePilotOnUI();
    }

    public void ShowPilotNameInput()
    {
        pilotNameScreen.SetActive(true);
        mainMenuScreen.SetActive(false);
        inputPilotName.text = MainGameManager.Instance.PilotName;
        inputPilotName.ActivateInputField();
    }


    public void Access(string pilotname, string pilotpass)
    {
        MainGameManager.Instance.Access(pilotname, pilotpass);
        this.UpdatePilotOnUI();
        this.pilotNameScreen.SetActive(false);
        this.mainMenuScreen.SetActive(true);
        // LoadPilot...
    }

    private void UpdatePilotOnUI()
    {
        this.TextPilotName.text = MainGameManager.Instance.PilotName;
        this.TextPilotScore.text = MainGameManager.Instance.PilotScore.ToString();
        this.TextPilotDistance.text = "-" + MainGameManager.Instance.PilotMaxDistance.ToString();
        this.TextPilotCredits.text = MainGameManager.Instance.PilotCredits.ToString();
    }


    public void ShowOptionsMenu()
    {
        optionsMenuScreen.SetActive(true);
        mainMenuScreen.SetActive(false);
    }
/*
    public void SetPilotName()
    {
        MainGameManager.Instance.SetPilotName(inputPilotName.text);
        mainMenuScreen.SetActive(true);
        pilotNameScreen.SetActive(false);
    }
*/
    public void LaunchGame()
    {
        MainGameManager.Instance.LaunchGame();
    }

    public void Exit()
    {
        #if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
        #else
            Application.Quit();
        #endif
    }
}
