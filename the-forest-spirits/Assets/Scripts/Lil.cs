/*
 * A lil' namespace with lil' libraries making it
 * easier to access various functionality
 */

using System.Linq;
using UnityEngine;

namespace Lil
{
    public static class Guy
    {
        public static bool HasItem(InventoryItem item) {
            return Player.PlayerInstance.inventory.Items.Contains(item);
        }

        public static bool HasItem(string name) {
            return Player.PlayerInstance.inventory.Items.Count(it => it.Collectable.itemName == name) > 0;
        }
        
        public static void Collect(InventoryItem item) => Player.PlayerInstance.inventory.AddItem(item);
        
        // Parents the given GameObject to the inventory 
        public static void Adopt(GameObject any) {
            any.transform.parent = Player.PlayerInstance.inventory.transform;
        }
    }

    public static class Inventory
    {
        public static bool IsOpen => Player.PlayerInstance.inventory.display.Open;
        
        public static void Show() => Player.PlayerInstance.inventory.display.Show();
        public static void Hide() => Player.PlayerInstance.inventory.display.Hide();
        public static void Toggle() => Player.PlayerInstance.inventory.display.Toggle();


        
    }

}