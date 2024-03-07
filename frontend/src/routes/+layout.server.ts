import type { LayoutServerLoad } from "./$types";
import { formSchema } from "$routes/recipe/create/schema";
import { superValidate } from "sveltekit-superforms";
import { zod } from "sveltekit-superforms/adapters";

export const load: LayoutServerLoad = async ({ locals }) => {
  const { loginSession } = locals

  return {
    form: await superValidate(zod(formSchema)),
    session: loginSession
  };
};

