using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    private Animator animator;
    private Rigidbody characterRigidbody;
    private float roadWidth;
    private float PlatformHalfWidth = 10f;
    private float PlatformLineWidth = 1f;
    private float PlayerWidth = 4f;
    private float ExtraRoadWidthReduction = 2f;
    public bool heldDown = false;
    private Vector2 touchedPosition = Vector2.zero;
    private float forwardSpeed = 10f;
    private Vector3 PlayerStartingPosition = new Vector3(0, 42.28f, 91.28f);  //TO DO: must be determined
    [SerializeField] private Material AllyMaterial;
    [SerializeField] private GameObject AmmoPrefab;
    public List<GameObject> Bullets = new List<GameObject>();
    private int CurrentGameBulletCount;
    private float AmmoSpeed = 42f;
    private void Awake()
    {
        instance = this;
        animator = GetComponent<Animator>();
        characterRigidbody = GetComponent<Rigidbody>();
        roadWidth = PlatformHalfWidth - PlatformLineWidth - PlayerWidth - ExtraRoadWidthReduction;
        CurrentGameBulletCount = 0;
    }
    void Update()
    {

        if (Input.GetMouseButtonDown(0) && SecondStageManager.instance.DidSecondStageStart && !SecondStageManager.instance.DidSecondStageEnd)
        {

            if (!heldDown)
            {
                heldDown = true;
                touchedPosition = Input.mousePosition;
                StartCoroutine(EvaluateUserInput());
            }

        }

        if (Input.GetMouseButtonUp(0))
        {
            heldDown = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("WhiteHuman") && CurrentGameBulletCount > 0)
        {
            AudioManager.instance.Play("FiringGun");
            other.tag = "Ally";
            other.GetComponent<BoxCollider>().enabled = false;
            GameObject currentBullet = Bullets[CurrentGameBulletCount - 1];
            currentBullet.transform.position = transform.position + Vector3.forward;
            currentBullet.transform.localScale = new Vector3(5, 5, 3);
            currentBullet.transform.rotation = Quaternion.Euler(Vector3.zero);
            currentBullet.GetComponent<Rigidbody>().velocity = Vector3.forward * AmmoSpeed;
            currentBullet.GetComponent<AmmoManager>().ShouldArrangeXCoordinate = false;
            CurrentGameBulletCount--;
            UIScript.instance.UpdateAmmoText(CurrentGameBulletCount);
        }
        else if (other.CompareTag("AmmoBall"))
        {
            if (other.GetComponent<BallManager>().isFirstBall)
            {
                StartCoroutine(StackAmmos());
                other.gameObject.SetActive(false);
            }
            else
            {
                other.gameObject.SetActive(false);
            }
        }

        else if (other.CompareTag("SecondStageFinish"))
        {
            SecondStageManager.instance.DidSecondStageEnd = true;
            Stop();
            animator.SetTrigger("IsCommand");
            ThirdStageManager.instance.StartThirdStage();
        }
    }

    public IEnumerator GainNewAlly(Collider other)
    {
        yield return null;
        AllyManager allyManager = other.GetComponentInParent<AllyManager>();
        other.GetComponentInParent<Animator>().enabled = true;
        other.GetComponent<SkinnedMeshRenderer>().material = AllyMaterial;
        SecondStageManager.instance.Allies.Add(other.transform.parent.gameObject);
        allyManager.StartMoveharactersBehindRoutine(transform.position);

    }

    public void startGainNewAllyRoutine(Collider other)
    {
        StartCoroutine(GainNewAlly(other));
    }

    private IEnumerator EvaluateUserInput()
    {
        while (true)
        {
            if (!SecondStageManager.instance.DidSecondStageEnd)
            {
                transform.position += new Vector3((Input.mousePosition.x - touchedPosition.x) / 300f * roadWidth, 0, 0);

                float positionX = Mathf.Clamp(transform.position.x, -7.5f, 7.5f);

                transform.position = new Vector3(positionX, transform.position.y, transform.position.z);
                touchedPosition = Input.mousePosition;
            }
            if (!heldDown)
            {
                yield break;
            }
            yield return new WaitForFixedUpdate();


        }
    }

    public void Stop()
    {
        characterRigidbody.velocity = Vector3.zero;
    }
    public void ResetPlayer()
    {
        transform.position = PlayerStartingPosition;
        
    }
    public void Move()
    {
        animator.SetBool("IsRunning", true);
        characterRigidbody.velocity = Vector3.forward * forwardSpeed;
        
    }

    public IEnumerator MoveToCenter()
    {
        float currentX = transform.position.x;
        for (int i = 0; i < 10; i++)
        {
            transform.position = new Vector3(currentX - (currentX / 10) * i, transform.position.y, transform.position.z);
            yield return new WaitForSeconds(0.01f);
        }
        transform.position = new Vector3(0, transform.position.y, transform.position.z);
    }

    private IEnumerator StackAmmos()
    {
        AudioManager.instance.Play("BulletStacking");
        CurrentGameBulletCount = FirstStageManager.instance.balls.Count;
        for (int i=0; i< CurrentGameBulletCount; i++)
        {
            GameObject CurrentAmmo = Instantiate(AmmoPrefab, new Vector3(0, 44.1f + 0.25f * i, 89.9f), Quaternion.Euler(new Vector3(0,90,0)));
            Bullets.Add(CurrentAmmo);
            yield return new WaitForSeconds(0.04f);

        }
        
        SecondStageManager.instance.SecondButtonCode();
        StartCoroutine(FirstStageManager.instance.DestroyAllBalls());
    }

    public void StartDestroyAllAmmosRoutine()
    {
        StartCoroutine(DestroyAllAmmos());
    }

    private IEnumerator DestroyAllAmmos()
    {
        yield return null;
        foreach(GameObject bullet in Bullets)
        {
            Destroy(bullet);
        }
    }
}
