using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProfileMenuUI : MonoBehaviour
{
    [SerializeField] private UserProfileManager userProfileManager;
    [SerializeField] private GameObject userProfileButtonPrefab;
    [SerializeField] private Transform userProfileButtonParent;
    [SerializeField] private InputField createProfileInputField;

    private List<UserProfileButton> userProfileButtons = new List<UserProfileButton>();

    public Animator crossFade;
    public Animator musicCrossFade;

    private void Awake()
    {
        InitializeProfileButtons();
    }

    private void InitializeProfileButtons()
    {
        for (int i = 0; i < userProfileManager.UserProfiles.Count; i++)
        {
            GameObject newButton = Instantiate(userProfileButtonPrefab, userProfileButtonParent);
            UserProfileButton buttonComponent = newButton.GetComponent<UserProfileButton>();
            buttonComponent.Initialize(this, userProfileManager.UserProfiles[i]);
            userProfileButtons.Add(buttonComponent);
        }
    }

    public void CreateProfile()
    {
        string profileName = createProfileInputField.text;
        userProfileManager.AddUserProfile(profileName);
        RefreshProfileButtons();
    }

    public void RemoveProfile(UserProfile userProfile)
    {
        userProfileManager.RemoveUserProfile(userProfile);
        RefreshProfileButtons();
    }

    private void RefreshProfileButtons()
    {
        for (int i = 0; i < userProfileButtons.Count; i++)
        {
            Destroy(userProfileButtons[i].gameObject);
        }
        userProfileButtons.Clear();
        InitializeProfileButtons();
    }
}
