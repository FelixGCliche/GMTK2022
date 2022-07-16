using UnityEngine;

public class Dice : MonoBehaviour, IDrag
{
    [SerializeField] public AudioClip[] grabSFXs;
    [SerializeField] public AudioClip[] dropSFXs;

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
        diceRigidBody.constraints = RigidbodyConstraints.FreezeRotation;
        SoundManager.Instance.PlaySfx(grabSFXs[Random.Range(0, grabSFXs.Length)], transform.position);
    }

    public void OnEndDrag()
    {
        diceRigidBody.useGravity = true;
        diceRigidBody.constraints = RigidbodyConstraints.None;
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
            SoundManager.Instance.PlaySfx(dropSFXs[Random.Range(0, dropSFXs.Length)], transform.position);
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
