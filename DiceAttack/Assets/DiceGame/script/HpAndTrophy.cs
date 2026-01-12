using UnityEngine;

public class HpAndTrophy : MonoBehaviour
{
    public GameObject[] hp;
    public GameObject[] trophy;
    public GambleGame gambleGame;
    public NewMonoBehaviourScript newMonoBehaviourScript;
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
    
    public void Hhp()
    {
        if(newMonoBehaviourScript.loses > 0)
            hp[hp.Length - newMonoBehaviourScript.loses].SetActive(false);
    } 
}
