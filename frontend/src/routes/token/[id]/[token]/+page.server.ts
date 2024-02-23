import { dev } from "$app/environment";
import { PRIVATE_API_URL } from "$env/static/private";
import { redirect, type ServerLoad } from "@sveltejs/kit";

export const load: ServerLoad = async ({ cookies, fetch, params }) => {
  const body = {
    id: params.id,
    token: params.token
  }
  
  const res = await fetch(`${PRIVATE_API_URL}/identity/token`, {
    method: "POST",
    headers: {
      "Content-Type": "application/json"
    },
    body: JSON.stringify(body)
  });

  if(res.ok) {
    const session: LoginSession = await res.json();

    if(session != undefined) {
      cookies.set(
        'userToken',
        session?.token, 
        {
          httpOnly: true,
          path: '/',
          secure: !dev,
          sameSite: 'strict',
          maxAge: 4 * 60 * 60
        }
      );
    }
  }

  redirect(302, '/');
}