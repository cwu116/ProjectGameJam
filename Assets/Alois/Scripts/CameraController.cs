using UnityEngine;

#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

#if UNITY_EDITOR
using UnityEditor;
#endif

public class CameraController : MonoBehaviour
{
    /// <summary>
    /// 相机状态类
    /// </summary>
    private class CameraState
    {
        /// <summary>
        /// 坐标x值
        /// </summary>
        public float posX;
        /// <summary>
        /// 坐标y值
        /// </summary>
        public float posY;
        /// <summary>
        /// 坐标z值
        /// </summary>
        public float posZ;
        /// <summary>
        /// 旋转x值
        /// </summary>
        public float rotX;
        /// <summary>
        /// 旋转y值
        /// </summary>
        public float rotY;
        /// <summary>
        /// 旋转z值 
        /// </summary>
        public float rotZ;

        //活动区域限制
        private readonly float xMinValue;
        private readonly float xMaxValue;
        private readonly float yMinValue;
        private readonly float yMaxValue;

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public CameraState()
        {
            xMinValue = float.MinValue;
            xMaxValue = float.MaxValue;
            yMinValue = float.MinValue;
            yMaxValue = float.MaxValue;
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="xMinValue"></param>
        /// <param name="xMaxValue"></param>
        /// <param name="yMinValue"></param>
        /// <param name="zMaxValue"></param>
        public CameraState(float xMinValue, float xMaxValue, float yMinValue, float yMaxValue)
        {
            this.xMinValue = xMinValue;
            this.xMaxValue = xMaxValue;
            this.yMinValue = yMinValue;
            this.yMaxValue = yMaxValue;
        }

        /// <summary>
        /// 根据Transform组件更新状态
        /// </summary>
        /// <param name="t">Transform组件</param>
        public void SetFromTransform(Transform t)
        {
            posX = t.position.x;
            posY = t.position.y;
            posZ = t.position.z;
            rotX = t.eulerAngles.x;
            rotY = t.eulerAngles.y;
            rotZ = t.eulerAngles.z;
        }
        /// <summary>
        /// 移动
        /// </summary>
        public void Translate(Vector3 translation, bool mouseScroll)
        {
            Vector3 rotatedTranslation = Quaternion.Euler(mouseScroll ? rotX : 0f, rotY, rotZ) * translation;
            posX += rotatedTranslation.x;
            posY += rotatedTranslation.y;
            posZ += rotatedTranslation.z;

            posX = Mathf.Clamp(posX, xMinValue, xMaxValue);
            posY = Mathf.Clamp(posY, yMinValue, yMaxValue);
            //posZ = Mathf.Clamp(posZ, zMinValue, zMaxValue);
        }
        /// <summary>
        /// 根据目标状态插值运算
        /// </summary>
        /// <param name="target">目标状态</param>
        /// <param name="positionLerpPct">位置插值率</param>
        /// <param name="rotationLerpPct">旋转插值率</param>
        public void LerpTowards(CameraState target, float positionLerpPct, float rotationLerpPct)
        {
            posX = Mathf.Lerp(posX, target.posX, positionLerpPct);
            posY = Mathf.Lerp(posY, target.posY, positionLerpPct);
            posZ = Mathf.Lerp(posZ, target.posZ, positionLerpPct);
            rotX = Mathf.Lerp(rotX, target.rotX, rotationLerpPct);
            rotY = Mathf.Lerp(rotY, target.rotY, rotationLerpPct);
            rotZ = Mathf.Lerp(rotZ, target.rotZ, rotationLerpPct);
        }
        /// <summary>
        /// 根据状态刷新Transform组件
        /// </summary>
        /// <param name="t">Transform组件</param>
        public void UpdateTransform(Transform t)
        {
            t.position = new Vector3(posX, posY, posZ);
            t.rotation = Quaternion.Euler(rotX, rotY, rotZ);
        }
    }

    //控制开关
    [SerializeField] private bool toggle = true;

    //是否限制活动范围
    [SerializeField] private bool isRangeClamped;
    //限制范围 当isRangeClamped为true时起作用
    [SerializeField] private float xMinValue = -100f;   //x最小值
    [SerializeField] private float xMaxValue = 100f;    //x最大值
    [SerializeField] private float yMinValue = -100f;   //z最小值
    [SerializeField] private float yMaxValue = 100f;    //z最大值

    //移动速度
    [SerializeField] private float translateSpeed = 10f;
    //加速系数 Shift按下时起作用
    [SerializeField] private float boost = 3.5f;
    //插值到目标位置所需的时间
    [Range(0.01f, 1f), SerializeField] private float positionLerpTime = 1f;
    //插值到目标旋转所需的时间
    [Range(0.01f, 1f), SerializeField] private float rotationLerpTime = 1f;
    //鼠标运动的灵敏度
    [Range(0.1f, 1f), SerializeField] private float mouseMovementSensitivity = 0.5f;
    //鼠标滚轮运动的速度
    [SerializeField] private float mouseScrollMoveSpeed = 10f;
    //用于鼠标滚轮移动 是否反转方向
    [SerializeField] private bool invertScrollDirection = false;
    //移动量
    private Vector3 translation = Vector3.zero;
    //边缘大小
    private readonly float edgeSize = 10f;

