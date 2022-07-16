using System.Collections;
using UnityEngine;
using DG.Tweening;

public class Dice : MonoBehaviour, IDrag
{
    public delegate void ChangeStateDelegate(DiceState newDiceState);
    public event ChangeStateDelegate ChangeState;

    [Header("Sides")]
    [SerializeField] public Vector3[] sidesAngles;

    [Header("SFX")]
    [SerializeField] public AudioClip[] grabSFXs;
    [SerializeField] public AudioClip[] dropSFXs;
    [SerializeField] public AudioClip[] rollSFXs;

    [Header("Drop down laser")]
    [SerializeField] private GameObject dropDownImpact;

    private Rigidbody diceRigidBody;
    private LineRenderer dropDownLaserLineRenderer;
    private GameObject dropDownImpactInstance;

    private IEnumerator dropDownLaserCoroutine;

    private int rollValue = 1;
    private DiceState diceState = DiceState.SPINNING;

    private QuickOutline quickOutline;

    private void Awake()
    {
        diceRigidBody = GetComponent<Rigidbody>();
        dropDownLaserLineRenderer = GetComponentInChildren<LineRenderer>();
        dropDownImpactInstance = Instantiate(dropDownImpact);
        quickOutline = GetComponent<QuickOutline>();
    }

    private void Start()
    {
        quickOutline.enabled = false;
        Roll();
    }

    private void Roll()
    {
        rollValue = Random.Range(0, sidesAngles.Length) + 1;
        StartCoroutine(Spin());
    }

    private IEnumerator Spin()
    {
        ChangeDiceState(DiceState.SPINNING);
            yield return null;
        SoundManager.Instance.PlaySfx(rollSFXs[0], transform.position, true);
        diceRigidBody.useGravity = false;

        for (int i = 0; i < 3; i++)
        {
            transform.DORotate(new Vector3(320, 280 , 300), 0.3f, RotateMode.WorldAxisAdd);
            yield return new WaitForSeconds(0.25f);
        }
        transform.DORotate(sidesAngles[rollValue-1], 0.1f);
        float diceScale = 0.5f + (1f * rollValue / sidesAngles.Length);
        transform.DOScale(transform.localScale * diceScale, 0.1f);
        yield return new WaitForSeconds(0.2f);
        
        ChangeDiceState(DiceState.PICKABLE);
        quickOutline.enabled = true;
        diceRigidBody.useGravity = true;
    }

    public void OnStartDrag()
    {
        dropDownLaserCoroutine = DropDownLaser();
        StartCoroutine(dropDownLaserCoroutine);

        diceRigidBody.useGravity = false;
        ChangeDiceState( DiceState.PICKED);
        diceRigidBody.constraints = RigidbodyConstraints.FreezeRotation;
        SoundManager.Instance.PlaySfx(grabSFXs[Random.Range(0, grabSFXs.Length)], transform.position, true);
    }

    public void OnEndDrag()
    {
        StopCoroutine(dropDownLaserCoroutine);
        HideDropDownLaser();

        diceRigidBody.useGravity = true;
        quickOutline.enabled = false;
        ChangeDiceState(DiceState.FALLING);
        diceRigidBody.constraints = RigidbodyConstraints.None;
        //diceRigidBody.velocity = Vector3.zero;
    }

    public bool CanBeDragged()
    {
        return diceState == DiceState.PICKABLE;
    }

    private void OnCollisionEnter(Collision collision)
    {
        SoundManager.Instance.PlaySfx(dropSFXs[Random.Range(0, dropSFXs.Length)], transform.position, true);
        if(diceState == DiceState.FALLING)
        {
            ChangeDiceState(DiceState.STABILIZING);
        }
    }

    private void Update()
    {
        if(diceState == DiceState.STABILIZING)
        {
            if(diceRigidBody.velocity.magnitude <= 0.1 && diceState != DiceState.FALLEN)
            {
                DiceGameManager.Instance.EndCurrentTurn();
                ChangeDiceState(DiceState.FALLEN);
            }
        }
    }

    private IEnumerator DropDownLaser()
    {
        while(true)
        {
            dropDownLaserLineRenderer.SetPosition(0, transform.position);

            RaycastHit hit;
            if(Physics.Raycast(transform.position, Vector3.down, out hit, Mathf.Infinity, LayerMask.GetMask("Default") | LayerMask.GetMask("Draggable")))
            {
                dropDownLaserLineRenderer.SetPosition(1, hit.point);
                dropDownImpactInstance.transform.position = hit.point;
            }
            else
            {
                dropDownLaserLineRenderer.SetPosition(1, transform.position);  
                dropDownImpactInstance.transform.position = transform.position;
            }
            yield return null;
        }
    }

    private void HideDropDownLaser()
    {
        Destroy(dropDownLaserLineRenderer.gameObject);
        Destroy(dropDownImpactInstance.gameObject);
    }

    private void ChangeDiceState(DiceState newDiceState)
    {
        diceState = newDiceState;
        if(ChangeState != null)
            ChangeState(diceState);
    }

}
