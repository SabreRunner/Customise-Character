using PushForward.ExtensionMethods;
using UnityEngine;
using UnityEngine.UI;

namespace Customisation
{
    public class ImageSelector : BaseMonoBehaviour
    {
        [SerializeField] Image image;
        [SerializeField] Sprite[] images;

        public void SetImage(int imageNumber)
        {
            if (!imageNumber.Between(0, this.images.Length - 1))
            {
                this.Log("SetImage", "Value of " + nameof(imageNumber) + " is out bounds.");
                return;
            }

            if (this.images[imageNumber] == default)
            {
                this.Log("SetImage", "Images array does not contain a sprite at position: " + imageNumber);
                return;
            }
            this.image.sprite = this.images[imageNumber];
        }

        private void OnValidate()
        {
            if (this.image == null)
            { this.image = this.GetComponent<Image>(); }
        }
    }
}
