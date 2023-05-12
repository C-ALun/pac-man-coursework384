using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UserProfileButton : MonoBehaviour
{
    [SerializeField] private Text profileNameText;

    [SerializeField] private Text HighestScoreText;
    [SerializeField] private Text HighestLevelText;
    [SerializeField] private Text AchievementCountText;

    [SerializeField] private Button deleteButton;
    [SerializeField] private Button selectButton;

    private UserProfile userProfile;
    private ProfileMenuUI profileMenuUI;


    private void Awake()
    {
        deleteButton.onClick.AddListener(DeleteProfile);
        selectButton.onClick.AddListener(SelectProfile);
    }

    public void Initialize(ProfileMenuUI profileMenuUI, UserProfile userProfile)
    {
        this.userProfile = userProfile;
        this.profileMenuUI = profileMenuUI;
        profileNameText.text = userProfile.ProfileName;

        HighestLevelText.text = "HighestLevel: " + userProfile.HighestLevel.ToString();
        HighestScoreText.text = "HighestScore: " + userProfile.HighestScore.ToString();
        AchievementCountText.text = "AchievementCount: " + userProfile.UnlockedAchievementIds.Count.ToString();
    }

    private void DeleteProfile()
    {
        profileMenuUI.RemoveProfile(userProfile);
    }

    private void SelectProfile()
    {
        PlayerPrefs.SetString("SelectedProfile", userProfile.ProfileName);
        PlayerPrefs.Save();
        StartCoroutine(LoadMenuScene());
    }

    private IEnumerator LoadMenuScene() {
        profileMenuUI.crossFade.SetTrigger("Start");
        profileMenuUI.musicCrossFade.SetTrigger("Start");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("MenuScene");
    }
}
