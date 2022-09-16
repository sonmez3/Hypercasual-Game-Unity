using System.Collections;
using UnityEngine;

public class MovingGate : MonoBehaviour
{
    private float allowableWidth;
    private float movementSpeed = 10f;
    private float GateHalfWidth = 4.5f;
    private float PlatformHalfWidth = 10f;
    private float PlatformLineWidth = 1f;
    void Start()
    {
        allowableWidth = PlatformHalfWidth - PlatformLineWidth - GateHalfWidth;
        StartCoroutine(Move());
    }

    private IEnumerator Move()
    {
        yield return new WaitForSeconds(Random.Range(0, 1f));
        while (true)
        {
            while (transform.localPosition.x > -allowableWidth) // yolun sonuna kadar sola git
            {
                transform.position += Vector3.left * movementSpeed * Time.deltaTime;
                yield return null;
            }


            while (transform.localPosition.x < allowableWidth) // yolun sonuna kadar sağa git
            {
                transform.position += Vector3.right * movementSpeed * Time.deltaTime;
                yield return null;
            }
        }
    }
}
