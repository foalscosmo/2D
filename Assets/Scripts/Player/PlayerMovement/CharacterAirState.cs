using CombatInterface;
using UnityEngine;

namespace Player.PlayerMovement
{
    // Represents the character state when in the air
    public class CharacterAirState : CharacterBaseState, IAttacker
    {
        // Constructor to initialize the air state
        public CharacterAirState(CharacterStateMachine currentContext, CharacterStateFactory characterStateFactory, CharacterStats characterStats, DetectionStats detectionStats, CharacterDetection characterDetection, CharacterComponents components, CharacterInput input, CharacterDash characterDash, CharacterMovement characterMovement, CharacterJump characterJump, CharacterAnimation characterAnimation) : base(currentContext, characterStateFactory, characterStats, detectionStats, characterDetection, components, input, characterDash, characterMovement, characterJump, characterAnimation)
        {
        }

        // Called when entering the air state
        public override void EnterState()
        {
            // No specific actions needed when entering the air state
        }

        // Called on every frame update while in the air state
        public override void UpdateState()
        { 
            // Perform animations and actions relevant to the air state
            JumpAnimations();
            AirDashAnimation();
            FallAnimation();
            if (characterInput.Attack.action.triggered) PerformAttack();
        }

        // Called on every physics update while in the air state
        public override void FixedUpdate()
        {
            // Move horizontally, jump, apply gravity, handle jump end, and dash if applicable
            characterMovement.MoveHorizontally(stats.MoveVector.x * stats.MoveSpeed);
            characterJump.Jump();
            characterJump.ApplyGravity();
            characterJump.HandleJumpEnd();
            if (stats.IsDashing)  characterDash.Dash();
            stats.DashCooldownTimer = Mathf.Max(0f, stats.DashCooldownTimer - Time.deltaTime);
        }

        // Called when exiting the air state
        public override void ExitState()
        { 
            // Reset air-related state variables
            stats.NumberOfJumps = 0;
            stats.AirAttackCounter = 0;
        }

        // Plays jump animations based on the number of jumps performed
        private void JumpAnimations()
        {
            if (!characterInput.JumpAction.action.triggered || !stats.IsJump) return;
            switch (stats.NumberOfJumps)
            {
                case 1:
                    characterAnimation.ChangeAnimationState(characterAnimation.jumpAnim, 0.1f);
                    break;
                case 2:
                    characterAnimation.ChangeAnimationState(characterAnimation.doubleJumpAnim, 0.1f);
                    break;
            }
        }

        // Plays fall animation when not dashing or attacking
        private void FallAnimation()
        {
            if (!stats.IsDashing && !stats.IsAttacking)
            {
                characterAnimation.ChangeAnimationState(characterAnimation.jumpAnim, 0.1f);
            }
        }

        // Plays air dash animation when dash action is triggered and conditions are met
        private void AirDashAnimation()
        {
            if (characterInput.DashAction.action.triggered && stats.DashCooldownTimer <= 0 && characterComponents.Rb.velocity.x != 0)
            {
                stats.IsDashing = true;
                characterAnimation.ChangeAnimationState(characterAnimation.dashAnim,0.1f);
                stats.DashCooldownTimer = 0.5f;
            }
        }
        
        // Performs attack action in the air
        public void PerformAttack()
        {
            if(stats.AirAttackCounter > 1) return;
            characterComponents.Rb.velocity = new Vector2(0, 5); 
            characterAnimation.ChangeAnimationState(characterAnimation.kickAnim, 0f);
            stats.IsAttacking = true;
            stats.AirAttackCounter++;
        }
        
    }
}