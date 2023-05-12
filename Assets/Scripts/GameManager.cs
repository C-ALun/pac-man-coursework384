using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public Ghost[] ghosts;
    public Pacman pacman;
    public Transform pellets;

    public Text gameOverText;
    public Text pauseText;
    public Text scoreText;
    public Text highestScoreText;
    public Text levelText;
    public Text livesText;
    public Text pacmanBehaviorText;
    public Text countdownText;

    public int ghostMultiplier { get; private set; } = 1;
    public int score { get; private set; }
    public int lives { get; private set; }
    public int level { get; private set; }

    private List<PointInTime> pacManPositions;
    private List<PointInTime>[] ghostPositions;
    private List<bool>[] pelletActiveStates;
    private List<int> scoresList;
    private List<int> livesList;

    private UserProfile selectedProfile;
    private UserProfileManager userProfileManager;

    public bool isRewinding = false;
    private float returnToMenuTime = 2f;
    private bool isPaused = false;

    private bool isGame = false;

    [SerializeField]
    private float startDelayTime;

    //public static int gameId { get; private set; } = 1;

    [SerializeField] private AchievementNotification achievementController;

    public static GameManager instance { get; private set; }


    private void Start()
    {
        
        SetupStats();
        DelayedStart();
        
    }


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        userProfileManager = FindObjectOfType<UserProfileManager>();

        achievementController = GetComponentInChildren<AchievementNotification>();
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)) {
            TogglePause();
        }

        if (lives <= 0 && Input.anyKeyDown) {
            NewGame();
        }

        if (Input.GetKeyDown(KeyCode.Space)) {
            StartRewind();
        }
        if (Input.GetKeyUp(KeyCode.Space)) {
            StopRewind();
        }

        if (Input.GetKeyDown(KeyCode.Q)) {
            SaveGameData();
            SceneManager.LoadScene("MenuScene");
        }
    }

    /*Toggles the paused state of the game and adjusts the game time scale accordingly.*/
    private void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            Time.timeScale = 0f;
            pauseText.enabled = true;
        } else {
            Time.timeScale = 1f;
            pauseText.enabled = false;
        }
    }

    private void FixedUpdate() {
        if (isRewinding) {
            Rewind();
        } else {
            Record();
        }
    }

    /*Sets up the initial game stats and updates the UI to reflect these stats.*/
    private void SetupStats()
    {
        selectedProfile = userProfileManager.GetSelectedProfile();

        SetHighestScoreText();
        SetPacmanBehaviourText(pacman.currentMode);

    }

    /*If possible, it rewinds the state of the game by applying the states stored in the lists*/
    private void Rewind() {
        if (pacManPositions.Count > 0) {
            PointInTime pointInTime = pacManPositions[0];
            pacman.transform.position = pointInTime.position;
            pacman.transform.rotation = pointInTime.rotation;
            pacManPositions.RemoveAt(0);

            for (int i = 0; i < ghosts.Length; i++)
            {
                pointInTime = ghostPositions[i][0];
                ghosts[i].transform.position = pointInTime.position;
                ghosts[i].transform.rotation = pointInTime.rotation;
                ghostPositions[i].RemoveAt(0);
            }

            for (int i = 0; i < pellets.childCount; i++)
            {
                pellets.GetChild(i).gameObject.SetActive(pelletActiveStates[i][0]);
                pelletActiveStates[i].RemoveAt(0);
            }

            SetLives(livesList[0]);
            livesList.RemoveAt(0);

            SetScore(scoresList[0]);
            scoresList.RemoveAt(0);

        } else {
            StopRewind();
        }
    }
    /*Records the state of the game objects and stores them in lists.*/
    private void Record() {
        if (isGame) {
            pacManPositions.Insert(0, new PointInTime(pacman.transform.position, pacman.transform.rotation));

            for (int i = 0; i < ghosts.Length; i++)
            {
                ghostPositions[i].Insert(0, new PointInTime(ghosts[i].transform.position, ghosts[i].transform.rotation));
            }

            for (int i = 0; i < pellets.childCount; i++)
            {
                pelletActiveStates[i].Insert(0, pellets.GetChild(i).gameObject.activeSelf);
            }

            livesList.Insert(0, lives);
            scoresList.Insert(0, score);
        }
    }

    /* Begins the rewind process.*/
    public void StartRewind() {
        isRewinding = true;
        pacman.movement.rigidbody.isKinematic = true;
        for (int i = 0; i < ghosts.Length; i++) {
            ghosts[i].movement.rigidbody.isKinematic = true;
        }
    }

    /* Stops the rewind process.*/
    public void StopRewind() {
        isRewinding = false;
        pacman.movement.rigidbody.isKinematic = false;
        for (int i = 0; i < ghosts.Length; i++) {
            ghosts[i].movement.rigidbody.isKinematic = false;
        }
    }

    /*Resets the score, level, and lives, and starts a new round.*/
    private void NewGame()
    {
        SetScore(0);
        SetLevel(0);
        SetLives(3);
        NewRound();
    }

    /*Sets up a new round of the game.*/
    private void NewRound()
    {
        scoresList = new List<int>();
        livesList = new List<int>();
        ghostPositions = new List<PointInTime>[ghosts.Length];
        for (int i = 0; i < ghostPositions.Length; i++)
        {
            ghostPositions[i] = new List<PointInTime>();
        }
        pacManPositions = new List<PointInTime>();
        pelletActiveStates = new List<bool>[pellets.childCount];
        for (int i = 0; i < pelletActiveStates.Length; i++)
        {
            pelletActiveStates[i] = new List<bool>();
        }
        gameOverText.enabled = false;
        pauseText.enabled = false;
        pacmanBehaviorText.enabled = true;

        SetLevel(level + 1);

        foreach (Transform pellet in pellets) {
            pellet.gameObject.SetActive(true);
        }

        ResetState();
    }

    /*Resets the states of the game objects.*/
    private void ResetState()
    {
        for (int i = 0; i < ghosts.Length; i++) {
            ghosts[i].ResetState();
        }

        pacman.ResetState();

        StartCoroutine(WaitForRealTime(2f));
    }

    /*Ends the game and saves high scores.*/
    private void GameOver()
    {
        gameOverText.enabled = true;

        for (int i = 0; i < ghosts.Length; i++) {
            ghosts[i].gameObject.SetActive(false);
        }

        pacman.gameObject.SetActive(false);

        if (selectedProfile != null)
        {
            if (score > selectedProfile.HighestScore) 
            {
                selectedProfile.HighestScore = score;
            }
            if (level > selectedProfile.HighestLevel)
            {
                selectedProfile.HighestLevel = level;
            }
            userProfileManager.SaveUserProfiles();
        }
        PlayerPrefs.DeleteKey("Score");

        StartCoroutine(ReturnToMenu());
    }

    /*A coroutine that waits for a specific time and then loads the menu scene.*/
    private IEnumerator ReturnToMenu()
    {
        yield return new WaitForSeconds(returnToMenuTime);
        SceneManager.LoadScene("MenuScene");
    }

    /*Updates the lives count and displays it on the UI.*/
    private void SetLives(int lives)
    {
        this.lives = lives;
        livesText.text = "x" + lives.ToString();
    }

    /*Updates the score and displays it on the UI.*/
    private void SetScore(int score)
    {
        this.score = score;
        achievementController.TriggerScoreAchievement(selectedProfile, this.score);

        SetHighestScoreText();
        scoreText.text = score.ToString().PadLeft(2, '0');
    }

    /*Updates the display of the highest score and level.*/
    private void SetHighestScoreText()
    {
        int displayScore = (score > selectedProfile.HighestScore) ? score : selectedProfile.HighestScore;
        int displayLevel = (level > selectedProfile.HighestLevel) ? level : selectedProfile.HighestLevel;

        highestScoreText.text = displayScore.ToString() + " L" + displayLevel.ToString();
    }

    /* Updates the level and displays it on the UI.*/
    private void SetLevel(int level)
    {
        this.level = level;
        achievementController.TriggerLevelAchievement(selectedProfile, level - 1, this.lives == 3);

        SetHighestScoreText();
        levelText.text = "LEVEL " + level.ToString();
    }

    /*Updates the UI to show the current behavior of Pacman.*/
    public void SetPacmanBehaviourText(string behaviorName)
    {
        pacmanBehaviorText.text = "Pacman Behavior: " + behaviorName;
    }


    /*Handles Pacman being eaten by a ghost.*/
    public void PacmanEaten()
    {
        pacman.DeathSequence();

        SetLives(lives - 1);

        if (lives > 0) {
            Invoke(nameof(ResetState), 3f);
        } else {
            GameOver();
        }
    }

    /*Handles a ghost being eaten by Pacman.*/
    public void GhostEaten(Ghost ghost)
    {
        int points = ghost.points * ghostMultiplier;
        SetScore(score + points);

        pacman.IncrementGhostsEaten();
        achievementController.TriggerGhostAchievement(selectedProfile, pacman.numberOfGhostsEaten);

        ghostMultiplier++;
    }

    /*Handles a pellet being eaten by Pacman.*/
    public void PelletEaten(Pellet pellet)
    {
        pellet.gameObject.SetActive(false);

        SetScore(score + pellet.points);

        if (!HasRemainingPellets())
        {
            pacman.gameObject.SetActive(false);
            Invoke(nameof(NewRound), 3f);
        }
    }

    /*Handles a power pellet being eaten by Pacman.*/
    public void PowerPelletEaten(PowerPellet pellet)
    {
        for (int i = 0; i < ghosts.Length; i++) {
            ghosts[i].frightened.Enable(pellet.duration);
        }

        PelletEaten(pellet);
        CancelInvoke(nameof(ResetGhostMultiplier));
        Invoke(nameof(ResetGhostMultiplier), pellet.duration);
    }

    /*Checks if any pellets are left in the level.*/
    private bool HasRemainingPellets()
    {
        foreach (Transform pellet in pellets)
        {
            if (pellet.gameObject.activeSelf) {
                return true;
            }
        }

        return false;
    }

    /*Resets the multiplier for ghost points.*/
    private void ResetGhostMultiplier()
    {
        ghostMultiplier = 1;
    }

    /*Saves the current game data.*/
    private void SaveGameData() {
        PlayerPrefs.SetInt(selectedProfile.ProfileName + "Score", score);
        PlayerPrefs.SetInt(selectedProfile.ProfileName + "Level", level);
        PlayerPrefs.SetInt(selectedProfile.ProfileName + "Lives", lives);

        PlayerPrefs.SetFloat(selectedProfile.ProfileName + "PacmanPositionX", pacman.transform.position.x);
        PlayerPrefs.SetFloat(selectedProfile.ProfileName + "PacmanPositionY", pacman.transform.position.y);
        PlayerPrefs.SetFloat(selectedProfile.ProfileName + "PacmanPositionZ", pacman.transform.position.z);

        for (int i = 0; i < ghosts.Length; i++)
        {
            PlayerPrefs.SetFloat(selectedProfile.ProfileName + $"Ghost{i}PositionX", ghosts[i].transform.position.x);
            PlayerPrefs.SetFloat(selectedProfile.ProfileName + $"Ghost{i}PositionY", ghosts[i].transform.position.y);
            PlayerPrefs.SetFloat(selectedProfile.ProfileName + $"Ghost{i}PositionZ", ghosts[i].transform.position.z);
        }

        for (int i = 0; i < pellets.childCount; i++)
        {
            PlayerPrefs.SetInt(selectedProfile.ProfileName + $"Pellet{i}Active", pellets.GetChild(i).gameObject.activeSelf ? 1 : 0);
        }

        PlayerPrefs.SetInt(selectedProfile.ProfileName + "gameData", 1);
        PlayerPrefs.Save();
    }

    /*Loads saved game data.*/
    private void LoadGameData() {
        if (PlayerPrefs.HasKey(selectedProfile.ProfileName + "gameData")) {
            SetScore(PlayerPrefs.GetInt(selectedProfile.ProfileName + "Score"));
            SetLives(PlayerPrefs.GetInt(selectedProfile.ProfileName + "Lives"));
            SetLevel(PlayerPrefs.GetInt(selectedProfile.ProfileName + "Level"));

            float x = PlayerPrefs.GetFloat(selectedProfile.ProfileName + "PacmanPositionX");
            float y = PlayerPrefs.GetFloat(selectedProfile.ProfileName + "PacmanPositionY");
            float z = PlayerPrefs.GetFloat(selectedProfile.ProfileName + "PacmanPositionZ");
            pacman.transform.position = new Vector3(x,y,z);


            for (int i = 0; i < ghosts.Length; i++) {
                x = PlayerPrefs.GetFloat(selectedProfile.ProfileName + $"Ghost{i}PositionX");
                y = PlayerPrefs.GetFloat(selectedProfile.ProfileName + $"Ghost{i}PositionY");
                z = PlayerPrefs.GetFloat(selectedProfile.ProfileName + $"Ghost{i}PositionZ");
                ghosts[i].transform.position = new Vector3(x, y, z);
            }

            for (int i = 0; i < pellets.childCount; i++) {
                bool active = PlayerPrefs.GetInt(selectedProfile.ProfileName + $"Pellet{i}Active") == 1;
                pellets.GetChild(i).gameObject.SetActive(active);
            }

            PlayerPrefs.DeleteKey(selectedProfile.ProfileName + "gameData");
        }
    }

    /*Begins the game after a certain delay.*/
    private void DelayedStart()
    {
        NewGame();
        HidePacmanAndGhosts();

        Time.timeScale = 0;

        StartCoroutine(WaitForRealTime(startDelayTime, 2f));
    }

    public IEnumerator WaitForRealTime(float delay) // Credit - https://answers.unity.com/questions/787180/make-a-coroutine-run-when-timetimescale-0.html
    {

        while (true)
        {
            float pauseEndTime = Time.realtimeSinceStartup + delay;
            while (Time.realtimeSinceStartup < pauseEndTime)
            {
                yield return 0;
            }
            break;
        }

        Time.timeScale = 1;
    }

    public IEnumerator WaitForRealTime(float delay, float secondDelay) // Credit - https://answers.unity.com/questions/787180/make-a-coroutine-run-when-timetimescale-0.html
    {
        countdownText.enabled = true;

        while (true) // At the end of this loop, pacman + ghosts placed on map
        {
            float pauseEndTime = Time.realtimeSinceStartup + delay - secondDelay;
            while (Time.realtimeSinceStartup < pauseEndTime)
            {
                yield return 0;
            }
            break;
        }

        NewGame();
        LoadGameData();

        while (true) // At end of this loop, the game timescale is set to normal and the game begins
        {
            float pauseEndTime = Time.realtimeSinceStartup + secondDelay;
            while (Time.realtimeSinceStartup < pauseEndTime)
            {
                yield return 0;
            }
            break;
        }

        Time.timeScale = 1;
        countdownText.enabled = false;
        isGame = true;
    }
    
    /*Makes the Pacman and ghost game objects inactive.*/
    private void HidePacmanAndGhosts()
    {
        for (int i = 0; i < ghosts.Length; i++)
        {
            ghosts[i].gameObject.SetActive(false);
        }

        pacman.gameObject.SetActive(false);
    }
}