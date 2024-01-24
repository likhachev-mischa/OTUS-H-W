using DI;

namespace Game
{
    public class GameFinisherController: IGamePostConstructListener, IGameFinishListener
    {
        private GameManager gameManager;
        private Character character;
        
        [Inject]
        private void Construct(Character character, GameManager gameManager)
        {
            this.character = character;
            this.gameManager = gameManager;
        }

        public void OnPostConstruct()
        {
            character.Death.Subscribe(gameManager.FinishGame);
        }
        
       public void OnFinish()
       {
           character.Death.Unsubscribe(gameManager.FinishGame);
       }
    }
}