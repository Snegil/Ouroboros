using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    GameObject playerOne;
    GameObject playerOneTowPoint;
    GameObject playerTwo;
    GameObject playerTwoTowPoint;
    PlayerLineManager lineManager;
    SpriteRenderer playerManagerSpriteRenderer;

    bool isJoint = true;
    [SerializeField, Header("THE DISTANCE FOR THE PLAYERS TO CONNECT!")]
    float connectionDistance = 10f;
    public bool IsJoint { get { return isJoint; } }

    List<bool> splitActions = new();

    SpriteRenderer spriteRenderer;

    [Space, SerializeField, Header("The force applied to the players when hit by a hazard.")]
    float hazardExplosiveForceAmount = 5f;

    [SerializeField, Header("The upward force applied to the players when hit by a hazard.")]
    float hazardUpwardForce = 2f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

        playerOne = GameObject.FindWithTag("PlayerOne");
        playerTwo = GameObject.FindWithTag("PlayerTwo");
        lineManager = gameObject.GetComponent<PlayerLineManager>();
        playerManagerSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();

        playerOneTowPoint = playerOne.transform.GetChild(0).gameObject;
        playerTwoTowPoint = playerTwo.transform.GetChild(0).gameObject;
        //Debug.Log(playerOne);
        //Debug.Log(playerTwo);
    }
    void Update()
    {
        // if (playerOne.transform.position.x < playerTwo.transform.position.x)
        // {
        //     spriteRenderer.flipY = true;
        // }
        // else
        // {
        //     spriteRenderer.flipY = false;
        // }
    }

    public GameObject GetPlayerOne()
    {
        return playerOne;
    }
    public GameObject GetPlayerTwo()
    {
        return playerTwo;
    }
    public Vector2 AveragePosition()
    {
        return (playerOne.transform.position + playerTwo.transform.position) / 2f;
    }
    public float DistanceBetweenPlayers()
    {
        return Vector2.Distance(playerOne.transform.position, playerTwo.transform.position);
    }
    public float DistanceBetweenCentreAndPlayerOne()
    {
        return Vector2.Distance(AveragePosition(), playerOne.transform.position);
    }
    public float DistanceBetweenCentreAndPlayerTwo()
    {
        return Vector2.Distance(AveragePosition(), playerTwo.transform.position);
    }
    public void SplitAction()
    {
        splitActions.Add(true);
        if (splitActions.Count >= 1 && isJoint)
        {
            SplitPlayers();
        }
        else if (splitActions.Count >= 1 && !isJoint)
        {
            JoinPlayers();
        }
    }
    public void ClearSplitAction()
    {
        splitActions.Clear();
    }
    public void SplitPlayers()
    {
        isJoint = false;
        playerManagerSpriteRenderer.enabled = false;
        lineManager.DisableLines();
        playerOne.GetComponent<SpringJoint2D>().enabled = false;
        playerTwo.GetComponent<SpringJoint2D>().enabled = false;
        playerOne.GetComponent<DistanceJoint2D>().enabled = false;
        playerTwo.GetComponent<DistanceJoint2D>().enabled = false;
        playerOneTowPoint.SetActive(true);
        playerTwoTowPoint.SetActive(true);
        GetComponent<CapsuleCollider2D>().enabled = false;
    }
    public void JoinPlayers()
    {
        RaycastHit2D[] raycastHits2D = Physics2D.RaycastAll(playerOne.transform.position, (playerTwo.transform.position - playerOne.transform.position).normalized, connectionDistance);
        Debug.DrawRay(playerOne.transform.position, (playerTwo.transform.position - playerOne.transform.position).normalized * connectionDistance, Color.blue, 1f);
        for (int i = 0; i < raycastHits2D.Length; i++)
        {
            if (raycastHits2D[i].collider.CompareTag("PlayerTwo"))
            {
                ActivateJoin();
                return;
            }
        }
    }

    // Join the players.
    void ActivateJoin()
    {
        isJoint = true;
        transform.rotation = quaternion.identity;
        playerManagerSpriteRenderer.enabled = true;
        lineManager.EnableLines();
        playerOne.GetComponent<SpringJoint2D>().enabled = true;
        playerTwo.GetComponent<SpringJoint2D>().enabled = true;
        playerOne.GetComponent<DistanceJoint2D>().enabled = true;
        playerTwo.GetComponent<DistanceJoint2D>().enabled = true;
        playerOneTowPoint.SetActive(false);
        playerTwoTowPoint.SetActive(false);
        GetComponent<CapsuleCollider2D>().enabled = true;
    }

    // Apply explosive force to the players and split them if they are jointed.
    // If the affected party is not the playermanager, only apply force to that game.
    // This is used by Sawblade and door.
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
            playerOne.GetComponent<PlayerMovement>().ActivateStunTimer();
            playerTwo.GetComponent<PlayerMovement>().ActivateStunTimer();
            return;
        }

        if (affectedParty == null)
        {
            return;
        }

        ExplodeOne(hazardLocation, explosiveForceAmount, upwardForce, affectedParty);

        if (affectedParty.GetComponent<PlayerManager>() == null) return;

        affectedParty.GetComponent<PlayerMovement>().ActivateStunTimer();
    }


    // Push both players with a force away from the origin.
    public void ExplodeBoth(Transform hazardlocation, float explosiveForceAmount, float upwardForce)
    {
        Vector2 directionToPlayerOne = (playerOne.transform.position - hazardlocation.position).normalized;
        Vector2 directionToPlayerTwo = (playerTwo.transform.position - hazardlocation.position).normalized;
        //directionToPlayerOne.y = upwardForce * hazardlocation.up.normalized.y;
        directionToPlayerOne.y = upwardForce * (playerOne.transform.position.y > hazardlocation.position.y ? 1 : -1);
        //directionToPlayerTwo.y = upwardForce * hazardlocation.up.normalized.y;
        directionToPlayerTwo.y = upwardForce * (playerTwo.transform.position.y > hazardlocation.position.y ? 1 : -1);
        playerOne.GetComponent<Rigidbody2D>().AddForce(directionToPlayerOne * explosiveForceAmount, ForceMode2D.Impulse);
        playerTwo.GetComponent<Rigidbody2D>().AddForce(directionToPlayerTwo * explosiveForceAmount, ForceMode2D.Impulse);
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