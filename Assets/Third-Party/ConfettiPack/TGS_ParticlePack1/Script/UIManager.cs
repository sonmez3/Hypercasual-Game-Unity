using UnityEngine;

public class UIManager : MonoBehaviour {

    #region Variables

    public static UIManager Instance;

    public ParticleSystem[] Particles;

    public GameObject Info_UI, Btns_UI, ParticleBtn_Prefab;
    public Transform Particle_Parent;

    #endregion

    #region Unity_InBuild_Methods

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        int length = (int)(Particles.Length / 3);
        Particle_Parent.GetComponent<RectTransform>().sizeDelta = new Vector2 (380f, 120 + (120 * length));
        for (int i = 0; i < Particles.Length; i++)
        {
            GameObject btn = Instantiate(ParticleBtn_Prefab, Particle_Parent);
            btn.GetComponent<ParticleBtn>().SetValues(i);
        }
    }

    #endregion

    public void PlayPS(int index)
    {
        SetBtnsUI_Visibility(false);
        Particles[index].Play();
    }

    public void SetBtnsUI_Visibility(bool value)
    {
        Btns_UI.SetActive(value);
        for (int i = 0; i < Particles.Length; i++)
            Particles[i].Stop();
    }

    public void InfoBtn()
    {
        Btns_UI.SetActive(false);
        Info_UI.SetActive(true);
    }

    public void BackBtn()
    {
        Btns_UI.SetActive(true);
        Info_UI.SetActive(false);
    }

}
