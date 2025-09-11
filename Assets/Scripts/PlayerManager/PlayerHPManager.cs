using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHPManager : MonoBehaviour
{
    [SerializeField]
    float hp = 100f;
    [SerializeField]
    float maxHP = 100f;
    [Space, SerializeField, Header("HP REGEN PER SECOND")]
    float hpRegen = 1f;
    [SerializeField, Header("HP LOST PER SECOND")]
    float hpLost = 1f;

    PlayerManager playerManager;

    [SerializeField]
    Image vignette;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerManager = GetComponent<PlayerManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (vignette == null) return;

        if (!playerManager.IsJoint)
        {
            hp -= hpLost * Time.deltaTime;
        }
        if (playerManager.IsJoint)
        {
            hp = Mathf.Clamp(hp + hpRegen * Time.deltaTime, 0f, maxHP);
        }
        vignette.color = new Color(1f, 1f, 1f, 1f - (hp / maxHP));

        if (hp <= 0f)
        {
            Debug.LogWarning("When in doubt, look intelligent.");
        }
    }
    public float HP
    {
        get { return hp; }
    }
    public float MaxHP 
    {
        get { return maxHP; }
    }
}