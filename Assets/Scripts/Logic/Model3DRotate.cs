using UnityEngine;

public class Model3DRotate : MonoBehaviour
{
    [SerializeField] private float _rotationSensitivity;
    [SerializeField] private Transform _model;
    [SerializeField] private float _autoRotateSpeed;

    private void FixedUpdate()
    {
        if (Input.touchCount > 0)
            RotateModel();
        else
            AutoRotateModel();
    }

    private void AutoRotateModel()
    {
        _model.localEulerAngles += new Vector3(0, _autoRotateSpeed) ;
    }

    public void RotateModel()
    {
        Touch touch = Input.GetTouch(0);
        if(touch.phase == TouchPhase.Moved)
            _model.localEulerAngles += new Vector3(0, -touch.deltaPosition.x) * _rotationSensitivity;
    }
}