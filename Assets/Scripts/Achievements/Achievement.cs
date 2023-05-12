using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Achievement
{
    public int Id { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public bool Unlocked { get; private set; }

    public Achievement(int id, string name, string description)
    {
        Id = id;
        Name = name;
        Description = description;
        Unlocked = false;
    }

    public void Unlock(UserProfile userProfile)
    {
        Unlocked = true;
        //PlayerPrefs.SetInt(Id.ToString(), 1);

        userProfile.UnlockedAchievementIds.Add(Id);
    }
}

