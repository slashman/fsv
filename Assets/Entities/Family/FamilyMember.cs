using UnityEngine;

public class FamilyMember: MonoBehaviour {
	public int HP;
	public int Hunger;
	private int MaxHP;
	public int BaseCarryCapacity;
	public StatusBox statusBox;
	public bool isHuman;
	public bool isAdult;
	public string memberName;

	void Start () {
		statusBox.StatusText.text = "";
		statusBox.AssignFamilyMember(this);
		MaxHP = this.HP;
		Hunger = 0;
		UpdateBox();
	}

	public int GetCarryCapacity () {
		// if is sick reduce
		return BaseCarryCapacity;
	}

	public void TakeDamage(int damage) {
		HP -= damage;
		if (HP <= 0) {
			HP = 0;
			Expedition.i.Die(this);
		}
		UpdateBox();
	}

	private string GetHungerDescription () {
		switch (Hunger) {
			case 0:
				return "";
			case 1:
				return "Hungry";
			case 2:
				return "Starving";
		}
		return "";
	}

	public void UpdateBox () {
		statusBox.HPText.text = this.HP.ToString();
		statusBox.HungerText.text = GetHungerDescription();
		if (Hunger > 1) {
			statusBox.HungerText.color = new Color32(254,0,0,255);
		} else {
			statusBox.HungerText.color = new Color32(0,254,254,255);
		}
	}

	public void Heal () {
		HP += UnityEngine.Random.Range (5, 10);
		if (HP > MaxHP) {
			HP = MaxHP;
		}
	}

	public bool Wins () {
		return UnityEngine.Random.Range (0, 10) > 5;
	}

	public void ConsumeFood () {
		InventoryItem foodItem = Expedition.i.inventory.Find(i => i.itemType == ItemType.FOOD);
		if (foodItem.quantity <= 0) {
			return;
		}
		if (Hunger == 0) {
			return;
		}
		Hunger --;
		foodItem.quantity--;
		if (Hunger < 0) {
			Hunger = 0;
		}
		GameUI.i.UpdateStatus();
		UpdateBox();
	}

	public void HungerUpdate () {
		Hunger++;
		if (Hunger > 2) {
			Hunger = 2;
			TakeDamage(UnityEngine.Random.Range(1, 2));
		}
		GameUI.i.UpdateStatus();
		UpdateBox();
		Expedition.i.CheckDeath();
	}

}