using UnityEngine;

public class Deneme : MonoBehaviour
{
    //public GameObject Prrrefab;
    private Vector3 positfdaw;
    public void DenemeButtonCode()
    {
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(0, 0, 100), 5 * Time.deltaTime);
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(0, 0, 100), 5 * Time.deltaTime);
    }
}
