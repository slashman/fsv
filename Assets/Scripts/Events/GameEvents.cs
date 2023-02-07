using System;
using System.Collections.Generic;

public static class GameEvents {
	private static GameEvent[] events = new GameEvent[] {
		new GameEvent() { id = "militia", prompt = "A group of people armed with old rifles and machetes approaches." +
			"\nOne man moves forward from the group while several men point to you with their guns." +
            "\n“You look like a puto chulavita!“"
			, options = new GameEventOption[] {

			new GameEventOption() { description = "“I’m not a chulavita, I swear! Please let me pass with my family!“\r\n" }
		}},
		new GameEvent() { id = "casaquemada", prompt = "Zapata's family home... Burnt by those who threatened us out of our home.", options = new GameEventOption[] {
			new GameEventOption() { description = "It's better not to look, and keep walking..." }
		}},
		new GameEvent() {
			id = "stolenAnimal",
			prompt = "After a good night’s sleep, you notice XXX is missing. After spending several hours looking for it, you decide it’s time to move on.",
			options = new GameEventOption[] {
				new GameEventOption() { description = "I should have been more careful." }
			}
		},
		new GameEvent() {
			id = "cart",
			prompt = "Ahead on the road you see what appears to be a cart. It has some food inside it that seems edible. The surrounding soil is covered by blood. There are no mules or horses, and no one answer your calls.",
			options = new GameEventOption[] {
				new GameEventOption() { description = "Load Rosita with all the food. [+20 food]" },
				new GameEventOption() { description = "Leave some food behind. Someone else may need it. [+10 food]" }
			}
		},
		new GameEvent() {
			id = "rioMelcocho",
			prompt = "Years ago, when looking at the crystalline waters of this river, at its colors, you’d think there can only exist peace." +
			"\nNowadays however, you only think how easily your family will get shot if you stop here.",
			options = new GameEventOption[] {
				new GameEventOption() { description = "Don't stop now! We must go!" }
			}
		},
		new GameEvent() {
			id = "finca",
			prompt = "There was a time when you’d visit the family who lived here." +
			"\nBack then, you could trust your neighbors. You felt safe." +
			"\nNow, you’re afraid to ask for help. What if they think you are part of the Chulavitas?",
			options = new GameEventOption[] {
				new GameEventOption() { description = "People have been shot for far less..." }
			}
		},
		new GameEvent() { id = "house", prompt = "Your family aproaches what seems like an abandoned house." +
			"\nProbably another family displaced by violence." +
			"\n Should we search for food, risking to be taken as thieves and attacked?", options = new GameEventOption[] {
			new GameEventOption() { id = "steal", description = "Let's take the risk, we don't have a choice." },
			new GameEventOption() { description = "No. Let's continue down the road." },
		}},
		new GameEvent() { id = "house_food", prompt = "Luckily, we found some yuca in the kitchen.", options = new GameEventOption[] {
			new GameEventOption() { description = "Blessed be our mother the virgin." }
		}},
		new GameEvent() { id = "house_flee", prompt = "We are caught red-handed, and driven off the house by the machete.", options = new GameEventOption[] {
			new GameEventOption() { description = "Have mercy! Have mercy!" }
		}}
	};

	public static GameEvent Get(string id) {
		return Array.Find(events, e => e.id == id);
	}

	public static void OptionSelected (GameEvent currentEvent, GameEventOption option) {
		if (currentEvent.id == "house") {
			if (option.id == "steal") {
				int dice = UnityEngine.Random.Range(1, 4);
				if (dice < 3) {
					InventoryItem food = Expedition.i.inventory.Find(i => i.itemType == ItemType.FOOD);
					food.quantity = food.quantity + UnityEngine.Random.Range(5, 8);;
					GameUI.i.ShowEvent(GameEvents.Get("house_food"));
				} else {
					FamilyMember rando = Expedition.i.members[UnityEngine.Random.Range(0, Expedition.i.members.Count)];
					rando.TakeDamage(UnityEngine.Random.Range(2, 5));
					GameUI.i.ShowEvent(GameEvents.Get("house_flee"));
				}
				GameUI.i.UpdateStatus();
				return;
			}
		}
		GameUI.i.EventsDialog.Hide();
		World.i.ResumeTime();
	}
}