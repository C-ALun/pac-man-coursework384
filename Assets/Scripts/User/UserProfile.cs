using System;
using System.Collections.Generic;

[Serializable]
public class UserProfile
{
    public string ProfileName;
    public int HighestScore;
    public int HighestLevel;
    public List<int> UnlockedAchievementIds;

    public UserProfile(string profileName)
    {
        ProfileName = profileName;
        HighestScore = 0;
        HighestLevel = 0;
        UnlockedAchievementIds = new List<int>();
    }

}
