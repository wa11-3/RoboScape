using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundEffect : MonoBehaviour
{
    public static SoundEffect current;

    private void Awake()
    {
        if (current != null && current != this)
        {
            Destroy(gameObject);
            return;
        }
        current = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "LevelOne" || SceneManager.GetActiveScene().name == "LevelTwo" || SceneManager.GetActiveScene().name == "LevelThree")
        {
            Debug.Log("Destruyendo");
            Destroy(gameObject);
        }
    }
}