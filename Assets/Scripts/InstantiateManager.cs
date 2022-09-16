using UnityEngine;

public class InstantiateManager : MonoBehaviour
{
    public static InstantiateManager instance;
    [SerializeField] Transform LevelParent;

    private void Awake()
    {
        instance = this;
    }


    public void InstantiateLevel(int level)
    {
        Instantiate(Resources.Load("LevelPrefabs/level" + level.ToString()), Vector3.zero, Quaternion.Euler(Vector3.zero), LevelParent);
    }

}
