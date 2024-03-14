using System.Collections;
using CombatInterface;
using UnityEngine;

namespace Player.PlayerMovement
{
    public class CharacterGroundedState : CharacterBaseState, IAttacker
    {
        public override void EnterState()
        {
            // Resetting various flags and counters when entering the grounded state
            detectionStats.IsLedgeDetected = false;
            stats.IsJump = false;
            stats.IsAttacking = false;
            stats.NumberOfJumps = 0;
            characterComponents.Rb.gravityScale = 5;
        }

        public override void UpdateState()
        {
            // Handling movement animations, dashing, rolling, and attacks
            MoveAnim();
            Dash();
            Roll();

            if (characterInput.JumpAction.action.triggered && stats.IsJump)
                JumpAnim();

            if (characterInput.Attack.action.triggered && characterInput.MoveUp.action.ReadValue<float>() < 0.9f)
            {
                PerformAttack();
            }
            else if (characterInput.MoveUp.action.ReadValue<float>() >= 0.9f && characterInput.Attack.action.triggered)
            {
                stats.AttackCounter = 4;
                PerformAttack();
            }
        }

        public override void FixedUpdate()
        {
            // Moving character horizontally if not attacking
            if (!stats.IsAttacking) characterMovement.MoveHorizontally(stats.MoveVector.x * stats.MoveSpeed);
        }

        public override void ExitState()
        {
            // No exit action defined for grounded state
        }

        private void MoveAnim()
        {
            // Skipping animations if dashing or attacking
            if (stats.IsDashing || stats.IsAttacking) return;

            // Starting landing animation coroutine
            ctx.StartCoroutine(Land());

            // Skipping if falling
            if (stats.IsFalling) return;

            // Changing animation based on character's horizontal velocity
            switch (characterComponents.Rb.velocity.x)
            {
                case > 0:
                case < 0:
                    characterAnimation.ChangeAnimationState(characterAnimation.runAnim, 0.1f);
                    break;
                case 0:
                    characterAnimation.ChangeAnimationState(characterAnimation.idleAnim, 0.1f);
                    break;
            }
        }

        private void JumpAnim()
        {
            // Triggering jump animation
            characterAnimation.ChangeAnimationState(characterAnimation.jumpAnim, 0.1f);
        }

        public void PerformAttack()
        {
            // Skipping attack if already attacking
            if (stats.IsAttacking) return;

            // Performing different attacks based on the attack counter
            switch (stats.AttackCounter)
            {
                case 0:
                    characterComponents.Rb.velocity = Vector2.zero;
                    stats.IsAttacking = true;
                    characterAnimation.ChangeAnimationState(characterAnimation.groundAttack01, 0f);
                    stats.AttackCounter++;
                    break;
                case 1:
                    characterComponents.Rb.velocity = Vector2.zero;
                    characterAnimation.ChangeAnimationState(characterAnimation.groundAttack03, 0f);
                    stats.IsAttacking = true;
                    stats.AttackCounter++;
                    break;
                case 2:
                    characterComponents.Rb.velocity = Vector2.zero;
                    var randomValue = Random.Range(0f, 1f);
                    characterAnimation.ChangeAnimationState(
                        randomValue < 0.5f ? characterAnimation.punch01Anim : characterAnimation.kick01Anim, 0f);
                    stats.IsAttacking = true;
                    stats.AttackCounter++;
                    break;
                case 3:
                    characterComponents.Rb.velocity = Vector2.zero;
                    characterAnimation.ChangeAnimationState(characterAnimation.groundAttack04, 0f);
                    stats.IsAttacking = true;
                    stats.AttackCounter = 0;
                    break;
                case 4:
                    characterComponents.Rb.velocity = Vector2.zero;
                    characterAnimation.ChangeAnimationState(characterAnimation.groundUpAttack, 0f);
                    stats.IsAttacking = true;
                    stats.AttackCounter = 0;
                    break;
            }
        }

        private void Roll()
        {
            // Checking conditions for rolling
            if (!(characterInput.MoveDown.action.ReadValue<float>() >= 0.8f) ||
                !characterInput.DashAction.action.triggered ||
                !(stats.DashCooldownTimer <= 0) || characterComponents.Rb.velocity.x == 0) return;

            // Triggering dash and changing animation if dashing
            characterDash.Dash();
            if (stats.IsDashing) characterAnimation.ChangeAnimationState(characterAnimation.rollAnim, 0.1f);
            stats.DashCooldownTimer = 0.5f;
        }

        private void Dash()
        {
            // Updating dash cooldown timer
            stats.DashCooldownTimer = Mathf.Max(0f, stats.DashCooldownTimer - Time.deltaTime);

            // Checking conditions for dashing
            if (!(characterInput.MoveDown.action.ReadValue<float>() < 0.8f) ||
                !characterInput.DashAction.action.triggered ||
                !(stats.DashCooldownTimer <= 0) || characterComponents.Rb.velocity.x == 0) return;

            // Triggering dash and changing animation if dashing
            characterDash.Dash();
            if (stats.IsDashing) characterAnimation.ChangeAnimationState(characterAnimation.dashAnim, 0.1f);
            stats.DashCooldownTimer = 0.5f;
        }

        private IEnumerator Land()
        {
            // Skipping if not falling
            if (!stats.IsFalling) yield break;

            // Changing animation to land animation and waiting
            characterAnimation.ChangeAnimationState(characterAnimation.landAnim, 0.1f);
            yield return new WaitForSeconds(0.1f);
            stats.IsFalling = false;
        }

        // Constructor initializing the base state with necessary components
        public CharacterGroundedState(CharacterStateMachine currentContext, CharacterStateFactory characterStateFactory,
            CharacterStats characterStats, DetectionStats detectionStats, CharacterDetection characterDetection,
            CharacterComponents components, CharacterInput input, CharacterDash characterDash,
            CharacterMovement characterMovement, CharacterJump characterJump, CharacterAnimation characterAnimation) :
            base(currentContext, characterStateFactory, characterStats, detectionStats, characterDetection, components,
                input, characterDash, characterMovement, characterJump, characterAnimation)
        {
        }
    }
}
