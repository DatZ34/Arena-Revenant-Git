using UnityEngine;
using Unity.Cinemachine;
public class CharacterCamera : MonoBehaviour
{
    // Chỉ lo:
        // Bật tắt InputAxisControllerCamera
        // xuất ra giá trị currentRotY và newRotY

    [SerializeField] private CinemachineInputAxisController controllerCamera;
    private CinemachineCamera cineCam; 
    [SerializeField] private Transform trackingTargetCam; // vị trí theo dõi cần gán vào ở cine
    private float rotY
    {
        get
        {
            return controllerCamera.transform.localEulerAngles.y;
        }
    }
    public float currentRotY;
    public float newRotY;

    void Awake()
    {
        if(trackingTargetCam == null) 
        {
            // CẦN LƯU Ý: 
            // Sau này khi spawn model nhân vật cần 1 script để lấy ngay Transform "neck_01" trong root của model 
            // Và gán vào trackingTargetCam
            Debug.LogWarning("Không tìm thấy transform 'neck_01' để gán vào Cinemachine");
        }

        if(controllerCamera == null)
        {
            GameObject camObj = GameObject.FindWithTag("PlayerCamera");
            if(camObj != null)
            {
                controllerCamera = camObj.GetComponent<CinemachineInputAxisController>();
                cineCam = camObj.GetComponent<CinemachineCamera>();
                if(cineCam != null)
                {
                    cineCam.Follow = trackingTargetCam;
                    cineCam.LookAt = trackingTargetCam;
                }
            }
        }
    }


    void Update()
    {
        HandleInputAxisCamera();
    }

    void HandleInputAxisCamera() // cần đưa qua Scritp Input
    {
        if (Input.GetMouseButton(0))
        {
            controllerCamera.enabled = true;
            currentRotY = rotY;
        }
        else
        {
            controllerCamera.enabled = false;
            newRotY = currentRotY;
        }
    }
}
