using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PilotData
{
    public string Name;
    public string hashPass;
    public int BestScore;
    public int Credits;
    public int MaxDistance;

    #region Options

    public float musicVolume;
    public float SoundVolume;
    public int dificult;

    #endregion
}
