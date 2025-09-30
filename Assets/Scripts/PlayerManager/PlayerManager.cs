using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.Mathematics;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    List<GameObject> players = new();

    GameObject playerOneTowPoint;
    GameObject playerTwoTowPoint;
    PlayerLineManager lineManager;
    SpriteRenderer playerManagerSpriteRenderer;

    bool isJoint = true;
    [SerializeField, Header("THE DISTANCE FOR THE PLAYERS TO CONNECT!")]
    float connectionDistance = 10f;
    public bool IsJoint { get { return isJoint; } }

    [Space, SerializeField, Header("The force applied to the players when hit by a hazard.")]
    float hazardExplosiveForceAmount = 5f;

    [SerializeField, Header("The upward force applied to the players when hit by a hazard.")]
    float hazardUpwardForce = 2f;

    BloodSystem[] bloodSystems;

    [SerializeField, Header("The distance when the players are split")]
    float splitMaxDistance = 8;
    [SerializeField, Header("The distance when the players are joint")]
    float jointMaxDistance = 2;

    [SerializeField]
    float splitActionCooldown = 0.5f;
    float setSplitActionCooldown;
    void Awake()
    {
        players.Add(GameObject.FindWithTag("PlayerOne"));
        players.Add(GameObject.FindWithTag("PlayerTwo"));

        jointMaxDistance = players[0].GetComponent<DistanceJoint2D>().distance;

        lineManager = gameObject.GetComponent<PlayerLineManager>();
        playerManagerSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();

        playerOneTowPoint = players[0].transform.GetChild(0).gameObject;
        playerTwoTowPoint = players[1].transform.GetChild(0).gameObject;

        setSplitActionCooldown = splitActionCooldown;
    }
    void Start()
    {
        GameObject[] bloodSystemGameObjects = GameObject.FindGameObjectsWithTag("PlayerBlood");
      
        bloodSystems = new BloodSystem[bloodSystemGameObjects.Length];

        for (int i = 0; i < bloodSystemGameObjects.Length; i++)
        {
            bloodSystems[i] = bloodSystemGameObjects[i].GetComponent<BloodSystem>();
        }
    }

    void Update()
    {
        if (splitActionCooldown > 0)
        {
            splitActionCooldown -= Time.deltaTime;
            return;
        }
    }

    public GameObject GetPlayerOne()
    {
        return players[0];
    }
    public GameObject GetPlayerTwo()
    {
        return players[1];
    }
    public Vector2 AveragePosition()
    {
        return (players[0].transform.position + players[1].transform.position) / 2f;
    }
    public float DistanceBetweenPlayers()
    {
        return Vector2.Distance(players[0].transform.position, players[1].transform.position);
    }
    public float DistanceBetweenCentreAndPlayerOne()
    {
        return Vector2.Distance(AveragePosition(), players[0].transform.position);
    }
    public float DistanceBetweenCentreAndPlayerTwo()
    {
        return Vector2.Distance(AveragePosition(), players[0].transform.position);
    }

    public void SplitAction()
    {
        if (splitActionCooldown > 0) return;

        splitActionCooldown = setSplitActionCooldown;
        
        if (isJoint)
        {
            SplitPlayers();
        }
        else
        {
            JoinPlayers();
        }
    }
    
    public void SplitPlayers()
    {
        isJoint = false;
        playerManagerSpriteRenderer.enabled = false;
        lineManager.DisableLines();

        for (int i = 0; i < players.Count; i++)
        {
            players[i].GetComponent<SpringJoint2D>().enabled = false;
            players[i].GetComponent<DistanceJoint2D>().distance = splitMaxDistance;
        }

        playerOneTowPoint.SetActive(true);
        playerTwoTowPoint.SetActive(true);
        foreach (BloodSystem bloodSystem in bloodSystems)
        {
            if (bloodSystem == null) return;

            bloodSystem.EnableBlood();
        }
        GetComponent<CapsuleCollider2D>().enabled = false;
    }
    public void JoinPlayers()
    {
        RaycastHit2D raycastHit2D = Physics2D.Raycast(players[0].transform.position, (players[1].transform.position - players[0].transform.position).normalized, connectionDistance);
        Debug.DrawRay(players[0].transform.position, (players[1].transform.position - players[0].transform.position).normalized * connectionDistance, Color.blue, 1f);
        if (raycastHit2D.collider == null) return;
        if (raycastHit2D.collider.CompareTag("PlayerTwo"))
        {
            ActivateJoin();
            return;
        }
    }

    // Join the players.
    void ActivateJoin()
    {
        isJoint = true;
        transform.rotation = quaternion.identity;
        playerManagerSpriteRenderer.enabled = true;
        lineManager.EnableLines();

        for (int i = 0; i < players.Count; i++)
        {
            players[i].GetComponent<SpringJoint2D>().enabled = true;
            players[i].GetComponent<DistanceJoint2D>().distance = jointMaxDistance;            
        }
        playerOneTowPoint.SetActive(false);
        playerTwoTowPoint.SetActive(false);
        foreach (BloodSystem bloodSystem in bloodSystems)
        {
            if (bloodSystem == null) return;
            
            bloodSystem.DisableBlood();
        }
        GetComponent<CapsuleCollider2D>().enabled = true;
    }

    // Apply explosive force to the players and split them if they are jointed.
    // If the affected party is not the playermanager, only apply force to that game.
    // This is used by Sawblade and door.
    // 31415 is just a random number to check if the parameters have been set by the caller or not. It just happens to be the first 5 digits of pi.
    public void HazardSplit(Transform hazardLocation, GameObject affectedParty, float explosiveForceAmount = 31415, float upwardForce = 31415)
    {
        if (explosiveForceAmount == 31415)
        {
            explosiveForceAmount = hazardExplosiveForceAmount;
        }
        if (upwardForce == 31415)
        {
            upwardForce = hazardUpwardForce;
        }

        if (affectedParty == gameObject)
        {
            SplitPlayers();
            ExplodeBoth(hazardLocation, explosiveForceAmount, upwardForce);
            for (int i = 0; i < players.Count; i++)
            {
                players[i].GetComponent<PlayerStunned>().ActivateStunTimer();
            }
            return;
        }

        if (affectedParty == null)
        {
            return;
        }

        ExplodeOne(hazardLocation, explosiveForceAmount, upwardForce, affectedParty);

        if (affectedParty.GetComponent<PlayerManager>() == null) return;

        affectedParty.GetComponent<PlayerStunned>().ActivateStunTimer();
    }


    // Push both players with a force away from the origin.
    public void ExplodeBoth(Transform hazardlocation, float explosiveForceAmount, float upwardForce)
    {
        List<Vector3> directions = new List<Vector3>
        {
            (players[0].transform.position - hazardlocation.position).normalized,
            (players[1].transform.position - hazardlocation.position).normalized
        };

        directions[0] = new Vector3(directions[0].x, upwardForce * (players[0].transform.position.y > hazardlocation.position.y ? 1 : -1), directions[0].z);
        directions[1] = new Vector3(directions[1].x, upwardForce * (players[1].transform.position.y > hazardlocation.position.y ? 1 : -1), directions[1].z);
        for (int i = 0; i < players.Count; i++)
        {
            players[i].GetComponent<Rigidbody2D>().AddForce(directions[i]);
        }
        players[0].GetComponent<Rigidbody2D>().AddForce(directions[0] * explosiveForceAmount, ForceMode2D.Impulse);
        players[1].GetComponent<Rigidbody2D>().AddForce(directions[1] * explosiveForceAmount, ForceMode2D.Impulse);
    }

    // Push a specific gameobject with a force away from the origin.
    public void ExplodeOne(Transform hazardlocation, float explosiveForceAmount, float upwardForce, GameObject affectedParty)
    {
        Vector2 directionToAffectedParty = (affectedParty.transform.position - hazardlocation.position).normalized;
        //directionToAffectedParty.y = hazardlocation.up.normalized.y;
        directionToAffectedParty.y = upwardForce * (affectedParty.transform.position.y > hazardlocation.position.y ? 1 : -1);
        Vector2 explosiveForce = new(directionToAffectedParty.x * explosiveForceAmount, directionToAffectedParty.y * upwardForce);
        
        affectedParty.GetComponent<Rigidbody2D>().AddForce(explosiveForce, ForceMode2D.Impulse);
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        // Split the players if the playermanager get hits by a moving door.
        if (collision.GetComponent<DoorTimed>() != null && collision.GetComponent<DoorTimed>().IsMoving() || collision.GetComponent<DoorOpenClose>() != null && collision.GetComponent<DoorOpenClose>().IsMoving())
        {
            Debug.Log("SPLIT");
            HazardSplit(collision.transform, gameObject);
        }
    }
}