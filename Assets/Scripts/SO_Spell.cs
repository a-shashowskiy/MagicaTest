using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MagicaTest
{
    [System.Serializable]
    public struct Spell
    {
        public int id;
        public string Name; // �������� ����������
        public float Damage; // ���� �� ����������
        public float ManaCost; // ���� �� ���������� ���������� � ������������ �� ������� �� ����������� ��� ��� ����� ���������
        public float SpeedFlying; // �������� ������ ����������
        public float Cooldown; // ����� ����������� ����������
        public bool playerArchived; // �������� �� �� ��� ���������� ��� �������� ������� � ����� ���� ������������
        public GameObject SpellPrefab; // ������ ���������� ��� ������

        // ��������� ����������. ���� ������������ ScriptableObject, ��� ��������� ������ � �������� � 
        // �������� �� �� ��� �� ����� ������ ������, ����� ������������ ��� ����� ��������� ������� ���������� � ��������
        public void GetSpell()
        {
            playerArchived = true;
        }
    }

    [CreateAssetMenu(fileName = "SO_Spell", menuName = "Spell", order = 1)]
    class SO_Spell : ScriptableObject
    {
        public List<Spell> Spells; 

        //NOTE: �� ����� ������ ����������, �� ��� ������� ��������
        // ���� � ScriptableObject ��������� ��������� �������, �� ����� ������������ ��� ������ ��� ��������� ��� ������
        // ���� ������� ������� ������� ���������� � �������� ������ � �� �������������

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
