using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class AllyManager : MonoBehaviour
{
    public bool ShouldMove = false;
    private Transform Target;
    public Vector3 AllyOffset = Vector3.zero;
    public NavMeshAgent agent;
    private float MinDistance = 1.2f;
    [SerializeField] private Transform WeaponParent;
    [SerializeField] private Transform Weapon;
    private void Start()
    {
        Target = PlayerController.instance.transform;
    }
    private void LateUpdate()
    {
        if (ShouldMove)
        {
            transform.position = new Vector3(Target.position.x, 42.14f, Target.position.z) + AllyOffset;
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, -5, 5), transform.position.y, transform.position.z);
        }
        
    }

    private IEnumerator MoveToCharactersBehind(Vector3 position)
    {
        agent.SetDestination(position);
        yield return null;
        while (true)
        {
            if ((transform.position - position).sqrMagnitude <= MinDistance)
            {
                ShouldMove = true;
                AllyOffset = transform.position - PlayerController.instance.transform.position;
                agent.isStopped = true;
                GetComponent<NavMeshAgent>().enabled = false;
                transform.rotation = Quaternion.Euler(Vector3.zero);
                transform.GetChild(1).GetComponent<BoxCollider>().isTrigger = false;
                transform.GetChild(1).tag = "AllyChild";
                break;
            }
            yield return null;
        }
        transform.rotation = Quaternion.Euler(Vector3.zero);
    }

    private IEnumerator MoveToBarrier(Vector3 position)
    {
        agent.isStopped = false;
        agent.SetDestination(position);
        yield return null;
        while (true)
        {
            if ((transform.position - position).sqrMagnitude <= MinDistance)
            {
                agent.isStopped = true;
                GetComponent<NavMeshAgent>().enabled = false;
                transform.rotation = Quaternion.Euler(Vector3.zero);
                Weapon.parent = WeaponParent;
                Weapon.localPosition = new Vector3(0.057f, 0.007f, 0.065f);
                StartFightRoutine();
                yield break;
            }
            yield return null;
        }
    }

    public void StartMoveharactersBehindRoutine(Vector3 position)
    {
        StartCoroutine(MoveToCharactersBehind(position));
    }

    public void StartMoveToBarrierRoutine(Vector3 position)
    {
        StartCoroutine(MoveToBarrier(position));
    }

    public void StopAlly() 
    {
        agent.isStopped = true;
    }

    public void StartFightRoutine()
    {
        StartCoroutine(Fight());
    }

    private IEnumerator Fight()
    {
        while (true)
        {
            if (ThirdStageManager.instance.DidGameComplete)
            {
                GetComponent<Animator>().SetTrigger("AllyIsDance" + Random.Range(1, 4).ToString());
                break;
            }

            Instantiate(Resources.Load("Gun_Bullet_ThirdStage"), transform.position, Quaternion.Euler(Vector3.zero));
            AudioManager.instance.Play("FiringGun");
            yield return new WaitForSeconds(1.0f);
        }
        
   
    }

}
