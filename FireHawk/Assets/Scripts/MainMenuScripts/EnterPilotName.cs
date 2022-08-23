using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnterPilotName : MonoBehaviour
{
    [SerializeField] private TMP_InputField enterNameInput;
    [SerializeField] private TMP_InputField enterPassInput;
    [SerializeField] private GameObject errorPanel;
    [SerializeField] private MainMenu mainMenu;
    private MainGameManager mainGameManager;
    // Start is called before the first frame update
    void Start()
    {
        this.mainGameManager = MainGameManager.Instance;
        this.errorPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PilotAccessControl()
    {
        if ((enterNameInput.text.Length > 0) && enterPassInput.text.Length > 0)
        {
            if (this.mainGameManager.ExistPilot(enterNameInput.text.ToLower()))
            {
                if (this.mainGameManager.CheckPassword(enterNameInput.text.ToLower(), enterPassInput.text.ToLower()))
                {
                    mainMenu.Access(enterNameInput.text.ToLower(), enterPassInput.text.ToLower());                    
                }
                else
                {
                    //error
                    this.errorPanel.SetActive(true);
                }
            }
            else
            {
                //Create Pilot....
                this.mainGameManager.CreatePilot(enterNameInput.text.ToLower(), enterPassInput.text.ToLower());
                mainMenu.ShowMainMenu();
            }
        }
    }

    public void ReturnToAccessScreen()
    {
        this.errorPanel.SetActive(false);
    }
}
