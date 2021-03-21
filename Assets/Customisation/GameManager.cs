namespace Customisation
{
    public class GameManager : BaseMonoBehaviour
    {
        private static UserData userData;

        public static int UserLevel => GameManager.userData.Level;
        public static int UserCoins => GameManager.userData.Coins;

        private void Start()
        {
            GameManager.userData = UserDataGetter.GetUserData();
        }
    }
}
