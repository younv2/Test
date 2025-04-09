using UnityEngine;
using UnityEngine.UI;
public class Star : MonoBehaviour
{
    public Image starImage;

    void Awake()
    {
        
    }

    void Start()
    {
        starImage = GetComponent<Image>();   
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Setting(int starCount)
    {
        if (starImage == null)
        {
            starImage = GetComponent<Image>();
        }
        if(starImage == null)
        {
            Debug.LogError("SpriteRenderer component is missing on the Star object!");
            return;
        }
        Debug.Log("[Star] starImage: ");
        starImage.sprite = Resources.Load<Sprite>($"Images/Star/Star{starCount}");
        Debug.Log("[Star] starImage: " + starImage.sprite.name);
    }
}
