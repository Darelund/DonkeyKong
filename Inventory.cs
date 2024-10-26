using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.XAudio2;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using static DonkeyKong.Inventory;

namespace DonkeyKong
{
    public class Inventory
    {
        public List<Item> Items { get; set; }
        private Dictionary<int, Slot> Slots;
        public Inventory()
        {
            Items = new List<Item>();
            Slots = new Dictionary<int, Slot>()
            {
                { 1, new Slot(ItemType.Wearable) },
                { 2, new Slot(ItemType.Consumable) },
                { 3, new Slot(ItemType.Weapon) },
            };
        }

        public void Update(GameTime gameTime, Vector2 pos)
        {
            foreach (var slot in Slots)
            {
                switch (slot.Value.Type)
                {
                    case ItemType.Wearable:
                        slot.Value.Position = pos;
                     //   Debug.WriteLine(($"Slot pos: {slot.Value.Position}"));
                        break;
                    case ItemType.Consumable:
                        slot.Value.Position = pos;
                        break;
                    case ItemType.Weapon:
                        slot.Value.Position = pos;
                        break;
                }
              //  slot.Value.ItemSlot.Update(gameTime);
            }
            for (var i = 1; i <= Slots.Keys.Count; i++)
            {
               if(Slots.TryGetValue(i, out Slot slot))
                {
                    if(slot.ItemSlot != null)
                    {
                        slot.ItemSlot.Position = slot.Position;
                        slot.ItemSlot.Update(gameTime);
                       // Debug.WriteLine(($"Item pos: {slot.ItemSlot.Position}"));
                    }
                }

            }


            if (InputManager.WearButton())
            {
                foreach (var item in Items)
                {
                    if (item.Type == ItemType.Wearable && Slots[1].ItemSlot == null)
                    {
                        Slots[1].ItemSlot = item;
                        Slots[1].ItemSlot.Position = Slots[1].Position;
                        item.OnExpired += Slots[1].ClearSlot;
                        Debug.WriteLine($"Trying to wear: {item.Type}");
                    }
                }
            }
            if (InputManager.ConsumeButton())
            {
                foreach (var item in Items)
                {
                    if (item.Type == ItemType.Consumable && Slots[2].ItemSlot == null)
                    {
                        Slots[2].ItemSlot = item;
                        item.OnExpired += Slots[2].ClearSlot;
                    }
                    Debug.WriteLine($"Type: {item.Type}");
                }
            }
            if (InputManager.UseButton())
            {
                foreach (var item in Items)
                {
                    if (item.Type == ItemType.Weapon && Slots[3].ItemSlot == null)
                    {
                        Slots[3].ItemSlot = item;
                        item.OnExpired += Slots[2].ClearSlot;
                    }
                    Debug.WriteLine($"Type: {item.Type}");
                }
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            for (var i = 1; i <= Slots.Keys.Count; i++)
            {
                if (Slots.TryGetValue(i, out Slot slot))
                {
                    if (slot.ItemSlot != null)
                    {
                        slot.ItemSlot.Draw(spriteBatch);
                        Debug.WriteLine("Draw it");
                    }
                }
            }
        }

        public class Slot
        {
            public Item ItemSlot;
            public ItemType Type { get; }
            public Vector2 Position { get; set; }

            public Slot(ItemType type)
            {
                Type = type;
            }
            public void ClearSlot()
            {
                ItemSlot = null;
            }
        }
    }

}
