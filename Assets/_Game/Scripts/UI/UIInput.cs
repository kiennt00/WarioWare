using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInput : UICanvas
{
    [SerializeField] List<PressButton> listPressButton = new();

    private void Awake()
    {
        for (int i = 0; i < listPressButton.Count; i++)
        {
            // fact: error if using listPressButton[i] directly instead of button

            PressButton button = listPressButton[i];

            button.SetOnPressAction(() =>
            {
                this.PostEvent(EventID.OnPressAction, button.Type);
            });

            button.SetOnPointerDownAction(() =>
            {
                this.PostEvent(EventID.OnPointerDownAction, button.Type);
            });

            button.SetOnPointerUpAction(() =>
            {
                this.PostEvent(EventID.OnPointerUpAction, button.Type);
            });
        }
    }
}
