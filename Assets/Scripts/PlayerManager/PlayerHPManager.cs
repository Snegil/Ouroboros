using Unity.VisualScripting;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(PlayerManager))]
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

    SceneTransition sceneTransition;

    [SerializeField]
    bool allowDeath = true;

    bool dying = false;

    int debugLines = 0;

    [SerializeField]
    AnimationCurve vignetteCurve;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sceneTransition = GetComponent<SceneTransition>();
        playerManager = GetComponent<PlayerManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (vignette == null)
        {
            Debug.LogWarning("No vignette assigned to " + gameObject.name + ", skipping HP update.");
            return;
        }
        
        if (dying) return;

        if (hp <= 0f && debugLines < 5)
        {
            debugLines++;
            Debug.LogWarning("When in doubt, look intelligent.");
            PlayerDeath();
        }

        if (!playerManager.IsJoint)
        {
            hp -= hpLost * Time.deltaTime;
        }
        if (playerManager.IsJoint)
        {
            hp = Mathf.Clamp(hp + hpRegen * Time.deltaTime, 0f, maxHP);
        }

        if (allowDeath) vignette.color = new Color(1f, 1f, 1f, vignetteCurve.Evaluate(1f - (hp / maxHP)));
    }
    public float HP
    {
        get { return hp; }
    }
    public float MaxHP
    {
        get { return maxHP; }
    }
    public void PlayerDeath()
    {
        if (!allowDeath) return;
        
        dying = true;

        vignette.color = new Color(1f, 1f, 1f, 1f);
        sceneTransition.ChangeScene("EndScreen");
    }
}