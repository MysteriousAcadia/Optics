using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
[RequireComponent(typeof(Button))]
public class SpriteChanger : MonoBehaviour
{
    public ButtonCombo[] Sprites;
    int currentState;
    Image currentImage;
    Button currentButton;
    Text buttonLabel;

    private void Start()
    {
        currentState = 0;
        currentImage = GetComponent<Image>();
        currentButton = GetComponent<Button>();
        currentButton.onClick.AddListener(() => ToggleSprite());
        buttonLabel = transform.GetChild(0).GetComponent<Text>();
    }

    void ToggleSprite()
    {
        currentState++;
        currentState = currentState % Sprites.Length;
        currentImage.sprite = Sprites[currentState].sprite;
        buttonLabel.text = Sprites[currentState].label;
    }
}

[System.Serializable]
public struct ButtonCombo
{
    public Sprite sprite;
    public string label;
}
