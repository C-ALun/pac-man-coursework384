using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class EventQueue
{
    private Queue<Achievement> eventQueue = new Queue<Achievement>();
    public AchievementDisplay achievementDisplay;

    private bool isProcessing = false;

    public EventQueue(AchievementDisplay display)
    {
        achievementDisplay = display;
    }

    public void EnqueueEvent(Achievement achievement)
    {
        eventQueue.Enqueue(achievement);
    }

    public IEnumerator ProcessEvents() {
        if (isProcessing)
        {
            yield break;
        }

        isProcessing = true;

        while(eventQueue.Count > 0) {
            Achievement achievement = eventQueue.Dequeue();
            
                // Wait for the display and fade-out durations before processing the next event
            yield return achievementDisplay.StartCoroutine(achievementDisplay.ShowAchievement(achievement.Name)); 
        }

        isProcessing = false;
    }
}
