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
        //diceRigidBody.velocity = Vector3.zero;
    }

    public bool CanBeDragged()
    {
        return diceState == DiceState.PICKABLE;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(diceState == DiceState.FALLING)
        {
            diceState = DiceState.STABILIZING;
            SoundManager.Instance.PlaySfx(dropSFXs[Random.Range(0, dropSFXs.Length)], transform.position);
        }
    }

    private void Update()
    {
        if(diceState == DiceState.STABILIZING)
        {
            if(diceRigidBody.velocity.magnitude <= 0.1 && diceState != DiceState.FALLEN)
            {
                TurnManager.Instance.StartNextTurn();
                diceState = DiceState.FALLEN;
            }
        }
    }

}
