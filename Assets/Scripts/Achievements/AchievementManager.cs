using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class AchievementManager : MonoBehaviour
{
    public List<Achievement> Achievements { get; private set; }

    // public delegate void AchievementsInitializedHandler();
    // public event AchievementsInitializedHandler OnAchievementsInitialized;

    private void Awake()
    {
        InitializeAchievements();
    }

    private void InitializeAchievements()
    {
        Achievements = new List<Achievement>
        {
            new Achievement(1, "Complete Level 1", "Complete the first level"),
            new Achievement(2, "Complete Level 1 Without Losing a Life", "Complete level 1 without Losing a Life"),
            new Achievement(3, "Complete Level 3", "Complete the Third level"),
            new Achievement(4, "Complete Level 3 Without Losing a Life", "Complete level 3 Without Losing a Life"),
            new Achievement(5, "Complete Level 5", "Complete the Fifth level"),
            new Achievement(6, "Complete Level 5 Without Losing a Life", "Complete level 5 Without Losing a Life"),
            new Achievement(7, "1000 Score", "Reach 1000 points"),
            new Achievement(8, "10000 Score", "Reach 10000 points"),
            new Achievement(9, "20000 Score", "Reach 20000 points"),
            new Achievement(10, "30000 Score", "Reach 30000 points"),
            new Achievement(11, "Eaten 1 Ghost", "Eat 1 ghost"),
            new Achievement(12, "Eaten 5 Ghosts", "Eat 5 ghosts"),
            new Achievement(13, "Eaten 10 Ghosts", "Eat 10 ghosts"),
            new Achievement(14, "Eaten 20 Ghosts", "Eat 20 ghosts"),
            new Achievement(15, "Eaten 50 Ghosts", "Eat 50 ghosts"),
            new Achievement(16, "Eaten 100 Ghosts", "Eat 100 ghosts")
            // Add more achievements here
        };

        // if (OnAchievementsInitialized != null)
        // {
        //     OnAchievementsInitialized();
        // }
    }
}
