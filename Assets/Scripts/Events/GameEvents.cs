using System;
using System.Collections.Generic;

public static class GameEvents {
	private static GameEvent[] events = new GameEvent[] {
		new GameEvent() { id = "militia", prompt = "Un grupo armado te detiene, su lider pregunta:\n“Qué tenemos aquí? Reconozco chulavitas con solo verlos!“", options = new GameEventOption[] {
			new GameEventOption() { description = "No somos chulavitas! Por favor déjame pasar con mi familia!" }
		}},
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