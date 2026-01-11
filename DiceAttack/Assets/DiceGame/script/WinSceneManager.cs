using UnityEngine;

using UnityEngine.SceneManagement;

public class WinSceneManager : MonoBehaviour

{

    public void OpenMap()

    {

        SceneManager.LoadScene("Map");

    }

}