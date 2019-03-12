using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonsOfDoom.Core
{
    public class Player : Character
    {
        // Properties
        public int X { get; set; }
        public int Y { get; set; }
        public IEquipable equipedWeapon { get; set; } = null;
        public List<List<IBackpackAble>> backpack = new List<List<IBackpackAble>>();

        //Konstruktor
        public Player(int inputHealth, int x, int y) : base(inputHealth, 20)
        {
            X = x;
            Y = y;
        }
        
        //Metoder
        /// <summary>
        /// XML är tufft
        /// </summary>
        /// <param name="itemToAdd"></param>
        public void addItemToBackpack(IBackpackAble itemToAdd)
        {
            bool alreadyInList = false;
            foreach (List<IBackpackAble> itemList in backpack)
            {
                if (itemToAdd.Name == itemList[0].Name)
                {
                    alreadyInList = true;

                    itemList.Add(itemToAdd);
                }
            }

            if (!alreadyInList)
            {
                List<IBackpackAble> newList = new List<IBackpackAble>();

                newList.Add(itemToAdd);

                backpack.Add(newList);
            }
        }
        public void useItemInBackpack(int indexItem)
        {
            if (backpack.Count >= indexItem)
            {
                if (backpack[indexItem - 1].Count > 0)
                {
                    backpack[indexItem - 1][backpack[indexItem - 1].Count - 1].UseItem(this);
                    backpack[indexItem - 1].RemoveAt(backpack[indexItem - 1].Count - 1);
                    
                    if (backpack[indexItem - 1].Count == 0)
                    {
                        backpack.RemoveAt(indexItem - 1);
                    }
                }
            }
        }
        public void equipItem(IEquipable itemToEquip)
        {
            if (equipedWeapon != null)
            {
                equipedWeapon.ApplyEffect(this, false);
                addItemToBackpack(equipedWeapon);
            }
            if (itemToEquip is Weapon)
            {
                equipedWeapon = itemToEquip;
                itemToEquip.ApplyEffect(this, true);
                
            }
        }
    }
}
