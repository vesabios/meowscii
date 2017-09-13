using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Ink.Runtime;

public class InkManager : MonoBehaviour
{
    public static InkManager instance;
	static InputFrame inkInputFrame;


    [SerializeField]
    private TextAsset inkJSONAsset;
    private static Story story;


    void Awake()
    {
		if (instance != null)
			return;
		
        instance = this;

		inkInputFrame = gameObject.AddComponent<InkInputFrame>();


    }


	public static void ExitStory() {
		InkConsole.instance.ClearAll();
		inkInputFrame.ActivateLast();
	}

    public static void StartStory()
    {
        story = new Story(instance.inkJSONAsset.text);

        story.BindExternalFunction("SetScene", (string arg1) => {

            // SceneManager.instance.SwitchScene(arg1);
            // new scene name is arg1

            return null;

        });

		inkInputFrame.Activate ();

        RefreshView();
    }

    static void RefreshView()
    {

        InkConsole.instance.ClearAll();



        while (story.canContinue)
        {
            string text = story.Continue().Trim();
            CreateContentView(text);
        }

        if (story.currentChoices.Count > 0)
        {
            for (int i = 0; i < story.currentChoices.Count; i++)
            {
                Choice choice = story.currentChoices[i];

                CreateChoiceView(
                    choice.text.Trim(), 
                    delegate {
                        OnClickChoiceButton(choice);
                    });
            }
        }
        else
        {
			
            CreateChoiceView(
                "[X]", 
                delegate {
					ExitStory();

                });


        }

		//Debug.Log ("refreshing view...");
		ScreenComponent.redraw = true;

    }

    static void OnClickChoiceButton(Choice choice)
    {
        story.ChooseChoiceIndex(choice.index);
        RefreshView();
    }

    static void CreateContentView(string text)
    {

		// render text, presumably in a box
		InkConsole.instance.AddText(text);


    }

    static void CreateChoiceView(string text, UnityEngine.Events.UnityAction callback)
    {

        InkConsole.instance.AddButton("> "+text, callback);

    }

	public static void Escape() {
		ExitStory ();

	}

}
