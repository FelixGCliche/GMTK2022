//Inspired by https://www.youtube.com/watch?v=HfqRKy5oFDQ

using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;

public class DragAndDropController : MonoBehaviour
{
    [SerializeField] private InputAction mouseClick;
    [SerializeField] private float mouseDragPhysicsSpeed = 10f;
    [SerializeField] private float mouseDragSpeed = 0.1f;
    [SerializeField] private SoundManager soundManager;

    private Camera mainCamera;
    private Vector3 velocity = Vector3.zero;
    private WaitForFixedUpdate waitForFixedUpdate = new WaitForFixedUpdate();

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    private void OnEnable()
    {
        mouseClick.Enable();
        mouseClick.performed += MousePressed;
    }

    private void OnDisable()
    {
        mouseClick.performed -= MousePressed;
        mouseClick.Disable();
    }

    public void MousePressed(InputAction.CallbackContext context)
    {
        Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Draggable")))
        {
            if(hit.collider != null && (hit.collider.gameObject.CompareTag("Draggable") || (hit.collider.gameObject.GetComponent<IDrag>() != null && hit.collider.gameObject.GetComponent<IDrag>().CanBeDragged())))
            {
                soundManager.OnGrab();
                StartCoroutine(DragUpdate(hit.collider.gameObject));
            }
        }
    }

    private IEnumerator DragUpdate(GameObject clickedObject)
    {
        clickedObject.TryGetComponent<Rigidbody>(out var rigidbody);
        clickedObject.TryGetComponent<IDrag>(out var iDragComponent);
        iDragComponent?.OnStartDrag();
        while(mouseClick.ReadValue<float>() != 0)
        {
            Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
            RaycastHit hit;
            Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("DroppingZone"));

            if(rigidbody != null)
            {
                Vector3 direction = hit.point - clickedObject.transform.position;
                rigidbody.velocity = direction * mouseDragPhysicsSpeed;
                yield return waitForFixedUpdate;
            }
            
            else
            {
                clickedObject.transform.position = Vector3.SmoothDamp(clickedObject.transform.position, hit.point, ref velocity, mouseDragSpeed);
                yield return null;
            }
            
        }
        iDragComponent?.OnEndDrag();
    }
}