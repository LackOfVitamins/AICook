import { fail, type Actions } from "@sveltejs/kit";
import { superValidate } from "sveltekit-superforms/server";
import { formSchema } from "./schema";
import { PRIVATE_API_URL } from "$env/static/private";

export const actions: Actions = {
  default: async (event) => {
    const form = await superValidate(event, formSchema);
    const loginSession = event.locals.loginSession;

    if (loginSession == undefined) {
      return fail(401, {
        form
      });
    }

    if (!form.valid) {
      return fail(400, {
        form
      });
    }

    const apiCall = await fetch(`${PRIVATE_API_URL}/recipe/create`,
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
      form
    }
  }
};