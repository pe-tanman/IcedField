using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void goTo2PlayerGame()
    {
        SceneManager.LoadScene("MainScene");
    }
    public void goToMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }
    public void goToFourPlayerGame()
    {
        SceneManager.LoadScene("MainSceneFourPlayers");
    }
    public void playAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void goToHelp()
    {
        SceneManager.LoadScene("HelpScene");
    }

}
