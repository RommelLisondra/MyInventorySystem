import React from "react";
import { useNavigate } from "react-router-dom";
import { useItemDetail } from "../../hooks/useItemDetail";
import ItemDetailList from "../../components/itemDetail/ItemDetailList";
import type { ItemDetail } from "../../types/itemDetail";
import { ROUTES } from "../../constants/routes";

const ItemDetailListPage: React.FC = () => {
    const { itemDetails, deleteItemDetail, reloadItemDetails,searchItemDetail } = useItemDetail();
    const navigate = useNavigate();

    return (
        <ItemDetailList
            itemDatails={itemDetails}
            onDelete={deleteItemDetail}
            onEdit={(id) => navigate(ROUTES.ITEM_DETAIL_EDIT.replace(":id", String(id)))}
            onReload={reloadItemDetails} 
            onUpdate={function (_itemDetail: ItemDetail): void {
                throw new Error("Function not implemented.");
            } } 
            onSearch={searchItemDetail}   />
    );
};

export default ItemDetailListPage;