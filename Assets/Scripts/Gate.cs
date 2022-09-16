using UnityEngine;
using TMPro;

public class Gate : MonoBehaviour
{
    public string sign;  //÷
    public int value;
    public bool isSingle;
    private void Start()
    {
        transform.GetChild(2).GetChild(0).GetComponent<TextMeshPro>().text = sign + value.ToString();
    }
}
