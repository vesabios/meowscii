using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class PGameProperties : PD {

    public int currentFloor = 1;
    public int deaths = 0;
    public float gameTime = 0;
    public bool canContinue = true;
    public int elapsedSeconds = 0;
    public List<int> floorSeeds = new List<int>();
    public List<bool> floorGenerated = new List<bool>();

}
