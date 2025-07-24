using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{

    [SerializeField] GameObject pauseScreen;


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && pauseScreen != null)
        {
            if (pauseScreen.activeInHierarchy)
            {
                PauseGame(false);
            }
            else
            {
                PauseGame(true);
            }
        }

    }


    public void LoadMainMenu()
    {
        ResetMap();
        SceneManager.LoadScene("MainMenu");

    }
    public void LoadGameOver()
    {
        ResetMap();
        SceneManager.LoadScene("GameOver");

    }


    public void LoadNewGame()
    {
        ResetMap();

        SceneManager.LoadScene("Map1");
    }

    public void LoadNextMap()
    {
        // reset lại giá trị các phòng của boss để đảm bảo khi load lại nó sẽ đúng

        RoomSpawner.haveRoomMiniBoss = false;
        RoomSpawner.haveRoomTrader = false;
        RoomSpawner.haveRoomBoss = false;
        // phải set bằng 1 do chuyển về đây có thể đang từ pause và nó đang bằng 0

        Time.timeScale = 1f;

        string currentScene = SceneManager.GetActiveScene().name;

        if (currentScene == "Map1")
            SceneManager.LoadScene("Map2");
        else if (currentScene == "Map2")
            SceneManager.LoadScene("Map3");
        else
            SceneManager.LoadScene("Victory");
    }



    public void QuitGame()
    {
        Application.Quit();

    }


    public void PauseGame(bool active)
    {
        pauseScreen.SetActive(active);
        Time.timeScale = active ? 0f : 1f;
    }


    void ResetMap()
    {
        // xóa toàn bộ coin 
        CoinManager.RemoveCoin(CoinManager.GetCoin());

        // reset lại giá trị các phòng của boss để đảm bảo khi load lại nó sẽ đúng

        RoomSpawner.haveRoomMiniBoss = false;
        RoomSpawner.haveRoomTrader = false;
        RoomSpawner.haveRoomBoss = false;

        // phải set bằng 1 do chuyển về đây có thể đang từ pause và nó đang bằng 0

        Time.timeScale = 1f;
        if (Player.instance != null)
        {
            // xóa player 
            Destroy(Player.instance.gameObject);
            Player.instance = null;
        }
    }


}
