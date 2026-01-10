using UnityEngine;
using UnityEngine.SceneManagement;
public class Map : MonoBehaviour
{
    public void OpenMap()
    {
        SceneManager.LoadScene("Map");
    }
}
