import { PRIVATE_API_URL } from "$env/static/private";
import type { User } from "@/types/user";
import type { Handle, HandleFetch } from "@sveltejs/kit";

export const handle: Handle = async ({ event, resolve}) => {
  event.locals.toasts = [];

  const token = event.cookies.get('userToken');

  if(token != undefined)
  {
    const userRes = await fetch(`${PRIVATE_API_URL}/identity/user`, {
      headers: {
        Authorization: `Bearer ${token}`
      }
    });

    if(userRes.ok)
    {
      const user: User = await userRes.json();

      event.locals.loginSession = {
        token,
        user
      };
    }
  }

  const response = await resolve(event);
  return response;
}

export const handleFetch: HandleFetch = async ({ event, request, fetch }) => {
  const session = event.locals.loginSession;
  if(session != undefined)
    request.headers.set("Authorization", `Bearer ${session.token}`);

  return await fetch(request);
}