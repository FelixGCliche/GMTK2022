using System.Collections;
using UnityEngine;
using DG.Tweening;

public class Dice : MonoBehaviour, IDrag
{
    [Header("Sides")]
    [SerializeField] public Vector3[] sidesAngles;

    [Header("SFX")]
    [SerializeField] public AudioClip[] grabSFXs;
    [SerializeField] public AudioClip[] dropSFXs;

    private Rigidbody diceRigidBody;
    private bool hasBeenDropped = false;
    private bool waitingToStabilize = false;
    private bool isActiveDice = true;
    private int rollValue = 1;
    private DiceState diceState = DiceState.SPINNING;

    private void Awake()
    {
        diceRigidBody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        Roll();
    }

    private void Roll()
    {
        rollValue = Random.Range(0, sidesAngles.Length) + 1;
        StartCoroutine(Spin());
        //transform.rotation = Quaternion.Euler(sidesAngles[rollValue-1]);
    }

    private IEnumerator Spin()
    {
        diceState = DiceState.SPINNING;
        diceRigidBody.useGravity = false;

        for (int i = 0; i < 3; i++)
        {
            transform.DORotate(new Vector3(320, 280 , 300), 0.3f, RotateMode.WorldAxisAdd);
            yield return new WaitForSeconds(0.25f);
        }
        transform.DORotate(sidesAngles[rollValue-1], 0.1f);
        yield return new WaitForSeconds(0.2f);
        
        diceState = DiceState.PICKABLE;
        diceRigidBody.useGravity = true;
    }

    public void OnStartDrag()
    {
        diceRigidBody.useGravity = false;
        diceState = DiceState.PICKED;
        diceRigidBody.constraints = RigidbodyConstraints.FreezeRotation;
        SoundManager.Instance.PlaySfx(grabSFXs[Random.Range(0, grabSFXs.Length)], transform.position);
    }

    public void OnEndDrag()
    {
        diceRigidBody.useGravity = true;
        diceState = DiceState.FALLING;
        diceRigidBody.constraints = RigidbodyConstraints.None;
        hasBeenDropped = true;
        //diceRigidBody.velocity = Vector3.zero;
    }

    public bool CanBeDragged()
    {
        return diceState == DiceState.PICKABLE;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(hasBeenDropped)
        {
            diceState = DiceState.FALLEN;
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
