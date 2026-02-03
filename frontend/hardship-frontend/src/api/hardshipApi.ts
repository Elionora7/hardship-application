import { api } from "./axios";
import type {
  HardshipApplication,
  CreateHardshipRequest,
} from "../types/hardship";

export const getHardshipApplications = async (): Promise<HardshipApplication[]> => {
  const res = await api.get("/hardship");
  return res.data;
};

export const createHardshipApplication = async (
  payload: CreateHardshipRequest
): Promise<HardshipApplication> => {
  const res = await api.post("/hardship", payload);
  return res.data;
};

export const updateHardshipApplication = async (
  id: number,
  payload: CreateHardshipRequest
): Promise<HardshipApplication> => {
  const body = { ...payload, id };
  const res = await api.put(`/hardship/${id}`, body);
  return res.data;
};


export const deleteHardshipApplication = async (id: number): Promise<void> => {
  await api.delete(`/hardship/${id}`);
};
