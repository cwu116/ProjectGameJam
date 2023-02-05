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
    /// ���״̬��
    /// </summary>
    private class CameraState
    {
        /// <summary>
        /// ����xֵ
        /// </summary>
        public float posX;
        /// <summary>
        /// ����yֵ
        /// </summary>
        public float posY;
        /// <summary>
        /// ����zֵ
        /// </summary>
        public float posZ;
        /// <summary>
        /// ��תxֵ
        /// </summary>
        public float rotX;
        /// <summary>
        /// ��תyֵ
        /// </summary>
        public float rotY;
        /// <summary>
        /// ��תzֵ 
        /// </summary>
        public float rotZ;

        //���������
        private readonly float xMinValue;
        private readonly float xMaxValue;
        private readonly float yMinValue;
        private readonly float yMaxValue;

        /// <summary>
        /// Ĭ�Ϲ��캯��
        /// </summary>
        public CameraState()
        {
            xMinValue = float.MinValue;
            xMaxValue = float.MaxValue;
            yMinValue = float.MinValue;
            yMaxValue = float.MaxValue;
        }
        /// <summary>
        /// ���캯��
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
        /// ����Transform�������״̬
        /// </summary>
        /// <param name="t">Transform���</param>
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
        /// �ƶ�
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
        /// ����Ŀ��״̬��ֵ����
        /// </summary>
        /// <param name="target">Ŀ��״̬</param>
        /// <param name="positionLerpPct">λ�ò�ֵ��</param>
        /// <param name="rotationLerpPct">��ת��ֵ��</param>
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
        /// ����״̬ˢ��Transform���
        /// </summary>
        /// <param name="t">Transform���</param>
        public void UpdateTransform(Transform t)
        {
            t.position = new Vector3(posX, posY, posZ);
            t.rotation = Quaternion.Euler(rotX, rotY, rotZ);
        }
    }

    //���ƿ���
    [SerializeField] private bool toggle = true;

    //�Ƿ����ƻ��Χ
    [SerializeField] private bool isRangeClamped;
    //���Ʒ�Χ ��isRangeClampedΪtrueʱ������
    [SerializeField] private float xMinValue = -100f;   //x��Сֵ
    [SerializeField] private float xMaxValue = 100f;    //x���ֵ
    [SerializeField] private float yMinValue = -100f;   //z��Сֵ
    [SerializeField] private float yMaxValue = 100f;    //z���ֵ

    //�ƶ��ٶ�
    [SerializeField] private float translateSpeed = 10f;
    //����ϵ�� Shift����ʱ������
    [SerializeField] private float boost = 3.5f;
    //��ֵ��Ŀ��λ�������ʱ��
    [Range(0.01f, 1f), SerializeField] private float positionLerpTime = 1f;
    //��ֵ��Ŀ����ת�����ʱ��
    [Range(0.01f, 1f), SerializeField] private float rotationLerpTime = 1f;
    //����˶���������
    [Range(0.1f, 1f), SerializeField] private float mouseMovementSensitivity = 0.5f;
    //�������˶����ٶ�
    [SerializeField] private float mouseScrollMoveSpeed = 10f;
    //�����������ƶ� �Ƿ�ת����
    [SerializeField] private bool invertScrollDirection = false;
    //�ƶ���
    private Vector3 translation = Vector3.zero;
    //��Ե��С
    private readonly float edgeSize = 10f;

    private CameraState initialCameraState;
    private CameraState targetCameraState;
    private CameraState interpolatingCameraState;

    private void Awake()
    {
        //��ʼ��
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
        //U���������õ���ʼ״̬
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

    //��ȡ����
    private Vector3 GetInputTranslation(out bool mouseScroll)
    {
        Vector3 ts = new Vector3();

#if ENABLE_INPUT_SYSTEM
            //��ȡ�����ֹ���ֵ
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
            //��Shift������ʱ����
            if (Keyboard.current.leftShiftKey.isPressed) ts *= boost;
#else
        //if (Input.GetKey(KeyCode.LeftShift)) ts *= boost;
#endif
        mouseScroll = wheelValue != 0f;
        return ts;
    }

    //�жϹ���Ƿ�����Ļ��Ե
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
        //������ƻ��Χ ������Χ���Ƴ���
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
    /// �������������ʼ״̬
    /// </summary>
    public void ResetCamera()
    {
        initialCameraState.UpdateTransform(transform);
        targetCameraState.SetFromTransform(transform);
        interpolatingCameraState.SetFromTransform(transform);
    }
}
