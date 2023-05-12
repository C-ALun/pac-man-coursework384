using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [Header("Scripts")]
    [SerializeField]
    private UserProfileManager userProfileManager;

    [Header("Blinky")]
    [SerializeField]
    private GameObject blinkyIcon;
    [SerializeField]
    private Text blinkyName;

    [Header("Pinky")]
    [SerializeField]
    private GameObject pinkyIcon;
    [SerializeField]
    private Text pinkyName;

    [Header("Inky")]
    [SerializeField]
    private GameObject inkyIcon;
    [SerializeField]
    private Text inkyName;

    [Header("Clyde")]
    [SerializeField]
    private GameObject clydeIcon;
    [SerializeField]
    private Text clydeName;


    [Header("Pellets")]
    [SerializeField]
    private GameObject tenPts;
    [SerializeField]
    private GameObject fiftyPts;

    [Header("Timing Variables")]
    [SerializeField]
    private float ghostNameDelay;
    [SerializeField]
    private float ghostIconDelay;

    [SerializeField]
    private Text startKeyText;

    [SerializeField]
    private Text achievementsPage;

    [SerializeField]
    private Text profilePageText;

    [SerializeField]
    private Text highestScoreText;

    [SerializeField]
    private Text quitText;

    [SerializeField]
    private Animator crossFade;

    [SerializeField]
    private Animator musicCrossFade;


    private void Start() {
        SetupStats();
        ClearUI();
        StartCoroutine(LoadUI());
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.M))
        {
           StartCoroutine(GoToAchievementsScene());
        }

        if (Input.GetKeyUp(KeyCode.P)) {
            StartCoroutine(GoToProfileScene());
        }

        if (Input.GetKeyDown(KeyCode.Space)) {
            StartCoroutine(StartGame());
        }

        if (Input.GetKeyUp(KeyCode.Q)) {
            Application.Quit();
        }

    }


    private IEnumerator GoToProfileScene()
    {
        crossFade.SetTrigger("Start");
        musicCrossFade.SetTrigger("Start");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("ProfileScene");
    }

    private IEnumerator GoToAchievementsScene()
    {
        crossFade.SetTrigger("Start");
        musicCrossFade.SetTrigger("Start");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("AchievementsScene");
    }

    private IEnumerator StartGame() {
        crossFade.SetTrigger("Start");
        musicCrossFade.SetTrigger("Start");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("GameScene");
    }

    private IEnumerator LoadUI() {
        yield return new WaitForSeconds(1f);

        blinkyIcon.SetActive(true);
        yield return new WaitForSeconds(ghostIconDelay);

        blinkyName.enabled = true;
        yield return new WaitForSeconds(ghostNameDelay);

        pinkyIcon.SetActive(true);
        yield return new WaitForSeconds(ghostIconDelay);

        pinkyName.enabled = true;
        yield return new WaitForSeconds(ghostNameDelay);

        inkyIcon.SetActive(true);
        yield return new WaitForSeconds(ghostIconDelay);

        inkyName.enabled = true;
        yield return new WaitForSeconds(ghostNameDelay);

        clydeIcon.SetActive(true);
        yield return new WaitForSeconds(ghostIconDelay);

        clydeName.enabled = true;
        yield return new WaitForSeconds(ghostNameDelay);

        tenPts.SetActive(true);
        yield return new WaitForSeconds(1f);
        fiftyPts.SetActive(true);
        yield return new WaitForSeconds(1f);

        profilePageText.enabled = true;
        yield return new WaitForSeconds(1f);

        achievementsPage.enabled = true;
        yield return new WaitForSeconds(1f);

        quitText.enabled = true;
        yield return new WaitForSeconds(1f);

        startKeyText.enabled = true;
        StartCoroutine(FadeTextToPartialAlpha(1.5f, startKeyText));
    }

    private void ClearUI()
    {
        blinkyIcon.SetActive(false);
        blinkyName.enabled = false;

        pinkyIcon.SetActive(false);
        pinkyName.enabled = false;

        inkyIcon.SetActive(false);
        inkyName.enabled = false;

        clydeIcon.SetActive(false);
        clydeName.enabled = false;

        tenPts.SetActive(false);
        fiftyPts.SetActive(false);

        startKeyText.enabled = false;
        achievementsPage.enabled = false;
        quitText.enabled = false;
        profilePageText.enabled = false;
        
    }

    public IEnumerator FadeTextToFullAlpha(float t, Text i) // https://forum.unity.com/threads/fading-in-out-gui-text-with-c-solved.380822/
    {
        i.color = new Color(i.color.r, i.color.g, i.color.b, 0.1f);
        while (i.color.a < 1.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a + (Time.deltaTime / t));
            yield return null;
        }
        StartCoroutine(FadeTextToPartialAlpha(1.5f, startKeyText));
    }

    public IEnumerator FadeTextToPartialAlpha(float t, Text i) // https://forum.unity.com/threads/fading-in-out-gui-text-with-c-solved.380822/
    {
        i.color = new Color(i.color.r, i.color.g, i.color.b, 1);
        while (i.color.a > 0.1f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a - (Time.deltaTime / t));
            yield return null;
        }
        StartCoroutine(FadeTextToFullAlpha(1.5f, startKeyText));
    }

    private void SetupStats()
    {
        UserProfile selectedProfile = userProfileManager.GetSelectedProfile();
        if (selectedProfile != null) {
            highestScoreText.text = selectedProfile.HighestScore.ToString() + " L" + selectedProfile.HighestLevel.ToString();
        }
    }
}
