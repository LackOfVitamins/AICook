import { fail, type Actions } from "@sveltejs/kit";
import { superValidate } from "sveltekit-superforms";
import { formSchema } from "./schema";
import { PRIVATE_API_URL } from "$env/static/private";
import { zod } from "sveltekit-superforms/adapters";

export const actions: Actions = {
  default: async (event) => {
    const { locals, fetch } = event;
    const form = await superValidate(event, zod(formSchema));

    if (locals.loginSession == undefined) {
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