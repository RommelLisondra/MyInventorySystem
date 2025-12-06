import { USER_ACCOUNT_API } from "../constants/api";

export const login = async (username: string, password: string) => {
  const res = await fetch(`${USER_ACCOUNT_API}/login`, {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify({ username, password }),
  });
  
  if (!res.ok) throw new Error("Login failed");

  const data = await res.json();
  if (data.token) localStorage.setItem("jwtToken", data.token);

  return data;
};

export const logout = () => localStorage.removeItem("jwtToken");
export const getToken = () => localStorage.getItem("jwtToken");

export const authFetch = async (url: string, options: RequestInit = {}) => {
  const token = getToken();
  const headers = new Headers(options.headers);
  headers.set("Content-Type", "application/json");
  if (token) headers.set("Authorization", `Bearer ${token}`);

  const res = await fetch(url, { ...options, headers });
  if (!res.ok) throw new Error(await res.text() || "Request failed");
  return res.json();
};