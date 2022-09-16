//using UnityEngine;
//using System.Collections;
//using TMPro;

//public class PlayerManager : MonoBehaviour
//{
//    public static PlayerManager instance;

//    public int ZombiePreviousTransformationLevel = 0;
//    public int ZombieCurrentTransformationLevel = 0;
//    public new Rigidbody rigidbody;
//    public TextMeshProUGUI levelText;
//    public Animator animator;
//    public int level;
//    public float forwardSpeed = 14f;
//    private bool heldDown = false;
//    private Vector2 touchedPosition = Vector2.zero;
//    private float roadWidth; // half width
//    public Human human;
//    public TwoHuman twohuman;
//    public int AttackingHumanCount = 0;
//    public int CoinAmountPerKill;
//    private float TriggerEnterObjectX;
//    public int CurrentEndGamePlatformNumber = 0;
//    public int UpgradeZombieRequiredLevel;
//    [SerializeField] private GameObject ZombieLowerTransformationEffect;
//    [SerializeField] private GameObject ZombieHigherTransformationEffect;
//    [SerializeField] private Material[] GlowMaterials;

//    public float MinClampValue;
//    public float MaxClampValue;
//    #region Unity Methods
//    private void Awake()
//    {
//        instance = this;
//        animator = transform.GetChild(0).GetComponent<Animator>();
//        rigidbody = GetComponent<Rigidbody>();
//        roadWidth = Constants.PlatformHalfWidth - Constants.PlatformLineWidth - Constants.PlayerWidth - Constants.ExtraRoadWidthReduction;
//        MinClampValue = -1 * roadWidth;
//        MaxClampValue = roadWidth;
//    }
//    private void Start()
//    {
//        level = Constants.GetPlayerLevel();
//        levelText.text = level.ToString();
//        UpgradeZombieRequiredLevel = 15 * (Constants.GetMapLevel() + 1);
//        ZombieCurrentTransformationLevel = Mathf.Min((level / UpgradeZombieRequiredLevel), 17);
//        ZombiePreviousTransformationLevel = ZombieCurrentTransformationLevel;
//        transform.GetChild(ZombieCurrentTransformationLevel).gameObject.SetActive(true);
//        animator = transform.GetChild(ZombieCurrentTransformationLevel).GetComponent<Animator>();

//    }
//    void Update()
//    {

//        if (Input.GetMouseButtonDown(0) && UIManager.instance.DidLevelStart && !UIManager.instance.DidLevelEnd)
//        {

//            if (!heldDown)
//            {
//                heldDown = true;
//                touchedPosition = Input.mousePosition;
//                //Debug.Log("held down");
//                StartCoroutine(EvaluateUserInput());
//            }

//        }

//        if (Input.GetMouseButtonUp(0))
//        {
//            heldDown = false;
//        }
//    }

//    private void OnTriggerEnter(Collider other)
//    {
//        if (other.CompareTag("Gate"))
//        {

//            Gate gate = other.GetComponent<Gate>();
//            TriggerEnterObjectX = gate.transform.position.x;
//            if (TriggerEnterObjectX * transform.position.x > 0)
//            {
//                other.transform.GetChild(2).gameObject.SetActive(false);
//                if (gate != null)
//                {
//                    switch (gate.sign)
//                    {
//                        case 0:
//                            level += gate.value;
//                            AudioManager.instance.Play("BlueGate");
//                            break;
//                        case 1:
//                            level *= gate.value;
//                            AudioManager.instance.Play("BlueGate");
//                            break;
//                        case 2:
//                            level -= gate.value;
//                            AudioManager.instance.Play("RedGate");
//                            break;
//                        case 3:
//                            level /= gate.value;
//                            AudioManager.instance.Play("RedGate");
//                            break;
//                        default:
//                            break;
//                    }
//                    if (level > 0)
//                    {
//                        levelText.text = level.ToString();
//                    }
//                    else
//                    {
//                        animator.SetTrigger("isDie");
//                        LostGame();
//                    }
//                    ZombieCurrentTransformationLevel = Mathf.Min((level / UpgradeZombieRequiredLevel), 17);
//                    if (ZombiePreviousTransformationLevel > ZombieCurrentTransformationLevel)
//                    {
//                        animator = transform.GetChild(ZombieCurrentTransformationLevel).GetComponent<Animator>();

