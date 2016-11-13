using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
[RequireComponent(typeof(Button))]
public class ToggleButtonIcon : MonoBehaviour {

    public Sprite onIcon;
    public Sprite offIcon;
    public bool initialState;
    private bool on;

    void Start()
    {
        // This automatically registers the event click on the button component
        GetComponent<Button>().onClick.AddListener(() => { Click(); });
        on = initialState;
        SetIcon();
    }

    public void Click()
    {
        on = !on;
        SetIcon();
    }

    private void SetIcon()
    {
        if (on)
        {
            GetComponent<Image>().sprite = onIcon;
        }
        else
        {
            GetComponent<Image>().sprite = offIcon;
        }
    }
}
