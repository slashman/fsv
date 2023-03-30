using System;
using System.Collections.Generic;

public static class GameEvents {
	private static GameEvent[] events = new GameEvent[] {
		new GameEvent() {
			id = "finca",
			options = new GameEventOption[] {
				new GameEventOption() {
					action = () => {
						GameUI.i.EventsDialog.Hide();
						GameUI.i.ShowTransfer("finca");
					}
				}
			}
		},
		new GameEvent() {
			id = "uramita",
			options = new GameEventOption[] {
				new GameEventOption() {
					action = () => {
						GameUI.i.EventsDialog.Hide();
						GameUI.i.ShowTransfer("uramita");
					}
				}
			}
		},
		new GameEvent() {
			id = "dabeiba",
			options = new GameEventOption[] {
				new GameEventOption() {
					action = () => {
						GameUI.i.EventsDialog.Hide();
						GameUI.i.ShowTransfer("dabeiba");
					}
				}
			}
		},
		new GameEvent() { id = "chulavitas",
			options = new GameEventOption[] {
				new GameEventOption() {
					action = () => {
						GameUI.i.ShowEvent(GameEvents.Get("notAChula"));
					}
				},
				new GameEventOption() {
					action = () => {
						GameUI.i.ShowGameOver(Loc.Localize("chulavitas.2.outcome"));
					}
				}
			}
		},
		new GameEvent() { id = "cachiporros",
			options = new GameEventOption[] {
				new GameEventOption() {
					action = () => {
						GameUI.i.ShowEvent(GameEvents.Get("notAChula"));
					}
				},
				new GameEventOption() {
					action = () => {
						GameUI.i.ShowGameOver(Loc.Localize("cachiporros.2.outcome"));
					}
				}
			}
		},
			new GameEvent() {
				id = "notAChula",
				options = new GameEventOption[] {
					new GameEventOption() {
						action = () => {
							if (Expedition.i.GetHumans().Count == 1) {
								GameUI.i.ShowEvent(GameEvents.Get("notAChula3"));
								Expedition.i.GetHumans()[0].TakeDamage(UnityEngine.Random.Range(10, 30));
							} else if (UnityEngine.Random.Range(0, 100) < 20) {
								GameUI.i.ShowEvent(GameEvents.Get("notAChula2"));
							} else {
								FamilyMember rando = Expedition.i.RandomHuman();
								Expedition.i.Die(rando);
								GameUI.i.ShowPersonEvent(GameEvents.Get("notAChula1"), rando.memberName);
							}
						}
					}
				}
			},
				new GameEvent() {
					id = "notAChula1",
					options = new GameEventOption[] {
						new GameEventOption() { }
					}
				},
				new GameEvent() {
					id = "notAChula2",
					options = new GameEventOption[] {
						new GameEventOption() { }
					}
				},
				new GameEvent() {
					id = "notAChula3",
					options = new GameEventOption[] {
						new GameEventOption() { }
					}
				},
				new GameEvent() {
					id = "countryHouse",
					options = new GameEventOption[] {
						new GameEventOption () {
							action = () => {
								if (UnityEngine.Random.Range(0, 100) < 50) {
									GameUI.i.ShowEvent(GameEvents.Get("countryHouse_1"));
								} else {
									GameUI.i.ShowEvent(GameEvents.Get("countryHouse_2"));
									Expedition.i.Heal();
									Expedition.i.Fill();
									GameUI.i.UpdateStatus();
								}
							}
						},
						new GameEventOption () {
							action = () => {
								if (UnityEngine.Random.Range(0, 100) < 50) {
									GameUI.i.ShowEvent(GameEvents.Get("countryHouse_1"));
								} else {
									GameUI.i.ShowEvent(GameEvents.Get("countryHouse_3"));
									InventoryItem food = Expedition.i.inventory.Find(i => i.itemType == ItemType.FOOD);
									food.quantity = food.quantity + UnityEngine.Random.Range(20, 40);
									GameUI.i.UpdateStatus();
								}
							}
						}
					}
				},
					new GameEvent() {
						id = "countryHouse_1",
						options = new GameEventOption[] {
							new GameEventOption () { }
						}
					},
					new GameEvent() {
						id = "countryHouse_2",
						options = new GameEventOption[] {
							new GameEventOption () { }
						}
					},
					new GameEvent() {
						id = "countryHouse_3",
						options = new GameEventOption[] {
							new GameEventOption () { }
						}
					},

		new GameEvent() { id = "bandits", options = new GameEventOption[] {
			new GameEventOption() { action = () => {
				if (!Expedition.i.HasValuables()) {
					GameUI.i.ShowEvent(GameEvents.Get("bandits_5"));
				} else {
					Expedition.i.LoseValuables();
					GameUI.i.ShowEvent(GameEvents.Get("bandits_1"));
				}
			}},
			new GameEventOption() { action = () => {
				if (!Expedition.i.HasAdults()) {
					if (Expedition.i.HasValuables()) {
						Expedition.i.LoseValuables();
						GameUI.i.ShowEvent(GameEvents.Get("bandits_4"));
					} else {
						GameUI.i.ShowEvent(GameEvents.Get("bandits_5"));
					}
					return;
				}
				FamilyMember fighter = Expedition.i.RandomAdult();
				if (fighter.Wins()) {
					GameUI.i.ShowPersonEvent(GameEvents.Get("bandits_2"), fighter.memberName);
				} else {
					Expedition.i.LoseValuables();
					GameUI.i.ShowEvent(GameEvents.Get("bandits_3"));
				}
			}}
		}},
			new GameEvent() { id = "bandits_1",
			options = new GameEventOption[] {
				new GameEventOption() { },
			}},
			new GameEvent() { id = "bandits_2",
			options = new GameEventOption[] {
				new GameEventOption() { },
			}},
			new GameEvent() { id = "bandits_3",
			options = new GameEventOption[] {
				new GameEventOption() { },
			}},
			new GameEvent() { id = "bandits_4",
			options = new GameEventOption[] {
				new GameEventOption() { },
			}},
			new GameEvent() { id = "bandits_5",
			options = new GameEventOption[] {
				new GameEventOption() { },
			}},

		new GameEvent() { id = "casaquemada", options = new GameEventOption[] {
			new GameEventOption() { }
		}},
		new GameEvent() {
			id = "cart",
			options = new GameEventOption[] {
				new GameEventOption() { action = () => {
					InventoryItem food = Expedition.i.inventory.Find(i => i.itemType == ItemType.FOOD);
					food.quantity = food.quantity + 20;
					GameUI.i.UpdateStatus();
					GameUI.i.EventsDialog.Hide();
					Expedition.i.CheckDeath();
					World.i.ResumeTime();
				} },
				new GameEventOption() { action = () => {
					InventoryItem food = Expedition.i.inventory.Find(i => i.itemType == ItemType.FOOD);
					food.quantity = food.quantity + 10;
					GameUI.i.UpdateStatus();
					GameUI.i.EventsDialog.Hide();
					Expedition.i.CheckDeath();
					World.i.ResumeTime();
				} }
			}
		},
		new GameEvent() {
			id = "rioMelcocho",
			options = new GameEventOption[] {
				new GameEventOption() { }
			}
		},
		new GameEvent() {
			id = "finca2",
			options = new GameEventOption[] {
				new GameEventOption() { }
			}
		},
		new GameEvent() { id = "house", options = new GameEventOption[] {
			new GameEventOption() { 
				action = () => {
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
			},
			new GameEventOption() { },
		}},
		new GameEvent() { id = "house_food", options = new GameEventOption[] {
			new GameEventOption() { }
		}},
		new GameEvent() { id = "house_flee", options = new GameEventOption[] {
			new GameEventOption() { }
		}},
		new GameEvent() { id = "hunger", options = new GameEventOption[] {
			new GameEventOption() { }
		}}
	};

	public static GameEvent Get(string id) {
		return Array.Find(events, e => e.id == id);
	}

	public static void OptionSelected (GameEvent currentEvent, GameEventOption option) {
		if (option.action != null) {
			option.action();
			return;
		}
		GameUI.i.EventsDialog.Hide();
		Expedition.i.CheckDeath();
		World.i.ResumeTime();
	}
}