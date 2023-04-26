using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public GameObject starttx;
    public GameObject endtx;
    public GameObject creditstx;

    public GameObject[] startim = new GameObject[2];
    public GameObject[] endim = new GameObject[2];
    public GameObject[] creditim = new GameObject[2];


    public float menuIndex;

    private void Start()
    {
        menuIndex = 1;
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.W))
        {
            menuIndex -= 1;
        }
        if (Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyUp(KeyCode.S))
        {
            menuIndex += 1;
        }

        switch (menuIndex)
        {
            case 0:
                menuIndex = 1;
                break;
            case 1:
                startim[0].SetActive(true);
                startim[1].SetActive(true);
                creditim[0].SetActive(false);
                creditim[1].SetActive(false);
                endim[0].SetActive(false);
                endim[1].SetActive(false);
                break;
            case 2:
                startim[0].SetActive(false);
                startim[1].SetActive(false);
                creditim[0].SetActive(true);
                creditim[1].SetActive(true);
                endim[0].SetActive(false);
                endim[1].SetActive(false);
                break;
            case 3:
                startim[0].SetActive(false);
                startim[1].SetActive(false);
                creditim[0].SetActive(false);
                creditim[1].SetActive(false);
                endim[0].SetActive(true);
                endim[1].SetActive(true);
                break;
            case 4:
                menuIndex = 3;
                break;
        }

        if (Input.GetKeyUp(KeyCode.Return) || Input.GetKeyUp(KeyCode.Space))
        {
            switch (menuIndex)
            {
                case 1:
                    SceneManager.LoadScene("Lobby");
                    break;
                case 2:
                    SceneManager.LoadScene("Credits");
                    break;
                case 3:
                    Application.Quit();
                    break;
            }
                        
        }
    }
}
