using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AchievementDisplay : MonoBehaviour
{
    public Text messageText;

    public IEnumerator ShowAchievement(string achievementName)
    {
        messageText.text = "Achievement Unlocked: " + achievementName;
        messageText.canvasRenderer.SetAlpha(1f);
        yield return new WaitForSeconds(5f);
        messageText.CrossFadeAlpha(0f, 1f, false);
        yield return new WaitForSeconds(1f);
    }
}
