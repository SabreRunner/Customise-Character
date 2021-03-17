namespace Customisation
{
    using TMPro;
    using UnityEngine;
    using UnityEngine.UI;

    public class IconData
    {
        public Sprite iconSprite = default;
        public int requiredLevel = 0;
        public int price = 0;
        public bool purchased = false;
    }

    [CreateAssetMenu(menuName = "Customisation/Game Event", order = 10)]
    public class CosmeticData : ScriptableObject
    {
        public IconData[] icons;
    }

    public class IconController : BaseMonoBehaviour
    {
        [SerializeField] private Image backgroundImage;
        [SerializeField] private Image iconImage;
        [SerializeField] private Image lockImage;
        [SerializeField] private Image coinsImage;
        [SerializeField] private TextMeshProUGUI coinsTMP;
        [SerializeField] private Image levelImage;
        [SerializeField] private TextMeshProUGUI levelTMP;
        [SerializeField] private Color defaultColor;
        [SerializeField] private Color selectedColor;

        public void IconPressed()
        {

        }

        public void Initialise(IconData data, int userLevel, bool selected)
        {
            this.backgroundImage.color = selected ? this.selectedColor : this.defaultColor;
            this.iconImage.sprite = data.iconSprite;

            if (data.purchased)
            { return; }

            if (userLevel < data.requiredLevel)
            {
                this.lockImage.gameObject.SetActive(true);
                this.levelImage.gameObject.SetActive(true);
                this.levelTMP.text = "Level " + data.requiredLevel;
                this.coinsImage.gameObject.SetActive(false);
            }
            else
            {
                this.lockImage.gameObject.SetActive(false);
                this.levelImage.gameObject.SetActive(false);
                this.coinsImage.gameObject.SetActive(true);
                this.coinsTMP.text = data.price.ToString();
            }
        }
    }
}
