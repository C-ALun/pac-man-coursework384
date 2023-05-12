using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class AchievementSceneController : MonoBehaviour
{
    [SerializeField] private AchievementManager achievementManager;

    [SerializeField] private GameObject achievementItemPrefab;
    [SerializeField] private Transform achievementListContent;

    private UserProfile selectedProfile;
    private UserProfileManager userProfileManager;

    [SerializeField]
    private Animator musicCrossFade;

    private void Start()
    {
        selectedProfile = userProfileManager.GetSelectedProfile();
    
        DisplayAchievements();
    }

    private void Awake() {
        userProfileManager = FindObjectOfType<UserProfileManager>();
        achievementManager = GetComponent<AchievementManager>();
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.B)) {
            StartCoroutine(ReturnToMenu());
        }
    }

    private IEnumerator ReturnToMenu() {
        musicCrossFade.SetTrigger("Start");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("MenuScene");
    }

    private void DisplayAchievements()
    {
        foreach (Achievement achievement in achievementManager.Achievements)
        {
            GameObject achievementEntry = Instantiate(achievementItemPrefab, achievementListContent);
            Text achievementText = achievementEntry.GetComponent<Text>();

            bool isUnlocked = selectedProfile.UnlockedAchievementIds.Contains(achievement.Id);

            achievementText.text = $"{achievement.Description} {(isUnlocked ? "(Unlocked)" : "")}";
        }
    }
}
