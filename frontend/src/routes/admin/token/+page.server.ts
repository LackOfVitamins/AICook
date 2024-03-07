import { type ServerLoad } from "@sveltejs/kit";
import { PRIVATE_API_URL } from "$env/static/private"
import type { LoginToken } from "@/types/loginToken";

export const prerender = false;

export const load: ServerLoad = async ({ locals, fetch }) => {
  if (locals.loginSession == undefined)
    return {};

  const res = await fetch(`${PRIVATE_API_URL}/identity/admin/token`, {
    headers: {
      Authorization: `Bearer ${locals.loginSession.token}`
    }
  });

  if(!res.ok)
    return {};

  const tokens: LoginToken[] = await res.json();
  return { tokens };
}