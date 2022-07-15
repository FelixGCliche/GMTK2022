using UnityEngine;

public class Dice : MonoBehaviour, IDrag
{
    private Rigidbody diceRigidBody;

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
        diceRigidBody.velocity = Vector3.zero;
    }

}
