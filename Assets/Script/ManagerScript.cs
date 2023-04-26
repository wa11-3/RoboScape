using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ManagerScript : MonoBehaviour
{
    public GameObject gameover;
    public GameObject win;
    public GameObject ui;

    public SpriteRenderer blues;
    public SpriteRenderer yellows;
    public SpriteRenderer reds;
    public SpriteRenderer greens;

    public Image blueui;
    public Image yellowui;
    public Image redui;
    public Image greenui;

    public bool[] lights = { false, false, false, false };

    public Animator animdoor;

    private void Update()
    {
        if (LightCheck())
        {
            animdoor.Play("OpenDoor");
        }
        KeyCheck();
    }

    public IEnumerator GameOver()
    {
        gameover.SetActive(true);
        ui.SetActive(false);
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("Menu");
    }

    public IEnumerator Win()
    {
        switch (SceneManager.GetActiveScene().name)
        {
            case "LevelOne":
                SceneManager.LoadScene("LevelThree");
                break;

            case "LevelTwo":
                SceneManager.LoadScene("LevelOne");
                break;

            case "LevelThree":
                win.SetActive(true);
                ui.SetActive(false);
                yield return new WaitForSeconds(5);
                SceneManager.LoadScene("Credits");
                break;
        }
        
    }

    public void KeyManager(string col)
    {
        switch (col)
        {
            case "Blue":
                Debug.Log("Blue");
                blues.color += new Color(0, 0, 0, 255.0f);
                blueui.color += new Color(255.0f, 255.0f, 255.0f);
                lights[0] = true;
                break;

            case "Yellow":
                Debug.Log("Yellow");
                yellows.color += new Color(0, 0, 0, 255.0f);
                yellowui.color += new Color(255.0f, 255.0f, 255.0f);
                lights[1] = true;
                break;

            case "Red":
                Debug.Log("Red");
                reds.color += new Color(0, 0, 0, 255.0f);
                redui.color += new Color(255.0f, 255.0f, 255.0f);
                lights[2] = true;
                break;

            case "Green":
                Debug.Log("Green");
                greens.color += new Color(0, 0, 0, 255.0f);
                greenui.color += new Color(255.0f, 255.0f, 255.0f);
                lights[3] = true;
                break;
        }
    }

    bool LightCheck()
    {
        foreach(bool li in lights)
        {
            if (!li)
            {
                return false;
            }
        }
        return true;
    }

    void KeyCheck()
    {
        if (Input.GetKeyUp(KeyCode.R))
        {
            SceneManager.LoadScene("Menu");
        }
        if (Input.GetKeyUp(KeyCode.Q))
        {
            Application.Quit();
        }
    }
}