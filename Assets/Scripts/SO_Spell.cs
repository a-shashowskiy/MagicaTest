using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MagicaTest
{
    [System.Serializable]
    public struct Spell
    {
        public int id;
        public string Name; // Название заклинания
        public float Damage; // Урон от заклинания
        public float ManaCost; // Мана на сотворение заклинания в документации не указана но предположим что она может появиться
        public float SpeedFlying; // Скорость полета заклинания
        public float Cooldown; // Время перезарядки заклинания
        public bool playerArchived; // Проверка на то что заклинание уже получено игроком и может быть использовано
        public GameObject SpellPrefab; // Префаб заклинания для спавна

        // Получение заклинания. Изза особенностей ScriptableObject, это перепишет данные в рантайме и 
        // сохранит их но это не самый лучший способ, лучше использовать для этого отдельную систему сохранения и загрузки
        public void GetSpell()
        {
            playerArchived = true;
        }
    }

    [CreateAssetMenu(fileName = "SO_Spell", menuName = "Spell", order = 1)]
    class SO_Spell : ScriptableObject
    {
        public List<Spell> Spells; 

        //NOTE: Не самая лучшая реализация, но для примера подойдет
        // Хоть и ScriptableObject позволяет изменения рунтайм, но лучше использовать его только как контейнер для данных
        // Луче создать отельно систему сохранения и загрузки данных и их инициализацию

        public void AddSpell(string spellName)
        {
            for(int i=0; i < Spells.Count; i++)
            {
                if (Spells[i].Name == spellName)
                {
                    Spells[i].GetSpell(); 
                }
            }
        }
    }
}
