using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutoScript : MonoBehaviour
{
    public PlayersMovement playersMovement;

    public TextMeshProUGUI textMesh;

    int etapeTuto = 0;

    bool triggered = false;
    float timer = 0;
    float maxTime = 5f;
    private void Update()
    {
        switch (etapeTuto)
        {
            case 0:
                if ((playersMovement.PauseAction.triggered ||
                    !playersMovement.getCanva().transform.GetChild(0).gameObject.activeSelf) && 
                    triggered == true)
                {
                    triggered = false;
                    textMesh.SetText("USE WASD \n TO MOOVE IN THE SCENE");
                    etapeTuto++;
                    timer = 0;
                    return;
                }

                if (playersMovement.PauseAction.triggered)
                {
                    triggered = true;
                    textMesh.SetText("");
                }
                break;
            case 1:
                if(timer>maxTime - 2)
                {
                    triggered = false;
                    textMesh.SetText("USE ARROWS KEYS \n TO SHOOT IN THE SCENE");
                    etapeTuto++;
                    timer = 0;
                    return;
                }
                if (playersMovement.moveAction.triggered)
                {
                    triggered = true;
                }
                if(triggered == true)
                {
                    timer += Time.deltaTime;
                }
                break;
            case 2:
                if (timer > maxTime - 2)
                {
                    triggered = false;
                    textMesh.SetText("PRESS R \nTO RELOAD");
                    etapeTuto++;
                    timer = 0;
                    return;
                }
                if (playersMovement.shootAction.triggered)
                {
                    triggered = true;
                }
                if (triggered == true)
                {
                    timer += Time.deltaTime;
                }
                break;
            case 3:
                if (playersMovement.ReloadAction.triggered)
                {
                    triggered = false;
                    textMesh.SetText("A RANDOM NUMBER OF BULLETS \n WILL BE ADDED TO YOUR MAGAZINE");
                    etapeTuto++;
                    timer = 0;
                    return;
                }
                break;
            case 4:
                timer += Time.deltaTime;
                if (timer > maxTime)
                {
                    triggered = false;
                    textMesh.SetText("RELOADING AT 0 BULLETS REMAINING \n WILL GIVE A DAMAGE BOOST");
                    etapeTuto++;
                    timer = 0;
                    return;
                }
                break;

            case 5:
                timer += Time.deltaTime;
                if (timer > maxTime)
                {
                    triggered = false;
                    textMesh.SetText("BULLETS ON THE CORNER OF THE SCREEN\nINDICATE WHEN TO RELOAD");
                    etapeTuto++;
                    timer = 0;
                    return;
                }
                break;

            case 6:
                timer += Time.deltaTime;
                if (timer > maxTime)
                {
                    triggered = false;
                    textMesh.SetText("RED = LOW ON BULLETS\nTHE LAST BULLET DEAL MORE DAMAGE");
                    etapeTuto++;
                    timer = 0;
                    return;
                }
                break;
            case 7:
                timer += Time.deltaTime;
                if (timer > maxTime)
                {
                    triggered = false;
                    textMesh.SetText("COMPLETE THE DUNGEON TO WIN THE GAME\nHAVE FUN !");
                    etapeTuto++;
                    timer = 0;
                    return;
                }
                break;
            case 8:
                timer += Time.deltaTime;
                if (timer > maxTime)
                {
                    SceneManager.LoadScene("Menu");
                }
                break;

        }               
    }

}
