using System.Collections;
using UnityEngine;

public class Barnacle : MonoBehaviour
{
    public float moveAmount = 1f; 
    public float interval = 2f;  
    public float speed = 2f;  

    private float initialY;
    private bool movingUp = true; 

    private void Start()
    {
        initialY = transform.position.y; 
        StartCoroutine(MoveUpDownSmooth());
    }

    private IEnumerator MoveUpDownSmooth()
    {
        while (true)
        {
            float targetY = movingUp ? initialY + moveAmount : initialY;
            float elapsedTime = 0f;

            Vector3 startPos = transform.position;
            Vector3 targetPos = new Vector3(transform.position.x, targetY, transform.position.z);

            while (elapsedTime < interval)
            {
                transform.position = Vector3.Lerp(startPos, targetPos, elapsedTime / interval);
                elapsedTime += Time.deltaTime * speed;
                yield return null;
            }

            transform.position = targetPos;
            movingUp = !movingUp;
            yield return new WaitForSeconds(0.5f);
        }
    }
}
