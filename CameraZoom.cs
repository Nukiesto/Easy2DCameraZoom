using GameEngine.UsefulScripts;
using UnityEngine;
using UnityEngine.U2D;

namespace Game.GameCamera
{
    [RequireComponent(typeof(Camera))]
    public class CameraZoom : Singleton<CameraZoom>
    {
        //[Header("SpeedMovingCamera")]
        //[SerializeField] private float speedDefault = 8f;
        //[SerializeField] private float speedZoomMultiplier = 1;
        
        [Header("Zoom")]
        [SerializeField] private int zoomDefault;
        [SerializeField] private int zoomMax;
        [SerializeField] private int zoomMin;
        [SerializeField] private float zoomSpeedDefault;
        [SerializeField] private float zoomSpeedMultiplier = 1;
        
        private float _zoomCurrent;
        private float _zoomSpeedCurrent;
        //private float _speedCurrent;
        private PixelPerfectCamera _pixelPerfectCamera;
        
        private void Awake()
        {
            _pixelPerfectCamera = GetComponent<PixelPerfectCamera>();
            _zoomCurrent = zoomDefault;
            _zoomSpeedCurrent = zoomSpeedDefault;
            //_speedCurrent = speedDefault;
        }

        private void Update()
        {
            //var speedVector = new Vector3()
            //{
            //    x = Input.GetAxis("Horizontal") * _speedCurrent,
            //    y = Input.GetAxis("Vertical") * _speedCurrent,
            //    z = 0
            //};

            //ransform.position += speedVector;

            var deltaY = Input.mouseScrollDelta.y;
            if (deltaY != 0)
            {
                if (deltaY < 0)
                    _zoomCurrent -= _zoomSpeedCurrent;
                else
                    _zoomCurrent += _zoomSpeedCurrent;
                
                _zoomCurrent = Mathf.Clamp(_zoomCurrent, zoomMin, zoomMax);

                if (_zoomCurrent > zoomDefault)
                {
                    var c = _zoomCurrent / zoomDefault;
                    _zoomSpeedCurrent = zoomSpeedDefault / c * zoomSpeedMultiplier ;
                    //_speedCurrent = speedDefault / c * speedZoomMultiplier;
                }
                else
                {
                    var c = zoomDefault / _zoomCurrent;
                    _zoomSpeedCurrent = zoomSpeedDefault * c * zoomSpeedMultiplier;
                    //_speedCurrent = speedDefault * c * speedZoomMultiplier;
                }
                    
                
                //UpdateZoomSpeed();
                //if (_zoomCurrent % 2 != 0)
                _pixelPerfectCamera.assetsPPU = (int)_zoomCurrent;
            }

            //void UpdateZoomSpeed()
            //{
                
            //}
        }
        
        public static void AttachTo(GameObject gameObject)
        {
            var transform = Instance.transform;
            var pos = gameObject.transform.position;
            pos.z = transform.position.z;
            transform.position = pos;
        }
    }
}