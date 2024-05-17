using System.Collections.Generic;
using Character.CharacterScriptable;

namespace Character.CharacterScripts
{
    public enum States
    {
        GroundedState // Character is on the ground
    }
    public class BotStateFactory
    {
        private readonly Dictionary<States, BotBaseState> container = new();

        public BotStateFactory(BotStateMachine currentContext, BotDetectionStats botDetectionStats, BotStats botStats,
            BotComponents botComponents, BotMovement botMovement, BotInput botInput, BotAnimatorController botAnimatorController)
        {
            container[States.GroundedState] =
                new BotGroundedState(currentContext,botMovement, botInput, botDetectionStats, botStats, botComponents, botAnimatorController);
        }
        
        public BotBaseState Grounded()
        {
            return container[States.GroundedState];
        }
    }
}