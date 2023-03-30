public enum ItemType {
	MEDICINE,
	BANDAGES,
	MACHETE,
	CRUCIFIX,
	BIBLE,
	CLOCK,
	JEWELS,
	MIRROR,
	SHOES,
	RUANA,
	PAINTING,
	FOOD,
	POTS,
	WOODEN_TOY,
	SAINT,
	MATCHES
}

public class InventoryItem {
	public ItemType itemType;
	public string name;
	public int quantity;

	public int GetPrice () {
		switch (itemType) {
			case ItemType.FOOD: return 1;
			case ItemType.MEDICINE: return 20;
			case ItemType.BANDAGES: return 15;
			case ItemType.MACHETE: return 20;
			case ItemType.CRUCIFIX: return 30;
			case ItemType.PAINTING: return 15;
			case ItemType.BIBLE: return 20;
			case ItemType.CLOCK: return 20;
			case ItemType.JEWELS: return 10;
			case ItemType.MIRROR: return 10;
			case ItemType.SHOES: return 10;
			case ItemType.RUANA: return 10;
			case ItemType.POTS: return 15;
			case ItemType.WOODEN_TOY: return 5;
			case ItemType.SAINT: return 10;
			case ItemType.MATCHES: return 5;
		}
		return 0;
	}

	public float GetWeight () {
		switch (itemType) {
			case ItemType.FOOD: return 0.2f;
			case ItemType.MEDICINE: return 1f;
			case ItemType.BANDAGES: return 1f;
			case ItemType.MACHETE: return 2f;
			case ItemType.CRUCIFIX: return 4f;
			case ItemType.PAINTING: return 5f;
			case ItemType.BIBLE: return 1f;
			case ItemType.CLOCK: return 1f;
			case ItemType.JEWELS: return 1f;
			case ItemType.MIRROR: return 3f;
			case ItemType.SHOES: return 1f;
			case ItemType.RUANA: return 1f;
			case ItemType.POTS: return 3f;
			case ItemType.WOODEN_TOY: return 1f;
			case ItemType.SAINT: return 1f;
			case ItemType.MATCHES: return 1f;
		}
		return 0;
	}

	public string GetName () {
		return Loc.Localize("item." + itemType);
	}
}