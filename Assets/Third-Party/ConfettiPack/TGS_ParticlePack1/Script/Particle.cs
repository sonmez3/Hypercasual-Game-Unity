using UnityEngine;

public class Particle : MonoBehaviour
{
    #region Unity_InBuild_Methods

    void Start()
    {
        var main = GetComponent<ParticleSystem>().main;
        main.stopAction = ParticleSystemStopAction.Callback;
    }

    void OnParticleSystemStopped()
    {
        UIManager.Instance.SetBtnsUI_Visibility(true);
    }

    #endregion
}
