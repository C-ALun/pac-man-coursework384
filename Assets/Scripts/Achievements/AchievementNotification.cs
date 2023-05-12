using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementNotification : MonoBehaviour
{
    [SerializeField] private AchievementManager achievementManager;
    [SerializeField] private AchievementDisplay achievementDisplay;
    private EventQueue achievementQueue;

    private UserProfileManager userProfileManager;

    public static int numOfAchievementUnlocked {get; private set;} = 0;


    private void Awake()
    {
        achievementManager = GetComponent<AchievementManager>();
        achievementDisplay = GetComponent<AchievementDisplay>();

        userProfileManager = FindObjectOfType<UserProfileManager>();
        achievementQueue = new EventQueue(achievementDisplay);
    }

    public void EnqueueAchievement(UserProfile userProfile, Achievement achievement)
    {
        achievement.Unlock(userProfile);
        userProfileManager.SaveUserProfiles();
        numOfAchievementUnlocked++;
        achievementQueue.EnqueueEvent(achievement);
        ProcessAchievements();
    }

    public void ProcessAchievements()
    {
        StartCoroutine(achievementQueue.ProcessEvents());
    }

    public void TriggerScoreAchievement(UserProfile userProfile, int score)
    {
         Dictionary<string, int> scoreAchievements = new Dictionary<string, int>
        {
            {"1000 Score", 1000},
            {"10000 Score", 10000},
            {"20000 Score", 20000},
            {"30000 Score", 30000}
        };

        foreach (KeyValuePair<string, int> achievementPair in scoreAchievements)
        {
            if (score >= achievementPair.Value)
            {
                Achievement achievement = achievementManager.Achievements.Find(a => a.Name == achievementPair.Key);
                if (!userProfile.UnlockedAchievementIds.Contains(achievement.Id))
                {
                    EnqueueAchievement(userProfile, achievement);
                }
            }
        }
    }

    public void TriggerGhostAchievement(UserProfile userProfile, int ghostsEaten)
    {
        Dictionary<string, int> ghostAchievements = new Dictionary<string, int>
        {
            {"Eaten 1 Ghost", 1},
            {"Eaten 5 Ghosts", 5},
            {"Eaten 10 Ghosts", 10}
        };

        foreach (KeyValuePair<string, int> achievementPair in ghostAchievements)
        {
            if (ghostsEaten >= achievementPair.Value)
            {
                Achievement achievement = achievementManager.Achievements.Find(a => a.Name == achievementPair.Key);
                if (!userProfile.UnlockedAchievementIds.Contains(achievement.Id))
                {
                    EnqueueAchievement(userProfile, achievement);
                }
            }
        }
    }

    public void TriggerLevelAchievement(UserProfile userProfile, int level, bool noLifeLost)
    {
        string levelAchievementName = $"Complete Level {level}";
        string noLifeLostAchievementName = $"Complete Level {level} Without Losing a Life";

        Achievement levelAchievement = achievementManager.Achievements.Find(a => a.Name == levelAchievementName);
        Achievement noLifeLostAchievement = achievementManager.Achievements.Find(a => a.Name == noLifeLostAchievementName);

        if (levelAchievement != null && !userProfile.UnlockedAchievementIds.Contains(levelAchievement.Id))
        {
            EnqueueAchievement(userProfile, levelAchievement);
        }

        if (noLifeLostAchievement != null  && !userProfile.UnlockedAchievementIds.Contains(noLifeLostAchievement.Id))
        {
            EnqueueAchievement(userProfile, noLifeLostAchievement);
        }
    }

}
