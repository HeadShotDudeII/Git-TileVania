using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{
    [SerializeField] int playerLifes = 3;
    int playeScore = 0;
    [SerializeField] TextMeshProUGUI playerScoreText;
    [SerializeField] TextMeshProUGUI playerLifesText;


    void Awake()
    {
        int numGameSessions = FindObjectsOfType<GameSession>().Length;
        if (numGameSessions > 1)
        {
            Destroy(gameObject); // delete the new one keep the old one not delete the whole list.
        }
        else
        {
            DontDestroyOnLoad(gameObject);
            Debug.Log("Load Score is " + playeScore);
            Debug.Log("Load Lifes is " + playerLifes);


        }


    }

    private void Start()
    {
        playerLifesText.text = "Life: " + playerLifes.ToString();
        playerScoreText.text = "Score: " + playeScore.ToString();
    }

    public void ProcessPlayerDeath()
    {
        if (playerLifes > 1)
        {
            TakeLife();
        }
        else
        {
            ResetGameSession();
        }

    }
    public void AddScore()
    {

        playeScore++;
        UpdateScores();
        Debug.Log("AddScore after " + playeScore + "---------------------------------");

    }

    public void UpdateScores()
    {
        playerScoreText.text = "Scores " + playeScore.ToString();
        //+ " level " + SceneManager.GetActiveScene().buildIndex;
        //Debug.Log("Coin " + playeScore + " UITEXT: " + playerScoreText.text);
        //Debug.Log("inside updateScores" + playerScoreText.text);

    }

    public void UpdateLifes()
    {
        playerLifesText.text = "Lifes " + playerLifes.ToString();
        //+ "level " + SceneManager.GetActiveScene().buildIndex;
        //Debug.Log("Life " + playerLifes + " UITEXT + " + playerLifesText.text);

    }

    public int GetScore()
    {
        return playeScore;

    }

    public int GetLifes()
    {
        return playerLifes;
    }



    private void ResetGameSession()
    {
        FindObjectOfType<ScenePersist>().ResetScene();
        SceneManager.LoadScene(0); //load scene first then destroy self.
        Destroy(gameObject);

    }

    private void TakeLife()
    {
        playerLifes--;
        playeScore = 0;
        UpdateLifes();
        UpdateScores();
        Debug.Log("Player lifes is " + playerLifes);
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene);
    }


}
