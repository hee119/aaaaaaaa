using UnityEngine;

public class HpAndTrophy : MonoBehaviour
{
    public GameObject[] hp;
    public GameObject[] trophy;
    public GambleGame gambleGame;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Hp()
    {
        if(gambleGame.loses > 0)
        hp[hp.Length - gambleGame.loses].SetActive(false);
    }

    // Update is called once per frame
    public void Trophy()
    {
        if(gambleGame.wins > 0)
        trophy[gambleGame.wins - 1].SetActive(true);
    }
}
