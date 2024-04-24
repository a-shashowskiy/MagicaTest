using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MagicaTest
{
    public class SpellCasted : MonoBehaviour
    {
        [SerializeField] Spell _spellData; // Данные о заклинании
        [SerializeField] GameObject _destroySFX;  
        bool inited = false;
        public void Init(Spell spellData)
        {
            _spellData = spellData;
            inited = true;
        }

        public void Update() 
        {
            if (inited)
            {
                transform.Translate(Vector3.forward * Time.deltaTime * 10);
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            // Spawn SFX
            if (collision != null)
            {
                Instantiate(_destroySFX, transform.position, Quaternion.identity);
                Destroy(gameObject);
            } 
            if (collision.gameObject.tag == "Enemy")
            {
                collision.collider.GetComponent<EnemyAi>().TakeDamage(_spellData.Damage);
                Instantiate(_destroySFX, transform.position, Quaternion.identity);
                Destroy(gameObject);
            } 
        }
    }
}
