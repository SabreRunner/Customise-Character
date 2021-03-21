namespace Customisation
{
    using PushForward.EventSystem;
    using UnityEngine;

    public class GameManager : SingletonBehaviour<GameManager>
    {
        private static UserData userData;

        [SerializeField] private GameEventString levelUpdateEvent;
        [SerializeField] private GameEventString coinsUpdateEvent;

        public static int UserLevel
        {
            get => GameManager.userData.Level;
            set
            {
                GameManager.userData.Level = value;
                GameManager.Instance.levelUpdateEvent.Raise(value.ToString());
            }
        }
        public static int UserCoins
        {
            get => GameManager.userData.Coins;
            set
            {
                GameManager.userData.Coins = value;
                GameManager.Instance.coinsUpdateEvent.Raise(value.ToString());
            }
        }

        private void Start()
        {
            GameManager.userData = UserDataGetter.GetUserData();
        }
    }
}
