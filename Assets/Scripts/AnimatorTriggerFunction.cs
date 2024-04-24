using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MagicaTest
{
    public class AnimatorTriggerFunction : MonoBehaviour
    { 
        [SerializeField] private Transform _spellSpawnPoint;
        [SerializeField] private Spell _curentSpellData;

        [SerializeField] ParticleSystem _lightSpellEffect;

        MoveController _moveController;
        void Start()
        {
            _moveController = GetComponentInParent<MoveController>();
        }
        public void SetSpellToCast(Spell spell)
        {
            _curentSpellData = spell;
        }
        public void SpellCastingInAniamtion()
        {
            // Вызывается в анимации
            // Создаем объект заклинания
            if (_curentSpellData.Name != "LightBulb")
            {
                GameObject spell = Instantiate(_curentSpellData.SpellPrefab, _spellSpawnPoint.position, _moveController.transform.rotation);
                // Инициализируем объект заклинания
                spell.GetComponent<SpellCasted>().Init(_curentSpellData);
            }
            else
            {
                StartCoroutine(LightSpellEffect());
            }
        }

        IEnumerator LightSpellEffect()
        {
            _lightSpellEffect.gameObject.SetActive(true);
            yield return new WaitForSeconds(120);
            _lightSpellEffect.gameObject.SetActive(false);
        }
    }
}
