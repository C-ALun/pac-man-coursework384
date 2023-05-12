// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.UI;
// using UnityEngine.SceneManagement;

// public class ProgressSummariesController : MonoBehaviour
// {
//     public GameObject progressSummaryItemPrefab;
//     public Transform contentPanel;


//     private void Start()
//     {
//         LoadProgressSummaries();
//     }

//     private void Update() {
//         if (Input.GetKeyDown(KeyCode.B)) {
//             SceneManager.LoadScene("MenuScene");
//         }
//     }

//     private void LoadProgressSummaries()
//     {
//         int progressCount = PlayerPrefs.GetInt("ProgressCount", 0);
//         Debug.Log(progressCount);

//         for (int i = 1; i <= progressCount; i++)
//         {
//             if (PlayerPrefs.HasKey("GameData_" + i))
//             {
//                 GameData gameData = JsonUtility.FromJson<GameData>(PlayerPrefs.GetString("GameData_" + i));
//                 int level = gameData.level;
//                 int score = gameData.score;
//                 int numOfGhostEaten = gameData.numOfGhostEaten;
//                 int numOfAchievementUnlocked = gameData.numOfAchievementUnlocked;

//                 DisplayProgressSummary(i, level, score, numOfGhostEaten, numOfAchievementUnlocked);
//             }
//         }
//     }

//     private void DisplayProgressSummary(int index , int level, int score, int numOfGhostEaten, int numOfAchievementUnlocked)
//     {
//         GameObject progressSummaryItem = Instantiate(progressSummaryItemPrefab, contentPanel);
//         Text idText = progressSummaryItem.transform.Find("IdText").GetComponent<Text>();
//         Text levelText = progressSummaryItem.transform.Find("LevelText").GetComponent<Text>();
//         Text scoreText = progressSummaryItem.transform.Find("ScoreText").GetComponent<Text>();
//         Text numOfGhostEatenText = progressSummaryItem.transform.Find("NumOfGhostEatenText").GetComponent<Text>();
//         Text numOfAchievementUnlockedText = progressSummaryItem.transform.Find("NumOfAchievementUnlocked").GetComponent<Text>();
//         idText.text = index.ToString();
//         levelText.text = level.ToString();
//         scoreText.text = score.ToString();
//         numOfGhostEatenText.text = numOfGhostEaten.ToString();
//         numOfAchievementUnlockedText.text = numOfAchievementUnlocked.ToString();
//     }
// }