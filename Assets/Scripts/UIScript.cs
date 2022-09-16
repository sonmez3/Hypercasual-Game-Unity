using UnityEngine;
using TMPro;

public class UIScript : MonoBehaviour
{
    public static UIScript instance;
    [SerializeField] private GameObject StartGameButton;
    [SerializeField] private GameObject UpgradeIncomeButton;
    [SerializeField] private GameObject UpgradeMaxBallButton;
    [SerializeField] private GameObject UpgradeInitialBallButton;
    [SerializeField] private GameObject SettingsButton;
    [SerializeField] private TextMeshProUGUI AmmoText;
    [SerializeField] private GameObject SettingsPanel;
    [SerializeField] private GameObject TapToStartText;
    [SerializeField] private TextMeshProUGUI MoneyText;
    [SerializeField] private TextMeshProUGUI MaxBallUpgradeLevelText;
    [SerializeField] private TextMeshProUGUI MaxBallUpgradeMoneyText;
    [SerializeField] private TextMeshProUGUI InitialBallUpgradeLevelText;
    [SerializeField] private TextMeshProUGUI InitialBallUpgradeMoneyText;
    [SerializeField] private TextMeshProUGUI IncomeUpgradeLevelText;
    [SerializeField] private TextMeshProUGUI IncomeUpgradeMoneyText;
    [SerializeField] private GameObject GreyImageMaxBall;
    [SerializeField] private GameObject GreyImageInitialBall;
    [SerializeField] private GameObject GreyImageIncome;
    [SerializeField] private Transform LevelParent;
    [SerializeField] private TextMeshProUGUI LevelText;
    [SerializeField] private TextMeshProUGUI CurrentGameMoneyText;
    [SerializeField] private GameObject EndGamePanel;
    [SerializeField] private GameObject EndGameLosePanel;
    [SerializeField] private GameObject EndGameWinPanel;
    private int MaxBallUpgradeCostMultiplier = 250;
    private int InitialBallUpgradeCostMultiplier = 300;
    private int IncomeUpgradeCostMultiplier = 150;
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        UpdateMoneyText(PlayerPrefs.GetInt("TotalMoney"));
        MaxBallUpgradeLevelText.text = "Max Ball: " + (PlayerPrefs.GetInt("MaxBallUpgradeLevel") * 5 + 15).ToString();
        MaxBallUpgradeMoneyText.text = (MaxBallUpgradeCostMultiplier * PlayerPrefs.GetInt("MaxBallUpgradeLevel")).ToString();
        InitialBallUpgradeLevelText.text = "Ball: " + PlayerPrefs.GetInt("InitialBallUpgradeLevel").ToString();
        InitialBallUpgradeMoneyText.text = (InitialBallUpgradeCostMultiplier * PlayerPrefs.GetInt("InitialBallUpgradeLevel")).ToString();
        IncomeUpgradeLevelText.text = "Level " + PlayerPrefs.GetInt("IncomeUpgradeLevel").ToString();
        IncomeUpgradeMoneyText.text = (IncomeUpgradeCostMultiplier * PlayerPrefs.GetInt("IncomeUpgradeLevel")).ToString();
        LevelText.text = "Lvl." + PlayerPrefs.GetInt("CurrentGameLevel").ToString();
        if (PlayerPrefs.GetInt("TotalMoney") < MaxBallUpgradeCostMultiplier * PlayerPrefs.GetInt("MaxBallUpgradeLevel"))
        {
            GreyImageMaxBall.SetActive(true);
        }
        if (PlayerPrefs.GetInt("TotalMoney") < InitialBallUpgradeCostMultiplier * PlayerPrefs.GetInt("InitialBallUpgradeLevel"))
        {
            GreyImageInitialBall.SetActive(true);
        }
        if (PlayerPrefs.GetInt("TotalMoney") < IncomeUpgradeCostMultiplier * PlayerPrefs.GetInt("IncomeUpgradeLevel"))
        {
            GreyImageIncome.SetActive(true);
        }

