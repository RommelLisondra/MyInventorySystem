import React from "react";
import { useNavigate } from "react-router-dom";
import { useItem } from "../../hooks/useItem";
import ItemList from "../../components/item/ItemList";
import type { Item } from "../../types/item";
import { ROUTES } from "../../constants/routes";

const ItemListPage: React.FC = () => {
    const { items, deleteItem, reloadItems,searchItem } = useItem();
    const navigate = useNavigate();

    return (
        <ItemList
            items={items}
            onDelete={deleteItem}
            onEdit={(id) => navigate(ROUTES.ITEM_EDIT.replace(":id", String(id)))}
            onReload={reloadItems} 
            onUpdate={function (_item: Item): void {
                throw new Error("Function not implemented.");
            } } 
            onSearch={searchItem}   />
    );
};

export default ItemListPage;