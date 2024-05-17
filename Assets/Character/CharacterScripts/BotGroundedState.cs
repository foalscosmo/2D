using Character.CharacterScriptable;

namespace Character.CharacterScripts
{
    public class BotGroundedState : BotBaseState
    {
        public override void EnterState()
        {
        }

        public override void UpdateState()
        {
            MoveAnim();
        }

        public override void FixedUpdate()
        {
            botMovement.MoveHorizontally(botStats.MoveSpeed);
        }

        public override void ExitState()
        {
        }
        
        private void MoveAnim()
        {
            switch (botComponents.Rb.velocity.x)
            {
                case > 0:
                case < 0:
                    botStats.IsRunning = true;
                    switch (botStats.IsRotating)
                    {
                        case false:
                            botAnimatorController.ChangeAnimationState(botAnimatorController.botRunAnim, 0f);
                            break;
                        case true:
                            botAnimatorController.ChangeAnimationState(botAnimatorController.botRunningTurn, 0f);
                            break;
                    }

                    break;
                case 0:
                    switch (botStats.IsRunning)
                    {
                        case true:
                            botAnimatorController.ChangeAnimationState(botAnimatorController.botRunToStopAnim, 0.1f);
                            break;
                        case false:
                            botAnimatorController.ChangeAnimationState(botAnimatorController.botIdleAnim, 0.1f);
                            break;
                    }

                    break;
            }
        }

        public BotGroundedState(BotStateMachine currentContext, BotMovement botMovement, BotInput botInput, BotDetectionStats botDetectionStats, BotStats botStats, BotComponents botComponents, BotAnimatorController botAnimatorController) : base(currentContext, botMovement, botInput, botDetectionStats, botStats, botComponents, botAnimatorController)
        {
        }
    }
}