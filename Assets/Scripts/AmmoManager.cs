using UnityEngine;

public class AmmoManager : MonoBehaviour
{
    public bool ShouldArrangeXCoordinate;

    private void Start()
    {
        ShouldArrangeXCoordinate = true;
    }
    private void Update()
    {
        if (ShouldArrangeXCoordinate)
        {
            transform.position = new Vector3(PlayerController.instance.transform.position.x, transform.position.y, PlayerController.instance.transform.position.z - 1.61f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("WhiteHumanChild"))
        {
            if (PlayerPrefs.GetInt("IsVibration") == 1)
            {
                Vibration.VibratePop();
            }
            AudioManager.instance.Play("GainAlly");
            PlayerController.instance.startGainNewAllyRoutine(other);
            other.tag = "AllyChildForNow";
            gameObject.SetActive(false);
        }
    }
}
