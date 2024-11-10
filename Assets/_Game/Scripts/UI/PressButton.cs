using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PressButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] PressButtonType type;
    public PressButtonType Type => type;

    [SerializeField] bool increaseValue = true;
    [SerializeField] float increaseRate = 1f;
    [SerializeField] bool decreaseIfNoPress = false;
    [SerializeField] float decreaseRate = 1f;

    [SerializeField] Image icon;

    private bool isPressing = false;
    private Action onPressAction;
    private Action onPointerUpAction;
    private Action onPointerDownAction;
    private Action onPressFixedUpdateAction;

    private float normalizedValue = 0f;
    public float NormalizedValue => normalizedValue;

    private void FixedUpdate()
    {
        if (isPressing)
        {
            onPressFixedUpdateAction?.Invoke();
        }
    }

    private void Update()
    {
        if (isPressing)
        {
            if (increaseValue)
            {
                normalizedValue += Time.deltaTime * increaseRate;
                normalizedValue = Mathf.Clamp01(normalizedValue);
            }
            onPressAction?.Invoke();
        }
        else
        {
            if (increaseValue)
            {
                if (normalizedValue > 0f)
                {
                    if (decreaseIfNoPress)
                    {
                        normalizedValue -= Time.deltaTime * decreaseRate;
                        if (normalizedValue <= 0f)
                            normalizedValue = 0f;
                    }
                    else
                    {
                        normalizedValue = 0f;
                    }
                }
            }
        }
    }

    public void SetOnPressFixedAction(Action action)
    {
        onPressFixedUpdateAction = action;
    }

    public void SetOnPressAction(Action action)
    {
        onPressAction = action;
    }

    public void SetOnPointerDownAction(Action action)
    {
        onPointerDownAction = action;        
    }

    public void SetOnPointerUpAction(Action action)
    {
        onPointerUpAction = action;        
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (icon != null)
        {
            icon.transform.localScale = Vector3.one * 0.9f;
            icon.color = new Vector4(200f / 255f, 200f / 255f, 200f / 255f, 1f);
        }
        isPressing = true;
        onPointerDownAction?.Invoke();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (icon != null)
        {
            icon.transform.localScale = Vector3.one;
            icon.color = new Vector4(1f, 1f, 1f, 1f);
        }
        isPressing = false;
        onPointerUpAction?.Invoke();
    }

    private void OnDisable()
    {
        isPressing = false;
        normalizedValue = 0f;
    }
}
