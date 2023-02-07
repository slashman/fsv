using System.Collections.Generic;
using UnityEngine;

public class Expedition: MonoBehaviour {
	public static Expedition i;
	public List<FamilyMember> members;
	public List<InventoryItem> inventory;
	public float Progress;
	public int money;

	void Awake () {
		Expedition.i = this;
		Expedition.i.inventory = new List<InventoryItem>() {
			new InventoryItem() { itemType = ItemType.FOOD, quantity = 10 },
			new InventoryItem() { itemType = ItemType.MACHETE, quantity = 1 },
			/*new InventoryItem() { itemType = ItemType.BIBLE, quantity = 1 },
			new InventoryItem() { itemType = ItemType.JEWELS, quantity = 3 },
			new InventoryItem() { itemType = ItemType.PAINTING, quantity = 1 },
			new InventoryItem() { itemType = ItemType.RUANA, quantity = 3 },
			new InventoryItem() { itemType = ItemType.SHOES, quantity = 2 },*/
		};
	}

	public float GetBurden() {
		float acum = 0;
		foreach (InventoryItem item in inventory) {
			if (item.itemType == ItemType.FOOD) {
				acum += (float) item.quantity / 10.0f;
			} else {
				acum += item.quantity;
			}
		}
		return acum;
	}

	public int GetCarryCapacity() {
		int acum = 0;
		foreach (FamilyMember familyMember in members) {
			acum += familyMember.GetCarryCapacity();
		}
		return acum;
	}

	public void ConsumeFood () {
		InventoryItem foodItem = inventory.Find(i => i.itemType == ItemType.FOOD);
		for (int i = 0; i < members.Count; i++) {
			FamilyMember member = members[i];
			foodItem.quantity--;
			if (foodItem.quantity <= 0) {
				foodItem.quantity = 0;
				member.TakeDamage(UnityEngine.Random.Range(1, 2));
			}
		}
		GameUI.i.UpdateStatus();
		Expedition.i.CheckDeath();
	}

	public void Die (FamilyMember member) {
		Destroy(member.statusBox.gameObject);
		Destroy(member.gameObject);
		members.Remove(member);
		if (GetBurden() > GetCarryCapacity()) {
			// TODO: Inform lost items
			// Food is lost last
			foreach (InventoryItem inventoryItem in inventory) {
				if (inventoryItem.itemType == ItemType.FOOD) {
					continue;
				}
				while (inventoryItem.quantity > 0) {
					inventoryItem.quantity--;
					if (GetBurden() <= GetCarryCapacity()) {
						break;
					}
				}
			}
			if (GetBurden() > GetCarryCapacity()) {
				// Reduce food to min
				GetFoodItem().quantity = (int) UnityEngine.Mathf.Floor((float)GetCarryCapacity()  * 10.0f);
			}
			this.inventory = inventory.FindAll(i => i.quantity > 0 || i.itemType == ItemType.FOOD);
		}
	}

	public void CheckDeath () {
		bool foundHuman = false;
		for (int i = 0; i < members.Count; i++) {
			FamilyMember member = members[i];
			if (member.isHuman) {
				foundHuman = true;
				break;
			}
		}
		if (!foundHuman) {
			World.i.StopTime();
			GameUI.i.ShowGameOver("You didn't make it to the city\n\nBetter luck next life.");
		}
	}

	public bool HasAdults () {
		return members.Find(m => m.isHuman && m.isAdult) != null;
	}

	public FamilyMember RandomAdult () {
		List<FamilyMember> adults = members.FindAll(m => m.isHuman && m.isAdult);
		return adults[UnityEngine.Random.Range(0, adults.Count)];
	}

	public List<FamilyMember> GetHumans () {
		return members.FindAll(m => m.isHuman);
	}

	public FamilyMember RandomHuman () {
		List<FamilyMember> humans = members.FindAll(m => m.isHuman);
		return humans[UnityEngine.Random.Range(0, humans.Count)];
	}

	public void TimePassed () {
		Progress += Time.deltaTime * 1.0f;
		if (Progress >= 280) {
			World.i.StopTime();
			GameUI.i.ShowVictory();
		} else {
			GameUI.i.UpdateProgress();
		}
	}

	public InventoryItem GetFoodItem () {
		return this.inventory.Find(i => i.itemType == ItemType.FOOD);
	}

	public int GetTotalFood () {
		return GetFoodItem().quantity; // This item should never be removed
	}

	public void Heal () {
		foreach (FamilyMember familyMember in members) {
			familyMember.Heal();
		}
	}

	public bool HasValuables () {
		return this.inventory.Find(i => i.quantity > 0) != null;
	}

	public void LoseValuables () {
		for (int i = 0; i < 5; i++) {
			InventoryItem randomInventoryItem = this.inventory[UnityEngine.Random.Range(0, this.inventory.Count)];
			if (randomInventoryItem.quantity > 0)
				randomInventoryItem.quantity --;
		}
	}
}