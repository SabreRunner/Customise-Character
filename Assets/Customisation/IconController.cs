namespace Customisation
{
    using System;
    using PushForward.EventSystem;
    using TMPro;
    using UnityEngine;
    using UnityEngine.UI;

    public class IconController : BaseMonoBehaviour
    {
        #region inspector fields
        [SerializeField] private Image backgroundImage;
        [SerializeField] private Image iconImage;
        [SerializeField] private Image lockImage;
        [SerializeField] private Image coinsImage;
        [SerializeField] private TextMeshProUGUI coinsTMP;
        [SerializeField] private Image levelImage;
        [SerializeField] private TextMeshProUGUI levelTMP;
        [SerializeField] private Color defaultColor;
        [SerializeField] private Color selectedColor;
        #endregion // inspector fields

        private int dataIndex;
        private Action iconPressedAction;

        public void IconPressed()
        {
            this.iconPressedAction?.Invoke();
        }

        public void Initialise(int index, IconData data, int userLevel, bool selected, Action iconPressed)
        {
            this.dataIndex = index;
            this.backgroundImage.color = selected ? this.selectedColor : this.defaultColor;
            this.iconImage.sprite = data.iconSprite;

            if (data.purchased)
            {
                this.lockImage.gameObject.SetActive(false);
                this.levelImage.gameObject.SetActive(false);
                this.coinsImage.gameObject.SetActive(false);
                this.iconPressedAction = iconPressed;
                return;
            }

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
                this.iconPressedAction = iconPressed;
            }
        }
    }
}
