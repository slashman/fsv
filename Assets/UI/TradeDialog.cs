using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TradeDialog: MonoBehaviour {
	public GameObject panel;
	public GameObject transferRowPrefab;
	public Transform playerRowsContainer;
	public Transform containerRowsContainer;
	public TMP_Text capacityText;
	public TMP_Text moneyText;
	public TMP_Text targetNameText;
	public AudioClip openDoorSFX;
	public AudioClip closeDoorSFX;

	private bool freeTransfer;

	private string currentID;

	public static Dictionary<string, List<InventoryItem>> INVENTORIES = new Dictionary<string, List<InventoryItem>> () {
		{
			"finca",
			new List<InventoryItem> () {
				new InventoryItem () { itemType = ItemType.BIBLE, quantity = 2 },
				new InventoryItem () { itemType = ItemType.CRUCIFIX, quantity = 1 },
				new InventoryItem () { itemType = ItemType.FOOD, quantity = 10 },
				new InventoryItem () { itemType = ItemType.MACHETE, quantity = 2 },
				new InventoryItem () { itemType = ItemType.MATCHES, quantity = 2 },
				new InventoryItem () { itemType = ItemType.MIRROR, quantity = 1 },
				new InventoryItem () { itemType = ItemType.PAINTING, quantity = 1 },
				new InventoryItem () { itemType = ItemType.POTS, quantity = 5 },
				new InventoryItem () { itemType = ItemType.RUANA, quantity = 5 },
				new InventoryItem () { itemType = ItemType.SAINT, quantity = 3 },
				new InventoryItem () { itemType = ItemType.SHOES, quantity = 4 },
				new InventoryItem () { itemType = ItemType.WOODEN_TOY, quantity = 2 },
			}
		},
		{
			"dabeiba",
			new List<InventoryItem> () {
				new InventoryItem () { itemType = ItemType.FOOD, quantity = 100 },
				new InventoryItem () { itemType = ItemType.MACHETE, quantity = 10 },
				new InventoryItem () { itemType = ItemType.RUANA, quantity = 5 },
				new InventoryItem () { itemType = ItemType.SHOES, quantity = 4 }
			}
		},
		{
			"uramita",
			new List<InventoryItem> () {
				new InventoryItem () { itemType = ItemType.FOOD, quantity = 100 },
				new InventoryItem () { itemType = ItemType.MACHETE, quantity = 10 },
				new InventoryItem () { itemType = ItemType.RUANA, quantity = 5 },
				new InventoryItem () { itemType = ItemType.SHOES, quantity = 4 }
			}
		}
	};

	private List<InventoryItem> targetInventory;

	public static string getInventoryName (string id) {
		switch (id) {
			case "finca":
				return "Finca Los Rodriguez";
			case "dabeiba":
				return "Dabeiba";
			case "uramita":
				return "Uramita";
		}
		return "";
	}

	public void Show (string targetInventoryId) {
		currentID = targetInventoryId;
		this.targetInventory = INVENTORIES[targetInventoryId];
		targetNameText.text = getInventoryName(targetInventoryId);
		freeTransfer = targetInventoryId == "finca";
		if (currentID == "finca" || currentID == "dabeiba" || currentID == "uramita") {
			GetComponent<AudioSource>().PlayOneShot(openDoorSFX);	
			World.i.PauseBGSFX();
		}
		
		UpdateInventories();
		panel.SetActive(true);
	}

	public void UpdateInventories () {
		foreach (Transform row in playerRowsContainer) {
			GameObject.Destroy(row.gameObject);
 		}
		foreach (Transform row in containerRowsContainer) {
			GameObject.Destroy(row.gameObject);
 		}
		foreach (InventoryItem item in Expedition.i.inventory) {
			TransferRow newRow = Instantiate(transferRowPrefab, playerRowsContainer).GetComponent<TransferRow>();
			newRow.textfield.text = item.quantity == 1 ? item.GetName() : item.quantity + "x " + item.GetName();
			newRow.SetTargetInventory(this, item, true, freeTransfer, targetInventory);
		}
		foreach (InventoryItem item in targetInventory) {
			TransferRow newRow = Instantiate(transferRowPrefab, containerRowsContainer).GetComponent<TransferRow>();
			newRow.textfield.text = item.quantity == 1 ? item.GetName() : item.quantity + "x " + item.GetName();
			newRow.SetTargetInventory(this, item, false, freeTransfer, Expedition.i.inventory);
		}
		capacityText.text = "Carrying " + Expedition.i.GetBurden() + "/" + Expedition.i.GetCarryCapacity();
		moneyText.text = "Money " + Expedition.i.money + " pesos";
	}

	public void Hide () {
		World.i.ResumeTime();
		panel.SetActive(false);
		GameUI.i.UpdateStatus();
		if (currentID == "finca" || currentID == "dabeiba" || currentID == "uramita") {
			GetComponent<AudioSource>().PlayOneShot(closeDoorSFX);
			World.i.UnPauseBGSFX();	
		}
		
	}
}