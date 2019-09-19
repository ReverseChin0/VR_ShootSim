using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Valve.VR.InteractionSystem
{
    public class Cargador : Interactable
    {
        public GameObject gbCargador;
        Interactable ActualInteraction = null;
        MeshRenderer gbCargadorRenderer = null;
        Coroutine Check = null;
        bool isLoad = false;

        private void Awake()
        {
            gbCargadorRenderer = gbCargador.GetComponent<MeshRenderer>();
        }

        public void OnPickup(Interactable _interaction)
        {
            gbCargadorRenderer.enabled = true;
            ActualInteraction = _interaction;
        }

        public void OnDrop(Interactable _interaction) {
            gbCargadorRenderer.enabled = false;
            ActualInteraction = null;

            if(Check != null){
                StopCoroutine(Check);

                transform.position = gbCargador.transform.position;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.GetInstanceID() != gbCargador.GetInstanceID())
                return;

            Quitar();
        }

        private void Quitar()
        {
            gbCargadorRenderer.enabled = false;

            ActualInteraction.attachedToHand.DetachObject(this.gameObject,false);

            GetComponent<Collider>().enabled = false;

            Rigidbody rg = GetComponent<Rigidbody>();

            rg.useGravity = false;
            rg.isKinematic = true;

            transform.parent = gbCargador.transform;
            transform.localRotation = Quaternion.identity;

            float interactionDistance = (ActualInteraction.gameObject.transform.position - gbCargador.transform.position).sqrMagnitude;
            Check = StartCoroutine(InteraccionCargador(interactionDistance));
        }

        IEnumerator InteraccionCargador(float _interactionDistance)
        {
            while(_interactionDistance *4 > (ActualInteraction.gameObject.transform.position - gbCargador.transform.position).sqrMagnitude)
            {
                float step = 0.2f * Time.deltaTime;

                transform.position = Vector3.MoveTowards(transform.position,gbCargador.transform.position,step);
                yield return null;
            }

            Regresar();
        }

        void Regresar()
        {
            gbCargadorRenderer.enabled = true;

            transform.parent = null;
            transform.position = ActualInteraction.gameObject.transform.position;

            GetComponent<Collider>().enabled = true;

            Rigidbody rg = GetComponent<Rigidbody>();

            rg.useGravity = true;
            rg.isKinematic = false;

            ActualInteraction.attachedToHand.AttachObject(this.gameObject,GrabTypes.Scripted,Hand.AttachmentFlags.ParentToHand);
        }


    }
}
