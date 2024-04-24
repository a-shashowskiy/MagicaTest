using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MagicaTest
{
    public class PLayer : MonoBehaviour
    {
        public float health = 100;
        [Range(0, 1)]
        public float protection = 0.5f;
        float atackRate = 0.2f;
        public Spell currentSpell;
        public Action<float> OnChangeHealth;

        MoveController moveController;
        SpellController spellController;
        [SerializeField] Rigidbody rb;
        [SerializeField] Animator anim;
        // Start is called before the first frame update
        void Start()
        {
            moveController = GetComponent<MoveController>();
            spellController = GetComponent<SpellController>();
            moveController.Init(rb, anim);
            spellController.AddSpell("FireBall"); //Basic spell
            currentSpell = spellController.GetCurentSpel();

            OnChangeHealth += UIController.instance.OnHelthChange;
            OnChangeHealth.Invoke(health);
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.X) /*&& !moveController.GetMovmentStatus()*/)
            {
               atackRate = currentSpell.Cooldown;
               StartCoroutine(Attack());
            }
            if (Input.GetKeyDown(KeyCode.Q) && !moveController.GetMovmentStatus())
            {
                spellController.SwichSpell(false);
                currentSpell = spellController.GetCurentSpel();
            }
            if (Input.GetKeyDown(KeyCode.E) && !moveController.GetMovmentStatus())
            {
                spellController.SwichSpell(true);
                currentSpell = spellController.GetCurentSpel();
            }
        }

        IEnumerator Attack()
        {
            rb.velocity = Vector3.zero;
            anim.SetBool("Attack", true);
            spellController.CastSpell(currentSpell); 
            yield return new WaitForSeconds(atackRate);
            anim.SetBool("Attack", false);
            StopCoroutine(Attack());
        }

        public void TakeDamage(int damage)
        {
            health = health - damage*protection;
            OnChangeHealth.Invoke(health);
            if(health <= 0)
            {
                Destroy(gameObject);
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.tag == "Enemy")
            {
                TakeDamage(collision.gameObject.GetComponent<EnemyAi>().damage);
            }
        }
    } 
}
