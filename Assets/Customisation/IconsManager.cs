namespace Customisation
{
    using System.Collections.Generic;
    using PushForward.EventSystem;
    using UnityEngine;

    public class IconsManager : BaseMonoBehaviour
    {
        [SerializeField] private CosmeticData cosmeticData;
        [SerializeField] private GameObject iconContainerPrefab;
        [SerializeField] private GameEventInt selectOutfit;
        [SerializeField] private GameEventInt selectEyes;
        [SerializeField] private GameEventInt selectMouth;

        private readonly List<IconController> iconControllers = new List<IconController>();
        private int selectedOutfit, selectedEyes, selectedMouth;

        private void BuyItem(StoreCategory category, int index)
        {
            CategoryData categoryData = this.cosmeticData.categories.FindFirst(data => data.category == category);

            IconData iconData = categoryData.icons[index];

            if (!iconData.purchased && iconData.requiredLevel <= GameManager.UserLevel && iconData.price <= GameManager.UserCoins)
            {
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
                controller.Initialise(localIndex, categoryData.icons[index], GameManager.UserLevel,
                                      itemSelected, categoryEvent, () => this.BuyItem(category, localIndex));
            }
        }

        private void Start()
        {
        }
    }
}