        //InstantiateManager.instance.InstantiateLevel(PlayerPrefs.GetInt("CurrentGameLevel"));
    }

    public void StartGame()
    {
        StartGameButton.SetActive(false);
        UpgradeIncomeButton.SetActive(false);
        UpgradeMaxBallButton.SetActive(false);
        UpgradeInitialBallButton.SetActive(false);
        SettingsButton.SetActive(false);
        GreyImageIncome.SetActive(false);
        GreyImageInitialBall.SetActive(false);
        GreyImageMaxBall.SetActive(false);
        TapToStartText.SetActive(false);
    }

    public void UpdateAmmoText(int ammo)
    {
        AmmoText.text = Mathf.Min(ammo, (PlayerPrefs.GetInt("MaxBallUpgradeLevel") * 5 + 15)).ToString();
    }

    public void SettingsButtonCode()
    {
        SettingsPanel.SetActive(true);
    }

    public void SettingsCloseButtonCode()
    {
        SettingsPanel.transform.GetChild(1).GetComponent<Animator>().SetTrigger("IsClosing");
    }

    public void UpdateMoneyText(int money)
    {
        MoneyText.text = money.ToString();
    }

    public void UpdateCurrentGameMoneyText(int money)
    {
        CurrentGameMoneyText.text = money.ToString();
    }

    public void UpgradeMaxBall()
    {
        if (PlayerPrefs.GetInt("TotalMoney") >= MaxBallUpgradeCostMultiplier * PlayerPrefs.GetInt("MaxBallUpgradeLevel"))
        {
            AudioManager.instance.Play("UpgradeSesi");
            PlayerPrefs.SetInt("TotalMoney", PlayerPrefs.GetInt("TotalMoney") - MaxBallUpgradeCostMultiplier * PlayerPrefs.GetInt("MaxBallUpgradeLevel"));
            UpdateMoneyText(PlayerPrefs.GetInt("TotalMoney"));
            PlayerPrefs.SetInt("MaxBallUpgradeLevel", PlayerPrefs.GetInt("MaxBallUpgradeLevel") + 1);
            MaxBallUpgradeLevelText.text = "Max Ball: " + (PlayerPrefs.GetInt("MaxBallUpgradeLevel") * 5 + 15).ToString();
            MaxBallUpgradeMoneyText.text = (MaxBallUpgradeCostMultiplier * PlayerPrefs.GetInt("MaxBallUpgradeLevel")).ToString();
            if (PlayerPrefs.GetInt("TotalMoney") < MaxBallUpgradeCostMultiplier * PlayerPrefs.GetInt("MaxBallUpgradeLevel"))
            {
                GreyImageMaxBall.SetActive(true);
            }
            if (PlayerPrefs.GetInt("TotalMoney") < InitialBallUpgradeCostMultiplier * PlayerPrefs.GetInt("InitialBallUpgradeLevel"))
            {
                GreyImageInitialBall.SetActive(true);
            }
            if (PlayerPrefs.GetInt("TotalMoney") < IncomeUpgradeCostMultiplier * PlayerPrefs.GetInt("IncomeUpgradeLevel"))
            {
                GreyImageIncome.SetActive(true);
            }
        }
    }

    public void UpgradeInitialBall()
    {
        if (PlayerPrefs.GetInt("TotalMoney") >= InitialBallUpgradeCostMultiplier * PlayerPrefs.GetInt("InitialBallUpgradeLevel"))
        {
            AudioManager.instance.Play("UpgradeSesi");
            PlayerPrefs.SetInt("TotalMoney", PlayerPrefs.GetInt("TotalMoney") - InitialBallUpgradeCostMultiplier * PlayerPrefs.GetInt("InitialBallUpgradeLevel"));
            UpdateMoneyText(PlayerPrefs.GetInt("TotalMoney"));
            PlayerPrefs.SetInt("InitialBallUpgradeLevel", PlayerPrefs.GetInt("InitialBallUpgradeLevel") + 1);
            InitialBallUpgradeLevelText.text = "Ball: " + PlayerPrefs.GetInt("InitialBallUpgradeLevel").ToString();
            InitialBallUpgradeMoneyText.text = (InitialBallUpgradeCostMultiplier * PlayerPrefs.GetInt("InitialBallUpgradeLevel")).ToString();
            if (PlayerPrefs.GetInt("TotalMoney") < MaxBallUpgradeCostMultiplier * PlayerPrefs.GetInt("MaxBallUpgradeLevel"))
            {
                GreyImageMaxBall.SetActive(true);
            }
            if (PlayerPrefs.GetInt("TotalMoney") < InitialBallUpgradeCostMultiplier * PlayerPrefs.GetInt("InitialBallUpgradeLevel"))
            {
                GreyImageInitialBall.SetActive(true);
            }
            if (PlayerPrefs.GetInt("TotalMoney") < IncomeUpgradeCostMultiplier * PlayerPrefs.GetInt("IncomeUpgradeLevel"))
            {
                GreyImageIncome.SetActive(true);
            }
        }
    }

    public void UpgradeIncome()
    {
        if (PlayerPrefs.GetInt("TotalMoney") >= IncomeUpgradeCostMultiplier * PlayerPrefs.GetInt("IncomeUpgradeLevel"))
        {
            AudioManager.instance.Play("UpgradeSesi");
            PlayerPrefs.SetInt("TotalMoney", PlayerPrefs.GetInt("TotalMoney") - IncomeUpgradeCostMultiplier * PlayerPrefs.GetInt("IncomeUpgradeLevel"));
            UpdateMoneyText(PlayerPrefs.GetInt("TotalMoney"));
            PlayerPrefs.SetInt("IncomeUpgradeLevel", PlayerPrefs.GetInt("IncomeUpgradeLevel") + 1);
            IncomeUpgradeLevelText.text = "Level " + PlayerPrefs.GetInt("IncomeUpgradeLevel").ToString();
            IncomeUpgradeMoneyText.text = (IncomeUpgradeCostMultiplier * PlayerPrefs.GetInt("IncomeUpgradeLevel")).ToString();
            if (PlayerPrefs.GetInt("TotalMoney") < MaxBallUpgradeCostMultiplier * PlayerPrefs.GetInt("MaxBallUpgradeLevel"))
            {
                GreyImageMaxBall.SetActive(true);
            }
            if (PlayerPrefs.GetInt("TotalMoney") < InitialBallUpgradeCostMultiplier * PlayerPrefs.GetInt("InitialBallUpgradeLevel"))
            {
                GreyImageInitialBall.SetActive(true);
            }
            if (PlayerPrefs.GetInt("TotalMoney") < IncomeUpgradeCostMultiplier * PlayerPrefs.GetInt("IncomeUpgradeLevel"))
            {
                GreyImageIncome.SetActive(true);
            }
        }
    }

    public void NextLevelButtonCode()
    {
        //Destroy(LevelParent.GetChild(0));
        //PlayerPrefs.SetInt("CurrentGameLevel", PlayerPrefs.GetInt("CurrentGameLevel") + 1);
        //InstantiateManager.instance.InstantiateLevel(PlayerPrefs.GetInt("CurrentGameLevel"));
        FirstStageManager.instance.balls.Clear();
        SecondStageManager.instance.Allies.Clear();
        PlayerController.instance.Bullets.Clear();
        PlayerController.instance.ResetPlayer();
        FirstStageManager.instance.InstantiateNewBalls(1);
        LevelText.text = "Lvl." + PlayerPrefs.GetInt("CurrentGameLevel").ToString();
        PlayerPrefs.SetInt("TotalMoney", PlayerPrefs.GetInt("TotalMoney") + (int)ThirdStageManager.instance.CurrentGameEarnedMoney);
        UpdateMoneyText(PlayerPrefs.GetInt("TotalMoney"));
        ThirdStageManager.instance.CurrentGameEarnedMoney = 0;
        if (PlayerPrefs.GetInt("TotalMoney") < MaxBallUpgradeCostMultiplier * PlayerPrefs.GetInt("MaxBallUpgradeLevel"))
        {
            GreyImageMaxBall.SetActive(true);
        }
        if (PlayerPrefs.GetInt("TotalMoney") < InitialBallUpgradeCostMultiplier * PlayerPrefs.GetInt("InitialBallUpgradeLevel"))
        {
            GreyImageInitialBall.SetActive(true);
        }
        if (PlayerPrefs.GetInt("TotalMoney") < IncomeUpgradeCostMultiplier * PlayerPrefs.GetInt("IncomeUpgradeLevel"))
        {
            GreyImageIncome.SetActive(true);
        }
        EndGamePanel.SetActive(false);
        EndGameWinPanel.SetActive(false);
        StartGameButton.SetActive(true);
        UpgradeIncomeButton.SetActive(true);
        UpgradeMaxBallButton.SetActive(true);
        UpgradeInitialBallButton.SetActive(true);
        SettingsButton.SetActive(true);
        TapToStartText.SetActive(true);
        CameraMovement.instance.Target = FirstStageManager.instance.balls[0].transform;
        CameraMovement.instance.ShouldFollow = true;
        CameraMovement.instance.GetComponent<Animator>().enabled = false;
        //CameraMovement.instance.offset = new Vector3(0, 60f, -25.2f);
    }

    public void RestartLevelButtonCode()
    {
        //Destroy(LevelParent.GetChild(0));
        //InstantiateManager.instance.InstantiateLevel(PlayerPrefs.GetInt("CurrentGameLevel"));
        FirstStageManager.instance.balls.Clear();
        SecondStageManager.instance.Allies.Clear();
        PlayerController.instance.Bullets.Clear();
        PlayerController.instance.ResetPlayer();
        FirstStageManager.instance.InstantiateNewBalls(1);
        LevelText.text = "Lvl." + PlayerPrefs.GetInt("CurrentGameLevel").ToString();
        ThirdStageManager.instance.CurrentGameEarnedMoney = 0;
        if (PlayerPrefs.GetInt("TotalMoney") < MaxBallUpgradeCostMultiplier * PlayerPrefs.GetInt("MaxBallUpgradeLevel"))
        {
            GreyImageMaxBall.SetActive(true);
        }
        if (PlayerPrefs.GetInt("TotalMoney") < InitialBallUpgradeCostMultiplier * PlayerPrefs.GetInt("InitialBallUpgradeLevel"))
        {
            GreyImageInitialBall.SetActive(true);
        }
        if (PlayerPrefs.GetInt("TotalMoney") < IncomeUpgradeCostMultiplier * PlayerPrefs.GetInt("IncomeUpgradeLevel"))
        {
            GreyImageIncome.SetActive(true);
        }
        EndGamePanel.SetActive(false);
        EndGameLosePanel.SetActive(false);
        StartGameButton.SetActive(true);
        UpgradeIncomeButton.SetActive(true);
        UpgradeMaxBallButton.SetActive(true);
        UpgradeInitialBallButton.SetActive(true);
        SettingsButton.SetActive(true);
        TapToStartText.SetActive(true);
        CameraMovement.instance.Target = FirstStageManager.instance.balls[0].transform;
        CameraMovement.instance.ShouldFollow = true;
        CameraMovement.instance.GetComponent<Animator>().enabled = false;
        //CameraMovement.instance.offset = new Vector3(0, 60f, -25.2f);
    }

    public void GameEndUI(bool isSuccess)
    {
        EndGamePanel.SetActive(true);
        if (isSuccess)
        {
            EndGameWinPanel.SetActive(true);
        }
        else
        {
            EndGameLosePanel.SetActive(true);
        }
    }


}