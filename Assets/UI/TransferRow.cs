using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TransferRow: MonoBehaviour {
	public TMP_Text textfield;

	private bool sellMode;
	private InventoryItem item;
	private List<InventoryItem> targetInventory;
	private TradeDialog parent;
	public TMP_Text buttonLabel;
	private bool freeTransfer;

	public void SetTargetInventory (TradeDialog parent, InventoryItem item, bool selling, bool freeTransfer, List<InventoryItem> targetInventory) {
		this.parent = parent;
		this.sellMode = selling;
		this.item = item;
		this.targetInventory = targetInventory;
		buttonLabel.text = freeTransfer ? "Transfer" : selling ? "Sell" : "Buy";
		this.freeTransfer = freeTransfer;
	}

	public void Transfer () {
		if (this.item.quantity <= 0) {
			return;
		}
		bool added = false;
		if (this.sellMode) {
			// Check can carry
		} else if (!freeTransfer) {
			if (Expedition.i.money < this.item.GetPrice()) {
				return;
			}
			Expedition.i.money -= this.item.GetPrice();
		}
			
		foreach (InventoryItem inventoryItem in targetInventory) {
			if (inventoryItem.itemType == item.itemType) {
				inventoryItem.quantity++;
				added = true;
			}
		}
		if (!added) {
			targetInventory.Add(new InventoryItem() { itemType = item.itemType, quantity = 1 });
		}
		this.item.quantity--;
		if (this.sellMode && !freeTransfer) {
			Expedition.i.money += this.item.GetPrice();
		}
		parent.UpdateInventories();
	}
}