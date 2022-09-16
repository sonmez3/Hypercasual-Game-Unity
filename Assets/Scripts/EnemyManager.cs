using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance;
    private Animator animator;
    private Rigidbody enemyRigidbody;
    [SerializeField] private Material BlackMaterial;
    private float forwardSpeed = 12f;
    private bool IsDead;
    private void Awake()
    {
        instance = this;

    }

    private void Start()
    {
        enemyRigidbody = GetComponent<Rigidbody>();
        IsDead = false;
        EnemyMove();
        animator = GetComponent<Animator>();
    }

    public void EnemyMove()
    {
        //animator.SetBool("isRunning", true);
        enemyRigidbody.velocity = Vector3.back * forwardSpeed;

    }

    public void EnemyStop()
    {
        //animator.SetBool("isRunning", false);
        enemyRigidbody.velocity = Vector3.zero;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ally"))
        {
            other.transform.GetChild(1).GetComponent<SkinnedMeshRenderer>().material = BlackMaterial;
            gameObject.transform.GetChild(1).GetComponent<SkinnedMeshRenderer>().material = BlackMaterial;
            EnemyStop();
        }
        else if (other.CompareTag("Bullet"))
        {
            if (PlayerPrefs.GetInt("IsVibration") == 1)
            {
                Vibration.VibratePop();
            }
            if (!AudioManager.instance.FindAudioSource("BotDie").isPlaying)
            {
                AudioManager.instance.Play("BotDie");
                
            }
            ThirdStageManager.instance.CurrentGameEarnedMoney += (0.9f + 0.1f * PlayerPrefs.GetInt("IncomeUpgradeLevel"));
            other.GetComponent<AllyBulletManager>().DidCollideWithEnemy = true;
            Instantiate(ThirdStageManager.instance.BlueBlood, transform.position + Vector3.up * 2.5f, Quaternion.Euler(Vector3.zero), ThirdStageManager.instance.EffctsParent);
            Instantiate(ThirdStageManager.instance.BlueEffect, transform.position, Quaternion.Euler(Vector3.zero), ThirdStageManager.instance.EffctsParent);
            //EnemyStop();
            //gameObject.transform.GetChild(1).GetComponent<SkinnedMeshRenderer>().material = BlackMaterial;
            if (!IsDead)
            {
                ThirdStageManager.instance.RemainingEnemies--;
            }
            IsDead = true;
            Destroy(gameObject);
        }
        else if (other.CompareTag("ThirdStageFinish"))
        {
            int CurrentIndex = 0;
            while (CurrentIndex <= SecondStageManager.instance.Allies.Count - 1)
            {
                if (SecondStageManager.instance.Allies[CurrentIndex] != null)
                {
                    Instantiate(ThirdStageManager.instance.BlueBlood, SecondStageManager.instance.Allies[CurrentIndex].transform.position + Vector3.up * 2.5f, Quaternion.Euler(Vector3.zero), ThirdStageManager.instance.EffctsParent);
                    Instantiate(ThirdStageManager.instance.BlueEffect, SecondStageManager.instance.Allies[CurrentIndex].transform.position, Quaternion.Euler(Vector3.zero), ThirdStageManager.instance.EffctsParent);
                    Destroy(SecondStageManager.instance.Allies[CurrentIndex]);
                    break;
                }
                CurrentIndex++;
            }
            if (CurrentIndex <= SecondStageManager.instance.Allies.Count - 1)
            {
                ThirdStageManager.instance.RemainingEnemies--;
                Destroy(gameObject);
            }
            else
            {
                EnemyStop();
                animator.SetTrigger("EnemyIsDance" + Random.Range(1, 4).ToString());
                ThirdStageManager.instance.DidWeLost = true;
                //Debug.Log("We have lost");
            }
        
        }
    }
}
