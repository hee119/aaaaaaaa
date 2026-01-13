using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour
{
    public GameObject[] clearImage;
    public GameObject player;
    public Transform[] stage;
    

    void Start()
    {
        if (SceneManager.GetActiveScene().name == "Map")
        {
            UpdateMap();
            player = GameObject.FindGameObjectWithTag("MapPlayer");
            player.transform.position = stage[GameManager.Instance.nowScene].position;
        }
    }

    void UpdateMap()
    {
        if (SceneManager.GetActiveScene().name != "Map")
            return;

        for (int i = 0; i < clearImage.Length; i++)
        {
            if(GameManager.Instance.winCount[i])
                clearImage[i].SetActive(true);
        }
    }
}