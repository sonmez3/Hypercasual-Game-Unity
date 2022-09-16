using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class BallManager : MonoBehaviour
{
    public static BallManager instance;
    private float BallSpeed = 42.5f;
    
    private Rigidbody BallRigidbody;
    public bool isFirstBall;
    public bool shouldCollideWithWall;
    private WaitForSeconds ZeroPointTwo = new WaitForSeconds(0.15f);
    private NavMeshAgent agent;
    private void Awake()
    {
        instance = this;
    }


    void Start()
    {
        BallRigidbody = GetComponent<Rigidbody>();
        shouldCollideWithWall = true;
        agent = GetComponent<NavMeshAgent>();
       
    }

    public void MoveBall(float yRotation)
    {
        BallRigidbody.velocity = new Vector3(BallSpeed * Mathf.Sin(yRotation * Mathf.PI / 180), 0,BallSpeed * Mathf.Cos(yRotation * Mathf.PI / 180));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Wall") && shouldCollideWithWall && transform.position.x < 9.6f && transform.position.x > -9.6f)  // 
        {
            StartCoroutine(ChangeCollisionStatus());
            BallRigidbody.velocity = new Vector3(-1 * (BallRigidbody.velocity.x), BallRigidbody.velocity.y, BallRigidbody.velocity.z);
        }
        else if (other.CompareTag("Gate") && isFirstBall) 
        {
            AudioManager.instance.Play("BlueGate");
            Gate gate = other.gameObject.GetComponent<Gate>();
            if (!gate.isSingle)
            {
                if (other.transform.position.x * transform.position.x > 0)
                {
                    string CurrentSign = gate.sign;
                    if (CurrentSign.Equals("+"))
                    {
                        FirstStageManager.instance.BallValue += gate.value;
                        UIScript.instance.UpdateAmmoText(FirstStageManager.instance.BallValue);
                        other.tag = "GatePassed";
                        other.transform.GetChild(2).gameObject.SetActive(false);
                        FirstStageManager.instance.InstantiateNewBalls(gate.value);
                    }
                    
                    else
                    {
                        int CurrentCount = FirstStageManager.instance.BallValue * (gate.value - 1);
                        FirstStageManager.instance.BallValue *= gate.value;
                        other.tag = "GatePassed";
                        other.transform.GetChild(2).gameObject.SetActive(false);
                        UIScript.instance.UpdateAmmoText(FirstStageManager.instance.BallValue);
                        FirstStageManager.instance.InstantiateNewBalls(CurrentCount);
                    }
                    
                }
                
            }
            else
            {
                string CurrentSign = gate.sign;
                if (CurrentSign.Equals("+"))
                {
                    FirstStageManager.instance.BallValue += gate.value;
                    UIScript.instance.UpdateAmmoText(FirstStageManager.instance.BallValue);
                    other.tag = "GatePassed";
                    other.transform.GetChild(2).gameObject.SetActive(false);
                    FirstStageManager.instance.InstantiateNewBalls(gate.value);
                }
                
                else 
                {
                    int CurrentCount = FirstStageManager.instance.BallValue * (gate.value - 1);
                    FirstStageManager.instance.BallValue *= gate.value;
                    other.tag = "GatePassed";
                    other.transform.GetChild(2).gameObject.SetActive(false);
                    UIScript.instance.UpdateAmmoText(FirstStageManager.instance.BallValue);
                    FirstStageManager.instance.InstantiateNewBalls(CurrentCount);
                }
                
            }
            
        }

        else if (other.CompareTag("FirstStageFinish"))
        {
            BallRigidbody.velocity = Vector3.zero;
            GetComponent<SphereCollider>().isTrigger = true;
            GetComponent<NavMeshAgent>().enabled = true;
            agent.SetDestination(new Vector3(0, 43.14f, 91.28f));
        }

       
    }

    


    private IEnumerator ChangeCollisionStatus()
    {
        shouldCollideWithWall = false;
        yield return ZeroPointTwo;
        shouldCollideWithWall = true;
    }

    
}
