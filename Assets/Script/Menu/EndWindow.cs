using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndWindow : MonoBehaviour
{

    public Text TextEnd;

    private void Start()
    {
        if(PassInfoBTWScene.result == PassInfoBTWScene.Result.win)
        {
            TextEnd.text = "YOU WON";
        }
        else
        {
            TextEnd.text = "YOU LOST";
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void Menu()
    {
        SceneManager.LoadScene("Menu");
    }


    public void QuitGame()
    {
        Application.Quit();
    }
}
