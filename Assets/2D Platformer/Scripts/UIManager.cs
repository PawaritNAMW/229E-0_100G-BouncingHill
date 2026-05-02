using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject pauseUI;

    private bool isPaused = false;

    // Update is called once per frame
    void Update()
    {
        // Toggle pause with ESC
        if (Input.GetKeyDown(KeyCode.Escape))
        {

            if (isPaused)
                Resume();
            else
                Pause();
        }
    }
    public void StartGame()
    {
        Time.timeScale = 1f; // IMPORTANT: unpause game
        SceneManager.LoadScene("Stage");
    }
    public void Pause()
    {
        if (pauseUI != null)
            pauseUI.SetActive(true);

        Time.timeScale = 0f;
        isPaused = true;
    }
    public void Resume()
    {
        if (pauseUI != null)
            pauseUI.SetActive(false);

        Time.timeScale = 1f;
        isPaused = false;
    }
    public void GoToMainMenu()
    {
        Time.timeScale = 1f; // IMPORTANT: unpause game
        SceneManager.LoadScene("Main_menu");
    }
    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
