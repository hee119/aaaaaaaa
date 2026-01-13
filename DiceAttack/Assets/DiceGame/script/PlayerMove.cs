using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public GameObject player;
    public StageAttack Attack;
    private string myName;
    
    
    public void Move()
    {
        myName = gameObject.name;
        player.transform.position = transform.position;
        Attack.trans = gameObject;
        GameManager.Instance.nowScene = int.Parse(myName);
        Debug.Log(GameManager.Instance.nowScene);
    }
}
