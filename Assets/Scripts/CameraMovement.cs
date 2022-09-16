using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [HideInInspector] public static CameraMovement instance;
    public Transform Target;
    public Vector3 offset = new Vector3(0, 60f, -25.2f);
    public bool ShouldFollow;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        Target = FirstStageManager.instance.balls[0].transform;
        ShouldFollow = true;
    }
    private void LateUpdate()
    {
        if (ShouldFollow)
        {
            transform.position = new Vector3(0, 0, Target.position.z) + offset;
        }
        
    }

    public void ChangeShouldFollow()
    {
        ShouldFollow = false;
    }
}
