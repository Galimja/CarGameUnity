using JoostenProductions;
using UnityEngine;

namespace Game.InputLogic
{
    internal class InputKeyboard : BaseInputView
    {
        [SerializeField] private float _speedMultipler = 0.3f;


        private void Start() =>
            UpdateManager.SubscribeToUpdate(Move);

        private void OnDestroy() =>
            UpdateManager.UnsubscribeFromUpdate(Move);


        private void Move()
        {
            float moveValue = Speed * Time.deltaTime * _speedMultipler;


            if (Input.GetKey(KeyCode.RightArrow))
            {
                OnRightMove(moveValue);
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                OnLeftMove(moveValue);
            }
        }

    }
}