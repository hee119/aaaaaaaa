using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour
{
    public GameObject[] clearImage;
    public GameObject player;
    public Transform[] stage;

    private void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void Start()
    {
        UpdateMap();
        if (SceneManager.GetActiveScene().name == "Map")
        {
            player = GameObject.FindGameObjectWithTag("MapPlayer");
            player.transform.position = stage[GameManager.Instance.nowScene].position;
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
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