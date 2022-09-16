using UnityEngine;
using UnityEngine.UI;

public class ParticleBtn : MonoBehaviour
{
    #region Variables

    public Text ParticleIndex_Txt;
    public int Index;

    #endregion

    public void SetValues(int index)
    {
        Index = index;
        ParticleIndex_Txt.text = (index + 1).ToString();
    }

    public void PlayPS()
    {
        UIManager.Instance.PlayPS(Index);
    }

}
