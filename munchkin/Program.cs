using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace munchkin
{
    internal static class Program
    { 
        enum role
        {
            analyst,developer,tester
        }
        enum domitary
        {
            du, pushkina,kp
        }
        enum slot
        {
            head,body,shoes,not_required,hand
        }
        enum table_state
        {
            battle,first_player_move,second_player_move,punishment,waiting_for_action
        }
        abstract class Card
        {
            public int id;
            public string name;
        }

        struct monster_bonus
        {
            public int role_value;
            public role bonus_role;
            public int domitary_value;
            public domitary bonus_domitary;
        }
        class item : Card
        {
            public int power;
            public bool big_item;
            public slot slot;
            public Munchkin equipped;
            public item(int id, string name, int power, bool big_item, slot slot, Munchkin equipped)
            {
                this.id = id;
                this.name = name;
                this.power = power;
                this.big_item = big_item;
                this.slot = slot;
                this.equipped = equipped;
            }

            public void Drop(slot slot,List<Card> droppedcards,Munchkin player)
            {
                player.items[slot] = null;

            }
        }
        class Munchkin
        {
            public int id;
            public string name;
            public int level;
            public Dictionary<slot,item> items;
            public bool teamlead;
            public role role1;
            public role role2;
            public bool double_domitary;
            public domitary domitary1;
            public domitary domitary2;
            
        }
        class Monster:Card
        {
            public int level;
            public monster_bonus bonus;
            public Monster (int id, string name, int level,monster_bonus bonus)
            {

            }
        }
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new enter());
        }
    }
}
