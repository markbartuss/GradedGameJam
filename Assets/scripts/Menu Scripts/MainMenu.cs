using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    void Start()
    {
        Cursor.visible = true;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
    }
    public void Play()
    {
        //changes to the game scene when the method play gets called
        SceneChange();
        Cursor.visible = false;
    }
    public void BackToMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        Time.timeScale = 1;
    }
    public void SceneChange()//goes to the next scene in the buildIndex
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Time.timeScale = 1f;
    }
    public void quit()
    {
        Application.Quit();
    }
}
