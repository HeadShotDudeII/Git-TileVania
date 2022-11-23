using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    [SerializeField] float loadLevelDelay = 1f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            StartCoroutine(LoadNextLevel());


        }
    }

    IEnumerator LoadNextLevel()
    {
        yield return new WaitForSecondsRealtime(loadLevelDelay);
        int nextlevelNumber = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextlevelNumber == SceneManager.sceneCountInBuildSettings)
        {
            nextlevelNumber = 0;
        }
        SceneManager.LoadScene(nextlevelNumber);


    }

}
