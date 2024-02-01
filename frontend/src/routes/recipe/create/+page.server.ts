import { fail, type Actions } from "@sveltejs/kit";
import { superValidate } from "sveltekit-superforms/server";
import { formSchema } from "./schema";
import { PUBLIC_API_URL } from "$env/static/public";

export const actions: Actions = {
  default: async (event) => {
    const form = await superValidate(event, formSchema);

    if (!form.valid) {
      return fail(400, {
        form
      });
    }

    const apiCall = await fetch(`${PUBLIC_API_URL}/recipe/create`,
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