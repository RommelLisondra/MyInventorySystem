
import { createAsyncThunk } from "@reduxjs/toolkit";
import * as documentReferenceService from "../../services/documentReferenceService";
import type { DocumentReference } from "../../types/documentReference";
import type { PagedResponse } from "../../types/pagedResponse";

// Fetch all DocumentReferences
export const fetchDocumentReferences = createAsyncThunk(
  "documentReference/fetchAll",
  async (_, thunkAPI) => {
    try {
      return await documentReferenceService.getDocumentReferences();
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

// Fetch all DocumentReferences By page
export const fetchDocumentReferencesPaged = createAsyncThunk<PagedResponse<DocumentReference[]>, // return type
  { pageNumber: number; pageSize: number }, // parameters
  { rejectValue: string } // rejection type
>(
  "documentReference/fetchPaged",
  async ({ pageNumber, pageSize }, thunkAPI) => {
    try {
      return await documentReferenceService.getDocumentReferencesPaged(pageNumber, pageSize);
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

// Fetch single DocumentReference
export const fetchDocumentReferenceById = createAsyncThunk(
  "documentReference/fetchById",
  async (id: number, thunkAPI) => {
    try {
      return await documentReferenceService.getDocumentReferenceById(id);
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

// Add new DocumentReference
export const addDocumentReference = createAsyncThunk(
  "documentReference/add",
  async (DocumentReference: DocumentReference, thunkAPI) => {
    try {
      return await documentReferenceService.createDocumentReference(DocumentReference);
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

export const updateDocumentReferenceById = createAsyncThunk<DocumentReference, DocumentReference>(
  "documentReference/update",
  async (DocumentReference, thunkAPI) => {
    try {
      await documentReferenceService.updateDocumentReference(DocumentReference);
      // Return updated DocumentReference so reducer gets correct payload type
      return DocumentReference;
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

// Delete DocumentReference
export const deleteDocumentReferenceById = createAsyncThunk<number, number>(
  "documentReference/delete",
  async (id, thunkAPI) => {
    try {
      await documentReferenceService.deleteDocumentReference(id);
      return id;
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

// Search DocumentReferences
export const searchDocumentReferences = createAsyncThunk(
  "documentReference/search",
  async (name: string, thunkAPI) => {
    try {
      return await documentReferenceService.searchDocumentReferences(name);
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

