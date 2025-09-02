using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    GameObject thatSkink;
    GameObject otherSkink;

    [SerializeField, Header("THE DISTANCE FOR THE SKINKS TO CONNECT!")]
    float connectionDistance = 10f;
    [SerializeField]
    LayerMask layerMask;


    ConnectedPlayers connectedPlayers;
    PlayerManagerPosition playerManagerPosition;
    bool isJoint = true;
    List<bool> splitAction = new();

    public bool IsJoint { get { return isJoint; } }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        thatSkink = GameObject.FindWithTag("PlayerOne");
        otherSkink = GameObject.FindWithTag("PlayerTwo");

        thatSkink.GetComponent<SpringJoint2D>().connectedBody = otherSkink.GetComponent<Rigidbody2D>();
        otherSkink.GetComponent<SpringJoint2D>().connectedBody = thatSkink.GetComponent<Rigidbody2D>();

        connectedPlayers = GetComponent<ConnectedPlayers>();

        playerManagerPosition = gameObject.AddComponent<PlayerManagerPosition>();
        playerManagerPosition.SetPlayerManager(this);
    }

    public GameObject GetThatSkink()
    {
        return thatSkink;
    }
    public GameObject GetOtherSkink()
    {
        return otherSkink;
    }

    // SplitSkinks once used by one of the skinks adds to a list, and checks if the list has 2 items, if so, toggle the joint state of both skinks and clear the list.
    // If the input is cancelled, clear the list (RAN BY SPLITSKINKS.CS).
    public void SplitAction()
    {
        splitAction.Add(true);

        if (splitAction.Count < 2)
        {
            return;
        }

        if (isJoint)
        {
            SplitSkinks();
        }
        else
        {
            ConnectSkinks();
        }
        splitAction.Clear();
    }

    public void SplitSkinks()
    {
        isJoint = false;
        connectedPlayers.ToggleLine();
        thatSkink.GetComponent<SpringJoint2D>().enabled = false;
        otherSkink.GetComponent<SpringJoint2D>().enabled = false;
    }

    public void ConnectSkinks()
    {
        RaycastHit2D raycastHit2D = Physics2D.Raycast(thatSkink.transform.position, (otherSkink.transform.position - thatSkink.transform.position).normalized, connectionDistance, layerMask);
        //Debug.DrawRay(thatSkink.transform.position, (otherSkink.transform.position - thatSkink.transform.position).normalized * connectionDistance, Color.blue, 1f);
        if (!raycastHit2D) { return; }
        if (raycastHit2D.collider.CompareTag("PlayerTwo"))
        {
            isJoint = true;
            connectedPlayers.ToggleLine();
            thatSkink.GetComponent<SpringJoint2D>().enabled = true;
            otherSkink.GetComponent<SpringJoint2D>().enabled = true;
        }
    }
    public void ClearSplitAction()
    {
        if (splitAction.Count <= 0) { return; }
        splitAction.Clear();
    }
    
    public Vector2 AveragePosition()
    {
        return (thatSkink.transform.position + otherSkink.transform.position) / 2f;
    }
}