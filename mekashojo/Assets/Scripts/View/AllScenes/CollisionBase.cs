using System;
using UnityEngine;

namespace View
{
    public class CollisionBase : MonoBehaviour
    {
        protected Action<Collider2D> playOnEnter;
        protected Action<Collider2D> playWhileIn;
        protected Action<Collider2D> playOnExit;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            playOnEnter?.Invoke(collision);
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            playWhileIn?.Invoke(collision);
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            playWhileIn?.Invoke(collision);
        }
    }
}
