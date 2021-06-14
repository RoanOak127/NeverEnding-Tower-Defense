using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Michsky.UI.ModernUIPack;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameControl : MonoBehaviour
{
    public int health = 100;
    public int cash = 500;
    private int level = 1;
    public int numEasyEnemies = 10;
    public int numMedEnemies = 5;
    public int numHardEnemies = 1;
    public int totalEnemiesAlive;
    
    public Vector3 activeTowerPosition;
    public TowerMenus activeTowerSpotScript;

    public GameObject buyMenu;
    public GameObject upgradeMenu;
    [SerializeField] ModalWindowManager gameOverPanel;
    //[SerializeField] GameObject gameOverPanel;
    [SerializeField] GameObject uiPanel;

    public GameObject playButton;
    public ProgressBar healthBar;
    public Text showCash;
    public Text LevelDisplay;
    public Text EnemiesLeftDisplay;
    public Text UpgradeDisplay;

    [SerializeField] Sprite playIcon;
    [SerializeField] Sprite waitIcon;


    public bool isNextLevelReady;
    public bool isDead = false;

    // Start is called before the first frame update
    void Start()
    {
        //PlayerPrefs.DeleteAll();  //sometimes it remembers random things you play with and wont go back
        //PlayerPrefs
        if (PlayerPrefs.HasKey("StartHealth"))
            health = PlayerPrefs.GetInt("StartHealth");
        else
            PlayerPrefs.SetInt("StartHealth", health);

        if (PlayerPrefs.HasKey("StartCash"))
            cash = PlayerPrefs.GetInt("StartCash");
        else
            PlayerPrefs.SetInt("StartCash", cash);
        
        if (PlayerPrefs.HasKey("Easy"))
            numEasyEnemies = PlayerPrefs.GetInt("Easy");
        else
            PlayerPrefs.SetInt("Easy", numEasyEnemies);

        if (PlayerPrefs.HasKey("Med"))
            numMedEnemies = PlayerPrefs.GetInt("Med");
        else
            PlayerPrefs.SetInt("Med", numMedEnemies);

        if (PlayerPrefs.HasKey("Hard"))
            numHardEnemies = PlayerPrefs.GetInt("Hard");
        else
            PlayerPrefs.SetInt("Hard", numHardEnemies);

        Time.timeScale = 1;

        gameOverPanel.AnimateWindow();
        //gameOverPanel.UpdateUI();
        //gameOverPanel.CloseWindow();
        //gameOverPanel.SetActive(false);
        uiPanel.SetActive(true);
        buyMenu.SetActive(false);
        upgradeMenu.SetActive(false);

        UpgradeDisplay.text = "";
        healthBar.currentPercent = health;
        showCash.text = "$" + cash;
        LevelDisplay.text = "Level: " + level;
        totalEnemiesAlive = numEasyEnemies + numMedEnemies + numHardEnemies;
        EnemiesLeftDisplay.text = "Enemies Left: " + totalEnemiesAlive;

    }

    // Update is called once per frame
    void Update()
    {
        if(isNextLevelReady && totalEnemiesAlive <= 0) //I have a bug where multiple towers kill the same enemey giving it an overkill number
        {
            isNextLevelReady = false;
            setNextLevel();
        }
        if(health <= 0)
        {
            callGameOver();
        }
    }

    private void setNextLevel()
    {
        level++;
        numEasyEnemies += 3;
        if (level % 2 == 0) //every two levels
            numMedEnemies += 2;
        if (level % 3 == 0) //every 3 levels
            numHardEnemies += 1;

        gameObject.GetComponent<EnemySpawner>().numEasy = numEasyEnemies;
        gameObject.GetComponent<EnemySpawner>().numMed = numMedEnemies;
        gameObject.GetComponent<EnemySpawner>().numHard = numHardEnemies;

        totalEnemiesAlive = numEasyEnemies + numMedEnemies + numHardEnemies;

        LevelDisplay.text = "Level: " + level;

        playButton.GetComponent<ButtonManagerBasicIcon>().buttonIcon = playIcon;
        playButton.GetComponent<ButtonManagerBasicIcon>().UpdateUI();

    }

    public void onPlayClick()
    {
        Debug.Log("Clicked Play");
        playButton.GetComponent<ButtonManagerBasicIcon>().buttonIcon = waitIcon;
        playButton.GetComponent<ButtonManagerBasicIcon>().UpdateUI();
        EnemiesLeftDisplay.text = "Enemies Left: " + totalEnemiesAlive;
        StartCoroutine(gameObject.GetComponent<EnemySpawner>().Spawn());
    }

    public void callGameOver()
    {
        Time.timeScale = 0;
        gameOverPanel.AnimateWindow();
        gameOverPanel.UpdateUI();
        //gameOverPanel.OpenWindow();
        //gameOverPanel.SetActive(true);
        uiPanel.SetActive(false);
        buyMenu.SetActive(false);
        upgradeMenu.SetActive(false);

    }

    //ends the game
    //TODO pull up game over menu show current score, make a high a score and show it, ask if they want to replay the game
    public void GameOver()
    {
        PlayerPrefs.DeleteAll();

#if  UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit(); //quits the application but not the editor

#endif
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }    
}
