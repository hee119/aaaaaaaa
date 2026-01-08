using UnityEngine;

public class KeyBoardManager : MonoBehaviour
{
    PlayerLogic playerLogic;

    void Awake()
    {
        playerLogic = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerLogic>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            playerLogic.Attack();
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            playerLogic.Attack();
        }
    }
}
