import React from "react";
import { useNavigate } from "react-router-dom";
import { useClassification } from "../../hooks/useClassification";
import ClassificationList from "../../components/classification/ClassificationList";
import type { Classification } from "../../types/classification";
import { ROUTES } from "../../constants/routes";

const ClassificationListPage: React.FC = () => {
    const { classification, deleteClassification, reloadClassifications,searchClassification } = useClassification();
    const navigate = useNavigate();

    return (
        <ClassificationList
            classification={classification}
            onDelete={deleteClassification}
            onEdit={(id) => navigate(ROUTES.CLASSIFICATION_EDIT.replace(":id", String(id)))}
            onReload={reloadClassifications} 
            onUpdate={function (_classification: Classification): void {
                throw new Error("Function not implemented.");
            } } 
            onSearch={searchClassification}   />
    );
};

export default ClassificationListPage;