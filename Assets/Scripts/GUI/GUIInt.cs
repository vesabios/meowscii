using UnityEngine;
using System.Collections;

public class GUIInt : GUIElement
{

    public int value;

    string valueString;
    int originalValue;

    Color32 activeBrush;
    Color32 inactiveBrush;

    int cursorIndex = 0;

    bool typedInYet = false;

    static float cursorBlinkTimer = 0;


    public void Start()
    {
        activeBrush = Screen.GenerateBrush(35, 3);
        inactiveBrush = Screen.GenerateBrush(63, 57);

    }

    public bool ShouldDrawCursor()
    {
        cursorBlinkTimer += Time.deltaTime;
        if (cursorBlinkTimer < 0.25f)
            return true;
        if (cursorBlinkTimer > 0.5f)
            cursorBlinkTimer = 0;

        return false;
    }


    public override void LocalDraw()
    {
        globalRect.width = rect.width = 4;


        if (active)
        {
            GUI.DrawString((int)globalRect.xMin, (int)globalRect.yMin, "      ", activeBrush);

            GUI.DrawString((int)globalRect.xMin, (int)globalRect.yMin, valueString, activeBrush);
            if (ShouldDrawCursor())
                GUI.DrawString((int)globalRect.xMin+valueString.Length, (int)globalRect.yMin, ((char)219).ToString(), activeBrush);
        }
        else
        {
            GUI.DrawString((int)globalRect.xMin, (int)globalRect.yMin, "      ", inactiveBrush);

            GUI.DrawString((int)globalRect.xMin, (int)globalRect.yMin, value.ToString(), inactiveBrush);

        }


    }

    public override void OnDestroy()
    {
        field.SetValue(objectToInspect, value);
        base.OnDestroy();

    }

	protected override void Select()
    {
        if (active) return;
        if (bCanActivate)
        {
            Activate();
            typedInYet = false;
            originalValue = value;
            valueString = value.ToString();
        }
    }

    public override void CheckKeyboard()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            value = originalValue;
            ActivateLast();
        }

        if (Input.GetKeyDown(KeyCode.Return)) {
            ActivateLast();
        }

        if (Input.GetKeyDown(KeyCode.Backspace)) {
            int len = valueString.Length;
            if (len>0)
                valueString = valueString.Substring(0, len - 1);

        }

        if (Input.GetKeyDown(KeyCode.Space)) {
            valueString = "";
        }

        if (valueString.Length > 5) return;

        if (Input.GetKeyDown(KeyCode.Alpha0)) { CheckTyped(); valueString += "0"; }
        if (Input.GetKeyDown(KeyCode.Alpha1)) { CheckTyped(); valueString += "1"; }
        if (Input.GetKeyDown(KeyCode.Alpha2)) { CheckTyped(); valueString += "2"; }
        if (Input.GetKeyDown(KeyCode.Alpha3)) { CheckTyped(); valueString += "3"; }
        if (Input.GetKeyDown(KeyCode.Alpha4)) { CheckTyped(); valueString += "4"; }
        if (Input.GetKeyDown(KeyCode.Alpha5)) { CheckTyped(); valueString += "5"; }
        if (Input.GetKeyDown(KeyCode.Alpha6)) { CheckTyped(); valueString += "6"; }
        if (Input.GetKeyDown(KeyCode.Alpha7)) { CheckTyped(); valueString += "7"; }
        if (Input.GetKeyDown(KeyCode.Alpha8)) { CheckTyped(); valueString += "8"; }
        if (Input.GetKeyDown(KeyCode.Alpha9)) { CheckTyped();  valueString += "9"; }

        if (valueString=="")
        {
            value = 0;
            return;
        }

        value = System.Convert.ToInt32(valueString);


    }

    void CheckTyped()
    {
        if (!typedInYet)
        {
            typedInYet = true;
            valueString = "";
        }
    }



}
