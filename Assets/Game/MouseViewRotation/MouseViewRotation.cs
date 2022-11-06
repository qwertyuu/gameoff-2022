using UnityEngine;

public class MouseViewRotation : MonoBehaviour
{
    [SerializeField] private float sensitivity = 360;
    [SerializeField] private Transform cameraHolder = null;

    private float _verticalLook;
    private float _horizontalLook;
    
    private void LateUpdate()
    {
        float dt = Time.deltaTime;
        RotateCharacterHorizontal(dt);
        RotateCameraVertical(dt);
    }
    
    private void RotateCharacterHorizontal(float deltaTime)
    {
        transform.rotation = Quaternion.identity;
        _horizontalLook += Input.GetAxis("Mouse X") * deltaTime * sensitivity;
        transform.Rotate(0, _horizontalLook, 0);
    }

    private void RotateCameraVertical(float deltaTime)
    {
        cameraHolder.Rotate(-_verticalLook, 0, 0);
        _verticalLook -= Input.GetAxis("Mouse Y") * deltaTime * sensitivity;
        _verticalLook = Mathf.Clamp(_verticalLook, -89, 89);
        cameraHolder.Rotate(_verticalLook, 0, 0);
    }
}
