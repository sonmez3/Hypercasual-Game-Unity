using UnityEngine;
using UnityEngine.AI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private NavMeshSurface surface;
    private void Awake()
    {
        //if(!PlayerPrefs.HasKey("BallValue"))
        //{
        //    PlayerPrefs.SetInt("BallValue", 1);
        //}

        if (!PlayerPrefs.HasKey("CurrentGameLevel"))
        {
            PlayerPrefs.SetInt("CurrentGameLevel", 1);
        }

        if (!PlayerPrefs.HasKey("SoundEffectVolumeLevel"))
        {
            PlayerPrefs.SetFloat("SoundEffectVolumeLevel", 1f);
        }

        if (!PlayerPrefs.HasKey("IsVibration"))
        {
            PlayerPrefs.SetInt("IsVibration", 1);
        }

        if (!PlayerPrefs.HasKey("TotalMoney"))
        {
            PlayerPrefs.SetInt("TotalMoney", 10000);
        }

        if (!PlayerPrefs.HasKey("MaxBallUpgradeLevel"))
        {
            PlayerPrefs.SetInt("MaxBallUpgradeLevel", 1);
        }

        if (!PlayerPrefs.HasKey("InitialBallUpgradeLevel"))
        {
            PlayerPrefs.SetInt("InitialBallUpgradeLevel", 1);
        }

        if (!PlayerPrefs.HasKey("IncomeUpgradeLevel"))
        {
            PlayerPrefs.SetInt("IncomeUpgradeLevel", 1);
        }

        

        Vibration.Init();
        surface.BuildNavMesh();
    }

    private void Start()
    {
        GameAnalyticsSDK.GameAnalytics.Initialize();
    }
}
