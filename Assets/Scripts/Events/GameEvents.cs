using System;
using System.Collections.Generic;

public static class GameEvents {
	private static GameEvent[] events = new GameEvent[] {
		new GameEvent() {
			id = "finca",
			prompt = "You approach your home, built with the sweat of your brow - years of hard work. Now you have no choice but leave it behind.",
			options = new GameEventOption[] {
				new GameEventOption() {
					description = "Let's hurry and pack whatever we can fit in Rosita, and on our own backs",
					action = () => {
						GameUI.i.EventsDialog.Hide();
						GameUI.i.ShowTransfer("finca");
					}
				}
			}
		},
		new GameEvent() {
			id = "uramita",
			prompt = "You approach Uramita, a small town where your family used to live many years ago.",
			options = new GameEventOption[] {
				new GameEventOption() {
					description = "We can try to trade some of what we have here.",
					action = () => {
						GameUI.i.EventsDialog.Hide();
						GameUI.i.ShowTransfer("uramita");
					}
				}
			}
		},
		new GameEvent() {
			id = "dabeiba",
			prompt = "You approach Dabeiba, a trade center of the region growing increasingly dangerous.",
			options = new GameEventOption[] {
				new GameEventOption() {
					description = "We can try to trade some of what we have here.",
					action = () => {
						GameUI.i.EventsDialog.Hide();
						GameUI.i.ShowTransfer("dabeiba");
					}
				}
			}
		},
		new GameEvent() { id = "chulavitas", prompt = "A group of people armed with old rifles and machetes approaches." +
			"\n\nOne man moves forward from the group while several men point to you with their guns." +
			"\n\n“You look like a god-damned cachiporro, I know one when I see it!“", options = new GameEventOption[] {
				new GameEventOption() {
					description = "I’m not a cachiporro, I swear! Please let me pass!",
					action = () => {
						GameUI.i.ShowEvent(GameEvents.Get("notAChula"));
					}
				},
				new GameEventOption() {
					description = "Death to all the chusma, you will destroy our country... You shall burn in hell!",
					action = () => {
						GameUI.i.ShowGameOver("The man spits on your face before hitting you with the butt of his rifle. Everything blacks out.\n\n"+
							"When you wake up, the first thing you see is a crowd. You don’t see your family.\n\n"+
							"You try to move, but your hands are tied. A man grabs your hair and lifts your face. You see many people. Where are you? Your head hurts too much to think and the man speaks.\n\n"+
							"Look at the faces of the traitors! Know that even if the traitors are children or old people we don’t care! We will pacify this country, with fire and blood if needed!\n\n"+
							"Everything blacks out again. This time forever.");
					}
				}
			}
		},
		new GameEvent() { id = "cachiporros", prompt = "A group of people armed with old rifles and machetes approaches." +
			"\n\nOne man moves forward from the group while several men point to you with their guns." +
			"\n\n“You look like a god-damned chulavita, I know one when I see it!“", options = new GameEventOption[] {
				new GameEventOption() {
					description = "I’m not a chulavita, I swear! Please let me pass!",
					action = () => {
						GameUI.i.ShowEvent(GameEvents.Get("notAChula"));
					}
				},
				new GameEventOption() {
					description = "Death to all the state-backed oppressor land-grabber murderers!",
					action = () => {
						GameUI.i.ShowGameOver("The man spits on your face before hitting you with the butt of his rifle. Everything blacks out.\n\n"+
							"When you wake up, the first thing you see is a crowd. You don’t see your family.\n\n"+
							"You try to move, but your hands are tied. A man grabs your hair and lifts your face. You see many people. Where are you? Your head hurts too much to think and the man speaks.\n\n"+
							"Look at the faces of the traitors! Know that even if the traitors are children or old people we don’t care! We will pacify this country, with fire and blood if needed!\n\n"+
							"Everything blacks out again. This time forever.");
					}
				}
			}
		},
			new GameEvent() {
				id = "notAChula",
				prompt = "The man turns his back on you, and without looking gives a signal to his men after spitting to the ground.\n\n- Kill them.",
				options = new GameEventOption[] {
					new GameEventOption() {
						description = "Have Mercy!",
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
					prompt = "You see a chance to escape and shout for your family to run to the woods.\n\n"+
						"Chaos ensues as bullets fly. After a while you stop running and see who’s with you.\n\n"+
						"Sadly XXX is missing.",
					options = new GameEventOption[] {
						new GameEventOption() { description = "God rest his soul." }
					}
				},
				new GameEvent() {
					id = "notAChula2",
					prompt = "You see a chance to escape and shout for your family to run to the woods.\n\n"+
						"Chaos ensues as bullets fly. After a while you stop running and see who’s with you.\n\n"+
						"God is blessed, you are all able to regroup and continue.",
					options = new GameEventOption[] {
						new GameEventOption() { description = "Thank you, holy virgin." }
					}
				},
				new GameEvent() {
					id = "notAChula3",
					prompt = "You see a chance to escape and run to the woods.\n\n"+
						"After a while you stop running and notice you were hit. ",
					options = new GameEventOption[] {
						new GameEventOption() { description = "Argh!" }
					}
				},
				new GameEvent() {
					id = "countryHouse",
					prompt = "You see a farm with fat animals and crops ready for harvest. It doesn’t look like there is anyone home.",
					options = new GameEventOption[] {
						new GameEventOption () {
							description = "Call out for help.",
							action = () => {
								if (UnityEngine.Random.Range(0, 100) < 50) {
									GameUI.i.ShowEvent(GameEvents.Get("countryHouse_1"));
								} else {
									GameUI.i.ShowEvent(GameEvents.Get("countryHouse_2"));
									Expedition.i.Heal();
									GameUI.i.UpdateStatus();
								}
							}
						},
						new GameEventOption () {
							description = "Steal some crops and chickens.",
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
						prompt = "“No, no, no! Get out! THIEVES! REBELS! KILLERS!” An old man runs out from the house with a machete, driving your family out of the property.",
						options = new GameEventOption[] {
							new GameEventOption () {
								description = "Run away!"
							}
						}
					},
					new GameEvent() {
						id = "countryHouse_2",
						prompt = "An old man comes out of the house and approaches: Oh no… Please don’t tell me you also come from San Vicente. Come inside and share a meal with me. Helping is the least I could do.",
						options = new GameEventOption[] {
							new GameEventOption () {
								description = "May God bless you and your family."
							}
						}
					},
					new GameEvent() {
						id = "countryHouse_3",
						prompt = "Hearing no answer, you go ahead and steal some crops and chickens.",
						options = new GameEventOption[] {
							new GameEventOption () {
								description = "God, forgive us."
							}
						}
					},

		new GameEvent() { id = "bandits", prompt = "Some bandoleros approach with machetes and wooden clubs. “Give us your valuables! Or die!”", options = new GameEventOption[] {
			new GameEventOption() { description = "Drop some items and run.", action = () => {
				if (!Expedition.i.HasValuables()) {
					GameUI.i.ShowEvent(GameEvents.Get("bandits_5"));
				} else {
					Expedition.i.LoseValuables();
					GameUI.i.ShowEvent(GameEvents.Get("bandits_1"));
				}
			}},
			new GameEventOption() { description = "Fight!", action = () => {
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
			new GameEvent() { id = "bandits_1", prompt = "The bandoleros run to grab the items. They are also famished, maybe ran out of their homes too. Using this opportunity, you and your family flee.",
			options = new GameEventOption[] {
				new GameEventOption() { description = "Ok"},
			}},
			new GameEvent() { id = "bandits_2", prompt = "XXX will have to heal minor wounds, but you’ll retain your family’s positions.",
			options = new GameEventOption[] {
				new GameEventOption() { description = "Ok" },
			}},
			new GameEvent() { id = "bandits_3", prompt = "You black out. Your family seems scared but safe. The bandoleros were scared and in a hurry so they only stole some items.",
			options = new GameEventOption[] {
				new GameEventOption() { description = "Ok" },
			}},
			new GameEvent() { id = "bandits_4", prompt = "With no one able to fight, the bandoleros laugh and take some items before leaving.",
			options = new GameEventOption[] {
				new GameEventOption() { description = "Curse them." },
			}},
			new GameEvent() { id = "bandits_5", prompt = "Unfortunately for the bandoleros, they have more than you have.",
			options = new GameEventOption[] {
				new GameEventOption() { description = "Sorry." },
			}},

		new GameEvent() { id = "casaquemada", prompt = "The house of our friends, the Zapata...\n\nBurnt down by those who threatened us out of our home.", options = new GameEventOption[] {
			new GameEventOption() { description = "It's better not to look, kids, and just keep walking..." }
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
			id = "finca2",
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
			new GameEventOption() { 
				description = "Let's take the risk, we don't have a choice.",
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
		if (option.action != null) {
			option.action();
			return;
		}
		GameUI.i.EventsDialog.Hide();
		World.i.ResumeTime();
	}
}