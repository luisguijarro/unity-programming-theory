using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class MainGameManager : MonoBehaviour
{
    public static MainGameManager Instance;
    private string activePilot;
    internal bool appStarted = false;

    private Dictionary<string, PilotData> pilotsList; // Key PilotName

    private float musicVolume;
    private float soundVolume;
    //private int dificult = 1; //Easy by default.
    
    // Start is called before the first frame update
    void Start()
    {

    }

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        if (Instance != null)
        {
            Destroy(this.gameObject);
            return;
        }

        Instance = this;
        this.activePilot = "";
        LoadData();
        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LaunchGame()
    {
        SceneManager.LoadScene(1, LoadSceneMode.Single); // Load MainScene.
    }

    public void SaveData()
    {
        PilotData[] pilotsData = new PilotData[this.pilotsList.Count];

        int counter = 0;
        foreach (PilotData pd in this.pilotsList.Values)
        {
            pilotsData[counter] = pd;
            counter++;
        }

        AllGameData dataObject = new AllGameData(pilotsData);
        
        string json = JsonUtility.ToJson(dataObject);
        File.WriteAllText(Application.persistentDataPath + "/saveddatafile.json", json);
        Debug.Log("Data Saved.");
    }

    public void LoadData()
    {
        this.pilotsList = new Dictionary<string, PilotData>();
        string path = Application.persistentDataPath + "/saveddatafile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            AllGameData dataObject = JsonUtility.FromJson<AllGameData>(json);

            PilotData[] pilotsData = dataObject.pilotsData;

            this.pilotsList = new Dictionary<string, PilotData>();
            for (int i=0; i<pilotsData.Length;i++)
            {
                this.pilotsList.Add(pilotsData[i].Name, pilotsData[i]);
            }
            Debug.Log("Data Loaded.");
        }
    }

    public bool ExistPilot(string pilot)
    {
        return this.pilotsList.ContainsKey(pilot);
    }

    public bool CheckPassword(string user, string pass)
    {
        //return true; // MORE CODE....
        if (this.pilotsList.ContainsKey(user))
        {
            //Hash128 userHash = new Hash128();
            PilotAccessData pad = new PilotAccessData(user, pass);
            //UnityEngine.HashUtilities.ComputeHash128<PilotAccessData>(ref pad, ref userHash);
            Debug.Log("UserHash: " + pad.GetHashCode().ToString()); //userHash.ToString());

            return /*userHash*/pad.GetHashCode().ToString() == (this.pilotsList[user].hashPass);
        }
        return false;
    }

    public void Access(string pilotname, string pilotpass)
    {
        // LoadPilot...
        if (CheckPassword(pilotname, pilotpass))
        {
            this.activePilot = pilotname; //this.pilotsList[pilotname];
        }
    }

    public void CreatePilot(string pilotname, string pilotpass)
    {
        //Hash128 userHash = new Hash128();
        PilotAccessData pad = new PilotAccessData(pilotname, pilotpass);
        //UnityEngine.HashUtilities.ComputeHash128<PilotAccessData>(ref pad, ref userHash);
        Debug.Log("UserHash: " + pad.GetHashCode().ToString()); //userHash.ToString());

        PilotData pd = new PilotData();
        pd.BestScore = 0;
        pd.Credits = 0;
        pd.dificult = 1; // Easy by Default.
        pd.musicVolume = 1f;
        pd.SoundVolume = 1f;
        pd.Name = pilotname;
        pd.hashPass = pad.GetHashCode().ToString();

        if (this.pilotsList == null) { this.pilotsList = new Dictionary<string, PilotData>(); }
        this.pilotsList.Add(pilotname, pd);

        this.activePilot = pilotname; //this.pilotsList[pilotname];

        SaveData();
    }

    public void GameOver(int score, int distance, int credits)
    {
        // Update Values
        if (pilotsList[activePilot].BestScore < score)
        {
            pilotsList[activePilot].BestScore = score;
        }
        if (pilotsList[activePilot].MaxDistance < distance)
        {
            pilotsList[activePilot].MaxDistance = distance;
        }
        pilotsList[activePilot].Credits = credits;
        SaveData();
    }

    #region PROPERTIES:

    public string PilotName
    {
        get 
        { 
            if (this.pilotsList.ContainsKey(activePilot))
            {
                return this.pilotsList[activePilot].Name; 
            }
            return "";
        }
    }

    public int PilotCredits
    {
        get 
        { 
            if (this.pilotsList.ContainsKey(activePilot))
            {
                return this.pilotsList[activePilot].Credits; 
            }
            return 0;
        }
    }

    public int PilotScore
    {
        get 
        { 
            if (this.pilotsList.ContainsKey(activePilot))
            {
                return this.pilotsList[activePilot].BestScore; 
            }
            return 0;
        }
    }

    public int PilotMaxDistance
    {
        get 
        { 
            if (this.pilotsList.ContainsKey(activePilot))
            {
                return this.pilotsList[activePilot].MaxDistance; 
            }
            return 0;
        }
    }

    public float MusicVolume 
    {
        set { this.musicVolume = value; }
        get { return this.musicVolume; }
    }

    public float SoundVolume 
    {
        set { this.soundVolume = value; }
        get { return this.soundVolume; }
    }

    public float Dificult // we define as float to use with slider
    {
        set { this.pilotsList[this.activePilot].dificult = (int)value; }
        get { return (int)this.pilotsList[this.activePilot].dificult; }
    }

    #endregion
}

public struct PilotAccessData
{
    string user;
    string pass;
    public PilotAccessData(string username, string password)
    {
        this.user = username;
        this.pass = password;
    }
}
