using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MagicaTest
{
    public class SpellController : MonoBehaviour
    { 
        [SerializeField] SO_Spell Spells;
        [SerializeField] AnimatorTriggerFunction _animatorTriger;
        [SerializeField] ParticleSystem _castSpellEffect;
        Spell currentSpell;
        Action<String> OnSpellChange;
        void Start()
        {
             OnSpellChange += UIController.instance.OnSpellChange;
        }
        public void AddSpell(string spell)
        {
            Spells.AddSpell(spell);
            currentSpell = Spells.Spells.Find(x => x.Name == spell); // Set new spell as current
        }
   
        public Spell GetCurentSpel()
        {
            return currentSpell;
        }

        public void SwichSpell(bool dir)
        {
            if(dir)
            {
               foreach (var spell in Spells.Spells)
                {
                   if (spell.Name == currentSpell.Name)
                    {
                       currentSpell = Spells.Spells[Spells.Spells.IndexOf(spell) + 1 <= Spells.Spells.Count-1 ? Spells.Spells.IndexOf(spell) + 1 : Spells.Spells.IndexOf(spell)];
                       break;
                   }
               }
            }
            else
            {
                foreach (var spell in Spells.Spells)
                {
                    if (spell.Name == currentSpell.Name)
                    {
                        currentSpell = Spells.Spells[Spells.Spells.IndexOf(spell) - 1 >= 0? Spells.Spells.IndexOf(spell) - 1 : Spells.Spells.IndexOf(spell)];
                        break;
                    }
                }
            }

            OnSpellChange.Invoke(currentSpell.Name);
        }

        public void CastSpell(Spell spell)
        { 
            _castSpellEffect.Play();
            _animatorTriger.SetSpellToCast(spell); 
        }
    }
}
