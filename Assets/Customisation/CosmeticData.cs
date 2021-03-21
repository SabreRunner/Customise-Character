namespace Customisation
{
    using System;
    using UnityEngine;

    [Serializable] public enum StoreCategory { Eyes, Mouths, Outfits }

    [Serializable]
    public class IconData
    {
        public Sprite iconSprite = default;
        public int requiredLevel = 0;
        public int price = 0;
        public bool purchased = false;
    }

    [Serializable]
    public class CategoryData
    {
        public StoreCategory category;
        public IconData[] icons;
    }

    [CreateAssetMenu(menuName = "Customisation/Cosmetic Data", order = 10)]
    public class CosmeticData : ScriptableObject
    {
        public CategoryData[] categories;
        
        public CategoryData GetOutfits => this.categories.FindFirst(data => data.category == StoreCategory.Outfits);
        public CategoryData GetEyes => this.categories.FindFirst(data => data.category == StoreCategory.Eyes);
        public CategoryData GetMouths => this.categories.FindFirst(data => data.category == StoreCategory.Mouths);
    }
}