using System.Collections.Generic;
using Unity.Mathematics;
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

    quaternion originalRotation;

    SpriteRenderer spriteRenderer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        originalRotation = transform.rotation;
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
        if (playerOne.transform.position.x < playerTwo.transform.position.x)
        {
            spriteRenderer.flipY = true;
        }
        else
        {
            spriteRenderer.flipY = false;
        }
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
        if (splitActions.Count >= 2 && isJoint)
        {
            SplitPlayers();
        }
        else if (splitActions.Count >= 2 && !isJoint)
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
    void ActivateJoin()
    {
        isJoint = true;
        transform.rotation = originalRotation;
        playerManagerSpriteRenderer.enabled = true;
        lineManager.EnableLines();
        playerOne.GetComponent<SpringJoint2D>().enabled = true;
        playerTwo.GetComponent<SpringJoint2D>().enabled = true;
        playerOne.GetComponent<DistanceJoint2D>().enabled = true;
        playerTwo.GetComponent<DistanceJoint2D>().enabled = true;
        playerOneTowPoint.SetActive(false);
        playerTwoTowPoint.SetActive(false);
    }
    public void HazardSplit(Vector3 hazardLocation, float explosiveForceAmount, float upwardForce, GameObject affectedParty)
    {
        if (affectedParty == gameObject)
        {
            SplitPlayers();
            ExplosiveForce(hazardLocation, explosiveForceAmount, upwardForce);
            playerOne.GetComponent<PlayerMovement>().ActivateStunTimer();
            playerTwo.GetComponent<PlayerMovement>().ActivateStunTimer();
            return;
        }

        if (affectedParty == null)
        {
            return;
        }

        LimitedExplosiveForce(hazardLocation, explosiveForceAmount, upwardForce, affectedParty);
        
        if (affectedParty.GetComponent<PlayerManager>() == null) return;

        affectedParty.GetComponent<PlayerMovement>().ActivateStunTimer();
    }

    public void ExplosiveForce(Vector3 hazardlocation, float explosiveForceAmount, float upwardForce)
    {
        Vector2 directionToPlayerOne = (playerOne.transform.position - hazardlocation).normalized;
        Vector2 directionToPlayerTwo = (playerTwo.transform.position - hazardlocation).normalized;
        directionToPlayerOne.y = upwardForce;
        directionToPlayerTwo.y = upwardForce;
        playerOne.GetComponent<Rigidbody2D>().AddForce(directionToPlayerOne * explosiveForceAmount, ForceMode2D.Impulse);
        playerTwo.GetComponent<Rigidbody2D>().AddForce(directionToPlayerTwo * explosiveForceAmount, ForceMode2D.Impulse);
    }

    public void LimitedExplosiveForce(Vector3 hazardlocation, float explosiveForceAmount, float upwardForce, GameObject affectedParty)
    {
        Vector2 directionToAffectedParty = (affectedParty.transform.position - hazardlocation).normalized;
        directionToAffectedParty.y = upwardForce;
        affectedParty.GetComponent<Rigidbody2D>().AddForce(directionToAffectedParty * explosiveForceAmount, ForceMode2D.Impulse);
    }
}