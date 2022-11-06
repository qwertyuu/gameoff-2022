using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private CharacterController controller;
    [SerializeField] private float speed = 5;
    [SerializeField] private float rotationSpeed = 4;
    [SerializeField] private Transform cameraTransform;

    void Update()
    {
        float strife = Input.GetAxis("Horizontal");
        float acceleration = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(strife, 0, acceleration);
        movement = Vector3.ClampMagnitude(movement, 1);
        movement = transform.TransformVector(movement) * (speed * Time.deltaTime);

        controller.Move(movement);


        float lookHorizontal = Input.GetAxis("Mouse X");
        float lookVertical = Input.GetAxis("Mouse Y");

        transform.Rotate(Vector3.up, lookHorizontal * rotationSpeed);
        cameraTransform.Rotate(Vector3.right, lookVertical * rotationSpeed);
    }
}