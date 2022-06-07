using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public TextMeshProUGUI nameText;
    public Text highScoreText;
    public Text ScoreText;
    public GameObject GameOverText;
    
    private bool m_Started = false;
    private int m_Points;
    
    private bool m_GameOver = false;

    void Start()
    {
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);
        
        int[] pointCountArray = new [] {1,1,2,2,5,5};
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                Brick brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }

        SetHighScore();
        SetName();
    }

    private void Update()
    {
        if (!m_Started) {
            if (Input.GetKeyDown(KeyCode.Space)) {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        } else if (m_GameOver) {
            if (Input.GetKeyDown(KeyCode.Space)) {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape)) {
            SceneManager.LoadScene(0);
        }

    }

    void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = $"Score : {m_Points}";
    }

    void SetName() {
        if (DataManager.Instance == null) nameText.text = "Name: not been set";
        else nameText.text = $"Name: {DataManager.Instance.playerName}";
    }

    void SetHighScore() {
        if (DataManager.Instance != null) {
            if (DataManager.Instance.highScore == 0) highScoreText.text = $"Best Score : Not set yet";
            else highScoreText.text = $"Best Score(Name: {DataManager.Instance.highScoreName}): {DataManager.Instance.highScore}";
        } else highScoreText.text = $"Best Score : Not set yet";
    }

    public void GameOver()
    {
        m_GameOver = true;
        GameOverText.SetActive(true);

        if (DataManager.Instance.highScore < m_Points) {
            DataManager.Instance.highScore = m_Points;
            DataManager.Instance.highScoreName = DataManager.Instance.playerName;
            SetHighScore();
        }
    }
}
