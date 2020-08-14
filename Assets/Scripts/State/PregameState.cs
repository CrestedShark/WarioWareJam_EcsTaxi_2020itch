﻿using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class PregameState : StateMachineBehaviour
{
   public int timeToWait = 5;
   public int miniIndex = 0;

   // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
   override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
   {
      Toolbox.Instance.CurrentState = GameState.State.Pregame;
      Toolbox.Instance.State.ResetTriggers();
      Toolbox.Instance.MiniManager.result = MinigameManager.MinigameState.None;
      Toolbox.Instance.MiniManager.minigameScene = null;
      Toolbox.Instance.Canvas.canvasElements[0].SetActive(false);
      Toolbox.Instance.State.SetTrigger(GameState.Trigger.Ready);
   }

   // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
   //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
   //{
   //}

   // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
   //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
   //{

   //}

   // OnStateMove is called right after Animator.OnAnimatorMove()
   //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
   //{
   //    // Implement code that processes and affects root motion
   //}

   // OnStateIK is called right after Animator.OnAnimatorIK()
   //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
   //{
   //    // Implement code that sets up animation IK (inverse kinematics)
   //}
}
