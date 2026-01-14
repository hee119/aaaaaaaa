using System.Collections;
using UnityEngine;

public class Left : MonoBehaviour
{
    public RectTransform target;

    public float slideAmount = 790f;
    public float minX = -1192f; // 왼쪽 한계
    public float maxX = 1202f;     // 오른쪽 한계

    Vector2 startPos;
    Vector2 targetPos;

    public void Touch()
    {
        startPos = target.anchoredPosition;

        float nextX = startPos.x + slideAmount;

        // 🔒 여기서 제한
        nextX = Mathf.Clamp(nextX, minX, maxX);

        targetPos = new Vector2(nextX, startPos.y);

        StartCoroutine(Move());
    }

    IEnumerator Move()
    {
        float timer = 0f;
        float duration = 0.3f;

        while (timer < duration)
        {
            float t = timer / duration;
            target.anchoredPosition = Vector2.Lerp(startPos, targetPos, t);
            timer += Time.deltaTime;
            yield return null;
        }

        target.anchoredPosition = targetPos;
    }
}