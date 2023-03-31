using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TransferRow: MonoBehaviour {
	public TMP_Text textfield;

	private bool toFamily;
	private InventoryItem item;
	private List<InventoryItem> targetInventory;
	private TradeDialog parent;
	public TMP_Text buttonLabel;
	private bool freeTransfer;

	public void SetTargetInventory (TradeDialog parent, InventoryItem item, bool toFamily, bool freeTransfer, List<InventoryItem> targetInventory) {
		this.parent = parent;
		this.toFamily = toFamily;
		this.item = item;
		this.targetInventory = targetInventory;
		buttonLabel.text = freeTransfer ? toFamily ? Loc.Localize("inventory.take") : Loc.Localize("inventory.leave") : toFamily ? Loc.Localize("inventory.buy") : Loc.Localize("inventory.sell");
		this.freeTransfer = freeTransfer;
	}

	public void Transfer () {
		if (this.item.quantity <= 0) {
			return;
		}
		bool added = false;
		if (this.toFamily) {
			if (!Expedition.i.CanCarry(this.item)) {
				return;
			}
		}
		if (!freeTransfer) {
			if (toFamily) {
				if (Expedition.i.money < this.item.GetPrice()) {
					return;
				}
				Expedition.i.money -= this.item.GetPrice();
			} else {
				Expedition.i.money += this.item.GetPrice();
			}
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
		parent.UpdateInventories();
	}
}