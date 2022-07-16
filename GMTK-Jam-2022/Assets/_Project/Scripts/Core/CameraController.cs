using UnityEngine;

namespace Gisha.GMTK2022.Core
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private float followSpeed;
        [Space] [SerializeField] private Transform minBound;
        [SerializeField] private Transform maxBound;
        
        private float _cameraWidth, _cameraHeight;
        private Camera _cam;

        private void Awake()
        {
            _cam = Camera.main;
        }

        private void Start()
        {
            _cameraHeight = _cam.orthographicSize;
            _cameraWidth = (float) Screen.width / Screen.height * _cameraHeight;
            
            if (target == null)
                Debug.LogError("Target wasn't set.");
        }
        

        private void FixedUpdate()
        {
            if (target == null)
                return;

            FollowTarget();
        }

        private void FollowTarget()
        {
            float targetX = target.position.x;
            float targetY = target.position.y;

            if (targetX + _cameraWidth > maxBound.position.x)
                targetX = maxBound.position.x - _cameraWidth;
            if (targetX - _cameraWidth < minBound.position.x)
                targetX = minBound.position.x + _cameraWidth;
            if (targetY + _cameraHeight > maxBound.position.y)
                targetY = maxBound.position.y - _cameraHeight;
            if (targetY - _cameraHeight < minBound.position.y)
                targetY = minBound.position.y + _cameraHeight;

            Vector3 targetPos = new Vector3(targetX, targetY, -10f);
            transform.position = Vector3.Lerp(transform.position, targetPos, followSpeed * Time.deltaTime);
        }
    }
}