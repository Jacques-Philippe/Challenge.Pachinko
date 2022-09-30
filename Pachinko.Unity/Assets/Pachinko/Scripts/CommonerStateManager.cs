using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Assets.Pachinko.Scripts
{
    public enum COMMONER_STATE
    {
        WHISTLING,
        GRUMBLING
    }

    public interface ICommonerStateTransitionEvent { }

    public class HitOnHeadTransitionEvent : ICommonerStateTransitionEvent
    {

    }
    public class TransitionEventData
    {
        public COMMONER_STATE from;
        public COMMONER_STATE to;
        public Type transition;
    }


    public class CommonerStateManager : MonoBehaviour
    {
        [SerializeField]
        private AudioClip GrumbleSound;

        [SerializeField]
        private AudioClip WhistlingSound;

        [SerializeField]
        [Range(1, 5)]
        private float Speed;

        private Vector2 currentDirection = Vector2.left;

        private AudioSource source;


        COMMONER_STATE currentState = COMMONER_STATE.WHISTLING;

        private List<TransitionEventData> allTransitions = new List<TransitionEventData>()
        {
            new TransitionEventData(){ from=COMMONER_STATE.WHISTLING, to=COMMONER_STATE.GRUMBLING, transition=typeof(HitOnHeadTransitionEvent)}
        };

        public void HandleTransition(ICommonerStateTransitionEvent transitionEvent)
        {
            var currentTransition = allTransitions.First(t => t.from == this.currentState && transitionEvent.GetType() == t.transition);
            var nextState = currentTransition.to;
            Debug.Log($"Transitioning from {this.currentState} to {nextState} given {transitionEvent.GetType()}");

            if (transitionEvent is HitOnHeadTransitionEvent)
            {
                if (source.isPlaying)
                {
                    source.Stop();
                }
                StartCoroutine("Grumble");
            }
        }

        public void TurnAround()
        {
            var sprite = this.GetComponent<SpriteRenderer>();
            if (sprite != null) sprite.flipX = !sprite.flipX;
            this.currentDirection = -currentDirection;
        }

        private IEnumerator Grumble()
        {
            //Play the whistling sound
            source.clip = this.GrumbleSound;
            source.loop = true;
            source.Play();
            //Play the grumble sound
            yield return null;
        }

        private IEnumerator Whistle()
        {
            //Play the whistling sound
            source.clip = this.WhistlingSound;
            source.loop = true;
            source.Play();
            yield return new WaitUntil(() =>
            {
                Walk();
                return this.currentState != COMMONER_STATE.WHISTLING;
            });
            
            yield return null;
        }

        private void Walk()
        {
            Vector2 movement = this.currentDirection.normalized * this.Speed * Time.deltaTime;
            this.transform.position += new Vector3(movement.x, movement.y, 0.0f);
        }

        private void Start()
        {
            this.source = this.GetComponent<AudioSource>();
            StartCoroutine("Whistle");
        }

    }
}