    private CameraState initialCameraState;
    private CameraState targetCameraState;
    private CameraState interpolatingCameraState;

    private void Awake()
    {
        //初始化
        if (isRangeClamped)
        {
            initialCameraState = new CameraState(xMinValue, xMaxValue, yMinValue, yMaxValue);
            targetCameraState = new CameraState(xMinValue, xMaxValue, yMinValue, yMaxValue);
            interpolatingCameraState = new CameraState(xMinValue, xMaxValue, yMinValue, yMaxValue);
        }
        else
        {
            initialCameraState = new CameraState();
            targetCameraState = new CameraState();
            interpolatingCameraState = new CameraState();
        }
    }
    private void OnEnable()
    {
        initialCameraState.SetFromTransform(transform);
        targetCameraState.SetFromTransform(transform);
        interpolatingCameraState.SetFromTransform(transform);
    }
    private void LateUpdate()
    {
        if (!toggle) return;
        //if (OnResetUpdate()) return;
        OnTranslateUpdate();
    }

    private bool OnResetUpdate()
    {
#if ENABLE_INPUT_SYSTEM
            bool uPressed = Keyboard.current.uKey.wasPressedThisFrame;
#else
        bool uPressed = Input.GetKeyDown(KeyCode.U);
#endif
        //U键按下重置到初始状态
        if (uPressed)
        {
            ResetCamera();
            return true;
        }
        return false;
    }
    private void OnTranslateUpdate()
    {
        translation = GetInputTranslation(out bool mouseScroll) * Time.deltaTime * translateSpeed;
        targetCameraState.Translate(translation, mouseScroll);
        float positionLerpPct = 1f - Mathf.Exp(Mathf.Log(1f - .99f) / positionLerpTime * Time.deltaTime);
        float rotationLerpPct = 1f - Mathf.Exp(Mathf.Log(1f - .99f) / rotationLerpTime * Time.deltaTime);
        interpolatingCameraState.LerpTowards(targetCameraState, positionLerpPct, rotationLerpPct);
        interpolatingCameraState.UpdateTransform(transform);
    }

    //获取输入
    private Vector3 GetInputTranslation(out bool mouseScroll)
    {
        Vector3 ts = new Vector3();

#if ENABLE_INPUT_SYSTEM
            //读取鼠标滚轮滚动值
            float wheelValue = Mouse.current.scroll.ReadValue().y;
#else
        float wheelValue = Input.GetAxis("Mouse ScrollWheel");
#endif
        ts += (wheelValue == 0 ? Vector3.zero : (wheelValue > 0 ? Vector3.up : Vector3.down) * (invertScrollDirection ? -1 : 1)) * mouseScrollMoveSpeed;

        if (IsMouseOnEdge(out Vector2 direction))
        {
            ts += (Vector3.right * direction.x + Vector3.up * direction.y) * mouseMovementSensitivity;
        }
#if ENABLE_INPUT_SYSTEM
            //左Shift键按下时加速
            if (Keyboard.current.leftShiftKey.isPressed) ts *= boost;
#else
        //if (Input.GetKey(KeyCode.LeftShift)) ts *= boost;
#endif
        mouseScroll = wheelValue != 0f;
        return ts;
    }

    //判断光标是否处于屏幕边缘
    private bool IsMouseOnEdge(out Vector2 direction)
    {
        direction = Vector2.zero;
        bool flag = Input.mousePosition.x <= edgeSize || Input.mousePosition.x >= Screen.width - edgeSize;
        if (flag)
        {
            direction += Input.mousePosition.x < edgeSize ? Vector2.left : Vector2.right;
        }
        if (Input.mousePosition.y <= edgeSize || Input.mousePosition.y >= Screen.height - edgeSize)
        {
            direction += Input.mousePosition.y < edgeSize ? Vector2.down : Vector2.up;
            flag = true;
        }
        direction = direction.normalized;
        return flag;
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        //如果限制活动范围 将区域范围绘制出来
        if (isRangeClamped)
        {
            Handles.color = Color.cyan;
            Vector3[] points = new Vector3[8]
            {
                    new Vector3(xMinValue, yMinValue, 0f),
                    new Vector3(xMaxValue, yMinValue, 0f),
                    new Vector3(xMaxValue, yMaxValue, 0f),
                    new Vector3(xMinValue, yMaxValue, 0f),
                    new Vector3(xMinValue, yMinValue, 10f),
                    new Vector3(xMaxValue, yMinValue, 10f),
                    new Vector3(xMaxValue, yMaxValue, 10f),
                    new Vector3(xMinValue, yMaxValue, 10f)
            };
            for (int i = 0; i < 4; i++)
            {
                int start = i % 4;
                int end = (i + 1) % 4;
                Handles.DrawLine(points[start], points[end]);
                Handles.DrawLine(points[start + 4], points[end + 4]);
                Handles.DrawLine(points[start], points[i + 4]);
            }
        }
    }
#endif

    /// <summary>
    /// 重置摄像机到初始状态
    /// </summary>
    public void ResetCamera()
    {
        initialCameraState.UpdateTransform(transform);
        targetCameraState.SetFromTransform(transform);
        interpolatingCameraState.SetFromTransform(transform);
    }
}
