using UnityEngine;
using UnityEngine.SceneManagement;

public class Retry : MonoBehaviour
{
    public void ReZero()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