//                        StartCoroutine(LowerTransformationRoutine(ZombiePreviousTransformationLevel, ZombieCurrentTransformationLevel));

//                    }
//                    else if (ZombiePreviousTransformationLevel < ZombieCurrentTransformationLevel)
//                    {
//                        animator = transform.GetChild(ZombieCurrentTransformationLevel).GetComponent<Animator>();

//                        StartCoroutine(HigherTransformationRoutine(ZombiePreviousTransformationLevel, ZombieCurrentTransformationLevel));

//                    }


//                }
//            }


//        }
//        else if (other.CompareTag("MovingGate"))
//        {
//            Gate gate = other.GetComponent<Gate>();
//            other.transform.GetChild(2).gameObject.SetActive(false);
//            if (gate != null)
//            {
//                switch (gate.sign)
//                {
//                    case 0:
//                        level += gate.value;
//                        AudioManager.instance.Play("BlueGate");
//                        break;
//                    case 1:
//                        level *= gate.value;
//                        AudioManager.instance.Play("BlueGate");
//                        break;
//                    case 2:
//                        level -= gate.value;
//                        AudioManager.instance.Play("RedGate");
//                        break;
//                    case 3:
//                        level /= gate.value;
//                        AudioManager.instance.Play("RedGate");
//                        break;
//                    default:
//                        break;
//                }
//                if (level > 0)
//                {
//                    levelText.text = level.ToString();
//                }
//                else
//                {
//                    animator.SetTrigger("isDie");
//                    LostGame();
//                }
//                ZombieCurrentTransformationLevel = Mathf.Min((level / UpgradeZombieRequiredLevel), 17);
//                if (ZombiePreviousTransformationLevel > ZombieCurrentTransformationLevel)
//                {
//                    animator = transform.GetChild(ZombieCurrentTransformationLevel).GetComponent<Animator>();

//                    StartCoroutine(LowerTransformationRoutine(ZombiePreviousTransformationLevel, ZombieCurrentTransformationLevel));

//                }
//                else if (ZombiePreviousTransformationLevel < ZombieCurrentTransformationLevel)
//                {
//                    animator = transform.GetChild(ZombieCurrentTransformationLevel).GetComponent<Animator>();

//                    StartCoroutine(HigherTransformationRoutine(ZombiePreviousTransformationLevel, ZombieCurrentTransformationLevel));

//                }


//            }
//        }

//        else if (other.CompareTag("Human"))
//        {
//            TriggerEnterObjectX = other.transform.position.x;
//            if (TriggerEnterObjectX * transform.position.x > 0)
//            {
//                AttackingHumanCount = 1;
//                human = other.GetComponentInChildren<Human>();
//                if (human != null)
//                {
//                    if (level >= human.value)
//                    {
//                        //level += human.value;
//                        //levelText.text = level.ToString();
//                        //human.gameObject.SetActive(false); // TODO: Human zombie transformation animation
//                        animator.SetTrigger("isAttacking");
//                        // TODO: animation switch to eat

//                    }
//                    //else  // lost the game
//                    //{
//                    //    LostGame();  

//                    //}
//                }
//            }

//        }

//        else if (other.CompareTag("HumanChild"))
//        {
//            TriggerEnterObjectX = other.transform.position.x;
//            if (TriggerEnterObjectX * transform.position.x > 0)
//            {
//                if (level < human.value)
//                {
//                    other.GetComponentInChildren<Animator>().SetTrigger("IsAttack");
//                    animator.SetTrigger("isDie");
//                    LostGame();
//                }
//            }

//        }

