using CodeMonkey.Utils;
using UnityEngine;
using UnityEngine.UI;

public class ArrowPointer : MonoBehaviour
{
    public Transform targetTransform;


    public float offset = 200f;

    [SerializeField]
    private Camera UICamera;

    RectTransform rectTransform;

    Image image;

    Camera mainCam;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        image = GetComponent<Image>();
        mainCam = Camera.main;
        UICamera = GameObject.FindGameObjectWithTag("Uicamera").GetComponent<Camera>();

    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        if (targetTransform != null)
        {
            Vector3 fromPosition = mainCam.transform.position;
            Vector3 toPosition = targetTransform.position;
            fromPosition.z = 0;
            Vector3 dir = (targetTransform.GetComponent<ship>().currDirection * -1f).normalized;

            float angle = UtilsClass.GetAngleFromVector(dir);

            rectTransform.localEulerAngles = new Vector3(0, 0, angle);

            Vector3 targetScreenPosition = mainCam.WorldToScreenPoint(toPosition);
            bool isOffscreen = targetScreenPosition.x <= offset || targetScreenPosition.x >= Screen.width - offset || targetScreenPosition.y <= offset || targetScreenPosition.y >= Screen.height - offset;

            if (isOffscreen)
            {
                image.enabled = true;
                Vector3 cappedTargetPosition = targetScreenPosition;
                if (cappedTargetPosition.x <= offset)
                {
                    cappedTargetPosition.x = offset;
                }
                if (cappedTargetPosition.x >= Screen.width - offset)
                {
                    cappedTargetPosition.x = Screen.width - offset;
                }
                if (cappedTargetPosition.y <= offset)
                {
                    cappedTargetPosition.y = offset;
                }
                if (cappedTargetPosition.y >= Screen.height - offset)
                {
                    cappedTargetPosition.y = Screen.height - offset;
                }

                Vector3 pointerWorldPosition = UICamera.ScreenToWorldPoint(cappedTargetPosition);
                rectTransform.position = pointerWorldPosition;
                rectTransform.localPosition = new Vector3(rectTransform.localPosition.x, rectTransform.localPosition.y, 0);
            }
            else
            {
                image.enabled = false;
                Destroy(gameObject);
            }

        }
        else
        {
            Destroy(gameObject);
        }


    }
}
