using UnityEngine;

public class Dice : MonoBehaviour, IDrag
{
    private Rigidbody diceRigidBody;
    private bool hasBeenDropped = false;
    private bool waitingToStabilize = false;
    private bool isActiveDice = true;

    private void Awake()
    {
        diceRigidBody = GetComponent<Rigidbody>();
    }

    public void OnStartDrag()
    {
        diceRigidBody.useGravity = false;
    }

    public void OnEndDrag()
    {
        diceRigidBody.useGravity = true;
        hasBeenDropped = true;
        //diceRigidBody.velocity = Vector3.zero;
    }

    public bool CanBeDragged()
    {
        return !hasBeenDropped;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(hasBeenDropped)
        {
            waitingToStabilize = true;
        }
    }

    private void Update()
    {
        if(waitingToStabilize)
        {
            if(diceRigidBody.velocity.magnitude <= 0.1 && isActiveDice )
            {
                TurnManager.Instance.StartNextTurn();
                isActiveDice = false;
            }
        }
    }



}
