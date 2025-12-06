import React from "react";
import { useNavigate } from "react-router-dom";
import { useItemInventory } from "../../hooks/useItemInventory";
import ItemInventoryList from "../../components/itemInventory/ItemInventoryList";
import type { ItemInventory } from "../../types/itemInventory";
import { ROUTES } from "../../constants/routes";

const ItemInventoryListPage: React.FC = () => {
    const { itemInventory, deleteItemInventory, reloadItemInventorys,searchItemInventories } = useItemInventory();
    const navigate = useNavigate();

    return (
        <ItemInventoryList
            iteminventory={itemInventory}
            onDelete={deleteItemInventory}
            onEdit={(id) => navigate(ROUTES.ITEM_INVENTORY_EDIT.replace(":id", String(id)))}
            onReload={reloadItemInventorys} 
            onUpdate={function (_iteminventory: ItemInventory): void {
                throw new Error("Function not implemented.");
            } } 
            onSearch={searchItemInventories}   />
    );
};

export default ItemInventoryListPage;