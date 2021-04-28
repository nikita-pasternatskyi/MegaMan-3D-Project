using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class CommandsTest : NetworkBehaviour
{
    public int ChangeableNumber = 0;

    [SerializeField] private int myNumber;
    [SerializeField] private float numberEffector;
    //If i need this number to be changed by server, AND accessed by player - i need syncvar
    [SyncVar][SerializeField] private float resultNumber;

    public override void OnStartLocalPlayer()
    {
        CalculateResultOnServer();
    }

    private void Awake()
    {       
        ChangeableNumber = 2;
    }


    #region
    [Command]
    private void CalculateResultOnServer()
    {
        CalculateResult();
        //resultNumber = myNumber + numberEffector;
        //Debug.Log(resultNumber);
    }

    //Executes ONLY on local client and doesnt give any information to the server
    private void CalculateResult()
    {
        resultNumber = myNumber + numberEffector;
        myNumber = 5;
        Debug.Log(resultNumber);
    }

    #endregion
}
