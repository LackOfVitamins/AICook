import { PRIVATE_API_URL } from "$env/static/private"
import type { PageServerLoad } from "./$types";
import { superValidate } from "sveltekit-superforms";
import { formSchema } from "./schema";
import { zod } from "sveltekit-superforms/adapters";
import type { User } from "@/types/user";
import { fail, type Actions } from "@sveltejs/kit";

export const prerender = false;

export const load: PageServerLoad = async ({ locals, fetch }) => {
  if (locals.loginSession == undefined)
    return {};

  const res = await fetch(`${PRIVATE_API_URL}/identity/admin/user`, {
    headers: {
      Authorization: `Bearer ${locals.loginSession.token}` 
    }
  });

  if(!res.ok)
    return {};

  const users: User[] = await res.json();
  return { 
    users, 
    createForm: await superValidate(zod(formSchema)),
    updateForm: await superValidate(zod(formSchema))
  };
}

export const actions: Actions = {
  default: async (event) => {
    const form = await superValidate(event, zod(formSchema));
    const loginSession = event.locals.loginSession;

    if (loginSession == undefined) {
      return fail(401, {
        form
      });
    }

    if (!form.valid) {
      return fail(400, {
        form,
      });
    }
    
    const apiCall = await fetch(`${PRIVATE_API_URL}/identity/admin/user`,
    {
      method: "POST",
      headers: {
        Authorization: `Bearer ${loginSession.token}`,
        "Content-Type": "application/json"
      },
      body: JSON.stringify(form.data)
    });

    if (!apiCall.ok) {
      return fail(apiCall.status, {
        form
      });
    }
    
    return {
      form,
    };
  },
}