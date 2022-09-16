using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class ThirdStageManager : MonoBehaviour
{
    public static ThirdStageManager instance;
    private float[] XCoordinates = {0.16f, 2.56f, -2.24f, 5.05f, -4.95f, 7.63f, -7.66f };
    private float MaxZCoordinateForBarrier = 303.13f;
    private float ZDistanceForBarrier = 2f;
    public bool DidGameComplete;
    private int CurrentGameLevel;
    private int[] EnemyNumbersForEachLevel = { 100, 150, 200 };
    private Vector3 EnemySummonPosition = new Vector3(0, 42.14f, 420);
    [SerializeField] GameObject EnermyPrefab;
    public int RemainingEnemies;
    public bool DidWeLost;
    private WaitForSeconds ZeroPointFive = new WaitForSeconds(0.5f);
    public float CurrentGameEarnedMoney;
    public ParticleSystem BlueBlood;
    public ParticleSystem BlueEffect;
    public Transform EffctsParent;
    private void Awake()
    {
        instance = this;
        
    }

    private void Start()
    {
        CurrentGameLevel = PlayerPrefs.GetInt("CurrentGameLevel");
        CurrentGameEarnedMoney = 0;
        DidGameComplete = false;
        DidWeLost = false;
        RemainingEnemies = EnemyNumbersForEachLevel[CurrentGameLevel - 1];
        StartCoroutine(GameEndChecker());
        
    }
    public void StartThirdStage()
    {
        int CurrentIndex = 0;
        int CurrentRow = 0;
        
        foreach (GameObject ally in SecondStageManager.instance.Allies)
        {
            if (ally != null)
            {
                AllyManager CurrentAllyManager = ally.GetComponent<AllyManager>();
                CurrentAllyManager.ShouldMove = false;
                ally.GetComponent<NavMeshAgent>().enabled = true;
                CurrentAllyManager.StartMoveToBarrierRoutine(new Vector3(XCoordinates[CurrentIndex], 42.14f, MaxZCoordinateForBarrier - ZDistanceForBarrier * CurrentRow));
                CurrentIndex++;
                ally.GetComponent<Animator>().SetTrigger("IsFiring");
                if (CurrentIndex == 7)
                {
                    CurrentIndex = 0;
                    CurrentRow++;
                }
             }
        }
        CameraMovement.instance.GetComponent<Animator>().enabled = true;
        StartCoroutine(SummonEnemies());
        PlayerController.instance.StartDestroyAllAmmosRoutine();
    }

    private IEnumerator SummonEnemies()
    {
        yield return null;
        for (int i=0; i < EnemyNumbersForEachLevel[CurrentGameLevel - 1]; i++)
        {
            Instantiate(EnermyPrefab, EnemySummonPosition + new Vector3(Random.Range(-7f, 7f), 0, Random.Range(-5f, 5f)), Quaternion.Euler(new Vector3(0,-180,0)));
            yield return new WaitForSeconds(0.03f);
        }
    }

    private IEnumerator GameEndChecker()
    {
        while (!DidWeLost)
        {
            if (RemainingEnemies <= 0)
            {
                break;
            }
            yield return ZeroPointFive;
        }
        DidGameComplete = true;
        if (DidWeLost)
        {
            AudioManager.instance.Play("FinishFail");
            UIScript.instance.GameEndUI(false);
            StartCoroutine(DestroyAllEffects());
        }
        else
        {
            AudioManager.instance.Play("FinishSuccess");
            UIScript.instance.UpdateCurrentGameMoneyText((int) CurrentGameEarnedMoney);
            UIScript.instance.GameEndUI(true);
            StartCoroutine(DestroyAllEffects());
        }
    }

    private IEnumerator DestroyAllEffects()
    {
        yield return null;
        for (int i= EffctsParent.childCount -1; i>= 0; i--)
        {
            Destroy(EffctsParent.GetChild(i).gameObject);
        }
    }

}
