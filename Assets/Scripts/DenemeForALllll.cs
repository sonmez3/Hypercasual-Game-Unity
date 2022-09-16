using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DenemeForALllll : MonoBehaviour
{
    public float speeddneme;
    public float speeddeneme2;
    // Start is called before the first frame update
    void Start()
    {
        speeddneme = 50;
        speeddeneme2 = 28f;
        //StartCoroutine(deneme());   
        StartCoroutine(deneme2());
    }

    private IEnumerator deneme()
    {
        for (int i=0; i< 50; i++)
        {
            GetComponent<Rigidbody>().velocity = new Vector3(0, 0, speeddneme);
            speeddneme -= 15.0f / 50;
            yield return new WaitForSeconds(0.1f);
        }
        
        GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0f);
    }

    private IEnumerator deneme2()
    {
        GetComponent<Rigidbody>().velocity = new Vector3(0, 0, speeddeneme2);
        yield return new WaitForSeconds(5f);
        GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
    }
    
}
