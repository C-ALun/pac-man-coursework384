using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int level;
    public int score;
    public int lives;
    public PointInTime pacmanData;
    public List<PointInTime> ghostData;
    public List<bool> pelletData;
    
}

