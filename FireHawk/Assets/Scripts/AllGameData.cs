using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AllGameData
{
    public PilotData[] pilotsData;
    public AllGameData(PilotData[] pilotsdata)
    {
        this.pilotsData = pilotsdata;
    }
}
