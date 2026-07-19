using UnityEngine;
using Unity.Cinemachine;

[RequireComponent(typeof(CharacterInput))]
public class CharacterCamera : MonoBehaviour
{
    // Chỉ lo:
        // Bật tắt InputAxisControllerCamera
        // xoay chiểu Y của nhân vật dựa trên Input mouse và góc xoay của cam
    
    // === Tiến độ === 
        // Tạm thời hoàn chỉnh

    [Header("Scripts Preferences")]
    [SerializeField] private CharacterInput characterInput;

    [Header("Properties")]
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
    private float currentRotY;
    private float newRotY;

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
        if(characterInput == null)
        {
            characterInput = GetComponent<CharacterInput>();
        }
    }


    void Update()
    {
        HandleInputAxisCamera();
    }

    void HandleInputAxisCamera() // cần đưa qua Scritp Input
    {
        if (characterInput.isLeftMousePressed)
        {
            controllerCamera.enabled = true;
            currentRotY = rotY;
        }
        else
        {
            controllerCamera.enabled = false;
            newRotY = currentRotY;
            Vector3 currentEuler = transform.rotation.eulerAngles;
            currentEuler.y = newRotY;
            transform.rotation = Quaternion.Euler(currentEuler);
        }
    }
}
