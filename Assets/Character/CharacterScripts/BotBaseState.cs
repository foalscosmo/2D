using Character.CharacterScriptable;

namespace Character.CharacterScripts
{
    public abstract class BotBaseState
    {
        protected BotStateMachine ctx;
        protected BotMovement botMovement;
        protected BotInput botInput;
        protected BotDetectionStats botDetectionStats;
        protected BotStats botStats;
        protected BotComponents botComponents;
        protected BotAnimatorController botAnimatorController;
        
        protected BotBaseState(BotStateMachine currentContext,BotMovement botMovement, BotInput botInput,
            BotDetectionStats botDetectionStats, BotStats botStats, BotComponents botComponents, BotAnimatorController botAnimatorController)
        {
            ctx = currentContext;
            this.botMovement = botMovement;
            this.botInput = botInput;
            this.botDetectionStats = botDetectionStats;
            this.botStats = botStats;
            this.botComponents = botComponents;
            this.botAnimatorController = botAnimatorController;
        }

        // Abstract methods for entering, updating, fixing, and exiting states
        public abstract void EnterState();
        public abstract void UpdateState();
        public abstract void FixedUpdate();
        public abstract void ExitState();
    }
}