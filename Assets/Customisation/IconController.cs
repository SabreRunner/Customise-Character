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
        private GameEventInt selectEvent = default;
        private Action buyItemAction = default;

        public void IconPressed()
        {
            if (this.selectEvent != default)
            { this.selectEvent.Raise(this.dataIndex); }
            else if (this.buyItemAction != default)
            { this.buyItemAction.Invoke(); }
        }

        public void Initialise(int index, IconData data, int userLevel, bool selected, GameEventInt select, Action buyItem)
        {
            this.dataIndex = index;
            this.backgroundImage.color = selected ? this.selectedColor : this.defaultColor;
            this.iconImage.sprite = data.iconSprite;

            if (selected)
            { return; }

            if (data.purchased)
            {
                this.lockImage.gameObject.SetActive(false);
                this.levelImage.gameObject.SetActive(false);
                this.coinsImage.gameObject.SetActive(false);
                this.selectEvent = select;
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
                this.buyItemAction = buyItem;
            }
        }
    }
}
