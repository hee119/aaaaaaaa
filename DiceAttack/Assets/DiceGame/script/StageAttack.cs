using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageAttack : MonoBehaviour
{
    public GameObject trans;
    public StageManager manager;

    private void Awake()
    {
        trans = gameObject;
    }

    public void Attack()
    {
        Debug.Log(trans.name);
        Debug.Log(manager);                       // manager가 null인지 확인
        Debug.Log(manager.clearText.Length);      // 배열 길이 확인
        Debug.Log(manager.clearText[0]);          // 0번 요소 확인

        if (GameManager.Instance.winCount == 0 && int.Parse(trans.name) == 1)
        {
            SceneManager.LoadScene(trans.name);
        }
        if (manager.clearText[0].activeSelf && (int.Parse(trans.name) == 2 || int.Parse(trans.name) == 3))
        {
            SceneManager.LoadScene(trans.name);
        }

        if (manager.clearText[1].activeSelf && (int.Parse(trans.name) == 3 || int.Parse(trans.name) == 5 || int.Parse(trans.name) == 4))
        {
            SceneManager.LoadScene(trans.name);
        }

        if  (manager.clearText[3].activeSelf  && int.Parse(trans.name) == 6)
        {
            SceneManager.LoadScene(trans.name);
        }

        if (manager.clearText[4].activeSelf && (int.Parse(trans.name) == 2 || int.Parse(trans.name) == 3)) ;
    }
}
