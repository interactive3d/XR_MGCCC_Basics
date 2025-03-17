using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour {
    public static GameManager Instance { get; private set; }

    public TMP_Text scoreText;
    public TMP_Text gameOverText;

    private int score = 0;
    private int activeMoles = 0;
    public int maxActiveMoles = 5;
    public float maxMoleTime = 10f;
    private float difficultyIncreaseRate = 0.9f; // Each level speeds up the game
    private float minPopUpTime = 0.5f;

    private bool gameActive = true;
    
    public bool startGame = false;
    public GameObject[] moles;
    public AnimationClip[] molesDown;
    public AnimationClip[] molesUp;
    public Animation[] molesAnimationDown;

    private void Awake() {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Update() {
        if (startGame) {
            startGame = false;

        }
        if (Input.GetKeyDown(KeyCode.R)) {
            for (int i = 0; i<moles.Length; i++) {
                moles[i].GetComponent<Animator>().SetTrigger("PopDown");
            }
        }
        if (Input.GetKeyDown(KeyCode.T)) {
            for (int i = 0; i < moles.Length; i++) {
                moles[i].GetComponent<Animator>().SetTrigger("PopUp");

            }
        }
    }

    public void ResetGame() {
        startGame = false;
        score = 0;
        // start countdown

    }


    public bool CanMoleAppear() {
        return activeMoles < maxActiveMoles;
    }

    public void MoleAppeared() {
        activeMoles++;
    }

    public void MoleDisappeared() {
        activeMoles--;
    }

    public void MoleHit() {
        if (!gameActive) return;

        score++;
        UpdateUI();
        // IncreaseDifficulty();
    }

    public void MoleMissed() {
        if (!gameActive) return;

        // EndGame();
    }

    private void IncreaseDifficulty() {
        if (score % 5 == 0) // Every 5 hits, increase difficulty
        {
            minPopUpTime *= difficultyIncreaseRate;
            maxMoleTime *= difficultyIncreaseRate;
        }
    }

    private void UpdateUI() {
        scoreText.text = "Score: " + score;
    }

    private void EndGame() {
        gameActive = false;
        gameOverText.text = "Game Over!";
        // Invoke("RestartGame", 3f);
    }

    private void RestartGame() {
        // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Debug.Log("This will restart the game now");
    }
}
