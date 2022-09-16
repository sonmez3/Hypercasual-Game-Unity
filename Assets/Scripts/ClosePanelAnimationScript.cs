using UnityEngine;

public class ClosePanelAnimationScript : MonoBehaviour
{
    public void ClosePanelAnimationEvent()
    {
        transform.parent.gameObject.SetActive(false);
    }
}
