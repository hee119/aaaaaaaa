using UnityEngine;
using UnityEngine.SceneManagement;

public class a : MonoBehaviour
{
    public void OpenMap()

    {

        SceneManager.LoadScene("Map");

    }

    public void OpenStage1()
    {
        SceneManager.LoadScene("1");
    }
}
