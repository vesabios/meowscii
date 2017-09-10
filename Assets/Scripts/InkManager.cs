using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Ink.Runtime;

public class InkManager : MonoBehaviour
{
    public static InkManager instance;

    [SerializeField]
    private TextAsset inkJSONAsset;
    private Story story;


    void Awake()
    {
        instance = this;
    }

    public void StartStory()
    {
        story = new Story(inkJSONAsset.text);

        story.BindExternalFunction("SetScene", (string arg1) => {

            // SceneManager.instance.SwitchScene(arg1);
            // new scene name is arg1

            return null;

        });

        RefreshView();
    }

    void RefreshView()
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
                "End of story.\nRestart?", 
                delegate {
                    StartStory();
                });
        }

		//Debug.Log ("refreshing view...");
		ScreenComponent.redraw = true;

    }

    void OnClickChoiceButton(Choice choice)
    {
        story.ChooseChoiceIndex(choice.index);
        RefreshView();
    }

    void CreateContentView(string text)
    {

        InkConsole.instance.AddText(text);

         // render text, presumably in a box
            
        /*
        Text storyText = Instantiate(textPrefab) as Text;
        storyText.text = text;
        storyText.transform.SetParent(canvas.transform, false);

        */
    }

    void CreateChoiceView(string text, UnityEngine.Events.UnityAction callback)
    {

        InkConsole.instance.AddButton("> "+text, callback);

        // render buttons and assign callback

        /*
        Button choice = Instantiate(buttonPrefab) as Button;
        choice.transform.SetParent(canvas.transform, false);

        Text choiceText = choice.GetComponentInChildren<Text>();
        choiceText.text = text;

        HorizontalLayoutGroup layoutGroup = choice.GetComponent<HorizontalLayoutGroup>();
        layoutGroup.childForceExpandHeight = false;

        choice.onClick.AddListener(callback);
        */

    }

}
