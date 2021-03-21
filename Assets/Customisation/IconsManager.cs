namespace Customisation
{
    using System;
    using System.Collections.Generic;
    using PushForward.EventSystem;
    using UnityEngine;
    using Object = UnityEngine.Object;

    public class IconsManager : BaseMonoBehaviour
    {
        [SerializeField] private CosmeticData cosmeticData;
        [SerializeField] private GameObject iconContainerPrefab;
        [SerializeField] private GameEventInt selectOutfit;
        [SerializeField] private GameEventInt selectEyes;
        [SerializeField] private GameEventInt selectMouth;

        private readonly List<IconController> iconControllers = new List<IconController>();
        private int selectedOutfit = -1, selectedEyes = -1, selectedMouth = -1;

        private void SelectInCategory(StoreCategory category, int index)
        {
            switch (category)
            {
                case StoreCategory.Eyes: this.selectedEyes = index; break;
                case StoreCategory.Mouths: this.selectedMouth = index; break;
                case StoreCategory.Outfits: this.selectedOutfit = index; break;
                default: throw new ArgumentOutOfRangeException(nameof(category), category, null);
            }
        }

        private void BuyItem(StoreCategory category, int index)
        {
            CategoryData categoryData = this.cosmeticData.categories.FindFirst(data => data.category == category);

            IconData iconData = categoryData.icons[index];

            if (!iconData.purchased && iconData.requiredLevel <= GameManager.UserLevel && iconData.price <= GameManager.UserCoins)
            {
                GameManager.UserCoins -= iconData.price;
                iconData.purchased = true;
                this.InitialiseCategory(category);
            }
        }

        private void InitialiseCategory(StoreCategory category)
        {
            CategoryData categoryData = this.cosmeticData.categories.FindFirst(data => data.category == category);

            this.DestroyAllChildren();
            this.iconControllers.Clear();

            int selectedInCategory = category == StoreCategory.Eyes ? this.selectedEyes
                                         : category == StoreCategory.Mouths ? this.selectedMouth : this.selectedOutfit;
            GameEventInt categoryEvent = category == StoreCategory.Eyes ? this.selectEyes
                                         : category == StoreCategory.Mouths ? this.selectMouth : this.selectOutfit;

            for (int index = 0; index < categoryData.icons.Length; index++)
            {
                IconController controller = Object.Instantiate(this.iconContainerPrefab, this.transform)
                                                  .GetComponent<IconController>();
                int localIndex = index;
                bool itemSelected = index == selectedInCategory;
                IconData iconData = categoryData.icons[localIndex];

                void IconAction()
                {
                    if (itemSelected) { return; }
                    if (iconData.purchased)
                    {
                        this.SelectInCategory(category, localIndex);
                        categoryEvent.Raise(localIndex);
                        this.InitialiseCategory(category);
                        return;
                    }
                    this.BuyItem(category, localIndex);
                }

                controller.Initialise(localIndex, iconData, GameManager.UserLevel, itemSelected, IconAction);
            }
        }

        public void InitialiseOutfits() => this.InitialiseCategory(StoreCategory.Outfits);
        public void InitialiseEyes() => this.InitialiseCategory(StoreCategory.Eyes);
        public void InitialiseMouths() => this.InitialiseCategory(StoreCategory.Mouths);

        private void Start()
        {
            this.InitialiseCategory(StoreCategory.Outfits);
        }
    }
}
