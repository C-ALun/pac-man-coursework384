using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class UserProfileManager : MonoBehaviour
{
    [SerializeField] private List<UserProfile> userProfiles;

    public List<UserProfile> UserProfiles => userProfiles;

    private void Awake()
    {
        LoadUserProfiles();
    }

    public void AddUserProfile(string profileName)
    {
        if (string.IsNullOrEmpty(profileName)) return;

        if (userProfiles.Count >= 3) return;

        if (userProfiles.Any(profile => profile.ProfileName == profileName)) return;


        UserProfile newUserProfile = new UserProfile(profileName);
        userProfiles.Add(newUserProfile);
        SaveUserProfiles();
    }

    public void RemoveUserProfile(UserProfile userProfile)
    {
        userProfiles.Remove(userProfile);
        SaveUserProfiles();
    }

    public void SaveUserProfiles()
    {
        PlayerPrefs.SetString("UserProfiles", JsonHelper.ToJson(userProfiles.ToArray(), true));
        PlayerPrefs.Save();
    }

    private void LoadUserProfiles()
    {
        string profilesJson = PlayerPrefs.GetString("UserProfiles");
        if (string.IsNullOrEmpty(profilesJson))
        {
            userProfiles = new List<UserProfile>();
        }
        else
        {
            userProfiles = JsonHelper.FromJson<UserProfile>(profilesJson).ToList();
        }
    }

    public UserProfile GetSelectedProfile()
    {
        string profileName = PlayerPrefs.GetString("SelectedProfile");
        return userProfiles.Find(profile => profile.ProfileName == profileName);
    }
}
