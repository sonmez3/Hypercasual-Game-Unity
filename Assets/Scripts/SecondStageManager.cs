using System.Collections.Generic;
using UnityEngine;

public class SecondStageManager : MonoBehaviour
{
    public static SecondStageManager instance;
    public bool DidSecondStageStart;
    public bool DidSecondStageEnd;
    public List<GameObject> Allies = new List<GameObject>();
    private void Awake()
    {
        instance = this;
    }
    
    private void StartSecondManager()
    {
        DidSecondStageStart = true;
        DidSecondStageEnd = false;
        PlayerController.instance.Move();
        CameraMovement.instance.Target = PlayerController.instance.transform;
        
    }
    

    public void SecondButtonCode()
    {
        StartSecondManager();
    }

    public void AlignAllAllies()
    {

    }
}
