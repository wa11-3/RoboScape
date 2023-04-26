using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Lobby : MonoBehaviour
{
    public GameObject[] select;
    public int selectIndex;

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.A))
        {
            selectIndex -= 1;
        }
        if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.D))
        {
            selectIndex += 1;
        }

        if (selectIndex == 3)
            selectIndex = 2;
        if (selectIndex == -1)
            selectIndex = 0;

        for (int i=0; i < select.Length; i++)
        {
            select[i].SetActive(false);
        }
        select[selectIndex].SetActive(true);

        if (Input.GetKeyUp(KeyCode.Return) || Input.GetKeyUp(KeyCode.Space))
        {
            switch (selectIndex)
            {
                case 0:
                    SceneManager.LoadScene("LevelTwo");
                    break;
                case 1:
                    SceneManager.LoadScene("LevelOne");
                    break;
                case 2:
                    SceneManager.LoadScene("LevelThree");
                    break;
            }
        }
    }
}
