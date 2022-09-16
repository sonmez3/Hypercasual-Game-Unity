using UnityEngine;

public class TrapManager : MonoBehaviour
{
    public static TrapManager instance;

    //[SerializeField] private Material BlackMaterial;

    private void Awake()
    {
        instance = this;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("AllyChild"))
        {
            AudioManager.instance.Play("BotDie");
            Instantiate(ThirdStageManager.instance.BlueBlood, other.transform.position + Vector3.up * 2.5f, Quaternion.Euler(Vector3.zero), ThirdStageManager.instance.EffctsParent);
            Instantiate(ThirdStageManager.instance.BlueEffect, other.transform.position, Quaternion.Euler(Vector3.zero), ThirdStageManager.instance.EffctsParent);
            Destroy(other.transform.parent.gameObject);
        }
    }
}
