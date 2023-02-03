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
			case ItemType.FOOD: return 4;
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

	public string GetName () {
		switch (itemType) {
			case ItemType.FOOD: return "Yuca (Cassava)";
			case ItemType.MEDICINE: return "Medicine";
			case ItemType.BANDAGES: return "Bandages";
			case ItemType.MACHETE: return "Machete";
			case ItemType.CRUCIFIX: return "Crucifix";
			case ItemType.PAINTING: return "Sacred Family Painting";
			case ItemType.BIBLE: return "Bible";
			case ItemType.CLOCK: return "Clock";
			case ItemType.JEWELS: return "Jewels";
			case ItemType.MIRROR: return "Mirror";
			case ItemType.SHOES: return "Shoes";
			case ItemType.RUANA: return "Ruana";
			case ItemType.POTS: return "Pots";
			case ItemType.WOODEN_TOY: return "Wooden Toy";
			case ItemType.SAINT: return "Image of a Saint";
			case ItemType.MATCHES: return "Matches";
		}
		return "";
	}
}