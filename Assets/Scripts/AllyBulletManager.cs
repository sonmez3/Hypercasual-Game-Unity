using System.Collections;
using UnityEngine;

public class AllyBulletManager : MonoBehaviour
{
    private float forwardSpeed = 28f;
    private float PlatformEndZCoordinate = 430f;
    public bool DidCollideWithEnemy;
    void Start()
    {
        GetComponent<Rigidbody>().velocity = Vector3.forward * forwardSpeed;
        DidCollideWithEnemy = false;
        StartCoroutine(BulletRoutine());
    }

    

    private IEnumerator BulletRoutine()
    {
        while (true)
        {
            if (transform.position.z >= PlatformEndZCoordinate || DidCollideWithEnemy)
            {
                break;
            }
            yield return null;
        }

        Destroy(gameObject);
    }
}