//        else if (other.CompareTag("TwoHuman"))
//        {
//            TriggerEnterObjectX = other.transform.position.x;
//            if (TriggerEnterObjectX * transform.position.x > 0)
//            {
//                AttackingHumanCount = 2;
//                twohuman = other.GetComponent<TwoHuman>();
//                if (twohuman != null)
//                {
//                    if (level >= twohuman.value)
//                    {
//                        animator.SetTrigger("isAttacking");
//                    }
//                }
//            }

//        }

//        else if (other.CompareTag("TwoHumanChild"))
//        {
//            TriggerEnterObjectX = other.transform.position.x;
//            if (TriggerEnterObjectX * transform.position.x > 0)
//            {
//                //twohuman = other.GetComponentInParent<TwoHuman>(); //?
//                if (level < twohuman.value)
//                {
//                    other.transform.parent.transform.GetChild(1).GetChild(0).GetComponent<Animator>().SetTrigger("IsAttack");
//                    other.transform.parent.transform.GetChild(2).GetChild(0).GetComponent<Animator>().SetTrigger("IsAttack");
//                    animator.SetTrigger("isDie");
//                    LostGame();
//                }
//            }

//        }
//        else if (other.CompareTag("EndGamePlatform"))
//        {
//            if (Constants.GetVibration() == 1)
//            {
//                Vibration.VibratePop();
//            }
//            other.transform.GetChild(0).GetComponent<MeshRenderer>().material = GlowMaterials[CurrentEndGamePlatformNumber];
//            other.transform.GetChild(1).GetComponent<MeshRenderer>().material = GlowMaterials[CurrentEndGamePlatformNumber];
//            AudioManager.instance.Play("HumanKilling");
//            CurrentEndGamePlatformNumber++;
//            other.transform.GetChild(4).GetChild(1).gameObject.SetActive(true);
//            other.transform.GetChild(5).GetChild(1).gameObject.SetActive(true);
//            other.transform.GetChild(6).GetChild(1).gameObject.SetActive(true);

//            other.transform.GetChild(4).GetChild(0).GetComponent<Animator>().SetTrigger("isDead");
//            other.transform.GetChild(5).GetChild(0).GetComponent<Animator>().SetTrigger("isDead");
//            other.transform.GetChild(6).GetChild(0).GetComponent<Animator>().SetTrigger("isDead");

//        }
//        else if (other.CompareTag("EndGameLastPlatform"))
//        {
//            if (Constants.GetVibration() == 1)
//            {
//                Vibration.VibratePop();
//            }
//            other.transform.GetChild(0).GetComponent<MeshRenderer>().material = GlowMaterials[CurrentEndGamePlatformNumber];
//            other.transform.GetChild(1).GetComponent<MeshRenderer>().material = GlowMaterials[CurrentEndGamePlatformNumber];
//            AudioManager.instance.Play("HumanKilling");
//            CurrentEndGamePlatformNumber++;
//            other.transform.GetChild(4).GetChild(1).gameObject.SetActive(true);

//            other.transform.GetChild(4).GetChild(0).GetComponent<Animator>().SetTrigger("isDead");
//        }

//        //else if (other.CompareTag("Wall"))
//        //{
//        //    if (transform.position.x > 0)
//        //    {
//        //        MinClampValue = 0;
//        //    }
//        //    else
//        //    {
//        //        MaxClampValue = 0;
//        //    }
//        //}

//        //else if (other.CompareTag("WallChild"))
//        //{
//        //    if(transform.position.x > 0)
//        //    {
//        //        transform.position += new Vector3(1.2f, 0, 0);
//        //    }
//        //    else
//        //    {
//        //        transform.position += new Vector3(-1.2f, 0, 0);
//        //    }
//        //}
//    }

//    //private void OnTriggerExit(Collider other)
//    //{
//    //    if (other.CompareTag("Wall"))
//    //    {
//    //        MinClampValue = -1 * roadWidth;
//    //        MaxClampValue = roadWidth;

//    //    }
//    //}

//    //private void OnTriggerStay(Collider other)
//    //{
//    //    if (other.CompareTag("WallChild"))
//    //    {
//    //        if (transform.position.x > 0)
//    //        {
//    //            transform.position += new Vector3(1.2f, 0, 0);
//    //        }
//    //        else
//    //        {
//    //            transform.position += new Vector3(-1.2f, 0, 0);
//    //        }
//    //    }
//    //}
//    #endregion

