using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Audio;

public class Movimiento : MonoBehaviour
{

    public Camera cam;

    public NavMeshAgent agent;
    public float attackRange;
    public List<AudioClip> SwordSoundsAir;


    Animator animator;
    bool pegarAlLLegar = false;
    AudioSource audioSource;
    



    void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        //agent.updateRotation = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("a"))
        {
            animator.SetTrigger("Saluda");
        }

        if (Input.GetMouseButton(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                agent.SetDestination(hit.point);
                pegarAlLLegar = false;
            }
        }
        if (Input.GetMouseButton(1))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (Vector3.Distance(transform.position, hit.point) < attackRange)
                {
                    audioSource.clip = PickRandomSound(SwordSoundsAir);
                    audioSource.Play();
                    
                    animator.SetTrigger("ataque");
                    agent.destination = transform.position;
                    pegarAlLLegar = false;
                }
                else
                {
                    agent.SetDestination(hit.point);
                    pegarAlLLegar = true;
                }
            }
        }
        if (Vector3.Distance(transform.position, agent.destination) > 0.2)
        {
            animator.SetBool("caminando", true);
        }
        else
        {
            animator.SetBool("caminando", false);
            if (agent.updatePosition && pegarAlLLegar)
            {
                audioSource.clip = PickRandomSound(SwordSoundsAir);
                audioSource.Play();
                animator.SetTrigger("ataque");
                pegarAlLLegar = false;
            }
        }
    }

    AudioClip PickRandomSound(List<AudioClip> audioList)
    {
        return audioList[Random.Range(0, audioList.Count)];
    }
}
