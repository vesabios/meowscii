using UnityEngine;
using System.Collections;

public class GUIText : GUIElement {

    public string text;

    Color32 brush;
    Color32 activeBrush;

    int cursorIndex = 0;


    public void Start()
    {
        brush = Screen.GenerateBrush();
        activeBrush = Screen.GenerateBrush(63, 1);
    }


    public override void LocalDraw()
    {
        globalRect.width = rect.width = text.Length;
        if (active)
        {
            GUI.DrawString((int)globalRect.xMin, (int)globalRect.yMin, text, activeBrush);


        } else
        {
            GUI.DrawString((int)globalRect.xMin, (int)globalRect.yMin, text, brush);

        }


    }

	protected override void Select() {
        if (bCanActivate)
            Activate();
    }

    public override void CheckKeyboard()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) { ActivateLast(); }

        if (Input.GetKeyDown(KeyCode.Return)) { ActivateLast(); }

        if (Input.GetKeyDown(KeyCode.Escape)) { }
        if (Input.GetKeyDown(KeyCode.Backspace)) { }
        if (Input.GetKeyDown(KeyCode.Space)) { }


        if (Input.GetKeyDown(KeyCode.A)) { }
        if (Input.GetKeyDown(KeyCode.B)) { }
        if (Input.GetKeyDown(KeyCode.C)) { }
        if (Input.GetKeyDown(KeyCode.D)) { }
        if (Input.GetKeyDown(KeyCode.E)) { }
        if (Input.GetKeyDown(KeyCode.F)) { }
        if (Input.GetKeyDown(KeyCode.G)) { }
        if (Input.GetKeyDown(KeyCode.H)) { }
        if (Input.GetKeyDown(KeyCode.I)) { }
        if (Input.GetKeyDown(KeyCode.J)) { }
        if (Input.GetKeyDown(KeyCode.K)) { }
        if (Input.GetKeyDown(KeyCode.L)) { }
        if (Input.GetKeyDown(KeyCode.M)) { }
        if (Input.GetKeyDown(KeyCode.N)) { }
        if (Input.GetKeyDown(KeyCode.O)) { }
        if (Input.GetKeyDown(KeyCode.P)) { }
        if (Input.GetKeyDown(KeyCode.Q)) { }
        if (Input.GetKeyDown(KeyCode.R)) { }
        if (Input.GetKeyDown(KeyCode.S)) { }
        if (Input.GetKeyDown(KeyCode.T)) { }
        if (Input.GetKeyDown(KeyCode.U)) { }
        if (Input.GetKeyDown(KeyCode.V)) { }
        if (Input.GetKeyDown(KeyCode.W)) { }
        if (Input.GetKeyDown(KeyCode.X)) { }
        if (Input.GetKeyDown(KeyCode.Y)) { }
        if (Input.GetKeyDown(KeyCode.Z)) { }

    }

}
