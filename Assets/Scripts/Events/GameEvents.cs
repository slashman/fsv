using System;
using System.Collections.Generic;

public static class GameEvents {
	private static GameEvent[] events = new GameEvent[] {
		new GameEvent() { id = "militia", prompt = "Un grupo armado te detiene, su lider pregunta:\n“Qué tenemos aquí? Reconozco chulavitas con solo verlos!“", options = new GameEventOption[] {
			new GameEventOption() { description = "No somos chulavitas! Por favor déjame pasar con mi familia!" }
		}},
		new GameEvent() { id = "casaquemada", prompt = "La casa de los Zapata... seguro ya paso por aquí esa terrible gente“", options = new GameEventOption[] {
			new GameEventOption() { description = "No miren pa' allá mijitos, sigamos andando" }
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
		new GameEvent() { id = "house", prompt = "Tu familia se acerca a una casa, parece estar abandonada. Qué quieres hacer?", options = new GameEventOption[] {
			new GameEventOption() { description = "Asaltar la casa" },
			new GameEventOption() { description = "Seguir por el camino" },
		}}
	};

	public static GameEvent Get(string id) {
		return Array.Find(events, e => e.id == id);
	}

	public static void OptionSelected (GameEvent currentEevent, GameEventOption option) {
		GameUI.i.EventsDialog.Hide();
		World.i.ResumeTime();
	}
}