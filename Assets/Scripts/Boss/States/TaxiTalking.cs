﻿using UnityEngine;

public class TaxiTalking : StateMachineBehaviour
{
   public bool textAdvanced;
   public bool done;

   // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
   override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
   {
      Toolbox.Instance.BossScript.bossStateMachine.ResetTrigger("Base_toTaxi");
      Toolbox.Instance.AssetAnim.GameBoard.ResetTrigger("Open");
      Toolbox.Instance.AssetAnim.CloseGameBoard();
      Toolbox.Instance.AssetAnim.CloseGameWindow();

      Toolbox.Instance.BossScript.DisplayTextBox();

      if (Toolbox.Instance.Vars.TaxiDefeated || Toolbox.Instance.Vars.PlayerDefeated)
      {
         Toolbox.Instance.BossScript._taxiStrings.AdvanceEnemyDialogueState();
      }
      Toolbox.Instance.BossScript._taxiStrings.FetchEnemyDialogue();
      if (Toolbox.Instance.Vars.introductory)
      {
         Toolbox.Instance.BossScript._taxiStrings.AdvanceEnemyDialogueState();
         Toolbox.Instance.Vars.introductory = false;
      }
      Toolbox.Instance.BossScript.DisplayTextToRead();
      Toolbox.Instance.BossScript.DisplayTextAdvanceIndication();

      textAdvanced = false;
      done = false;
   }

   // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
   override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
   {
      if (textAdvanced && !done)
      {
         if (Toolbox.Instance.Vars.TaxiDefeated || Toolbox.Instance.Vars.PlayerDefeated)
         {
            Toolbox.Instance.BossScript.bossStateMachine.SetTrigger("Base_BossFightOver");
         }
         else
         {
            Toolbox.Instance.BossScript.bossStateMachine.SetTrigger("Base_toPlayer");
         }

         done = true;
      }
      else if (done)
      {
         ; //do nothing
      }
      else if ((Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.X)))
      {
         textAdvanced = true;
      }
   }

   // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
   override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
   {
      Toolbox.Instance.BossScript.CloseTextBox();
      Toolbox.Instance.BossScript.HideTextAdvanceIndication();
   }
}
