using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private changeCharacter _changeCharacter;
    [SerializeField]private Text PlayerHealthText;
    [SerializeField]private GameObject topWaveText;
    [SerializeField]private GameObject menuWave;
    private GameObject[] enemysInScene;
    public AudioSource[] audios;
    public int PlayerHealth;
    public int wave;
    public int topWave;
    public GameObject startPlayerGame;

    public GameObject SignTable;

    [SerializeField]private int isFirstRunGame = 0;

    public static GameManager instance;

    private bool IsPlaySoundEndGame = false;

    void Awake()
    {
        isFirstRunGame = PlayerPrefs.GetInt("CurrentNumberGameStart");
        //wave = 1;
        topWave = PlayerPrefs.GetInt("TopWave");
        if(!instance)
        {
            instance = this;
        }
        if(isFirstRunGame < 1)
        {
            if(topWaveText)
            {
                topWaveText.transform.GetChild(0).gameObject.SetActive(false);
                topWaveText.transform.GetChild(1).gameObject.SetActive(false);
                isFirstRunGame++;
                PlayerPrefs.SetInt("CurrentNumberGameStart", isFirstRunGame);
                //PlayerPrefs.SetInt("TopWave", wave);
            }
        }
        else
        {
            if(topWaveText)
            {
                isFirstRunGame++;
                topWaveText.transform.GetChild(0).gameObject.SetActive(true);
                topWaveText.transform.GetChild(1).gameObject.SetActive(true);
                topWaveText.transform.GetChild(1).GetComponent<Text>().text = topWave + " WAVE";
            }
        }
    }

    void Start()
    {
        _changeCharacter = GameObject.FindObjectOfType<changeCharacter>();
        Time.timeScale = 1;
        if(menuWave)
        {
            startWaveMenu();
        }
        if(SignTable)
        {
            SignTable.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "Wave: " + wave.ToString();
            SignTable.transform.GetChild(0).GetChild(2).GetComponent<Text>().text = "BEST: " + topWave.ToString() + " WAVE";
        }

         instancePlayerWithStartGame();
    }

    public void startWaveMenu()
    {
        if(!menuWave.activeSelf)
        {
            menuWave.SetActive(true);
            menuWave.transform.GetChild(1).gameObject.SetActive(true);
            menuWave.transform.GetChild(1).GetChild(1).GetComponent<Text>().text = wave.ToString();
            enemysInScene = GameObject.FindGameObjectsWithTag("Enemy");
            foreach(GameObject go in enemysInScene)
            {
                if(go)
                {
                    go.GetComponent<enemyMotion>().startWave = true;
                }
            }
            Invoke("timeMenuWave",1f);
        }
    }

    private void timeMenuWave()
    {
        menuWave.SetActive(false);
        menuWave.transform.GetChild(1).gameObject.SetActive(false);
        foreach(GameObject go in enemysInScene)
        {
            if(go)
            {
                go.GetComponent<enemyMotion>().startWave = false;
            }
        }
    }

    void Update()
    {
        if(PlayerHealthText)
        {
            PlayerHealthText.text = PlayerHealth.ToString();
        }

        if(PlayerHealth <= 0)
        {
            GameManager.instance.audios[7].Pause();
            if(!IsPlaySoundEndGame)
            {
                GameManager.instance.audios[5].Play();
                IsPlaySoundEndGame = true;
            }
            menuWave.SetActive(true);
            menuWave.transform.GetChild(1).gameObject.SetActive(false);
            menuWave.transform.GetChild(0).gameObject.SetActive(true);
            Time.timeScale = 0;
        }
    }
    public void loadSceneManin()
    {
        SceneManager.LoadScene(1);
    }

    public void restartGame()
    {
        AdManager.instance.startAd(); 
        loadSceneManin();  
    }

    private void instancePlayerWithStartGame()
    {
        if(startPlayerGame)
        {
            GameObject player = Instantiate(startPlayerGame, _changeCharacter.spawnPosition.position, Quaternion.identity);
            player.transform.position += new Vector3(0,0,111);
            _changeCharacter.countPlayerInScene++;
            ControlActiveCharacter.instance.staticPlayer = player;
        }
    }

    public void cancelAndResumeSound()
    {
        if(AudioListener.volume == 1)
        {
            AudioListener.volume = 0;
        }
        else
        {
            AudioListener.volume = 1;
        }
    }

    public void PauseGame()
    {
        if(Time.timeScale == 1)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    public void toMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void activeMenuRestartAndMainMenuGame()
    {
        menuWave.gameObject.SetActive(true);
        menuWave.transform.GetChild(1).gameObject.SetActive(false);
        menuWave.transform.GetChild(0).gameObject.SetActive(false);
        menuWave.transform.GetChild(2).gameObject.SetActive(true);
        PauseGame();
    }

    public void resumeGame()
    {
        PauseGame();
        menuWave.gameObject.SetActive(false);
        menuWave.transform.GetChild(1).gameObject.SetActive(false);
        menuWave.transform.GetChild(0).gameObject.SetActive(false);
        menuWave.transform.GetChild(2).gameObject.SetActive(false);
    }
}
