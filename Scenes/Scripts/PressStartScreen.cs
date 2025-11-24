using UnityEngine;
using UnityEngine.SceneManagement;

public class PressStartScreen : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}