using System.Collections;
using UnityEngine;

public class Left : MonoBehaviour
{
    public GameObject target;
    Vector3 targetCurrentPos = Vector3.zero;
    Vector3 targetPos = Vector3.zero;

    public void Touch()
    {
        targetCurrentPos = target.transform.position;
        targetPos = new Vector3(targetCurrentPos.x - 790, target.transform.position.y, target.transform.position.z);
        StartCoroutine(RightIE());
    }

    IEnumerator RightIE()
    {
        float timer = 0;
        while (timer < 2)
        {
            target.transform.position = Vector3.Lerp(targetCurrentPos, targetPos, timer);
            timer += Time.deltaTime;
            yield return null;
        }
    }
}