//    public void Stop()
//    {
//        animator.SetBool("isRunning", false);
//        rigidbody.velocity = Vector3.zero;
//    }
//    public void ResetPlayer()
//    {
//        transform.position = Constants.PlayerStartingPosition;
//        level = Constants.GetPlayerLevel();
//        levelText.text = level.ToString();
//    }
//    public void Move()
//    {
//        rigidbody.velocity = Vector3.forward * forwardSpeed;
//    }

//    private IEnumerator EvaluateUserInput()
//    {
//        while (true)
//        {
//            if (!UIManager.instance.DidLevelEnd)
//            {
//                transform.position += new Vector3((Input.mousePosition.x - touchedPosition.x) / 300f * roadWidth, 0, 0);

//                float positionX = Mathf.Clamp(transform.position.x, MinClampValue, MaxClampValue);

//                transform.position = new Vector3(positionX, transform.position.y, transform.position.z);
//                touchedPosition = Input.mousePosition;
//            }
//            if (!heldDown)
//            {
//                //Debug.Log("held down false");
//                yield break;
//            }
//            yield return new WaitForFixedUpdate();


//        }
//    }

//    private void LostGame()
//    {
//        AudioManager.instance.Play("FinishFail");
//        UIManager.instance.DidLevelEnd = true;
//        Stop();
//        UIManager.instance.EndGamePanel.gameObject.SetActive(true);
//        UIManager.instance.EndGamePanel.GetChild(1).gameObject.SetActive(true);

//        // TODO: animation switch to die
//    }

//    public IEnumerator LowerTransformationRoutine(int OldTransformationLevel, int NewTransformationLevel)
//    {
//        ZombieLowerTransformationEffect.SetActive(true);
//        ZombieLowerTransformationEffect.GetComponent<ParticleSystem>().Play();
//        AudioManager.instance.Play("ZombieDowngrade");
//        yield return new WaitForSeconds(0.1f);
//        ZombiePreviousTransformationLevel = ZombieCurrentTransformationLevel;
//        //transform.GetChild(OldTransformationLevel).gameObject.SetActive(false);
//        for (int i = 0; i < 18; i++)
//        {
//            transform.GetChild(i).gameObject.SetActive(false);
//        }
//        transform.GetChild(NewTransformationLevel).gameObject.SetActive(true);
//        animator.SetBool("isRunning", true);
//        yield return new WaitForSeconds(0.6f);
//        ZombieLowerTransformationEffect.SetActive(false);

//    }

//    public IEnumerator HigherTransformationRoutine(int OldTransformationLevel, int NewTransformationLevel)
//    {
//        ZombieHigherTransformationEffect.SetActive(true);
//        ZombieHigherTransformationEffect.GetComponent<ParticleSystem>().Play();
//        AudioManager.instance.Play("ZombieUpgrade");
//        yield return new WaitForSeconds(0.1f);
//        ZombiePreviousTransformationLevel = ZombieCurrentTransformationLevel;
//        //transform.GetChild(OldTransformationLevel).gameObject.SetActive(false);
//        for (int i = 0; i < 18; i++)
//        {
//            transform.GetChild(i).gameObject.SetActive(false);
//        }
//        transform.GetChild(NewTransformationLevel).gameObject.SetActive(true);
//        animator.SetBool("isRunning", true);
//        yield return new WaitForSeconds(0.6f);
//        ZombieHigherTransformationEffect.SetActive(false);
//    }

//    public IEnumerator MoveToCenter()
//    {
//        float currentX = transform.position.x;
//        for (int i = 0; i < 10; i++)
//        {
//            transform.position = new Vector3(currentX - (currentX / 10) * i, transform.position.y, transform.position.z);
//            yield return new WaitForSeconds(0.01f);
//        }
//        transform.position = new Vector3(0, transform.position.y, transform.position.z);
//    }
//}
