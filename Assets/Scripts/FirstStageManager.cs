using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstStageManager : MonoBehaviour
{
    public static FirstStageManager instance;
    [SerializeField] private Transform MovingArrow;
    public int BallValue;
    public List<GameObject> balls = new List<GameObject>();
    [SerializeField] GameObject BallPrefab;
    private float BallSpeed = 42.5f;
    private float BallDistance = 1.9f;
    private Vector3 CurrentPosition;
    private void Awake()
    {
        instance = this;
        
    }
    void Start() 
    {
        BallValue = PlayerPrefs.GetInt("InitialBallUpgradeLevel");
        UIScript.instance.UpdateAmmoText(BallValue);
        InstantiateNewBalls(1);
    }
    private void StartFirstStage()
    {
        CameraMovement.instance.Target = balls[0].transform;
        MoveAllBalls(MovingArrow.rotation.eulerAngles.y);
        BallValue = PlayerPrefs.GetInt("InitialBallUpgradeLevel");
        InstantiateNewBalls(BallValue - 1);
        UIScript.instance.UpdateAmmoText(BallValue);
        UIScript.instance.StartGame();
    }
 
    public void ButtonEvent()
    {
        StartFirstStage();
    }

    public void StopAllBalls()
    {
        foreach(GameObject ball in balls)
        {
            ball.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
    }

    public void MoveAllBalls(float yRotation)
    {
        foreach (GameObject ball in balls)
        {
            ball.GetComponent<Rigidbody>().velocity = new Vector3(BallSpeed * Mathf.Sin(yRotation * Mathf.PI / 180), 0, BallSpeed * Mathf.Cos(yRotation * Mathf.PI / 180));
        }
    }

    public void InstantiateNewBalls(int count)
    {
        int CurrentMaxBall = (PlayerPrefs.GetInt("MaxBallUpgradeLevel") * 5 + 15);
        for (int i=0; i< count; i++)
        {
            if (balls.Count < CurrentMaxBall)
            {
                if (balls.Count > 0)
                {
                    if (balls[balls.Count - 1].GetComponent<Rigidbody>().velocity != Vector3.zero)
                    {
                        CurrentPosition = balls[balls.Count - 1].transform.position - balls[balls.Count - 1].GetComponent<Rigidbody>().velocity.normalized * BallDistance;
                    }
                    else
                    {
                        CurrentPosition = balls[balls.Count - 1].transform.position - Vector3.forward * BallDistance;
                    }
                    
                    GameObject CurrentBall = Instantiate(BallPrefab, CurrentPosition, Quaternion.Euler(Vector3.zero));
                    CurrentBall.GetComponent<BallManager>().isFirstBall = false;
                    CurrentBall.GetComponent<Rigidbody>().velocity = balls[balls.Count - 1].GetComponent<Rigidbody>().velocity;
                    balls.Add(CurrentBall);
                }

                else
                {
                    GameObject CurrentBall = Instantiate(BallPrefab, new Vector3(0, 43.14f, 3.2f), Quaternion.Euler(Vector3.zero));
                    CurrentBall.GetComponent<BallManager>().isFirstBall = true;
                    balls.Add(CurrentBall);
                }
            }
            
        }
    }

    public IEnumerator DestroyAllBalls()
    {
        yield return null;
        foreach (GameObject ball in balls)
        {
            Destroy(ball);    
        }
    }
}
