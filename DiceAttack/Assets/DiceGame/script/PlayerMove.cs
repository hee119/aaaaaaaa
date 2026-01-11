using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public GameObject player;
    public StageAttack Attack;
    public void Move()
    {
        player.transform.position = transform.position;
        Attack.trans = gameObject;
    }
}
